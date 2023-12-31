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
    public partial class PeriodController : Abstract
    {
        public PeriodController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Shared/Period")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Shared.Period> cores;

                cores = (await _unitOfWork.SharedPeriods
                                         .GetAllAsync())
                                         .ToArray();

                var models = new List<Models.Shared.Period>();

                if(cores != null)
                {
                    cores = cores.OrderBy(m => m.Sort)
                                 .ThenBy(m => m.Name);

                    foreach(var core in cores)
                    {
                        var model = new Models.Shared.Period(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Shared.Period>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Shared/Period/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Shared.Period>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Shared/Period/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Shared/Period/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/shared/period");

            var period = await _unitOfWork.SharedPeriods.GetAsync(id);

            if (period == null)
                return Redirect("/Administration/Shared/Period");
            else
            {
                if (period.CreatedById != null)
                    period.CreatedBy = await _unitOfWork.Users.GetAsync(period.CreatedById);

                if (period.UpdatedById != null)
                    period.UpdatedBy = await _unitOfWork.Users.GetAsync(period.UpdatedById);

                var model = new Models.Shared.Period(period);

                return View("/Views/Administration/Shared/Period/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Shared/Period/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Shared.Period model)
        {
            if (model == null)
                return Redirect("/Administration/Shared/Period");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Shared/Period/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Shared/Period/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.SharedPeriods
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Shared/Period");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/Period/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/Period/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the Period.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Shared/Period/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Shared/Period/Edit/{id}")]
        [HttpGet("/Administration/Shared/Period/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Shared.Period period;

            if (string.IsNullOrWhiteSpace(id))
            {
                period = new Data.Core.Domain.Shared.Period
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                period = await _unitOfWork.SharedPeriods.GetAsync(id);

                if (period == null)
                    return Redirect(
                        "/Administration/Shared/Period");
            }

            var model = new Models.Shared.Period(period);
            await model.GetCreatedAndUpdated(_unitOfWork);

            return View("/Views/Administration/Shared/Period/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Shared/Period/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Shared.Period model)
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

                    return View("/Views/Administration/Shared/Period/Edit.cshtml", model);
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
                    return View("/Views/Administration/Shared/Period/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.SharedPeriods.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Shared/Period");

                return View("/Views/Administration/Shared/Period/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Shared/Period/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Shared/Period/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Shared/Period");
        }
    }
}