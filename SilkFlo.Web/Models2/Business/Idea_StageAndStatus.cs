using System.Linq;

namespace SilkFlo.Web.Models.Business
{
    public partial class Idea
    {
        public ViewModels.Business.ManageStageAndStatus.Getter ManageStageAndStatus { get; } = new();

        public void SetLastIdeaStage(IdeaStage lastIdeaStage)
        {
            _lastIdeaStage = lastIdeaStage;
        }

        private IdeaStage _lastIdeaStage;
        public IdeaStage LastIdeaStage
        { 
            get
            {
                if(_lastIdeaStage != null)
                    return _lastIdeaStage;
                
                _lastIdeaStage = IdeaStages.Where(x => x.IsInWorkFlow)
                        .OrderBy(x => x.DateStartEstimate)
                        .LastOrDefault();

                return _lastIdeaStage;
            }
        }
    }
}