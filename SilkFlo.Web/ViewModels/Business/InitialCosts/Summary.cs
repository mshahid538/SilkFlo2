using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.InitialCosts
{
    public class Summary : SummaryAbstract
    {
        public Summary(
            List<Models.Business.RoleCost> roleCosts,
            Models.Shop.Currency currency,
            int averageWorkingDay)
        {
            RoleCosts = roleCosts;
            Currency = currency;
            AverageWorkingDay = averageWorkingDay;
        }


        public List<Models.Business.RoleCost> RoleCosts { get; }
        public int AverageWorkingDay { get; }
    }
}