using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Models.Business
{
    public partial class Client
    {
        public Data.Core.IUnitOfWork UnitOfWork { get; set; }

        public async Task<SVGChartTools.DataSet.PieChart> GetPieCharPipelineBenefitByDifficultyDataSetAsync(
            int year)
        {
            var ideas =
                (await UnitOfWork.BusinessIdeas
                    .FindAsync(x => (x.CreatedDate ?? DateTime.MinValue).Year == year
                                    && x.ClientId == Id
                                    && !x.IsDraft))
                .ToArray();

            return await GetPieCharPipelineBenefitByDifficultyDataSetAsync(
                Idea.Create(ideas), 
                "Business.Idea.Summary",
                "/api/Business/Idea/FilterSummary");
        }

		public async Task<SVGChartTools.DataSet.PieChart> GetPieCharPipelineBenefitByDifficultyDataSetByDateRangeAsync(
			DateTime? startDate, DateTime? endDate)
		{
            SilkFlo.Data.Core.Domain.Business.Idea[] ideas;

            if (startDate.HasValue && endDate.HasValue)
            {
                ideas = (await UnitOfWork.BusinessIdeas.FindAsync(x => (x.CreatedDate?.Date ?? DateTime.MinValue.Date) >= startDate?.Date
                        && (x.CreatedDate?.Date ?? DateTime.MinValue.Date) <= endDate?.Date
                        && x.ClientId == Id
                        && !x.IsDraft))
                .ToArray();
            }
            else
            {

                ideas = (await UnitOfWork.BusinessIdeas.FindAsync(x => (x.CreatedDate ?? DateTime.MinValue).Year == (DateTime.Now.Year)
                            && x.ClientId == Id
                            && !x.IsDraft))
                    .ToArray();
            }

			return await GetPieCharPipelineBenefitByDifficultyDataSetAsync(
				Idea.Create(ideas),
				"Business.Idea.Summary",
				"/api/Business/Idea/FilterSummary");
		}

		public async Task<SVGChartTools.DataSet.PieChart> GetPieCharPipelineBenefitByDifficultyDataSetAsync(
            Idea[] ideas,
            string filterTargetId,
            string targetUrl)
        {
            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            var easyCount = 0;
            var moderateCount = 0;
            var difficultCount = 0;
            decimal estimatedBenefitTotal = 0;

            var easyCode = "";
            var moderateCode = "";
            var difficultCode = "";

            decimal estimatedBenefitEasyTotal = 0;
            decimal estimatedBenefitModerateTotal = 0;
            decimal estimatedBenefitDifficultTotal = 0;


            foreach (var model in ideas)
            {
                if (model.LastIdeaStage?.StageId != Data.Core.Enumerators.Stage.n07_Deployed.ToString())
                {
                    model.UnitOfWork = UnitOfWork;

                    await UnitOfWork.BusinessIdeaApplicationVersions.GetForIdeaAsync(model.GetCore());

                    await model.GetEaseOfImplementationAsync();
                    model.GetBenefitPerEmployee_Currency();
                    var estimatedBenefit = await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                    estimatedBenefitTotal += estimatedBenefit;

                    switch (model.EaseOfImplementationWord)
                    {
                        case "Easy":
                            easyCount++;
                            easyCode += $"{model.Id},";
                            estimatedBenefitEasyTotal += estimatedBenefit;
                            break;
                        case "Difficult":
                            difficultCount++;
                            difficultCode += $"{model.Id},";
                            estimatedBenefitDifficultTotal += estimatedBenefit;
                            break;
                        default:
                            moderateCount++;
                            moderateCode += $"{model.Id},";
                            estimatedBenefitModerateTotal += estimatedBenefit;
                            break;
                    }

                }
            }

            if (!string.IsNullOrWhiteSpace(easyCode))
                easyCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{easyCode}','{filterTargetId}','{targetUrl}')";

            if (!string.IsNullOrWhiteSpace(difficultCode))
                difficultCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{difficultCode}','{filterTargetId}','{targetUrl}')";

            if (!string.IsNullOrWhiteSpace(moderateCode))
                moderateCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{moderateCode}','{filterTargetId}','{targetUrl}')";



            var easyPercent = !ideas.Any() ? 0 : (int)(easyCount/ (decimal)ideas.Count() * 100);
            var moderatePercent = !ideas.Any() ? 0 : (int)(moderateCount / (decimal)ideas.Count() * 100);
            var difficultPercent = !ideas.Any() ? 0 : (int)(difficultCount / (decimal)ideas.Count() * 100);

            await UnitOfWork.ShopCurrencies.GetCurrencyForAsync(GetCore());

            var pieChartKeys = new List<SVGChartTools.DataSet.PieChartSlice>
            {
                new() { Title = "Easy", Value = easyPercent, ValueString = easyPercent + "%", Background = "var(--bs-green)", OnClick = easyCode, HtmlTooltip = Currency?.Symbol + " " + estimatedBenefitEasyTotal.ToString("#,###") + "<br/>" + easyCount + " Easy"},
                new() { Title = "Moderate", Value = moderatePercent, ValueString = moderatePercent + "%", Background = "var(--bs-warning-lighter)", OnClick = moderateCode, HtmlTooltip = Currency?.Symbol + " " + estimatedBenefitModerateTotal.ToString("#,###") + "<br/>" + moderateCount + " Moderate" },
                new() { Title = "Difficult", Value = difficultPercent, ValueString = difficultPercent + "%", Background = "var(--bs-danger)", OnClick = difficultCode, HtmlTooltip = Currency?.Symbol + " " + estimatedBenefitDifficultTotal.ToString("#,###") + "<br/>" + difficultCount + " Difficult" }
            };

            var text = "No Ideas";

            if (ideas.Any())
                text = estimatedBenefitTotal.ToString(Currency?.Symbol + " #,###");

            var data = new SVGChartTools.DataSet.PieChart(
                pieChartKeys,
                text);

            return data;
        }

        public async Task<SVGChartTools.DataSet.PieChart> GetPieChartPipelineIdeaQualityDataSetAsync(
            Idea[] ideas)

        {
            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            var poorCount = 0;
            var averageCount = 0;
            var goodCount = 0;

            foreach (var model in ideas)
            {
                model.UnitOfWork = UnitOfWork;
                await model.GetIdeaScoreModalAsync();

                var word = model.IdeaScoreWord;

                switch (word)
                {
                    case "Poor":
                        poorCount++;
                        break;
                    case "Average":
                        averageCount++;
                        break;
                    default:
                        goodCount++;
                        break;
                }

            }


            var poorPercent = !ideas.Any() ? 0 : (int)(poorCount / (decimal)ideas.Count() * 100);
            var averagePercent = !ideas.Any() ? 0 : (int)(averageCount / (decimal)ideas.Count() * 100);
            var goodPercent = !ideas.Any() ? 0 : (int)(goodCount / (decimal)ideas.Count() * 100);


            var pieChartKeys = new List<SVGChartTools.DataSet.PieChartSlice>
            {
                new() { Title = "Poor", Value = poorPercent, ValueString = poorPercent + "%", Background = "var(--bs-danger)", },
                new() { Title = "Average", Value = averagePercent, ValueString = averagePercent + "%", Background = "var(--bs-warning-lighter)", },
                new() { Title = "Good", Value = goodPercent, ValueString = goodPercent + "%", Background = "var(--bs-green)", }
            };


            var data = new SVGChartTools.DataSet.PieChart(
                pieChartKeys);

            return data;
        }
    }
}