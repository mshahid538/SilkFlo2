/*********************************************************
       Code Generated By  .  .  .  .  Delaney's ScriptBot
       WWW .  .  .  .  .  .  .  .  .  www.scriptbot.io
       Template Name.  .  .  .  .  .  Project Green 3.0
       Template Version.  .  .  .  .  20210407 014
       Author .  .  .  .  .  .  .  .  Delaney

                      ,        ,--,_
                       \ _ ___/ /\|
                       ( )__, )
                      |/_  '--,
                        \ `  / '

 *********************************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using SilkFlo.Data.Core;

namespace SilkFlo.Web.Controllers.Administration.Business
{
    public partial class ClientController : Abstract
    {
        public ClientController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Business/Client")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Business.Client> cores;

                cores = (await _unitOfWork.BusinessClients
                                         .GetAllAsync())
                                         .ToArray();

                await _unitOfWork.BusinessClients.GetPracticeAccountForAsync(cores);
                await _unitOfWork.SharedClientTypes.GetTypeForAsync(cores);

                var models = new List<Models.Business.Client>();

                if(cores != null)
                {
                    cores = cores.OrderBy(m => m.Name);

                    foreach(var core in cores)
                    {
                        var model = new Models.Business.Client(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Business.Client>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Business/Client/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Business.Client>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Business/Client/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Business/Client/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/business/client");

            var client = await _unitOfWork.BusinessClients.GetAsync(id);

            if (client == null)
                return Redirect("/Administration/Business/Client");
            else
            {
                await _unitOfWork.Users.GetAccountOwnerForAsync(client);
                await _unitOfWork.ShopDiscounts.GetAgencyDiscountForAsync(client);
                await _unitOfWork.BusinessClients.GetAgencyForAsync(client);
                await _unitOfWork.SharedCountries.GetCountryForAsync(client);
                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);
                await _unitOfWork.SharedIndustries.GetIndustryForAsync(client);
                await _unitOfWork.SharedLanguages.GetLanguageForAsync(client);
                await _unitOfWork.BusinessClients.GetPracticeAccountForAsync(client);
                await _unitOfWork.SharedClientTypes.GetTypeForAsync(client);

                if (client.CreatedById != null)
                    client.CreatedBy = await _unitOfWork.Users.GetAsync(client.CreatedById);

                if (client.UpdatedById != null)
                    client.UpdatedBy = await _unitOfWork.Users.GetAsync(client.UpdatedById);

                var model = new Models.Business.Client(client);

                return View("/Views/Administration/Business/Client/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Business/Client/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Business.Client model)
        {
            if (model == null)
                return Redirect("/Administration/Business/Client");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Business/Client/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Business/Client/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.BusinessClients
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Business/Client");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Business/Client/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Business/Client/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the Client.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Business/Client/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Business/Client/Edit/{id}")]
        [HttpGet("/Administration/Business/Client/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Business.Client client;

            if (string.IsNullOrWhiteSpace(id))
            {
                client = new Data.Core.Domain.Business.Client
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                client = await _unitOfWork.BusinessClients.GetAsync(id);

                if (client == null)
                    return Redirect(
                        "/Administration/Business/Client");
                await _unitOfWork.Users.GetAccountOwnerForAsync(client);
                await _unitOfWork.ShopDiscounts.GetAgencyDiscountForAsync(client);
                await _unitOfWork.BusinessClients.GetAgencyForAsync(client);
                await _unitOfWork.SharedCountries.GetCountryForAsync(client);
                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);
                await _unitOfWork.SharedIndustries.GetIndustryForAsync(client);
                await _unitOfWork.SharedLanguages.GetLanguageForAsync(client);
                await _unitOfWork.BusinessClients.GetPracticeAccountForAsync(client);
                await _unitOfWork.SharedClientTypes.GetTypeForAsync(client);

            }

            var model = new Models.Business.Client(client);
            await model.GetCreatedAndUpdated(_unitOfWork);

            var coreAccountOwners = await _unitOfWork.Users.GetAllAsync();
            model.AccountOwners.Add(new Models.User{ DisplayText = "Select..." });
            model.AccountOwners.AddRange(Models.User.Create(coreAccountOwners));
            var coreAgencyDiscounts = await _unitOfWork.ShopDiscounts.GetAllAsync();
            model.AgencyDiscounts.Add(new Models.Shop.Discount{ DisplayText = "Select..." });
            model.AgencyDiscounts.AddRange(Models.Shop.Discount.Create(coreAgencyDiscounts));
            var coreAgencies = await _unitOfWork.BusinessClients.GetAllAsync();
            model.Agencies.Add(new Models.Business.Client{ DisplayText = "Select..." });
            model.Agencies.AddRange(Models.Business.Client.Create(coreAgencies));
            var coreCountries = await _unitOfWork.SharedCountries.GetAllAsync();
            model.Countries.Add(new Models.Shared.Country{ DisplayText = "Select..." });
            model.Countries.AddRange(Models.Shared.Country.Create(coreCountries));
            var coreCurrencies = await _unitOfWork.ShopCurrencies.GetAllAsync();
            model.Currencies.AddRange(Models.Shop.Currency.Create(coreCurrencies));
            var coreIndustries = await _unitOfWork.SharedIndustries.GetAllAsync();
            model.Industries.Add(new Models.Shared.Industry{ DisplayText = "Select..." });
            model.Industries.AddRange(Models.Shared.Industry.Create(coreIndustries));
            var coreLanguages = await _unitOfWork.SharedLanguages.GetAllAsync();
            model.Languages.AddRange(Models.Shared.Language.Create(coreLanguages));
            var corePracticeAccounts = await _unitOfWork.BusinessClients.GetAllAsync();
            model.PracticeAccounts.Add(new Models.Business.Client{ DisplayText = "Select..." });
            model.PracticeAccounts.AddRange(Models.Business.Client.Create(corePracticeAccounts));
            var coreTypes = await _unitOfWork.SharedClientTypes.GetAllAsync();
            model.Types.AddRange(Models.Shared.ClientType.Create(coreTypes));

            return View("/Views/Administration/Business/Client/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Business/Client/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Business.Client model)
        {
            try
            {
                // Get the userId
                var userId = GetUserId();


                // Is the userId present.
                // If not return to view.                
                if (userId == null)
                {
                    ModelState.AddModelError(
                            "Error",
                            "userId is missing.");

                    return View("/Views/Administration/Business/Client/Edit.cshtml", model);
                }


                // Is the model valid?
                // If not return to view.
                bool isValid = Validate(_unitOfWork, model);

                var feedback = new ViewModels.Feedback();
                feedback = await model.CheckUniqueAsync(_unitOfWork, feedback);
                if (!feedback.IsValid)
                {
                    isValid = false;
                    foreach (var feedbackElement in feedback.Elements)
                        model.Errors.Add(feedbackElement.Value);
                }

                if (!isValid)
                {
                    // Get parent: User -> AccountOwner
                    await _unitOfWork.Users.GetAccountOwnerForAsync(model.GetCore());
                    model.AccountOwner =
                        model.GetCore().AccountOwner == null ?
                            null :
                            new Models.User(model.GetCore().AccountOwner);


                    // Get parent: Shop.Discount -> AgencyDiscount
                    await _unitOfWork.ShopDiscounts.GetAgencyDiscountForAsync(model.GetCore());
                    model.AgencyDiscount =
                        model.GetCore().AgencyDiscount == null ?
                            null :
                            new Models.Shop.Discount(model.GetCore().AgencyDiscount);


                    // Get parent: Business.Client -> Agency
                    await _unitOfWork.BusinessClients.GetAgencyForAsync(model.GetCore());
                    model.Agency =
                        model.GetCore().Agency == null ?
                            null :
                            new Models.Business.Client(model.GetCore().Agency);


                    // Get parent: Shared.Country -> Country
                    await _unitOfWork.SharedCountries.GetCountryForAsync(model.GetCore());
                    model.Country =
                        model.GetCore().Country == null ?
                            null :
                            new Models.Shared.Country(model.GetCore().Country);


                    // Get parent: Shop.Currency -> Currency
                    await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(model.GetCore());
                    model.Currency =
                        model.GetCore().Currency == null ?
                            null :
                            new Models.Shop.Currency(model.GetCore().Currency);


                    // Get parent: Shared.Industry -> Industry
                    await _unitOfWork.SharedIndustries.GetIndustryForAsync(model.GetCore());
                    model.Industry =
                        model.GetCore().Industry == null ?
                            null :
                            new Models.Shared.Industry(model.GetCore().Industry);


                    // Get parent: Shared.Language -> Language
                    await _unitOfWork.SharedLanguages.GetLanguageForAsync(model.GetCore());
                    model.Language =
                        model.GetCore().Language == null ?
                            null :
                            new Models.Shared.Language(model.GetCore().Language);


                    // Get parent: Business.Client -> PracticeAccount
                    await _unitOfWork.BusinessClients.GetPracticeAccountForAsync(model.GetCore());
                    model.PracticeAccount =
                        model.GetCore().PracticeAccount == null ?
                            null :
                            new Models.Business.Client(model.GetCore().PracticeAccount);


                    // Get parent: Shared.ClientType -> Type
                    await _unitOfWork.SharedClientTypes.GetTypeForAsync(model.GetCore());
                    model.Type =
                        model.GetCore().Type == null ?
                            null :
                            new Models.Shared.ClientType(model.GetCore().Type);

                    // Get list of potential parents: User -> AccountOwner
                    var coreAccountOwners = await _unitOfWork.Users.GetAllAsync();
                    model.AccountOwners.Add(new Models.User{ DisplayText = "<Empty>" });
                    foreach (var core in coreAccountOwners)
                        model.AccountOwners.Add(new Models.User(core));

                    // Get list of potential parents: Shop.Discount -> AgencyDiscount
                    var coreAgencyDiscounts = await _unitOfWork.ShopDiscounts.GetAllAsync();
                    model.AgencyDiscounts.Add(new Models.Shop.Discount{ DisplayText = "<Empty>" });
                    foreach (var core in coreAgencyDiscounts)
                        model.AgencyDiscounts.Add(new Models.Shop.Discount(core));

                    // Get list of potential parents: Business.Client -> Agency
                    var coreAgencies = await _unitOfWork.BusinessClients.GetAllAsync();
                    model.Agencies.Add(new Models.Business.Client{ DisplayText = "<Empty>" });
                    foreach (var core in coreAgencies)
                        model.Agencies.Add(new Models.Business.Client(core));

                    // Get list of potential parents: Shared.Country -> Country
                    var coreCountries = await _unitOfWork.SharedCountries.GetAllAsync();
                    model.Countries.Add(new Models.Shared.Country{ DisplayText = "<Empty>" });
                    foreach (var core in coreCountries)
                        model.Countries.Add(new Models.Shared.Country(core));

                    // Get list of potential parents: Shop.Currency -> Currency
                    var coreCurrencies = await _unitOfWork.ShopCurrencies.GetAllAsync();
                    foreach (var core in coreCurrencies)
                        model.Currencies.Add(new Models.Shop.Currency(core));

                    // Get list of potential parents: Shared.Industry -> Industry
                    var coreIndustries = await _unitOfWork.SharedIndustries.GetAllAsync();
                    model.Industries.Add(new Models.Shared.Industry{ DisplayText = "<Empty>" });
                    foreach (var core in coreIndustries)
                        model.Industries.Add(new Models.Shared.Industry(core));

                    // Get list of potential parents: Shared.Language -> Language
                    var coreLanguages = await _unitOfWork.SharedLanguages.GetAllAsync();
                    foreach (var core in coreLanguages)
                        model.Languages.Add(new Models.Shared.Language(core));

                    // Get list of potential parents: Business.Client -> PracticeAccount
                    var corePracticeAccounts = await _unitOfWork.BusinessClients.GetAllAsync();
                    model.PracticeAccounts.Add(new Models.Business.Client{ DisplayText = "<Empty>" });
                    foreach (var core in corePracticeAccounts)
                        model.PracticeAccounts.Add(new Models.Business.Client(core));

                    // Get list of potential parents: Shared.ClientType -> Type
                    var coreTypes = await _unitOfWork.SharedClientTypes.GetAllAsync();
                    foreach (var core in coreTypes)
                        model.Types.Add(new Models.Shared.ClientType(core));


                    return View("/Views/Administration/Business/Client/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.BusinessClients.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Business/Client");

                return View("/Views/Administration/Business/Client/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Business/Client/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Business/Client/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Business/Client");
        }
    }
}