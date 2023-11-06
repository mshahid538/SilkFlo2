using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Application;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using SilkFlo.Data.Core.Repositories;
using SilkFlo.Data.Core.Repositories.Agency;
using SilkFlo.Data.Core.Repositories.Application;
using SilkFlo.Data.Core.Repositories.Business;
using SilkFlo.Data.Core.Repositories.CRM;
using SilkFlo.Data.Core.Repositories.Shared;
using SilkFlo.Data.Core.Repositories.Shop;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        string UserId { get; set; }

        bool IncludeDeleted { get; set; }

        bool Commit { get; set; }

        IAnalyticRepository Analytics { get; }

        ILogRepository Logs { get; }

        IMessageRepository Messages { get; }

        SilkFlo.Data.Core.Repositories.IRoleRepository Roles { get; }

        IUserRepository Users { get; }

        IUserAchievementRepository UserAchievements { get; }

        IUserBadgeRepository UserBadges { get; }

        IUserRoleRepository UserRoles { get; }

        IWebHookLogRepository WebHookLogs { get; }

        IManageTenantRepository AgencyManageTenants { get; }

        IHotSpotRepository ApplicationHotSpots { get; }

        IPageRepository ApplicationPages { get; }

        ISettingRepository ApplicationSettings { get; }

        IApplicationRepository BusinessApplications { get; }

        IClientRepository BusinessClients { get; }

        ICollaboratorRepository BusinessCollaborators { get; }

        ICollaboratorRoleRepository BusinessCollaboratorRoles { get; }

        ICommentRepository BusinessComments { get; }

        IDepartmentRepository BusinessDepartments { get; }

        IDocumentRepository BusinessDocuments { get; }

        IFollowRepository BusinessFollows { get; }

        IIdeaRepository BusinessIdeas { get; }

        IIdeaApplicationVersionRepository BusinessIdeaApplicationVersions { get; }

        IIdeaOtherRunningCostRepository BusinessIdeaOtherRunningCosts { get; }

        IIdeaRunningCostRepository BusinessIdeaRunningCosts { get; }

        IIdeaStageRepository BusinessIdeaStages { get; }

        IIdeaStageStatusRepository BusinessIdeaStageStatuses { get; }

        IImplementationCostRepository BusinessImplementationCosts { get; }

        ILocationRepository BusinessLocations { get; }

        IOtherRunningCostRepository BusinessOtherRunningCosts { get; }

        IProcessRepository BusinessProcesses { get; }

        IRecipientRepository BusinessRecipients { get; }

        SilkFlo.Data.Core.Repositories.Business.IRoleRepository BusinessRoles { get; }

        IRoleCostRepository BusinessRoleCosts { get; }

        IRoleIdeaAuthorisationRepository BusinessRoleIdeaAuthorisations { get; }

        IRunningCostRepository BusinessRunningCosts { get; }

        ISoftwareVenderRepository BusinessSoftwareVenders { get; }

        ITeamRepository BusinessTeams { get; }

        IUserAuthorisationRepository BusinessUserAuthorisations { get; }

        IVersionRepository BusinessVersions { get; }

        IVoteRepository BusinessVotes { get; }

        ICompanySizeRepository CRMCompanySizes { get; }

        IJobLevelRepository CRMJobLevels { get; }

        IProspectRepository CRMProspects { get; }

        IAchievementRepository SharedAchievements { get; }

        IApplicationStabilityRepository SharedApplicationStabilities { get; }

        IAutomationGoalRepository SharedAutomationGoals { get; }

        IAutomationTypeRepository SharedAutomationTypes { get; }

        IAverageNumberOfStepRepository SharedAverageNumberOfSteps { get; }

        IBadgeRepository SharedBadges { get; }

        IClientTypeRepository SharedClientTypes { get; }

        ICostTypeRepository SharedCostTypes { get; }

        ICountryRepository SharedCountries { get; }

        IDataInputPercentOfStructuredRepository SharedDataInputPercentOfStructureds { get; }

        IDecisionCountRepository SharedDecisionCounts { get; }

        IDecisionDifficultyRepository SharedDecisionDifficulties { get; }

        IDocumentationPresentRepository SharedDocumentationPresents { get; }

        IIdeaAuthorisationRepository SharedIdeaAuthorisations { get; }

        IIdeaStatusRepository SharedIdeaStatuses { get; }

        IIndustryRepository SharedIndustries { get; }

        IInputRepository SharedInputs { get; }

        IInputDataStructureRepository SharedInputDataStructures { get; }

        ILanguageRepository SharedLanguages { get; }

        INumberOfWaysToCompleteProcessRepository SharedNumberOfWaysToCompleteProcesses { get; }

        IPeriodRepository SharedPeriods { get; }

        IProcessPeakRepository SharedProcessPeaks { get; }

        IProcessStabilityRepository SharedProcessStabilities { get; }

        IRuleRepository SharedRules { get; }

        IStageRepository SharedStages { get; }

        IStageGroupRepository SharedStageGroups { get; }

        ISubmissionPathRepository SharedSubmissionPaths { get; }

        ITaskFrequencyRepository SharedTaskFrequencies { get; }

        ICouponRepository ShopCoupons { get; }

        ICurrencyRepository ShopCurrencies { get; }

        IDiscountRepository ShopDiscounts { get; }

        IPriceRepository ShopPrices { get; }

        IProductRepository ShopProducts { get; }

        ISubscriptionRepository ShopSubscriptions { get; }

        void Add(Analytic core);

        Task AddAsync(Analytic core);

        void Add(IEnumerable<Analytic> cores);

        Task AddAsync(IEnumerable<Analytic> cores);

        void GetCreatedUpdated(IEnumerable<Analytic> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Analytic> cores);

        void GetCreatedUpdated(Analytic core);

        Task GetCreatedUpdatedAsync(Analytic core);

        void Add(SilkFlo.Data.Core.Domain.Log core);

        Task AddAsync(SilkFlo.Data.Core.Domain.Log core);

        void Add(IEnumerable<SilkFlo.Data.Core.Domain.Log> cores);

        Task AddAsync(IEnumerable<SilkFlo.Data.Core.Domain.Log> cores);

        void GetCreatedUpdated(IEnumerable<SilkFlo.Data.Core.Domain.Log> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<SilkFlo.Data.Core.Domain.Log> cores);

        void GetCreatedUpdated(SilkFlo.Data.Core.Domain.Log core);

        Task GetCreatedUpdatedAsync(SilkFlo.Data.Core.Domain.Log core);

        void Add(Message core);

        Task AddAsync(Message core);

        void Add(IEnumerable<Message> cores);

        Task AddAsync(IEnumerable<Message> cores);

        void GetCreatedUpdated(IEnumerable<Message> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Message> cores);

        void GetCreatedUpdated(Message core);

        Task GetCreatedUpdatedAsync(Message core);

        void Add(SilkFlo.Data.Core.Domain.Role core);

        Task AddAsync(SilkFlo.Data.Core.Domain.Role core);

        void Add(IEnumerable<SilkFlo.Data.Core.Domain.Role> cores);

        Task AddAsync(IEnumerable<SilkFlo.Data.Core.Domain.Role> cores);

        void GetCreatedUpdated(IEnumerable<SilkFlo.Data.Core.Domain.Role> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<SilkFlo.Data.Core.Domain.Role> cores);

        void GetCreatedUpdated(SilkFlo.Data.Core.Domain.Role core);

        Task GetCreatedUpdatedAsync(SilkFlo.Data.Core.Domain.Role core);

        void Add(User core);

        Task AddAsync(User core);

        void Add(IEnumerable<User> cores);

        Task AddAsync(IEnumerable<User> cores);

        void GetCreatedUpdated(IEnumerable<User> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<User> cores);

        void GetCreatedUpdated(User core);

        Task GetCreatedUpdatedAsync(User core);

        void Add(UserAchievement core);

        Task AddAsync(UserAchievement core);

        void Add(IEnumerable<UserAchievement> cores);

        Task AddAsync(IEnumerable<UserAchievement> cores);

        void GetCreatedUpdated(IEnumerable<UserAchievement> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<UserAchievement> cores);

        void GetCreatedUpdated(UserAchievement core);

        Task GetCreatedUpdatedAsync(UserAchievement core);

        void Add(UserBadge core);

        Task AddAsync(UserBadge core);

        void Add(IEnumerable<UserBadge> cores);

        Task AddAsync(IEnumerable<UserBadge> cores);

        void GetCreatedUpdated(IEnumerable<UserBadge> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<UserBadge> cores);

        void GetCreatedUpdated(UserBadge core);

        Task GetCreatedUpdatedAsync(UserBadge core);

        void Add(UserRole core);

        Task AddAsync(UserRole core);

        void Add(IEnumerable<UserRole> cores);

        Task AddAsync(IEnumerable<UserRole> cores);

        void GetCreatedUpdated(IEnumerable<UserRole> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<UserRole> cores);

        void GetCreatedUpdated(UserRole core);

        Task GetCreatedUpdatedAsync(UserRole core);

        void Add(WebHookLog core);

        Task AddAsync(WebHookLog core);

        void Add(IEnumerable<WebHookLog> cores);

        Task AddAsync(IEnumerable<WebHookLog> cores);

        void GetCreatedUpdated(IEnumerable<WebHookLog> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<WebHookLog> cores);

        void GetCreatedUpdated(WebHookLog core);

        Task GetCreatedUpdatedAsync(WebHookLog core);

        void Add(ManageTenant core);

        Task AddAsync(ManageTenant core);

        void Add(IEnumerable<ManageTenant> cores);

        Task AddAsync(IEnumerable<ManageTenant> cores);

        void GetCreatedUpdated(IEnumerable<ManageTenant> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<ManageTenant> cores);

        void GetCreatedUpdated(ManageTenant core);

        Task GetCreatedUpdatedAsync(ManageTenant core);

        void Add(HotSpot core);

        Task AddAsync(HotSpot core);

        void Add(IEnumerable<HotSpot> cores);

        Task AddAsync(IEnumerable<HotSpot> cores);

        void GetCreatedUpdated(IEnumerable<HotSpot> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<HotSpot> cores);

        void GetCreatedUpdated(HotSpot core);

        Task GetCreatedUpdatedAsync(HotSpot core);

        void Add(Page core);

        Task AddAsync(Page core);

        void Add(IEnumerable<Page> cores);

        Task AddAsync(IEnumerable<Page> cores);

        void GetCreatedUpdated(IEnumerable<Page> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Page> cores);

        void GetCreatedUpdated(Page core);

        Task GetCreatedUpdatedAsync(Page core);

        void Add(Setting core);

        Task AddAsync(Setting core);

        void Add(IEnumerable<Setting> cores);

        Task AddAsync(IEnumerable<Setting> cores);

        void GetCreatedUpdated(IEnumerable<Setting> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Setting> cores);

        void GetCreatedUpdated(Setting core);

        Task GetCreatedUpdatedAsync(Setting core);

        void Add(SilkFlo.Data.Core.Domain.Business.Application core);

        Task AddAsync(SilkFlo.Data.Core.Domain.Business.Application core);

        void Add(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> cores);

        Task AddAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> cores);

        void GetCreatedUpdated(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> cores);

        void GetCreatedUpdated(SilkFlo.Data.Core.Domain.Business.Application core);

        Task GetCreatedUpdatedAsync(SilkFlo.Data.Core.Domain.Business.Application core);

        void Add(Client core);

        Task AddAsync(Client core);

        void Add(IEnumerable<Client> cores);

        Task AddAsync(IEnumerable<Client> cores);

        void GetCreatedUpdated(IEnumerable<Client> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Client> cores);

        void GetCreatedUpdated(Client core);

        Task GetCreatedUpdatedAsync(Client core);

        void Add(Collaborator core);

        Task AddAsync(Collaborator core);

        void Add(IEnumerable<Collaborator> cores);

        Task AddAsync(IEnumerable<Collaborator> cores);

        void GetCreatedUpdated(IEnumerable<Collaborator> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Collaborator> cores);

        void GetCreatedUpdated(Collaborator core);

        Task GetCreatedUpdatedAsync(Collaborator core);

        void Add(CollaboratorRole core);

        Task AddAsync(CollaboratorRole core);

        void Add(IEnumerable<CollaboratorRole> cores);

        Task AddAsync(IEnumerable<CollaboratorRole> cores);

        void GetCreatedUpdated(IEnumerable<CollaboratorRole> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<CollaboratorRole> cores);

        void GetCreatedUpdated(CollaboratorRole core);

        Task GetCreatedUpdatedAsync(CollaboratorRole core);

        void Add(Comment core);

        Task AddAsync(Comment core);

        void Add(IEnumerable<Comment> cores);

        Task AddAsync(IEnumerable<Comment> cores);

        void GetCreatedUpdated(IEnumerable<Comment> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Comment> cores);

        void GetCreatedUpdated(Comment core);

        Task GetCreatedUpdatedAsync(Comment core);

        void Add(Department core);

        Task AddAsync(Department core);

        void Add(IEnumerable<Department> cores);

        Task AddAsync(IEnumerable<Department> cores);

        void GetCreatedUpdated(IEnumerable<Department> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Department> cores);

        void GetCreatedUpdated(Department core);

        Task GetCreatedUpdatedAsync(Department core);

        void Add(Document core);

        Task AddAsync(Document core);

        void Add(IEnumerable<Document> cores);

        Task AddAsync(IEnumerable<Document> cores);

        void GetCreatedUpdated(IEnumerable<Document> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Document> cores);

        void GetCreatedUpdated(Document core);

        Task GetCreatedUpdatedAsync(Document core);

        void Add(Follow core);

        Task AddAsync(Follow core);

        void Add(IEnumerable<Follow> cores);

        Task AddAsync(IEnumerable<Follow> cores);

        void GetCreatedUpdated(IEnumerable<Follow> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Follow> cores);

        void GetCreatedUpdated(Follow core);

        Task GetCreatedUpdatedAsync(Follow core);

        void Add(Idea core);

        Task AddAsync(Idea core);

        void Add(IEnumerable<Idea> cores);

        Task AddAsync(IEnumerable<Idea> cores);

        void GetCreatedUpdated(IEnumerable<Idea> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Idea> cores);

        void GetCreatedUpdated(Idea core);

        Task GetCreatedUpdatedAsync(Idea core);

        void Add(IdeaApplicationVersion core);

        Task AddAsync(IdeaApplicationVersion core);

        void Add(IEnumerable<IdeaApplicationVersion> cores);

        Task AddAsync(IEnumerable<IdeaApplicationVersion> cores);

        void GetCreatedUpdated(IEnumerable<IdeaApplicationVersion> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<IdeaApplicationVersion> cores);

        void GetCreatedUpdated(IdeaApplicationVersion core);

        Task GetCreatedUpdatedAsync(IdeaApplicationVersion core);

        void Add(IdeaOtherRunningCost core);

        Task AddAsync(IdeaOtherRunningCost core);

        void Add(IEnumerable<IdeaOtherRunningCost> cores);

        Task AddAsync(IEnumerable<IdeaOtherRunningCost> cores);

        void GetCreatedUpdated(IEnumerable<IdeaOtherRunningCost> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<IdeaOtherRunningCost> cores);

        void GetCreatedUpdated(IdeaOtherRunningCost core);

        Task GetCreatedUpdatedAsync(IdeaOtherRunningCost core);

        void Add(IdeaRunningCost core);

        Task AddAsync(IdeaRunningCost core);

        void Add(IEnumerable<IdeaRunningCost> cores);

        Task AddAsync(IEnumerable<IdeaRunningCost> cores);

        void GetCreatedUpdated(IEnumerable<IdeaRunningCost> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<IdeaRunningCost> cores);

        void GetCreatedUpdated(IdeaRunningCost core);

        Task GetCreatedUpdatedAsync(IdeaRunningCost core);

        void Add(IdeaStage core);

        Task AddAsync(IdeaStage core);

        void Add(IEnumerable<IdeaStage> cores);

        Task AddAsync(IEnumerable<IdeaStage> cores);

        void GetCreatedUpdated(IEnumerable<IdeaStage> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<IdeaStage> cores);

        void GetCreatedUpdated(IdeaStage core);

        Task GetCreatedUpdatedAsync(IdeaStage core);

        void Add(IdeaStageStatus core);

        Task AddAsync(IdeaStageStatus core);

        void Add(IEnumerable<IdeaStageStatus> cores);

        Task AddAsync(IEnumerable<IdeaStageStatus> cores);

        void GetCreatedUpdated(IEnumerable<IdeaStageStatus> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<IdeaStageStatus> cores);

        void GetCreatedUpdated(IdeaStageStatus core);

        Task GetCreatedUpdatedAsync(IdeaStageStatus core);

        void Add(ImplementationCost core);

        Task AddAsync(ImplementationCost core);

        void Add(IEnumerable<ImplementationCost> cores);

        Task AddAsync(IEnumerable<ImplementationCost> cores);

        void GetCreatedUpdated(IEnumerable<ImplementationCost> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<ImplementationCost> cores);

        void GetCreatedUpdated(ImplementationCost core);

        Task GetCreatedUpdatedAsync(ImplementationCost core);

        void Add(Location core);

        Task AddAsync(Location core);

        void Add(IEnumerable<Location> cores);

        Task AddAsync(IEnumerable<Location> cores);

        void GetCreatedUpdated(IEnumerable<Location> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Location> cores);

        void GetCreatedUpdated(Location core);

        Task GetCreatedUpdatedAsync(Location core);

        void Add(OtherRunningCost core);

        Task AddAsync(OtherRunningCost core);

        void Add(IEnumerable<OtherRunningCost> cores);

        Task AddAsync(IEnumerable<OtherRunningCost> cores);

        void GetCreatedUpdated(IEnumerable<OtherRunningCost> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<OtherRunningCost> cores);

        void GetCreatedUpdated(OtherRunningCost core);

        Task GetCreatedUpdatedAsync(OtherRunningCost core);

        void Add(Process core);

        Task AddAsync(Process core);

        void Add(IEnumerable<Process> cores);

        Task AddAsync(IEnumerable<Process> cores);

        void GetCreatedUpdated(IEnumerable<Process> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Process> cores);

        void GetCreatedUpdated(Process core);

        Task GetCreatedUpdatedAsync(Process core);

        void Add(Recipient core);

        Task AddAsync(Recipient core);

        void Add(IEnumerable<Recipient> cores);

        Task AddAsync(IEnumerable<Recipient> cores);

        void GetCreatedUpdated(IEnumerable<Recipient> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Recipient> cores);

        void GetCreatedUpdated(Recipient core);

        Task GetCreatedUpdatedAsync(Recipient core);

        void Add(SilkFlo.Data.Core.Domain.Business.BusinessRole core);

        Task AddAsync(SilkFlo.Data.Core.Domain.Business.BusinessRole core);

        void Add(IEnumerable<SilkFlo.Data.Core.Domain.Business.BusinessRole> cores);

        Task AddAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.BusinessRole> cores);

        void GetCreatedUpdated(IEnumerable<SilkFlo.Data.Core.Domain.Business.BusinessRole> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.BusinessRole> cores);

        void GetCreatedUpdated(SilkFlo.Data.Core.Domain.Business.BusinessRole core);

        Task GetCreatedUpdatedAsync(SilkFlo.Data.Core.Domain.Business.BusinessRole core);

        void Add(RoleCost core);

        Task AddAsync(RoleCost core);

        void Add(IEnumerable<RoleCost> cores);

        Task AddAsync(IEnumerable<RoleCost> cores);

        void GetCreatedUpdated(IEnumerable<RoleCost> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<RoleCost> cores);

        void GetCreatedUpdated(RoleCost core);

        Task GetCreatedUpdatedAsync(RoleCost core);

        void Add(RoleIdeaAuthorisation core);

        Task AddAsync(RoleIdeaAuthorisation core);

        void Add(IEnumerable<RoleIdeaAuthorisation> cores);

        Task AddAsync(IEnumerable<RoleIdeaAuthorisation> cores);

        void GetCreatedUpdated(IEnumerable<RoleIdeaAuthorisation> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<RoleIdeaAuthorisation> cores);

        void GetCreatedUpdated(RoleIdeaAuthorisation core);

        Task GetCreatedUpdatedAsync(RoleIdeaAuthorisation core);

        void Add(RunningCost core);

        Task AddAsync(RunningCost core);

        void Add(IEnumerable<RunningCost> cores);

        Task AddAsync(IEnumerable<RunningCost> cores);

        void GetCreatedUpdated(IEnumerable<RunningCost> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<RunningCost> cores);

        void GetCreatedUpdated(RunningCost core);

        Task GetCreatedUpdatedAsync(RunningCost core);

        void Add(SoftwareVender core);

        Task AddAsync(SoftwareVender core);

        void Add(IEnumerable<SoftwareVender> cores);

        Task AddAsync(IEnumerable<SoftwareVender> cores);

        void GetCreatedUpdated(IEnumerable<SoftwareVender> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<SoftwareVender> cores);

        void GetCreatedUpdated(SoftwareVender core);

        Task GetCreatedUpdatedAsync(SoftwareVender core);

        void Add(Team core);

        Task AddAsync(Team core);

        void Add(IEnumerable<Team> cores);

        Task AddAsync(IEnumerable<Team> cores);

        void GetCreatedUpdated(IEnumerable<Team> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Team> cores);

        void GetCreatedUpdated(Team core);

        Task GetCreatedUpdatedAsync(Team core);

        void Add(UserAuthorisation core);

        Task AddAsync(UserAuthorisation core);

        void Add(IEnumerable<UserAuthorisation> cores);

        Task AddAsync(IEnumerable<UserAuthorisation> cores);

        void GetCreatedUpdated(IEnumerable<UserAuthorisation> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<UserAuthorisation> cores);

        void GetCreatedUpdated(UserAuthorisation core);

        Task GetCreatedUpdatedAsync(UserAuthorisation core);

        void Add(SilkFlo.Data.Core.Domain.Business.Version core);

        Task AddAsync(SilkFlo.Data.Core.Domain.Business.Version core);

        void Add(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> cores);

        Task AddAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> cores);

        void GetCreatedUpdated(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> cores);

        void GetCreatedUpdated(SilkFlo.Data.Core.Domain.Business.Version core);

        Task GetCreatedUpdatedAsync(SilkFlo.Data.Core.Domain.Business.Version core);

        void Add(Vote core);

        Task AddAsync(Vote core);

        void Add(IEnumerable<Vote> cores);

        Task AddAsync(IEnumerable<Vote> cores);

        void GetCreatedUpdated(IEnumerable<Vote> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Vote> cores);

        void GetCreatedUpdated(Vote core);

        Task GetCreatedUpdatedAsync(Vote core);

        void Add(CompanySize core);

        Task AddAsync(CompanySize core);

        void Add(IEnumerable<CompanySize> cores);

        Task AddAsync(IEnumerable<CompanySize> cores);

        void GetCreatedUpdated(IEnumerable<CompanySize> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<CompanySize> cores);

        void GetCreatedUpdated(CompanySize core);

        Task GetCreatedUpdatedAsync(CompanySize core);

        void Add(JobLevel core);

        Task AddAsync(JobLevel core);

        void Add(IEnumerable<JobLevel> cores);

        Task AddAsync(IEnumerable<JobLevel> cores);

        void GetCreatedUpdated(IEnumerable<JobLevel> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<JobLevel> cores);

        void GetCreatedUpdated(JobLevel core);

        Task GetCreatedUpdatedAsync(JobLevel core);

        void Add(Prospect core);

        Task AddAsync(Prospect core);

        void Add(IEnumerable<Prospect> cores);

        Task AddAsync(IEnumerable<Prospect> cores);

        void GetCreatedUpdated(IEnumerable<Prospect> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Prospect> cores);

        void GetCreatedUpdated(Prospect core);

        Task GetCreatedUpdatedAsync(Prospect core);

        void Add(Achievement core);

        Task AddAsync(Achievement core);

        void Add(IEnumerable<Achievement> cores);

        Task AddAsync(IEnumerable<Achievement> cores);

        void GetCreatedUpdated(IEnumerable<Achievement> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Achievement> cores);

        void GetCreatedUpdated(Achievement core);

        Task GetCreatedUpdatedAsync(Achievement core);

        void Add(ApplicationStability core);

        Task AddAsync(ApplicationStability core);

        void Add(IEnumerable<ApplicationStability> cores);

        Task AddAsync(IEnumerable<ApplicationStability> cores);

        void GetCreatedUpdated(IEnumerable<ApplicationStability> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<ApplicationStability> cores);

        void GetCreatedUpdated(ApplicationStability core);

        Task GetCreatedUpdatedAsync(ApplicationStability core);

        void Add(AutomationGoal core);

        Task AddAsync(AutomationGoal core);

        void Add(IEnumerable<AutomationGoal> cores);

        Task AddAsync(IEnumerable<AutomationGoal> cores);

        void GetCreatedUpdated(IEnumerable<AutomationGoal> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<AutomationGoal> cores);

        void GetCreatedUpdated(AutomationGoal core);

        Task GetCreatedUpdatedAsync(AutomationGoal core);

        void Add(AutomationType core);

        Task AddAsync(AutomationType core);

        void Add(IEnumerable<AutomationType> cores);

        Task AddAsync(IEnumerable<AutomationType> cores);

        void GetCreatedUpdated(IEnumerable<AutomationType> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<AutomationType> cores);

        void GetCreatedUpdated(AutomationType core);

        Task GetCreatedUpdatedAsync(AutomationType core);

        void Add(AverageNumberOfStep core);

        Task AddAsync(AverageNumberOfStep core);

        void Add(IEnumerable<AverageNumberOfStep> cores);

        Task AddAsync(IEnumerable<AverageNumberOfStep> cores);

        void GetCreatedUpdated(IEnumerable<AverageNumberOfStep> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<AverageNumberOfStep> cores);

        void GetCreatedUpdated(AverageNumberOfStep core);

        Task GetCreatedUpdatedAsync(AverageNumberOfStep core);

        void Add(Badge core);

        Task AddAsync(Badge core);

        void Add(IEnumerable<Badge> cores);

        Task AddAsync(IEnumerable<Badge> cores);

        void GetCreatedUpdated(IEnumerable<Badge> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Badge> cores);

        void GetCreatedUpdated(Badge core);

        Task GetCreatedUpdatedAsync(Badge core);

        void Add(ClientType core);

        Task AddAsync(ClientType core);

        void Add(IEnumerable<ClientType> cores);

        Task AddAsync(IEnumerable<ClientType> cores);

        void GetCreatedUpdated(IEnumerable<ClientType> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<ClientType> cores);

        void GetCreatedUpdated(ClientType core);

        Task GetCreatedUpdatedAsync(ClientType core);

        void Add(CostType core);

        Task AddAsync(CostType core);

        void Add(IEnumerable<CostType> cores);

        Task AddAsync(IEnumerable<CostType> cores);

        void GetCreatedUpdated(IEnumerable<CostType> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<CostType> cores);

        void GetCreatedUpdated(CostType core);

        Task GetCreatedUpdatedAsync(CostType core);

        void Add(Country core);

        Task AddAsync(Country core);

        void Add(IEnumerable<Country> cores);

        Task AddAsync(IEnumerable<Country> cores);

        void GetCreatedUpdated(IEnumerable<Country> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Country> cores);

        void GetCreatedUpdated(Country core);

        Task GetCreatedUpdatedAsync(Country core);

        void Add(DataInputPercentOfStructured core);

        Task AddAsync(DataInputPercentOfStructured core);

        void Add(IEnumerable<DataInputPercentOfStructured> cores);

        Task AddAsync(IEnumerable<DataInputPercentOfStructured> cores);

        void GetCreatedUpdated(IEnumerable<DataInputPercentOfStructured> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<DataInputPercentOfStructured> cores);

        void GetCreatedUpdated(DataInputPercentOfStructured core);

        Task GetCreatedUpdatedAsync(DataInputPercentOfStructured core);

        void Add(DecisionCount core);

        Task AddAsync(DecisionCount core);

        void Add(IEnumerable<DecisionCount> cores);

        Task AddAsync(IEnumerable<DecisionCount> cores);

        void GetCreatedUpdated(IEnumerable<DecisionCount> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<DecisionCount> cores);

        void GetCreatedUpdated(DecisionCount core);

        Task GetCreatedUpdatedAsync(DecisionCount core);

        void Add(DecisionDifficulty core);

        Task AddAsync(DecisionDifficulty core);

        void Add(IEnumerable<DecisionDifficulty> cores);

        Task AddAsync(IEnumerable<DecisionDifficulty> cores);

        void GetCreatedUpdated(IEnumerable<DecisionDifficulty> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<DecisionDifficulty> cores);

        void GetCreatedUpdated(DecisionDifficulty core);

        Task GetCreatedUpdatedAsync(DecisionDifficulty core);

        void Add(DocumentationPresent core);

        Task AddAsync(DocumentationPresent core);

        void Add(IEnumerable<DocumentationPresent> cores);

        Task AddAsync(IEnumerable<DocumentationPresent> cores);

        void GetCreatedUpdated(IEnumerable<DocumentationPresent> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<DocumentationPresent> cores);

        void GetCreatedUpdated(DocumentationPresent core);

        Task GetCreatedUpdatedAsync(DocumentationPresent core);

        void Add(IdeaAuthorisation core);

        Task AddAsync(IdeaAuthorisation core);

        void Add(IEnumerable<IdeaAuthorisation> cores);

        Task AddAsync(IEnumerable<IdeaAuthorisation> cores);

        void GetCreatedUpdated(IEnumerable<IdeaAuthorisation> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<IdeaAuthorisation> cores);

        void GetCreatedUpdated(IdeaAuthorisation core);

        Task GetCreatedUpdatedAsync(IdeaAuthorisation core);

        void Add(IdeaStatus core);

        Task AddAsync(IdeaStatus core);

        void Add(IEnumerable<IdeaStatus> cores);

        Task AddAsync(IEnumerable<IdeaStatus> cores);

        void GetCreatedUpdated(IEnumerable<IdeaStatus> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<IdeaStatus> cores);

        void GetCreatedUpdated(IdeaStatus core);

        Task GetCreatedUpdatedAsync(IdeaStatus core);

        void Add(Industry core);

        Task AddAsync(Industry core);

        void Add(IEnumerable<Industry> cores);

        Task AddAsync(IEnumerable<Industry> cores);

        void GetCreatedUpdated(IEnumerable<Industry> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Industry> cores);

        void GetCreatedUpdated(Industry core);

        Task GetCreatedUpdatedAsync(Industry core);

        void Add(Input core);

        Task AddAsync(Input core);

        void Add(IEnumerable<Input> cores);

        Task AddAsync(IEnumerable<Input> cores);

        void GetCreatedUpdated(IEnumerable<Input> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Input> cores);

        void GetCreatedUpdated(Input core);

        Task GetCreatedUpdatedAsync(Input core);

        void Add(InputDataStructure core);

        Task AddAsync(InputDataStructure core);

        void Add(IEnumerable<InputDataStructure> cores);

        Task AddAsync(IEnumerable<InputDataStructure> cores);

        void GetCreatedUpdated(IEnumerable<InputDataStructure> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<InputDataStructure> cores);

        void GetCreatedUpdated(InputDataStructure core);

        Task GetCreatedUpdatedAsync(InputDataStructure core);

        void Add(Language core);

        Task AddAsync(Language core);

        void Add(IEnumerable<Language> cores);

        Task AddAsync(IEnumerable<Language> cores);

        void GetCreatedUpdated(IEnumerable<Language> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Language> cores);

        void GetCreatedUpdated(Language core);

        Task GetCreatedUpdatedAsync(Language core);

        void Add(NumberOfWaysToCompleteProcess core);

        Task AddAsync(NumberOfWaysToCompleteProcess core);

        void Add(IEnumerable<NumberOfWaysToCompleteProcess> cores);

        Task AddAsync(IEnumerable<NumberOfWaysToCompleteProcess> cores);

        void GetCreatedUpdated(IEnumerable<NumberOfWaysToCompleteProcess> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<NumberOfWaysToCompleteProcess> cores);

        void GetCreatedUpdated(NumberOfWaysToCompleteProcess core);

        Task GetCreatedUpdatedAsync(NumberOfWaysToCompleteProcess core);

        void Add(Period core);

        Task AddAsync(Period core);

        void Add(IEnumerable<Period> cores);

        Task AddAsync(IEnumerable<Period> cores);

        void GetCreatedUpdated(IEnumerable<Period> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Period> cores);

        void GetCreatedUpdated(Period core);

        Task GetCreatedUpdatedAsync(Period core);

        void Add(ProcessPeak core);

        Task AddAsync(ProcessPeak core);

        void Add(IEnumerable<ProcessPeak> cores);

        Task AddAsync(IEnumerable<ProcessPeak> cores);

        void GetCreatedUpdated(IEnumerable<ProcessPeak> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<ProcessPeak> cores);

        void GetCreatedUpdated(ProcessPeak core);

        Task GetCreatedUpdatedAsync(ProcessPeak core);

        void Add(ProcessStability core);

        Task AddAsync(ProcessStability core);

        void Add(IEnumerable<ProcessStability> cores);

        Task AddAsync(IEnumerable<ProcessStability> cores);

        void GetCreatedUpdated(IEnumerable<ProcessStability> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<ProcessStability> cores);

        void GetCreatedUpdated(ProcessStability core);

        Task GetCreatedUpdatedAsync(ProcessStability core);

        void Add(Rule core);

        Task AddAsync(Rule core);

        void Add(IEnumerable<Rule> cores);

        Task AddAsync(IEnumerable<Rule> cores);

        void GetCreatedUpdated(IEnumerable<Rule> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Rule> cores);

        void GetCreatedUpdated(Rule core);

        Task GetCreatedUpdatedAsync(Rule core);

        void Add(Stage core);

        Task AddAsync(Stage core);

        void Add(IEnumerable<Stage> cores);

        Task AddAsync(IEnumerable<Stage> cores);

        void GetCreatedUpdated(IEnumerable<Stage> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Stage> cores);

        void GetCreatedUpdated(Stage core);

        Task GetCreatedUpdatedAsync(Stage core);

        void Add(StageGroup core);

        Task AddAsync(StageGroup core);

        void Add(IEnumerable<StageGroup> cores);

        Task AddAsync(IEnumerable<StageGroup> cores);

        void GetCreatedUpdated(IEnumerable<StageGroup> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<StageGroup> cores);

        void GetCreatedUpdated(StageGroup core);

        Task GetCreatedUpdatedAsync(StageGroup core);

        void Add(SubmissionPath core);

        Task AddAsync(SubmissionPath core);

        void Add(IEnumerable<SubmissionPath> cores);

        Task AddAsync(IEnumerable<SubmissionPath> cores);

        void GetCreatedUpdated(IEnumerable<SubmissionPath> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<SubmissionPath> cores);

        void GetCreatedUpdated(SubmissionPath core);

        Task GetCreatedUpdatedAsync(SubmissionPath core);

        void Add(TaskFrequency core);

        Task AddAsync(TaskFrequency core);

        void Add(IEnumerable<TaskFrequency> cores);

        Task AddAsync(IEnumerable<TaskFrequency> cores);

        void GetCreatedUpdated(IEnumerable<TaskFrequency> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<TaskFrequency> cores);

        void GetCreatedUpdated(TaskFrequency core);

        Task GetCreatedUpdatedAsync(TaskFrequency core);

        void Add(Coupon core);

        Task AddAsync(Coupon core);

        void Add(IEnumerable<Coupon> cores);

        Task AddAsync(IEnumerable<Coupon> cores);

        void GetCreatedUpdated(IEnumerable<Coupon> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Coupon> cores);

        void GetCreatedUpdated(Coupon core);

        Task GetCreatedUpdatedAsync(Coupon core);

        void Add(Currency core);

        Task AddAsync(Currency core);

        void Add(IEnumerable<Currency> cores);

        Task AddAsync(IEnumerable<Currency> cores);

        void GetCreatedUpdated(IEnumerable<Currency> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Currency> cores);

        void GetCreatedUpdated(Currency core);

        Task GetCreatedUpdatedAsync(Currency core);

        void Add(Discount core);

        Task AddAsync(Discount core);

        void Add(IEnumerable<Discount> cores);

        Task AddAsync(IEnumerable<Discount> cores);

        void GetCreatedUpdated(IEnumerable<Discount> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Discount> cores);

        void GetCreatedUpdated(Discount core);

        Task GetCreatedUpdatedAsync(Discount core);

        void Add(Price core);

        Task AddAsync(Price core);

        void Add(IEnumerable<Price> cores);

        Task AddAsync(IEnumerable<Price> cores);

        void GetCreatedUpdated(IEnumerable<Price> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Price> cores);

        void GetCreatedUpdated(Price core);

        Task GetCreatedUpdatedAsync(Price core);

        void Add(Product core);

        Task AddAsync(Product core);

        void Add(IEnumerable<Product> cores);

        Task AddAsync(IEnumerable<Product> cores);

        void GetCreatedUpdated(IEnumerable<Product> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Product> cores);

        void GetCreatedUpdated(Product core);

        Task GetCreatedUpdatedAsync(Product core);

        void Add(Subscription core);

        Task AddAsync(Subscription core);

        void Add(IEnumerable<Subscription> cores);

        Task AddAsync(IEnumerable<Subscription> cores);

        void GetCreatedUpdated(IEnumerable<Subscription> cores);

        Task GetCreatedUpdatedAsync(IEnumerable<Subscription> cores);

        void GetCreatedUpdated(Subscription core);

        Task GetCreatedUpdatedAsync(Subscription core);

        string GeneratePasswordHash(string password);

        string GeneratePasswordResetToken(User user);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        SignInResult ValidateCredentials(string email, string password, out User user);

        SignInResult ValidateCredentials(string userId, string email, string password, out User user);

        SignInResult ValidatePasswordHash(string email, string passwordHash, out User user);

        bool VerifyPassword(string password, string passwordHash);

        SignInResult ResetPassword(User user, string resetToken, string password);

        Task<SignInResult> ResetPasswordAsync(User user, string resetToken, string password);

        DataStoreResult Complete();

        Task<DataStoreResult> CompleteAsync();

        //(ZipArchive ZipArchive, MemoryStream MemoryStream) Backup();

        void Log(
          Exception exception,
          Severity severity = Severity.Critical,
          [CallerMemberName] string methodName = null,
          [CallerFilePath] string sourceFile = null,
          [CallerLineNumber] int lineNumber = 0,
          string userId = "");

        void Log(
          Exception exception,
          string text,
          Severity severity = Severity.Critical,
          [CallerMemberName] string methodName = null,
          [CallerFilePath] string sourceFile = null,
          [CallerLineNumber] int lineNumber = 0,
          string userId = "");

        void Log(
          string text,
          Severity severity = Severity.Critical,
          [CallerMemberName] string methodName = null,
          [CallerFilePath] string sourceFile = null,
          [CallerLineNumber] int lineNumber = 0,
          string userId = "");

        Task<string> IsUniqueAsync<T>(T entity);
        Task<SilkFlo.Data.Core.Domain.Role> InsertRoleAsync(IUnitOfWork unitOfWork, string roleId, string name, string description, int sort);
        Task<Persistence.ApplicationDbContext> GetDataSetAsync();
        Task InsertRolesAsync(IUnitOfWork unitOfWork);
    }
}
