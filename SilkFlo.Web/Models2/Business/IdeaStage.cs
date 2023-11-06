using System;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Models.Business
{
    public partial class IdeaStage
    {
        public string Name => Stage?.Name;

        public Shared.IdeaStatus Status { get; set; }

        public string StatusId { get; set; }


        public int Day
        {
            get
            {
                if (DateStart == null || DateEnd == null)
                    return 0;

                return ((DateTime)DateEnd - (DateTime)DateStart).Days + 1;
            }
        }

        public override string ToString()
        {
            return StageId + " " + DateStartEstimate.ToString("yyyy-MM-dd");
        }

        public static async Task GatLast(
            Data.Core.IUnitOfWork unitOfWork,
            Idea idea)
        {
            if(unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            if (idea == null)
                throw new ArgumentNullException(nameof(idea));

            var core = await GatLast(
                unitOfWork,
                idea.Id);

            idea.SetLastIdeaStage(core);
        }

        public static async Task<IdeaStage> GatLast(
            Data.Core.IUnitOfWork unitOfWork,
            string ideaId)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            if (string.IsNullOrWhiteSpace(ideaId))
                throw new ArgumentNullException(nameof(ideaId));

            var core = (await unitOfWork.BusinessIdeaStages
                    .FindAsync(x => x.IdeaId == ideaId && x.IsInWorkFlow))
                .OrderBy(x => x.DateStartEstimate)
                .LastOrDefault();

            if (core == null)
                return null;

            return new IdeaStage(core);
        }


        // Data.Core.Enumerators.Stage.n00_Idea
        // Models.Business.IdeaStage.AddWorkFlow
        public static async Task AddWorkFlow(
            Data.Core.IUnitOfWork unitOfWork,
            Data.Core.Domain.Business.Idea idea )
        {
            var firstStage = Data.Core.Enumerators.Stage.n00_Idea;
            if (idea.SubmissionPathId == Data.Core.Enumerators.SubmissionPath.COEUser.ToString())
                firstStage = Data.Core.Enumerators.Stage.n01_Assess;

            var date = DateTime.Now;
            var ideaStage = new Data.Core.Domain.Business.IdeaStage
            {
                Idea = idea,
                StageId = firstStage.ToString(),
                DateStartEstimate = date,
                DateStart = date,
                IsInWorkFlow = true,
            };

            await unitOfWork.AddAsync(ideaStage);
            await unitOfWork.CompleteAsync();

            if (firstStage == Data.Core.Enumerators.Stage.n00_Idea)
            {
                var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
                {
                    IdeaStageId = ideaStage.Id,
                    StatusId = Data.Core.Enumerators.IdeaStatus.n00_Idea_AwaitingReview.ToString(),
                    Date = date
                };
                await unitOfWork.AddAsync(ideaStageStatus);
                await unitOfWork.CompleteAsync();
            }
            else
            {
                var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
                {
                    IdeaStageId = ideaStage.Id,
                    StatusId = Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
                    Date = date
                };
                await unitOfWork.AddAsync(ideaStageStatus);
                await unitOfWork.CompleteAsync();
            }

            var stages = (await unitOfWork.SharedStages.FindAsync(x => x.Id != firstStage.ToString())).ToArray();
            if (firstStage == Data.Core.Enumerators.Stage.n01_Assess)
                stages = stages.Where(x => x.Id != Data.Core.Enumerators.Stage.n00_Idea.ToString()).ToArray();

            var now = DateTime.Now;
            foreach (var stage in stages)
            {
                ideaStage = new Data.Core.Domain.Business.IdeaStage
                {
                    Idea = idea,
                    DateStartEstimate = now,
                    Stage = stage
                };

                now = now.AddSeconds(1);
                await unitOfWork.AddAsync(ideaStage);
            }
            await unitOfWork.CompleteAsync();
        }
    }
}