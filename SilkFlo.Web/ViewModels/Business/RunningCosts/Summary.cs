using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.RunningCosts
{
    public class Summary : SummaryAbstract
    {
        public Summary(
            List<Models.Business.RunningCost> runningCosts,
            Models.Shop.Currency currency,
            bool show = true)
        {
            RunningCosts = runningCosts;
            Currency = currency;
            Show = show;
        }


        public List<Models.Business.RunningCost> RunningCosts { get; }
    }
}