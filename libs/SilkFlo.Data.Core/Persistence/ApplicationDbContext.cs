using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Application;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        : base("Server=tcp:silkflo-dev.database.windows.net,1433;Initial Catalog=silkflo-test;Persist Security Info=False;User ID=sfadmin;Password=Sf-admin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Check if there are any pending changes
            if (ChangeTracker.HasChanges())
            {
                var entries = ChangeTracker.Entries();

            }
            return base.SaveChanges();
        }

        public DbSet<Analytic> Analytics { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<SilkFlo.Data.Core.Domain.Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserAchievement> UserAchievements { get; set; }

        public DbSet<UserBadge> UserBadges { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<WebHookLog> WebHookLogs { get; set; }

        public DbSet<ManageTenant> AgencyManageTenants { get; set; }

        public DbSet<HotSpot> ApplicationHotSpots { get; set; }

        public DbSet<Page> ApplicationPages { get; set; }

        public DbSet<Setting> ApplicationSettings { get; set; }

        public DbSet<SilkFlo.Data.Core.Domain.Business.Application> BusinessApplications { get; set; }

        public DbSet<Client> BusinessClients { get; set; }

        public DbSet<Collaborator> BusinessCollaborators { get; set; }

        public DbSet<CollaboratorRole> BusinessCollaboratorRoles { get; set; }

        public DbSet<Comment> BusinessComments { get; set; }

        public DbSet<Department> BusinessDepartments { get; set; }

        public DbSet<Document> BusinessDocuments { get; set; }

        public DbSet<Follow> BusinessFollows { get; set; }

        public DbSet<Idea> BusinessIdeas { get; set; }

        public DbSet<IdeaApplicationVersion> BusinessIdeaApplicationVersions { get; set; }

        public DbSet<IdeaOtherRunningCost> BusinessIdeaOtherRunningCosts { get; set; }

        public DbSet<IdeaRunningCost> BusinessIdeaRunningCosts { get; set; }

        public DbSet<IdeaStage> BusinessIdeaStages { get; set; }

        public DbSet<IdeaStageStatus> BusinessIdeaStageStatuses { get; set; }

        public DbSet<ImplementationCost> BusinessImplementationCosts { get; set; }

        public DbSet<Location> BusinessLocations { get; set; }

        public DbSet<OtherRunningCost> BusinessOtherRunningCosts { get; set; }

        public DbSet<Process> BusinessProcesses { get; set; }

        public DbSet<Recipient> BusinessRecipients { get; set; }

        public DbSet<SilkFlo.Data.Core.Domain.Business.BusinessRole> BusinessRoles { get; set; }

        public DbSet<RoleCost> BusinessRoleCosts { get; set; }

        public DbSet<RoleIdeaAuthorisation> BusinessRoleIdeaAuthorisations { get; set; }

        public DbSet<RunningCost> BusinessRunningCosts { get; set; }

        public DbSet<SoftwareVender> BusinessSoftwareVenders { get; set; }

        public DbSet<Team> BusinessTeams { get; set; }

        public DbSet<UserAuthorisation> BusinessUserAuthorisations { get; set; }

        public DbSet<Domain.Business.Version> BusinessVersions { get; set; }

        public DbSet<Vote> BusinessVotes { get; set; }

        public DbSet<CompanySize> CRMCompanySizes { get; set; }

        public DbSet<JobLevel> CRMJobLevels { get; set; }

        public DbSet<Prospect> CRMProspects { get; set; }

        public DbSet<Achievement> SharedAchievements { get; set; }

        public DbSet<ApplicationStability> SharedApplicationStabilities { get; set; }

        public DbSet<AutomationGoal> SharedAutomationGoals { get; set; }

        public DbSet<AutomationType> SharedAutomationTypes { get; set; }

        public DbSet<AverageNumberOfStep> SharedAverageNumberOfSteps { get; set; }

        public DbSet<Badge> SharedBadges { get; set; }

        public DbSet<ClientType> SharedClientTypes { get; set; }

        public DbSet<CostType> SharedCostTypes { get; set; }

        public DbSet<Country> SharedCountries { get; set; }

        public DbSet<DataInputPercentOfStructured> SharedDataInputPercentOfStructureds { get; set; }

        public DbSet<DecisionCount> SharedDecisionCounts { get; set; }

        public DbSet<DecisionDifficulty> SharedDecisionDifficulties { get; set; }

        public DbSet<DocumentationPresent> SharedDocumentationPresents { get; set; }

        public DbSet<IdeaAuthorisation> SharedIdeaAuthorisations { get; set; }

        public DbSet<IdeaStatus> SharedIdeaStatuses { get; set; }

        public DbSet<Industry> SharedIndustries { get; set; }

        public DbSet<Input> SharedInputs { get; set; }

        public DbSet<InputDataStructure> SharedInputDataStructures { get; set; }

        public DbSet<Language> SharedLanguages { get; set; }

        public DbSet<NumberOfWaysToCompleteProcess> SharedNumberOfWaysToCompleteProcesses { get; set; }

        public DbSet<Period> SharedPeriods { get; set; }

        public DbSet<ProcessPeak> SharedProcessPeaks { get; set; }

        public DbSet<ProcessStability> SharedProcessStabilities { get; set; }

        public DbSet<Rule> SharedRules { get; set; }

        public DbSet<Stage> SharedStages { get; set; }

        public DbSet<StageGroup> SharedStageGroups { get; set; }

        public DbSet<SubmissionPath> SharedSubmissionPaths { get; set; }

        public DbSet<TaskFrequency> SharedTaskFrequencies { get; set; }

        public DbSet<Coupon> ShopCoupons { get; set; }

        public DbSet<Currency> ShopCurrencies { get; set; }

        public DbSet<Discount> ShopDiscounts { get; set; }

        public DbSet<Price> ShopPrices { get; set; }

        public DbSet<Product> ShopProducts { get; set; }

        public DbSet<Subscription> ShopSubscriptions { get; set; }

        public void CompleteAsync()
        {
            SaveChanges();
        }
    }
}
