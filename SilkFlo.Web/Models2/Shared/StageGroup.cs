using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Web.Models.Business;
using System;

namespace SilkFlo.Web.Models.Shared
{
    public partial class StageGroup
    {
        public static async Task<List<Idea>> GetIdeasAsync(
            string name,
            Client client,
            Data.Core.IUnitOfWork unitOfWork)
        {
            await unitOfWork.BusinessIdeas.GetForClientAsync(client.GetCore());

            var models = client.Ideas;

            foreach (var model in models)
            {
                var ideaStages =
                    (await(unitOfWork.BusinessIdeaStages
                        .FindAsync(x => x.IdeaId == model.Id && x.IsInWorkFlow)))
                        .OrderBy(x => x.DateStartEstimate)
                        .ToList();

                await unitOfWork.SharedStages.GetStageForAsync(ideaStages);
                model.IdeaStages = Models.Business.IdeaStage.Create(ideaStages);

                await unitOfWork.GetCreatedUpdatedAsync(model.GetCore());
            }



            // Filter the list
            var stageGroupId = name?.ToLower() switch
            {
                "review" => Data.Core.Enumerators.StageGroup.n00_Review.ToString(),
                "assess" => Data.Core.Enumerators.StageGroup.n01_Assess.ToString(),
                "decision" => Data.Core.Enumerators.StageGroup.n02_Decision.ToString(),
                "build" => Data.Core.Enumerators.StageGroup.n03_Build.ToString(),
                "deployed" => Data.Core.Enumerators.StageGroup.n04_Deployed.ToString(),
                _ => ""
            };

            if (!string.IsNullOrWhiteSpace(stageGroupId))
                models = models.Where(x =>
                    !x.IsDraft
                    && x.LastIdeaStage?.Stage?.StageGroupId == stageGroupId).ToList();



            return models;
        }
    }
}
