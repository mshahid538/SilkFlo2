using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SilkFlo.Web.Controllers
{
    public class WorkshopController : AbstractAPI
    {
        public WorkshopController(Data.Core.IUnitOfWork unitOfWork,
                                   Services.ViewToString viewToString,
                                   IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }

        [Route("/Workshop/StageGroup/{stageGroup}")]
        [Authorize]
        public async Task<IActionResult> Workshop(string stageGroup)
        {
            return await WorkshopViewAsync(false, stageGroup);
        }

        [Route("/api/Workshop/StageGroup/{stageGroup}")]
        [Authorize]
        public async Task<IActionResult> WorkshopAPI(string stageGroup)
        {
            return await WorkshopViewAsync(true, stageGroup);
        }

        [Route("/api/Workshop/All")]
        public async Task<IActionResult> WorkshopAllAPI(DateTime? startDate, DateTime? endDate, bool? isWeekly, bool? isMonthly, bool? isYearly, string processOwners, string ideaSubmitters, string departmentsId, string teamsId)
        {
            return await WorkshopViewAsync(true, "All", startDate, endDate, isWeekly,  isMonthly, isYearly, processOwners, ideaSubmitters, departmentsId, teamsId);
        }

        [Route("/api/Workshop/Build")]
        public async Task<IActionResult> WorkshopBuildAPI()
        {
            return await WorkshopViewAsync(true, "Build");
        }

        private async Task<IActionResult> WorkshopViewAsync(bool returnStringContent,
                                           string stageGroup,
										   DateTime? startDate = null, DateTime? endDate = null, bool? isWeekly = null, bool? isMonthly = null, 
                                           bool? isYearly = null, string processOwners = "", string ideaSubmitters = "", string departmentsId = "", string teamsId = "")
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
            {
                if(returnStringContent)
                    return NegativeFeedback();

                return Redirect("/account/signin");
            }


            var clientCore = await GetClientAsync();

            const string errorPage = "../Home/Page";

            if (clientCore == null)
            {
                const string content = "<h1>No tenant selected</h1>";
                if(returnStringContent)
                    return Content(content);

                return View(errorPage, content);
            }

            var client = new Models.Business.Client(clientCore);

            var product = GetProductCookie();

            var viewModel = await ViewModels.Workshop
                .Page.BuildAsync(stageGroup,
                    Models.Shared.StageGroup.Create(await _unitOfWork.SharedStageGroups.GetAllAsync()),
                    client.GetCore(),
                    _unitOfWork,
                    _authorization,
                    User,
                    product,
					startDate, endDate, isWeekly, isMonthly, isYearly, processOwners, ideaSubmitters, departmentsId, teamsId);
            

            // Return the view.
            const string url = "/Views/Workshop/Page.cshtml";
            if (returnStringContent)
            {
                var html = await _viewToString.PartialAsync(url, viewModel);
                return Content(html);
            }

            return View(url, viewModel);
        }
    }
}