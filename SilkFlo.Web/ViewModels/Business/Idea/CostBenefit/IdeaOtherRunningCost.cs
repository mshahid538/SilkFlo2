using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.Idea.CostBenefit
{
    public class IdeaOtherRunningCost
    {
        public List<Models.Business.IdeaOtherRunningCost> IdeaOtherRunningCosts { get; set; }

        public List<Models.Business.OtherRunningCost> OtherRunningCosts { get; set; }

        public string Title { get; set; }
        public string CurrencySymbol { get; set; }

        public string JavascriptNamespace { get; set; }

        public string TableName { get; set; }

        public string NumberTitle { get; set; }
        public string TotalTitle { get; set; }

        public string CostTypeId { get; set; }
    }
}
