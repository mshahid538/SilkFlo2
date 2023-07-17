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
    public partial class PageController : Abstract
    {
        public PageController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Application/Page")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Application.Page> cores;

                cores = (await _unitOfWork.ApplicationPages
                                         .GetAllAsync())
                                         .ToArray();

                var models = new List<Models.Application.Page>();

                if(cores != null)
                {
                    cores = cores.OrderBy(m => m.Sort)
                                 .ThenBy(m => m.Name);

                    foreach(var core in cores)
                    {
                        var model = new Models.Application.Page(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Application.Page>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Application/Page/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Application.Page>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Application/Page/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Application/Page/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/application/page");

            var page = await _unitOfWork.ApplicationPages.GetAsync(id);

            if (page == null)
                return Redirect("/Administration/Application/Page");
            else
            {
                if (page.CreatedById != null)
                    page.CreatedBy = await _unitOfWork.Users.GetAsync(page.CreatedById);

                if (page.UpdatedById != null)
                    page.UpdatedBy = await _unitOfWork.Users.GetAsync(page.UpdatedById);

                var model = new Models.Application.Page(page);

                return View("/Views/Administration/Application/Page/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Application/Page/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Application.Page model)
        {
            if (model == null)
                return Redirect("/Administration/Application/Page");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Application/Page/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Application/Page/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.ApplicationPages
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Application/Page");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Application/Page/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Application/Page/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the Page.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Application/Page/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Application/Page/Edit/{id}")]
        [HttpGet("/Administration/Application/Page/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Application.Page page;

            if (string.IsNullOrWhiteSpace(id))
            {
                page = new Data.Core.Domain.Application.Page
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                page = await _unitOfWork.ApplicationPages.GetAsync(id);

                if (page == null)
                    return Redirect(
                        "/Administration/Application/Page");
            }

            var model = new Models.Application.Page(page);
            await model.GetCreatedAndUpdated(_unitOfWork);

            return View("/Views/Administration/Application/Page/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Application/Page/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Application.Page model)
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

                    return View("/Views/Administration/Application/Page/Edit.cshtml", model);
                }


                // Is the model valid?
                // If not return to view.
                bool isValid = Validate(_unitOfWork, model);
                if (!isValid)
                {
                    return View("/Views/Administration/Application/Page/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.ApplicationPages.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Application/Page");

                return View("/Views/Administration/Application/Page/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Application/Page/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Application/Page/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Application/Page");
        }
    }
}