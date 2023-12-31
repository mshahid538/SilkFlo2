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
    public partial class IdeaStatusController : Abstract
    {
        public IdeaStatusController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Shared/IdeaStatus")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Shared.IdeaStatus> cores;

                cores = (await _unitOfWork.SharedIdeaStatuses
                                         .GetAllAsync())
                                         .ToArray();

                await _unitOfWork.SharedStages.GetStageForAsync(cores);

                var models = new List<Models.Shared.IdeaStatus>();

                if(cores != null)
                {
                    cores = cores.OrderBy(m => m.Sort)
                                 .ThenBy(m => m.Name);

                    foreach(var core in cores)
                    {
                        var model = new Models.Shared.IdeaStatus(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Shared.IdeaStatus>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Shared/IdeaStatus/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Shared.IdeaStatus>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Shared/IdeaStatus/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Shared/IdeaStatus/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/shared/ideaStatus");

            var ideaStatus = await _unitOfWork.SharedIdeaStatuses.GetAsync(id);

            if (ideaStatus == null)
                return Redirect("/Administration/Shared/IdeaStatus");
            else
            {
                await _unitOfWork.SharedStages.GetStageForAsync(ideaStatus);

                if (ideaStatus.CreatedById != null)
                    ideaStatus.CreatedBy = await _unitOfWork.Users.GetAsync(ideaStatus.CreatedById);

                if (ideaStatus.UpdatedById != null)
                    ideaStatus.UpdatedBy = await _unitOfWork.Users.GetAsync(ideaStatus.UpdatedById);

                var model = new Models.Shared.IdeaStatus(ideaStatus);

                return View("/Views/Administration/Shared/IdeaStatus/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Shared/IdeaStatus/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Shared.IdeaStatus model)
        {
            if (model == null)
                return Redirect("/Administration/Shared/IdeaStatus");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Shared/IdeaStatus/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Shared/IdeaStatus/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.SharedIdeaStatuses
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Shared/IdeaStatus");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/IdeaStatus/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/IdeaStatus/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the IdeaStatus.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Shared/IdeaStatus/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Shared/IdeaStatus/Edit/{id}")]
        [HttpGet("/Administration/Shared/IdeaStatus/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Shared.IdeaStatus ideaStatus;

            if (string.IsNullOrWhiteSpace(id))
            {
                ideaStatus = new Data.Core.Domain.Shared.IdeaStatus
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                ideaStatus = await _unitOfWork.SharedIdeaStatuses.GetAsync(id);

                if (ideaStatus == null)
                    return Redirect(
                        "/Administration/Shared/IdeaStatus");
                await _unitOfWork.SharedStages.GetStageForAsync(ideaStatus);

            }

            var model = new Models.Shared.IdeaStatus(ideaStatus);
            await model.GetCreatedAndUpdated(_unitOfWork);

            var coreStages = await _unitOfWork.SharedStages.GetAllAsync();
            model.Stages.AddRange(Models.Shared.Stage.Create(coreStages));

            return View("/Views/Administration/Shared/IdeaStatus/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Shared/IdeaStatus/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Shared.IdeaStatus model)
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

                    return View("/Views/Administration/Shared/IdeaStatus/Edit.cshtml", model);
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
                    // Get parent: Shared.Stage -> Stage
                    await _unitOfWork.SharedStages.GetStageForAsync(model.GetCore());
                    model.Stage =
                        model.GetCore().Stage == null ?
                            null :
                            new Models.Shared.Stage(model.GetCore().Stage);

                    // Get list of potential parents: Shared.Stage -> Stage
                    var coreStages = await _unitOfWork.SharedStages.GetAllAsync();
                    foreach (var core in coreStages)
                        model.Stages.Add(new Models.Shared.Stage(core));


                    return View("/Views/Administration/Shared/IdeaStatus/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.SharedIdeaStatuses.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Shared/IdeaStatus");

                return View("/Views/Administration/Shared/IdeaStatus/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Shared/IdeaStatus/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Shared/IdeaStatus/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Shared/IdeaStatus");
        }
    }
}