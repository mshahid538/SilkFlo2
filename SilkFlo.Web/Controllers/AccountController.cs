/*********************************************************
       Code Generated By  .  .  .  .  Delaney's ScriptBot
       WWW .  .  .  .  .  .  .  .  .  www.scriptbot.io
       Template Name.  .  .  .  .  .  Project Green 3.0
       Template Version.  .  .  .  .  20210403 004
       Author .  .  .  .  .  .  .  .  Delaney

                          .___,
                       ___('v')___
                       `"-\._./-"'
                           ^ ^

 *********************************************************/

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.IO;
using System.Linq;
using System.Text;
using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Email;
using SilkFlo.Web.Models.Business;
using SilkFlo.Web.Models.Shop;
using SilkFlo.Web.Services;
using SilkFlo.Web.Services.Models.Account;
using SignInResult = SilkFlo.Data.Core.SignInResult;

namespace SilkFlo.Web.Controllers;

[Route("[controller]/[action]")]
public partial class AccountController : AbstractAPI
{
    public AccountController(IUnitOfWork unitOfWork,
        ViewToString viewToString,
        IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization)
    {
    }


    #region Access denied

    [HttpGet]
    public IActionResult AccessDenied()
    {
        if (Request.QueryString.Value != null
            && Request.QueryString.HasValue
            && Request.QueryString.Value.IndexOf("%2Fapi%2F", StringComparison.Ordinal) > -1)
            return Content("");

        ViewBag.HideNavigation = true;
        return View(
            "/Views/MessagePage.cshtml",
            new ViewModels.MessagePage
            {
                Title = "Access Denied",
                Message = "<span class=\"silkflo-text-danger\">You do not have access to this resource.<span>",
                ShowContinueButton = false
            });
    }

    #endregion

    #region ConfirmEmail

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string userId,
        string confirmationToken = null)
    {
        if (string.IsNullOrEmpty(confirmationToken))
            return RedirectToAction(
                "Index",
                "Home");

        var user = await _unitOfWork.Users.GetAsync(userId);

        if (user != null)
            if (user.EmailConfirmationToken == confirmationToken)
            {
                if (!string.IsNullOrWhiteSpace(user.EmailNew))
                {
                    user.Email = user.EmailNew.ToLower();
                    user.EmailNew = "";
                }

                user.IsEmailConfirmed = true;
                await _unitOfWork.CompleteAsync();

                ViewBag.HideNavigation = true;
                return View(
                    "/Views/MessagePage.cshtml",
                    new ViewModels.MessagePage
                    {
                        Title = "Email Confirmed",
                        Message = "Thank you for confirming your email."
                    });
            }

        return Redirect("/account/EmailNotSent");
    }

    #endregion

    [HttpGet("/account/ResendConfirmEmail/userId/{userId}")]
    [AllowAnonymous]
    public async Task<IActionResult> ResendConfirmEmail(string userId)
    {
        try
        {
            var user = await _unitOfWork.Users.GetAsync(userId);

            // Guard Clause
            if (user == null)
                return BadRequest("User not found");

            if (user.IsEmailConfirmed)
                return NoContent();

            user.EmailNew = user.Email;


            await SendEmailConfirmationMessageAsync(user);
            await _unitOfWork.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            ViewBag.HideNavigation = true;
            return View(
                "/Views/MessagePage.cshtml", 
                new ViewModels.MessagePage
                {
                    Title = "Email Confirmation Sent",
                    Message = "Open the email and click on the link to confirm the change."
                });
        }
        catch (Exception e)
        {
            _unitOfWork.Log(e);
            return BadRequest();
        }
    }



    #region ForgotPassword

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(
        ForgotPassword model)
    {
        if (!ModelState.IsValid)
            return View(model);


        var user = await _unitOfWork.Users.GetUsingEmailAsync(model.Email);

        if (user is not {IsEmailConfirmed: true})
            return RedirectToAction(
                nameof(ForgotPasswordConfirmation));


        // Create the email
        var token =
            await _unitOfWork.GeneratePasswordResetTokenAsync(user);

        var resetUrl =
            $"Account/ResetPassword?userId={user.Id}&resetToken={token}";


        BookMark[] bookmarks =
        {
            new("FIRSTNAME", user.FirstName),
            new("EMAIL", user.Email),
            new BookmarkLink("PATH", resetUrl, "reset your password here")
        };

        await Service.SendAsync(
            "Account Recovery - " + Data.Core.Settings.ApplicationName,
            Template.PasswordReset,
            new MailBox(Data.Core.Settings.ApplicationName, "hello@silkflo.com"),
            new MailBox(user.Fullname, user.Email),
            bookmarks);

        return RedirectToAction(
            nameof(ForgotPasswordConfirmation));
    }

    #endregion

    #region ForgotPasswordConfirmation

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    #endregion

    #region Change Password

    // /account/ChangePassword
    [Authorize]
    public IActionResult ChangePassword(
        string returnUrl,
        string userId)
    {
        return View(
            new ChangePassword
            {
                Id = userId,
                ReturnUrl = returnUrl
            });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(
        ChangePassword changePassword)
    {
        if (ModelState.IsValid)
        {
            var message = changePassword.IsPasswordValid();
            if (!string.IsNullOrWhiteSpace(message))
            {
                ModelState.AddModelError(
                    "Error",
                    message);
            }

            if (changePassword.IsMatched())
            {
                var user = await _unitOfWork.Users.GetAsync(changePassword.Id);

                if (user == null)
                {
                    ModelState.AddModelError("Error",
                        "Could not find User");
                    return View(changePassword);
                }

                if (_unitOfWork.VerifyPassword(changePassword.OldPassword, user.PasswordHash))
                {
                    user.PasswordHash = _unitOfWork.GeneratePasswordHash(changePassword.Password);

                    if (await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    {
                        if (changePassword.ReturnUrl == null) return View();

                        changePassword.ReturnUrl = changePassword.ReturnUrl.Replace("{and}", "&");

                        // Get the parameters
                        var parts = changePassword.ReturnUrl.Split('?');
                        var routeValues = parts[1].Split('&');
                        var userId = routeValues[0].Split('=');
                        var returnUrl = routeValues[1].Split('=');

                        return RedirectToAction(
                            "UserProfile", "account", new {userId = userId[1], returnUrl = returnUrl[1]});
                    }

                    ModelState.AddModelError("Error",
                        "Could not change the password");
                }
                else
                {
                    ModelState.AddModelError(
                        "Error",
                        "The current password is not correct");
                }
            }
            else
            {
                ModelState.AddModelError(
                    "Error",
                    "The passwords do not match");
            }
        }

        return View(changePassword);
    }

    #endregion

    #region Lockout

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Lockout()
    {
        ViewBag.HideNavigation = true;
        return View(
            "/Views/MessagePage.cshtml",
            new ViewModels.MessagePage
            {
                Title = "Locked Out",
                Message = "This account has been locked out, please try again later."
            });
    }

    #endregion


    #region ResetPassword

    // Example: https://localhost:5001/Account/ResetPassword?userId=TEST&resetToken=TEST
    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPassword(
        string resetToken = null)
    {
        if (string.IsNullOrEmpty(resetToken))
            return Redirect("/");

        var model = new ResetPassword {ResetToken = resetToken};

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(
        ResetPassword model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _unitOfWork.Users.GetUsingEmailAsync(model.Email);

        if (user == null)
            // Don't reveal that the user does not exist
            return RedirectToAction(
                nameof(ResetPasswordConfirmation));


        if (model.Email.IndexOf("practiceaccount.xyz", StringComparison.OrdinalIgnoreCase) > -1)
        {
            ModelState.AddModelError(
                string.Empty,
                "It is not possible to change the password of practice accounts.");
            return View();
        }


        var message = model.IsPasswordValid(true);
        if (!string.IsNullOrWhiteSpace(message))
        {
          
            ModelState.AddModelError(
                string.Empty,
                message);
            return View();
        }

        // Do the passwords match?
        if (!model.IsMatched())
        {
            ModelState.AddModelError(
                string.Empty,
                "The passwords provided do not match.");
            return View();
        }


        // Reset the password
        try
        {
            if (await _unitOfWork.ResetPasswordAsync(user,
                    model.ResetToken,
                    model.Password) == SignInResult.Succeeded)
                return RedirectToAction(
                    nameof(ResetPasswordConfirmation));
        }
        catch
        {
            ModelState.AddModelError(
                string.Empty,
                "There was an unknown error while trying to reset your password. Please try again later.");
        }

        ModelState.AddModelError(
            string.Empty,
            "Could not reset the password");

        return View();
    }

    #endregion

    #region ResetPasswordConfirmation

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }

    #endregion


    private async Task<DateTime> GetExpiratoryDate(User user)
    {
        var expiratoryDate = DateTime.MaxValue;

        // Client is null?
        if (user.Client == null)
        {
            expiratoryDate = DateTime.Now;
            return expiratoryDate;
        }


        // Client is not tenant
        if (user.Client.TypeId != Enumerators.ClientType.Client39.ToString()
            || user.Client.IsPractice)
            return expiratoryDate;


        // subscription valid?
        var subscription =
            await new Client(user.Client)
                .GetLastSubscriptionAsync(_unitOfWork);


        // subscription valid?
        if (subscription != null)
            return subscription.DateEnd ?? DateTime.MaxValue;


        expiratoryDate = DateTime.Now;
        return expiratoryDate;
    }


    #region Sign In

    // /account/signin
    [HttpGet]
    public async Task<IActionResult> SignIn()
    {
        if ((await AuthorizeAsync(Policy.Subscriber)).Succeeded)
            return Redirect("/Dashboard");

        try
        {
            var signIn = new SignIn();

            if (Request.Cookies[Cookie.RememberMe.ToString()] != null)
                signIn.RememberMe = bool.Parse(Request.Cookies[Cookie.RememberMe.ToString()] ?? string.Empty);

            if (Request.Cookies[Cookie.StaySignedIn.ToString()] != null)
                signIn.StaySignedIn = bool.Parse(Request.Cookies[Cookie.StaySignedIn.ToString()] ?? string.Empty);


            if (signIn.StaySignedIn)
            {
                var email = Request.Cookies[Cookie.Email.ToString()];
                var passwordHash = Request.Cookies[Cookie.PasswordHash.ToString()];

                var signInResult = _unitOfWork.ValidatePasswordHash(email,
                    passwordHash,
                    out var user);


                if (signInResult != SignInResult.Succeeded)
                    return View(signIn);


                // Is Valid
                await _unitOfWork.UserRoles.GetForUserAsync(user);
                await _unitOfWork.Roles.GetRoleForAsync(user.UserRoles);
                await _unitOfWork.BusinessClients.GetClientForAsync(user);


                var expiratoryDate = await GetExpiratoryDate(user);
                Services.Models.Cookie.Save(
                    user,
                    signIn.StaySignedIn,
                    expiratoryDate,
                    this);

                if (expiratoryDate < DateTime.Now)
                {
                    SignOutProcess();
                    return View(signIn);
                }

                return RedirectToAction(
                    "Index",
                    "Home");
            }

            if (signIn.RememberMe)
                signIn.Email = Request.Cookies[Cookie.Email.ToString()];

            return View(signIn);
        }
        catch
        {
            return View();
        }
    }


    // /account/signin
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn(
        SignIn model,
        [FromQuery(Name = "returnUrl")] string returnUrl = "")
    {
        // Guard Clause
        if (model == null)
        {
            ModelState.AddModelError(
                string.Empty,
                "The object is missing");

            model = new SignIn();
            return View(model);
        }


        if (!string.IsNullOrWhiteSpace(returnUrl))
            returnUrl = returnUrl.Trim();


        if (!string.IsNullOrWhiteSpace(returnUrl))
        {
            if (returnUrl.IndexOf("?returnUrl", StringComparison.OrdinalIgnoreCase) == 0)
                returnUrl = returnUrl.Substring(10);

            if (returnUrl.IndexOf("=", StringComparison.OrdinalIgnoreCase) == 0)
                returnUrl = returnUrl.Substring(1);
        }

        // Guard Clause
        if (!ModelState.IsValid)
            return View(model);


        User user = null;

        var isPracticeUser = false;
        var signInResult = SignInResult.Failed;
        if (model.Email.IndexOf(Settings.PracticeAccountEmailSuffix, StringComparison.OrdinalIgnoreCase) == -1)
        {
            // This is NOT a practice account
            signInResult = _unitOfWork.ValidateCredentials(
                model.Email,
                model.Password,
                out user);
        }
        else
        {
            // We have a practice account
            isPracticeUser = true;
            var settings = await GetApplicationSettingsAsync();

            // Are we allowing practice account to sign in?
            // (Head over to Settings in the UI to change practice account sign in.
            if (settings.PracticeAccountCanSignIn
                && settings.PracticeAccountPassword == model.Password)
            {
                user = await _unitOfWork.Users.GetByEmailAsync(model.Email);

                if (user != null)
                {
                    signInResult = SignInResult.Succeeded;

                    if (user.IsLockedOut)
                        signInResult = SignInResult.IsLockedOut;

                    if (!user.IsEmailConfirmed)
                        signInResult = SignInResult.EmailNotConfirmed;
                }
            }
        }


        // Guard Clause
        if (user == null)
        {
            ModelState.AddModelError(
                string.Empty,
                "Invalid sign in attempt");

            return View(model);
        }


        switch (signInResult)
        {
            // Is Valid and email is NOT confirmed
            case SignInResult.Succeeded when !user.IsEmailConfirmed:
            {
                // Redirect to Sign Up Confirmation
                var callbackUrl =
                    Url.EmailConfirmationLink(
                        user.Id,
                        user.EmailConfirmationToken, Request.Scheme);


                BookMark[] bookmarks =
                {
                    new("FIRSTNAME", user.FirstName),
                    new BookmarkLink("PATH", callbackUrl, "")
                };

                await Service.SendAsync(
                    "Confirm Your Email - " + Data.Core.Settings.ApplicationName,
                    Template.EmailConfirmation,
                    new MailBox(Data.Core.Settings.ApplicationName, "hello@silkflo.com"),
                    new MailBox(user.Fullname, user.Email),
                    bookmarks);

                var signUpConfirmation =
                    new SignUpConfirmation(
                        user.Email,
                        returnUrl);

                return View(
                    "SignUpConfirmation",
                    signUpConfirmation);
            }


            // Is Valid
            case SignInResult.Succeeded:
                {
                    await _unitOfWork.UserRoles.GetForUserAsync(user);
                    await _unitOfWork.Roles.GetRoleForAsync(user.UserRoles);

                    returnUrl = await SignInAsync(
                        user,
                        model,
                        returnUrl,
                        isPracticeUser);

                    if(returnUrl == "/account/signin")
                        return View(model);

                    if (returnUrl == "/Account/SubscriptionExpired")
                        return Redirect("/Account/SubscriptionExpired");


                    // Exit
                    if (returnUrl == null
                    || !Url.IsLocalUrl(returnUrl))
                    return RedirectToAction("Index",
                        "Home");


                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);


                    return RedirectToAction("Index",
                        "Home");
                }


            case SignInResult.IsLockedOut:
                return RedirectToAction(nameof(Lockout));


            case SignInResult.EmailNotConfirmed:

                //var url = "https://" + GetDomainName() + "/account/ResendConfirmEmail/userId/" + user.Id;
                var url = "/account/ResendConfirmEmail/userId/" + user.Id;

                ModelState.AddModelError(
                    string.Empty,
                    "Please confirm your email address before attempting to sign in. <a style=\"color: var(--bs-dark);\" href=\"" + url + "\">Resend email confirmation message</a>.");

                return View(model);


            case SignInResult.Failed:
            case SignInResult.Expired:
            case SignInResult.IsNotAllowed:
            default:
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid sign in attempt");

                return View(model);
        }
    }
    #endregion

    #region Sign Out

    public new IActionResult SignOut()
    {
        SignOutProcess();

        return Redirect("/");
    }

    #endregion


    #region User Profile

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> UserProfile
    (string userId,
        string returnUrl,
        string newEmail = "")
    {
        var signedInUserId = GetUserId();

        if (signedInUserId != userId)
            return Redirect(
                "/account/accessDenied");

        var core = await _unitOfWork.Users.GetAsync(userId);

        if (core == null)
            return Redirect(
                "/Index");

        var model = new Models.User();

        // We have to clone the core as the user my have changed the email address
        // and clicked cancel on the EmailChanged view.
        model.GetCore().Update(core);
        model.GetCore().Id = core.Id;

        if (!string.IsNullOrEmpty(newEmail))
            // New email address from the returnURL from EmailChanged.
            model.Email = newEmail;

        ViewBag.ReturnUrl = returnUrl;

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UserProfile(
        Models.User user,
        string returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(user);

        var isError = false;
        if (string.IsNullOrEmpty(user.Email))
        {
            ModelState.AddModelError(
                "Error",
                "The email address is not present");

            isError = true;
        }

        if (string.IsNullOrEmpty(user.FirstName)
            && string.IsNullOrEmpty(user.LastName))
        {
            ModelState.AddModelError(
                "Error",
                "Both the first and last names are not present.");

            isError = true;
        }

        var userId = GetUserId();

        if (await _unitOfWork.Users
                .SingleOrDefaultAsync(x => x.Email == user.Email
                                           && x.Id != userId) != null)
        {
            ModelState.AddModelError(
                "Error",
                "Another user is using this email address.");

            isError = true;
        }

        if (isError)
            return View(user);

        var coreInDataStore = await _unitOfWork.Users.GetAsync(userId);

        if (coreInDataStore.Email != user.Email)
        {
            if (!string.IsNullOrEmpty(returnUrl))
                returnUrl = returnUrl.Replace("&", "{and}");

            var url = string.Concat("/account/EmailChanged?returnUrlIsSent=",
                returnUrl,
                "&userId=",
                user.Id,
                "&email=",
                user.Email,
                "&oldEmail=",
                coreInDataStore.Email);

            return Redirect(url);
        }

        await _unitOfWork.Users.AddAsync(user.GetCore());

        if (await _unitOfWork.CompleteAsync() != DataStoreResult.Success)
            return View(user);


        await _unitOfWork.UserRoles.GetForUserAsync(user.GetCore());
        await _unitOfWork.Roles.GetRoleForAsync(user.GetCore().UserRoles);


        Services.Models.Cookie.Save(
            user.GetCore(),
            false,
            await GetExpiratoryDate(user.GetCore()),
            this);

        if (returnUrl == null)
            return RedirectToAction(
                "Index",
                "Home");


        returnUrl = returnUrl.Replace("{and}", "&");

        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);


        return RedirectToAction(
            "Index",
            "Home");
    }

    #endregion

    #region Email Changed
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmailChanged(string userId,
        string confirmationToken = null)
    {
        if (string.IsNullOrEmpty(confirmationToken))
            return RedirectToAction(
                "Index",
                "Home");

        var user =
            await
                _unitOfWork.Users
                    .SingleOrDefaultAsync(x => x.Id == userId
                                               && x.EmailConfirmationToken == confirmationToken);

        if (user != null)
        {
            user.IsEmailConfirmed = true;
            user.Email = user.EmailNew;
            user.EmailNew = "";

            await _unitOfWork.CompleteAsync();
            ViewBag.HideNavigation = true;
            return View(
                "/Views/MessagePage.cshtml",
                new ViewModels.MessagePage
                {
                    Title = "Email Change Confirmed",
                    Message = "Thank you for confirming your new email address."
                });
        }
        
        return Redirect("/account/EmailNotSent");
    }

    [HttpGet("/account/EmailNotSent")]
    public IActionResult EmailNotSent()
    {
        ViewBag.HideNavigation = true;
        return View(
            "/Views/MessagePage.cshtml",
            new ViewModels.MessagePage
            {
                Title = "Email not Confirmed",
                Message = "The confirmation message is out of date."
            });
    }

    public IActionResult CancelEmailChanged(
        string returnUrlIsSent = null,
        string userId = "",
        string newEmail = "")
    {
        var url = $"/account/UserProfile?userId={userId}&returnUrl={returnUrlIsSent}&newEmail={newEmail}";
        return Redirect(url);
    }

    #endregion

    [HttpGet("/account/MuteEmail/{id}")]
    public async Task<IActionResult> MuteEmail(string id)
    {
        var core =
            await _unitOfWork.Users
                .SingleOrDefaultAsync(x => x.Id == id);

        if (core == null)
            return View(null);

        core.IsMuted = true;

        await _unitOfWork.CompleteAsync();

        return View(new Models.User(core));
    }

    public IActionResult Cancel(
        string returnUrl = null)
    {
        if (returnUrl == null)
            return RedirectToAction(
                "Index",
                "Home");


        returnUrl = returnUrl.Replace("{and}", "&");

        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction(
            "Index",
            "Home");
    }


    [HttpPost("/api/account/IsEmailAlreadyUsed")]
    public async Task<IActionResult> IsEmailAlreadyUsed()
    {
        try
        {
            using var reader
                = new StreamReader(
                    Request.Body,
                    Encoding.UTF8,
                    true,
                    1024,
                    true);

            var email = await reader.ReadToEndAsync();

            var user = await _unitOfWork.Users.GetByEmailAsync(email);

            if (user == null)
                return Ok(false);
        }
        catch
        {
            // ignored
        }

        return Ok(true);
    }

    [Route("/Account/SubscriptionExpired")]
    public IActionResult SubscriptionExpired()
    {
        ViewBag.HideNavigation = true;
        Delete(Cookie.IsPractice);
        return View(
            "/Views/MessagePage.cshtml",
            new ViewModels.MessagePage
            {
                Title = "Subscription Out of Date",
                Message = "<span class=\"silkflo-text-danger\">Your subscription has expired.<br/>Not to worry, your data is safe.<br/>You can re-subscribe at anytime by clicking <a href=\"/Pricing\">subscribe</a>.</span>",
                ShowContinueButton = false
            });
    }
}