// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.DataSet
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Application;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;

namespace SilkFlo.Data.Persistence
{
  public class DataSet
  {
    public ListExtended<Analytic> Analytics { get; set; } = new ListExtended<Analytic>();

    public ListExtended<Log> Logs { get; set; } = new ListExtended<Log>();

    public ListExtended<Message> Messages { get; set; } = new ListExtended<Message>();

    public ListExtended<SilkFlo.Data.Core.Domain.Role> Roles { get; set; } = new ListExtended<SilkFlo.Data.Core.Domain.Role>();

    public ListExtended<User> Users { get; set; } = new ListExtended<User>();

    public ListExtended<UserAchievement> UserAchievements { get; set; } = new ListExtended<UserAchievement>();

    public ListExtended<UserBadge> UserBadges { get; set; } = new ListExtended<UserBadge>();

    public ListExtended<UserRole> UserRoles { get; set; } = new ListExtended<UserRole>();

    public ListExtended<WebHookLog> WebHookLogs { get; set; } = new ListExtended<WebHookLog>();

    public ListExtended<ManageTenant> AgencyManageTenants { get; set; } = new ListExtended<ManageTenant>();

    public ListExtended<HotSpot> ApplicationHotSpots { get; set; } = new ListExtended<HotSpot>();

    public ListExtended<Page> ApplicationPages { get; set; } = new ListExtended<Page>();

    public ListExtended<Setting> ApplicationSettings { get; set; } = new ListExtended<Setting>();

    public ListExtended<SilkFlo.Data.Core.Domain.Business.Application> BusinessApplications { get; set; } = new ListExtended<SilkFlo.Data.Core.Domain.Business.Application>();

    public ListExtended<Client> BusinessClients { get; set; } = new ListExtended<Client>();

    public ListExtended<Collaborator> BusinessCollaborators { get; set; } = new ListExtended<Collaborator>();

    public ListExtended<CollaboratorRole> BusinessCollaboratorRoles { get; set; } = new ListExtended<CollaboratorRole>();

    public ListExtended<Comment> BusinessComments { get; set; } = new ListExtended<Comment>();

    public ListExtended<Department> BusinessDepartments { get; set; } = new ListExtended<Department>();

    public ListExtended<Document> BusinessDocuments { get; set; } = new ListExtended<Document>();

    public ListExtended<Follow> BusinessFollows { get; set; } = new ListExtended<Follow>();

    public ListExtended<Idea> BusinessIdeas { get; set; } = new ListExtended<Idea>();

    public ListExtended<IdeaApplicationVersion> BusinessIdeaApplicationVersions { get; set; } = new ListExtended<IdeaApplicationVersion>();

    public ListExtended<IdeaOtherRunningCost> BusinessIdeaOtherRunningCosts { get; set; } = new ListExtended<IdeaOtherRunningCost>();

    public ListExtended<IdeaRunningCost> BusinessIdeaRunningCosts { get; set; } = new ListExtended<IdeaRunningCost>();

    public ListExtended<IdeaStage> BusinessIdeaStages { get; set; } = new ListExtended<IdeaStage>();

    public ListExtended<IdeaStageStatus> BusinessIdeaStageStatuses { get; set; } = new ListExtended<IdeaStageStatus>();

    public ListExtended<ImplementationCost> BusinessImplementationCosts { get; set; } = new ListExtended<ImplementationCost>();

    public ListExtended<Location> BusinessLocations { get; set; } = new ListExtended<Location>();

    public ListExtended<OtherRunningCost> BusinessOtherRunningCosts { get; set; } = new ListExtended<OtherRunningCost>();

    public ListExtended<Process> BusinessProcesses { get; set; } = new ListExtended<Process>();

    public ListExtended<Recipient> BusinessRecipients { get; set; } = new ListExtended<Recipient>();

    public ListExtended<SilkFlo.Data.Core.Domain.Business.BusinessRole> BusinessRoles { get; set; } = new ListExtended<SilkFlo.Data.Core.Domain.Business.BusinessRole>();

    public ListExtended<RoleCost> BusinessRoleCosts { get; set; } = new ListExtended<RoleCost>();

    public ListExtended<RoleIdeaAuthorisation> BusinessRoleIdeaAuthorisations { get; set; } = new ListExtended<RoleIdeaAuthorisation>();

    public ListExtended<RunningCost> BusinessRunningCosts { get; set; } = new ListExtended<RunningCost>();

    public ListExtended<SoftwareVender> BusinessSoftwareVenders { get; set; } = new ListExtended<SoftwareVender>();

    public ListExtended<Team> BusinessTeams { get; set; } = new ListExtended<Team>();

    public ListExtended<UserAuthorisation> BusinessUserAuthorisations { get; set; } = new ListExtended<UserAuthorisation>();

    public ListExtended<Version> BusinessVersions { get; set; } = new ListExtended<Version>();

    public ListExtended<Vote> BusinessVotes { get; set; } = new ListExtended<Vote>();

    public ListExtended<CompanySize> CRMCompanySizes { get; set; } = new ListExtended<CompanySize>();

    public ListExtended<JobLevel> CRMJobLevels { get; set; } = new ListExtended<JobLevel>();

    public ListExtended<Prospect> CRMProspects { get; set; } = new ListExtended<Prospect>();

    public ListExtended<Achievement> SharedAchievements { get; set; } = new ListExtended<Achievement>();

    public ListExtended<ApplicationStability> SharedApplicationStabilities { get; set; } = new ListExtended<ApplicationStability>();

    public ListExtended<AutomationGoal> SharedAutomationGoals { get; set; } = new ListExtended<AutomationGoal>();

    public ListExtended<AutomationType> SharedAutomationTypes { get; set; } = new ListExtended<AutomationType>();

    public ListExtended<AverageNumberOfStep> SharedAverageNumberOfSteps { get; set; } = new ListExtended<AverageNumberOfStep>();

    public ListExtended<Badge> SharedBadges { get; set; } = new ListExtended<Badge>();

    public ListExtended<ClientType> SharedClientTypes { get; set; } = new ListExtended<ClientType>();

    public ListExtended<CostType> SharedCostTypes { get; set; } = new ListExtended<CostType>();

    public ListExtended<Country> SharedCountries { get; set; } = new ListExtended<Country>();

    public ListExtended<DataInputPercentOfStructured> SharedDataInputPercentOfStructureds { get; set; } = new ListExtended<DataInputPercentOfStructured>();

    public ListExtended<DecisionCount> SharedDecisionCounts { get; set; } = new ListExtended<DecisionCount>();

    public ListExtended<DecisionDifficulty> SharedDecisionDifficulties { get; set; } = new ListExtended<DecisionDifficulty>();

    public ListExtended<DocumentationPresent> SharedDocumentationPresents { get; set; } = new ListExtended<DocumentationPresent>();

    public ListExtended<IdeaAuthorisation> SharedIdeaAuthorisations { get; set; } = new ListExtended<IdeaAuthorisation>();

    public ListExtended<IdeaStatus> SharedIdeaStatuses { get; set; } = new ListExtended<IdeaStatus>();

    public ListExtended<Industry> SharedIndustries { get; set; } = new ListExtended<Industry>();

    public ListExtended<Input> SharedInputs { get; set; } = new ListExtended<Input>();

    public ListExtended<InputDataStructure> SharedInputDataStructures { get; set; } = new ListExtended<InputDataStructure>();

    public ListExtended<Language> SharedLanguages { get; set; } = new ListExtended<Language>();

    public ListExtended<NumberOfWaysToCompleteProcess> SharedNumberOfWaysToCompleteProcesses { get; set; } = new ListExtended<NumberOfWaysToCompleteProcess>();

    public ListExtended<Period> SharedPeriods { get; set; } = new ListExtended<Period>();

    public ListExtended<ProcessPeak> SharedProcessPeaks { get; set; } = new ListExtended<ProcessPeak>();

    public ListExtended<ProcessStability> SharedProcessStabilities { get; set; } = new ListExtended<ProcessStability>();

    public ListExtended<Rule> SharedRules { get; set; } = new ListExtended<Rule>();

    public ListExtended<Stage> SharedStages { get; set; } = new ListExtended<Stage>();

    public ListExtended<StageGroup> SharedStageGroups { get; set; } = new ListExtended<StageGroup>();

    public ListExtended<SubmissionPath> SharedSubmissionPaths { get; set; } = new ListExtended<SubmissionPath>();

    public ListExtended<TaskFrequency> SharedTaskFrequencies { get; set; } = new ListExtended<TaskFrequency>();

    public ListExtended<Coupon> ShopCoupons { get; set; } = new ListExtended<Coupon>();

    public ListExtended<Currency> ShopCurrencies { get; set; } = new ListExtended<Currency>();

    public ListExtended<Discount> ShopDiscounts { get; set; } = new ListExtended<Discount>();

    public ListExtended<Price> ShopPrices { get; set; } = new ListExtended<Price>();

    public ListExtended<Product> ShopProducts { get; set; } = new ListExtended<Product>();

    public ListExtended<Subscription> ShopSubscriptions { get; set; } = new ListExtended<Subscription>();
  }
}
