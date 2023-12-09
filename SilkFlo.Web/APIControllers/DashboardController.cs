using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core.Domain.Business;
using System.Globalization;
using SilkFlo.Web.Models.Business;
using SilkFlo.Web.ViewModels;
using SilkFlo.Data.Persistence;

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

            if (previous == current)
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

                    if (ideaStage == null)
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


                var html = await _viewToString.PartialAsync("Shared/Dashboard/Component/_AutomationProgramPerformance.cshtml",
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

                if (client == null)
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


                var html = await _viewToString.PartialAsync("Shared/Dashboard/Component/_PipelineBenefitsByStage.cshtml",
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

                    var dateStart = ideaStage.DateStart ?? ideaStage.DateStartEstimate;

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
        public async Task<IActionResult> GetIdeaById([FromForm] string Id)
        {
            try
            {
                var obj = await _unitOfWork.BusinessIdeas.GetAsync(Id);
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






        [HttpPost("/api/Dashboard/AddEmployeeIdea")]
        public async Task<IActionResult> AddEmployeeIdea([FromBody] ViewModels.Business.Idea.Modal model, [FromQuery] string UserId, [FromQuery] string ClientId, bool isPractice)
        {
            var feedback = new Feedback();

            // Guard Clause
            if (model == null)
            {
                feedback.DangerMessage("The model is missing");
                return BadRequest(feedback);
            }

            //// Permission Clause
            //if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
            //{
            //    feedback.DangerMessage("Unauthorised");
            //    return BadRequest(feedback);
            //}


            var id = Guid.NewGuid().ToString();

            foreach (var collaborator in model.Collaborators)
            {
                collaborator.IdeaId = id;
            }

            ModelState.Clear();
            TryValidateModel(model);

            if (!ModelState.IsValid)
            {
                feedback = GetFeedback(ModelState, feedback);
                var messageElement = "<ul>";
                foreach (var (_, value) in feedback.Elements)
                {
                    messageElement += $"<li class=\"text-danger\">{value}</li>";
                }

                messageElement += "</ul";

                feedback.Message = messageElement;
                return BadRequest(feedback);
            }


            var message = CanAddCollaborator(model.Collaborators);
            //if (!string.IsNullOrWhiteSpace(message))
            //{
            //    feedback.Message = message;
            //    return BadRequest(feedback);
            //}


            var tenant = await GetClient(ClientId, UserId, isPractice);

            //message = await CanAddProcess(
            //    new Models.Business.Client(tenant),
            //    "Cannot add additional process ideas.");

            //if (!string.IsNullOrWhiteSpace(message))
            //{
            //    feedback.WarningMessage(message);
            //    return BadRequest(feedback);
            //}


            var idea = new Data.Core.Domain.Business.Idea
            {
                Id = id,
                IsDraft = false,
                SubmissionPathId = Data.Core.Enumerators.SubmissionPath.StandardUser.ToString(),
                ClientId = tenant.Id,
                Name = model.Name,
                Summary = model.Summary,
                DepartmentId = model.DepartmentId,
                TeamId = model.TeamId,
                ProcessId = model.ProcessId,
                RuleId = model.RuleId,
                InputId = model.InputId,
                InputDataStructureId = model.InputDataStructureId,
                ProcessStabilityId = model.ProcessStabilityId,
                DocumentationPresentId = model.DocumentationPresentId,
                ProcessOwnerId = model.ProcessOwnerId,
                Rating = model.Rating
            };

            var uniqueMessage = await _unitOfWork.IsUniqueAsync(idea);// UnitOfWork.IsUniqueAsync(idea);
            if (!string.IsNullOrWhiteSpace(uniqueMessage))
            {
                feedback.WarningMessage(uniqueMessage);
                return BadRequest(feedback);
            }

            await _unitOfWork.AddAsync(idea);
            await _unitOfWork.CompleteAsync();

            // Models.Business.IdeaStage.AddWorkFlow(
            //    _unitOfWork, 
            //    idea);
            #region AddWorkflow
            var firstStage = Data.Core.Enumerators.Stage.n00_Idea;
            if (idea.SubmissionPathId == Data.Core.Enumerators.SubmissionPath.COEUser.ToString())
                firstStage = Data.Core.Enumerators.Stage.n01_Assess;

            var date = DateTime.Now;
            var ideaStage = new Data.Core.Domain.Business.IdeaStage
            {
                Idea = idea,
                StageId = firstStage.ToString(),
                DateStartEstimate = date,
                DateStart = date,
                IsInWorkFlow = true,
            };

            await _unitOfWork.AddAsync(ideaStage);
            await _unitOfWork.CompleteAsync();

            if (firstStage == Data.Core.Enumerators.Stage.n00_Idea)
            {
                var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
                {
                    IdeaStageId = ideaStage.Id,
                    StatusId = Data.Core.Enumerators.IdeaStatus.n00_Idea_AwaitingReview.ToString(),
                    Date = date
                };
                await _unitOfWork.AddAsync(ideaStageStatus);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
                {
                    IdeaStageId = ideaStage.Id,
                    StatusId = Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
                    Date = date
                };
                await _unitOfWork.AddAsync(ideaStageStatus);
                await _unitOfWork.CompleteAsync();
            }

            var stages = (await _unitOfWork.SharedStages.FindAsync(x => x.Id != firstStage.ToString())).ToArray();
            if (firstStage == Data.Core.Enumerators.Stage.n01_Assess)
                stages = stages.Where(x => x.Id != Data.Core.Enumerators.Stage.n00_Idea.ToString()).ToArray();

            var now = DateTime.Now;
            foreach (var stage in stages)
            {
                ideaStage = new Data.Core.Domain.Business.IdeaStage
                {
                    Idea = idea,
                    DateStartEstimate = now,
                    Stage = stage
                };

                now = now.AddSeconds(1);
                await _unitOfWork.AddAsync(ideaStage);
            }
            await _unitOfWork.CompleteAsync();
            #endregion

            //await Models.Business.Collaborator.UpdateAsync(
            //    _unitOfWork,
            //    model.Collaborators,
            //    idea.Id,
            //    userId);
            #region Collaborators UpdateAsync
            if (model.Collaborators == null || model.Collaborators.Count <= 0)
            {
                //await _unitOfWork.CompleteAsync();
                return Ok(new { Message = "Employee Idea saved successfully." });
            }

            //  var userId = GetUserId();

            // Get the existing.
            // We need some field content, before deleting them
            var cores = (await _unitOfWork
                    .BusinessCollaborators
                    .FindAsync(x => x.IdeaId == idea.Id))
                    .ToArray();



            // Prepare
            foreach (var modelC in model.Collaborators)
            {
                var core = cores.SingleOrDefault(x => x.UserId == modelC.UserId
                                                      && x.IdeaId == idea.Id);
                if (core == null)
                    continue;

                modelC.IsInvitationConfirmed = core.IsInvitationConfirmed;
                modelC.InvitedById = core.InvitedById;
            }



            // Remove existing
            // This will also remove connected Business.CollaboratorRole rows
            await _unitOfWork.BusinessCollaborators.RemoveRangeAsync(cores);

            // The content of this will be used to populate the Business.UserAuthorisation table.
            var newUserAuthorisation = new List<Data.Core.Domain.Business.UserAuthorisation>();

            foreach (var collaborator in model.Collaborators)
            {
                if (collaborator.CollaboratorRoles == null)
                    continue;

                var collaboratorRoles = new List<Models.Business.CollaboratorRole>();

                var core = collaborator.GetCore();
                core.IdeaId = idea.Id;

                if (string.IsNullOrWhiteSpace(core.InvitedById))
                    core.InvitedById = UserId;

                await _unitOfWork.AddAsync(core);
                foreach (var collaboratorRole in collaborator.CollaboratorRoles)
                {
                    var collaboratorRoleCore = collaboratorRole.GetCore();
                    collaboratorRoleCore.Collaborator = collaborator.GetCore();
                    await _unitOfWork.AddAsync(collaboratorRoleCore);

                    // This will be used to add userAuthorisations
                    if (collaboratorRoles.All(x => x.RoleId != collaboratorRole.RoleId))
                        collaboratorRoles.Add(collaboratorRole);
                }



                // Remove the userAuthorisation from the de-normalized table
                var userAuthorisations =
                    (await _unitOfWork.BusinessUserAuthorisations
                        .FindAsync(x => x.UserId == collaborator.UserId && x.IdeaId == idea.Id)).ToList();

                await _unitOfWork.BusinessUserAuthorisations.RemoveRangeAsync(userAuthorisations);


                // Create Business.UserAuthorisation records
                foreach (var collaboratorRole in collaboratorRoles)
                {
                    var roleIdeaAuthorisation =
                        await _unitOfWork
                            .BusinessRoleIdeaAuthorisations
                            .SingleOrDefaultAsync(x => x.RoleId == collaboratorRole.RoleId);

                    if (newUserAuthorisation
                        .Any(x => x.UserId == collaborator.UserId
                                  && x.IdeaId == idea.Id
                                  && x.IdeaAuthorisationId == roleIdeaAuthorisation.IdeaAuthorisationId))
                        continue;

                    var userAuthorisation = new Data.Core.Domain.Business.UserAuthorisation
                    {
                        UserId = collaborator.UserId,
                        IdeaId = idea.Id,
                        CollaboratorRoleId = collaboratorRole.Id,
                        IdeaAuthorisationId = roleIdeaAuthorisation.IdeaAuthorisationId
                    };

                    // Add the userAuthorisation to the de-normalized table
                    newUserAuthorisation.Add(userAuthorisation);
                }
            }

            // Add the userAuthorisations to the de-normalized table
            await _unitOfWork.AddAsync(newUserAuthorisation);
            #endregion

            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Employee Idea saved successfully." });
        }







        [HttpPost("/api/Dashboard/AddCeoIdea")]
        public async Task<IActionResult> Post([FromBody] Models.Business.Idea model, [FromQuery] string UserId, [FromQuery] string ClientId, bool isPractice)
        {
            //var errors = new List<FieldError>();
            var feedback = new Feedback
            {
                NamePrefix = "Business.Idea."
            };

            try
            {
                // Guard Clause
                if (model == null)
                {
                    feedback.DangerMessage("The model is missing.");
                    return BadRequest(feedback);
                }

                //// Permission Clause
                //if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                //{
                //    feedback.DangerMessage("You are not authorised save an idea.");
                //    return BadRequest(feedback);
                //}

                // Permission Clause
                //if (string.IsNullOrWhiteSpace(model.Id)
                //    && !(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
                //{
                //    feedback.DangerMessage(
                //        "You do not have permission to save a centre of excellence driven automation idea.");
                //    return BadRequest(feedback);
                //}

                // Permission Clause
                //if (!(await AuthorizeAsync(Policy.ReviewNewIdeas)).Succeeded
                //    && !(await AuthorizeAsync(Policy.ReviewAssessedIdeas)).Succeeded
                //    && !(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
                //{
                //    feedback.DangerMessage("You do not have permission to save this idea.");
                //    return BadRequest(feedback);
                //}


                var tenant = await GetClient(ClientId, UserId, isPractice);


                // Can add process permissions Clause
                //var message = await CanAddProcess(
                //    new Models.Business.Client(tenant),
                //    "Cannot add additional process ideas.");

                //if (!string.IsNullOrWhiteSpace(message))
                //{
                //    feedback.WarningMessage(message);
                //    return BadRequest(feedback);
                //}



                //// Can add Collaborators permissions Clause
                //message = CanAddCollaborator(model.Collaborators);
                //if (!string.IsNullOrWhiteSpace(message))
                //{
                //    feedback.WarningMessage(message);
                //    return BadRequest(feedback);
                //}




                model.ClientId = tenant.Id;

                // Prepare the IdeaApplicationVersions for validation, by Idea.Id
                foreach (var ideaApplicationVersion in model.IdeaApplicationVersions)
                    ideaApplicationVersion.IdeaId = model.Id;



                if (!model.IsNew)
                {
                    // Not new? Check if the idea already on the database?
                    var coreOnDataStore = await _unitOfWork.BusinessIdeas.GetAsync(model.Id);

                    // The model was not found on the database.
                    // Log a hack and return view.
                    if (coreOnDataStore == null)
                    {
                        _unitOfWork.Log("The user attempted to save an idea with an incorrect Id.");

                        feedback.DangerMessage("The id is not valid");
                        return BadRequest(feedback);
                    }
                }


                // Validate
                if (string.IsNullOrWhiteSpace(model.Name))
                    feedback.Add("Name", "The name of your idea is missing");
                else if (model.Name.Length > 100)
                    feedback.Add("Name", "Name must be between 1 and 100 in length");


                if (string.IsNullOrWhiteSpace(model.Summary))
                    feedback.Add("Summary", "Summary is missing");
                else if (model.Summary.Length > 750)
                    feedback.Add("Summary", "Name must be between 1 and 750 in length");


                var uniqueMessage = await _unitOfWork.IsUniqueAsync(model.GetCore());
                if (!string.IsNullOrWhiteSpace(uniqueMessage))
                    feedback.Add("Name", "Your idea name is not unique");


                //if (!model.IsDraft
                //    && model.SubmissionPathId != Data.Core.Enumerators.SubmissionPath.StandardUser.ToString())
                //    feedback = Validate(model, feedback);



                //// Is NOT valid?
                //if (!feedback.IsValid)
                //    return BadRequest(feedback);



                var core = model.GetCore();

                if (!core.IsDraft)
                {
                    // Add stage if the idea is not draft
                    if (!core.IsNew)
                        await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(core);

                    if (!core.IdeaStages.Any())
                    {
                        //First save ideaa
                        await _unitOfWork.AddAsync(core);

                        //then it's stages and status
                        await Models.Business.IdeaStage.AddWorkFlow(_unitOfWork, core);


                        await _unitOfWork.CompleteAsync();

                        //continue
                        // Add Applications
                        await SaveApplicationsListAsync(
                            model.IdeaApplicationVersions,
                            model.Id);

                       // var userId = GetUserId();

                        await Models.Business.Collaborator.UpdateAsync(
                            _unitOfWork,
                            model.Collaborators,
                            model.Id,
                            UserId);


                        await _unitOfWork.CompleteAsync();
                    }
                }
                else
                {


                    await _unitOfWork.AddAsync(core);

                    await _unitOfWork.CompleteAsync();

                    // Add Applications
                    await SaveApplicationsListAsync(
                        model.IdeaApplicationVersions,
                        model.Id);

                    //var userId = GetUserId();

                    await Models.Business.Collaborator.UpdateAsync(
                        _unitOfWork,
                        model.Collaborators,
                        model.Id,
                        UserId);


                    await _unitOfWork.CompleteAsync();

                }

                return Ok(new { Message = "CEO Idea saved successfully." });
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var message = Security.Settings.GetEnvironment() == Security.Environment.Production
                    ? "Error saving ideas. Error logged."
                    : ex.Message;

                feedback.DangerMessage(message);
                return BadRequest(feedback);
            }
        }






        private Feedback Validate(
      Models.Business.Idea model,
      Feedback feedback)
        {
            if (string.IsNullOrWhiteSpace(model.PainPointComment))
            {
                feedback.Add(
                    "PainPointComment",
                    "Pain Point Comment is missing");
            }

            if (string.IsNullOrWhiteSpace(model.NegativeImpactComment))
            {
                feedback.Add(
                    "NegativeImpactComment",
                    "Negative Impact Comment is missing");
            }


            if (string.IsNullOrWhiteSpace(model.DepartmentId))
            {
                feedback.Add(
                    "DepartmentId",
                    "Business Unit is missing");
            }


            if (string.IsNullOrWhiteSpace(model.RuleId))
            {
                feedback.Add(
                    "RuleId",
                    "Rule is missing");
            }

            if (string.IsNullOrWhiteSpace(model.InputId))
            {
                feedback.Add(
                    "InputId",
                    "Input Data Structure is missing");
            }

            if (string.IsNullOrWhiteSpace(model.InputDataStructureId))
            {
                feedback.Add(
                    "InputDataStructureId",
                    "Input is missing");
            }

            if (string.IsNullOrWhiteSpace(model.ProcessStabilityId))
            {
                feedback.Add(
                    "ProcessStabilityId",
                    "Process Stability is missing");
            }

            if (string.IsNullOrWhiteSpace(model.DocumentationPresentId))
            {
                feedback.Add(
                    "DocumentationPresentId",
                    "Documentation Present is missing");
            }


            if (string.IsNullOrWhiteSpace(model.AutomationGoalId))
            {
                feedback.Add(
                    "AutomationGoalId",
                    "Automation Goal is missing");
            }




            if (string.IsNullOrWhiteSpace(model.ApplicationStabilityId))
            {
                feedback.Add(
                    "ApplicationStabilityId",
                    "Application Stability is missing");
            }


            if (model.AverageWorkingDay == null)
            {
                feedback.Add(
                    "AverageWorkingDay",
                    "Average Working Day is missing");
            }


            if (model.WorkingHour == null)
            {
                feedback.Add(
                    "WorkingHour",
                    "Working Hour is missing");
            }


            if (string.IsNullOrWhiteSpace(model.TaskFrequencyId))
            {
                feedback.Add(
                    "TaskFrequencyId",
                    "Task Frequency is missing");
            }


            if (model.ActivityVolumeAverage == null || model.ActivityVolumeAverage == 0)
            {
                feedback.Add(
                    "ActivityVolumeAverage",
                    "Activity Volume Average is missing");
            }


            if (model.EmployeeCount == null || model.EmployeeCount == 0)
            {
                feedback.Add(
                    "EmployeeCount",
                    "Employee Count is missing");
            }


            if (model.AverageProcessingTime == null || model.AverageProcessingTime == 0)
            {
                feedback.Add(
                    "AverageProcessingTime",
                    "Average Processing Time is missing");
            }


            if (string.IsNullOrWhiteSpace(model.ProcessPeakId))
            {
                feedback.Add(
                    "ProcessPeakId",
                    "Process Peak is missing");
            }


            if (string.IsNullOrWhiteSpace(model.AverageNumberOfStepId))
            {
                feedback.Add(
                    "AverageNumberOfStepId",
                    "Average Number of Step is missing");
            }


            if (string.IsNullOrWhiteSpace(model.DataInputPercentOfStructuredId))
            {
                feedback.Add(
                    "DataInputPercentOfStructuredId",
                    "Data Input Percent of Structured is missing");
            }


            if (string.IsNullOrWhiteSpace(model.NumberOfWaysToCompleteProcessId))
            {
                feedback.Add(
                    "NumberOfWaysToCompleteProcessId",
                    "Number of Ways to Complete Process is missing");
            }


            if (string.IsNullOrWhiteSpace(model.DecisionCountId))
            {
                feedback.Add(
                    "DecisionCountId",
                    "Decision Count is missing");
            }


            if (string.IsNullOrWhiteSpace(model.DecisionDifficultyId))
            {
                feedback.Add(
                    "DecisionDifficultyId",
                    "How difficult are the decisions that you must take to complete the process? is missing");
            }


            if (model.IdeaApplicationVersions.Count == 0)
            {
                feedback.Add(
                    "IdeaApplicationVersion",
                    "No applications are selected.");
            }

            if (string.IsNullOrWhiteSpace(model.ProcessOwnerId))
            {
                feedback.Add(
                    "ProcessOwnerId",
                    "Process Owner is missing");
            }
            return feedback;
        }




        private async Task SaveApplicationsListAsync(
     IReadOnlyCollection<Models.Business.IdeaApplicationVersion> ideaApplicationVersions,
     string id)
        {
            // Guard Clause
            if (ideaApplicationVersions == null)
                return;

            // Guard Clause
            if (string.IsNullOrWhiteSpace(id))
                return;

            if (!(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
                return;

            // Remove old
            var oldItems =
                (await _unitOfWork.BusinessIdeaApplicationVersions.FindAsync(x => x.IdeaId == id)).ToArray();

            await _unitOfWork.BusinessIdeaApplicationVersions.RemoveRangeAsync(oldItems);



            // Add new
            foreach (var ideaApplication in from application in ideaApplicationVersions
                                            where application.IsSelected
                                            select new Data.Core.Domain.Business.IdeaApplicationVersion
                                            {
                                                IdeaId = id,
                                                VersionId = application.VersionId,
                                                LanguageId = application.LanguageId,
                                                IsThinClient = application.IsThinClient,
                                            })
            {
                await _unitOfWork.AddAsync(ideaApplication);
            }
        }











        public async Task<Data.Core.Domain.Business.Client> GetClient(string ClientId, string UserId, bool isPractice, bool checkSubscription = false)
        {

            if (string.IsNullOrWhiteSpace(ClientId))
            {
                // Cookies are missing; therefore, we must use the user.ClientId

                var user = await _unitOfWork.Users.GetAsync(UserId);

                // Guard Clause
                if (user == null)
                    return null;

                var client = await GetSingleOrDefaultValidatedAsync(user, ClientId);

                if (isPractice && client is { IsPractice: false })
                    client = await _unitOfWork.BusinessClients.SingleOrDefaultAsync(x => x.Id == client.PracticeId);

                return client;
            }
            else
            {
                // Guard Clause
                if (string.IsNullOrWhiteSpace(UserId))
                    return null;

                var user = await _unitOfWork.Users.GetAsync(UserId);

                // Guard Clause
                if (user == null)
                    return null;

                var client = await GetSingleOrDefaultValidatedAsync(user, ClientId, checkSubscription);


                //problem of cookies

                if (isPractice && client is { IsPractice: false })
                    client = await _unitOfWork.BusinessClients.SingleOrDefaultAsync(x => x.Id == client.PracticeId);

                return client;
            }
        }
    }
}