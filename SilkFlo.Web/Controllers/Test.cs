using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Persistence;
using SilkFlo.Web.Models.Charts;
using Stripe;
using SVGChartTools.DataSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows;

namespace SilkFlo.Web.Controllers
{
    public class Test : Controller
    {
        public IActionResult index()
        {
            return View();
        }

        public IActionResult End()
        {
            return View("Subscription/_EndNotification");
        }
        public IActionResult gracePeriod()
        {
            return View("Subscription/GracePeriod");
        }
        public IActionResult pre2day()
        {
            return View("Subscription/PreEnd2day");
        }
        public IActionResult pre7day()
        {
            return View("Subscription/preEnd7day");
        }
        public IActionResult preLast()
        {
            return View("Subscription/PreEndLast");
        }

        public TypeofChart getTypeofCharts()
        {
            TypeofChart typeofChart = null;
            //string basePath = AppDomain.CurrentDomain.BaseDirectory;
            //string jsonFilePath = Path.Combine(basePath, "Views", "Shared", "Charts", "Data", "ChartType.json");


            //try
            //{
            //    string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
            //   typeofChart = JsonConvert.DeserializeObject<TypeofChart>(jsonContent);

               
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //}

            //



            typeofChart = new TypeofChart()
            {
                lineChart = true,
                barChart = true,
                pieChart = true,
                bubbleChart = false,
                doughnutChart = true,
                scatterChart = true,
                polarChart = false,
            };
            return typeofChart;
        }


        public IActionResult Dashboard()
        {
            var charts = new vmCharts();
            TypeofChart bolTypeofChart = getTypeofCharts();
            if (bolTypeofChart.lineChart)
            {
                charts.lineChart = CreateLineChart("lineChart");
            }
            if (bolTypeofChart.barChart)
            {
                charts.barChart = CreateBarChart("barChart");
            }
            if (bolTypeofChart.pieChart)
            {
                charts.pieChart = CreatePieChart("pieChart");
            }
            if (bolTypeofChart.doughnutChart)
            {
                charts.doughnutChart = CreateDoughnutChart("doughnutChart");
            }
            if (bolTypeofChart.bubbleChart)
            {
                charts.bubbleChart = CreateBubbleChart("bubbleChart");
            }
            if (bolTypeofChart.polarChart)
            {
                charts.polarChart = CreatePolarChart("polarChart");
            }
            if (bolTypeofChart.scatterChart)
            {
                charts.scatterChart = CreateScatterChart("scatterChart");
            }
            
           
           
           
           
           
           
          
            return View("~/Views/Shared/Charts/Dashboard.cshtml" , charts);
        }

        private vmChart CreateLineChart(string ChartName)
        {
            var yAxesTicks = new ChartTicks { BeginAtZero = true };
            var xAxesTicks = new ChartTicks { BeginAtZero = false };

            var yAxesScale = new ChartScale { Ticks = yAxesTicks };
            var xAxesScale = new ChartScale { Ticks = xAxesTicks };

            
            var lineChart = new vmChart
            {
                ChartName = ChartName,
                strType = "line",
                Height = "300",
                Width = "250",
                Responsive = true,
                Animation = true,
                Legend  = new ChartLegend
                {
                    Position= "bottom"
                },
                Labels = { "2000", "2005", "2010", "2015", "2020" },
                data = new List<ChartData>
                {
                    new ChartData
                    {
                        //Label = "Deploved",
                        Data = new List<double> { 30, 50, 20, 70, 40 },
                        BackgroundColor = new List<string> { "rgba(255, 99, 132, 0.2)" },
                        BorderColor = new List<string> { "rgb(255, 99, 132)" },
                        BorderWidth = 1,
                        Fill = false,
                        Tension = Convert.ToDecimal( 0.1),
                        Events=new List<string>{ "click" },
                        ScaleShowVaEues=true,
                        Scales=new ChartAxes  {
                            yAxes = new List<ChartScale> { yAxesScale },
                            xAxes = new List<ChartScale> { xAxesScale }
                        },

                    },
                    new ChartData
                    {
                        //Label = "Estimated",
                        Data = new List<double> { 50, 10, 30, 50, 90 },
                        BackgroundColor = new List<string> { "rgba(255, 99, 132, 0.2)" },
                        BorderColor = new List<string> { "rgb(75, 192, 192)" },
                        BorderWidth = 1,
                        Fill = false,
                        Tension = Convert.ToDecimal( 0.1),
                         Events=new List<string>{ "click" },
                        ScaleShowVaEues=true,
                        Scales=new ChartAxes  {
                            yAxes = new List<ChartScale> { yAxesScale },
                            xAxes = new List<ChartScale> { xAxesScale }
                        },
                    }
                },
            };
            return lineChart;
        }

        private vmChart CreateBarChart(string ChartName)
        {
            var BarChart = new vmChart
            {
                ChartName = ChartName,
                strType = "bar",
                Height = "500",
                Width = "200",
                Responsive = true,
                Animation = true,
                Labels = new List<string> { "2000", "2005", "2010", "2015", "2020" },
                data = new List<ChartData>
                {
                    new ChartData
                    {
                        //Label = "My Chart",
                        Data = new List<double> { 30, 50, 20, 70, 40 },
                        BackgroundColor = new List<string>
                        {
                            "rgba(255, 99, 132, 0.2)",
                            "rgba(255, 159, 64, 0.2)",
                            "rgba(255, 205, 86, 0.2)",
                            "rgba(75, 192, 192, 0.2)",
                            "rgba(54, 162, 235, 0.2)"
                        },
                        BorderColor = new List<string>
                        {
                            "rgb(255, 99, 132)",
                            "rgb(255, 159, 64)",
                            "rgb(255, 205, 86)",
                            "rgb(75, 192, 192)",
                            "rgb(54, 162, 235)"
                        },
                        BorderWidth = 1
                    }
                },
                
            };
            return BarChart;
        }


        private vmChart CreatePieChart(string ChartName)
        {
            var PieChart = new vmChart
            {
                ChartName=ChartName,
                strType = "pie",
                Height = "150",
                Width = "200",
                Responsive = true,
                Animation = true,
                Labels = { "2005", "2010", "2015", "2020", "2023" },
                data = new List<ChartData>
                {
                   new ChartData {
                   //Label = "My Chart",
                    Data = new List<double> { 30, 50, 20, 70, 40 },
                    BackgroundColor =  new List<string>() { "rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)" },
                    BorderColor = new List<string>() { "rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)" },
                    HoverOffset=4
                    },
                },




            };
            return PieChart;
        }
        private vmChart CreateDoughnutChart(string ChartName)
        {
            var PieChart = new vmChart
            {
                ChartName = ChartName,
                strType = "doughnut",
                Height = "150",
                Width = "200",
                Responsive = true,
                Animation = true,
                Labels = { "2005", "2010", "2015", "2020", "2023" },
                data = new List<ChartData>
                {
                   new ChartData {
                   //Label = "My Chart",
                    Data = new List<double> { 30, 50, 20, 70, 40 },
                      BackgroundColor =new List<string>() { "rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)" },
                    BorderColor = new List<string>() { "rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)" },
                    HoverOffset=4
                    },
                },




            };
            return PieChart;
        }
        private vmChart CreateScatterChart(string ChartName)
        {
            var PieChart = new vmChart
            {
                ChartName = ChartName,
                strType = "scatter",
                Height = "150",
                Width = "200",
                Responsive = true,
                Animation = true,
                Labels = { "2005", "2010", "2015", "2020", "2023" },
                data = new List<ChartData>
                {
                   new ChartData {
                   //Label = "My Chart",
                    Data = new List<double> { 30, 50, 20, 70, 40 },
                    BackgroundColor =new List<string>() { "rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)" },
                    BorderColor = new List<string>() { "rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)" },
                    HoverOffset=4
                    },
                },




            };
            return PieChart;
        }
        private vmChart CreateBubbleChart(string ChartName)
        {
            var PieChart = new vmChart
            {
                ChartName = ChartName,
                strType = "bubble",
                Height = "150",
                Width = "200",
                Responsive = true,
                Animation = true,
                Labels = { "2005", "2010", "2015", "2020", "2023" },
                data = new List<ChartData>
                {
                   new ChartData {
                   //Label = "My Chart",
                    Data = new List<double> { 30, 50, 20, 70, 40 },
                     BackgroundColor =new List<string>() { "rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)" },
                    BorderColor = new List<string>() { "rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)" },
                    HoverOffset=4
                    },
                },




            };
            return PieChart;
        }
        private vmChart CreatePolarChart(string ChartName)
        {
            var PieChart = new vmChart
            {
                ChartName = ChartName,
                strType = "polarArea",
                Height = "150",
                Width = "200",
                Responsive = true,
                Animation = true,
                Labels = { "2005", "2010", "2015", "2020", "2023" },
                data = new List<ChartData>
                {
                   new ChartData {
                   //Label = "My Chart",
                    Data = new List<double> { 30, 50, 20, 70, 40 },
                     BackgroundColor =new List<string>() { "rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)" },
                    BorderColor = new List<string>() { "rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)" },
                    HoverOffset=4
                    },
                },




            };
            return PieChart;
        }
    }
}



