﻿/*********************************************************
       Code Generated By  .  .  .  .  Delaney's ScriptBot
       WWW .  .  .  .  .  .  .  .  .  www.scriptbot.io
       Template Name.  .  .  .  .  .  Project Green 2.0
       Template Version.  .  .  .  .  20210407 006
       Author .  .  .  .  .  .  .  .  Delaney

                          .___,
                       ___('v')___
                       `"-\._./-"'
                           ^ ^

 *********************************************************/

namespace SilkFlo.Web
{
    public class Policy
    {
        // 1.1	Automation Ideas Permissions
        public static string Subscriber { get; } = "Subscriber";

        /// <summary>
        /// Can view the information available in any Idea page.
        /// </summary>
        public static string ViewIdeas { get; } = "View Ideas";

        /// <summary>
        /// Can share ideas through the employee-driven flow.
        /// </summary>
        public static string ShareEmployeeDrivenIdeas { get; } = "Share Employee Driven Ideas";

        /// <summary>
        /// Can submit ideas through the CoE-driven flow.
        /// </summary>
        public static string SubmitCoEDrivenIdeas { get;} = "Submit CoE Driven Ideas";

        /// <summary>
        /// Can check newly submitted ideas that are in Idea Stage
        /// or Awaiting Review Status
        /// and review their content, in order to make a decision around approving it, marking it as a duplicate or rejecting it.
        /// </summary>
        public static string ReviewNewIdeas { get; } = "Review New Ideas";

        /// <summary>
        /// Can take a decision around approve/reject/put on hold for ideas that are in Qualification Stage | Awaiting Review status.
        /// </summary>
        public static string ReviewAssessedIdeas { get; } = "Review Assessed Ideas";

        /// <summary>
        /// Can view and edit all the sections of any idea: About, Cost-Benefit Analysis, Documentation, Components and Collaborators.
        /// </summary>
        public static string EditAllIdeaFields { get; } = "Edit All Idea Fields";

        /// <summary>
        /// Can change the Stage and Status of any idea by accessing the idea automation profile > About > edit mode > Phase and Status dropdowns.
        /// </summary>
        public static string EditIdeasStageAndStatus { get; } = "Edit Ideas Stage and Status";

        /// <summary>
        /// Can assign a Process Owner to an automation idea during the approval process.
        /// </summary>
        public static string AssignProcessOwner { get; } = "Assign Process Owner";

        /// <summary>
        /// Can change the status of the idea to Archived, hiding it from the Explore Automation page.
        /// </summary>
        public static string ArchiveIdeas { get; } = "Archive Ideas";

        /// <summary>
        /// Can permanently delete the idea and all its related content from the database.
        /// </summary>
        public static string DeleteIdeas { get; } = "Delete Ideas";

        /// <summary>
        /// Can view and download the built-in Reports.
        /// </summary>
        public static string ViewReports { get; } = "View Reports";

        /// <summary>
        /// Can view and download the built-in Dashboards
        /// </summary>
        public static string ViewTenantDashboards { get; } = "View Tenant Dashboards";

        /// <summary>
        /// Can view sensitive information about estimated benefits and costs extracted from the Detailed Assessment and the Cost-Benefit Analysis.
        /// * The information is available in the following columns:
        ///    Est. Benefit (FTEs),
        ///    Est. Benefit (currency),
        ///    Est. Implementation Costs,
        ///    Est. Running Costs,
        ///    Est. Net Benefit Year 1,
        ///    Est. Net Benefit Year 2.
        /// </summary>
        public static string ViewCostInfoInAutomationPipeline { get; } = "View Cost Info in Automation Pipeline";




        // Tenant Permissions
        /// <summary>
        /// Can view the Settings page that for the tenant.
        /// </summary>
        public static string ManageTenantSettings { get; } = "Manage Tenant Settings";

        /// <summary>
        /// Can view the People summary page.
        /// This page contains the options allowing you to view / add / edit / delete user accounts.
        /// </summary>
        public static string ManageTenantUsers { get; } = "Manage Tenant Users";

        /// <summary>
        /// Can view the User Roles page.
        /// The page contains options to add and remove users from roles.
        /// </summary>
        public static string ManageTenantUserRoles { get; } = "Manage Tenant User Roles";

        /// <summary>
        /// Can view the Settings page that for the tenant.
        /// </summary>
        public static string ManageAgencySettings { get; } = "Manage Agency Settings";

        /// <summary>
        /// Can view the People summary page.
        /// This page contains the options allowing you to view / add / edit / delete user accounts.
        /// </summary>
        public static string ManageAgencyUsers { get; } = "Manage Agency Users";

        /// <summary>
        /// Can view the User Roles page.
        /// The page contains options to add and remove users from roles.
        /// </summary>
        public static string ManageAgencyUserRoles { get; } = "Manage Agency User Roles";





        // Internal Miscellaneous Policies
        public const string Administrator = "Administrator";
        public const string DifferentUser = "Different User";
        public const string AnyRole = "Any Role";
        public const string CanViewAdministrationArea = "Can View Administration Area";
        public const string CanBackupDataSet = "Can Backup DataSet";
    }
}