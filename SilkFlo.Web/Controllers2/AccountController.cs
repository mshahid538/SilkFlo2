using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace SilkFlo.Web.Controllers
{
    public partial class AccountController
    {
        [HttpGet("/SignUp/userId/{id}")]
        public async Task<IActionResult> SignUpShort(string id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);

            // Guard Clause
            if (user == null)
                return Redirect("/");
            

            // Guard Clause
            if (user.IsEmailConfirmed)
                return Redirect("/account/SignIn");


            if (User.Identity is {IsAuthenticated: true})
            {
                SignOutCookies();
                return Redirect($"/SignUp/userId/{id}");
            }



            var model = new Services.Models.Account.SignUpShort
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return View(
                model);
        }

        [HttpPost("/SignUp/userId/{id}")]
        public async Task<IActionResult> SignUpShort(SilkFlo.Web.Services.Models.Account.SignUpShort model)
        {
            if (!ModelState.IsValid)
                return View(model);


            // Check password match
            if (model.Password != model.ConfirmPassword)
                ModelState.AddModelError("Error",
                    "The passwords do not match");

            if (!ModelState.IsValid)
                return View(model);

            var message = model.IsPasswordValid(true);
            if (!string.IsNullOrWhiteSpace(message))
            {
                ModelState.AddModelError("Error",
                    message);

                return View(model);
            }

            // GEt the user record
            var user = await _unitOfWork.Users.GetAsync(model.Id);

            if(user == null)
                ModelState.AddModelError("Error",
                    "You user account could not be found");

            if (!ModelState.IsValid)
                return View(model);


            if (user.IsLockedOut)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unauthorised sign in attempt");

                return View(model);
            }


            if (user.Email.IndexOf(Settings.PracticeAccountEmailSuffix, StringComparison.InvariantCulture) != -1)
            {
                var sPracticeAccountCanSignIn  = await GetApplicationSettingsAsync(Data.Core.Enumerators.Setting.PracticeAccountCanSignIn);
                bool.TryParse(sPracticeAccountCanSignIn, out var practiceAccountCanSignIn);

                if (!practiceAccountCanSignIn)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "Cannot not sign in with a practice account.To fix sign in as an administrator. Navigate to SilkFlo > Settings, then set 'Practice Account Can Sign-In' to true.");

                    return View(model);
                }
            }


            // Is Valid
            await _unitOfWork.UserRoles.GetForUserAsync(user);
            await _unitOfWork.Roles.GetRoleForAsync(user.UserRoles);


            if (user.UserRoles.Count == 0)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unauthorised sign in attempt");

                return View(model);
            }


            var passwordHash = _unitOfWork.GeneratePasswordHash(model.Password);
            user.PasswordHash = passwordHash;
            user.IsEmailConfirmed = true;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;



            await SetCookies(user, model.StaySignedIn, model.RememberMe, user.Email);

            await _unitOfWork.CompleteAsync();


            return RedirectToAction("Index",
                "Home");
        }


        public async Task SetCookies(
            Data.Core.Domain.User user,
            bool staySignedIn,
            bool rememberMe,
            string email
            )
        {

            Services.Models.Cookie.Save(user,
                staySignedIn,
                await GetExpiratoryDate(user),
                this);


            Add(Services.Cookie.RememberMe,
                rememberMe,
                DateTime.Now.AddDays(30),
                true);

            Add(Services.Cookie.StaySignedIn,
                staySignedIn,
                DateTime.Now.AddDays(30),
                true);

            if (staySignedIn || rememberMe)
                Add(Services.Cookie.Email,
                    email,
                    DateTime.Now.AddDays(30),
                    true);
            else
                Delete(Services.Cookie.Email);



            if (!string.IsNullOrWhiteSpace(user.ClientId))
                Add(Services.Cookie.ClientId,
                    user.ClientId,
                    DateTime.Now.AddDays(30),
                    false);
        }


        public void SignOutCookies()
        {
            HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);


            var rememberMe = false;
            if (Request.Cookies[Services.Cookie.RememberMe.ToString()] != null)
                rememberMe = bool.Parse(Request.Cookies[Services.Cookie.RememberMe.ToString()] ?? string.Empty);

            if (!rememberMe)
            {
                Delete(Services.Cookie.RememberMe);
                Delete(Services.Cookie.Email);
            }

            Delete(Services.Cookie.StaySignedIn);
            Delete(Services.Cookie.PasswordHash);
            Delete(Services.Cookie.ClientId);
        }

        [HttpGet("/SignUp/{referrerClientId}")]
        public async Task<IActionResult> SignUpWithReferrerCode(string referrerClientId)
        {
            if ((await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return await PageApiAsync("<h1>Please sign out before using this link.</h1>");


            return Ok();
        }
    }
}