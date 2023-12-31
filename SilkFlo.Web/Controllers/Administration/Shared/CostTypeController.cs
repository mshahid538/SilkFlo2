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

namespace SilkFlo.Web.Controllers.Administration.Shared
{
    public partial class CostTypeController : Abstract
    {
        public CostTypeController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Shared/CostType")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Shared.CostType> cores;

                cores = (await _unitOfWork.SharedCostTypes
                                         .GetAllAsync())
                                         .ToArray();

                var models = new List<Models.Shared.CostType>();

                if(cores != null)
                {
                    foreach(var core in cores)
                    {
                        var model = new Models.Shared.CostType(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Shared.CostType>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Shared/CostType/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Shared.CostType>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Shared/CostType/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Shared/CostType/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/shared/costType");

            var costType = await _unitOfWork.SharedCostTypes.GetAsync(id);

            if (costType == null)
                return Redirect("/Administration/Shared/CostType");
            else
            {
                if (costType.CreatedById != null)
                    costType.CreatedBy = await _unitOfWork.Users.GetAsync(costType.CreatedById);

                if (costType.UpdatedById != null)
                    costType.UpdatedBy = await _unitOfWork.Users.GetAsync(costType.UpdatedById);

                var model = new Models.Shared.CostType(costType);

                return View("/Views/Administration/Shared/CostType/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Shared/CostType/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Shared.CostType model)
        {
            if (model == null)
                return Redirect("/Administration/Shared/CostType");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Shared/CostType/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Shared/CostType/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.SharedCostTypes
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Shared/CostType");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/CostType/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/CostType/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the CostType.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Shared/CostType/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Shared/CostType/Edit/{id}")]
        [HttpGet("/Administration/Shared/CostType/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Shared.CostType costType;

            if (string.IsNullOrWhiteSpace(id))
            {
                costType = new Data.Core.Domain.Shared.CostType
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                costType = await _unitOfWork.SharedCostTypes.GetAsync(id);

                if (costType == null)
                    return Redirect(
                        "/Administration/Shared/CostType");
            }

            var model = new Models.Shared.CostType(costType);
            await model.GetCreatedAndUpdated(_unitOfWork);

            return View("/Views/Administration/Shared/CostType/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Shared/CostType/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Shared.CostType model)
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

                    return View("/Views/Administration/Shared/CostType/Edit.cshtml", model);
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
                    return View("/Views/Administration/Shared/CostType/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.SharedCostTypes.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Shared/CostType");

                return View("/Views/Administration/Shared/CostType/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Shared/CostType/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Shared/CostType/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Shared/CostType");
        }
    }
}