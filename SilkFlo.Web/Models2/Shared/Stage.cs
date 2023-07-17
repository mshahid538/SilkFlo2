using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Web.Models.Shared
{
    public partial class Stage
    {
        public List<IdeaStatus> Statuses { get; set; } = new ();


        /// <summary>
        /// Get the last Stage for the supplies IdeaStage
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="lastIdeaStage"></param>
        /// <returns></returns>
        public static async Task<Stage> GetLastAsync(
            Data.Core.IUnitOfWork unitOfWork,
            Business.IdeaStage lastIdeaStage)
        {
            await unitOfWork.SharedStages.GetStageForAsync(lastIdeaStage.GetCore());

            if (lastIdeaStage.Stage != null)
                return lastIdeaStage.Stage;


            var core = await unitOfWork.SharedStages
                .GetAsync(Data.Core
                    .Enumerators
                    .Stage
                    .n00_Idea
                    .ToString());

            if (core == null)
            {
                lastIdeaStage.Stage = null;
                return null;
            }

            var model = new Stage(core);
            lastIdeaStage.Stage = model;
            return model;

        }
    }
}