using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SVGChartTools.DataSet;
using SilkFlo.Web.Controllers;
using Newtonsoft.Json.Linq;
using SilkFlo.Web.ViewModels.Workshop;
using SilkFlo.Data.Persistence;

namespace SilkFlo.Web.APIControllers
{
    public class WorkshopController : Controllers.AbstractAPI
    {
        public WorkshopController(Data.Core.IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization)
        {
        }

        [Route("/Workshop")]
        [Route("/Workshop/StageGroup")]
        [Route("/Workshop/All")]

        public async Task<IActionResult> GetStage()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            return Redirect("/Workshop/StageGroup/All");
        }


        [Route("/api/Workshop/StageGroup/Id/{stageGroup}")]
        public async Task<IActionResult> GetStage(string stageGroup, string processOwners, string ideaSubmitters, string departmentsId, string teamsId,
                                                  DateTime? startDate, DateTime? endDate, bool? isWeekly, bool? isMonthly, bool? isYearly, string guid = "")
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();


            var client = new Models.Business.Client(clientCore);


            var product = GetProductCookie();

            //if (stageGroup == "Deployed")
            //{
            //    var htmlDeployed = await ViewModels.Workshop.PipeLineDeployed.GetHtmlAsync(
            //        _unitOfWork,
            //        _authorization,
            //        User,
            //        client,
            //        _viewToString,
            //        product);

            //    return Content(htmlDeployed);
            //}

            var stageGroupCore = await _unitOfWork.SharedStageGroups.GetByNameAsync(stageGroup);

            await _unitOfWork.BusinessIdeas.GetForClientAsync(client.GetCore());

            var tileUrls = new List<string>();

            switch (stageGroup.ToLower())
            {
                case "review":
                    tileUrls.Add("StageGroup/" + stageGroup + "/TotalIdeas");
                    tileUrls.Add("StageGroup/" + stageGroup + "/AwaitingReview");
                    break;
                case "assess":
                    tileUrls.Add("StageGroup/" + stageGroup + "/TotalIdeas");
                    tileUrls.Add("StageGroup/" + stageGroup + "/PotentialBenefit");
                    tileUrls.Add("StageGroup/" + stageGroup + "/PotentialHourSavings");
                    tileUrls.Add("StageGroup/" + stageGroup + "/AwaitingReview");
                    break;
                case "decision":
                    tileUrls.Add("StageGroup/" + stageGroup + "/PotentialBenefit");
                    tileUrls.Add("StageGroup/" + stageGroup + "/EstimatedOneTimeCost");
                    tileUrls.Add("StageGroup/" + stageGroup + "/EstimatedRunningCosts");
                    break;
                case "build":
                    tileUrls.Add("StageGroup/" + stageGroup + "/TotalInBuild");
                    tileUrls.Add("StageGroup/" + stageGroup + "/PotentialBenefit");
                    tileUrls.Add("StageGroup/" + stageGroup + "/TotalAtRisk");
                    tileUrls.Add("StageGroup/" + stageGroup + "/TotalBenefitAtRisk");
                    tileUrls.Add("StageGroup/" + stageGroup + "/EstimatedOneTimeCost");
                    //tileUrls.Add("StageGroup/" + stageGroup + "/AverageBuildTime");
                    break;
                case "deployed":
                    tileUrls.Add("StageGroup/" + stageGroup + "/TotalInDeployed");
                    tileUrls.Add("StageGroup/" + stageGroup + "/PotentialBenefit");
                    tileUrls.Add("StageGroup/" + stageGroup + "/PotentialHourSavings");
                    break;
            }

            var pipeLine = await ViewModels.Workshop.PipeLine.BuildAsync(
            _unitOfWork,
            _authorization,
            stageGroupCore == null ? null : new Models.Shared.StageGroup(stageGroupCore),
            User,
            client.GetCore(),
            tileUrls);

            pipeLine.FilterTargetId = "Business.Idea.Summary";
             pipeLine.TargetUrl = "/api/Business/Idea/Build/FilterSummary";

            
            await pipeLine.GetIdeas(product);

            //adding List of pOwn & iSub
            var processOwnerList = new List<KeyValuePair<string, string>>();
            var ideaSubmitterList = new List<KeyValuePair<string, string>>();
            foreach (var x in pipeLine.IdeaSummary.Ideas)
            {
                var idea = x.GetCore();
                await _unitOfWork.Users.GetProcessOwnerForAsync(idea);

                if (!processOwnerList.Any(x => x.Value == idea.ProcessOwnerId))
                    processOwnerList.Add(new KeyValuePair<string, string>(idea.ProcessOwner.Fullname, idea.ProcessOwnerId));

                if (!ideaSubmitterList.Any(x => x.Value == idea.ProcessOwnerId))
                    ideaSubmitterList.Add(new KeyValuePair<string, string>(idea.ProcessOwner.Fullname, idea.ProcessOwnerId));
            }

            pipeLine.IdeaSummary.POList = processOwnerList;
            pipeLine.IdeaSummary.ISList = ideaSubmitterList;

            // Get departments
            var departmentsCore = await _unitOfWork.BusinessDepartments.FindAsync(x => x.ClientId == client.Id);
            foreach (var department in departmentsCore)
                pipeLine.IdeaSummary.Departments.Add(new Models.Business.Department(department));

            if (client.Ideas.Count == 0)
                pipeLine.IdeaSummary.NoIdeas = true;

            #region IdeaFiltersBehaviour
            if (isWeekly.HasValue && isWeekly.Value)
            {
                var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isMonthly.HasValue && isMonthly.Value)
            {
                var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isYearly.HasValue && isYearly.Value)
            {
                var previousYearStartDate = DateTime.Now.AddYears(-1);
                pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
            }

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(departmentsId))
            {
                var departmentsIdList = departmentsId.Split(",");
                pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(teamsId))
            {
                var teamsIdList = teamsId.Split(",");
                pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
            }

            #endregion

            var ideasList = pipeLine.IdeaSummary.Ideas;
            pipeLine.Ideas = ideasList.ToArray();

            var html = await _viewToString.PartialAsync("Shared/Workshop/_PipeLine.cshtml", pipeLine);
            return Content(html);
        }



        [HttpGet("api/Workshop/Deployed/Table")]
        [HttpGet("api/Workshop/Deployed/Table/Page/{pageNumber}")]
        [HttpGet("api/Workshop/Deployed/Table/Search/{text}")]
        [HttpGet("api/Workshop/Deployed/Table/Search/{text}/Page/{pageNumber}")]
        public async Task<IActionResult> GetTable(
            string text = "",
            int pageNumber = 1)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            try
            {
                var clientCore = await GetClientAsync();

                if (clientCore == null)
                    return NegativeFeedback();

                var client = new Models.Business.Client(clientCore);

                var product = GetProductCookie();

                var html = await ViewModels.Workshop.PipeLineDeployed.GetTableAsync(
                    _unitOfWork,
                    _authorization,
                    User,
                    client,
                    _viewToString,
                    product,
                    text,
                    pageNumber);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error Logged");
            }
        }

        [HttpGet("api/Workshop/GetAutomationBuildPipeline")]
        [HttpGet("api/Workshop/GetAutomationBuildPipeline/year/{year:int}")]
        public async Task<IActionResult> GetAutomationBuildPipeline(int? year = null, string? processOwners = null, string? ideaSubmitters = null,
                                                  string? departmentsId = null, string? teamsId = null, DateTime? startDate = null, DateTime? endDate = null,
                                                  bool? isWeekly = null, bool? isMonthly = null, bool? isYearly = null
)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();


            try
            {
                var client = await GetClientAsync();
                var currency = await GetTenantCurrencyAsync();

                if (client == null)
                    return NegativeFeedback();

                year ??= DateTime.Now.Year;

                var cores = (await _unitOfWork.BusinessIdeas
                        .FindAsync(x => x.ClientId == client.Id && !x.IsDraft))
                        .ToArray();

                var models = Models.Business.Idea.Create(cores);

                #region IdeaFiltersBehaviour
                if (isWeekly.HasValue && isWeekly.Value)
                {
                    var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                    models = models.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToArray();
                }
                else if (isMonthly.HasValue && isMonthly.Value)
                {
                    var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                    models = models.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToArray();
                }
                else if (isYearly.HasValue && isYearly.Value)
                {
                    var previousYearStartDate = DateTime.Now.AddYears(-1);
                    models = models.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToArray();
                }
                else if (startDate.HasValue && endDate.HasValue)
                {
                    models = models.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToArray();
                }

                if (!String.IsNullOrWhiteSpace(ideaSubmitters))
                {
                    var isList = ideaSubmitters.Split(",");
                    models = models.Where(x => isList.Contains(x.ProcessOwnerId)).ToArray();
                }

                if (!String.IsNullOrWhiteSpace(processOwners))
                {
                    var poList = processOwners.Split(",");
                    models = models.Where(x => poList.Contains(x.ProcessOwnerId)).ToArray();
                }

                if (!String.IsNullOrWhiteSpace(departmentsId))
                {
                    var departmentsIdList = departmentsId.Split(",");
                    models = models.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToArray();
                }

                if (!String.IsNullOrWhiteSpace(teamsId))
                {
                    var teamsIdList = teamsId.Split(",");
                    models = models.Where(x => teamsIdList.Contains(x.TeamId)).ToArray();
                }

                #endregion


                var buildIdeas = new List<Models.Business.Idea>();
                var deployedIdeas = new List<Models.Business.Idea>();

                foreach (var idea in models)
                {
                    await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(idea.GetCore());
                    await _unitOfWork.SharedStages.GetStageForAsync(idea.GetCore().IdeaStages);

                    var buildId = Data.Core.Enumerators.StageGroup.n03_Build.ToString();
                    var deployedId = Data.Core.Enumerators.StageGroup.n04_Deployed.ToString();

                    //if (idea.LastIdeaStage.Stage.StageGroupId == buildId)
                    //    buildIdeas.Add(idea);

                    //if (idea.LastIdeaStage.Stage.StageGroupId == deployedId)
                    //    deployedIdeas.Add(idea);

                    if(idea.IdeaStages.Any(x => x.Stage.StageGroupId == buildId && x.IsInWorkFlow))
                        buildIdeas.Add(idea);

                    if (idea.IdeaStages.Any(x => x.Stage.StageGroupId == deployedId && x.IsInWorkFlow))
                        deployedIdeas.Add(idea);

                }


                if (!buildIdeas.Any()
                    && !deployedIdeas.Any())
                    return Content("<h2>No ideas in build or deployed</h2>");


                var data = new Chart
                {
                    // X Axis
                    XAxisLabels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                };

                // Data set 1 - Build
                var dataSet = await ViewModels.Chart.DataSet.IdeaEstimate.Get(
                    buildIdeas,
                    (int)year,
                    currency,
                    "fill: var(--bs-warning-lighter); stroke: var(--bs-warning-lighter);",
                    "Build",
                    new List<Data.Core.Enumerators.StageGroup>
                    {
                        Data.Core.Enumerators.StageGroup.n03_Build,
                    },
                    _unitOfWork,
                    "Business.Idea.Summary",
                    "/api/Business/Idea/Build/FilterSummary",
                    false);

                data.DataSets.Add(dataSet);


                // Data set 2 - Deployed
                dataSet = await ViewModels.Chart.DataSet.IdeaEstimate.Get(
                    deployedIdeas,
                    (int)year,
                    currency,
                    "fill: var(--bs-green); stroke: var(--bs-green);",
                    "Deployed",
                    new List<Data.Core.Enumerators.StageGroup>
                    {
                        Data.Core.Enumerators.StageGroup.n04_Deployed
                    },
                    _unitOfWork,
                    "Business.Idea.Summary",
                    "/api/Business/Idea/Build/FilterSummary",
                    false);

                data.DataSets.Add(dataSet);


                var barChart = new ViewModels.Chart.Bar(data)
                {
                    YDivisionsCount = 8,
                    YLabel = "Estimated Benefit " + currency.Symbol
                };


                var html = await _viewToString.PartialAsync("Shared/Dashboard/Component/_AutomationProgramPerformance.cshtml",
                    new ViewModels.Dashboard.Component.AutomationProgramPerformance
                    {
                        BarChart = barChart,
                        Year = (int)year,
                        IncludeDiscoverKey = false
                    });
                return Content(html);
            }
            catch (Exception e)
            {
                _unitOfWork.Log(e);
                return NegativeFeedback("Error: Get data from data store.");
            }
        }



        [HttpGet("api/Business/Idea/GetSummary/GroupName/{name}")]
        public async Task<IActionResult> GetSummaryByGroupName(string name)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();


            await _unitOfWork.BusinessIdeas.GetForClientAsync(clientCore);


            await _unitOfWork.BusinessIdeas.GetForClientAsync(clientCore);

            var models = Models.Business.Idea.Create(clientCore.Ideas);

            // Get the stage groups
            foreach (var model in models)
            {
                var ideaStages =
                    (await (_unitOfWork.BusinessIdeaStages
                        .FindAsync(x => x.IdeaId == model.Id && x.IsInWorkFlow)))
                    .ToList();

                await _unitOfWork.SharedStages.GetStageForAsync(ideaStages);
                model.IdeaStages = Models.Business.IdeaStage.Create(ideaStages);
                model.IdeaStages = model.IdeaStages.OrderBy(x => x.DateStartEstimate).ToList();
            }

            var stageGroupId = name.ToLower() switch
            {
                "review" => Data.Core.Enumerators.StageGroup.n00_Review.ToString(),
                "assess" => Data.Core.Enumerators.StageGroup.n01_Assess.ToString(),
                "decision" => Data.Core.Enumerators.StageGroup.n02_Decision.ToString(),
                "build" => Data.Core.Enumerators.StageGroup.n03_Build.ToString(),
                "deployed" => Data.Core.Enumerators.StageGroup.n04_Deployed.ToString(),
                _ => ""
            };



            if (!string.IsNullOrWhiteSpace(stageGroupId))
                models = models.Where(x =>
                    !x.IsDraft
                    && x.LastIdeaStage?.Stage?.StageGroupId == stageGroupId).ToList();



            var ideaSummary = await ViewModels.Business.Idea.Summary.BuildAsync(_authorization, User); ;
            ideaSummary.Ideas.AddRange(models);

            var product = GetProductCookie();

            foreach (var idea in ideaSummary.Ideas)
            {
                idea.UnitOfWork = _unitOfWork;
                await idea.GetSummaryView_MetaDataAsync(product);
            }


            if (name == "Review")
                ideaSummary.IdeaScore = true;
            else
                ideaSummary.BenefitHours = true;

            ideaSummary.Goal = true;

            var html = await _viewToString.PartialAsync("Shared/Business/Idea/_Summary.cshtml", ideaSummary);

            return Content(html);
        }




        [HttpGet("api/tile/StageGroup/{stageGroupName}/TotalIdeas")]
        public async Task<IActionResult> GetTileStageGroupReviewTotalIdeas(string stageGroupName, string processOwners, string ideaSubmitters, 
                                                  string departmentsId, string teamsId, DateTime? startDate, DateTime? endDate, 
                                                  bool? isWeekly, bool? isMonthly, bool? isYearly)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var ideas = await Models.Shared.StageGroup.GetIdeasAsync(
                stageGroupName,
                new Models.Business.Client(clientCore),
                _unitOfWork);


            var monthCount = 0;
            var lastMonthCount = 0;

            var date = DateTime.Now;
            var month = date.Month;
            var year = date.Year;

            date = date.AddMonths(-1);
            var monthLast = date.Month;
            var yearLast = date.Year;


            #region IdeaFiltersBehaviour
            if (isWeekly.HasValue && isWeekly.Value)
            {
                var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isMonthly.HasValue && isMonthly.Value)
            {
                var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isYearly.HasValue && isYearly.Value)
            {
                var previousYearStartDate = DateTime.Now.AddYears(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
            }

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                ideas = ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                ideas = ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(departmentsId))
            {
                var departmentsIdList = departmentsId.Split(",");
                ideas = ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(teamsId))
            {
                var teamsIdList = teamsId.Split(",");
                ideas = ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
            }

            #endregion


            foreach (var ideaStage in ideas.Select(idea => idea.LastIdeaStage))
            {
                if (ideaStage.DateStartEstimate.Month == month
                    && ideaStage.DateStartEstimate.Year == year)
                    monthCount++;

                else if (ideaStage.DateStartEstimate.Month == monthLast && ideaStage.DateStartEstimate.Year == yearLast)
                    lastMonthCount++;
            }


            var total = ideas.Count();
            var totalChargeIn = DashboardController.GetChangeIn(lastMonthCount, monthCount);


            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Total Ideas",
                        total.ToString(),
                        "var(--bs-primary)",
                        "/Icons/Idea Solid.svg",
                        "",
                        "",
                        totalChargeIn,
                        "Total-ideas-in-" + stageGroupName));

            return Content(html);
        }

        [HttpGet("api/tile/StageGroup/{stageGroupName}/AwaitingReview")]
        public async Task<IActionResult> GetTileStageGroupReviewAwaitingReview(string stageGroupName, string processOwners, string ideaSubmitters,
                                                  string departmentsId, string teamsId, DateTime? startDate, DateTime? endDate,
                                                  bool? isWeekly, bool? isMonthly, bool? isYearly)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            stageGroupName = stageGroupName.ToLower();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var ideas = await Models.Shared.StageGroup.GetIdeasAsync(
                stageGroupName,
                new Models.Business.Client(clientCore),
                _unitOfWork);



            #region IdeaFiltersBehaviour
            if (isWeekly.HasValue && isWeekly.Value)
            {
                var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isMonthly.HasValue && isMonthly.Value)
            {
                var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isYearly.HasValue && isYearly.Value)
            {
                var previousYearStartDate = DateTime.Now.AddYears(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
            }

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                ideas = ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                ideas = ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(departmentsId))
            {
                var departmentsIdList = departmentsId.Split(",");
                ideas = ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(teamsId))
            {
                var teamsIdList = teamsId.Split(",");
                ideas = ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
            }

            #endregion




            foreach (var idea in ideas)
            {
                await _unitOfWork.BusinessIdeaStageStatuses.GetForIdeaStageAsync(idea.LastIdeaStage.GetCore());
                idea.LastIdeaStage.StatusId = idea.LastIdeaStage?.IdeaStageStatuses.Last()?.StatusId;
            }

            var statusId = stageGroupName switch
            {
                "review" => Data.Core.Enumerators.IdeaStatus.n00_Idea_AwaitingReview.ToString(),
                "assess" => Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
                _ => ""
            };

            ideas = ideas.Where(x =>
                    x.LastIdeaStage.StatusId == statusId)
                .ToList();

            var monthCount = 0;
            var lastMonthCount = 0;

            var date = DateTime.Now;
            var month = date.Month;
            var year = date.Year;

            date = date.AddMonths(-1);
            var monthLast = date.Month;
            var yearLast = date.Year;

            foreach (var ideaStage in ideas.Select(idea => idea.LastIdeaStage))
            {
                if (ideaStage.DateStartEstimate.Month == month
                    && ideaStage.DateStartEstimate.Year == year)
                    monthCount++;

                else if (ideaStage.DateStartEstimate.Month == monthLast && ideaStage.DateStartEstimate.Year == yearLast)
                    lastMonthCount++;
            }


            var total = ideas.Count();
            var totalChargeIn = DashboardController.GetChangeIn(lastMonthCount, monthCount);


            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Awaiting Review",
                        total.ToString(),
                        "var(--bs-warning)",
                        "/Icons/Clipboard.svg",
                        "",
                        "",
                        totalChargeIn,
                        "Total-ideas-in-review-Awaiting-Review"));
            return Content(html);
        }




        [HttpGet("api/tile/StageGroup/{stageGroupName}/PotentialBenefit")]
        public async Task<IActionResult> GetTileStageGroupAssessPotentialBenefit(string stageGroupName, string processOwners, string ideaSubmitters,
                                                  string departmentsId, string teamsId, DateTime? startDate, DateTime? endDate,
                                                  bool? isWeekly, bool? isMonthly, bool? isYearly)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();
            var ideas = await Models.Shared.StageGroup.GetIdeasAsync(
                stageGroupName,
                new Models.Business.Client(clientCore),
                _unitOfWork);

            decimal amount = 0;


            #region IdeaFiltersBehaviour
            if (isWeekly.HasValue && isWeekly.Value)
            {
                var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isMonthly.HasValue && isMonthly.Value)
            {
                var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isYearly.HasValue && isYearly.Value)
            {
                var previousYearStartDate = DateTime.Now.AddYears(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
            }

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                ideas = ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                ideas = ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(departmentsId))
            {
                var departmentsIdList = departmentsId.Split(",");
                ideas = ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(teamsId))
            {
                var teamsIdList = teamsId.Split(",");
                ideas = ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
            }

            #endregion



            foreach (var idea in ideas) {
                idea.UnitOfWork = _unitOfWork;
                amount += Convert.ToDecimal(await idea.GetBenefitPerCompanyCurrencyForWorkshop());


            }
               
            var amountString = "";
            if (amount > 1000)
            {
                amount = amount / 1000;
                amountString = amount.ToString("#,###") + "K";
            }
            else
                amountString = amount.ToString("#,###");

            await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(clientCore);

            if (clientCore.Currency != null)
                amountString = clientCore.Currency.Symbol + "&nbsp;" + amountString;


            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Potential Benefit",
                        amountString,
                        "var(--bs-primary)",
                        "/Icons/Idea Solid.svg",
                        "",
                        "",
                        0));

            return Content(html);
        }

        [HttpGet("api/tile/StageGroup/Assess/PotentialHourSavings")]
        public async Task<IActionResult> GetTileStageGroupAssessPotentialHourSavings(string processOwners, string ideaSubmitters,
                                                  string departmentsId, string teamsId, DateTime? startDate, DateTime? endDate,
                                                  bool? isWeekly, bool? isMonthly, bool? isYearly)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var ideas = await Models.Shared.StageGroup.GetIdeasAsync(
                "Assess",
                new Models.Business.Client(clientCore),
                _unitOfWork);


            decimal amount = 0;


            #region IdeaFiltersBehaviour
            if (isWeekly.HasValue && isWeekly.Value)
            {
                var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isMonthly.HasValue && isMonthly.Value)
            {
                var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isYearly.HasValue && isYearly.Value)
            {
                var previousYearStartDate = DateTime.Now.AddYears(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
            }

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                ideas = ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                ideas = ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(departmentsId))
            {
                var departmentsIdList = departmentsId.Split(",");
                ideas = ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(teamsId))
            {
                var teamsIdList = teamsId.Split(",");
                ideas = ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
            }

            #endregion


            foreach (var idea in ideas)
            {
                idea.UnitOfWork = _unitOfWork;
                amount += await idea.GetEstimateAsync(idea.GetBenefitPerCompany_Hours());
            }
              


            var amountString = "";
            if (amount > 1000)
            {
                amount = amount / 1000;
                amountString = amount.ToString("#,###") + "K";
            }
            else
                amountString = amount.ToString("#,###");



            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Potential&nbsp;Hour&nbsp;Savings",
                        amountString,
                        "var(--bs-success)",
                        "/Icons/Improvement Solid.svg",
                        "",
                        "",
                        0));

            return Content(html);
        }

        [HttpGet("api/tile/StageGroup/Deployed/PotentialHourSavings")]
        public async Task<IActionResult> GetTileStageGroupDeployedPotentialHourSavings(string processOwners, string ideaSubmitters,
                                                  string departmentsId, string teamsId, DateTime? startDate, DateTime? endDate,
                                                  bool? isWeekly, bool? isMonthly, bool? isYearly)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var ideas = await Models.Shared.StageGroup.GetIdeasAsync(
                "Deployed",
                new Models.Business.Client(clientCore),
                _unitOfWork);


            decimal amount = 0;

            #region IdeaFiltersBehaviour
            if (isWeekly.HasValue && isWeekly.Value)
            {
                var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isMonthly.HasValue && isMonthly.Value)
            {
                var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isYearly.HasValue && isYearly.Value)
            {
                var previousYearStartDate = DateTime.Now.AddYears(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
            }

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                ideas = ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                ideas = ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(departmentsId))
            {
                var departmentsIdList = departmentsId.Split(",");
                ideas = ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(teamsId))
            {
                var teamsIdList = teamsId.Split(",");
                ideas = ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
            }

            #endregion


            foreach (var idea in ideas) {
                idea.UnitOfWork = _unitOfWork;
                amount += await idea.GetEstimateAsync(idea.GetBenefitPerCompany_Hours());
            }
                


            var amountString = "";
            if (amount > 1000)
            {
                amount = amount / 1000;
                amountString = amount.ToString("#,###") + "K";
            }
            else
                amountString = amount.ToString("#,###");



            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Potential&nbsp;Hour&nbsp;Savings",
                        amountString,
                        "var(--bs-success)",
                        "/Icons/Improvement Solid.svg",
                        "",
                        "",
                        0));

            return Content(html);
        }


        [HttpGet("api/tile/StageGroup/{stageGroupName}/EstimatedOneTimeCost")]
        public async Task<IActionResult> GetTileStageGroupDecisionEstimatedOneTimeCost(string stageGroupName, string processOwners, string ideaSubmitters,
                                                  string departmentsId, string teamsId, DateTime? startDate, DateTime? endDate,
                                                  bool? isWeekly, bool? isMonthly, bool? isYearly)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var ideas = await Models.Shared.StageGroup.GetIdeasAsync(
                stageGroupName,
                new Models.Business.Client(clientCore),
                _unitOfWork);


            decimal amount = 0;


            #region IdeaFiltersBehaviour
            if (isWeekly.HasValue && isWeekly.Value)
            {
                var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isMonthly.HasValue && isMonthly.Value)
            {
                var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isYearly.HasValue && isYearly.Value)
            {
                var previousYearStartDate = DateTime.Now.AddYears(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
            }

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                ideas = ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                ideas = ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(departmentsId))
            {
                var departmentsIdList = departmentsId.Split(",");
                ideas = ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(teamsId))
            {
                var teamsIdList = teamsId.Split(",");
                ideas = ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
            }

            #endregion


            foreach (var idea in ideas) {
                idea.UnitOfWork = _unitOfWork;
                amount += Convert.ToDecimal(await idea.GetOneTimeCost());

            }

            var amountString = "";
            if (amount > 1000)
            {
                amount = amount / 1000;
                amountString = amount.ToString("#,###") + "K";
            }
            else
                amountString = amount.ToString("#,###");
            if (amountString == "") {
                amountString = "0.0";
            }
            if (clientCore.Currency != null)
                amountString = clientCore.Currency.Symbol + "&nbsp;" + amountString;

            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Est.&nbsp;One&nbsp;Time&nbsp;Costs",
                        amountString,
                        "var(--bs-danger)",
                        "/Icons/Currency.svg",
                        "",
                        "",
                        0));

            return Content(html);

        }

        [HttpGet("api/tile/StageGroup/Decision/EstimatedRunningCosts")]
        public async Task<IActionResult> GetTileStageGroupDecisionEstimatedRunningCosts(string processOwners, string ideaSubmitters,
                                                  string departmentsId, string teamsId, DateTime? startDate, DateTime? endDate,
                                                  bool? isWeekly, bool? isMonthly, bool? isYearly)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var ideas = await Models.Shared.StageGroup.GetIdeasAsync(
                "Decision",
                new Models.Business.Client(clientCore),
                _unitOfWork);


            decimal amount = 0;


            #region IdeaFiltersBehaviour
            if (isWeekly.HasValue && isWeekly.Value)
            {
                var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isMonthly.HasValue && isMonthly.Value)
            {
                var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isYearly.HasValue && isYearly.Value)
            {
                var previousYearStartDate = DateTime.Now.AddYears(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
            }

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                ideas = ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                ideas = ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(departmentsId))
            {
                var departmentsIdList = departmentsId.Split(",");
                ideas = ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(teamsId))
            {
                var teamsIdList = teamsId.Split(",");
                ideas = ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
            }

            #endregion


            foreach (var idea in ideas)
            {
                //idea.UnitOfWork = _unitOfWork;
                amount += Convert.ToDecimal(await idea.GetRunningCostsAsync(_unitOfWork));

            }


            var amountString = "";
            if (amount > 1000)
            {
                amount = amount / 1000;
                amountString = amount.ToString("#,###") + "K";
            }
            else if (amountString == "" && amount==0) {
                amountString = "0.0";
            }
            else
            {
                amountString = amount.ToString("#,###");
            }
                

            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Est.&nbsp;Running&nbsp;Costs",
                        amountString,
                        "var(--bs-warning)",
                        "/Icons/Improvement Solid.svg",
                        "",
                        "",
                        0));

            return Content(html);

        }

        [HttpGet("api/tile/StageGroup/Build/EstimatedRunningCosts")]
        public async Task<IActionResult> GetTileStageGroupBuildEstimatedRunningCosts()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var ideas = await Models.Shared.StageGroup.GetIdeasAsync(
                "Decision",
                new Models.Business.Client(clientCore),
                _unitOfWork);


            decimal amount = 0;
            foreach (var idea in ideas)
                amount += Convert.ToDecimal(idea.RunningCost);


            var amountString = "";
            if (amount > 1000)
            {
                amount = amount / 1000;
                amountString = amount.ToString("#,###") + "K";
            }
            else
                amountString = amount.ToString("#,###");

            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Est.&nbsp;Running&nbsp;Costs",
                        amountString,
                        "var(--bs-warning)",
                        "/Icons/Improvement Solid.svg",
                        "",
                        "",
                        0));

            return Content(html);

        }



        [HttpGet("api/tile/StageGroup/Build/TotalInBuild")]
        public async Task<IActionResult> GetTileStageGroupBuildTotalInBuild(string processOwners, string ideaSubmitters,
                                                  string departmentsId, string teamsId, DateTime? startDate, DateTime? endDate,
                                                  bool? isWeekly, bool? isMonthly, bool? isYearly)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var ideas = await Models.Shared.StageGroup.GetIdeasAsync(
                "Build",
                new Models.Business.Client(clientCore),
                _unitOfWork);

            #region IdeaFiltersBehaviour
            if (isWeekly.HasValue && isWeekly.Value)
            {
                var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isMonthly.HasValue && isMonthly.Value)
            {
                var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isYearly.HasValue && isYearly.Value)
            {
                var previousYearStartDate = DateTime.Now.AddYears(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
            }

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                ideas = ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                ideas = ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(departmentsId))
            {
                var departmentsIdList = departmentsId.Split(",");
                ideas = ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(teamsId))
            {
                var teamsIdList = teamsId.Split(",");
                ideas = ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
            }

            #endregion


            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Total&nbsp;In&nbsp;Build",
                        Convert.ToString(ideas.Count),
                        "var(--bs-warning)",
                        "/Icons/Code.svg",
                        "",
                        "",
                        0));

            return Content(html);
        }

        [HttpGet("api/tile/StageGroup/Deployed/TotalInDeployed")]
        public async Task<IActionResult> GetTileStageGroupDeployedTotalInDeployed(string processOwners, string ideaSubmitters,
                                                  string departmentsId, string teamsId, DateTime? startDate, DateTime? endDate,
                                                  bool? isWeekly, bool? isMonthly, bool? isYearly)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var ideas = await Models.Shared.StageGroup.GetIdeasAsync(
                "Deployed",
                new Models.Business.Client(clientCore),
                _unitOfWork);

            #region IdeaFiltersBehaviour
            if (isWeekly.HasValue && isWeekly.Value)
            {
                var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isMonthly.HasValue && isMonthly.Value)
            {
                var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isYearly.HasValue && isYearly.Value)
            {
                var previousYearStartDate = DateTime.Now.AddYears(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
            }

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                ideas = ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                ideas = ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(departmentsId))
            {
                var departmentsIdList = departmentsId.Split(",");
                ideas = ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(teamsId))
            {
                var teamsIdList = teamsId.Split(",");
                ideas = ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
            }

            #endregion


            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Total&nbsp;In&nbsp;Deployed",
                        Convert.ToString(ideas.Count),
                        "var(--bs-warning)",
                        "/Icons/Code.svg",
                        "",
                        "",
                        0));

            return Content(html);
        }

        [HttpGet("api/tile/StageGroup/Build/TotalAtRisk")]
        public async Task<IActionResult> GetTileStageGroupBuildTotalAtRisk(string processOwners, string ideaSubmitters,
                                                  string departmentsId, string teamsId, DateTime? startDate, DateTime? endDate,
                                                  bool? isWeekly, bool? isMonthly, bool? isYearly)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var ideas = await Models.Shared.StageGroup.GetIdeasAsync(
                "Build",
                new Models.Business.Client(clientCore),
                _unitOfWork);


            int amount = 0;

            #region IdeaFiltersBehaviour
            if (isWeekly.HasValue && isWeekly.Value)
            {
                var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isMonthly.HasValue && isMonthly.Value)
            {
                var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isYearly.HasValue && isYearly.Value)
            {
                var previousYearStartDate = DateTime.Now.AddYears(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
            }

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                ideas = ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                ideas = ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(departmentsId))
            {
                var departmentsIdList = departmentsId.Split(",");
                ideas = ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(teamsId))
            {
                var teamsIdList = teamsId.Split(",");
                ideas = ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
            }

            #endregion


            foreach (var idea in ideas) {
                idea.UnitOfWork = _unitOfWork;
                var obj = await idea.GetIdeaStatusByIdAsync(idea.Id);
                JObject json = JObject.Parse(obj);
                var str = json["value"].ToString();
                if (str == "At Risk")
                {
                    amount++;
                }
            }
            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Total&nbsp;at&nbsp;Risk",
                        Convert.ToString(amount),
                        "var(--bs-yellow)",
                        "/Icons/warning Large.svg",
                        "",
                        "",
                        0));

            return Content(html);
        }

        [HttpGet("api/tile/StageGroup/Build/TotalBenefitAtRisk")]
        public async Task<IActionResult> GetTileStageGroupBuildTotalBenefitAtRisk(string processOwners, string ideaSubmitters,
                                                  string departmentsId, string teamsId, DateTime? startDate, DateTime? endDate,
                                                  bool? isWeekly, bool? isMonthly, bool? isYearly)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var ideas = await Models.Shared.StageGroup.GetIdeasAsync(
                "Build",
                new Models.Business.Client(clientCore),
                _unitOfWork);


            decimal amount = 0;

            #region IdeaFiltersBehaviour
            if (isWeekly.HasValue && isWeekly.Value)
            {
                var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isMonthly.HasValue && isMonthly.Value)
            {
                var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (isYearly.HasValue && isYearly.Value)
            {
                var previousYearStartDate = DateTime.Now.AddYears(-1);
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                ideas = ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
            }

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                ideas = ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                ideas = ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(departmentsId))
            {
                var departmentsIdList = departmentsId.Split(",");
                ideas = ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(teamsId))
            {
                var teamsIdList = teamsId.Split(",");
                ideas = ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
            }

            #endregion


            foreach (var idea in ideas)
            {
                idea.UnitOfWork = _unitOfWork;
                var obj = await idea.GetIdeaStatusByIdAsync(idea.Id);
                JObject json = JObject.Parse(obj);
                var str = json["value"].ToString();
                if (str == "At Risk") {
                    idea.UnitOfWork = _unitOfWork;
                    amount += Convert.ToDecimal(await idea.GetBenefitPerCompanyCurrencyForWorkshop());
                }
            }

            var amountString = "";
            if (amount > 1000)
            {
                amount = amount / 1000;
                amountString = amount.ToString("#,###") + "K";
            }
            else if (amountString == "" && amount==0)
            {
                amountString = "0.0";
            }
            else { amountString = amount.ToString("#,###"); }

            await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(clientCore);

            if (clientCore.Currency != null)
                amountString = clientCore.Currency.Symbol + "&nbsp;" + amountString;

            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Total&nbsp;Benefit&nbsp;at&nbsp;Risk",
                        amountString,
                        "var(--bs-yellow)",
                        "/Icons/warning Large.svg",
                        "",
                        "",
                        0));

            return Content(html);
        }



        //[HttpGet("api/tile/StageGroup/Build/AverageBuildTime")]
        //public async Task<IActionResult> GetTileStageGroupBuildAverageBuildTime()
        //{
        //    var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
        //        new ViewModels.Dashboard
        //            .SummaryButton("Avg&nbsp;Build&nbsp;Time",
        //                "ToDo",
        //                "var(--bs-info)",
        //                "/Icons/Time.svg",
        //                "",
        //                "",
        //                0));

        //    return Content(html);
        //}
    }
}