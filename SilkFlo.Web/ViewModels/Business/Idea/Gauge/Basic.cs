namespace SilkFlo.Web.ViewModels.Business.Idea.Gauge
{
    public class Basic
    {
        public string Title { get; set; }

        public string TitleMargin { get; set; } = "1.5rem 0 0 0";

        public string StyleTitle
        { 
            get 
            {
                string style = "";
                if(!string.IsNullOrWhiteSpace(TitleMargin))
                {
                    style += $"margin: {TitleMargin};"; 
                }


                if (!string.IsNullOrWhiteSpace(FontSize))
                {
                    style += $"font-size: {FontSize};";
                }

                if (string.IsNullOrWhiteSpace(style))
                    return "";



                return $"style=\"{style}\""; ;
            }
        }

        public string GaugeGraphic { get; set; }
        public decimal Value { get; set; }

        public string ValueUnits { get; set; } = "%";

        public string HotSpotId { get; set; }

        public string FontSize { get; set; }
    }
}
