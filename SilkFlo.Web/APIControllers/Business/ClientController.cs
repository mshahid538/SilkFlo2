using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core;
using SilkFlo.Web.ViewModels;
using static System.Int32;

namespace SilkFlo.Web.Controllers.Business
{
    public class ClientController : AbstractAPI
    {
        public ClientController(IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }



        [HttpGet("/api/Business/Client/GetWorkspaceSelector")]
        public async Task<IActionResult> GetWorkspaceSelector()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();


            try
            {
                // Get the select tenantId
                var userId = GetUserId();

                // Guard Clause
                if (userId == null)
                    return NegativeFeedback("No userId");

                var user = await _unitOfWork.Users.GetAsync(userId);

                // Guard Clause
                if (user == null)
                    return NegativeFeedback($"User with id {userId} missing");



                var tenantCore = await GetClientAsync();
                

                // Guard Clause
                if (tenantCore == null)
                    return NegativeFeedback("No Client");

                
                
                
                var showPracticeToggle = !string.IsNullOrWhiteSpace(tenantCore.PracticeId) || tenantCore.IsPractice;


                var clients = await GetForUserValidatedAsync(user);


                // Guard Clause
                if (clients == null)
                {
                    _unitOfWork.Log("The tenants list is null.");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }


                await _unitOfWork.BusinessClients.GetPracticeAccountForAsync(clients);


                var kvPairs = new Dictionary<string, string>();
                var key = "";
                var selectedClientId = "";

                if (clients.Count == 0)
                {
                    key = tenantCore.Id;

                    if (!tenantCore.IsPractice)
                        key += "," +
                               tenantCore.PracticeId;
                }
                else if (clients.Count == 1)
                {
                    var client = clients[0];
                    key = client
                        .Id;

                    if (!client.IsPractice)
                    {
                        var practiceAccountId = client.PracticeAccount == null ? client.Id : client.PracticeAccount.Id;

                        key += "," +
                               practiceAccountId;
                    }
                }
                else
                {
                    // Populate the kvPairs
                    foreach (var item in clients)
                    {
                        var model = new Models.Business.Client(item);

                        var name = item.Name;
                        if (model.IsAgency)
                            name += " (Agency)";


                        var pair = item.Id;
                        if (item.PracticeAccount != null)
                            pair += "," + item.PracticeAccount.Id;

                        kvPairs.Add(pair,
                            name);

                        if (tenantCore.Id == item.PracticeAccount?.Id && tenantCore.IsPractice)
                        {
                            selectedClientId = item.Id;
                            if (item.PracticeAccount != null)
                                selectedClientId += "," + item.PracticeAccount.Id;
                        }
                        else if (tenantCore.Id == item.Id)
                        {
                            selectedClientId = item.Id;
                            if (item.PracticeAccount != null)
                                selectedClientId += "," + item.PracticeAccount.Id;
                        }
                    }
                }


                var id = tenantCore.Id;
                var clientId = Request.Cookies[Services.Cookie.ClientId.ToString()];
                if (!string.IsNullOrWhiteSpace(clientId))
                    id = clientId;

                if (string.IsNullOrWhiteSpace(clientId) && clients.Count > 0)
                {
                    id = clients[0]
                        .Id;

                    Add(Services.Cookie.ClientId,
                        id,
                        DateTime.Now.AddDays(1000),
                        false);
                }


                // Get IsPractice state from cookie
                var sIsPractice = Request.Cookies[Services.Cookie.IsPractice.ToString()];

                bool isPractice;
                if (string.IsNullOrWhiteSpace(sIsPractice))
                {
                    var client = await _unitOfWork.BusinessClients.SingleOrDefaultAsync(x => x.Id == id);

                    if (client == null)
                        return Content("");

                    isPractice = client.IsPractice;


                    Add(Services.Cookie.IsPractice,
                        isPractice,
                        DateTime.Now.AddDays(1000),
                        false);
                }
                else
                {
                    bool.TryParse(
                        sIsPractice,
                        out isPractice);
                }



                if (user.Client == null)
                {
                    await _unitOfWork.BusinessClients.GetClientForAsync(user);
                }

                var isPracticeUser = false;
                if (user.Client != null)
                {
                    isPracticeUser = user.Client.IsPractice;
                }


                var workspaceSelected = new WorkspaceSelected(selectedClientId,
                                                               kvPairs)
                {
                    IsPracticeAccount = isPractice,
                    TenantId = key,
                    IsPracticeUser = isPracticeUser,
                    ShowPracticeToggle = showPracticeToggle
                };





                var html = await _viewToString.PartialAsync("Shared/_WorkspaceSelector.cshtml",
                    workspaceSelected);


                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("/api/Business/Tenants/GetModal")]
        [HttpGet("/api/Business/Tenants/GetModal/id/{id}")]
        public async Task<IActionResult> GetModal(string id)
        {
            return await CreateModalView(id);
        }

        private async Task<IActionResult> CreateModalView(string id)
        {
            var feedback = new Feedback();

            // Guard Clause
            if (!(await AuthorizeAsync(Policy.ManageAgencySettings)).Succeeded)
            {
                feedback.DangerMessage("Unauthorised");
                return BadRequest(feedback);
            }


            try
            {
                Data.Core.Domain.Business.Client core;
                if (string.IsNullOrWhiteSpace(id))
                    core = new Data.Core.Domain.Business.Client();
                else
                {
                    core = await _unitOfWork.BusinessClients.GetAsync(id);

                    // Guard Clause
                    if (core == null)
                    {
                        feedback.DangerMessage("Unauthorised: Invalid Id");
                        return BadRequest(feedback);
                    }
                }

                var model = new Models.Business.Client(core);

                await PrepareForModal(
                    model);

                await model.SetStatusAsync(_unitOfWork);

                if (model.IsNew)
                    model.IsDemo = true;



                var userId = GetUserId();
                var user = await _unitOfWork.Users.GetAsync(userId);
                await _unitOfWork.BusinessClients.GetClientForAsync(user);

#if DEBUG
                var isAdmin = (await AuthorizeAsync(Policy.Administrator)).Succeeded;
#else
                var isAdmin = false;
#endif

                var viewModel = new ViewModels.Business.Client.Modal
                {
                    Client = model,
                    ShowActivateCheckBox = isAdmin,
                    ShowSubscription = isAdmin,
                    ShowLicence = (await AuthorizeAsync(Policy.ManageAgencySettings)).Succeeded 
                                  && user.Client.Name == Data.Core.Settings.ApplicationName
                };

                if (!model.IsNew
                    && model.TypeId == Enumerators.ClientType.Client39.ToString())
                {
                    var subscription = await model.GetLastSubscriptionAsync(_unitOfWork);
                    if (subscription != null && string.IsNullOrWhiteSpace(subscription.PriceId))
                    {
                        viewModel.DateStart = subscription.DateStart;
                        viewModel.DateEnd = subscription.DateEnd;
                    }
                }

                var html = await _viewToString.PartialAsync(
                    "Shared/Business/Client/_Modal.cshtml",
                    viewModel);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Error getting client");
                return BadRequest(feedback);
            }
        }



        private async Task PrepareForModal(Models.Business.Client model)
        {
            try
            {
                // Get Countries
                var countries = await _unitOfWork.SharedCountries.GetAllAsync();
                model.Countries = Models.Shared.Country.Create(countries, true);


                // Get Industries
                var industries = await _unitOfWork.SharedIndustries.GetAllAsync();
                model.Industries = Models.Shared.Industry.Create(industries, true);


                if (!model.IsNew)
                {
                    await _unitOfWork.Users.GetAccountOwnerForAsync(model.GetCore());
                    if (model.AccountOwner != null)
                    {
                        await _unitOfWork.UserRoles.GetForUserAsync(model.AccountOwner.GetCore());
                    }

                    await model.SetStatusAsync(_unitOfWork);
                }


                // Only SilkFlo Administrator users can change the client type
                if ((await AuthorizeAsync(Policy.Administrator)).Succeeded)
                {
                    if(model.IsNew)
                        model.IsActive = true;

                    model.Types = Models.Shared.ClientType.Create(await _unitOfWork.SharedClientTypes.GetAllAsync());

                    var discountTiers = (await _unitOfWork.ShopDiscounts.GetAllAsync()).ToArray();



                    model.ReferrerDiscountTiers.Add(new Models.Shop.Discount());
                    var referrerDiscountTiers = discountTiers.Where(x => x.Name is "Silver" or "Gold");
                    model.ReferrerDiscountTiers.AddRange(Models.Shop.Discount.Create(referrerDiscountTiers));



                    model.ResellerDiscountTiers.Add(new Models.Shop.Discount());
                    var resellerDiscountTiers = discountTiers.ToList();
                    model.ResellerDiscountTiers.AddRange(Models.Shop.Discount.Create(resellerDiscountTiers));



                    if(model.TypeId == Enumerators.ClientType.ReferrerAgency41.ToString())
                        model.ReferrerDiscountId = model.AgencyDiscountId;

                    if (model.TypeId == Enumerators.ClientType.ResellerAgency45.ToString())
                        model.ResellerDiscountId = model.AgencyDiscountId;


                    if(!string.IsNullOrWhiteSpace(model.Website)
                    && !string.IsNullOrWhiteSpace(model.AccountOwnerEmail))
                        model.AccountOwnerEmail = model.AccountOwnerEmail.Replace("@" + model.Website, "");
                }
            }
            catch (Exception ex)
            {
                Log(ex);
                throw;
            }
        }




        [HttpGet("/Settings/Tenant")]
        public IActionResult RedirectToTenantAccount()
        {
            return Redirect("/Settings/Tenant/Account");
        }

        [HttpGet("/Settings/Agency")]
        public IActionResult RedirectToAgencyAccount()
        {
            return Redirect("/Settings/Agency/Account");
        }


        [HttpGet("/Settings")]
        [HttpGet("/Settings/Tenant/Account")]
        [HttpGet("/Settings/Tenant/Account/{tab}")]
        [HttpGet("/Settings/Agency/Account")]
        public async Task<IActionResult> GetAccount(string tab = "company-details")
        {
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings, false)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageAgencySettings, false)).Succeeded)
                return Redirect("/account/signin");


            return await CreateAccountView(false, tab);
        }

        [HttpGet("/api/Settings/Tenant/Account")]
        [HttpGet("/api/Settings/Agency/Account")]
        public async Task<IActionResult> AccountApi()
        {
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings,false)).Succeeded
             && !(await AuthorizeAsync(Policy.ManageAgencySettings, false)).Succeeded)
                return NegativeFeedback();


            return await CreateAccountView(true);
        }

        private async Task<IActionResult> CreateAccountView(
            bool returnStringContent,
            string tab = "company-details")
        {
            try
            {
                var client = await GetClientAsync(false);



                if (client == null)
                {
                    if (returnStringContent)
                        return NegativeFeedback();

                    return ViewDanger();
                }

                await _unitOfWork.Users.GetAccountOwnerForAsync(client);
                if (client.AccountOwner != null)
                    await _unitOfWork.UserRoles.GetForUserAsync(client.AccountOwner);

                var model = new Models.Business.Client(client)
                {
                    Tab = tab
                };


                model.Subscription = await model.GetLastSubscriptionAsync(_unitOfWork, false);

                if (model.Subscription != null)
                {
                    await _unitOfWork.ShopPrices.GetPriceForAsync(model.Subscription.GetCore());
                    await _unitOfWork.ShopProducts.GetProductForAsync(model.Subscription.GetCore().Price);
                    await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(model.Subscription.GetCore().Price);
                    await _unitOfWork.SharedPeriods.GetPeriodForAsync(model.Subscription.GetCore().Price);

                    var sTrialPeriod = await GetApplicationSettingsAsync(Enumerators.Setting.TrialPeriod);
                    TryParse(sTrialPeriod, out var trialPeriod);
                }

                var countries = await _unitOfWork.SharedCountries.GetAllAsync();
                model.Countries.Add(new Models.Shared.Country { DisplayText = "Select..." });
                model.Countries.AddRange(Models.Shared.Country.Create(countries));

                var industries = await _unitOfWork.SharedIndustries.GetAllAsync();
                model.Industries = Models.Shared.Industry.Create(industries);

                model.AffiliateURL = $"{Request.Scheme}://{Request.Host}/pricing/referrerCode/" + model.Id;



                const string url = "/Views/Settings/Account2.cshtml";
                if (returnStringContent)
                {
                    model.AffiliateLink = $"&lt;a href=\"{model.AffiliateURL}\"&gt;Sign Up to SilkFlo&lt;/a&gt;";
                    var html = await _viewToString.PartialAsync(url, model);
                    return Content(html);
                }

                model.AffiliateLink = $"<a href=\"{model.AffiliateURL}\">Sign Up to SilkFlo</a>";
                return View(url, model);
            }
            catch (Exception ex)
            {
                Log(ex);

                const string error = "Error fetching data";
                if (returnStringContent)
                    return NegativeFeedback(error);

                return ViewDanger(error);
            }
        }


        /// <summary>
        /// This Save is used by Reseller agencies (including SilkFlo) to save their tenant details
        /// Only SilkFlo agency can save NEW tenants
        /// </summary>
        /// <param name="model"></param>
        /// <param name="validateOnly">Validate and send back results</param>
        /// <param name="sendInvite">Send an invitation message after save</param>
        /// <returns></returns>
        [HttpPost("/api/Models/Business/Tenant/SaveModal")]
        [HttpPost("/api/Models/Business/Tenant/SaveModal/sendInvite/{sendInvite}")]
        [HttpPost("/api/Models/Business/Tenant/SaveModal/validateOnly/{validateOnly}")]
        public async Task<IActionResult> TenantSaveModal(
            [FromBody] Models.Business.Client model, 
            bool validateOnly = false,
            bool sendInvite = false)
        {
            var feedback = new Feedback
            {
                NamePrefix = "Modal.Business.Client."
            };

            // Guard Clause
            if (model == null)
            {
                feedback.DangerMessage("No content supplied");
                return BadRequest(feedback);
            }

            // Guard Clause
            if (!(await AuthorizeAsync(Policy.ManageAgencySettings)).Succeeded)
            {
                feedback.DangerMessage("Unauthorised");
                return BadRequest(feedback);
            }

            try
            {
                // Check bogus id
                if (!string.IsNullOrWhiteSpace(model.Id))
                {
                    var core = _unitOfWork.BusinessClients.GetAsync(model.Id);
                    if (core == null)
                    {
                        // We have a bogus id
                        model.Id = null;

                        feedback.DangerMessage("Unauthorised");
                        return BadRequest(feedback);
                    }
                }


                var agencyCore = await GetClientAsync();

                // Guard Clause - No signed in client found
                if (agencyCore == null)
                {
                    feedback.DangerMessage("Unauthorised");
                    return BadRequest(feedback);
                }


                var agency = new Models.Business.Client(agencyCore);

                // Guard Clause - Check is agency
                if (!agency.IsAgency)
                {
                    feedback.DangerMessage("Unauthorised");
                    return BadRequest(feedback);

                }

                // Guard Clause - Account Cannot create tenants
                if (model.IsNew
                    && agency.Name != Data.Core.Settings.ApplicationName)
                {
                    feedback.DangerMessage("Account Cannot create tenants");
                    return BadRequest(feedback);

                }

                // Guard Clause - Check is agency
                if (!agency.IsNew
                    && string.IsNullOrWhiteSpace(agency.AccountOwnerId))
                {
                    feedback.DangerMessage("Account Owner Missing");
                    return BadRequest(feedback);
                }



                feedback = await model.ValidateAdvancedAsync(
                    _unitOfWork,
                    feedback);


                if (model.IsNew 
                 && feedback.IsValid)
                {
                    // Create a new account owner id.
                    // This will enable the new client to pass validation
                    model.AccountOwnerId = Guid.NewGuid().ToString();
                }


                if (agency.Name != Data.Core.Settings.ApplicationName)
                    model.TypeId = Enumerators.ClientType.Client39.ToString();

                model.CurrencyId = "gbp";
                model.LanguageId = "en-gb";

                if (!model.IsNew 
                 && string.IsNullOrWhiteSpace(model.TypeId))
                {
                    model.TypeId = "Fake Id used to pass validation";
                }


                ModelState.Clear();
                TryValidateModel(model);
                feedback = GetFeedback(ModelState, feedback);


                feedback = await model.CheckUniqueAsync(_unitOfWork, feedback);




                string newAccountOwnerId = null;
                if(model.IsNew)
                {
                    // Reset account owner id for new client.
                    newAccountOwnerId = model.AccountOwnerId;
                    model.AccountOwnerId = null;
                }


                if (!feedback.IsValid) 
                    return BadRequest(feedback);


                if (validateOnly)
                    return Ok();


                feedback = await model.Save(
                    _unitOfWork,
                    (await AuthorizeAsync(Policy.Administrator)).Succeeded,
                    sendInvite,
                    newAccountOwnerId,
                    agency,
                    feedback);

                if (feedback.IsValid)
                    return Ok(model.Id);



                return BadRequest(feedback);
            }
            catch (Exception ex)
            {
                Log(ex);

                feedback.DangerMessage("Error Saving Client");
                feedback.IsValid = false;
                return BadRequest(feedback);
            }
        }


        /// <summary>
        /// Save signed in tenant of agency
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("api/Models/Business/Client/Save")]
        public async Task<IActionResult> Save([FromBody] Models.Business.Client model)
        {
            var feedback = new Feedback
            {
                NamePrefix = "Business.Client."
            };

            try
            {
                // Guard Clause
                if (!(await AuthorizeAsync(Policy.ManageTenantSettings, false)).Succeeded
                    && !(await AuthorizeAsync(Policy.ManageAgencySettings, false)).Succeeded)
                {
                    feedback.DangerMessage("Unauthorised");
                    return BadRequest(feedback);
                }


                // Guard Clause
                if (model?.Id == null)
                {
                    feedback.DangerMessage("No Content");
                    return BadRequest(feedback);
                }


                var core = await _unitOfWork.BusinessClients.GetAsync(model.Id);

                // Guard Clause
                if (core == null)
                    return await CreateAccountView(true);



                // We cannot save practice account
                if (core.IsPractice)
                    return Ok();



                var modelOld = new Models.Business.Client(core);

                // Guard Clause
                if ((await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded && modelOld.IsAgency
                    && (await AuthorizeAsync(Policy.ManageAgencySettings)).Succeeded && !modelOld.IsAgency)
                {
                    feedback.DangerMessage("Unauthorised");
                    return BadRequest(feedback);
                }

                if (core.TypeId != Enumerators.ClientType.Client39.ToString())
                    model.AllowGuestSignIn = false;

                if (core.AllowGuestSignIn != model.AllowGuestSignIn
                    && core.AllowGuestSignIn)
                {
                    // Check if there are any guests
                    await _unitOfWork.Users.GetForClientAsync(core);
                    var guestsPresent = false;
                    foreach (var user in core.Users)
                    {
                        if (!user.Email.Contains("@" + core.Website, StringComparison.OrdinalIgnoreCase))
                        {
                            guestsPresent = true;
                            break;
                        }
                    }

                    if (guestsPresent)
                    {
                        feedback.Add(
                            "AllowGuestSignIn", 
                            $"There are currently guest users on the system.<br>" +
                            "Please delete or change their email addresses<br>"+
                            "to be suffixed with @{core.Website}.<br><br>" +
                            "Go to the <a class=\"text-dark\" href=\"/Settings/tenant/Guests\">People</a> screen and choose the Guests filter.");
                        feedback.IsValid = false;
                    }
                }

                if (!ModelState.IsValid)
                {
                    foreach (var modelStateKey in ViewData.ModelState.Keys)
                    {
                        var value = ViewData.ModelState[modelStateKey];
                        if (value == null)
                            continue;

                        foreach (var error in value.Errors)
                        {
                            if (modelStateKey is "Name" 
                                or "Address1"
                                or "Address2" 
                                or "City" 
                                or "State" 
                                or "PostCode" 
                                or "CountryId" 
                                or "AccountOwnerId" 
                                or "IndustryId" 
                                or "AverageWorkingDay" 
                                or "AverageWorkingHour")
                            {
                                feedback.Elements.Add(modelStateKey, error.ErrorMessage);
                                feedback.IsValid = false;
                            }
                        }
                    }
                }

                feedback = await model.ValidateAdvancedAsync(_unitOfWork, feedback);


                // Guard Clause
                if (!feedback.IsValid)
                    return BadRequest(feedback);

                var changeStripeName = core.Name != model.Name;

                var changeStripeEmail = false;
                if (core.AccountOwnerId != model.AccountOwnerId)
                {
                    changeStripeEmail = true;

                    await _unitOfWork.Users.GetAccountOwnerForAsync(model.GetCore());

                    if (model.AccountOwner == null)
                    {
                        feedback.Add("AccountOwnerId", "Cannot find account owner.");
                        return BadRequest(feedback);
                    }

                    await _unitOfWork.UserRoles.GetForUserAsync(model.GetCore().AccountOwner);

                    var userRole =
                        model.GetCore().AccountOwner.UserRoles.SingleOrDefault(
                            x => x.RoleId == ((int)Enumerators.Role.AccountOwner).ToString());

                    if (userRole == null)
                    {
                        userRole = new Data.Core.Domain.UserRole
                        {
                            UserId = model.GetCore().AccountOwnerId,
                            RoleId = Enumerators.Role.AccountOwner.ToString()
                        };

                        await _unitOfWork.AddAsync(userRole);
                    }
                }



                core.Name = model.Name;
                core.Address1 = model.Address1;
                core.Address2 = model.Address2;
                core.City = model.City;
                core.State = model.State;
                core.PostCode = model.PostCode;
                core.CountryId = model.CountryId;
                core.IndustryId = model.IndustryId;
                core.AverageWorkingDay = model.AverageWorkingDay;
                core.AverageWorkingHour = model.AverageWorkingHour;
                core.AccountOwnerId = model.AccountOwnerId;
                core.ReceiveMarketing = model.ReceiveMarketing;
                core.AllowGuestSignIn = model.AllowGuestSignIn;


                // Update the practice account
                var practice = await _unitOfWork.BusinessClients.SingleOrDefaultAsync(x => x.Id == core.PracticeId);
                if (practice != null)
                {
                    practice.Name = model.Name + " - Practice";
                    practice.Address1 = model.Address1;
                    practice.Address2 = model.Address2;
                    practice.City = model.City;
                    practice.State = model.State;
                    practice.PostCode = model.PostCode;
                    practice.CountryId = model.CountryId;
                    practice.IndustryId = model.IndustryId;
                    practice.AverageWorkingDay = model.AverageWorkingDay;
                    practice.AverageWorkingHour = model.AverageWorkingHour;
                    practice.AccountOwnerId = model.AccountOwnerId;
                    practice.ReceiveMarketing = model.ReceiveMarketing;
                }


                await _unitOfWork.CompleteAsync();


                if (!changeStripeName && !changeStripeEmail)
                    return Ok();

                //await Payment.Manager.SaveAsync(core);
                await SilkFlo.Web.Services2.Models.PaymentManager.SaveAsync(core);

                return Ok();

            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Unknown error saving to database");
                return BadRequest(feedback);
            }
        }


        [HttpGet("/api/Business/Tenant/SendInvite/UserId/{userId}")]
        public async Task<IActionResult> SendInvite(
            string userId)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Ok("<span class=\"text-danger\">Unauthorised.</span>");

            if (string.IsNullOrWhiteSpace(userId))
                return Ok("<span class=\"text-danger\">User Id is missing.</span>");

            var user = await _unitOfWork.Users.GetAsync(userId);

            if (string.IsNullOrWhiteSpace(user.Email))
                return Ok($"<span class=\"text-danger\">EMail address is missing for {user.Fullname}.</span>");

            await _unitOfWork.BusinessClients.GetClientForAsync(user);

            if (user.Client == null)
                return Ok($"<span class=\"text-danger\">No client found for {user.Fullname}.</span>");


            var model = new Models.User(user);
            var message = await model.Client.SendInvitation(
                model);


            if(string.IsNullOrWhiteSpace(message))
                return Ok($"<span class=\"text-success\">Invite email sent to {user.Fullname} ({user.Email}).</span>");


            _unitOfWork.Log(message);


            return Ok("<span class=\"text-danger\">Message was not sent.</span>");
        }


        [HttpDelete("/api/Models/Business/Client/Delete/id/{id}")]
        public IActionResult Delete(string id)
        {
            var feedback = new Feedback();

            try
            {
                feedback.Message = "<span class=\"text-info\">ToDo: Remove department, team and process from LIVE and PRACTICE ideas.</span>";
                return BadRequest(feedback);

                //if (!(await AuthorizeAsync(Policy.Administrator)).Succeeded)
                //{
                //    feedback.Message = "<span class=\"text-danger\">Error: Unauthorised</span>";
                //    return BadRequest(feedback);
                //}


                //var client = await _unitOfWork.BusinessClients.GetAsync(id);

                //var practice = await _unitOfWork.BusinessClients.GetAsync(client.PracticeId);

                //var userId = client.AccountOwnerId;
                //client.AccountOwnerId = "";
                //practice.AccountOwnerId = "";

                //await _unitOfWork.BusinessClients.AddAsync(client);
                //await _unitOfWork.BusinessClients.AddAsync(practice);
                //await _unitOfWork.BusinessClients.RemoveAsync(id);

                //await _unitOfWork.CompleteAsync();

                //return Ok();
            }
            catch (ChildDependencyException ex)
            {
                feedback.DangerMessage(ex.Message);
                return BadRequest(feedback);
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Unknown error deleting to database");
                return BadRequest(feedback);
            }
        }


        [HttpGet("/api/Settings/Tenant/Account/Subscriptions")]
        public async Task<IActionResult> GetSubscriptions()
        {
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings, false)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageAgencySettings, false)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync(false);

                await _unitOfWork.ShopSubscriptions.GetForTenantAsync(client);
                await _unitOfWork.ShopPrices.GetPriceForAsync(client.TenantSubscriptions);
                
                foreach (var subscription in client.TenantSubscriptions)
                    await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(subscription.Price);

                foreach (var subscription in client.TenantSubscriptions)
                {
                    await _unitOfWork.ShopProducts.GetProductForAsync(subscription.Price);
                    await _unitOfWork.SharedPeriods.GetPeriodForAsync(subscription.Price);
                }

                var model = new Models.Business.Client(client);

                var html = await _viewToString.PartialAsync(
                    "Shared/Shop/Subscription/_TenantSummary.cshtml",
                    model);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return NegativeFeedback("Error Logged");
            }
        }
    }
}