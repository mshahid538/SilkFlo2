namespace SilkFlo.Web.Models.Business
{
    public partial class OtherRunningCost
    {
        public Shop.Currency Currency { get; set; }
        public string NumberFormat { get; set; } = "#,##0.00";
        
        public decimal Annual
        {
            get
            {
                if (FrequencyId == Data.Core.Enumerators.Period.Monthly.ToString())
                    return Cost * 12;

                return Cost;
            }
        }
    }
}
