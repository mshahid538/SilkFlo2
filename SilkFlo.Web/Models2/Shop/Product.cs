namespace SilkFlo.Web.Models.Shop
{
    public partial class Product
    {
        public Price Price { get; set; }
        public string BookDemoButtonText { get; set; }
        public bool ShowBookDemoButton => !string.IsNullOrEmpty(BookDemoButtonText);
        public bool ShowSelectButton { get; set; }
        public string SelectButtonText { get; set; } = "Select";
        public bool ShowButtons => ShowBookDemoButton || ShowSelectButton;
        public int TrialDays { get; set; }
        public bool IsCurrent { get; set; }
        public decimal Refund { get; set; }
    }
}