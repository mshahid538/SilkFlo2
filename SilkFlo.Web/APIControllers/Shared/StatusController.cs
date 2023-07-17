using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.APIControllers.Business
{
    public partial class StatusController : Controllers.AbstractAPI
    {
        public StatusController(Data.Core.IUnitOfWork unitOfWork,
                                Services.ViewToString viewToString,
                                IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }




        [HttpPost("/api/Shared/Stage/GetStatuses")]
        public async Task<IActionResult> GetStatuses()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return BadRequest("Error: Unauthorised");

            string content = "";

            try
            {
                var stages = await GetModelAsync<List<Models.Shared.Stage>>();

                var ideaStatuses = (await _unitOfWork.SharedIdeaStatuses
                                             .FindAsync(ideaStatus => stages.Any(stage => stage.Id == ideaStatus.StageId))).ToArray();


                if (!ideaStatuses.Any())
                {
                    content = "No Statuses Present";
                }
                else
                {
                    content += "<div class=\"form-check\"><input displayName=\"All\" class=\"form-check-input\" type=\"checkbox\" id=\"toggleFilterStatus\" onclick=\"SilkFlo.Pages.Cards.FilterCriteria.ToggleFilters(event, 'filterStatus');\"><label class=\"form-check-label\" for=\"toggleFilterStatus\" style=\"white-space:nowrap;\"><i>Select All</i></label></div>";

                    ideaStatuses = ideaStatuses.GroupBy(x => x.Name)
                                               .Select(g => g.OrderBy(y => y.Name).First())
                                               .ToArray();

                    ideaStatuses = ideaStatuses.OrderBy(x => x.Name).ToArray();


                    foreach (var ideaStatus in ideaStatuses)
                    {
                        content += $"<div class=\"form-check\"><input displayName=\"{ideaStatus.Name}\" name=\"filterStatus\" class=\"form-check-input\" type=\"checkbox\" id=\"{ideaStatus.Id}\" onclick=\"SilkFlo.Pages.Cards.FilterCriteria.UnSelectFilter(event, 'toggleFilterStatus');\"><label style=\"white-space:nowrap;\" class=\"form-check-label\" for=\"{ideaStatus.Name}\">{ideaStatus.Name}</label></div>";
                    }
                }
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Log(ex);
                return Content("<span class=\"text-danger\">Error returning records</span>");
            }

            

            return Content(content);
        }
    }
}