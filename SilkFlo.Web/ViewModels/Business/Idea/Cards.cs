using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.Idea
{
    public class Cards
    {
        public string Title { get; set; }
        public bool ShowFilter { get; set; } = true;
        public bool ShowSort { get; set; } = true;
        public bool Wrap { get; set; } = true;
        public bool ShowNoIdeasCard { get; set; } = true;

        public FilterCriteria FilterCriteria { get; set; }

        public List<Models.Business.Idea> Ideas { get; set; } = new();

        public List<Models.Business.Department> Departments { get; set; } = new();

        public List<Models.Shared.SubmissionPath> SubmissionPaths { get; set; } = new();
        public List<Models.Business.Version> Versions { get; set; } = new();

        public List<Models.Shared.Stage> Stages { get; set; } = new();

        public List<Models.Shared.IdeaStatus> DeployedStatuses { get; set; } = new();

        public int Count { get; set; }

        public string HotSpotId { get; set; }
    }
}