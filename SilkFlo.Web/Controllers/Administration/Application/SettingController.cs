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

namespace SilkFlo.Web.Controllers.Administration.Application
{
    public partial class SettingController : Abstract
    {
        public SettingController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Application/Setting")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Application.Setting> cores;

                cores = (await _unitOfWork.ApplicationSettings
                                         .GetAllAsync())
                                         .ToArray();

                var models = new List<Models.Application.Setting>();

                if(cores != null)
                {
                    foreach(var core in cores)
                    {
                        var model = new Models.Application.Setting(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Application.Setting>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Application/Setting/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Application.Setting>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Application/Setting/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Application/Setting/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/application/setting");

            var setting = await _unitOfWork.ApplicationSettings.GetAsync(id);

            if (setting == null)
                return Redirect("/Administration/Application/Setting");
            else
            {
                if (setting.CreatedById != null)
                    setting.CreatedBy = await _unitOfWork.Users.GetAsync(setting.CreatedById);

                if (setting.UpdatedById != null)
                    setting.UpdatedBy = await _unitOfWork.Users.GetAsync(setting.UpdatedById);

                var model = new Models.Application.Setting(setting);

                return View("/Views/Administration/Application/Setting/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Application/Setting/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Application.Setting model)
        {
            if (model == null)
                return Redirect("/Administration/Application/Setting");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Application/Setting/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Application/Setting/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.ApplicationSettings
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Application/Setting");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Application/Setting/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Application/Setting/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the Setting.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Application/Setting/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Application/Setting/Edit/{id}")]
        [HttpGet("/Administration/Application/Setting/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Application.Setting setting;

            if (string.IsNullOrWhiteSpace(id))
            {
                setting = new Data.Core.Domain.Application.Setting
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                setting = await _unitOfWork.ApplicationSettings.GetAsync(id);

                if (setting == null)
                    return Redirect(
                        "/Administration/Application/Setting");
            }

            var model = new Models.Application.Setting(setting);
            await model.GetCreatedAndUpdated(_unitOfWork);

            return View("/Views/Administration/Application/Setting/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Application/Setting/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Application.Setting model)
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

                    return View("/Views/Administration/Application/Setting/Edit.cshtml", model);
                }


                // Is the model valid?
                // If not return to view.
                bool isValid = Validate(_unitOfWork, model);
                if (!isValid)
                {
                    return View("/Views/Administration/Application/Setting/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.ApplicationSettings.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Application/Setting");

                return View("/Views/Administration/Application/Setting/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Application/Setting/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Application/Setting/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Application/Setting");
        }
    }
}