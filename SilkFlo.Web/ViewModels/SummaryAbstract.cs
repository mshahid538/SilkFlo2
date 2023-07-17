namespace SilkFlo.Web.ViewModels
{
    public abstract class SummaryAbstract
    {
        public Models.Shop.Currency Currency { get; protected set; }
        public string NumberFormat { get; protected set; } = "#,##0.00";
        public bool Show { get; set; } = true;
    }
}
