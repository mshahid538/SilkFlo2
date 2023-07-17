using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Dashboard.Component
{
    public class PipelineBenefitsByStage
    {
        public int Year { get; set; }
        public Chart.Doughnut DoughnutChart { get; set; }

        public List<SVGChartTools.DataSet.PieChartSlice> PieChartKeys { get; set; }
    }
}
