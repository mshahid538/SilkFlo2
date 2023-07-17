using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core;
using SilkFlo.Web.ViewModels;

namespace SilkFlo.Web.APIControllers.Business
{
    public class APITeamController : Controllers.AbstractAPI
    {
        public APITeamController(Data.Core.IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/api/business/team/departmentId/{id}")]
        public async Task<IActionResult> GetBusinessTeam(string id)
        {
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            var content = "<option value=\"\">Select...</option>";

            var userId = GetUserId();

            // Guard Clause
            if (userId == null)
                return Content(content);

            var user = await _unitOfWork.Users.GetAsync(userId);

            // Guard Clause
            if (user == null)
                return Content(content);


            var tenant = await GetClientAsync();

            if(tenant == null)
                return Content(content);


            var cores = (await _unitOfWork.BusinessTeams.FindAsync(x => x.ClientId == tenant.Id
            && x.DepartmentId == id)).ToArray();

            // Guard Clause
            if(!cores.Any())
                return Content(content);


            const string template = "<option value=\"{id}\">{name}</option>";
            foreach (var core in cores)
            {
                var s = template.Replace("{id}", core.Id);
                s = s.Replace("{name}", core.Name);

                content += s;
            }


            return Content(content);
        }



        [HttpPost("/api/Business/Department/GetFilterTeamElements")]
        public async Task<IActionResult> GetFilterTeamElements()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return BadRequest("Error: Unauthorised");


            var departments = await GetModelAsync<List<Models.Business.Department>>();

            var teams = (await _unitOfWork.BusinessTeams
                .FindAsync(team => departments.Any(department => department.Id == team.DepartmentId))).ToArray();


            var content = "";
            if (teams.Any())
            {
                content += "<div class=\"form-check\"><input displayName=\"All\" class=\"form-check-input\" type=\"checkbox\" id=\"toggleFilterTeam\" onclick=\"SilkFlo.Pages.Cards.FilterCriteria.ToggleFilters(event, 'filterTeam');\"><label class=\"form-check-label\" for=\"toggleFilterTeam\" style=\"white-space:nowrap;\"><i>Select All</i></label></div>";
                foreach (var team in teams)
                    content += $"<div class=\"form-check\"><input displayName=\"{team.Name}\" name=\"filterTeam\" class=\"form-check-input\" type=\"checkbox\" id=\"{team.Id}\" onclick=\"SilkFlo.Pages.Cards.FilterCriteria.UnSelectFilter( event, 'toggleFilterTeam');\"><label style=\"white-space:nowrap;\" class=\"form-check-label\" for=\"{team.Id}\">{team.Name}</label></div>";
            }
            else
            {
                content = "No Teams Present";
            }


            return Content(content);
        }


        [HttpPost("/api/Business/Team/Post")]
        public async Task<IActionResult> Post([FromBody] Models.Business.Team model)
        {
            var feedback = new Feedback();
            feedback.DangerMessage("Unauthorised");

            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return BadRequest(feedback);

            if (model == null)
            {
                feedback.Message = "Area missing.";
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



            var team = await _unitOfWork.BusinessTeams.GetAsync(model.Id);

            if (team.ClientId != client.Id)
                return BadRequest(feedback);

            if (team.Name == model.Name)
                return Ok("Saved Area");

            try
            {
                await _unitOfWork.AddAsync(model.GetCore());
                await _unitOfWork.CompleteAsync();

                return Ok("Saved Area");
            }
            catch (Exception ex)
            {
                feedback.Message = ex.Message;
                return BadRequest(feedback);
            }
        }

        [HttpDelete("/api/Models/Business/Team/Delete/Id/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var feedback = new Feedback();
            feedback.DangerMessage("Unauthorised");

            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return BadRequest(feedback);

            try
            {
                var result = await _unitOfWork.BusinessTeams.RemoveAsync(id);

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