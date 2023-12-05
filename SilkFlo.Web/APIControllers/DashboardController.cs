using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core.Domain.Business;
using System.Globalization;
using SilkFlo.Web.Models.Business;

namespace SilkFlo.Web.Controllers
{
    public partial class DashboardController
    {
        [HttpGet("api/Dashboard/GetApplicationMode")]
        public async Task<IActionResult> GetApplicationMode()
        {
            var environment = Security.Settings.GetEnvironment();
            var html = environment switch
            {
                Security.Environment.Development => await _viewToString.PartialAsync(
                    "shared/_ApplicationMode.cshtml",
                    "Development Build: " + Settings.Build),
                Security.Environment.Test => await _viewToString.PartialAsync(
                    "shared/_ApplicationMode.cshtml",
                    "Test Build: " + Settings.Build),
                _ => ""
            };

            return Content(html);
        }

        [HttpGet("api/Dashboard/GetPracticeMode")]
        public async Task<IActionResult> GetPracticeMode()
        {
            var userId = GetUserId();

            if (userId == null)
                return Content("");

            var environment = Security.Settings.GetEnvironment();
            var html = environment switch
            {
                Security.Environment.Development => await _viewToString.PartialAsync(
                    "shared/_PracticeMode.cshtml",
                    "Development Build: " + Settings.Build),
                Security.Environment.Test => await _viewToString.PartialAsync(
                    "shared/_PracticeMode.cshtml",
                    "Test Build: " + Settings.Build),
                _ => ""
            };

            return Content(html);
        }

        [HttpGet("/api/Dashboard/GetTotalIdeas")]
        public async Task<IActionResult> GetTotalIdeas(DateTime? startDate, DateTime? endDate, bool? isWeekly, bool? isMonthly, bool? isYearly, string processOwners, string ideaSubmitters, string departmentsId, string teamsId)
        {
            try
            {
               // Check Authorization
                const string unauthorizedMessage = "<h1 class=\"text-danger\">Error: Unauthorised</h1>";

                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return await PageApiAsync(unauthorizedMessage);



                var clientCore = await GetClientAsync();

                if (clientCore == null)
                    return  Content("<h1 class=\"text-danger\">Unauthorised</h1>");

                var client = new Models.Business.Client(clientCore);

                await _unitOfWork.BusinessIdeas.GetForClientAsync(client.GetCore());
                await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(client.GetCore().Ideas);


                var monthCount = 0;
                var lastMonthCount = 0;

                var date = DateTime.Now;
                var month = date.Month;
                var year = date.Year;

                date = date.AddMonths(-1);
                var monthLast = date.Month;
                var yearLast = date.Year;




                var ideas = new List<Models.Business.Idea>();

                var ideaList = client.Ideas.Where(x => !x.IsDraft && x.IdeaStages.Any());
                
                if(isWeekly.HasValue && isWeekly.Value)
                {
					var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
					ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date);
				}
                else if(isMonthly.HasValue && isMonthly.Value)
                {
                    var previousMonthStartDate = DateTime.Now.AddMonths(-1);
					ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date);
				}
                else if(isYearly.HasValue && isYearly.Value)
                {
                    var previousYearStartDate = DateTime.Now.AddYears(-1);
					ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date);
				}
                else if(startDate.HasValue && endDate.HasValue)
                {
                    ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date);
                }

                if (!String.IsNullOrWhiteSpace(ideaSubmitters))
                {
                    var isList = ideaSubmitters.Split(",");
                    ideaList = ideaList.Where(x => isList.Contains(x.ProcessOwnerId));
                }

                if (!String.IsNullOrWhiteSpace(processOwners))
                {
                    var poList = processOwners.Split(",");
                    ideaList = ideaList.Where(x => poList.Contains(x.ProcessOwnerId));
                }

                if (!String.IsNullOrWhiteSpace(departmentsId))
                {
					var departmentsIdList = departmentsId.Split(",");
					ideaList = ideaList.Where(x => departmentsIdList.Contains(x.DepartmentId));
				}

				if (!String.IsNullOrWhiteSpace(teamsId))
				{
					var teamsIdList = teamsId.Split(",");
					ideaList = ideaList.Where(x => teamsIdList.Contains(x.TeamId));
				}


				foreach (var idea in ideaList)
                {
                    var ideaStage = idea.LastIdeaStage;

                    if (ideaStage == null)
                        continue;

                    await _unitOfWork.SharedStages.GetStageForAsync(ideaStage.GetCore());

                    if (ideaStage.Stage.StageGroupId == Data.Core.Enumerators.StageGroup.n03_Build.ToString() 
                        || ideaStage.Stage.StageGroupId == Data.Core.Enumerators.StageGroup.n04_Deployed.ToString())
                        continue;

                    ideas.Add(idea);

                    var createdDate = idea.CreatedDate?? DateTime.MinValue;
                    if (createdDate.Month == month && createdDate.Year == year)
                        monthCount++;

                    else if (createdDate.Month == monthLast && createdDate.Year == yearLast)
                        lastMonthCount++;
                }



                var total = ideas.Count();
                var totalChargeIn = GetChangeIn(lastMonthCount, monthCount);


                var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                                                         new ViewModels.Dashboard
                                                                        .SummaryButton("Total Ideas",
                                                                                        total.ToString(),
                                                                                        "var(--bs-primary)",
                                                                                        "/Icons/Idea Solid.svg",
                                                                                        "",
                                                                                        "SilkFlo.SideBar.OnClick('Explore/Ideas')",
                                                                                        totalChargeIn,
                                                                                        "Total-Ideas"));
                return Content(html);

            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("/api/Dashboard/GetTotalInBuild")]
        public async Task<IActionResult> GetTotalInBuild(DateTime? startDate, DateTime? endDate, bool? isWeekly, bool? isMonthly, bool? isYearly, string processOwners, string ideaSubmitters, string departmentsId, string teamsId)
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("");

                var clientCore = await GetClientAsync();

                if (clientCore == null)
                    return Content("<h1 class=\"text-danger\">Unauthorised</h1>");

                var client = new Models.Business.Client(clientCore);


                await _unitOfWork.BusinessIdeas.GetForClientAsync(client.GetCore());
                await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(client.GetCore().Ideas);


                var monthCount = 0;
                var lastMonthCount = 0;

                var date = DateTime.Now;
                var month = date.Month;
                var year = date.Year;

                date = date.AddMonths(-1);
                var monthLast = date.Month;
                var yearLast = date.Year;

                var ideas = new List<Models.Business.Idea>();

				var ideaList = client.Ideas;

				if (isWeekly.HasValue && isWeekly.Value)
				{
					var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
					ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
				}
				else if (isMonthly.HasValue && isMonthly.Value)
				{
					var previousMonthStartDate = DateTime.Now.AddMonths(-1);
					ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
				}
				else if (isYearly.HasValue && isYearly.Value)
				{
					var previousYearStartDate = DateTime.Now.AddYears(-1);
					ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
				}
				else if (startDate.HasValue && endDate.HasValue)
				{
					ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
				}

                if (!String.IsNullOrWhiteSpace(ideaSubmitters))
                {
                    var isList = ideaSubmitters.Split(",");
                    ideaList = ideaList.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
                }

                if (!String.IsNullOrWhiteSpace(processOwners))
                {
                    var poList = processOwners.Split(",");
                    ideaList = ideaList.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
                }

				if (!String.IsNullOrWhiteSpace(departmentsId))
				{
					var departmentsIdList = departmentsId.Split(",");
					ideaList = ideaList.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
				}

				if (!String.IsNullOrWhiteSpace(teamsId))
				{
					var teamsIdList = teamsId.Split(",");
					ideaList = ideaList.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
				}


				foreach (var idea in ideaList)
                {
                    if (idea.IsDraft)
                        continue;


                    if (!idea.IdeaStages.Any())
                        continue;


                    var ideaStage = idea.LastIdeaStage;

                    if (ideaStage == null)
                        continue;

                    var ideaStageCore = ideaStage.GetCore();

                    await _unitOfWork.SharedStages.GetStageForAsync(ideaStageCore);
                    await _unitOfWork.SharedStageGroups.GetStageGroupForAsync(ideaStageCore.Stage);

                    if (ideaStage.Stage.StageGroupId != Data.Core.Enumerators.StageGroup.n03_Build.ToString())
                        continue;

                    ideas.Add(idea);


                    if (ideaStage.CreatedDate == null)
                        continue;

                    var createdDate = (DateTime)ideaStage.CreatedDate;


                    if (createdDate.Month == month && createdDate.Year == year)
                        monthCount++;

                    else if (createdDate.Month == monthLast && createdDate.Year == yearLast)
                        lastMonthCount++;
                }


                var total = ideas.Count;
                var totalChargeIn = GetChangeIn(lastMonthCount, monthCount);


                var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                                                           new ViewModels.Dashboard
                                                                          .SummaryButton("Total in Build",
                                                                                         total.ToString(),
                                                                                         "var(--bs-warning)",
                                                                                         "/Icons/Improvement Solid.svg",
                                                                                         "",
                                                                                         "SilkFlo.SideBar.OnClick('Workshop/Build')",
                                                                                         totalChargeIn,
                                                                                         "Total-In-Build"));
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpGet("/api/Dashboard/GetTotalDeployed")]
        public async Task<IActionResult> GetTotalDeployed(DateTime? startDate, DateTime? endDate, bool? isWeekly, bool? isMonthly, bool? isYearly, string processOwners, string ideaSubmitters, string departmentsId, string teamsId)
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("");


                var clientCore = await GetClientAsync();

                if (clientCore == null)
                    return Content("<h1 class=\"text-danger\">Unauthorised</h1>");

                var client = new Models.Business.Client(clientCore);


                await _unitOfWork.BusinessIdeas.GetForClientAsync(client.GetCore());
                await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(client.GetCore().Ideas);


                int monthCount = 0;
                int lastMonthCount = 0;

                var date = DateTime.Now;
                int month = date.Month;
                int year = date.Year;

                date = date.AddMonths(-1);
                int monthLast = date.Month;
                int yearLast = date.Year;

                var ideas = new List<Models.Business.Idea>();

				var ideaList = client.Ideas;

				if (isWeekly.HasValue && isWeekly.Value)
				{
					var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
					ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
				}
				else if (isMonthly.HasValue && isMonthly.Value)
				{
					var previousMonthStartDate = DateTime.Now.AddMonths(-1);
					ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
				}
				else if (isYearly.HasValue && isYearly.Value)
				{
					var previousYearStartDate = DateTime.Now.AddYears(-1);
					ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
				}
				else if (startDate.HasValue && endDate.HasValue)
				{
					ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
				}

                if (!String.IsNullOrWhiteSpace(ideaSubmitters))
                {
                    var isList = ideaSubmitters.Split(",");
                    ideaList = ideaList.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
                }

                if (!String.IsNullOrWhiteSpace(processOwners))
                {
                    var poList = processOwners.Split(",");
                    ideaList = ideaList.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
                }

				if (!String.IsNullOrWhiteSpace(departmentsId))
				{
					var departmentsIdList = departmentsId.Split(",");
					ideaList = ideaList.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
				}

				if (!String.IsNullOrWhiteSpace(teamsId))
				{
					var teamsIdList = teamsId.Split(",");
					ideaList = ideaList.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
				}


				foreach (var idea in ideaList)
                {
                    if (idea.IsDraft)
                        continue;


                    if (!idea.IdeaStages.Any())
                        continue;


                    var ideaStage = idea.LastIdeaStage;

                    if (ideaStage == null)
                        continue;

                    var ideaStageCore = ideaStage.GetCore();

                    await _unitOfWork.SharedStages.GetStageForAsync(ideaStageCore);
                    await _unitOfWork.SharedStageGroups.GetStageGroupForAsync(ideaStageCore.Stage);

                    if (ideaStage.Stage.StageGroupId != Data.Core.Enumerators.StageGroup.n04_Deployed.ToString())
                        continue;


                    ideas.Add(idea);

                    if (ideaStage.CreatedDate == null)
                        continue;


                    var createdDate = (DateTime)ideaStage.CreatedDate;
                    if (createdDate.Month == month && createdDate.Year == year)
                    {
                        monthCount++;
                    }
                    else if (createdDate.Month == monthLast && createdDate.Year == yearLast)
                    {
                        lastMonthCount++;
                    }
                }


                var total = ideas.Count;
                var totalChargeIn = GetChangeIn(lastMonthCount, monthCount);

                var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                                                         new ViewModels.Dashboard
                                                                        .SummaryButton("Total Deployed",
                                                                                        total.ToString(),
                                                                                        "var(--bs-success)",
                                                                                        "/Icons/RobotHead.svg",
                                                                                        "",
                                                                                        "SilkFlo.SideBar.OnClick('Explore/Automations')",
                                                                                        totalChargeIn,
                                                                                        "Total-Deployed"));
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        internal static decimal GetChangeIn(int previous, int current)
        {
            decimal chargeIn;

            if(previous == current)
                return 0;
            
            if (current > previous)
            {
                if (previous == 0)
                    return 100;

                chargeIn = previous / (decimal)current * 100;
            }
            else
            {
                if (current == 0)
                    return -100;

                chargeIn = current / (decimal)previous * -100;
            }

            return chargeIn;
        }



        [HttpGet("/api/Dashboard/GetTotalDeploymentBenefits")]
        public async Task<IActionResult> GetTotalDeploymentBenefits(DateTime? startDate, DateTime? endDate, bool? isWeekly, bool? isMonthly, bool? isYearly, string processOwners, string ideaSubmitters, string departmentsId, string teamsId)
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("");

                var currency = await GetTenantCurrencyAsync();


                var client = await GetClientAsync();

                var ideas = (await _unitOfWork.BusinessIdeas.FindAsync(x => x.ClientId == client.Id && !x.IsDraft)).ToArray();
                await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(ideas);
                ideas = ideas.Where(x =>
                    x.IdeaStages.Last().StageId == Data.Core.Enumerators.Stage.n07_Deployed.ToString()).ToArray();

                decimal totalValue = 0;
                decimal totalHoursValue = 0;

				if (isWeekly.HasValue && isWeekly.Value)
				{
					var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
					ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToArray();
				}
				else if (isMonthly.HasValue && isMonthly.Value)
				{
					var previousMonthStartDate = DateTime.Now.AddMonths(-1);
					ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToArray();
				}
				else if (isYearly.HasValue && isYearly.Value)
				{
					var previousYearStartDate = DateTime.Now.AddYears(-1);
					ideas = ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToArray();
				}
				else if (startDate.HasValue && endDate.HasValue)
				{
					ideas = ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToArray();
				}

                if (!String.IsNullOrWhiteSpace(ideaSubmitters))
                {
                    var isList = ideaSubmitters.Split(",");
                    ideas = ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToArray();
                }

                if (!String.IsNullOrWhiteSpace(processOwners))
                {
                    var poList = processOwners.Split(",");
                    ideas = ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToArray();
                }

				if (!String.IsNullOrWhiteSpace(departmentsId))
				{
					var departmentsIdList = departmentsId.Split(",");
					ideas = ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToArray();
				}

				if (!String.IsNullOrWhiteSpace(teamsId))
				{
					var teamsIdList = teamsId.Split(",");
					ideas = ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToArray();
				}


				foreach (var idea in ideas)
                {
                    var model = new Models.Business.Idea(idea)
                    {
                        UnitOfWork = _unitOfWork
                    };

                    if (model.LastIdeaStage?.StageId == Data.Core.Enumerators.Stage.n07_Deployed.ToString())
                    {
                        model.GetBenefitPerEmployee_Currency();
                        totalValue += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);

                        model.GetBenefitPerEmployee_Hours();
                        totalHoursValue += await model.GetEstimateAsync(model.BenefitPerCompanyHoursValue);
                    }
                }


                //string total = $"{tenant.Currency.Symbol} {0.ToString("#,###")}";
                var total = currency.Symbol + " " + totalValue.ToString("#,###.00");
                const int totalChargeIn = 0;

                var summaryButton = new ViewModels.Dashboard
                    .SummaryButton("Deployed Benefits",
                        total,
                        "var(--bs-red)",
                        "/Icons/RobotHead.svg",
                        "",
                        "",
                        totalChargeIn,
                        "Deployed-Benefits")
                    {
                        Title2 = totalHoursValue.ToString("#,###") + " hrs"
                    };

                var html = await _viewToString.PartialAsync(
                    "Shared/Dashboard/_SummaryButton.cshtml",
                    summaryButton);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("/api/Dashboard/GetAutomationProgramPerformance")]
        [HttpGet("/api/Dashboard/GetAutomationProgramPerformance/Year/{year}")]
        public async Task<IActionResult> GetAutomationProgramPerformance(int? year, DateTime? startDate, DateTime? endDate, bool? isWeekly, bool? isMonthly, bool? isYearly, string processOwners, string ideaSubmitters, string departmentsId, string teamsId)
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return NegativeFeedback();

                var client = await GetClientAsync();
                var currency = await GetTenantCurrencyAsync();

                if (client == null)
                    return NegativeFeedback();


                year ??= DateTime.Now.Year;

                var cores = (await _unitOfWork.BusinessIdeas
                    .FindAsync(x => x.ClientId == client.Id && !x.IsDraft))
                    .ToArray();

                await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(cores);

                var models = Models.Business.Idea.Create(cores);

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

				var discoverIdeas = new List<Models.Business.Idea>();
                var buildIdeas = new List<Models.Business.Idea>();
                var deployedIdeas = new List<Models.Business.Idea>();

                // Assign model to the correct list
                foreach (var model in models)
                {
                    if (!model.IdeaStages.Any())
                        continue;


                    var ideaStage = model.LastIdeaStage;

                    if(ideaStage == null)
                        continue;

                    
                    if ((ideaStage.StageId == Data.Core.Enumerators.Stage.n00_Idea.ToString()
                         || ideaStage.StageId == Data.Core.Enumerators.Stage.n01_Assess.ToString()
                         || ideaStage.StageId == Data.Core.Enumerators.Stage.n02_Qualify.ToString()))
                    {
                        discoverIdeas.Add(model);
                    }
                    else if ((ideaStage.StageId == Data.Core.Enumerators.Stage.n03_Analysis.ToString()
                              || ideaStage.StageId == Data.Core.Enumerators.Stage.n04_SolutionDesign.ToString()
                              || ideaStage.StageId == Data.Core.Enumerators.Stage.n05_Development.ToString()
                              || ideaStage.StageId == Data.Core.Enumerators.Stage.n06_Testing.ToString()))
                    {
                        buildIdeas.Add(model);
                    }
                    else if ((ideaStage.StageId == Data.Core.Enumerators.Stage.n07_Deployed.ToString()))
                    {
                        deployedIdeas.Add(model);
                    }
                }


                if (!discoverIdeas.Any()
                    && !buildIdeas.Any()
                    && !deployedIdeas.Any())
                {
                    return Content("<label>No ideas with a start date in stages:</label><ul><li>Access</li><li>Qualify</li><li>Analysis</li><li>Solution Design</li><li>Development</li><li>Testing</li><li>Deployed</li></ul>");
                }


                var data = new SVGChartTools.DataSet.Chart
                {
                    // X Axis
                    XAxisLabels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                };

                // Data set 1 - Qualified
                var dataSet = await ViewModels.Chart.DataSet.IdeaEstimate.Get(
                    discoverIdeas,
                    (int)year,
                    currency,
                    "fill: var(--bs-red-lighter); stroke: var(--bs-red-lighter);",
                    "Discover",
                    new List<Data.Core.Enumerators.StageGroup>
                    {
                        Data.Core.Enumerators.StageGroup.n00_Review,
                        Data.Core.Enumerators.StageGroup.n01_Assess,
                        Data.Core.Enumerators.StageGroup.n02_Decision
                    },
                    _unitOfWork,
                    "Business.Idea.Summary",
                    "/api/Business/Idea/FilterSummary",
                    true);

                data.DataSets.Add(dataSet);


                // Data set 2 - Build
                dataSet = await ViewModels.Chart.DataSet.IdeaEstimate.Get(
                    buildIdeas,
                    (int)year,
                    currency,
                    "fill: var(--bs-warning-lighter); stroke: var(--bs-warning-lighter);",
                    "Build",
                    new List<Data.Core.Enumerators.StageGroup>
                    {
                        Data.Core.Enumerators.StageGroup.n03_Build
                    },
                    _unitOfWork,
                    "Business.Idea.Summary",
                    "/api/Business/Idea/FilterSummary",
                    true);

                data.DataSets.Add(dataSet);


                // Data set 3 - Deployed
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
                    "/api/Business/Idea/FilterSummary",
                    true);

                data.DataSets.Add(dataSet);



                //var data = ViewModels.Chart.Bar.TestData();

                var barChart = new ViewModels.Chart.Bar(data)
                {
                    YDivisionsCount = 8,
                    YLabel = "Estimated Benefit " + currency.Symbol
                };


                var html = await  _viewToString.PartialAsync("Shared/Dashboard/Component/_AutomationProgramPerformance.cshtml",
                                                             new ViewModels.Dashboard.Component.AutomationProgramPerformance
                                                             {
                                                                 BarChart = barChart,
                                                                 Year = (int)year,
                                                             });
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        


        [HttpGet("/api/Dashboard/GetPipelineBenefitsByStage")]
        public async Task<IActionResult> GetPipelineBenefitsByStage(DateTime? startDate, DateTime? endDate, bool? isWeekly, bool? isMonthly, bool? isYearly)
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("Unauthorised");

                var client = await GetClientAsync();

                if(client == null)
                    return Content("Unauthorised");

                var model = new Models.Business.Client(client);
                model.UnitOfWork = _unitOfWork;


                SVGChartTools.DataSet.PieChart data = new SVGChartTools.DataSet.PieChart(new List<SVGChartTools.DataSet.PieChartSlice>());

				if (isWeekly.HasValue && isWeekly.Value)
				{
					var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                    data = await model.GetPieCharPipelineBenefitByDifficultyDataSetByDateRangeAsync(previousWeekStartDate, DateTime.Now.Date);
				}
				else if (isMonthly.HasValue && isMonthly.Value)
				{
					var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                    data = await model.GetPieCharPipelineBenefitByDifficultyDataSetByDateRangeAsync(previousMonthStartDate, DateTime.Now.Date);
				}
				else if (isYearly.HasValue && isYearly.Value)
				{
					var previousYearStartDate = DateTime.Now.AddYears(-1);
                    data = await model.GetPieCharPipelineBenefitByDifficultyDataSetByDateRangeAsync(previousYearStartDate, DateTime.Now.Date);
				}
                else
                {
                    data = await model.GetPieCharPipelineBenefitByDifficultyDataSetByDateRangeAsync(null, null);
                }

                //var data = ViewModels.Chart.Doughnut.TestData();
                var chart = new ViewModels.Chart.Doughnut(data);
                

                var html = await  _viewToString.PartialAsync("Shared/Dashboard/Component/_PipelineBenefitsByStage.cshtml",
                                                         new ViewModels.Dashboard.Component.PipelineBenefitsByStage
                                                         {
                                                             Year = 1,
                                                             DoughnutChart = chart,
                                                             PieChartKeys = data.PieChartKeys,
                                                         });
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("/api/Dashboard/Tile/Hero")]
        public async Task<IActionResult> GetHero()
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("");

                if (!await IsNewUser())
                    return Content("");


                var html = await this.PartialAsync("Dashboard/_Hero.cshtml");


                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("/api/Dashboard/Tile/GetMyIdeas")]
        public async Task<IActionResult> GetTileMyIdeas()
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("");

                var userId = GetUserId();

                var user = await _unitOfWork.Users.GetAsync(userId);
                if (await IsNewUser(user))
                    return Content("");


                var monthCount = 0;
                var lastMonthCount = 0;

                var date = DateTime.Now;
                var month = date.Month;
                var year = date.Year;

                date = date.AddMonths(-1);
                var monthLast = date.Month;
                var yearLast = date.Year;


                foreach (var createdDate in from idea 
                             in user.Ideas 
                             where idea.CreatedDate != null 
                             select (DateTime)idea.CreatedDate)
                {
                    if (createdDate.Month == month && createdDate.Year == year)
                        monthCount++;

                    else if (createdDate.Month == monthLast && createdDate.Year == yearLast)
                        lastMonthCount++;
                }

				#region Change by Umair 
				var tenant = await GetClientAsync();


				var filter = new ViewModels.Business.Idea.FilterCriteria
				{
					UserRelationship = ViewModels.Business.Idea.UserRelationship.MyIdeas
				};

				var ideas = await Models.Business
					.Idea
					.GetForCardsAsync(_unitOfWork,
						GetUserId(),
						tenant,
						filter,
						this,
						true);
				
				var total = ideas.Count();
				#endregion


				//var total = user.Ideas.Count();
                var totalChargeIn = GetChangeIn(lastMonthCount, monthCount);


                var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                                                         new ViewModels.Dashboard
                                                                        .SummaryButton("My Ideas",
                                                                                        total.ToString(),
                                                                                        "var(--bs-primary)",
                                                                                        "/Icons/Idea Solid.svg",
                                                                                        "",
                                                                                        "SilkFlo.SideBar.OnClick('Explore/People')",
                                                                                        totalChargeIn,
                                                                                        "My-Ideas"));


                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("/api/Dashboard/Tile/GetMyTotalInBuild")]
        public async Task<IActionResult> GetMyTotalInBuild()
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("");

                var userId = GetUserId();

                var user = await _unitOfWork.Users.GetAsync(userId);
                if (await IsNewUser(user))
                    return Content("");

                await _unitOfWork.BusinessIdeas.GetForProcessOwnerAsync(user);
                await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(user.Ideas);


                var monthCount = 0;
                var lastMonthCount = 0;

                var date = DateTime.Now;
                var month = date.Month;
                var year = date.Year;

                date = date.AddMonths(-1);
                var monthLast = date.Month;
                var yearLast = date.Year;

                var ideas = new List<Models.Business.Idea>();

                var models = Models.Business.Idea.Create(user.Ideas);

                foreach (var idea in models)
                {
                    var ideaStage = idea.LastIdeaStage;
                    if (ideaStage == null)
                        continue;

                    await _unitOfWork.SharedStages.GetStageForAsync(ideaStage.GetCore());

                    if (ideaStage.Stage.StageGroupId != Data.Core.Enumerators.StageGroup.n03_Build.ToString())
                        continue;

                    ideas.Add(idea);


                    var createdDate = ideaStage.DateStart ?? ideaStage.DateStartEstimate;
                    if (createdDate.Month == month
                        && createdDate.Year == year)
                        monthCount++;

                    else if (createdDate.Month == monthLast 
                             && createdDate.Year == yearLast)
                        lastMonthCount++;
                }


                var total = ideas.Count;
                var totalChargeIn = GetChangeIn(lastMonthCount, monthCount);


                var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                                                           new ViewModels.Dashboard
                                                                          .SummaryButton("My Ideas in Build",
                                                                                         total.ToString(),
                                                                                         "var(--bs-warning)",
                                                                                         "/Icons/Improvement Solid.svg",
                                                                                         "",
                                                                                         "",
                                                                                         totalChargeIn,
                                                                                         "My-Total-In-Build"));
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("/api/Dashboard/Tile/GetMyTotalDeployed")]
        public async Task<IActionResult> GetMyTotalDeployed()
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("");

                var userId = GetUserId();

                var user = await _unitOfWork.Users.GetAsync(userId);
                if (await IsNewUser(user))
                    return Content("");


                await _unitOfWork.BusinessIdeas.GetForProcessOwnerAsync(user);
                await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(user.Ideas);

                var monthCount = 0;
                var lastMonthCount = 0;

                var date = DateTime.Now;
                var month = date.Month;
                var year = date.Year;

                date = date.AddMonths(-1);
                var monthLast = date.Month;
                var yearLast = date.Year;

                var ideas = new List<Models.Business.Idea>();

                var models = Models.Business.Idea.Create(user.Ideas);

                foreach (var idea in models)
                {
                    if (!idea.IdeaStages.Any())
                        continue;

                    var ideaStage = idea.LastIdeaStage;
                    await _unitOfWork.SharedStages.GetStageForAsync(ideaStage.GetCore());

                    if (ideaStage.Stage.StageGroupId != Data.Core.Enumerators.StageGroup.n04_Deployed.ToString())
                        continue;


                    ideas.Add(idea);

                    if (ideaStage.CreatedDate == null) continue;

                    var dateStart = ideaStage.DateStart?? ideaStage.DateStartEstimate;

                    if (dateStart.Month == month && dateStart.Year == year)
                        monthCount++;

                    else if (dateStart.Month == monthLast && dateStart.Year == yearLast)
                        lastMonthCount++;
                }


                var total = ideas.Count;
                var totalChargeIn = GetChangeIn(lastMonthCount, monthCount);

                var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                                                         new ViewModels.Dashboard
                                                                        .SummaryButton("My Ideas Deployed",
                                                                                        total.ToString(),
                                                                                        "var(--bs-success)",
                                                                                        "/Icons/RobotHead.svg",
                                                                                        "",
                                                                                        "",
                                                                                        totalChargeIn,
                                                                                        "My-Total-Deployed"));
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("/api/Dashboard/Tile/GetMyCollaborations")]
        public async Task<IActionResult> GetCollaborations()
        {
            try
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Content("");

                var userId = GetUserId();

                var user = await _unitOfWork.Users.GetAsync(userId);
                if (await IsNewUser(user))
                    return Content("");


                await _unitOfWork.BusinessCollaborators.GetForUserAsync(user);
                await _unitOfWork.BusinessIdeas.GetIdeaForAsync(user.Collaborators);


                var monthCount = 0;
                var lastMonthCount = 0;

                var date = DateTime.Now;
                var month = date.Month;
                var year = date.Year;

                date = date.AddMonths(-1);
                var monthLast = date.Month;
                var yearLast = date.Year;

                foreach (var createdDate in from collaborator in user.Collaborators
                    where collaborator.CreatedDate != null && !collaborator.Idea.IsDraft
                    select (DateTime)collaborator.CreatedDate)
                {
                    if (createdDate.Month == month && createdDate.Year == year)
                        monthCount++;

                    else if (createdDate.Month == monthLast && createdDate.Year == yearLast)
                        lastMonthCount++;
                }


                var total = user.Collaborators.Count(x => !x.Idea.IsDraft);
                var totalChargeIn = GetChangeIn(lastMonthCount, monthCount);

                var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                                                         new ViewModels.Dashboard
                                                                        .SummaryButton("My Collaborations",
                                                                                        total.ToString(),
                                                                                        "var(--bs-info)",
                                                                                        "/Icons/Users Solid.svg",
                                                                                        "",
                                                                                        "",
                                                                                        totalChargeIn,
                                                                                        "My-Collaborations"));
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        private async Task<bool> IsNewUser(Data.Core.Domain.User user = null)
        {
            var userId = GetUserId();
            user ??= await _unitOfWork.Users.GetAsync(userId);

            await _unitOfWork.BusinessIdeas.GetForProcessOwnerAsync(user);


            if (!user.Ideas.Any())
            {
                await _unitOfWork.BusinessCollaborators.GetForUserAsync(user);

                user.Collaborators = (await _unitOfWork.BusinessCollaborators
                                                       .FindAsync(x => x.UserId == userId
                                                                    && x.IsInvitationConfirmed))
                                                       .ToList();

                if (!user.Collaborators.Any())
                    return true;
            }

            return false;
        }







        /////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////    Api implementations    /////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////



        [HttpGet("/api/Dashboard/GetAllIdeas")]
        public async Task<IActionResult> GetAllIdeas(DateTime? startDate, DateTime? endDate, bool? isWeekly, bool? isMonthly, bool? isYearly, string processOwners, string ideaSubmitters, string departmentsId, string teamsId)
        {
            try
            {
                // Check Authorization
                //const string unauthorizedMessage = "Error: Unauthorised";

                //if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                //    return await PageApiAsync(unauthorizedMessage);
                var userids = "ea65f7fc-ad04-4fe6-ac6c-eb57d84e4217";

                var user = await _unitOfWork.Users.GetAsync(userids);




                var clientCore = await GetClientAsyncApi();

                if (clientCore == null)
                    return Content("Unauthorised");

                var client = new Models.Business.Client(clientCore);

                await _unitOfWork.BusinessIdeas.GetForClientAsync(client.GetCore());
                await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(client.GetCore().Ideas);

                var monthCount = 0;
                var lastMonthCount = 0;

                var date = DateTime.Now;
                var month = date.Month;
                var year = date.Year;

                date = date.AddMonths(-1);
                var monthLast = date.Month;
                var yearLast = date.Year;

                var ideas = new List<Models.Business.Idea>();

                var ideaList = client.Ideas.Where(x => !x.IsDraft && x.IdeaStages.Any());


                if (isWeekly.HasValue && isWeekly.Value)
                {
                    var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
                    ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date);
                }
                else if (isMonthly.HasValue && isMonthly.Value)
                {
                    var previousMonthStartDate = DateTime.Now.AddMonths(-1);
                    ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date);
                }
                else if (isYearly.HasValue && isYearly.Value)
                {
                    var previousYearStartDate = DateTime.Now.AddYears(-1);
                    ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date);
                }
                else if (startDate.HasValue && endDate.HasValue)
                {
                    ideaList = ideaList.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date);
                }

                if (!String.IsNullOrWhiteSpace(ideaSubmitters))
                {
                    var isList = ideaSubmitters.Split(",");
                    ideaList = ideaList.Where(x => isList.Contains(x.ProcessOwnerId));
                }

                if (!String.IsNullOrWhiteSpace(processOwners))
                {
                    var poList = processOwners.Split(",");
                    ideaList = ideaList.Where(x => poList.Contains(x.ProcessOwnerId));
                }

                if (!String.IsNullOrWhiteSpace(departmentsId))
                {
                    var departmentsIdList = departmentsId.Split(",");
                    ideaList = ideaList.Where(x => departmentsIdList.Contains(x.DepartmentId));
                }

                if (!String.IsNullOrWhiteSpace(teamsId))
                {
                    var teamsIdList = teamsId.Split(",");
                    ideaList = ideaList.Where(x => teamsIdList.Contains(x.TeamId));
                }


                foreach (var idea in ideaList)
                {
                    var ideaStage = idea.LastIdeaStage;

                    if (ideaStage == null)
                        continue;

                    await _unitOfWork.SharedStages.GetStageForAsync(ideaStage.GetCore());

                    if (ideaStage.Stage.StageGroupId == Data.Core.Enumerators.StageGroup.n03_Build.ToString()
                        || ideaStage.Stage.StageGroupId == Data.Core.Enumerators.StageGroup.n04_Deployed.ToString())
                        continue;

                    ideas.Add(idea);

                    var createdDate = idea.CreatedDate ?? DateTime.MinValue;
                    if (createdDate.Month == month && createdDate.Year == year)
                        monthCount++;

                    else if (createdDate.Month == monthLast && createdDate.Year == yearLast)
                        lastMonthCount++;
                }

                var total = ideas.Count();
                var totalChangeIn = GetChangeIn(lastMonthCount, monthCount);

                var result = new
                {
                    TotalIdeas = total.ToString(),
                    TotalChangeIn = totalChangeIn,
                    Ideas= ideas,
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }





        [HttpGet("/api/Dashboard/GetIdeasByUserId")]
        public async Task<IActionResult> GetIdeasByUserId([FromForm] string UserId)
        {
            try
            {
                //if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                //    return Content("");

             //   var userId = "ea65f7fc-ad04-4fe6-ac6c-eb57d84e4217";

               // var user = await _unitOfWork.Users.GetAsync(userId);
              //  var userId = GetUserId();

                var user = await _unitOfWork.Users.GetAsync(UserId);
                if (await IsNewUser(user))
                    return Content("");


                var monthCount = 0;
                var lastMonthCount = 0;

                var date = DateTime.Now;
                var month = date.Month;
                var year = date.Year;

                date = date.AddMonths(-1);
                var monthLast = date.Month;
                var yearLast = date.Year;

                foreach (var createdDate in from idea
                             in user.Ideas
                                            where idea.CreatedDate != null
                                            select (DateTime)idea.CreatedDate)
                {
                    if (createdDate.Month == month && createdDate.Year == year)
                        monthCount++;

                    else if (createdDate.Month == monthLast && createdDate.Year == yearLast)
                        lastMonthCount++;
                }
                var ideas = user.Ideas.ToList();


                var total = ideas.Count;
                var totalChangeIn = GetChangeIn(lastMonthCount, monthCount);
                var result = new
                {
                    TotalIdeas = total.ToString(),
                    TotalChangeIn = totalChangeIn,
                    Ideas = ideas,
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("/api/Dashboard/GetIdeaById")]
        public async Task<IActionResult> GetIdeaById([FromForm]string Id)
        {
            try
            {
              var obj= await _unitOfWork.BusinessIdeas.GetAsync(Id);
                if (obj != null)
                {
                    return Json(obj);

                }
                else
                {
                    return Json(new { });
                }

            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



    }
}
