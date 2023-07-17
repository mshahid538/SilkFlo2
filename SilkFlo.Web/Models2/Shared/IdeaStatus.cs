using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Models.Shared
{
    public partial class IdeaStatus
    {
        public static async Task<IdeaStatus> GetLastAsync(
            Data.Core.IUnitOfWork unitOfWork,
            Business.IdeaStage ideaStage)
        {
            await unitOfWork.BusinessIdeaStageStatuses.GetForIdeaStageAsync(ideaStage.GetCore());

            ideaStage.IdeaStageStatuses ??= new List<Business.IdeaStageStatus>();


            if (ideaStage.IdeaStageStatuses.Count == 0)
            {
                var statusId = Data.Core
                    .Enumerators
                    .IdeaStatus
                    .n00_Idea_AwaitingReview.ToString();

                // Add a new record
                var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
                {
                    IdeaStageId = ideaStage.Id,
                    StatusId = statusId,
                    Date = ideaStage.DateStartEstimate
                };

                await unitOfWork.AddAsync(ideaStageStatus);
                ideaStage.IdeaStageStatuses.Add(new Business.IdeaStageStatus(ideaStageStatus));

                var ideaStatus = await unitOfWork.SharedIdeaStatuses.GetAsync(statusId);

                ideaStage.Status = ideaStatus == null ? 
                    null : 
                    new IdeaStatus(ideaStatus);

                return ideaStage.Status;
            }
            else
            {
                // Get the last.
                var ideaStageStatus = ideaStage.IdeaStageStatuses
                    .OrderBy(x => x.Date)
                    .LastOrDefault();


                if (ideaStageStatus == null)
                {
                    ideaStage.Status = null;
                    return null;
                }

                await unitOfWork.SharedIdeaStatuses.GetStatusForAsync(ideaStageStatus.GetCore());

                ideaStage.Status = ideaStageStatus.Status;
                return ideaStage.Status;
            }
        }
    }
}