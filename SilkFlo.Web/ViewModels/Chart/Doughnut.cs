namespace SilkFlo.Web.ViewModels.Chart
{
    public class Doughnut
    {
        public Doughnut(SVGChartTools.DataSet.PieChart pieChart)
        {
            PieChart = pieChart;
        }


        public SVGChartTools.DataSet.PieChart PieChart { get; }

        public string SVG
        {
            get
            {
                var data = new SVGChartTools.DataSet.Data[PieChart.PieChartKeys.Count];

                var i = 0;
                foreach (var chartKey in PieChart.PieChartKeys)
                {
                    data[i] = new SVGChartTools.DataSet.Data(chartKey.Title, chartKey.Value)
                    {
                        CssStyle = $"fill: {chartKey.Background}; stroke: {chartKey.Background};",
                        CssClass = "clickMe transition",
                        OnClick = chartKey.OnClick,
                        HtmlTooltip = chartKey.HtmlTooltip
                    };
                    i++;
                }



                var paper = new SVGChartTools.PaperPieChart();

                var chart = new SVGChartTools.Chart.Doughnut(data,
                    paper.Centre,
                    paper.Radius,
                    40,
                    PieChart.CentreText)
                {
                    CSSStyle = "fill: var(--bs-gray-even-lightest);"
                };
                paper.Add(chart);

                return paper.SVG;
            }
        }
    }
}
