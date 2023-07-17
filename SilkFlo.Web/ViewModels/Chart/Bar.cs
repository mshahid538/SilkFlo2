using System;

namespace SilkFlo.Web.ViewModels.Chart
{
    public class Bar
    {
        public Bar(SVGChartTools.DataSet.Chart data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        //public static SVGChartTools.DataSet.Chart TestData()
        //{
        //    var data = new SVGChartTools.DataSet.Chart
        //    {
        //        // X Axis
        //        XAxisLabels = new string [] {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
        //    };


        //    // Data set 1 - Qualified
        //    var cssStyle = "fill: var(--bs-red-lighter); stroke: var(--bs-red-lighter);";
        //    var dataSet = new SVGChartTools.DataSet.Data[]
        //    {
        //        new("data", 2000, "£2,000<br>3 Qualified") { CssStyle = cssStyle },
        //        new("data", 2200, "£2,200<br>4 Qualified") { CssStyle = cssStyle },
        //        new("data", 2500, "£2,500<br>4 Qualified") { CssStyle = cssStyle },
        //        new("data", 2800, "£2,800<br>5 Qualified") { CssStyle = cssStyle },
        //        new("data", 3100, "£3,100<br>6 Qualified") { CssStyle = cssStyle }
        //    };
        //    data.DataSets.Add(dataSet);  

        //    // Data set 2
        //    cssStyle = "fill: var(--bs-warning-lighter); stroke: var(--bs-warning-lighter);";
        //    dataSet = new SVGChartTools.DataSet.Data[]
        //    {
        //        new("data", 2000, "£2,000<br>4 In progress") { CssStyle = cssStyle },
        //        new("data", 2000, "£2,000<br>4 In progress") { CssStyle = cssStyle },
        //        new("data", 2000, "£2,000<br>4 In progress") { CssStyle = cssStyle },
        //        new("data", 2000, "£2,000<br>4 In progress") { CssStyle = cssStyle },
        //        new("data", 2000, "£2,000<br>4 In progress") { CssStyle = cssStyle }
        //    };
        //    data.DataSets.Add(dataSet);


        //    // Data set 3
        //    cssStyle = "fill: var(--bs-green); stroke: var(--bs-green);";
        //    dataSet = new SVGChartTools.DataSet.Data[]
        //    {
        //        new("data", 1000, "£1,000<br>2 deployed") { CssStyle = cssStyle },
        //        new("data", 500, "£500<br>1 deployed") { CssStyle = cssStyle },
        //        new("data", 2500, "£2,500<br>3 deployed") { CssStyle = cssStyle },
        //        new("data", 1800, "£1,800<br>3 deployed") { CssStyle = cssStyle },
        //        new("data", 3100, "£3,100<br>4 deployed") { CssStyle = cssStyle }
        //    };
        //    data.DataSets.Add(dataSet);

        //    return data;
        //}


        public string NumberFormat { get; set; }


        // X Axis
        public int XDivisionsCount { get; set; }


        // Y Axis
        public int YDivisionsCount { get; set; }

        // X Axis
        public string XLabel { get; set; }


       // Y Axis
       public string YLabel { get; set; }


        public SVGChartTools.DataSet.Chart Data { get; }


        
        public string SVG
        {
            get
            {
                var paper = new SVGChartTools.Paper23();



                var barChart = new SVGChartTools.Chart.Bar(
                    new SVGChartTools.Point(0, 0), 
                    paper.Width,
                    paper.Height,
                    XLabel,
                    YLabel,
                    Data,
                    YDivisionsCount,
                    10)
                {
                    CSSStyle = "fill: transparent; stroke: var(--bs-black);",
                    XBarCssStyle = "stroke: var(--bs-gray-lighter); stroke-dasharray: 8 8;",
                    YBarCssStyle = "stroke: blue;",
                    YAxisCssStyle = "stroke: red;",
                    RightBorderCssStyle = "stroke: red;",
                    ShowYAxis = false,
                    ShowRightBorder = false,
                    ShowXBars = true,
                    ShowYBars = false,
                    NumberFormat = NumberFormat,
                    XDivisionsCount = XDivisionsCount,
                    YDivisionsCount = YDivisionsCount
                };

                paper.Add(barChart);

                return paper.SVG;
            }
        }
    }
}
