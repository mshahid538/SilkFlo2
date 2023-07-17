using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Web.ViewModels;

namespace SilkFlo.Web.APIControllers.Business
{
    public class APIProcessController : Controllers.AbstractAPI
    {
        public APIProcessController(Data.Core.IUnitOfWork unitOfWork,
                                    Services.ViewToString viewToString,
                                    IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/api/business/process/team/{id}")]
        public async Task<IActionResult> GetBusinessTeam(string id)
        {
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            string content = "<option value=\"\">Select...</option>";

            var userId = GetUserId();

            // Guard Clause
            if (userId == null)
                return Content(content);

            var user = await _unitOfWork.Users.GetAsync(userId);

            // Guard Clause
            if (user == null)
                return Content(content);



            var tenant = await GetClientAsync();

            if (tenant == null)
                return Content(content);

            var cores = await _unitOfWork.BusinessProcesses
                                   .FindAsync(x => x.ClientId == tenant.Id
                                           && x.TeamId == id);

            // Guard Clause
            var enumerable = cores as Process[] ?? cores.ToArray();
            if(!enumerable.Any())
                return Content(content);


            const string template = "<option value=\"{id}\">{name}</option>";
            foreach (var core in enumerable)
            {
                var s = template.Replace("{id}", core.Id);
                s = s.Replace("{name}", core.Name);

                content += s;
            }


            return Content(content);
        }


        [HttpPost("/api/Business/Process/Post")]
        public async Task<IActionResult> Post([FromBody] Models.Business.Process model)
        {
            var feedback = new Feedback();
            feedback.DangerMessage("Unauthorised");

            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return BadRequest(feedback);

            if (model == null)
            {
                feedback.Message = "Sub-Area missing.";
                return BadRequest(feedback);
            }


            var client = await GetClientAsync();

            if (client == null)
                return BadRequest(feedback);

            model.ClientId = client.Id;


            if (string.IsNullOrWhiteSpace(model.Id))
            {
                try
                {
                    await _unitOfWork.AddAsync(model.GetCore());
                    await _unitOfWork.CompleteAsync();

                    return Ok(model.Id);
                }
                catch (Exception ex)
                {
                    feedback.Message = ex.Message;
                    return BadRequest(feedback);
                }
            }



            var process = await _unitOfWork.BusinessProcesses.GetAsync(model.Id);

            if (process.ClientId != client.Id)
                return BadRequest(feedback);

            if (process.Name == model.Name)
                return Ok("Saved Sub-Area");

            try
            {
                await _unitOfWork.AddAsync(model.GetCore());
                await _unitOfWork.CompleteAsync();

                return Ok("Saved Sub-Area");
            }
            catch (Exception ex)
            {
                feedback.Message = ex.Message;
                return BadRequest(feedback);
            }
        }


        [HttpDelete("/api/Models/Business/Process/Delete/Id/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var feedback = new Feedback();
            feedback.DangerMessage("Unauthorised");

            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return BadRequest(feedback);


            try
            {
                var result = await _unitOfWork.BusinessProcesses.RemoveAsync(id);

                if (result == Data.Core.DataStoreResult.NotFound)
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
                feedback.Message = "Delete Failed";
                return BadRequest(feedback);
            }
        }
    }
}