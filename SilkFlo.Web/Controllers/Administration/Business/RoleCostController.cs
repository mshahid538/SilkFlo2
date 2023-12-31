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
    public partial class RoleCostController : Abstract
    {
        public RoleCostController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Business/RoleCost")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Business.RoleCost> cores;

                cores = (await _unitOfWork.BusinessRoleCosts
                                         .GetAllAsync())
                                         .ToArray();

                await _unitOfWork.BusinessClients.GetClientForAsync(cores);
                await _unitOfWork.BusinessRoles.GetRoleForAsync(cores);

                var models = new List<Models.Business.RoleCost>();

                if(cores != null)
                {
                    foreach(var core in cores)
                    {
                        var model = new Models.Business.RoleCost(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Business.RoleCost>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Business/RoleCost/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Business.RoleCost>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Business/RoleCost/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Business/RoleCost/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/business/roleCost");

            var roleCost = await _unitOfWork.BusinessRoleCosts.GetAsync(id);

            if (roleCost == null)
                return Redirect("/Administration/Business/RoleCost");
            else
            {
                await _unitOfWork.BusinessClients.GetClientForAsync(roleCost);
                await _unitOfWork.BusinessRoles.GetRoleForAsync(roleCost);

                if (roleCost.CreatedById != null)
                    roleCost.CreatedBy = await _unitOfWork.Users.GetAsync(roleCost.CreatedById);

                if (roleCost.UpdatedById != null)
                    roleCost.UpdatedBy = await _unitOfWork.Users.GetAsync(roleCost.UpdatedById);

                var model = new Models.Business.RoleCost(roleCost);

                return View("/Views/Administration/Business/RoleCost/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Business/RoleCost/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Business.RoleCost model)
        {
            if (model == null)
                return Redirect("/Administration/Business/RoleCost");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Business/RoleCost/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Business/RoleCost/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.BusinessRoleCosts
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Business/RoleCost");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Business/RoleCost/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Business/RoleCost/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the RoleCost.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Business/RoleCost/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Business/RoleCost/Edit/{id}")]
        [HttpGet("/Administration/Business/RoleCost/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Business.RoleCost roleCost;

            if (string.IsNullOrWhiteSpace(id))
            {
                roleCost = new Data.Core.Domain.Business.RoleCost
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                roleCost = await _unitOfWork.BusinessRoleCosts.GetAsync(id);

                if (roleCost == null)
                    return Redirect(
                        "/Administration/Business/RoleCost");
                await _unitOfWork.BusinessClients.GetClientForAsync(roleCost);
                await _unitOfWork.BusinessRoles.GetRoleForAsync(roleCost);

            }

            var model = new Models.Business.RoleCost(roleCost);
            await model.GetCreatedAndUpdated(_unitOfWork);

            var coreClients = await _unitOfWork.BusinessClients.GetAllAsync();
            model.Clients.AddRange(Models.Business.Client.Create(coreClients));
            var coreRoles = await _unitOfWork.BusinessRoles.GetAllAsync();
            model.Roles.AddRange(Models.Business.Role.Create(coreRoles));

            return View("/Views/Administration/Business/RoleCost/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Business/RoleCost/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Business.RoleCost model)
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

                    return View("/Views/Administration/Business/RoleCost/Edit.cshtml", model);
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


                    // Get parent: Business.Role -> Role
                    await _unitOfWork.BusinessRoles.GetRoleForAsync(model.GetCore());
                    model.Role =
                        model.GetCore().Role == null ?
                            null :
                            new Models.Business.Role(model.GetCore().Role);

                    // Get list of potential parents: Business.Client -> Client
                    var coreClients = await _unitOfWork.BusinessClients.GetAllAsync();
                    foreach (var core in coreClients)
                        model.Clients.Add(new Models.Business.Client(core));

                    // Get list of potential parents: Business.Role -> Role
                    var coreRoles = await _unitOfWork.BusinessRoles.GetAllAsync();
                    foreach (var core in coreRoles)
                        model.Roles.Add(new Models.Business.Role(core));


                    return View("/Views/Administration/Business/RoleCost/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.BusinessRoleCosts.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Business/RoleCost");

                return View("/Views/Administration/Business/RoleCost/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Business/RoleCost/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Business/RoleCost/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Business/RoleCost");
        }
    }
}