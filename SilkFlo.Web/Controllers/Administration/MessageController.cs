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

namespace SilkFlo.Web.Controllers.Administration
{
    public partial class MessageController : Abstract
    {
        public MessageController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Message")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Message> cores;

                cores = (await _unitOfWork.Messages
                                         .GetAllAsync())
                                         .ToArray();

                await _unitOfWork.BusinessClients.GetClientForAsync(cores);
                await _unitOfWork.Users.GetUserForAsync(cores);

                var models = new List<Models.Message>();

                if(cores != null)
                {
                    foreach(var core in cores)
                    {
                        var model = new Models.Message(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Message>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Message/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Message>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Message/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Message/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/message");

            var message = await _unitOfWork.Messages.GetAsync(id);

            if (message == null)
                return Redirect("/Administration/Message");
            else
            {
                await _unitOfWork.BusinessClients.GetClientForAsync(message);
                await _unitOfWork.Users.GetUserForAsync(message);

                if (message.CreatedById != null)
                    message.CreatedBy = await _unitOfWork.Users.GetAsync(message.CreatedById);

                if (message.UpdatedById != null)
                    message.UpdatedBy = await _unitOfWork.Users.GetAsync(message.UpdatedById);

                var model = new Models.Message(message);

                return View("/Views/Administration/Message/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Message/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Message model)
        {
            if (model == null)
                return Redirect("/Administration/Message");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Message/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Message/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.Messages
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Message");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Message/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Message/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the Message.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Message/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Message/Edit/{id}")]
        [HttpGet("/Administration/Message/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Message message;

            if (string.IsNullOrWhiteSpace(id))
            {
                message = new Data.Core.Domain.Message
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                message = await _unitOfWork.Messages.GetAsync(id);

                if (message == null)
                    return Redirect(
                        "/Administration/Message");
                await _unitOfWork.BusinessClients.GetClientForAsync(message);
                await _unitOfWork.Users.GetUserForAsync(message);

            }

            var model = new Models.Message(message);
            await model.GetCreatedAndUpdated(_unitOfWork);

            var coreClients = await _unitOfWork.BusinessClients.GetAllAsync();
            model.Clients.AddRange(Models.Business.Client.Create(coreClients));
            var coreUsers = await _unitOfWork.Users.GetAllAsync();
            model.Users.AddRange(Models.User.Create(coreUsers));

            return View("/Views/Administration/Message/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Message/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Message model)
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

                    return View("/Views/Administration/Message/Edit.cshtml", model);
                }


                // Is the model valid?
                // If not return to view.
                bool isValid = Validate(_unitOfWork, model);
                if (!isValid)
                {
                    // Get parent: Business.Client -> Client
                    await _unitOfWork.BusinessClients.GetClientForAsync(model.GetCore());
                    model.Client =
                        model.GetCore().Client == null ?
                            null :
                            new Models.Business.Client(model.GetCore().Client);


                    // Get parent: User -> User
                    await _unitOfWork.Users.GetUserForAsync(model.GetCore());
                    model.User =
                        model.GetCore().User == null ?
                            null :
                            new Models.User(model.GetCore().User);

                    // Get list of potential parents: Business.Client -> Client
                    var coreClients = await _unitOfWork.BusinessClients.GetAllAsync();
                    foreach (var core in coreClients)
                        model.Clients.Add(new Models.Business.Client(core));

                    // Get list of potential parents: User -> User
                    var coreUsers = await _unitOfWork.Users.GetAllAsync();
                    foreach (var core in coreUsers)
                        model.Users.Add(new Models.User(core));


                    return View("/Views/Administration/Message/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.Messages.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Message");

                return View("/Views/Administration/Message/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Message/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Message/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Message");
        }
    }
}