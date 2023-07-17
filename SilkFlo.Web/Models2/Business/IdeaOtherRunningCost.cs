using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Models.Business
{
    public partial class IdeaOtherRunningCost
    {
        public string CurrencySymbol { get; set; }

        public decimal CostPerYearValue
        {
            get
            {
                if (OtherRunningCost == null)
                    return 0;

                if (OtherRunningCost.FrequencyId == Data.Core.Enumerators.Period.Annual.ToString())
                    return OtherRunningCost.Cost;

                return OtherRunningCost.Cost * 12;
            }
        }

        public string CostPerYear => CurrencySymbol + "\u00A0" + CostPerYearValue.ToString("#,##0.00");


        public decimal CostPerMonthValue
        {
            get
            {
                if (OtherRunningCost == null)
                    return 0;

                if (OtherRunningCost.FrequencyId == Data.Core.Enumerators.Period.Monthly.ToString())
                    return OtherRunningCost.Cost;

                return OtherRunningCost.Cost / 12;
            }
        }

        public string CostPerMonth => CurrencySymbol + "\u00A0" + CostPerMonthValue.ToString("#,##0.00");


        public decimal TotalCostPerYearValue => Number * CostPerYearValue;
        public string TotalCostPerYear => CurrencySymbol + "\u00A0" + TotalCostPerYearValue.ToString("#,##0.00");


        public decimal TotalCostPerMonthValue => Number * CostPerMonthValue;
        public string TotalCostPerMonth => CurrencySymbol + "\u00A0" + TotalCostPerMonthValue.ToString("#,##0.00");

        public string JavascriptNamespace { get; set; }

        public string CostTypeId { get; set; }
    }
}
