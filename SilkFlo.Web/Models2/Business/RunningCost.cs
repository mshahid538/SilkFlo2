namespace SilkFlo.Web.Models.Business
{
    public partial class RunningCost
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

        public string Name
        {
            get
            {
                var venderName = "Unknown vender";
                if (Vender != null)
                    venderName = Vender.Name;

                var automationType = "Unknown Automation Type";
                if (AutomationTypeId != null)
                    automationType = AutomationTypeId;

                return venderName + " (" + automationType + ")";
            }
        }

        public string Name2 => Name + " " + LicenceType;
    }
}
