namespace SilkFlo.Web.ViewModels.Business.Idea.CostBenefit
{
    public class Summary
    {
        public string Id { get; set; }
        public string Currency { get; set; }
        public string EaseOfImplementationWord { get; set; }
        public string EaseOfImplementationFinal { get; set; }


        public string IdeaStageGanttComponent { get; set; }
        public string RobotEstimationComponent { get; set; }
        public string OneTimeCostsComponent { get; set; }
        public string RPASoftwareCostsComponent { get; set; }
        public string IdeaOtherRunningCostComponent { get; set; }
        public string FooterComponent { get; set; }
    }
}
