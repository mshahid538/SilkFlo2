namespace SilkFlo.Web.Models.Business
{
    public partial class ImplementationCost
    {
        public string CurrencySymbol { get; set; } = "£";

        public decimal CostDayValue => Role?.Cost ?? 0;

        public string CostDay => CurrencySymbol + "\u00A0" + CostDayValue.ToString("#,##0.00");

        public decimal TotalCostValue => CostDayValue * Day /100 * Allocation;

        public string TotalCost => CurrencySymbol + "\u00A0" + TotalCostValue.ToString("#,##0.00");
    }
}
