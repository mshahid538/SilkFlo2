namespace SilkFlo.Data.Core
{
    public class Enumerators
    {
        public enum Role
        {
            Administrator = RoleId.Administrator,    // SilkFlo
            UATTest = RoleId.UATTest,
            CanBackup = RoleId.CanBackup,
            AccountOwner = -165,
            ProgramManager = -287,
            IdeaApprover = -3123,
            AuthorisedUser = -544,
            RPASponsor = -598,
            StandardUser = -126,
            AgencyUser = -743,          // Agency
            AgencyAdministrator = -998  // Agency
        }


        public enum StageGroup
        {
            n00_Review,
            n01_Assess,
            n02_Decision,
            n03_Build,
            n04_Deployed,
        }

        public enum Stage
        {
            n00_Idea,
            n01_Assess,
            n02_Qualify,
            n03_Analysis,
            n04_SolutionDesign,
            n05_Development,
            n06_Testing,
            n07_Deployed,
        }

        public enum IdeaStatus
        {
            n00_Idea_AwaitingReview,
            n01_Idea_Duplicate,
            n02_Idea_Rejected,
            n03_Idea_Archived,
            n04_Assess_AwaitingReview,
            n05_Assess_NotStarted,
            n06_Assess_InProgress,
            n07_Assess_OnHold,
            n08_Assess_Postponed,
            n09_Assess_Rejected,
            n10_Assess_Archived,
            n11_Qualify_AwaitingReview,
            n12_Qualify_OnHold,
            n13_Qualify_Approved,
            n14_Qualify_Rejected,
            n15_Qualify_Archived,
            n16_Analysis_NotStarted,
            n17_Analysis_InProgress,
            n18_Analysis_OnHold,
            n19_Analysis_Cancelled,
            n20_Analysis_AtRisk,
            n21_Analysis_Delayed,
            n22_Analysis_Completed,
            n23_Analysis_Archived,
            n24_SolutionDesign_NotStarted,
            n25_SolutionDesign_InProgress,
            n26_SolutionDesign_OnHold,
            n27_SolutionDesign_Cancelled,
            n28_SolutionDesign_AtRisk,
            n29_SolutionDesign_Delayed,
            n30_SolutionDesign_Completed,
            n31_SolutionDesign_Archived,
            n32_Development_NotStarted,
            n33_Development_InProgress,
            n34_Development_OnHold,
            n35_Development_Cancelled,
            n36_Development_AtRisk,
            n37_Development_Delayed,
            n38_Development_Completed,
            n39_Development_Archived,
            n40_Testing_NotStarted,
            n41_Testing_InProgress,
            n42_Testing_OnHold,
            n43_Testing_Cancelled,
            n44_Testing_AtRisk,
            n45_Testing_Delayed,
            n46_Testing_Completed,
            n47_Testing_Archived,
            n48_Deployed_ReadyForProduction,
            n49_Deployed_HyperCare,
            n50_Deployed_OnHold,
            n51_Deployed_InProduction,
            n52_Deployed_Archived,
        }

        public enum IdeaAuthorization
        {
            EditAbout,
            EditAdvancedSettings,
            StageAndStatus,
            ViewCostBenefit,
            EditCostBenefit,
            EditDocumentation,
            ManageCollaborators,
        }

        public enum Setting
        {
            PracticeAccountCanSignIn,
            PracticeAccountPassword,
            TestEmailAccount,
            TrialPeriod
        }

        public enum SubmissionPath
        {
            StandardUser,
            COEUser,
        }

        public enum TaskFrequency
        {
            Daily,
            Weekly,
            Monthly,
            Quarterly,
            BiAnnually,
            Yearly
        }

        public enum ClientType
        {
            Client39,
            ReferrerAgency41,
            ResellerAgency45
        }

        public enum AutomationType
        {
            Attended,
            Unattended
        }

        public enum Period
        {
            Annual,
            Monthly
        }

        public enum CostType
        {
            Infrastructure,
            Other,
            SoftwareLicence,
            Support
        }

        public enum Page
        {
            PageNotFound,
            UnderConstruction,
            PlatformTerms,
        }

        public enum WebHookSource
        {
            Stripe
        }
    }
}