using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.ViewModels.Chart.DataSet
{
    public class IdeaEstimate
    {
        public static async Task<SVGChartTools.DataSet.Data[]> Get(List<Models.Business.Idea> ideas,
            int yearSelected,
            Data.Core.Domain.Shop.Currency currency,
            string cssStyle,
            string name,
            List<Data.Core.Enumerators.StageGroup> stageGroups,
            Data.Core.IUnitOfWork unitOfWork,
            string filterTargetId,
            string targetUrl,
            bool lastStageOnly,
            string callbackFunction = "")
        {
            if (!ideas.Any())
                return System.Array.Empty<SVGChartTools.DataSet.Data>();


            decimal jan = 0;
            decimal feb = 0;
            decimal mar = 0;
            decimal apr = 0;
            decimal may = 0;
            decimal jun = 0;
            decimal jul = 0;
            decimal aug = 0;
            decimal sep = 0;
            decimal oct = 0;
            decimal nov = 0;
            decimal dec = 0;

            decimal janCount = 0;
            decimal febCount = 0;
            decimal marCount = 0;
            decimal aprCount = 0;
            decimal mayCount = 0;
            decimal junCount = 0;
            decimal julCount = 0;
            decimal augCount = 0;
            decimal sepCount = 0;
            decimal octCount = 0;
            decimal novCount = 0;
            decimal decCount = 0;

            var janCode = "";
            var febCode = "";
            var marCode = "";
            var aprCode = "";
            var mayCode = "";
            var junCode = "";
            var julCode = "";
            var augCode = "";
            var sepCode = "";
            var octCode = "";
            var novCode = "";
            var decCode = "";



            foreach (var model in ideas)
            {
                model.UnitOfWork = unitOfWork;
                model.GetBenefitPerEmployee_Currency();
                var core = model.GetCore();


                var ideaStages = new List<Data.Core.Domain.Business.IdeaStage>();
                if (lastStageOnly 
                    && model.LastIdeaStage != null)
                    ideaStages.Add(model.LastIdeaStage.GetCore());
                else
                    ideaStages = core.IdeaStages;

                await unitOfWork.SharedStages.GetStageForAsync(ideaStages);


                for (var month = 1; month <= 12; month++)
                {
                    foreach (var ideaStage in ideaStages)
                    {
                        if (!ideaStage.IsInWorkFlow)
                            continue;

                        if (stageGroups.All(x => x.ToString() != ideaStage.Stage.StageGroupId))
                            continue;


                        var dateAStart = new System.DateTime(yearSelected, month, 1);
                        var dateAEnd = dateAStart.AddMonths(1);
                        dateAEnd = dateAEnd.AddDays(-1);

                        var dateBStart = ideaStage.DateStart ?? ideaStage.DateStartEstimate;
                        var dateBEnd = ideaStage.DateEnd ?? ideaStage.DateEndEstimate ?? dateBStart;

                        // This is here because deployed ideaStage have DateEndEstimate = DateStartEstimate 
                        if (ideaStage.Stage.StageGroupId == Data.Core.Enumerators.StageGroup.n04_Deployed.ToString())
                            dateBEnd = dateBStart;

                        if (!Services.DateTimeTools.IsOverLap(
                                dateAStart,
                                dateAEnd,
                                dateBStart,
                                dateBEnd))
                            continue;

                        // We do not want to include this stage if:
                        // * this stage is NOT deployed stage
                        // * AND the last stage is deployed stage
                        // * AND the deployed stage is in the month

                        var lastStage = model.LastIdeaStage;

                        if (ideaStage.StageId != "n07_Deployed"
                            && lastStage is {StageId: "n07_Deployed"})
                        {
                            var dateBDeployedStart = lastStage.DateStart ?? lastStage.DateStartEstimate;
                            var dateDeployedEnd = lastStage.DateEnd ?? lastStage.DateEndEstimate ?? dateBStart;
                            if (Services.DateTimeTools.IsOverLap(
                                    dateAStart,
                                    dateAEnd,
                                    dateBDeployedStart,
                                    dateDeployedEnd))
                                continue;
                        }

                        switch (month)
                        {
                            case 1:
                                if (janCode.IndexOf(model.Id, StringComparison.Ordinal) > -1)
                                    break;

                                jan += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                                janCount++;
                                janCode += $"{model.Id},";
                                break;
                            case 2:
                                if (febCode.IndexOf(model.Id, StringComparison.Ordinal) > -1)
                                    break;

                                feb += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                                febCount++;
                                febCode += $"{model.Id},";
                                break;
                            case 3:
                                if (marCode.IndexOf(model.Id, StringComparison.Ordinal) > -1)
                                    break;

                                mar += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                                marCount++;
                                marCode += $"{model.Id},";
                                break;
                            case 4:
                                if (aprCode.IndexOf(model.Id, StringComparison.Ordinal) > -1)
                                    break;

                                apr += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                                aprCount++;
                                aprCode += $"{model.Id},";
                                break;
                            case 5:
                                if (mayCode.IndexOf(model.Id, StringComparison.Ordinal) > -1)
                                    break;

                                may += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                                mayCount++;
                                mayCode += $"{model.Id},";
                                break;
                            case 6:
                                if (junCode.IndexOf(model.Id, StringComparison.Ordinal) > -1)
                                    break;

                                jun += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                                junCount++;
                                junCode += $"{model.Id},";
                                break;
                            case 7:
                                if (julCode.IndexOf(model.Id, StringComparison.Ordinal) > -1)
                                    break;

                                jul += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                                julCount++;
                                julCode += $"{model.Id},";
                                break;
                            case 8:
                                if (augCode.IndexOf(model.Id, StringComparison.Ordinal) > -1)
                                    break;

                                aug += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                                augCount++;
                                augCode += $"{model.Id},";
                                break;
                            case 9:
                                if (sepCode.IndexOf(model.Id, StringComparison.Ordinal) > -1)
                                    break;

                                sep += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                                sepCount++;
                                sepCode += $"{model.Id},";
                                break;
                            case 10:
                                if (octCode.IndexOf(model.Id, StringComparison.Ordinal) > -1)
                                    break;

                                oct += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                                octCount++;
                                octCode += $"{model.Id},";
                                break;
                            case 11:
                                if (octCode.IndexOf(model.Id, StringComparison.Ordinal) > -1)
                                    break;

                                nov += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                                novCount++;
                                novCode += $"{model.Id},";
                                break;
                            case 12:
                                if (decCode.IndexOf(model.Id, StringComparison.Ordinal) > -1)
                                    break;

                                dec += await model.GetEstimateAsync(model.BenefitPerCompanyCurrencyValue);
                                decCount++;
                                decCode += $"{model.Id},";
                                break;
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(callbackFunction))
                callbackFunction += ", " + callbackFunction;

            if (!string.IsNullOrWhiteSpace(janCode)) janCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{janCode[..^1]}','{filterTargetId}','{targetUrl}'{callbackFunction})";
            if (!string.IsNullOrWhiteSpace(febCode)) febCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{febCode[..^1]}','{filterTargetId}','{targetUrl}'{callbackFunction})";
            if (!string.IsNullOrWhiteSpace(marCode)) marCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{marCode[..^1]}','{filterTargetId}','{targetUrl}'{callbackFunction})";
            if (!string.IsNullOrWhiteSpace(aprCode)) aprCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{aprCode[..^1]}','{filterTargetId}','{targetUrl}'{callbackFunction})";
            if (!string.IsNullOrWhiteSpace(mayCode)) mayCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{mayCode[..^1]}','{filterTargetId}','{targetUrl}'{callbackFunction})";
            if (!string.IsNullOrWhiteSpace(junCode)) junCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{junCode[..^1]}','{filterTargetId}','{targetUrl}'{callbackFunction})";
            if (!string.IsNullOrWhiteSpace(julCode)) julCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{julCode[..^1]}','{filterTargetId}','{targetUrl}'{callbackFunction})";
            if (!string.IsNullOrWhiteSpace(augCode)) augCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{augCode[..^1]}','{filterTargetId}','{targetUrl}'{callbackFunction})";
            if (!string.IsNullOrWhiteSpace(sepCode)) sepCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{sepCode[..^1]}','{filterTargetId}','{targetUrl}'{callbackFunction})";
            if (!string.IsNullOrWhiteSpace(octCode)) octCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{octCode[..^1]}','{filterTargetId}','{targetUrl}'{callbackFunction})";
            if (!string.IsNullOrWhiteSpace(novCode)) novCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{novCode[..^1]}','{filterTargetId}','{targetUrl}'{callbackFunction})";
            if (!string.IsNullOrWhiteSpace(decCode)) decCode = $"SilkFlo.Models2.Business.Idea.FilterSummary('{decCode[..^1]}','{filterTargetId}','{targetUrl}'{callbackFunction})";

            var dataSet = new SVGChartTools.DataSet.Data[]
            {
                new("jan", jan, $"{currency.Symbol}{jan:#,##0.#0}<br>{janCount} {name}") { CssStyle = cssStyle, CssClass = "clickMe transition", OnClick = janCode },
                new("feb", feb, $"{currency.Symbol}{feb:#,##0.#0}<br>{febCount} {name}") { CssStyle = cssStyle, CssClass = "clickMe transition", OnClick = febCode },
                new("mar", mar, $"{currency.Symbol}{mar:#,##0.#0}<br>{marCount} {name}") { CssStyle = cssStyle, CssClass = "clickMe transition", OnClick = marCode },
                new("apr", apr, $"{currency.Symbol}{apr:#,##0.#0}<br>{aprCount} {name}") { CssStyle = cssStyle, CssClass = "clickMe transition", OnClick = aprCode },
                new("may", may, $"{currency.Symbol}{may:#,##0.#0}<br>{mayCount} {name}") { CssStyle = cssStyle, CssClass = "clickMe transition", OnClick = mayCode },
                new("jun", jun, $"{currency.Symbol}{jun:#,##0.#0}<br>{junCount} {name}") { CssStyle = cssStyle, CssClass = "clickMe transition", OnClick = junCode },
                new("jul", jul, $"{currency.Symbol}{jul:#,##0.#0}<br>{julCount} {name}") { CssStyle = cssStyle, CssClass = "clickMe transition", OnClick = julCode },
                new("aug", aug, $"{currency.Symbol}{aug:#,##0.#0}<br>{augCount} {name}") { CssStyle = cssStyle, CssClass = "clickMe transition", OnClick = augCode },
                new("sep", sep, $"{currency.Symbol}{sep:#,##0.#0}<br>{sepCount} {name}") { CssStyle = cssStyle, CssClass = "clickMe transition", OnClick = sepCode },
                new("oct", oct, $"{currency.Symbol}{oct:#,##0.#0}<br>{octCount} {name}") { CssStyle = cssStyle, CssClass = "clickMe transition", OnClick = octCode },
                new("nov", nov, $"{currency.Symbol}{nov:#,##0.#0}<br>{novCount} {name}") { CssStyle = cssStyle, CssClass = "clickMe transition", OnClick = novCode },
                new("dec", dec, $"{currency.Symbol}{dec:#,##0.#0}<br>{decCount} {name}") { CssStyle = cssStyle, CssClass = "clickMe transition", OnClick = decCode },

            };

            return dataSet;
        }
    }
}