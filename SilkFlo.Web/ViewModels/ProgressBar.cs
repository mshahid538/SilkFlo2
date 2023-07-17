namespace SilkFlo.Web.ViewModels
{
    public class ProgressBar
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public decimal Value { get; set; }
        public int Count { get; set; }
        public string Colour { get; set; }

        public int Height { get; set; } = 10;
        public int Width { get; set; } = 99;
        public int Radius { get; set; } = 2;
    }
}
