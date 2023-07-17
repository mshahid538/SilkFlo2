using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.ViewModels.Business.Idea.CostBenefit
{
    public class PostTimeline
    {
        public string Id { get; set; }
        public List<Models.Business.IdeaStage> IdeaStageEstimates { get; set; } = new();

    }
}
