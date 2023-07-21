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

namespace SilkFlo.Web.Controllers.Administration.Shop
{
    public partial class CurrencyController : Abstract
    {
        public CurrencyController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Shop/Currency")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Shop.Currency> cores;

                cores = (await _unitOfWork.ShopCurrencies
                                         .GetAllAsync())
                                         .ToArray();

                var models = new List<Models.Shop.Currency>();

                if(cores != null)
                {
                    foreach(var core in cores)
                    {
                        var model = new Models.Shop.Currency(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Shop.Currency>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Shop/Currency/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Shop.Currency>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Shop/Currency/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Shop/Currency/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/shop/currency");

            var currency = await _unitOfWork.ShopCurrencies.GetAsync(id);

            if (currency == null)
                return Redirect("/Administration/Shop/Currency");
            else
            {
                if (currency.CreatedById != null)
                    currency.CreatedBy = await _unitOfWork.Users.GetAsync(currency.CreatedById);

                if (currency.UpdatedById != null)
                    currency.UpdatedBy = await _unitOfWork.Users.GetAsync(currency.UpdatedById);

                var model = new Models.Shop.Currency(currency);

                return View("/Views/Administration/Shop/Currency/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Shop/Currency/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Shop.Currency model)
        {
            if (model == null)
                return Redirect("/Administration/Shop/Currency");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Shop/Currency/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Shop/Currency/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.ShopCurrencies
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Shop/Currency");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shop/Currency/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shop/Currency/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the Currency.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Shop/Currency/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Shop/Currency/Edit/{id}")]
        [HttpGet("/Administration/Shop/Currency/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Shop.Currency currency;

            if (string.IsNullOrWhiteSpace(id))
            {
                currency = new Data.Core.Domain.Shop.Currency
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                currency = await _unitOfWork.ShopCurrencies.GetAsync(id);

                if (currency == null)
                    return Redirect(
                        "/Administration/Shop/Currency");
            }

            var model = new Models.Shop.Currency(currency);
            await model.GetCreatedAndUpdated(_unitOfWork);

            return View("/Views/Administration/Shop/Currency/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Shop/Currency/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Shop.Currency model)
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

                    return View("/Views/Administration/Shop/Currency/Edit.cshtml", model);
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
                    return View("/Views/Administration/Shop/Currency/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.ShopCurrencies.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Shop/Currency");

                return View("/Views/Administration/Shop/Currency/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Shop/Currency/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Shop/Currency/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Shop/Currency");
        }
    }
}