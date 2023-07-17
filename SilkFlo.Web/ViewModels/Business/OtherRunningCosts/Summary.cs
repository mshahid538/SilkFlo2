using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.OtherRunningCosts
{
    public class Summary : SummaryAbstract
    {
        public Summary(
            List<Models.Business.OtherRunningCost> otherRunningCosts,
            Models.Shop.Currency currency)
        {
            OtherRunningCosts = otherRunningCosts;
            Currency = currency;
        }


        public List<Models.Business.OtherRunningCost> OtherRunningCosts { get; }
    }
}