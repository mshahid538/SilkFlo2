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
    public partial class ClientTypeController : Abstract
    {
        public ClientTypeController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Shared/ClientType")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Shared.ClientType> cores;

                cores = (await _unitOfWork.SharedClientTypes
                                         .GetAllAsync())
                                         .ToArray();

                var models = new List<Models.Shared.ClientType>();

                if(cores != null)
                {
                    cores = cores.OrderBy(m => m.Name);

                    foreach(var core in cores)
                    {
                        var model = new Models.Shared.ClientType(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Shared.ClientType>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Shared/ClientType/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Shared.ClientType>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Shared/ClientType/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Shared/ClientType/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/shared/clientType");

            var clientType = await _unitOfWork.SharedClientTypes.GetAsync(id);

            if (clientType == null)
                return Redirect("/Administration/Shared/ClientType");
            else
            {
                if (clientType.CreatedById != null)
                    clientType.CreatedBy = await _unitOfWork.Users.GetAsync(clientType.CreatedById);

                if (clientType.UpdatedById != null)
                    clientType.UpdatedBy = await _unitOfWork.Users.GetAsync(clientType.UpdatedById);

                var model = new Models.Shared.ClientType(clientType);

                return View("/Views/Administration/Shared/ClientType/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Shared/ClientType/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Shared.ClientType model)
        {
            if (model == null)
                return Redirect("/Administration/Shared/ClientType");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Shared/ClientType/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Shared/ClientType/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.SharedClientTypes
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Shared/ClientType");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/ClientType/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/ClientType/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the ClientType.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Shared/ClientType/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Shared/ClientType/Edit/{id}")]
        [HttpGet("/Administration/Shared/ClientType/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Shared.ClientType clientType;

            if (string.IsNullOrWhiteSpace(id))
            {
                clientType = new Data.Core.Domain.Shared.ClientType
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                clientType = await _unitOfWork.SharedClientTypes.GetAsync(id);

                if (clientType == null)
                    return Redirect(
                        "/Administration/Shared/ClientType");
            }

            var model = new Models.Shared.ClientType(clientType);
            await model.GetCreatedAndUpdated(_unitOfWork);

            return View("/Views/Administration/Shared/ClientType/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Shared/ClientType/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Shared.ClientType model)
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

                    return View("/Views/Administration/Shared/ClientType/Edit.cshtml", model);
                }


                // Is the model valid?
                // If not return to view.
                bool isValid = Validate(_unitOfWork, model);
                if (!isValid)
                {
                    return View("/Views/Administration/Shared/ClientType/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.SharedClientTypes.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Shared/ClientType");

                return View("/Views/Administration/Shared/ClientType/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Shared/ClientType/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Shared/ClientType/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Shared/ClientType");
        }
    }
}