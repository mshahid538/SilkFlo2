using SVGChartTools.DataSet;
using System;
using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Chart
{
    public class AutomationMatrix
    {
        public AutomationMatrix(SVGChartTools.Font xAxisFont,
                                SVGChartTools.Font yAxisFont,
                                SVGChartTools.Font xLabelFont,
                                SVGChartTools.Font yLabelFont,
                                List<XYZ> data,
                                bool drawLines = false,
                                SVGChartTools.Paper paper = null)
        {
            XAxisFont = xAxisFont ?? throw new ArgumentNullException(nameof(xAxisFont));
            YAxisFont = yAxisFont ?? throw new ArgumentNullException(nameof(yAxisFont));
            XLabelFont = xLabelFont ?? throw new ArgumentNullException(nameof(xLabelFont));
            YLabelFont = yLabelFont ?? throw new ArgumentNullException(nameof(yLabelFont));
            Data = data ?? throw new ArgumentNullException(nameof(data));
            DrawLines = drawLines;
            Paper = paper ?? new SVGChartTools.Paper(300, 300);
        }

        public SVGChartTools.Font XAxisFont { get; protected set; }
        public SVGChartTools.Font YAxisFont { get; protected set; }

        public SVGChartTools.Font XLabelFont { get; protected set; }
        public SVGChartTools.Font YLabelFont { get; protected set; }


        public List<XYZ> Data { get; }

        public string BackgroundColour { get; set; } = "white";

        public int XDivisionsCount { get; set; } = 5;
        public int YDivisionsCount { get; set; } = 5;

        public string XAxisTitle { get; set; } = "Ease of Implementation %";
        public string YAxisTitle { get; set; } = "Automation %";

        public SVGChartTools.Paper Paper { get; }

        public bool DrawLines { get; set; }

        public string SVG
        {
            get
            {
                if (DrawLines)
                {
                    var chart = new SVGChartTools.Chart.BubbleWithDividers(new SVGChartTools.Point(0, 0),
                        Paper.Width,
                        Paper.Height,
                        XAxisTitle,
                        YAxisTitle,
                        XAxisFont,
                        YAxisFont,
                        XLabelFont,
                        YLabelFont,
                        0,
                        100,
                        0,
                        100,
                        Data)
                    {
                        CSSStyle = "fill: transparent; stroke: var(--bs-black);",
                        BackgroundColour = BackgroundColour,
                        ShowXBars = true,
                        XBarCssStyle = "stroke: var(--bs-gray-lighter); stroke-dasharray: 8 8;",
                        XDivisionsCount = XDivisionsCount,
                        YDivisionsCount = YDivisionsCount
                    };

                    Paper.Add(chart);
                }
                else
                {
                    var chart = new SVGChartTools.Chart.Bubble(new SVGChartTools.Point(0, 0),
                        Paper.Width,
                        Paper.Height,
                        XAxisTitle,
                        YAxisTitle,
                        XAxisFont,
                        YAxisFont,
                        XLabelFont,
                        YLabelFont,
                        0,
                        100,
                        0,
                        100,
                        Data)
                    {
                        CSSStyle = "fill: transparent; stroke: var(--bs-black);",
                        BackgroundColour = BackgroundColour,
                        ShowXBars = true,
                        XBarCssStyle = "stroke: var(--bs-gray-lighter); stroke-dasharray: 8 8;",
                        XDivisionsCount = XDivisionsCount,
                        YDivisionsCount = YDivisionsCount
                    };

                    Paper.Add(chart);
                }

                return Paper.SVG;
            }
        }
    }
}
