using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkFlo.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Email;
using SilkFlo.Web.ViewModels;

namespace SilkFlo.Web.APIControllers
{
    public class UserController : Controllers.AbstractAPI
    {
        public UserController(IUnitOfWork unitOfWork,
                              Services.ViewToString viewToString,
                              IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/api/user/SearchProcessOwner/{text}")]
        [HttpGet("/api/user/SearchProcessOwner")]

        public async Task<IActionResult> GetSearchProcessOwners(string text = "")
        {
            try
            {
                var client = await GetClientAsync();

                if (client == null)
                    return NegativeFeedback();


                await _unitOfWork.Users.GetForClientAsync(client);
                await _unitOfWork.UserRoles.GetForUserAsync(client.Users);



                var models = Models.User.Create(client.Users);


                if (string.IsNullOrWhiteSpace(text))
                {
                    models = models.Where(x => x.Status == Models.Status.Active).ToList();
                }
                else
                {

                    text = text.Trim().ToLower();

                    var exactMatchRequired = false;
                    if (text.IndexOf("\"", StringComparison.Ordinal) == 0
                        && text.IndexOf("\"", 1, StringComparison.Ordinal) > 1)
                    {
                        exactMatchRequired = true;
                        text = text.Substring(1);
                        text = text.Substring(0, text.Length - 1);
                    }


                    if (exactMatchRequired)
                        models = models.Where(x => x.Fullname.ToLower() == text
                                                && x.Status == Models.Status.Active).ToList();
                    else
                        models = models.Where(x => x.Fullname.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   && x.Status == Models.Status.Active).ToList();
                }



                var html = await _viewToString.PartialAsync("Shared/user/_SearchSimpleResult.cshtml", models);

                return Content(html);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                return Content("<h3 class=\"text-danger\">Server error Requesting content. url: /api/user/SearchProcessOwner/<h3>");
            }
        }


        [HttpGet("/api/user/SearchAccountOwner/{text}")]
        [HttpGet("/api/user/SearchAccountOwner")]
        public async Task<IActionResult> GetSearchAccountOwners(string text = "")
        {
            if (!(await AuthorizeAsync("Manage Tenant Settings")).Succeeded)
                return NegativeFeedback();

            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("");

                var userId = GetUserId();

                if (userId == null)
                    return BadRequest();

                var user = await _unitOfWork.Users.GetAsync(userId);
                if (user == null)
                    return BadRequest();

                text = text.ToLower();


                var tenant = await GetClientAsync();

                User[] cores;
                
                if(tenant.AllowGuestSignIn)
                    cores = (await _unitOfWork.Users
                    .FindAsync(x => x.Fullname.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                    && x.ClientId == tenant.Id
                                    && x.IsEmailConfirmed
                                    && !x.IsLockedOut
                                    && x.Email.Contains("@" + tenant.Website, StringComparison.OrdinalIgnoreCase))).ToArray();
                else
                    cores = (await _unitOfWork.Users
                        .FindAsync(x => x.Fullname.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                        && x.IsEmailConfirmed
                                        && !x.IsLockedOut
                                        && x.ClientId == tenant.Id)).ToArray();


                await _unitOfWork.UserRoles.GetForUserAsync(cores);

                cores = cores.Where(x => x.UserRoles.Count > 0).ToArray();

                var models = cores.Select(core => new Models.User(core)).ToList();


                var html = await _viewToString.PartialAsync("Shared/user/_SearchSimpleResult.cshtml", models);

                return Content(html);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                return Content("<h3 class=\"text-danger\">Server error Requesting content<h3>");
            }
        }


        [HttpGet("/api/user/SearchAccountOwner/ClientId/{clientId}/{text}")]
        public async Task<IActionResult> SearchAccountOwnerForClient(string clientId, string text)
        {
            if (!(await AuthorizeAsync("Manage Tenant Settings")).Succeeded)
                return NegativeFeedback();

            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("");

                var userId = GetUserId();

                if (userId == null)
                    return BadRequest();

                var user = await _unitOfWork.Users.GetAsync(userId);
                if (user == null)
                    return BadRequest();

                text = text.ToLower();


                var cores = (await _unitOfWork.Users
                                   .FindAsync(x => x.Fullname.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   && x.ClientId == clientId)).ToArray();

                await _unitOfWork.UserRoles.GetForUserAsync(cores);

                var models = cores.Select(core => new Web.Models.User(core)).ToList();


                var html = await _viewToString.PartialAsync("Shared/user/_SearchSimpleResult.cshtml", models);

                return Content(html);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                return Content("<h3 class=\"text-danger\">Server error Requesting content<h3>");
            }
        }





        [HttpGet("/api/user/search/{text}")]
        [HttpGet("/api/user/search/{text}/page/{pageIndex}/PageCount/{pageCount}")]
        [HttpGet("/api/user/search/{text}/page/{pageIndex}/PageCount/{pageCount}/PageSize/{pageSize}")]
        [HttpGet("/api/user/search/{text}/PageSize/{pageSize}")]
        public async Task<IActionResult> Search(string text,
                                                int pageIndex = 1,
                                                int pageCount = 0,
                                                int pageSize = 10)
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("");

                var userId = GetUserId();

                if (userId == null)
                    return BadRequest();

                var user = await _unitOfWork.Users.GetAsync(userId);
                if (user == null)
                    return BadRequest();

                text = text.Trim().ToLower();


                var tenant = await GetClientAsync();


                var cores = (await _unitOfWork.Users
                                       .FindAsync(x => x.Fullname.ToLower().IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                       && x.ClientId == tenant.Id)).ToArray();

                var models = new List<Models.User>();

                foreach (var core in cores)
                    models.Add(new Web.Models.User(core));


                var viewModel = new UserSearchResult
                {
                    Users = models,
                    SearchText = text
                };


                string html = await _viewToString.PartialAsync("Shared/user/_SearchResult.cshtml", viewModel);

                return Content(html);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                return Content("<h3 class=\"text-danger\">Server error Requesting content<h3>");
            }
        }

        [HttpGet("/api/User/GetUserProfileSummary")]
        public async Task<IActionResult> GetUserProfileSummary()
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber, false)).Succeeded)
                    return Content("");

                // Get the select tenantId
                var userId = GetUserId();

                // Guard Clause
                if (userId == null)
                    return BadRequest();

                var user = await _unitOfWork.Users.GetAsync(userId);

                // Guard Clause
                if (user == null)
                    return BadRequest();

                var model = new Models.User(user);

                string html = await _viewToString.PartialAsync("Shared/_UserProfileSummary.cshtml", model);

                return Content(html);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                return Content("<h3 class=\"text-danger\">Server error Requesting content<h3>");
            }
        }

        [HttpGet("/api/User/GetUserProfileModal")]
        public async Task<IActionResult> GetUserProfileModal()
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber, false)).Succeeded)
                    return Content("");


                // Get the select tenantId
                var userId = GetUserId();

                // Guard Clause
                if (userId == null)
                    return Content("");

                var user = await _unitOfWork.Users.GetAsync(userId);

                // Guard Clause
                if (user == null)
                    return Content("");

                var model = new Models.User(user);

                await _unitOfWork.BusinessDepartments.GetDepartmentForAsync(user);
                await _unitOfWork.BusinessLocations.GetLocationForAsync(user);
                await _unitOfWork.Users.GetManagerForAsync(user);
                await _unitOfWork.BusinessClients.GetClientForAsync(user);

                
                model.EmailNew = model.Email;

                var locations = (await _unitOfWork.BusinessLocations.FindAsync(x => x.ClientId == user.ClientId)).ToList();
                foreach (var location in locations)
                    model.Locations.Add(new Models.Business.Location(location));

                var departments = (await _unitOfWork.BusinessDepartments.FindAsync(x => x.ClientId == user.ClientId)).ToList();
                foreach (var department in departments)
                    model.Departments.Add(new Models.Business.Department(department));


                var managers = (await _unitOfWork.Users.FindAsync(x => x.ClientId == user.ClientId
                                                                    && x.Id != user.Id)).ToList();
                foreach (var manager in managers)
                    model.Managers.Add(new Models.User(manager));


                var viewModel = new ViewModels.Account.UserProfile
                {
                    User = model,
                };

                if (model.Client == null)
                {
                    var client = await GetClientAsync(false);
                    if (client != null && client.IsPractice)
                        client = await _unitOfWork.BusinessClients.SingleOrDefaultAsync(x => x.PracticeId == client.Id);

                    if(client != null)
                        model.Client = new Models.Business.Client(client);
                }


                viewModel.AllowGuestEmail = model.Client != null 
                       && model.Client.AllowGuestSignIn
                       && !model.Email.Contains("@" + model.Client.Website, StringComparison.OrdinalIgnoreCase);

                if (viewModel.AllowGuestEmail)
                {
                    model.EmailPrefix = model.Email;
                    model.EmailNewPrefix = model.EmailPrefix;
                }

                var html = await _viewToString.PartialAsync("Shared/Account/_UserProfile.cshtml", viewModel);

                return Content(html);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                return Content("<h3 class=\"text-danger\">Server error Requesting content<h3>");
            }
        }

        [HttpPost("/api/User/PostProfile")]
        public async Task<IActionResult> PostProfile([FromBody] Models.User model)
        {
            var feedback = new Feedback
            {
                NamePrefix = "UserProfile.User."
            };


            // Guard Clause
            if (!(await AuthorizeAsync(Policy.Subscriber, false)).Succeeded)
            {
                feedback.DangerMessage("Unauthorised");
                return BadRequest(feedback);
            }

            try
            {
                // Guard Clause
                if (model == null)
                {
                    feedback.DangerMessage("Object missing");
                    return BadRequest(feedback);
                }


                // Guard Clause
                if (string.IsNullOrWhiteSpace(model.Id))
                {
                    feedback.DangerMessage("Id missing");
                    return BadRequest(feedback);
                }


                // Guard Clause
                if (string.IsNullOrEmpty(model.EmailNewPrefix))
                {
                    feedback.Elements.Add("Email", "The email address is not present.");
                    feedback.IsValid = false;
                }


                var core = await _unitOfWork.Users.GetAsync(model.Id);

                // Guard Clause
                if (core == null)
                {
                    feedback.DangerMessage("Cannot find user on the database");
                    return BadRequest(feedback);
                }




                await _unitOfWork.BusinessClients.GetClientForAsync(core);

                // Guard Clause
                if (core.Client == null)
                {
                    feedback.DangerMessage("Cannot find client on the database");
                    return BadRequest(feedback);
                }


                bool checkIsCorperate = false;
                //if (!core.Client.AllowGuestSignIn &&
                //    !core.Email.Contains("@" + core.Client.Website, StringComparison.OrdinalIgnoreCase))
                //{
                //    checkIsCorperate = true;
                //    model.Email = model.EmailPrefix.ToLower() + "@" + core.Client.Website.ToLower();
                //    if (model.EmailNewPrefix != null)
                //        model.EmailNew = model.EmailNewPrefix.ToLower() + "@" + core.Client.Website.ToLower();
                //}
                //else
                //{
                //    model.Email = model.EmailPrefix;
                //    model.EmailNew = model.EmailNewPrefix;

                //}


                if (core.Email.Contains("@" + core.Client.Website, StringComparison.OrdinalIgnoreCase))
                {
                    checkIsCorperate = true;
                    model.Email = model.EmailPrefix.ToLower() + "@" + core.Client.Website.ToLower();
                    if (model.EmailNewPrefix != null)
                        model.EmailNew = model.EmailNewPrefix.ToLower() + "@" + core.Client.Website.ToLower();
                }
                else if (core.Client.AllowGuestSignIn)
                {
                    model.Email = model.EmailPrefix;
                    model.EmailNew = model.EmailNewPrefix;
                }


                //Validate new email
                if (string.IsNullOrWhiteSpace(model.EmailNewPrefix))
                {
                    feedback.Add("EmailNewPrefix", "Email address is missing.");
                    feedback.IsValid = false;
                }
                else
                {
                    var message = await Email.Service.ValidateEmailAsync(model.EmailNew, checkIsCorperate);
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        feedback.Add("EmailNewPrefix", message);
                        feedback.IsValid = false;
                    }
                    else
                    {
                        var clone = await _unitOfWork.Users.SingleOrDefaultAsync(x =>
                            string.Equals(x.Email, model.EmailNew, StringComparison.CurrentCultureIgnoreCase) && x.Id != model.Id);

                        if (clone != null)
                        {
                            feedback.Add("EmailNewPrefix", "Email address already in use.");
                            feedback.IsValid = false;
                        }
                    }
                }

                ModelState.Clear();
                TryValidateModel(model);
                feedback = GetFeedback(ModelState, feedback);



                if (!feedback.IsValid)
                    return BadRequest(feedback);



                if (string.IsNullOrEmpty(model.FirstName)
                && string.IsNullOrEmpty(model.LastName))
                {
                    ModelState.AddModelError(
                        "Error",
                        "Both the first and last names are not present.");

                    feedback.IsValid = false;
                }


                if (await _unitOfWork.Users
                   .SingleOrDefaultAsync(x => x.Email == model.EmailNew
                                           && x.Id != model.Id) != null)
                {
                    ModelState.AddModelError(
                        "Error",
                        "Another user is using this email address.");

                    feedback.IsValid = false;
                }


                if (!feedback.IsValid)
                    return BadRequest(feedback);




                // Update user properties
                core.FirstName = model.FirstName;
                core.LastName = model.LastName;
                core.ManagerId = model.ManagerId;
                core.DepartmentId = model.DepartmentId;
                core.JobTitle = model.JobTitle;
                core.LocationId = model.LocationId;
                core.About = model.About;

                // Send Email Change message
                if (core.Email.ToLower() != model.EmailNew)
                {
                    core.EmailNew = model.EmailNew;
                    core.EmailConfirmationToken = Guid.NewGuid().ToString();
                    await SendEmailChangedConfirmationMessageAsync(core);
                }



                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                feedback.DangerMessage("Error Saving user profile");
                feedback.IsValid = false;
                return BadRequest(feedback);
            }
        }


        [Route("/api/Users/GetUserPasswordModal")]
        public async Task<IActionResult> GetUserPasswordModal()
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber, false)).Succeeded)
                    return Content("");


                // Get the select tenantId
                var userId = GetUserId();

                // Guard Clause
                if (userId == null)
                    return Content("");

                var user = await _unitOfWork.Users.GetAsync(userId);

                // Guard Clause
                if (user == null)
                    return Content("");


                var model = new Models.User(user);


                string html = await _viewToString.PartialAsync("Shared/Account/_ChangePassword.cshtml", model);

                return Content(html);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                return Content("");
            }
        }


        [HttpPost("/api/User/PostPasswordChange")]
        public async Task<IActionResult> PostPasswordChange(
            [FromBody] Services.Models.Account.ChangePassword model)
        {
            var feedback = new Feedback
            {
                NamePrefix = "User."
            };

            if (!(await AuthorizeAsync(Policy.Subscriber, false)).Succeeded)
            {
                feedback.DangerMessage("Unauthorised");
                return BadRequest(feedback);
            }

            try
            {
                feedback = GetFeedback(ModelState, feedback);

                if (!feedback.IsValid)
                    return BadRequest(feedback);


                var message = model.IsPasswordValid();
                if (!string.IsNullOrWhiteSpace(message))
                {
                    feedback.WarningMessage(message);
                    return BadRequest(feedback);
                }

                if (model.IsMatched())
                {
                    var user = await _unitOfWork.Users.GetAsync(model.Id);

                    if (user == null)
                    {
                        feedback.DangerMessage("Could not find User");
                        return BadRequest(feedback);
                    }


                    if (user.Email.IndexOf("practiceaccount.xyz", StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        feedback.DangerMessage("It is not possible to change the password of practice accounts");
                        return BadRequest(feedback);
                    }



                    if (_unitOfWork.VerifyPassword(model.OldPassword, user.PasswordHash))
                    {
                        user.PasswordHash = _unitOfWork.GeneratePasswordHash(model.Password);

                        if (await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                        {
                            return Ok();
                        }

                        feedback.DangerMessage("Database Error: Could not change the password");
                        return BadRequest(feedback);
                    }


                    feedback.WarningMessage("The current password is not correct");
                    return BadRequest(feedback);
                }


                feedback.WarningMessage("The passwords do not match");
                return BadRequest(feedback);

            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                feedback.DangerMessage("Server error Requesting content");
                return BadRequest(feedback);
            }
        }


        [HttpGet("/api/user/searchCollaborators/{text}")]
        public async Task<IActionResult> SearchCollaborators(string text)
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("");

                var userId = GetUserId();

                if (userId == null)
                    return Content("");

                var user = await _unitOfWork.Users.GetAsync(userId);
                if (user == null)
                    return Content("");

                var tenant = await GetClientAsync();

                var cores = await _unitOfWork.Users
                    .FindAsync(x => x.Fullname.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                    && x.ClientId == tenant.Id);



                var models = new List<Models.User>();

                foreach (var core in cores)
                    models.Add(new Web.Models.User(core));


                var html = await _viewToString.PartialAsync("Shared/user/_SearchCollaboratorResult.cshtml", models);

                return Content(html);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                return Content("<h3 class=\"text-danger\">Server error Requesting content<h3>");
            }
        }


        [HttpGet("/api/User/GetModal")]
        [HttpGet("/api/User/GetModal/id/{id}")]
        public async Task<IActionResult> GetModal(string id)
        {
            return await CreateModalView(id);
        }


        private async Task PrepareForModal(Models.User model,
                                           Models.Business.Client client)
        {
            try
            {
                var departments = (await _unitOfWork.BusinessDepartments.FindAsync(x => x.ClientId == client.Id)).ToList();
                model.Departments = Models.Business.Department.Create(departments, true);
                model.EmailSuffix = "@" + client.Website;
                model.Client = client;


                if (client.AllowGuestSignIn
                    && !string.IsNullOrWhiteSpace(model.Email)
                    && model.Email.Contains("@", StringComparison.Ordinal)
                    && !model.Email.Contains("@" + client.Website, StringComparison.OrdinalIgnoreCase))
                {
                    model.GuestEmail = model.Email;
                    var parts = model.Email.Split("@");
                    if(parts.Length == 2)
                        model.EmailPrefix = parts[0];
                }
                else
                    model.IsBusinessEmail = true;


                if (model.Roles.Any())
                    return;

                IEnumerable<Role> roles;

                if (client.IsAgency)
                    roles = await _unitOfWork.Roles.FindAsync(x => (x.Name.IndexOf("agency", StringComparison.InvariantCultureIgnoreCase) > -1
                                                                    || x.Name.IndexOf("account owner", StringComparison.InvariantCultureIgnoreCase) == 0)
                                                                   && x.Name.IndexOf("can backup dataset", StringComparison.InvariantCultureIgnoreCase) == -1
                                                                   && x.Name.IndexOf("administrator", StringComparison.InvariantCultureIgnoreCase) != 0
                                                                   && x.Name.IndexOf("uat tester", StringComparison.InvariantCultureIgnoreCase) == -1);

                else
                    roles = await _unitOfWork.Roles.FindAsync(x => (x.Name.IndexOf("agency", StringComparison.InvariantCultureIgnoreCase) == -1
                                                                    || x.Name.IndexOf("account owner", StringComparison.InvariantCultureIgnoreCase) == 0)
                                                   && x.Name.IndexOf("can backup dataset", StringComparison.InvariantCultureIgnoreCase) == -1
                                                   && x.Name.IndexOf("administrator", StringComparison.InvariantCultureIgnoreCase) == -1
                                                   && x.Name.IndexOf("uat tester", StringComparison.InvariantCultureIgnoreCase) == -1);

                model.Roles = Models.Role.Create(roles);


                await _unitOfWork.UserRoles.GetForUserAsync(model.GetCore());
                foreach (var role in model.Roles)
                    foreach (var userRole in model.GetCore().UserRoles)
                    {
                        if (role.Id != userRole.RoleId) 
                            continue;

                        role.IsSelected = true;
                        break;
                    }
            }
            catch (Exception ex)
            {
                Log(ex);
                throw;
            }
        }


        private async Task<IActionResult> CreateModalView(string id)
        {
            if (!(await AuthorizeAsync(Policy.ManageTenantUsers)).Succeeded
            && !(await AuthorizeAsync(Policy.ManageAgencyUsers)).Succeeded)
                return NegativeFeedback();

            try
            {
                var clientCore = await GetClientAsync();

                if (clientCore == null)
                    return NegativeFeedback();

                var client = new Models.Business.Client(clientCore);


                if ((await AuthorizeAsync(Policy.ManageTenantUsers)).Succeeded && client.IsAgency
                 && (await AuthorizeAsync(Policy.ManageAgencyUsers)).Succeeded && !client.IsAgency)
                    return NegativeFeedback();


                var core = string.IsNullOrWhiteSpace(id) ? 
                    new User() : 
                    await _unitOfWork.Users.GetAsync(id);


                var model = new Models.User(core);

                await PrepareForModal(model, client);


                var html = await _viewToString.PartialAsync("Shared/user/_Modal.cshtml", model);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("<h3 class=\"text-danger\">Server error Requesting content<h3>");
            }
        }

        [HttpPost("/api/Models/User/Save")]
        public async Task<IActionResult> SaveUser([FromBody] Models.User model)
        {
            var feedback = new Feedback
            {
                NamePrefix = "User."
            };

            // Guard Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantUsers)).Succeeded
            && !(await AuthorizeAsync(Policy.ManageAgencyUsers)).Succeeded)
            {
                feedback.DangerMessage("Unauthorised");
                return BadRequest(feedback);
            }


            try
            {
                // Guard Clause
                if (model == null)
                {
                    feedback.DangerMessage("No content supplied");
                    return BadRequest(feedback);
                }


                var clientCore = await GetClientAsync();


                // Guard Clause
                if (clientCore == null)
                {
                    feedback.DangerMessage("Unauthorised");
                    return BadRequest(feedback);
                }


                // Guard Clause
                if (clientCore.IsPractice
                    && model.IsNew)
                {
                    feedback.DangerMessage("You cannot create a new user on a practice account");
                    return BadRequest(feedback);
                }


                var client = new Models.Business.Client(clientCore);

                if (client.AllowGuestSignIn)
                {
                    if(model.IsBusinessEmail)
                        model.Email = model.EmailPrefix.ToLower() + "@" + client.Website.ToLower();
                    else
                    {
                        var message = await Service.ValidateEmailAsync(model.GuestEmail, false);
                        if (string.IsNullOrWhiteSpace(message))
                        {
                            model.Email = model.GuestEmail;
                        }
                        else
                        {
                            feedback.Elements.Add("Email", message);
                            return BadRequest(feedback);
                        }

                        var removeRoles = 
                            model.Roles
                                 .Where(x => x.Id == ((int) Enumerators.Role.AccountOwner).ToString())
                                 .ToList();
                            
                        foreach (var role in removeRoles)
                            model.Roles.Remove(role);
                    }
                }
                else
                    model.Email = model.EmailPrefix.ToLower() + "@" + client.Website.ToLower();


                var core = await _unitOfWork.Users.GetAsync(model.Id);

                if (core == null
                 && !string.IsNullOrWhiteSpace(model.Id))
                {
                    // We have a bogus id
                    model.Id = null;

                    feedback.DangerMessage("Unauthorised");
                    return BadRequest(feedback);
                }


                var str = await CanAddAuthorisedUser(client, model.Roles.ToArray(), model.Id);
                if (!string.IsNullOrWhiteSpace(str))
                {
                    feedback.DangerMessage(str);
                    return BadRequest(feedback);
                }


                str = await CanAddStandardUser(client, model.Roles.ToArray(), model.Id);
                if (!string.IsNullOrWhiteSpace(str))
                {
                    feedback.DangerMessage(str);
                    return BadRequest(feedback);
                }


                feedback.Message = "";

                // Validate
                ModelState.Clear();
                TryValidateModel(model);
                feedback = GetFeedback(ModelState, feedback);
                feedback = await model.CheckUniqueAsync(_unitOfWork, feedback);


                model.Roles = model.Roles.Where(x => x.IsSelected).ToList();


                // Validate the email format
                var environment = Security.Settings.GetEnvironment();

                if (!feedback.Elements.ContainsKey("Email")
                   && !string.IsNullOrWhiteSpace(model.Email)
                   && environment == Security.Environment.Production)
                {
                    var message = await Service.ValidateEmailAsync(model.Email);

                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        if (feedback.Elements.ContainsKey("Email"))
                            feedback.Elements["Email"] = message;
                        else
                            feedback.Elements.Add("Email", message);

                        feedback.IsValid = false;
                    }
                }


                if (!feedback.IsValid)
                    return BadRequest(feedback);



                // Save it
                core ??= new User();

                var isNew = core.IsNew;

                core.FirstName = model.FirstName;
                core.LastName = model.LastName;
                core.JobTitle = model.JobTitle;
                core.Email = model.Email;
                core.LocationId = model.LocationId;
                core.DepartmentId = model.DepartmentId;
                core.ClientId = client.Id;

                try
                {
                    await _unitOfWork.AddAsync(core);
                }
                catch (Exception ex)
                {
                    feedback.DangerMessage(ex.Message);
                    feedback.IsValid = false;
                }


                if (!feedback.IsValid)
                {
                    return BadRequest(feedback);
                }

                await _unitOfWork.UserRoles.GetForUserAsync(core);


                var roleChanged = false;
                if (model.Roles.Count != core.UserRoles.Count)
                {
                    roleChanged = true;
                }
                else
                {
                    foreach (var role in model.Roles)
                    {
                        roleChanged = core.UserRoles.All(x => x.RoleId != role.Id);
                        if (roleChanged)
                            break;
                    }
                }


                if (roleChanged)
                {
                    await _unitOfWork.UserRoles.RemoveRangeAsync(core.UserRoles);

                    foreach (var role in model.Roles)
                    {
                        var userRole = new UserRole()
                        {
                            UserId = core.Id,
                            RoleId = role.Id,
                        };

                        try
                        {
                            await _unitOfWork.AddAsync(userRole);
                        }
                        catch
                        {
                            // ignored
                        }
                    }


                    if (!isNew && model.Roles.Count > 0)
                    {
                        // Send Change role Email, if not new user
                        var roles = "";
                        foreach (var role in model.Roles)
                            roles += $"<li style=\"list-style: square;\">{role.Name}</li>";


                        if (!string.IsNullOrEmpty(roles))
                            roles = $"<ul>{roles}</ul>";

                        BookMark[] bookmarks =
                        {
                            new ("FIRSTNAME", core.FirstName),
                            new ("ROLES", roles),
                        };

                        await Service.SendAsync(
                            "Account Updated - " + Data.Core.Settings.ApplicationName,
                            Template.NotifyRoleChange,
                            new MailBox(Data.Core.Settings.ApplicationName, Service.ApplicationEmailAddress),
                            new MailBox(core.Fullname, core.Email),
                            bookmarks);
                    }
                }

                if (isNew)
                {
                    var userId = GetUserId();
                    var user = await _unitOfWork.Users.GetAsync(userId);
                    await Service.InviteTeamMemberAsync(user, core);
                }


                await _unitOfWork.CompleteAsync();
                return Ok(core.Id);

                
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Server error Requesting content");
                return BadRequest(feedback);
            }
        }



        [HttpDelete("/api/Models/User/Delete/Id/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.ManageTenantUsers)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageAgencyUsers)).Succeeded)
                    return BadRequest("Unauthorised");

                if (string.IsNullOrWhiteSpace(id))
                    return BadRequest("Id missing.");

                var clientCore = await GetClientAsync();

                if (clientCore == null)
                    return NegativeFeedback();

                var client = new Models.Business.Client(clientCore);

                // Guard Clause
                if ((await AuthorizeAsync(Policy.ManageTenantUsers)).Succeeded && client.IsAgency
                 && (await AuthorizeAsync(Policy.ManageAgencyUsers)).Succeeded && !client.IsAgency)
                    return BadRequest("Unauthorised");


                await _unitOfWork.Users.RemoveAsync(id);
                await _unitOfWork.CompleteAsync();

                return Ok(id);
            }
            catch (ChildDependencyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log(ex);
                return BadRequest("Database error while deleting user");
            }

        }



        [HttpGet("/api/User/GetCollaborators/Search/{text}")]
        public async Task<IActionResult> GetCollaborators(string text)
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return BadRequest("Error: Unauthorised");

                var client = await GetClientAsync();

                if (client == null)
                    return NegativeFeedback();


                await _unitOfWork.Users.GetForClientAsync(client);
                await _unitOfWork.UserRoles.GetForUserAsync(client.Users);

                var businessRoles = (await _unitOfWork.BusinessRoles.FindAsync(x => x.ClientId == client.Id || x.IsBuiltIn)).ToList();


                if (string.IsNullOrWhiteSpace(text))
                    return Content("");


                var models = Models.User.Create(client.Users);

                text = text.Trim().ToLower();

                var exactMatchRequired = false;
                if (text.IndexOf("\"", StringComparison.Ordinal) == 0
                    && text.IndexOf("\"", 1, StringComparison.Ordinal) > 1)
                {
                    exactMatchRequired = true;
                    text = text.Substring(1);
                    text = text.Substring(0, text.Length - 1);
                }


                models = exactMatchRequired ? 
                    models.Where(x => x.Fullname.ToLower() == text).ToList() : 
                    models.Where(x => x.Fullname.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1).ToList();


                foreach (var user in models)
                    user.BusinessRoles = Models.Business.Role.Create(businessRoles);


                var html = await _viewToString.PartialAsync("Shared/Business/Idea/ManageCollaborators/_Users.cshtml", models);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("<h3 class=\"text-danger\">Server error Requesting content<h3>");
            }
        }



        [HttpGet("/api/User/GetInviteTeamMemberModal")]
        public async Task<IActionResult> GetInviteTeamMemberModal()
        {
            var html = await PartialAsync("Shared/User/_InviteTeamMember.cshtml");

            return Content(html);
        }


        [HttpGet("/api/User/GetInviteTeamMemberModalBody")]
        public async Task<IActionResult> GetInviteTeamMemberModalBody()
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return BadRequest("Error: Unauthorised");

                var client = await GetClientAsync();

                if (client == null)
                    return await PageApiAsync("<span>Unauthorised</span>");


                if (client.TypeId == Enumerators.ClientType.ReferrerAgency41.ToString())
                    return Ok("<span>Cannot invite members</span>");


                var html = await _viewToString.PartialAsync(
                    "Shared/User/_InviteTeamMemberBody.cshtml", 
                    "@" + client.Website);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("<span>Server error Requesting content<span>");
            }
        }


        [HttpPost("/api/User/PostInviteTeamMember")]
        public async Task<IActionResult> PostInviteTeamMember([FromBody] ViewModels.User.InviteTeamMember viewModel)
        {
            var feedback = new Feedback();
            try
            {

                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                {
                    feedback.DangerMessage("Unauthorised");
                    return BadRequest(feedback);
                }

                if (viewModel == null)
                {
                    feedback.DangerMessage("No content");
                    return BadRequest(feedback);
                }

                feedback = GetFeedback(ModelState, feedback);


                if (!feedback.IsValid)
                    return BadRequest(feedback);



                var client = await GetClientAsync();

                if (client.IsPractice)
                {
                    feedback.DangerMessage("Cannot invite members in the practice account");
                    return BadRequest(feedback);
                }


                if (client.TypeId == Enumerators.ClientType.ReferrerAgency41.ToString())
                {
                    feedback.DangerMessage("Cannot invite members within a referrer agency");
                    return BadRequest(feedback);
                }

                var userId = GetUserId();
                var proposer = await _unitOfWork.Users.GetAsync(userId);

                if (proposer == null)
                {
                    feedback.DangerMessage("Could not find you on the database");
                    return BadRequest(feedback);
                }


                var email = viewModel.EmailPrefix + "@" + client.Website;

                // Does a user exist with this email,
                // but email is not confirmed.
                var invitee = await _unitOfWork.Users.GetByEmailAsync(email);

                if(invitee == null)
                {
                    invitee = new User
                    {
                        ClientId = client.Id,
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        Email = email
                    };

                    await _unitOfWork.AddAsync(invitee);

                    var userRole = new UserRole
                    {
                        User = invitee,
                        RoleId = ((int)Enumerators.Role.StandardUser).ToString()
                    };

                    await _unitOfWork.AddAsync(userRole);
                }
                else if(invitee.IsEmailConfirmed)
                {
                    feedback.DangerMessage("E-mail address already in use");
                    return BadRequest(feedback);
                }
                else
                {
                    invitee.ClientId = client.Id;
                    invitee.FirstName = viewModel.FirstName;
                    invitee.LastName = viewModel.LastName;
                }

                // Save
                await _unitOfWork.CompleteAsync();


                // Send
                var message = await Service.InviteTeamMemberAsync(proposer, invitee);

                if (!string.IsNullOrWhiteSpace(message))
                    return BadRequest("<span class=\"text-danger\">" + message + "</span>");


                feedback.InfoMessage($"Message Send to {invitee.Fullname}.");
                return Ok(feedback);
            }
            catch (Data.Persistence.UniqueConstraintException e)
            {
                feedback.DangerMessage(e.Message);
                return BadRequest(feedback);
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Unknown error saving to database");
                return BadRequest(feedback);
            }
        }
    }
}