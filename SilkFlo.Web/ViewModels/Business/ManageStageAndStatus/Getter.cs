using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.ViewModels.Business.ManageStageAndStatus
{
    public class Getter: Abstract
    {
        public Models.Shared.Stage Stage { get; set; }
        public DateTime DateStartEstimate { get; set; }

        public DateTime? DateEndEstimate { get; set; }


        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }




        public Models.Shared.IdeaStatus Status { get; set; }

        public List<Models.Shared.Stage> Stages { get; set; } = new();
        public List<Models.Shared.IdeaStatus> Statuses { get; set; } = new();
        public List<Models.Business.IdeaStageStatus> IdeaStageStatusHistory { get; set; } = new();

        public DateTime MinDateTime { get; set; }


        public string IdeaStageId { get; set; }
        public string StageId { get; set; }
        public string StatusId { get; set; }

        public string IdeaId { get; set; }

        public bool IsNew => string.IsNullOrWhiteSpace(IdeaId);

        public bool IsDraft { get; set; }


        public async Task GetAsync(
            Data.Core.IUnitOfWork unitOfWork,
            Models.Business.Idea idea,
            Data.Core.Domain.Shop.Product product,
            Models.Business.Client client = null)
        {


            try
            {
                // Guard Clause
                if (idea == null)
                {
                    var ex = new ArgumentNullException(nameof(idea));
                    unitOfWork.Log(ex);
                    throw ex;
                }

                IdeaId = idea.Id;
                IsDraft = idea.IsDraft;


                // Cheap permissions
                if (client == null)
                {
                    await unitOfWork.BusinessClients.GetClientForAsync(idea.GetCore());
                    client = idea.Client;
                }


                var lastIdeaStage = await Models.Business.IdeaStage.GatLast(unitOfWork, idea.Id);

                await IsIdeaLimitReached(
                    unitOfWork,
                    lastIdeaStage.GetCore(),
                    product,
                    client);


                Stage = await Models.Shared.Stage.GetLastAsync(unitOfWork, lastIdeaStage);
                Status = await Models.Shared.IdeaStatus.GetLastAsync(unitOfWork, lastIdeaStage);


                await unitOfWork.BusinessIdeaStageStatuses.GetForIdeaStageAsync(lastIdeaStage.GetCore());
                await unitOfWork.SharedIdeaStatuses.GetStatusForAsync(lastIdeaStage.GetCore().IdeaStageStatuses);
                IdeaStageStatusHistory = Models.Business.IdeaStageStatus.Create(lastIdeaStage.GetCore().IdeaStageStatuses);

                MinDateTime = GetMinDate(idea.GetCore());

                IdeaStageId = lastIdeaStage.Id;
                StageId = Stage.Id;
                StatusId = Status.Id;

                DateStartEstimate = lastIdeaStage.DateStartEstimate;
                DateEndEstimate = lastIdeaStage.DateEndEstimate;

                if (!idea.IsNew)
                {
                    var cores = await GetMoveToStatusesAsync(StatusId, unitOfWork);
                    foreach (var core in cores)
                    {
                        var model = new Models.Shared.IdeaStatus(core);
                        Statuses.Add(model);

                        if (core.StageId == Stage.Id)
                            model.Stage = Stage;
                        else
                            await unitOfWork.SharedStages.GetStageForAsync(core);
                    }
                }

                var stages = await unitOfWork.SharedStages.GetAllAsync();
                
                var isSelect = true;
                foreach (var core in stages)
                {
                    var model = new Models.Shared.Stage(core)
                    {
                        IsSelected = isSelect
                    };

                    await unitOfWork.SharedStageGroups.GetStageGroupForAsync(core);

                    Stages.Add(model);


                    if (model.Id == Stage.Id)
                        isSelect = false;
                }
            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
                throw;
            }
        }


        private async Task<Data.Core.Domain.Business.IdeaStage> GetLastIdeaStageAsync(Data.Core.IUnitOfWork unitOfWork,
                                                                                      Models.Business.Idea idea)
        {
            // Get IdeaStage M:M
            await unitOfWork.BusinessIdeaStages.GetForIdeaAsync(idea.GetCore());
            Data.Core.Domain.Business.IdeaStage ideaStage = null;

            var ideaStages = idea.GetCore().IdeaStages.Where(x => x.IsInWorkFlow)
                .OrderBy(x => x.DateStartEstimate)
                .ToList();




            if (ideaStages.Count != 0)
                ideaStage = ideaStages.Last();


            // No ideaStage? then create on
            if (ideaStage == null)
            {
                ideaStage = new Data.Core.Domain.Business.IdeaStage
                {
                    IdeaId = idea.Id,
                    StageId = Data.Core.Enumerators.Stage.n00_Idea.ToString(),
                    DateStartEstimate = DateTime.Now,
                    DateStart = DateTime.Now,
                    IsInWorkFlow = true,
                };

                await unitOfWork.AddAsync(ideaStage);


                var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
                {
                    StatusId = Data.Core.Enumerators.IdeaStatus.n00_Idea_AwaitingReview.ToString(),
                    IdeaStageId = ideaStage.Id,
                };

                await unitOfWork.AddAsync(ideaStageStatus);
            }


            // We have an ideaStage
            DateStartEstimate = ideaStage.DateStartEstimate;
            DateEndEstimate = ideaStage.DateEndEstimate;
                

            DateStart = ideaStage.DateStart;
            DateEnd = ideaStage.DateEnd;

            return ideaStage;
        }







        private static DateTime GetMinDate(Data.Core.Domain.Business.Idea idea)
        {
            if (!idea.IdeaStages.Any())
                return DateTime.Now;


            var last = idea.IdeaStages.Last();
            return last.DateStart?.Date ?? last.DateStartEstimate.Date;
        }

        public static async Task<List<Data.Core.Domain.Shared.IdeaStatus>> GetMoveToStatusesAsync(string statusId,
                                                                                 Data.Core.IUnitOfWork unitOfWork)
        {
            // Guard Clause
            if (string.IsNullOrWhiteSpace(statusId))
                return new List<Data.Core.Domain.Shared.IdeaStatus>();

            var statuses = (await unitOfWork.SharedIdeaStatuses.GetAllAsync()).ToArray();
            var manageStatusHelper = new Models.Business.ManageStatusHelper(statuses);



            var status = statuses.SingleOrDefault(x => x.Id == statusId);

            // Guard Clause
            if (status == null)
            {
                unitOfWork.Log($"Shared.IdeaStatuses with Id '{statusId}' missing");
                return new List<Data.Core.Domain.Shared.IdeaStatus>();
            }


            // 1 Idea.Awaiting Review
            if (status.Id == Data.Core.Enumerators.IdeaStatus.n00_Idea_AwaitingReview.ToString())
            {
                // 2 Idea.Duplicate
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n01_Idea_Duplicate);

                // 3 Idea.Rejected
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n02_Idea_Rejected);

                // 5 Assess.AwaitingReview
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview);
            }

            // 2 Idea.Duplicate
            // 3 Idea.Rejected
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n01_Idea_Duplicate.ToString()
                || status.Id == Data.Core.Enumerators.IdeaStatus.n02_Idea_Rejected.ToString())
            {
                // 4 Idea.Archive
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n03_Idea_Archived);
            }

            // 5 Assess.AwaitingReview
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString())
            {
                // 6 Assess.NotStarted
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n05_Assess_NotStarted);

                // 7 Assess.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress);

                // 9 Assess.Postponed
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n08_Assess_Postponed);

                // 10 Assess.Rejected
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n09_Assess_Rejected);
            }
            // 6 Assess.NotStarted
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n05_Assess_NotStarted.ToString())
            {
                // 7 Assess.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress);
            }

            // 7 Assess.InProgress
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress.ToString())
            {
                // 8 Assess.OnHold
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n07_Assess_OnHold);

                // 11 Assess.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n10_Assess_Archived);

                // 12 Qualify.AwaitingReview
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n11_Qualify_AwaitingReview);
            }

            // 8 Assess.OnHold
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n07_Assess_OnHold.ToString())
            {
                // 7 Assess.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress);
            }

            // 9 Assess.Postponed
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n08_Assess_Postponed.ToString())
            {
                // 7 Assess.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress);

                // 11 Assess.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n10_Assess_Archived);
            }

            // 10 Assess.Rejected
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n09_Assess_Rejected.ToString())
            {
                // 11 Assess.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n10_Assess_Archived);
            }

            // 12 Qualify.AwaitingReview
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n11_Qualify_AwaitingReview.ToString())
            {
                // 13 Qualify.OnHold
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n12_Qualify_OnHold);
                // 14 Qualify.Approved
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n13_Qualify_Approved);
                // 15 Qualify.Rejected
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n14_Qualify_Rejected);
                // 16 Qualify.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n15_Qualify_Archived);

            }

            // 13 Qualify.OnHold
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n12_Qualify_OnHold.ToString())
            {
                // 14 Qualify.Approved
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n13_Qualify_Approved);
                // 15 Qualify.Rejected
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n14_Qualify_Rejected);

            }

            // 14 Qualify.Approved
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n13_Qualify_Approved.ToString())
            {
                // 16 Qualify.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n15_Qualify_Archived);

                // 17 Analysis.NotStarted
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n16_Analysis_NotStarted);
            }

            // 15 Qualify.Rejected
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n14_Qualify_Rejected.ToString())
            {
                // 16 Qualify.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n15_Qualify_Archived);
            }

            // 17 Analysis.NotStarted
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n16_Analysis_NotStarted.ToString())
            {
                // 18 Analysis.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n17_Analysis_InProgress);
            }

            // 18 Analysis.InProgress
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n17_Analysis_InProgress.ToString())
            {
                // 20 Analysis.Cancelled
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n19_Analysis_Cancelled);

                // 19 Analysis.OnHold
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n18_Analysis_OnHold);

                // 21 Analysis.AtRisk
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n20_Analysis_AtRisk);

                // 22 Analysis.Delayed
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n21_Analysis_Delayed);

                // 23 Analysis.Completed
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n22_Analysis_Completed);
            }

            // 19 Analysis.OnHold
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n18_Analysis_OnHold.ToString())
            {
                // 18 Analysis.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n17_Analysis_InProgress);
            }

            // 20 Analysis.Cancelled
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n19_Analysis_Cancelled.ToString())
            {
                // 24 Analysis.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n23_Analysis_Archived);
            }

            // 21 Analysis.AtRisk
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n20_Analysis_AtRisk.ToString())
            {
                // 20 Analysis.Cancelled
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n19_Analysis_Cancelled);

                // 24 Analysis.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n23_Analysis_Archived);
            }

            // 22 Analysis.Delayed
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n21_Analysis_Delayed.ToString())
            {
                // 18 Analysis.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n17_Analysis_InProgress);

                // 24 Analysis.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n23_Analysis_Archived);
            }

            // 23 Analysis.Completed
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n22_Analysis_Completed.ToString())
            {
                // 25 SolutionDesign.NotStarted
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n24_SolutionDesign_NotStarted);
            }

            // 24 SolutionDesign.NotStarted
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n24_SolutionDesign_NotStarted.ToString())
            {
                // 25 SolutionDesign.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n25_SolutionDesign_InProgress);
            }

            // 26 SolutionDesign.InProgress
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n25_SolutionDesign_InProgress.ToString())
            {
                // 27 SolutionDesign.OnHold
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n26_SolutionDesign_OnHold);

                // 28 SolutionDesign.Cancelled
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n27_SolutionDesign_Cancelled);

                // 29 SolutionDesign.AtRisk
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n28_SolutionDesign_AtRisk);

                // 30 SolutionDesign.Delayed
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n29_SolutionDesign_Delayed);

                // 31 SolutionDesign.Completed
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n30_SolutionDesign_Completed);
            }

            // 27 SolutionDesign.OnHold
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n26_SolutionDesign_OnHold.ToString())
            {
                // 26 SolutionDesign.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n25_SolutionDesign_InProgress);
            }

            // 28 SolutionDesign.Cancelled
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n27_SolutionDesign_Cancelled.ToString())
            {
                // 32 SolutionDesign.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n31_SolutionDesign_Archived);
            }

            // 29 SolutionDesign.AtRisk
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n28_SolutionDesign_AtRisk.ToString())
            {
                // 28 SolutionDesign.Cancelled
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n27_SolutionDesign_Cancelled);

                // 32 SolutionDesign.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n31_SolutionDesign_Archived);
            }

            // 30 SolutionDesign.Delayed
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n29_SolutionDesign_Delayed.ToString())
            {
                // 26 SolutionDesign.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n25_SolutionDesign_InProgress);

                // 32 SolutionDesign.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n31_SolutionDesign_Archived);
            }

            // 31 SolutionDesign.Completed
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n30_SolutionDesign_Completed.ToString())
            {
                // 32 Development.NotStarted
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n32_Development_NotStarted);
            }

            // 32 Development.NotStarted
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n32_Development_NotStarted.ToString())
            {
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n33_Development_InProgress);
            }

            // 34 Development InProgress
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n33_Development_InProgress.ToString())
            {
                // 34 Development OnHold
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n34_Development_OnHold);

                // 35 Development OnHold
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n35_Development_Cancelled);

                // 37 Development.AtRisk
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n36_Development_AtRisk);

                // 38 Development.Delayed
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n37_Development_Delayed);

                // 39 Development.Completed
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n38_Development_Completed);
            }

            // 35 Development.OnHold
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n34_Development_OnHold.ToString())
            {
                // 34 Development.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n33_Development_InProgress);
            }

            // 36 Development.Cancelled
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n35_Development_Cancelled.ToString())
            {
                // 40 Development.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n39_Development_Archived);
            }


            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n36_Development_AtRisk.ToString())
            {
                // 36 Development OnHold
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n35_Development_Cancelled);

                // 40 Development.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n39_Development_Archived);
            }


            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n37_Development_Delayed.ToString())
            {
                // 34 Development.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n33_Development_InProgress);

                // 40 Development.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n39_Development_Archived);
            }

            // 39 Development.Completed
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n38_Development_Completed.ToString())
            {
                // 41 Testing.NotStarted
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n40_Testing_NotStarted);
            }

            // 41 Testing.NotStarted
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n40_Testing_NotStarted.ToString())
            {
                // 42 Testing.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n41_Testing_InProgress);
            }

            // 42 Testing.InProgress
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n41_Testing_InProgress.ToString())
            {
                // 43 Testing.OnHold
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n42_Testing_OnHold);

                // 44 Testing.Cancelled
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n43_Testing_Cancelled);

                // 45 Testing.AtRisk
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n44_Testing_AtRisk);

                // 46 Testing.Delayed
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n45_Testing_Delayed);

                // 47 Testing.Completed
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n46_Testing_Completed);
            }

            // 43 Testing.OnHold
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n42_Testing_OnHold.ToString())
            {
                // 42 Testing.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n41_Testing_InProgress);
            }

            // 44 Testing.Cancelled
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n43_Testing_Cancelled.ToString())
            {
                // 48 Testing.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n47_Testing_Archived);
            }

            // 45 Testing.AtRisk
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n44_Testing_AtRisk.ToString())
            {
                // 44 Testing.Cancelled
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n43_Testing_Cancelled);

                // 48 Testing.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n47_Testing_Archived);
            }

            // 46 Testing.Delayed
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n45_Testing_Delayed.ToString())
            {
                // 42 Testing.InProgress
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n41_Testing_InProgress);

                // 48 Testing.Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n47_Testing_Archived);
            }

            // 47 Testing.Completed
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n46_Testing_Completed.ToString())
            {
                // 49 Live.ReadyForProduction
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n48_Deployed_ReadyForProduction);
            }

            // 49Live/ReadyForProduction
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n48_Deployed_ReadyForProduction.ToString())
            {
                // 50 Live.Hypercare
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n49_Deployed_HyperCare);
                // 51 Live.OnHold
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n50_Deployed_OnHold);
            }

            // n50_Live_Hypercare
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n49_Deployed_HyperCare.ToString())
            {
                // 51 Live.OnHold
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n50_Deployed_OnHold);
                // 52 Live.InProduction
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n51_Deployed_InProduction);
            }


            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n50_Deployed_OnHold.ToString())
            {
                // 52 Live.InProduction
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n51_Deployed_InProduction);
            }

            // 52 Live.InProduction
            else if (status.Id == Data.Core.Enumerators.IdeaStatus.n51_Deployed_InProduction.ToString())
            {
                // n53_Live_Archived
                manageStatusHelper.Add(Data.Core.Enumerators.IdeaStatus.n52_Deployed_Archived);
            }


            return manageStatusHelper.Statuses;
        }
    }
}
