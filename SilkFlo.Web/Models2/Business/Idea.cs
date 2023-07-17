using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Models.Business
{
    public partial class Idea
    {
        public bool IsReadOnly { get; set; }
        public int FollowCount { get; set; }
        public int VoteCount { get; set; }

        public bool IsFollowed { get; set; }
        public bool IsVoted { get; set; }

        public bool CanEditAbout { get; set; }


        public bool CanEditProcessOwner { get; set; }


        public bool CanEditStages { get; set; }

        public bool HasPassedAccessStage { get; set; }

        public bool ShowDetailedAssessmentFields { get; set; }

        public decimal BenefitPerCompanyHoursRelativePercent { get; set; } = 100;
        public string BenefitPerCompanyHoursCssStyle { get; set; } = "fill: red; stroke: none; opacity:0.6;";

        public List<User> CollaboratingUsers { get; set; } = new();

        public List<IdeaStage> GanttIdeaStages { get; set; } = new();

        public ViewModels.Business.Idea.CostBenefit.OneTimeCosts OneTimeCosts { get; set; }
        public ViewModels.Business.Idea.CostBenefit.RPASoftwareCosts RPASoftwareCosts { get; set; }
        public ViewModels.Business.Idea.CostBenefit.IdeaOtherRunningCost IdeaOtherRunningCost { get; set; }


        public int? CollaboratorLimit { get; set; }

        // Show context Menu properties
        public bool ShowManageStagesMenuItem { get; set; }
        public bool ShowViewDetailsMenuItem { get; set; } = true;
        public bool ShowEditMenuItem { get; set; }
        public bool ShowDeleteMenuItem { get; set; }


        public bool Show =>
            ShowManageStagesMenuItem
            || ShowViewDetailsMenuItem
            || ShowEditMenuItem
            || ShowDeleteMenuItem;
        public async Task GetMetaData_Modal(Data.Core.IUnitOfWork unitOfWork,
            int? collaboratorLimit)
        {
            CollaboratorLimit = collaboratorLimit;

            // Get Departments
            var coreDepartments = await unitOfWork.BusinessDepartments.FindAsync(x => x.ClientId == ClientId);
            Departments.Add(new Department { DisplayText = "Select..." });
            foreach (var item in coreDepartments)
                Departments.Add(new Department(item));


            // Get Teams
            Teams.Add(new Team { DisplayText = "Select..." });
            if (!string.IsNullOrWhiteSpace(DepartmentId))
            {
                var teamsCore = await unitOfWork.BusinessTeams
                                           .FindAsync(x => x.DepartmentId == DepartmentId);

                foreach (var item in teamsCore)
                    Teams.Add(new Team(item));
            }



            // Get Processes
            Processes.Add(new Process { DisplayText = "Select..." });
            if (!string.IsNullOrWhiteSpace(TeamId))
            {
                var processCores = await unitOfWork.BusinessProcesses
                                              .FindAsync(x => x.TeamId == TeamId);

                foreach (var item in processCores)
                    Processes.Add(new Process(item));
            }



            // Get Rules
            var coreRules = (await unitOfWork.SharedRules.GetAllAsync())
                                             .OrderBy(x => x.Weighting);
            foreach (var item in coreRules)
                Rules.Add(new Shared.Rule(item));



            // Get Inputs
            var coreInputs = (await unitOfWork.SharedInputs.GetAllAsync())
                                              .OrderBy(x => x.Weighting);
            foreach (var item in coreInputs)
                Inputs.Add(new Shared.Input(item));


            // Get InputDataStructures
            var coreInputDataStructures = (await unitOfWork.SharedInputDataStructures.GetAllAsync())
                                              .OrderBy(x => x.Weighting);
            foreach (var item in coreInputDataStructures)
                InputDataStructures.Add(new Shared.InputDataStructure(item));



            // Get ApplicationStabilities
            var coreApplicationChanges = (await unitOfWork.SharedApplicationStabilities.GetAllAsync())
                                              .OrderBy(x => x.Weighting);
            foreach (var item in coreApplicationChanges)
                ApplicationStabilities.Add(new Shared.ApplicationStability(item));


            // Get DocumentationPresents
            var coreDocumentationPresents = (await unitOfWork.SharedDocumentationPresents.GetAllAsync())
                                              .OrderBy(x => x.Weighting);
            foreach (var item in coreDocumentationPresents)
                DocumentationPresents.Add(new Shared.DocumentationPresent(item));


            // Get ProcessStabilities
            var coreProcessStabilities = (await unitOfWork.SharedProcessStabilities.GetAllAsync())
                                              .OrderBy(x => x.Weighting);
            foreach (var item in coreProcessStabilities)
                ProcessStabilities.Add(new Shared.ProcessStability(item));

            // Get Collaborating Users
            await unitOfWork.BusinessCollaborators.GetForIdeaAsync(this.GetCore());
            await unitOfWork.Users.GetUserForAsync(this.GetCore().Collaborators);
            foreach (var collaborator in Collaborators)
            {
                CollaboratingUsers.Add(collaborator.User);
            }
        }





        public async Task GetMetaData_Detail(
            Data.Core.Domain.Shop.Product product,
            bool includeManageStageAndStatus,
            string userId = "")
        {
            var core = GetCore();

            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            await UnitOfWork.SharedApplicationStabilities.GetApplicationStabilityForAsync(core);
            await UnitOfWork.SharedAutomationGoals.GetAutomationGoalForAsync(core);
            await UnitOfWork.SharedAverageNumberOfSteps.GetAverageNumberOfStepForAsync(core);
            await UnitOfWork.BusinessClients.GetClientForAsync(core);
            await UnitOfWork.SharedDataInputPercentOfStructureds.GetDataInputPercentOfStructuredForAsync(core);
            await UnitOfWork.BusinessDepartments.GetDepartmentForAsync(core);
            await UnitOfWork.SharedInputDataStructures.GetInputDataStructureForAsync(core);
            await UnitOfWork.SharedInputs.GetInputForAsync(core);
            await UnitOfWork.SharedNumberOfWaysToCompleteProcesses.GetNumberOfWaysToCompleteProcessForAsync(core);
            await UnitOfWork.SharedProcessStabilities.GetProcessStabilityForAsync(core);
            await UnitOfWork.BusinessProcesses.GetProcessForAsync(core);
            await UnitOfWork.SharedProcessPeaks.GetProcessPeakForAsync(core);
            await UnitOfWork.SharedRules.GetRuleForAsync(core);
            await UnitOfWork.Users.GetProcessOwnerForAsync(core);
            await UnitOfWork.SharedTaskFrequencies.GetTaskFrequencyForAsync(core);
            await UnitOfWork.BusinessTeams.GetTeamForAsync(core);
            await UnitOfWork.SharedDocumentationPresents.GetDocumentationPresentForAsync(core);

            await UnitOfWork.ShopCurrencies.GetCurrencyForAsync(core.Client);
            await UnitOfWork.SharedLanguages.GetLanguageForAsync(core.Client);

            await UnitOfWork.BusinessVersions.GetForClientAsync(core.Client);
            await UnitOfWork.BusinessApplications.GetApplicationForAsync(core.Client?.Versions);

            await UnitOfWork.SharedDecisionCounts.GetDecisionCountForAsync(core);
            await UnitOfWork.SharedDecisionDifficulties.GetDecisionDifficultyForAsync(core);


           // IdeaStages = IdeaStages.Where(x => x.IsInWorkFlow).ToList();

            if (!IsDraft)
            {
                if(includeManageStageAndStatus)
                    await ManageStageAndStatus.GetAsync(
                        UnitOfWork, 
                        this, 
                        product);

                await UnitOfWork.BusinessIdeaStages.GetForIdeaAsync(GetCore());

                var lastIdeaStage = LastIdeaStage;
                var ideaStageStatus = lastIdeaStage?.GetCore().IdeaStageStatuses.OrderBy(x => x.CreatedDate).LastOrDefault();

                if (ideaStageStatus != null)
                {
                    await UnitOfWork.SharedIdeaStatuses.GetStatusForAsync(ideaStageStatus);

                    lastIdeaStage.Status = new Shared.IdeaStatus(ideaStageStatus.Status);

                    await UnitOfWork.SharedStages.GetStageForAsync(lastIdeaStage.GetCore());

                    if (lastIdeaStage.Stage.StageGroupId == Data.Core.Enumerators.StageGroup.n00_Review.ToString()
                        || lastIdeaStage.Stage.StageGroupId ==
                        Data.Core.Enumerators.StageGroup.n01_Assess.ToString())
                        HasPassedAccessStage = false;
                    else
                        HasPassedAccessStage = true;
                }
            }




            //idea.IdeaApplications
            await GetApplicationsAsync();





            var languages = await UnitOfWork.SharedLanguages.GetAllAsync();
            foreach (var item in languages)
                Languages.Add(new Shared.Language(item));






            await GetAutomationPotentialAsync();
            await GetEaseOfImplementationAsync();
            await GetFeasibilityScoreAsync();
            await GetPrimedScoreAsync();
            await GetFitnessScoreAsync();
            await GetIdeaScoreAsync();


            GetBenefitPerEmployee_Hours();
            GetBenefitPerEmployee_Currency();
            await CalculateAllEstimatedFTE();


            // Get Collaborating Users
            CollaboratingUsers = await Collaborator.GetUsersAsync(UnitOfWork, Id);
        }


        public async Task GetApplicationsAsync()
        {
            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            var core = this.GetCore();

            var applications = (await UnitOfWork.BusinessApplications.FindAsync(x => x.ClientId == core.Client?.Id)).ToArray();
            await UnitOfWork.BusinessIdeaApplicationVersions.GetForIdeaAsync(core);

            if (!applications.Any())
                return;

            foreach (var application in applications)
            {
                await UnitOfWork.BusinessVersions.GetForApplicationAsync(application);

                var versions = application.Versions.Where(version => version.IsLive).ToList();
                var versionFound = versions.Any();


                var applicationModel = new Application(application);

                foreach (var ideaApplicationVersion in core.IdeaApplicationVersions)
                {
                    foreach (var version in versions.Where(version => ideaApplicationVersion.VersionId == version.Id))
                    {
                        applicationModel.VersionId = version.Id;
                        applicationModel.IsSelected = true;
                        break;
                    }

                    if (!applicationModel.IsSelected)
                        continue;


                    var languageId = ideaApplicationVersion.LanguageId;
                    if (string.IsNullOrWhiteSpace(languageId))
                        languageId = core.Client.LanguageId;

                    applicationModel.LanguageId = languageId;
                    applicationModel.IsThinClient = ideaApplicationVersion.IsThinClient;
                    break;
                }

                if(versionFound)
                    Applications.Add(applicationModel);
            }
        }

        public async Task GetLists()
        {
            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            var core = GetCore();

            var coreApplicationChanges = await UnitOfWork.SharedApplicationStabilities.GetAllAsync();
            ApplicationStabilities.Add(new Shared.ApplicationStability { DisplayText = "Select..." });
            foreach (var item in coreApplicationChanges)
                ApplicationStabilities.Add(new Shared.ApplicationStability(item));

            var coreAutomationGoals = await UnitOfWork.SharedAutomationGoals.GetAllAsync();
            AutomationGoals.Add(new Shared.AutomationGoal { DisplayText = "Select..." });
            foreach (var item in coreAutomationGoals)
                AutomationGoals.Add(new Shared.AutomationGoal(item));

            var coreAverageNumberOfSteps = await UnitOfWork.SharedAverageNumberOfSteps.GetAllAsync();
            AverageNumberOfSteps.Add(new Shared.AverageNumberOfStep { DisplayText = "Select..." });
            foreach (var item in coreAverageNumberOfSteps)
                AverageNumberOfSteps.Add(new Shared.AverageNumberOfStep(item));

            var coreClients = await UnitOfWork.BusinessClients.GetAllAsync();
            foreach (var item in coreClients)
                Clients.Add(new Client(item));

            var coreDataInputPercentOfStructureds = await UnitOfWork.SharedDataInputPercentOfStructureds.GetAllAsync();
            DataInputPercentOfStructureds.Add(new Shared.DataInputPercentOfStructured { DisplayText = "Select..." });
            foreach (var item in coreDataInputPercentOfStructureds)
                DataInputPercentOfStructureds.Add(new Shared.DataInputPercentOfStructured(item));

            var coreInputDataStructures = await UnitOfWork.SharedInputDataStructures.GetAllAsync();
            InputDataStructures.Add(new Shared.InputDataStructure { DisplayText = "Select..." });
            foreach (var item in coreInputDataStructures)
                InputDataStructures.Add(new Shared.InputDataStructure(item));

            var coreInputs = await UnitOfWork.SharedInputs.GetAllAsync();
            Inputs.Add(new Shared.Input { DisplayText = "Select..." });
            foreach (var item in coreInputs)
                Inputs.Add(new Shared.Input(item));

            var coreNumberOfWaysToCompleteProcesses = await UnitOfWork.SharedNumberOfWaysToCompleteProcesses.GetAllAsync();
            NumberOfWaysToCompleteProcesses.Add(new Shared.NumberOfWaysToCompleteProcess { DisplayText = "Select..." });
            foreach (var item in coreNumberOfWaysToCompleteProcesses)
                NumberOfWaysToCompleteProcesses.Add(new Shared.NumberOfWaysToCompleteProcess(item));

            var coreProcessStabilities = await UnitOfWork.SharedProcessStabilities.GetAllAsync();
            ProcessStabilities.Add(new Shared.ProcessStability { DisplayText = "Select..." });
            foreach (var item in coreProcessStabilities)
                ProcessStabilities.Add(new Shared.ProcessStability(item));



            var coreProcessPeaks = await UnitOfWork.SharedProcessPeaks.GetAllAsync();
            ProcessPeaks.Add(new Shared.ProcessPeak { DisplayText = "Select..." });
            foreach (var item in coreProcessPeaks)
                ProcessPeaks.Add(new Shared.ProcessPeak(item));

            var coreRules = await UnitOfWork.SharedRules.GetAllAsync();
            Rules.Add(new Shared.Rule { DisplayText = "Select..." });
            foreach (var item in coreRules)
                Rules.Add(new Shared.Rule(item));

            //var coreProcessOwners = await UnitOfWork.Users.GetAllAsync();
            //foreach (var item in coreProcessOwners)
            //    ProcessOwners.Add(new User(item));


            var coreTaskFrequencies = await UnitOfWork.SharedTaskFrequencies.GetAllAsync();
            TaskFrequencies.Add(new Shared.TaskFrequency { DisplayText = "Select..." });
            foreach (var item in coreTaskFrequencies)
                TaskFrequencies.Add(new Shared.TaskFrequency(item));


            var coreDocumentationPresents = await UnitOfWork.SharedDocumentationPresents.GetAllAsync();
            DocumentationPresents.Add(new Shared.DocumentationPresent { DisplayText = "Select..." });
            foreach (var item in coreDocumentationPresents)
                DocumentationPresents.Add(new Shared.DocumentationPresent(item));

            var coreDecisionCounts = await UnitOfWork.SharedDecisionCounts.GetAllAsync();
            DecisionCounts.Add(new Shared.DecisionCount { DisplayText = "Select..." });
            foreach (var item in coreDecisionCounts)
                DecisionCounts.Add(new Shared.DecisionCount(item));

            var coreDecisionDifficulties = await UnitOfWork.SharedDecisionDifficulties.GetAllAsync();
            DecisionDifficulties.Add(new Shared.DecisionDifficulty { DisplayText = "Select..." });
            foreach (var item in coreDecisionDifficulties)
                DecisionDifficulties.Add(new Shared.DecisionDifficulty(item));


            var coreDepartments = await UnitOfWork.BusinessDepartments.FindAsync(x => x.ClientId == core.ClientId);
            Departments.Add(new Department { DisplayText = "Select..." });
            foreach (var item in coreDepartments)
                Departments.Add(new Department(item));



            Teams.Add(new Team { DisplayText = "Select..." });
            if (!string.IsNullOrWhiteSpace(DepartmentId))
            {
                var teamsCore = await UnitOfWork.BusinessTeams
                                           .FindAsync(x => x.DepartmentId == DepartmentId);

                foreach (var item in teamsCore)
                    Teams.Add(new Team(item));
            }



            Processes.Add(new Process { DisplayText = "Select..." });
            if (!string.IsNullOrWhiteSpace(TeamId))
            {
                var processCores = await UnitOfWork.BusinessProcesses
                                              .FindAsync(x => x.TeamId == TeamId);

                foreach (var item in processCores)
                    Processes.Add(new Process(item));
            }
        }


        public async Task GetLikes(string userId)
        {
            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            await UnitOfWork.BusinessFollows.GetForIdeaAsync(this.GetCore());
            await UnitOfWork.BusinessVotes.GetForIdeaAsync(this.GetCore());

            var core = this.GetCore();

            FollowCount = core.Follows.Count;
            var follow = core.Follows.SingleOrDefault(x => x.UserId == userId);
            if (follow != null)
                IsFollowed = true;

            VoteCount = core.Votes.Count;
            var vote = core.Votes.SingleOrDefault(x => x.UserId == userId);
            if (vote != null)
                IsVoted = true;
        }


        public static async Task<bool> GetForCardsByTextAsync(Data.Core.IUnitOfWork unitOfWork,
                                                              string userId,
                                                              Data.Core.Domain.Business.Client tenant,
                                                              ViewModels.Business.Idea.FilterCriteria filterCriteria,
                                                              Controllers.Abstract controller,
                                                              bool includeMyDrafts = false)
        {
            try
            {
                var ideas = tenant.Ideas;
                await unitOfWork.BusinessIdeas.GetForClientAsync(tenant);
                await unitOfWork.BusinessIdeas.GetForClientAsync(tenant);
                await unitOfWork.Users.GetProcessOwnerForAsync(ideas);

                switch (filterCriteria.UserRelationship)
                {
                    case ViewModels.Business.Idea.UserRelationship.MyIdeas:
                        ideas = ideas.Where(x => x.ProcessOwnerId == userId).ToList();
                        break;

                    case ViewModels.Business.Idea.UserRelationship.MyCollaborations:
                        {
                            var userAuthorisations = await unitOfWork
                                .BusinessUserAuthorisations
                                .FindAsync(x => x.UserId == userId);

                            var ids = userAuthorisations.Select(x => x.IdeaId).Distinct();
                            ideas = ideas.Where(s => ids.Contains(s.Id)).ToList();
                            break;
                        }
                }

                ideas = FilterByText(tenant.Ideas, filterCriteria.FilterSearch);

                return ideas != null && ideas.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
                throw;
            }
        }

        public static async Task<List<Idea>> GetForCardsAsync(Data.Core.IUnitOfWork unitOfWork,
                                                              string userId,
                                                              Data.Core.Domain.Business.Client tenant,
                                                              ViewModels.Business.Idea.FilterCriteria filterCriteria,
                                                              Controllers.Abstract controller,
                                                              bool includeMyDrafts = false)
        {
            try
            {
                await unitOfWork.BusinessIdeas.GetForClientAsync(tenant);

                var ideas = await GetIdeasAsync(
                    unitOfWork, 
                    userId, 
                    tenant, 
                    filterCriteria );



                var modelsIdea = new List<Idea>();
                foreach (var model in ideas)
                {
                    await unitOfWork.BusinessIdeaStages.GetForIdeaAsync(model.GetCore());
                    await unitOfWork.SharedStages.GetStageForAsync(model.GetCore().IdeaStages);

                    await IdeaStage.GatLast(unitOfWork, model);

                    if (filterCriteria.IsDeployedOnly)
                    {
                        await Shared.Stage.GetLastAsync(unitOfWork, model.LastIdeaStage);

                        // Guard Clause
                        if (model.LastIdeaStage.Stage == null)
                            continue;

                        if (model.LastIdeaStage.Stage.StageGroupId !=
                            Data.Core.Enumerators.StageGroup.n04_Deployed.ToString())
                            continue;

                    }

                    if (model.IsDraft
                        && !(includeMyDrafts && model.ProcessOwnerId == userId))
                        continue;

                    if (!(await controller.AuthorizeAsync(Policy.ReviewNewIdeas)).Succeeded 
                        && (model.LastIdeaStage.StageId == Data.Core.Enumerators.Stage.n00_Idea.ToString())
                        && userId != model.ProcessOwnerId)
                        continue;

                    if(model.LastIdeaStage != null)
                        await Shared.IdeaStatus.GetLastAsync(unitOfWork, model.LastIdeaStage);

                    if (model.LastIdeaStage == null
                        || model.LastIdeaStage.StageId == Data.Core.Enumerators.Stage.n00_Idea.ToString())
                    {
                        await model.GetIdeaScoreModalAsync();
                    }
                    else
                    {
                        await model.GetAutomationPotentialAsync();
                        model.GetBenefitPerEmployee_Hours();
                    }

                    modelsIdea.Add(model);
                }


                return modelsIdea;
            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
                throw;
            }

        }




        private static async Task<List<Idea>> GetIdeasAsync(Data.Core.IUnitOfWork unitOfWork,
                                                            string userId,
                                                            Data.Core.Domain.Business.Client tenant,
                                                            ViewModels.Business.Idea.FilterCriteria filterCriteria)
        {
            await unitOfWork.BusinessIdeas.GetForClientAsync(tenant);
            var ideas = tenant.Ideas;

            await unitOfWork.Users.GetProcessOwnerForAsync(ideas);

            switch (filterCriteria.UserRelationship)
            {
                case ViewModels.Business.Idea.UserRelationship.MyIdeas:
                    ideas = ideas.Where(x => x.ProcessOwnerId == userId).ToList();
                    break;

                case ViewModels.Business.Idea.UserRelationship.MyCollaborations:
                {
                    var userAuthorisations = await unitOfWork
                        .BusinessUserAuthorisations
                        .FindAsync(x => x.UserId == userId);

                    var ids = userAuthorisations.Select(x => x.IdeaId).Distinct();
                    ideas = ideas.Where(s => ids.Contains(s.Id)).ToList();
                    break;
                }
            }

            ideas = FilterByText(ideas, filterCriteria.FilterSearch);
            ideas = await FilterByStagesAsync(unitOfWork, ideas, filterCriteria);
            ideas = await FilterByStatusAsync(unitOfWork, ideas, filterCriteria);
            ideas = await FilterBySubmissionPathAsync(unitOfWork, ideas, filterCriteria);
            ideas = FilterByDepartment(ideas, filterCriteria);
            ideas = await FilterByTeamAsync(unitOfWork, ideas, filterCriteria);
            ideas = await FilterByVersionAsync(unitOfWork, ideas, filterCriteria);


            await unitOfWork.BusinessFollows.GetForIdeaAsync(ideas);
            await unitOfWork.BusinessVotes.GetForIdeaAsync(ideas);

            var modelsIdea = new List<Idea>();
            foreach (var core in ideas)
            {
                var model = new Idea(core)
                {
                    UnitOfWork = unitOfWork
                };
                await model.GetLikes(userId);
                modelsIdea.Add(model);
            }

            modelsIdea = SortBy(modelsIdea, filterCriteria);

            var count = modelsIdea.Count();
            filterCriteria.LastPage = count / filterCriteria.PageSize;

            if (count % filterCriteria.PageSize > 0)
                filterCriteria.LastPage++;

            return modelsIdea;
        }


        private static List<Data.Core.Domain.Business.Idea> FilterByText(List<Data.Core.Domain.Business.Idea> ideas,
                                                                  string text)
        {
            // Guard Clauses
            if (string.IsNullOrWhiteSpace(text))
                return ideas;

            if (ideas == null || ideas.Count == 0)
                return ideas;


            // Do the business
            var filteredIdeas = ideas.Where(x => 
                    (x.Name != null && x.Name.ToLower().IndexOf(text, StringComparison.Ordinal) > -1)
                    || (x.Summary != null && x.Summary.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.SubTitle != null && x.SubTitle.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.ActivityVolumeAverageComment != null && x.ActivityVolumeAverageComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.ApplicationStabilityComment != null && x.ApplicationStabilityComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.AverageEmployeeFullCostComment != null && x.AverageEmployeeFullCostComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.AverageErrorRateComment != null && x.AverageErrorRateComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.AverageNumberOfStepComment != null && x.AverageNumberOfStepComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.AverageProcessingTimeComment != null && x.AverageProcessingTimeComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.AverageReviewTimeComment != null && x.AverageReviewTimeComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.AverageReworkTimeComment != null && x.AverageReworkTimeComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.AverageWorkingDayComment != null && x.AverageWorkingDayComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.AverageWorkToBeReviewedComment != null && x.AverageWorkToBeReviewedComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.DataInputPercentOfStructuredComment != null && x.DataInputPercentOfStructuredComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.DataInputScannedComment != null && x.DataInputScannedComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.DecisionCountComment != null && x.DecisionCountComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.DecisionDifficultyComment != null && x.DecisionDifficultyComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.DocumentationPresentComment != null && x.DocumentationPresentComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.EmployeeCountComment != null && x.EmployeeCountComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.InputComment != null && x.InputComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.NegativeImpactComment != null && x.NegativeImpactComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.NumberOfWaysToCompleteProcessComment != null && x.NumberOfWaysToCompleteProcessComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.PainPointComment != null && x.PainPointComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.ProcessPeakComment != null && x.ProcessPeakComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.ProcessStabilityComment != null && x.ProcessStabilityComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.RatingComment != null && x.RatingComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.RuleComment != null && x.RuleComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.StructureComment != null && x.StructureComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.TaskFrequencyComment != null && x.TaskFrequencyComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.WorkingHourComment != null && x.WorkingHourComment.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.ProcessOwner?.FirstName != null && x.ProcessOwner?.FirstName.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                    || (x.ProcessOwner?.LastName != null && x.ProcessOwner?.LastName.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
            );

            if (filteredIdeas != null)
                return filteredIdeas.ToList();
            else
                return new List<Data.Core.Domain.Business.Idea>();
        }


        private static async Task<List<Data.Core.Domain.Business.Idea>> FilterByStagesAsync(Data.Core.IUnitOfWork unitOfWork, 
                                                                                     List<Data.Core.Domain.Business.Idea> ideas,
                                                                                     ViewModels.Business.Idea.FilterCriteria filterCriteria)
        {
            // Guard Clause
            if (filterCriteria.Stages == null
             || filterCriteria.Stages.Count == 0)
                return ideas;

            if (ideas == null
             || ideas.Count == 0)
                return ideas;


            // Do the business
            var ideasFiltered = new List<Data.Core.Domain.Business.Idea>();


            await unitOfWork.BusinessIdeaStages.GetForIdeaAsync(ideas);

            if (filterCriteria.IsDeployedOnly)
            {
                ideasFiltered = ideas.Where(x => x.IdeaStages.Any(y => y.StageId == "n7_Deployed")).ToList();
            }
            else
            {
                // Guard Clause
                if (filterCriteria.Stages == null
                 || filterCriteria.Stages.Count == 0)
                    return ideas;


                // Do the business
                foreach (var idea in ideas)
                {
                    //var lastStage = idea.IdeaStages.Last();
                    var model = new Idea(idea);
                    var lastStage = model.LastIdeaStage;

                    if (filterCriteria.Stages.FirstOrDefault(x => x.Id == lastStage.StageId) != null)
                    {
                        ideasFiltered.Add(idea);
                    }
                }
            }

            return ideasFiltered;
        }

        private static async Task<List<Data.Core.Domain.Business.Idea>> FilterByStatusAsync(Data.Core.IUnitOfWork unitOfWork,
                                                                                     List<Data.Core.Domain.Business.Idea> ideas,
                                                                                     ViewModels.Business.Idea.FilterCriteria filterCriteria)
        {
            // Guard Clause
            if (filterCriteria.Statuses == null
             || filterCriteria.Statuses.Count == 0)
                return ideas;


            if (ideas == null || ideas.Count == 0)
                return ideas;

            // Do the business
            var ideasFiltered = new List<Data.Core.Domain.Business.Idea>();

            await unitOfWork.BusinessIdeaStages.GetForIdeaAsync(ideas);


            foreach (var idea in ideas)
            {
                var model = new Idea(idea);
                var lastStage = model.LastIdeaStage;



                if (lastStage == null)
                    continue;

                // Get IdeaStageStatuses for ideaStage
                await unitOfWork.BusinessIdeaStageStatuses.GetForIdeaStageAsync(lastStage.GetCore());


                // Get last
                var ideaStageStatus = lastStage.IdeaStageStatuses
                                               .SingleOrDefault(x => x.CreatedDate == lastStage.IdeaStageStatuses.Max(y => y.CreatedDate));

                if(ideaStageStatus == null)
                    continue;

                await unitOfWork.SharedIdeaStatuses.GetStatusForAsync(ideaStageStatus.GetCore());

                if (filterCriteria.Statuses.Any(x => x.Name == ideaStageStatus.Status.Name))
                    ideasFiltered.Add(idea);
            }

            return ideasFiltered;
        }


        private static async Task<List<Data.Core.Domain.Business.Idea>> FilterBySubmissionPathAsync(Data.Core.IUnitOfWork unitOfWork,
                                                                                             List<Data.Core.Domain.Business.Idea> ideas,
                                                                                             ViewModels.Business.Idea.FilterCriteria filterCriteria)
        {
            // Guard Clauses
            if (filterCriteria.SubmissionPaths == null
             || filterCriteria.SubmissionPaths.Count == 0)
                return ideas;


            if (ideas == null || ideas.Count == 0)
                return ideas;


            await unitOfWork.SharedSubmissionPaths.GetSubmissionPathForAsync(ideas);

            var ideasFiltered = new List<Data.Core.Domain.Business.Idea>();
            foreach (var idea in ideas)
            {
                if (filterCriteria.SubmissionPaths.Any(x => x.Id == idea.SubmissionPathId))
                {
                    ideasFiltered.Add(idea);
                }
            }

            return ideasFiltered;
        }

        private static List<Data.Core.Domain.Business.Idea> FilterByDepartment(List<Data.Core.Domain.Business.Idea> ideas,
                                                                        ViewModels.Business.Idea.FilterCriteria filterCriteria)
        {
            // Guard Clauses
            if (filterCriteria.Departments == null
             || filterCriteria.Departments.Count == 0)
                return ideas;


            if (ideas == null || ideas.Count == 0)
                return ideas;


            var ideasFiltered = new List<Data.Core.Domain.Business.Idea>();

            foreach (var idea in ideas)
                if (filterCriteria.Departments.Any(x => x.Id == idea.DepartmentId))
                    ideasFiltered.Add(idea);

            return ideasFiltered;
        }


        private static async Task<List<Data.Core.Domain.Business.Idea>> FilterByTeamAsync(Data.Core.IUnitOfWork unitOfWork, 
                                                                                   List<Data.Core.Domain.Business.Idea> ideas,
                                                                                   ViewModels.Business.Idea.FilterCriteria filterCriteria)
        {
            // Guard Clauses
            if (filterCriteria.Teams == null
             || filterCriteria.Teams.Count == 0)
                return ideas;


            if (ideas == null || ideas.Count == 0)
                return ideas;

            var ideasFiltered = new List<Data.Core.Domain.Business.Idea>();
            foreach (var idea in ideas)
            {
                await unitOfWork.BusinessTeams.GetForDepartmentAsync(idea.Department);

                if (idea.Department.Teams.Any(team => filterCriteria.Teams.Any(x => x.Name == team.Name)))
                    ideasFiltered.Add(idea);
            }

            return ideasFiltered;
        }


        private static async Task<List<Data.Core.Domain.Business.Idea>> FilterByVersionAsync(Data.Core.IUnitOfWork unitOfWork, 
                                                                                      List<Data.Core.Domain.Business.Idea> ideas,
                                                                                      ViewModels.Business.Idea.FilterCriteria filterCriteria)
        {
            // Guard Clauses
            if (filterCriteria.Versions == null
             || filterCriteria.Versions.Count == 0)
                return ideas;




            if (ideas == null || ideas.Count == 0)
                return ideas;

            await unitOfWork.BusinessIdeaApplicationVersions.GetForIdeaAsync(ideas);
            var ideasFiltered = new List<Data.Core.Domain.Business.Idea>();


            foreach (var idea in ideas)
                foreach (var ideaApplicationVersion in idea.IdeaApplicationVersions)
                    if (filterCriteria.Versions.Any(x => x.Id == ideaApplicationVersion.VersionId))
                    {
                        ideasFiltered.Add(idea);
                        break;
                    }

            return ideasFiltered;
        }


        private static List<Idea> SortBy(List<Idea> ideas,
                                         ViewModels.Business.Idea.FilterCriteria filterCriteria)
        {
            if (ideas == null || ideas.Count == 0)
                return ideas;


            if (filterCriteria.SortById == "DateNewestFirst")
                return ideas.OrderByDescending(x => x.CreatedDate).ThenBy(x => x.Name).ToList();

            if (filterCriteria.SortById == "DateOldestFirst")
                return ideas.OrderBy(x => x.CreatedDate).ThenBy(x => x.Name).ToList();

            if (filterCriteria.SortById == "NameAtoZ")
                return ideas.OrderBy(x => x.Name).ToList();

            if (filterCriteria.SortById == "NameZtoA")
                return ideas.OrderByDescending(x => x.Name).ToList();

            if (filterCriteria.SortById == "OwnerAtoZ")
                return ideas.OrderBy(x => x.ProcessOwner.FirstName)
                            .ThenBy(x => x.ProcessOwner.LastName).ToList();

            if (filterCriteria.SortById == "OwnerZtoA")
                return ideas.OrderByDescending(x => x.ProcessOwner.FirstName)
                            .ThenBy(x => x.ProcessOwner.LastName).ToList();

            if (filterCriteria.SortById == "FollowedFirst")
                return ideas.OrderByDescending(x => x.IsFollowed).ThenBy(x => x.Name).ToList();

            if (filterCriteria.SortById == "LikedFirst")
                return ideas.OrderByDescending(x => x.IsVoted).ThenBy(x => x.Name).ToList();

            return ideas;
        }
    }
}
