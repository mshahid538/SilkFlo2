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
    public partial class DocumentController : Abstract
    {
        public DocumentController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Business/Document")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Business.Document> cores;

                cores = (await _unitOfWork.BusinessDocuments
                                         .GetAllAsync())
                                         .ToArray();

                await _unitOfWork.BusinessClients.GetClientForAsync(cores);
                await _unitOfWork.BusinessIdeas.GetIdeaForAsync(cores);

                var models = new List<Models.Business.Document>();

                if(cores != null)
                {
                    cores = cores.OrderBy(m => m.Client?.ToString())
                                 .ThenBy(m => m.Filename)
                                 .ThenBy(m => m.Text);

                    foreach(var core in cores)
                    {
                        var model = new Models.Business.Document(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Business.Document>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Business/Document/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Business.Document>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Business/Document/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Business/Document/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/business/document");

            var document = await _unitOfWork.BusinessDocuments.GetAsync(id);

            if (document == null)
                return Redirect("/Administration/Business/Document");
            else
            {
                await _unitOfWork.BusinessClients.GetClientForAsync(document);
                await _unitOfWork.BusinessIdeas.GetIdeaForAsync(document);

                if (document.CreatedById != null)
                    document.CreatedBy = await _unitOfWork.Users.GetAsync(document.CreatedById);

                if (document.UpdatedById != null)
                    document.UpdatedBy = await _unitOfWork.Users.GetAsync(document.UpdatedById);

                var model = new Models.Business.Document(document);

                return View("/Views/Administration/Business/Document/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Business/Document/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Business.Document model)
        {
            if (model == null)
                return Redirect("/Administration/Business/Document");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Business/Document/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Business/Document/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.BusinessDocuments
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Business/Document");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Business/Document/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Business/Document/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the Document.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Business/Document/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Business/Document/Edit/{id}")]
        [HttpGet("/Administration/Business/Document/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Business.Document document;

            if (string.IsNullOrWhiteSpace(id))
            {
                document = new Data.Core.Domain.Business.Document
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                document = await _unitOfWork.BusinessDocuments.GetAsync(id);

                if (document == null)
                    return Redirect(
                        "/Administration/Business/Document");
                await _unitOfWork.BusinessClients.GetClientForAsync(document);
                await _unitOfWork.BusinessIdeas.GetIdeaForAsync(document);

            }

            var model = new Models.Business.Document(document);
            await model.GetCreatedAndUpdated(_unitOfWork);

            var coreClients = await _unitOfWork.BusinessClients.GetAllAsync();
            model.Clients.AddRange(Models.Business.Client.Create(coreClients));

            return View("/Views/Administration/Business/Document/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Business/Document/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Business.Document model)
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

                    return View("/Views/Administration/Business/Document/Edit.cshtml", model);
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


                    return View("/Views/Administration/Business/Document/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.BusinessDocuments.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Business/Document");

                return View("/Views/Administration/Business/Document/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Business/Document/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Business/Document/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Business/Document");
        }
    }
}