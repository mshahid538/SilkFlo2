using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.Idea.CostBenefit
{
    public class RPASoftwareCosts
    {
        public List<Models.Business.IdeaRunningCost> IdeaRunningCosts { get; set; }

        public List<Models.Business.RunningCost> RunningCosts { get; set; }

        public string CurrencySymbol { get; set; }
    }
}