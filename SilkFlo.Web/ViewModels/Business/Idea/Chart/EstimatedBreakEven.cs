namespace SilkFlo.Web.ViewModels.Business.Idea.Chart
{
    public class EstimatedBreakEven
    {
        public EstimatedBreakEven(
            SVGChartTools.Paper paper)
        {
            Paper = paper;
            YAxisWidthValue = 30;
        }

        public SVGChartTools.Paper Paper { get; set; }

        public double YAxisWidthValue { get; set; }

        public string YAxisWidth => YAxisWidthValue + "px";
    }
}
