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
    public partial class LocationController : Abstract
    {
        public LocationController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Business/Location")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Business.Location> cores;

                cores = (await _unitOfWork.BusinessLocations
                                         .GetAllAsync())
                                         .ToArray();

                await _unitOfWork.BusinessClients.GetClientForAsync(cores);

                var models = new List<Models.Business.Location>();

                if(cores != null)
                {
                    cores = cores.OrderBy(m => m.Name);

                    foreach(var core in cores)
                    {
                        var model = new Models.Business.Location(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Business.Location>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Business/Location/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Business.Location>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Business/Location/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Business/Location/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/business/location");

            var location = await _unitOfWork.BusinessLocations.GetAsync(id);

            if (location == null)
                return Redirect("/Administration/Business/Location");
            else
            {
                await _unitOfWork.BusinessClients.GetClientForAsync(location);

                if (location.CreatedById != null)
                    location.CreatedBy = await _unitOfWork.Users.GetAsync(location.CreatedById);

                if (location.UpdatedById != null)
                    location.UpdatedBy = await _unitOfWork.Users.GetAsync(location.UpdatedById);

                var model = new Models.Business.Location(location);

                return View("/Views/Administration/Business/Location/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Business/Location/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Business.Location model)
        {
            if (model == null)
                return Redirect("/Administration/Business/Location");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Business/Location/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Business/Location/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.BusinessLocations
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Business/Location");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Business/Location/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Business/Location/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the Location.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Business/Location/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Business/Location/Edit/{id}")]
        [HttpGet("/Administration/Business/Location/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Business.Location location;

            if (string.IsNullOrWhiteSpace(id))
            {
                location = new Data.Core.Domain.Business.Location
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                location = await _unitOfWork.BusinessLocations.GetAsync(id);

                if (location == null)
                    return Redirect(
                        "/Administration/Business/Location");
                await _unitOfWork.BusinessClients.GetClientForAsync(location);

            }

            var model = new Models.Business.Location(location);
            await model.GetCreatedAndUpdated(_unitOfWork);

            var coreClients = await _unitOfWork.BusinessClients.GetAllAsync();
            model.Clients.AddRange(Models.Business.Client.Create(coreClients));

            return View("/Views/Administration/Business/Location/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Business/Location/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Business.Location model)
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

                    return View("/Views/Administration/Business/Location/Edit.cshtml", model);
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


                    return View("/Views/Administration/Business/Location/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.BusinessLocations.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Business/Location");

                return View("/Views/Administration/Business/Location/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Business/Location/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Business/Location/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Business/Location");
        }
    }
}