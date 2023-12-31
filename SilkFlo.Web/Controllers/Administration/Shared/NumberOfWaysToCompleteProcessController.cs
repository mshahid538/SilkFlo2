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
    public partial class NumberOfWaysToCompleteProcessController : Abstract
    {
        public NumberOfWaysToCompleteProcessController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Shared/NumberOfWaysToCompleteProcess")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Shared.NumberOfWaysToCompleteProcess> cores;

                cores = (await _unitOfWork.SharedNumberOfWaysToCompleteProcesses
                                         .GetAllAsync())
                                         .ToArray();

                var models = new List<Models.Shared.NumberOfWaysToCompleteProcess>();

                if(cores != null)
                {
                    cores = cores.OrderByDescending(m => m.Weighting)
                                 .ThenBy(m => m.Name);

                    foreach(var core in cores)
                    {
                        var model = new Models.Shared.NumberOfWaysToCompleteProcess(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Shared.NumberOfWaysToCompleteProcess>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Shared.NumberOfWaysToCompleteProcess>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Shared/NumberOfWaysToCompleteProcess/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/shared/numberOfWaysToCompleteProcess");

            var numberOfWaysToCompleteProcess = await _unitOfWork.SharedNumberOfWaysToCompleteProcesses.GetAsync(id);

            if (numberOfWaysToCompleteProcess == null)
                return Redirect("/Administration/Shared/NumberOfWaysToCompleteProcess");
            else
            {
                if (numberOfWaysToCompleteProcess.CreatedById != null)
                    numberOfWaysToCompleteProcess.CreatedBy = await _unitOfWork.Users.GetAsync(numberOfWaysToCompleteProcess.CreatedById);

                if (numberOfWaysToCompleteProcess.UpdatedById != null)
                    numberOfWaysToCompleteProcess.UpdatedBy = await _unitOfWork.Users.GetAsync(numberOfWaysToCompleteProcess.UpdatedById);

                var model = new Models.Shared.NumberOfWaysToCompleteProcess(numberOfWaysToCompleteProcess);

                return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Shared/NumberOfWaysToCompleteProcess/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Shared.NumberOfWaysToCompleteProcess model)
        {
            if (model == null)
                return Redirect("/Administration/Shared/NumberOfWaysToCompleteProcess");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.SharedNumberOfWaysToCompleteProcesses
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Shared/NumberOfWaysToCompleteProcess");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the NumberOfWaysToCompleteProcess.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Shared/NumberOfWaysToCompleteProcess/Edit/{id}")]
        [HttpGet("/Administration/Shared/NumberOfWaysToCompleteProcess/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Shared.NumberOfWaysToCompleteProcess numberOfWaysToCompleteProcess;

            if (string.IsNullOrWhiteSpace(id))
            {
                numberOfWaysToCompleteProcess = new Data.Core.Domain.Shared.NumberOfWaysToCompleteProcess
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                numberOfWaysToCompleteProcess = await _unitOfWork.SharedNumberOfWaysToCompleteProcesses.GetAsync(id);

                if (numberOfWaysToCompleteProcess == null)
                    return Redirect(
                        "/Administration/Shared/NumberOfWaysToCompleteProcess");
            }

            var model = new Models.Shared.NumberOfWaysToCompleteProcess(numberOfWaysToCompleteProcess);
            await model.GetCreatedAndUpdated(_unitOfWork);

            return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Shared/NumberOfWaysToCompleteProcess/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Shared.NumberOfWaysToCompleteProcess model)
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

                    return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/Edit.cshtml", model);
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
                    return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.SharedNumberOfWaysToCompleteProcesses.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Shared/NumberOfWaysToCompleteProcess");

                return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Shared/NumberOfWaysToCompleteProcess/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Shared/NumberOfWaysToCompleteProcess/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Shared/NumberOfWaysToCompleteProcess");
        }
    }
}