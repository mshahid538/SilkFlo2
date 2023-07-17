using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.Idea
{
    public class IdeaStageGantt
    {
        public bool IsReadOnly { get; set; }
        public List<Models.Business.IdeaStage> GanttIdeaStages { get; set; }
    }
}