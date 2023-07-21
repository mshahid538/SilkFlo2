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
    public partial class DiscountController : Abstract
    {
        public DiscountController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Shop/Discount")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Shop.Discount> cores;

                cores = (await _unitOfWork.ShopDiscounts
                                         .GetAllAsync())
                                         .ToArray();

                var models = new List<Models.Shop.Discount>();

                if(cores != null)
                {
                    foreach(var core in cores)
                    {
                        var model = new Models.Shop.Discount(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Shop.Discount>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Shop/Discount/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Shop.Discount>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Shop/Discount/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Shop/Discount/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/shop/discount");

            var discount = await _unitOfWork.ShopDiscounts.GetAsync(id);

            if (discount == null)
                return Redirect("/Administration/Shop/Discount");
            else
            {
                if (discount.CreatedById != null)
                    discount.CreatedBy = await _unitOfWork.Users.GetAsync(discount.CreatedById);

                if (discount.UpdatedById != null)
                    discount.UpdatedBy = await _unitOfWork.Users.GetAsync(discount.UpdatedById);

                var model = new Models.Shop.Discount(discount);

                return View("/Views/Administration/Shop/Discount/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Shop/Discount/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Shop.Discount model)
        {
            if (model == null)
                return Redirect("/Administration/Shop/Discount");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Shop/Discount/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Shop/Discount/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.ShopDiscounts
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Shop/Discount");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shop/Discount/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shop/Discount/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the Discount.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Shop/Discount/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Shop/Discount/Edit/{id}")]
        [HttpGet("/Administration/Shop/Discount/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Shop.Discount discount;

            if (string.IsNullOrWhiteSpace(id))
            {
                discount = new Data.Core.Domain.Shop.Discount
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                discount = await _unitOfWork.ShopDiscounts.GetAsync(id);

                if (discount == null)
                    return Redirect(
                        "/Administration/Shop/Discount");
            }

            var model = new Models.Shop.Discount(discount);
            await model.GetCreatedAndUpdated(_unitOfWork);

            return View("/Views/Administration/Shop/Discount/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Shop/Discount/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Shop.Discount model)
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

                    return View("/Views/Administration/Shop/Discount/Edit.cshtml", model);
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
                    return View("/Views/Administration/Shop/Discount/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.ShopDiscounts.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Shop/Discount");

                return View("/Views/Administration/Shop/Discount/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Shop/Discount/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Shop/Discount/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Shop/Discount");
        }
    }
}