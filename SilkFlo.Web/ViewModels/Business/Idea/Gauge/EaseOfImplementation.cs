namespace SilkFlo.Web.ViewModels.Business.Idea.Gauge
{
    public class EaseOfImplementation
    {

        public SVGChartTools.Paper Paper { get; set; }
        public decimal Value { get; set; }

        public string ValueUnits { get; set; } = "%";

        public string[] ValueTitle { get; set; }

        public string[] Title { get; set; }
        public string Difficulty { get; set; }
        public string Colour { get; set; } = "black";
    }
}
