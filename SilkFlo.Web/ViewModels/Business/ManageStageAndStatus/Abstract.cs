using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.ViewModels.Business.ManageStageAndStatus
{
    public abstract class Abstract
    {
        public string ErrorMessage { get; set; }

        public async Task<bool> IsIdeaLimitReached(
            Data.Core.IUnitOfWork unitOfWork,
            Data.Core.Domain.Business.IdeaStage lastIdeaStage,
            Data.Core.Domain.Shop.Product product,
            Models.Business.Client client)
        {
            if (lastIdeaStage.StageId == Data.Core.Enumerators.Stage.n00_Idea.ToString())
            {
                await unitOfWork.BusinessIdeas.GetForClientAsync(client.GetCore());
                await unitOfWork.BusinessIdeaStages.GetForIdeaAsync(client.GetCore().Ideas);

                var ideas =
                    client.Ideas
                        .Where(x => x.LastIdeaStage?.Id != Data.Core.Enumerators.Stage.n00_Idea.ToString());



                if (product.IdeaLimit < ideas.Count())
                {
                    ErrorMessage =
                        "You have reached the limit of ideas that you can develop, upgrade your plan for more.<br/>" +
                        $"Your current plan is limited to {product.IdeaLimit} processes.";
                    return true;
                }

                ErrorMessage = "";
            }
            else
                ErrorMessage = "";

            return false;
        }
    }
}
