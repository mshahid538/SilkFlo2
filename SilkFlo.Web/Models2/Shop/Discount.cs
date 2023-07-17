namespace SilkFlo.Web.Models.Shop
{
    public partial class Discount
    {
        public string DisplayReferrerName
        {
            get
            {
                var text = Name + ", " + PercentReferrer + "%";

                if (!string.IsNullOrWhiteSpace(DescriptionReferrer))
                    text += ", " + DescriptionReferrer;

                return text;
            }
        }

        public string DisplayResellerName
        {
            get
            {
                var text = Name + ", " + PercentReseller + "%";

                if (!string.IsNullOrWhiteSpace(DescriptionReseller))
                    text += ", " + DescriptionReseller;

                return text;
            }
        }
    }
}
