namespace SilkFlo.Web.ViewModels.Business.Idea.CostBenefit
{
    public class GrandTotals
    {
        public string CurrencySymbol { get; set; } = "£";


        public decimal RunningCostsGrandTotalCostPerYearValue { get; set; }
        public string RunningCostsGrandTotalCostPerYear => CurrencySymbol + "\u00A0" + RunningCostsGrandTotalCostPerYearValue.ToString("#,##0.00");
        public decimal RunningCostsGrandTotalCostPerMonthValue { get; set; }
        public string RunningCostsGrandTotalCostPerMonth => CurrencySymbol + "\u00A0" + RunningCostsGrandTotalCostPerMonthValue.ToString("#,##0.00");


        // Human Costs
        public decimal HumanCostsGrandTotalCostPerYearValue { get; set; }
        public string HumanCostsGrandTotalCostPerYear => CurrencySymbol + "\u00A0" + HumanCostsGrandTotalCostPerYearValue.ToString("#,##0.00");
        public decimal HumanCostsGrandTotalCostPerMonthValue { get; set; }
        public string HumanCostsGrandTotalCostPerMonth => CurrencySymbol + "\u00A0" + HumanCostsGrandTotalCostPerMonthValue.ToString("#,##0.00");


        public decimal TotalImplementationPeopleCostsGrandTotalCostPerYearValue { get; set; }
        public string TotalImplementationPeopleCostsGrandTotalCostPerYear => CurrencySymbol + "\u00A0" + TotalImplementationPeopleCostsGrandTotalCostPerYearValue.ToString("#,##0.00");
        public decimal TotalImplementationPeopleCostsGrandTotalCostPerMonthValue { get; set; }
        public string TotalImplementationPeopleCostsGrandTotalCostPerMonth => CurrencySymbol + "\u00A0" + TotalImplementationPeopleCostsGrandTotalCostPerMonthValue.ToString("#,##0.00");



        // Business.RunningCost => RPA Software Costs
        public decimal RpaSoftwareCostsGrandTotalCostPerYearValue { get; set; }
        public string RpaSoftwareCostsGrandTotalCostPerYear => CurrencySymbol + "\u00A0" + RpaSoftwareCostsGrandTotalCostPerYearValue.ToString("#,##0.00");
        public decimal RpaSoftwareCostsGrandTotalCostPerMonthValue { get; set; }
        public string RpaSoftwareCostsGrandTotalCostPerMonth => CurrencySymbol + "\u00A0" + RpaSoftwareCostsGrandTotalCostPerMonthValue.ToString("#,##0.00");



        // Business.OtherRunningCost - Software Licence => Other Software Costs
        public decimal OtherSoftwareCostsGrandTotalCostPerYearValue { get; set; }
        public string OtherSoftwareCostsGrandTotalCostPerYear => CurrencySymbol + "\u00A0" + OtherSoftwareCostsGrandTotalCostPerYearValue.ToString("#,##0.00");
        public decimal OtherSoftwareCostsGrandTotalCostPerMonthValue { get; set; }
        public string OtherSoftwareCostsGrandTotalCostPerMonth => CurrencySymbol + "\u00A0" + OtherSoftwareCostsGrandTotalCostPerMonthValue.ToString("#,##0.00");




        // Business.OtherRunningCost - Support => Support Team
        public decimal SupportTeamGrandTotalCostPerYearValue { get; set; }
        public string SupportTeamGrandTotalCostPerYear => CurrencySymbol + "\u00A0" + SupportTeamGrandTotalCostPerYearValue.ToString("#,##0.00");
        public decimal SupportTeamGrandTotalCostPerMonthValue { get; set; }
        public string SupportTeamGrandTotalCostPerMonth => CurrencySymbol + "\u00A0" + SupportTeamGrandTotalCostPerMonthValue.ToString("#,##0.00");



        // Business.OtherRunningCost - Infrastructure => Infrastructure
        public decimal InfrastructureGrandTotalCostPerYearValue { get; set; }
        public string InfrastructureGrandTotalCostPerYear => CurrencySymbol + "\u00A0" + InfrastructureGrandTotalCostPerYearValue.ToString("#,##0.00");
        public decimal InfrastructureGrandTotalCostPerMonthValue { get; set; }
        public string InfrastructureGrandTotalCostPerMonth => CurrencySymbol + "\u00A0" + InfrastructureGrandTotalCostPerMonthValue.ToString("#,##0.00");


        // Business.OtherRunningCost - Other => Other Costs
        public decimal OtherCostsGrandTotalCostPerYearValue { get; set; }
        public string OtherCostsGrandTotalCostPerYear => CurrencySymbol + "\u00A0" + OtherCostsGrandTotalCostPerYearValue.ToString("#,##0.00");
        public decimal OtherCostsGrandTotalCostPerMonthValue { get; set; }
        public string OtherCostsGrandTotalCostPerMonth => CurrencySymbol + "\u00A0" + OtherCostsGrandTotalCostPerMonthValue.ToString("#,##0.00");


        public decimal GrandTotalCostPerYearValue { get; set; }
        public string GrandTotalCostPerYear => CurrencySymbol + "\u00A0" + GrandTotalCostPerYearValue.ToString("#,##0.00");
        public decimal GrandTotalCostPerMonthValue { get; set; }
        public string GrandTotalCostPerMonth => CurrencySymbol + "\u00A0" + GrandTotalCostPerMonthValue.ToString("#,##0.00");

    }
}
