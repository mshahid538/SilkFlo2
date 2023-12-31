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
    public partial class ProcessStabilityController : Abstract
    {
        public ProcessStabilityController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Shared/ProcessStability")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Shared.ProcessStability> cores;

                cores = (await _unitOfWork.SharedProcessStabilities
                                         .GetAllAsync())
                                         .ToArray();

                var models = new List<Models.Shared.ProcessStability>();

                if(cores != null)
                {
                    cores = cores.OrderByDescending(m => m.Weighting)
                                 .ThenBy(m => m.Name);

                    foreach(var core in cores)
                    {
                        var model = new Models.Shared.ProcessStability(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Shared.ProcessStability>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Shared/ProcessStability/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Shared.ProcessStability>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Shared/ProcessStability/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Shared/ProcessStability/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/shared/processStability");

            var processStability = await _unitOfWork.SharedProcessStabilities.GetAsync(id);

            if (processStability == null)
                return Redirect("/Administration/Shared/ProcessStability");
            else
            {
                if (processStability.CreatedById != null)
                    processStability.CreatedBy = await _unitOfWork.Users.GetAsync(processStability.CreatedById);

                if (processStability.UpdatedById != null)
                    processStability.UpdatedBy = await _unitOfWork.Users.GetAsync(processStability.UpdatedById);

                var model = new Models.Shared.ProcessStability(processStability);

                return View("/Views/Administration/Shared/ProcessStability/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Shared/ProcessStability/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Shared.ProcessStability model)
        {
            if (model == null)
                return Redirect("/Administration/Shared/ProcessStability");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Shared/ProcessStability/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Shared/ProcessStability/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.SharedProcessStabilities
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Shared/ProcessStability");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/ProcessStability/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/ProcessStability/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the ProcessStability.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Shared/ProcessStability/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Shared/ProcessStability/Edit/{id}")]
        [HttpGet("/Administration/Shared/ProcessStability/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Shared.ProcessStability processStability;

            if (string.IsNullOrWhiteSpace(id))
            {
                processStability = new Data.Core.Domain.Shared.ProcessStability
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                processStability = await _unitOfWork.SharedProcessStabilities.GetAsync(id);

                if (processStability == null)
                    return Redirect(
                        "/Administration/Shared/ProcessStability");
            }

            var model = new Models.Shared.ProcessStability(processStability);
            await model.GetCreatedAndUpdated(_unitOfWork);

            return View("/Views/Administration/Shared/ProcessStability/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Shared/ProcessStability/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Shared.ProcessStability model)
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

                    return View("/Views/Administration/Shared/ProcessStability/Edit.cshtml", model);
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
                    return View("/Views/Administration/Shared/ProcessStability/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.SharedProcessStabilities.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Shared/ProcessStability");

                return View("/Views/Administration/Shared/ProcessStability/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Shared/ProcessStability/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Shared/ProcessStability/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Shared/ProcessStability");
        }
    }
}