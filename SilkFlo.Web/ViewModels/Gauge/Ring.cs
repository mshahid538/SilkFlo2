namespace SilkFlo.Web.ViewModels.Gauge
{
    public class Ring
    {
        public Ring(
            decimal data,
            string colour = "",
            decimal weight = 10,
            string lineCap = "round",
            string name = "")
        {
            Data = data;
            Colour = colour;
            Weight = weight;
            LineCap = lineCap;
            Name = name;
        }

        public decimal Data { get; }
        public string Colour { get; }
        public decimal Weight { get; }
        public string LineCap { get; }
        public string Name { get; }


        public string SVG
        {
            get
            {
                const decimal length = 100;
                var paper = new SVGChartTools.Paper(length, length)
                {
                    CSSStyle = "height: 100%; width: 100%"
                };

                var centre = new SVGChartTools.Point(length / 2, length / 2);

                var colour = "var(--bs-danger)";

                if (string.IsNullOrWhiteSpace(Colour))
                {
                    if (Data > 40
                        && Data <= 70)
                        colour = "var(--bs-warning)";
                    else if (Data > 70)
                        colour = "var(--bs-success)";
                }
                else
                    colour = Colour;


                var ringGauge = new SVGChartTools.Gauge.Ring(
                    Data,
                    centre,
                    centre.X,
                    10,
                    colour,
                    "var(--bs-gray-lightest)",
                    LineCap,
                    Name);

                paper.Add(ringGauge);

                return paper.SVG;
            }
        }
    }
}
