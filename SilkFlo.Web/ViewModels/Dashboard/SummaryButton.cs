namespace SilkFlo.Web.ViewModels.Dashboard
{
    public class SummaryButton
    {
        public SummaryButton(
            string title,
            string value,
            string buttonColour,
            string iconRelativeName,
            string url,
            string onClick,
            decimal? changeIn,
            string hotSpotId = "",
            string minWidth = "200px")
        {
            ButtonColour = buttonColour;
            Title = title;
            Value = value;
            IconRelativeName = iconRelativeName;
            URL = url;
            OnClick = onClick;
            ChangeIn = changeIn;
            HotSpotId = hotSpotId;
            MinWidth = minWidth;
        }

        public string ButtonColour { get; }
        public string Title { get; }
        public string Title2 { get; set; }

        public string Value { get; }
        public string IconRelativeName { get; set; }
        public string URL { get; set; }
        public string OnClick { get; set; }

        public decimal? ChangeIn { get; set; }

        //public string MinWidth { get; set; } = "140px";
        public string MinWidth { get; set; }

        public string HotSpotId { get; set; }

        public int IconWidth { get; set; }
    }
}