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
    public partial class RoleIdeaAuthorisationController : Abstract
    {
        public RoleIdeaAuthorisationController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Business/RoleIdeaAuthorisation")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Business.RoleIdeaAuthorisation> cores;

                cores = (await _unitOfWork.BusinessRoleIdeaAuthorisations
                                         .GetAllAsync())
                                         .ToArray();

                await _unitOfWork.BusinessClients.GetClientForAsync(cores);
                await _unitOfWork.SharedIdeaAuthorisations.GetIdeaAuthorisationForAsync(cores);
                await _unitOfWork.BusinessRoles.GetRoleForAsync(cores);

                var models = new List<Models.Business.RoleIdeaAuthorisation>();

                if(cores != null)
                {
                    foreach(var core in cores)
                    {
                        var model = new Models.Business.RoleIdeaAuthorisation(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Business.RoleIdeaAuthorisation>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Business/RoleIdeaAuthorisation/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Business.RoleIdeaAuthorisation>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Business/RoleIdeaAuthorisation/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Business/RoleIdeaAuthorisation/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/business/roleIdeaAuthorisation");

            var roleIdeaAuthorisation = await _unitOfWork.BusinessRoleIdeaAuthorisations.GetAsync(id);

            if (roleIdeaAuthorisation == null)
                return Redirect("/Administration/Business/RoleIdeaAuthorisation");
            else
            {
                await _unitOfWork.BusinessClients.GetClientForAsync(roleIdeaAuthorisation);
                await _unitOfWork.SharedIdeaAuthorisations.GetIdeaAuthorisationForAsync(roleIdeaAuthorisation);
                await _unitOfWork.BusinessRoles.GetRoleForAsync(roleIdeaAuthorisation);

                if (roleIdeaAuthorisation.CreatedById != null)
                    roleIdeaAuthorisation.CreatedBy = await _unitOfWork.Users.GetAsync(roleIdeaAuthorisation.CreatedById);

                if (roleIdeaAuthorisation.UpdatedById != null)
                    roleIdeaAuthorisation.UpdatedBy = await _unitOfWork.Users.GetAsync(roleIdeaAuthorisation.UpdatedById);

                var model = new Models.Business.RoleIdeaAuthorisation(roleIdeaAuthorisation);

                return View("/Views/Administration/Business/RoleIdeaAuthorisation/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Business/RoleIdeaAuthorisation/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Business.RoleIdeaAuthorisation model)
        {
            if (model == null)
                return Redirect("/Administration/Business/RoleIdeaAuthorisation");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Business/RoleIdeaAuthorisation/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Business/RoleIdeaAuthorisation/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.BusinessRoleIdeaAuthorisations
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Business/RoleIdeaAuthorisation");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Business/RoleIdeaAuthorisation/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Business/RoleIdeaAuthorisation/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the RoleIdeaAuthorisation.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Business/RoleIdeaAuthorisation/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Business/RoleIdeaAuthorisation/Edit/{id}")]
        [HttpGet("/Administration/Business/RoleIdeaAuthorisation/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Business.RoleIdeaAuthorisation roleIdeaAuthorisation;

            if (string.IsNullOrWhiteSpace(id))
            {
                roleIdeaAuthorisation = new Data.Core.Domain.Business.RoleIdeaAuthorisation
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                roleIdeaAuthorisation = await _unitOfWork.BusinessRoleIdeaAuthorisations.GetAsync(id);

                if (roleIdeaAuthorisation == null)
                    return Redirect(
                        "/Administration/Business/RoleIdeaAuthorisation");
                await _unitOfWork.BusinessClients.GetClientForAsync(roleIdeaAuthorisation);
                await _unitOfWork.SharedIdeaAuthorisations.GetIdeaAuthorisationForAsync(roleIdeaAuthorisation);
                await _unitOfWork.BusinessRoles.GetRoleForAsync(roleIdeaAuthorisation);

            }

            var model = new Models.Business.RoleIdeaAuthorisation(roleIdeaAuthorisation);
            await model.GetCreatedAndUpdated(_unitOfWork);

            var coreClients = await _unitOfWork.BusinessClients.GetAllAsync();
            model.Clients.AddRange(Models.Business.Client.Create(coreClients));
            var coreIdeaAuthorisations = await _unitOfWork.SharedIdeaAuthorisations.GetAllAsync();
            model.IdeaAuthorisations.AddRange(Models.Shared.IdeaAuthorisation.Create(coreIdeaAuthorisations));
            var coreRoles = await _unitOfWork.BusinessRoles.GetAllAsync();
            model.Roles.AddRange(Models.Business.Role.Create(coreRoles));

            return View("/Views/Administration/Business/RoleIdeaAuthorisation/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Business/RoleIdeaAuthorisation/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Business.RoleIdeaAuthorisation model)
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

                    return View("/Views/Administration/Business/RoleIdeaAuthorisation/Edit.cshtml", model);
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


                    // Get parent: Shared.IdeaAuthorisation -> IdeaAuthorisation
                    await _unitOfWork.SharedIdeaAuthorisations.GetIdeaAuthorisationForAsync(model.GetCore());
                    model.IdeaAuthorisation =
                        model.GetCore().IdeaAuthorisation == null ?
                            null :
                            new Models.Shared.IdeaAuthorisation(model.GetCore().IdeaAuthorisation);


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

                    // Get list of potential parents: Shared.IdeaAuthorisation -> IdeaAuthorisation
                    var coreIdeaAuthorisations = await _unitOfWork.SharedIdeaAuthorisations.GetAllAsync();
                    foreach (var core in coreIdeaAuthorisations)
                        model.IdeaAuthorisations.Add(new Models.Shared.IdeaAuthorisation(core));

                    // Get list of potential parents: Business.Role -> Role
                    var coreRoles = await _unitOfWork.BusinessRoles.GetAllAsync();
                    foreach (var core in coreRoles)
                        model.Roles.Add(new Models.Business.Role(core));


                    return View("/Views/Administration/Business/RoleIdeaAuthorisation/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.BusinessRoleIdeaAuthorisations.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Business/RoleIdeaAuthorisation");

                return View("/Views/Administration/Business/RoleIdeaAuthorisation/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Business/RoleIdeaAuthorisation/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Business/RoleIdeaAuthorisation/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Business/RoleIdeaAuthorisation");
        }
    }
}