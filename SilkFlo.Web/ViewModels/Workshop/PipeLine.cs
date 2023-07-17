using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SilkFlo.Web.ViewModels.Dashboard;

namespace SilkFlo.Web.ViewModels.Workshop
{
    public class PipeLine
    {
        private readonly Data.Core.Domain.Business.Client _client;
        private readonly Data.Core.IUnitOfWork _unitOfWork;
        private readonly IAuthorizationService _authorization;
        private readonly System.Security.Claims.ClaimsPrincipal _user;

        public List<int> Years { get; set; }

        private PipeLine(Models.Shared.StageGroup stageGroup,
                        Data.Core.Domain.Business.Client client,
                        Data.Core.IUnitOfWork unitOfWork,
                        IAuthorizationService authorization,
                        System.Security.Claims.ClaimsPrincipal user,
                        Business.Idea.Summary ideaSummary,
                        List<string> tileUrls)
        {
            StageGroup = stageGroup;

            _client = client ?? throw new ArgumentNullException(nameof(client));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            TileUrls = tileUrls ?? throw new ArgumentNullException(nameof(tileUrls));

            IdeaSummary = ideaSummary ?? throw new ArgumentNullException(nameof(ideaSummary));

            if (stageGroup?.Name == "Review")
                IdeaSummary.IdeaScore = true;
            else if (stageGroup?.Name == "Deployed")
            {
                IdeaSummary.IdeaScore = true;
                IdeaSummary.BenefitHours = true;
            }
            else { IdeaSummary.Goal = true; }
        }



        public static async Task<PipeLine> BuildAsync(
            Data.Core.IUnitOfWork unitOfWork,
            IAuthorizationService authorization,
            Models.Shared.StageGroup stageGroup,
            System.Security.Claims.ClaimsPrincipal user,
            Data.Core.Domain.Business.Client client,
            List<string> tileUrls)
        {
            var ideaSummary = await Business.Idea.Summary.BuildAsync(authorization, user);

            ideaSummary.MarginTop = stageGroup == null ? "330px" : "320px";

            var pipeline =  new PipeLine(
                stageGroup,
                client,
                unitOfWork,
                authorization,
                user,
                ideaSummary,
                tileUrls);

            if (stageGroup == null)
                return pipeline;


            switch (stageGroup.Name)
            {
                case "Review":
                    pipeline.DoughnutChartSubTitle = "Idea Quality";
                    pipeline.DoughnutChartSummary = "The chart below shows Ideas sorted by their Idea Score";
                    pipeline.BubbleChartTitle = "Idea fitness vs Primed score";
                    pipeline.BubbleChartSummary = "Ideas ranked by Fitness and Primed scores.";
                    break;
                case "Assess":
                    pipeline.DoughnutChartSubTitle = "Ease of implementation";
                    pipeline.DoughnutChartSummary = "Highlight automations in the pie chart below";
                    pipeline.BubbleChartTitle = "Automation Matrix";
                    pipeline.BubbleChartSummary = "Automations ranked by Potential and Ease of Implementation";
                    break;
                case "Decision":
                    pipeline.DoughnutChartSubTitle = "Ease of implementation";
                    pipeline.DoughnutChartSummary = "Highlight automations in the pie chart below";
                    pipeline.BubbleChartTitle = "Automation Matrix";
                    pipeline.BubbleChartSummary = "Automations ranked by Potential and Ease of Implementation";
                    break;
                //case "Build":
                //    pipeline.DoughnutChartSubTitle = "Ease of implementation";
                //    pipeline.DoughnutChartSummary = "Highlight automations in the pie chart below";
                //    pipeline.BubbleChartTitle = "Automation Build Pipeline";
                //    pipeline.BubbleChartSummary = "This chart shows benefits in pipeline based on Build and Deployed dates";
                //    break;
            }

            return pipeline;
        }



        public async Task GetIdeas(Data.Core.Domain.Shop.Product product)
        {
            Ideas = (await Models.Shared.StageGroup.GetIdeasAsync(
                StageGroup?.Name,
                new Models.Business.Client(_client),
                _unitOfWork)).ToArray();


            Years = new List<int>();

            foreach (var model in Ideas)
            {
                var ideaStage = model.LastIdeaStage;
                if (ideaStage == null)
                    continue;

                var date = ideaStage.DateStart ?? ideaStage.DateStartEstimate;
                var year = date.Year;
                if (Years.All(x => x != year))
                    Years.Add(year);

                model.GetTotalTimeNeededToPerformWorkWithoutAutomation();

            }


            IdeaSummary.Ideas.AddRange(Ideas);

            foreach (var idea in IdeaSummary.Ideas)
            {
                idea.UnitOfWork = _unitOfWork;
                await idea.GetSummaryView_MetaDataAsync(product);
            }
        }


        public Models.Business.Idea[] Ideas { get; set; }

        public string FilterTargetId { get; set; }
        public string TargetUrl { get; set; }

        public Models.Shared.StageGroup StageGroup { get; }

        public async Task<Chart.Doughnut> GetDoughnutChartAsync()
        {
            if (Ideas == null)
                throw new NullReferenceException("The idea collection is missing.");


            if (FilterTargetId == null)
                throw new NullReferenceException("The FilterTargetId is missing.");


            if (TargetUrl == null)
                throw new NullReferenceException("The TargetUrl is missing.");


            //var data = Chart.Doughnut.TestData();
            var model = new Models.Business.Client(_client)
            {
                UnitOfWork = _unitOfWork
            };

            var name = StageGroup?.Name.ToLower();
            if (name == "review")
            {
                var data = await model.GetPieChartPipelineIdeaQualityDataSetAsync(Ideas);
                PieChartKeys = data.PieChartKeys;
                return new Chart.Doughnut(data);
            }
            else
            {
                var data = await model.GetPieCharPipelineBenefitByDifficultyDataSetAsync(
                    Ideas, 
                    FilterTargetId,
                    TargetUrl);

                PieChartKeys = data.PieChartKeys;
                return new Chart.Doughnut(data);
            }
        }

        public Business.Idea.Summary IdeaSummary { get; }

        public async Task<Chart.AutomationMatrix> GetAutomationMatrixAsync()
        {
            var data = new List<SVGChartTools.DataSet.XYZ>();
            var name = StageGroup?.Name.ToLower();
            foreach (var idea in Ideas)
            {

                if (name == "review")
                {
                    await idea.GetIdeaScoreModalAsync();
                    var xyz = new SVGChartTools.DataSet.XYZ(
                        idea.PrimedScoreValue,
                        idea.FitnessScoreValue,
                        idea.IdeaScoreValue)
                    {
                        CSSStyle = $"fill: {idea.IdeaScoreColour}; opacity: 0.6; stroke: {idea.IdeaScoreColour};",
                        CSSClass = "bubbleGrow",
                        OnClick = $"SilkFlo.ViewModels.Workshop.SelectIdea('{idea.Id}')",
                        HTMLTooltip = $"<b>{idea.Name}</b><br/>" + 
                                      $"Idea&nbsp;Score:&nbsp;{idea.IdeaScoreValue:##}&nbsp;%<br/>" +
                                      "<spanPrimary>Click to move idea to top of list</spanPrimary>",
                    };

                    data.Add(xyz);
                }
                else
                {
                    idea.GetBenefitPerEmployee_Hours();
                    var benefit = await idea.GetEstimateAsync(idea.BenefitPerCompanyCurrencyValue);
                    var xyz = new SVGChartTools.DataSet.XYZ(
                        idea.EaseOfImplementationValue,
                        idea.AutomationPotentialValue ?? 0,
                         await idea.GetEstimateAsync(idea.BenefitPerCompanyHoursRelativePercent))
                    {
                        CSSStyle = $"fill: {idea.EaseOfImplementationColour}; opacity: 0.6; stroke: {idea.EaseOfImplementationColour};",
                        OnClick = $"SilkFlo.ViewModels.Workshop.SelectIdea('{idea.Id}')",
                        HTMLTooltip =  $"<b>{idea.Name}</b><br/>" + 
                                       $"Benefit:&nbsp;{_client.Currency?.Symbol}&nbsp{benefit:#,###.00}<br/>" + 
                                       $"Automation&nbsp;Potential:&nbsp;{(idea.AutomationPotentialValue??0):##}&nbsp;%<br/>" +
                                       $"Ease&nbsp;of&nbsp;Implementation:&nbsp;{(idea.EaseOfImplementationValue):##}&nbsp;%<br/>" +
                                       "<spanPrimary>Click to move idea to top of list</spanPrimary>",
                    };

                    data.Add(xyz);
                }
            }



            var axisFont = new SVGChartTools.Font("Poppins Thin", 12F, 100)
            {
                CSSStyle = "stroke: var(--bs-gray);"
            };
            var labelFont = new SVGChartTools.Font("Poppins Thin", 12F, 100)
            {
                CSSStyle = "stroke: var(--bs-gray-light);"
            };


            var paper = new SVGChartTools.Paper23
            {
                Height = 305
            };

            var automationMatrix = new Chart.AutomationMatrix
            (axisFont,
                axisFont,
                labelFont,
                labelFont,
                data,
                StageGroup?.Id != "n00_Review",
                paper)
            {
                BackgroundColour = "var(--bs-gray-even-lightest2)",
                XDivisionsCount = 10,
                YDivisionsCount = 5,
            };


            if (name == "review")
            {
                automationMatrix.XAxisTitle = "Primed Score %";
                automationMatrix.YAxisTitle = "Fitness Score %";
            }


            return automationMatrix;
        }

        public List<SVGChartTools.DataSet.PieChartSlice> PieChartKeys { get; set; }

        public string DoughnutChartSubTitle { get; set; }
        public string DoughnutChartSummary { get; set; }

        public string BubbleChartTitle { get; set; }
        public string BubbleChartSummary { get; set; }

        public Models.Shop.Currency Currency { get; set; }

        public List<string> TileUrls { get; set; } = new();

        public async Task<PipeLineDeployed> GetPipeLineDeployed(Data.Core.Domain.Shop.Product product)
        {
            return await PipeLineDeployed.GetPipeLineDeployedAsync(
                _unitOfWork,
                _authorization,
                _user,
                new Models.Business.Client(_client),
                product);
        }
    }
}
