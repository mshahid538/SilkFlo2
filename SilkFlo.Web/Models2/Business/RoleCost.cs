namespace SilkFlo.Web.Models.Business
{
    public partial class RoleCost
    {
        public Shop.Currency Currency { get; set; }
        public string NumberFormat { get; set; } = "#,##0.00";
        public int AverageWorkingDay { get; set; }
    }
}