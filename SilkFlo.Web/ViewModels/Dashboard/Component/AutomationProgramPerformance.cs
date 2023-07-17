namespace SilkFlo.Web.ViewModels.Dashboard.Component
{
    public class AutomationProgramPerformance
    {
        public int Year { get; set; }
        public Chart.Bar BarChart { get; set; }
        public bool IncludeDiscoverKey { get; set; } = true;
    }
}