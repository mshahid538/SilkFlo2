using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Models.Business
{
    public partial class IdeaRunningCost
    {
        public string CurrencySymbol { get; set; }

        public decimal CostPerYearValue
        {
            get
            {
                if (RunningCost == null)
                    return 0;

                if (RunningCost.FrequencyId == Data.Core.Enumerators.Period.Annual.ToString())
                    return RunningCost.Cost;

                return RunningCost.Cost * 12;
            }
        }

        public string CostPerYear => CurrencySymbol + "\u00A0" + CostPerYearValue.ToString("#,##0.00");


        public decimal CostPerMonthValue
        {
            get
            {
                if (RunningCost == null)
                    return 0;

                if (RunningCost.FrequencyId == Data.Core.Enumerators.Period.Monthly.ToString())
                    return RunningCost.Cost;

                return RunningCost.Cost / 12;
            }
        }

        public string CostPerMonth => CurrencySymbol + "\u00A0" + CostPerMonthValue.ToString("#,##0.00");


        public decimal TotalCostPerYearValue => LicenceCount * CostPerYearValue;
        public string TotalCostPerYear => CurrencySymbol + "\u00A0" + TotalCostPerYearValue.ToString("#,##0.00");


        public decimal TotalCostPerMonthValue => LicenceCount * CostPerMonthValue;
        public string TotalCostPerMonth => CurrencySymbol + "\u00A0" + TotalCostPerMonthValue.ToString("#,##0.00");

    }
}
