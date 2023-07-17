using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.Idea
{
    public enum UserRelationship
    {
        All,
        MyIdeas,
        MyCollaborations
    }

    public class FilterCriteria
    {
        public FilterCriteria()
        {
            PageIndex = 1;
            FirstPage = 1;
            PageSize = 10;
        }

        public UserRelationship UserRelationship { get; set; } = UserRelationship.All;
        public string FilterSearch { get; set; }
        public bool IsDeployedOnly { get; set; }
        public List<Models.Shared.Stage> Stages { get; set; }
        public List<Models.Shared.IdeaStatus> Statuses { get; set; }
        public List<Models.Shared.SubmissionPath> SubmissionPaths { get; set; }


        public List<Models.Business.Department> Departments { get; set; }
        public List<Models.Business.Team> Teams { get; set; }
        public List<Models.Business.Version> Versions { get; set; }
        public string SortById { get; set; }

        public int Count { get; set; }
        public int PageIndex { get; set; }

        public int FirstPage { get; set; }
        public int LastPage { get; set; }

        public int PageSize { get; set; }
    }
}
