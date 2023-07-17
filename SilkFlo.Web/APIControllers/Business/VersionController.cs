using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkFlo.Data.Core;
using SilkFlo.Data.Persistence;
using SilkFlo.Web.Controllers;
using SilkFlo.Web.ViewModels;

namespace SilkFlo.Web.APIControllers.Business
{
    public class VersionController : AbstractAPI
    {
        public VersionController(IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }

        [HttpPost("/api/Business/Version/Post")]
        public async Task<IActionResult> Post([FromBody] Models.Business.Version model)
        {
            var feedback = new Feedback();
            feedback.DangerMessage("Unauthorised");


            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return BadRequest(feedback);


            // Guard Clause
            if (model == null
                || string.IsNullOrWhiteSpace(model.Name))
            {
                feedback.Message = "Version missing.";
                return BadRequest(feedback);
            }


            // Guard Clause
            if (model.Application == null
                || string.IsNullOrWhiteSpace(model.Application.Name))
            {
                feedback.Message = "Application missing.";
                return BadRequest(feedback);
            }


            var client = await GetClientAsync();


            // Authorization Clause
            if (client == null)
                return BadRequest(feedback);


            // Is there a name conflict?
            var versions = (await _unitOfWork.BusinessVersions.FindAsync(x => x.ClientId == client.Id 
                                                                              && x.Name.ToLower() == model.Name.ToLower())).ToArray();
            if (versions.Any())
            {
                await _unitOfWork.BusinessApplications.GetApplicationForAsync(versions);

                var version = versions.SingleOrDefault(x =>
                    x.Id != model.Id
                    && x.Name.ToLower() == model.Name.ToLower()
                    && x.Application.Name.ToLower() == model.Application.Name.ToLower());

                if (version != null)
                {
                    feedback.Message = $"{model.Application.Name} {model.Name} already exists";
                    return BadRequest(feedback);
                }
            }

            var saveMe = false;



            // *** Deal with Application ***
            var applicationByName =
                await _unitOfWork.BusinessApplications
                    .SingleOrDefaultAsync(x => string.Equals(x.Name, model.Application.Name, StringComparison.CurrentCultureIgnoreCase)
                                               && x.ClientId == client.Id);

            var applicationById =
                await _unitOfWork.BusinessApplications.GetAsync(model.Application.Id);


            Data.Core.Domain.Business.Application application;


            // Deal with application
            var isApplicationRename = false;
            if (applicationByName == null && applicationById == null)
            {
                // new application
                application = new Data.Core.Domain.Business.Application
                {
                    Name = model.Application.Name,
                    Client = client,
                };

                saveMe = true;
            }
            else if (applicationByName == null)
            {
                // rename
                application = applicationById;
                application.Name = model.Application.Name;
                isApplicationRename = true;
                saveMe = true;
            }
            else
            {
                application = applicationByName;

                // check Case change
                if (application.Name != model.Application.Name)
                {
                    application.Name = model.Application.Name;
                    isApplicationRename = true;
                    saveMe = true;
                }
            }






            // Deal with version
            var core = await _unitOfWork.BusinessVersions.GetAsync(model.Id);
            if (core == null)
            {
                core = model.GetCore();
                core.Client = client;
                saveMe = true;
            }
            else if (core.Name != model.Name)
            {
                core.Name = model.Name;
                saveMe = true;
            }


            core.Application = application;

            if (core.IsLive != model.IsLive)
                saveMe = true;

            core.IsLive = model.IsLive;


            if(core.PlannedUpdateDate != model.PlannedUpdateDate)
                saveMe = true;


            core.PlannedUpdateDate = model.PlannedUpdateDate;


            if (!saveMe)
                return Ok();


            try
            {
                await _unitOfWork.AddAsync(core);
                await _unitOfWork.AddAsync(application);
                await _unitOfWork.CompleteAsync();

                string json = "{\"id\": \""+ model.Id +"\",";
                json += "\"applicationId\": \"" + application.Id +"\",";

                if (isApplicationRename)
                {
                    json += "\"applicationName\": \"" + application.Name + "\",";
                }

                json = json.Substring(0, json.Length - 1);

                json += "}";

                return Ok(json);
            }
            catch (InvalidFieldsException ex)
            {
                feedback.Message = ex.Message;
                return BadRequest(feedback);

            }
            catch (UniqueConstraintException ex)
            {
                feedback.Message = ex.Message;
                return BadRequest(feedback);
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.Message = "Save Failed";
                return BadRequest(feedback);
            }
        }

        [HttpDelete("/api/Business/Version/Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var feedback = new Feedback();
            feedback.DangerMessage("Unauthorised");

            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return BadRequest(feedback);


            id = id.Trim();

            try
            {
                var result = await _unitOfWork.BusinessVersions.RemoveAsync(id);

                if (result == DataStoreResult.NotFound)
                {
                    feedback.Message = "Not Found";
                    return BadRequest(feedback);
                }

                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            catch (ChildDependencyException ex)
            {
                feedback.Message = ex.Message;
                return BadRequest(feedback);
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.Message = "Delete Failed.";
                return BadRequest(feedback);
            }
        }
    }
}