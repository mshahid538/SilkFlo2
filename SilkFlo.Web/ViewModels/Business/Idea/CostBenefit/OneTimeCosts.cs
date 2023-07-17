using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.Idea.CostBenefit
{
    public class OneTimeCosts
    {
        public List<Models.Shared.Stage> Stages { get; set; }

        public List<Models.Business.ImplementationCost> ImplementationCosts { get; set; } = new();

        public string CurrencySymbol { get; set; }

        public string IdeaId { get; set; }

        public List<Models.Business.Role> Roles { get; set; } = new();
    }
}
