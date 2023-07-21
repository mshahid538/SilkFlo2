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
    public partial class IdeaRunningCostController : Abstract
    {
        public IdeaRunningCostController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Business/IdeaRunningCost")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Business.IdeaRunningCost> cores;

                cores = (await _unitOfWork.BusinessIdeaRunningCosts
                                         .GetAllAsync())
                                         .ToArray();

                await _unitOfWork.BusinessClients.GetClientForAsync(cores);
                await _unitOfWork.BusinessIdeas.GetIdeaForAsync(cores);
                await _unitOfWork.BusinessRunningCosts.GetRunningCostForAsync(cores);

                var models = new List<Models.Business.IdeaRunningCost>();

                if(cores != null)
                {
                    foreach(var core in cores)
                    {
                        var model = new Models.Business.IdeaRunningCost(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Business.IdeaRunningCost>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Business/IdeaRunningCost/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Business.IdeaRunningCost>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Business/IdeaRunningCost/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Business/IdeaRunningCost/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/business/ideaRunningCost");

            var ideaRunningCost = await _unitOfWork.BusinessIdeaRunningCosts.GetAsync(id);

            if (ideaRunningCost == null)
                return Redirect("/Administration/Business/IdeaRunningCost");
            else
            {
                await _unitOfWork.BusinessClients.GetClientForAsync(ideaRunningCost);
                await _unitOfWork.BusinessIdeas.GetIdeaForAsync(ideaRunningCost);
                await _unitOfWork.BusinessRunningCosts.GetRunningCostForAsync(ideaRunningCost);

                if (ideaRunningCost.CreatedById != null)
                    ideaRunningCost.CreatedBy = await _unitOfWork.Users.GetAsync(ideaRunningCost.CreatedById);

                if (ideaRunningCost.UpdatedById != null)
                    ideaRunningCost.UpdatedBy = await _unitOfWork.Users.GetAsync(ideaRunningCost.UpdatedById);

                var model = new Models.Business.IdeaRunningCost(ideaRunningCost);

                return View("/Views/Administration/Business/IdeaRunningCost/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Business/IdeaRunningCost/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Business.IdeaRunningCost model)
        {
            if (model == null)
                return Redirect("/Administration/Business/IdeaRunningCost");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Business/IdeaRunningCost/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Business/IdeaRunningCost/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.BusinessIdeaRunningCosts
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Business/IdeaRunningCost");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Business/IdeaRunningCost/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Business/IdeaRunningCost/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the IdeaRunningCost.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Business/IdeaRunningCost/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Business/IdeaRunningCost/Edit/{id}")]
        [HttpGet("/Administration/Business/IdeaRunningCost/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Business.IdeaRunningCost ideaRunningCost;

            if (string.IsNullOrWhiteSpace(id))
            {
                ideaRunningCost = new Data.Core.Domain.Business.IdeaRunningCost
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                ideaRunningCost = await _unitOfWork.BusinessIdeaRunningCosts.GetAsync(id);

                if (ideaRunningCost == null)
                    return Redirect(
                        "/Administration/Business/IdeaRunningCost");
                await _unitOfWork.BusinessClients.GetClientForAsync(ideaRunningCost);
                await _unitOfWork.BusinessIdeas.GetIdeaForAsync(ideaRunningCost);
                await _unitOfWork.BusinessRunningCosts.GetRunningCostForAsync(ideaRunningCost);

            }

            var model = new Models.Business.IdeaRunningCost(ideaRunningCost);
            await model.GetCreatedAndUpdated(_unitOfWork);

            var coreClients = await _unitOfWork.BusinessClients.GetAllAsync();
            model.Clients.AddRange(Models.Business.Client.Create(coreClients));

            return View("/Views/Administration/Business/IdeaRunningCost/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Business/IdeaRunningCost/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Business.IdeaRunningCost model)
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

                    return View("/Views/Administration/Business/IdeaRunningCost/Edit.cshtml", model);
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
                    // Get parent: Business.Client -> Client
                    await _unitOfWork.BusinessClients.GetClientForAsync(model.GetCore());
                    model.Client =
                        model.GetCore().Client == null ?
                            null :
                            new Models.Business.Client(model.GetCore().Client);

                    // Get list of potential parents: Business.Client -> Client
                    var coreClients = await _unitOfWork.BusinessClients.GetAllAsync();
                    foreach (var core in coreClients)
                        model.Clients.Add(new Models.Business.Client(core));


                    return View("/Views/Administration/Business/IdeaRunningCost/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.BusinessIdeaRunningCosts.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Business/IdeaRunningCost");

                return View("/Views/Administration/Business/IdeaRunningCost/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Business/IdeaRunningCost/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Business/IdeaRunningCost/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Business/IdeaRunningCost");
        }
    }
}