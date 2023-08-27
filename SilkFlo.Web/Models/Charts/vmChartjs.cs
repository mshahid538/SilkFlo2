using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.Drawing;

namespace SilkFlo.Web.Models.Charts
{

    public class vmChart
    {
        public string ChartName { get; set; }
        public vmChart() { Labels = new List<string>(); data = new List<ChartData>(); }
        public string strType { get; set; }
        public string Height { get; set; } // Height of the chart container
        public string Width { get; set; } // Width of the chart container
        public bool Responsive { get; set; } // Whether the chart should be responsive
        public bool Animation { get; set; } // Whether to enable animations
        public ChartLegend Legend { get; set; }
        public List<string> Labels { get; set; }
        public List<ChartData> data { get; set; }
        public ChartOptions options { get; set; }
        public List<ChartPlugin> Plugins { get; set; }
    }
    public class ChartData
    {
        public ChartData() { BorderColor = Events= BackgroundColor = new List<string>(); }

        public string Label { get; set; }
        public List<double> Data { get; set; }
        public List<string> BackgroundColor { get; set; }
        public List<string> BorderColor { get; set; }
        public int BorderWidth { get; set; }
        public int HoverOffset { get; set; }
        public bool Fill { get; set; }
        public decimal Tension { get; set; }
        public string PointBackgroundColor { get; set; }
        public string PointBorderColor { get; set; }
        public string PointHoverBackgroundColor { get; set; }
        public string PointHoverBorderColor { get; set; }
        public List<string> Events { get; set; }
        public bool ScaleShowVaEues { get; set; }
        public ChartAxes Scales { get; set; }
    }
    public class ChartPlugin
    {

    }
    public class ChartLegend
    {
        public bool Display { get; set; } // Is the legend shown?
        public string Position { get; set; } // Position of the legend
        public string Align { get; set; } // Alignment of the legend
        public int? MaxHeight { get; set; } // Maximum height of the legend, in pixels
        public int? MaxWidth { get; set; } // Maximum width of the legend, in pixels
        public bool FullSize { get; set; } // Should take the full width/height of the canvas
        public string OnClick { get; set; } // JavaScript function name or code for the click event callback
        public string OnHover { get; set; } // JavaScript function name or code for the hover event callback
        public string OnLeave { get; set; } // JavaScript function name or code for the leave event callback
        public bool Reverse { get; set; } // Legend will show datasets in reverse order
       
        public bool Rtl { get; set; } // Rendering the legends from right to left
        public string TextDirection { get; set; } // Force text direction 'rtl' or 'ltr' on the canvas for rendering
     
    }

    public class ChartPageViewModel
    {
        public ChartPageViewModel()
        {
            lstChart = new List<vmChart>();
        }
        public List<vmChart> lstChart { get; set; }
    }
    public class vmCharts
    {
        public vmChart lineChart { get; set; }
        public vmChart barChart { get; set; }
        public vmChart pieChart { get; set; }
        public vmChart doughnutChart { get; set; }
        public vmChart bubbleChart { get; set; }
        public vmChart polarChart { get; set; }
        public vmChart scatterChart { get; set; }
    }

    public class TypeofChart
    {
        public bool lineChart { get; set; }
        public bool barChart { get; set; }
        public bool pieChart { get; set; }
        public bool doughnutChart { get; set; }
        public bool bubbleChart { get; set; }
        public bool polarChart { get; set; }
        public bool scatterChart { get; set; }
    }


    public class ChartOptions
    {
        public bool ScaleShowValues { get; set; } // Whether to show scale values
        public ChartAxes scales { get; set; } // Scale options for axes
    }

   

   

    

    public class ChartAxes
    {
        public List<ChartScale> yAxes  { get; set; } // Options for Y axes
        public List<ChartScale> xAxes { get; set; } // Options for X axes
                                                    // Other common scales for all chart types
    }

    public class ChartScale
    {
        public ChartTicks Ticks { get; set; } // Options for scale ticks
        //public bool beginAtZero { get; set; }                          // Other common scale properties for all chart types
    }

    public class ChartTicks
    {
        public bool BeginAtZero { get; set; } // Whether scale ticks should begin at zero
                                              // Other common tick properties for all chart types
    }

   

  




}



