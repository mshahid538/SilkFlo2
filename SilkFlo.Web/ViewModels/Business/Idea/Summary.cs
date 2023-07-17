using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace SilkFlo.Web.ViewModels.Business.Idea
{
    public class Summary
    {
        public static async Task<Summary> BuildAsync(
            IAuthorizationService authorization,
            System.Security.Claims.ClaimsPrincipal user)
        {
            var showManageStagesMenuItem =
                (await authorization.AuthorizeAsync(user, Policy.EditIdeasStageAndStatus)).Succeeded;

            var showViewDetailsMenuItem = 
                (await authorization.AuthorizeAsync(user, Policy.ViewIdeas)).Succeeded;

            var showEditMenuItem = 
                (await authorization.AuthorizeAsync(user, Policy.ReviewNewIdeas)).Succeeded
                || (await authorization.AuthorizeAsync(user, Policy.ReviewAssessedIdeas)).Succeeded
                || (await authorization.AuthorizeAsync(user, Policy.EditAllIdeaFields)).Succeeded;

            var showDeleteMenuItem =
                (await authorization.AuthorizeAsync(user, Policy.DeleteIdeas)).Succeeded;

            return new Summary(
                showManageStagesMenuItem,
                showViewDetailsMenuItem,
                showEditMenuItem,
                showDeleteMenuItem);
        }

        private Summary(
            bool showManageStagesMenuItem,
            bool showViewDetailsMenuItem,
            bool showEditMenuItem, 
            bool showDeleteMenuItem)
        {
            ShowManageStagesMenuItem = showManageStagesMenuItem;
            ShowViewDetailsMenuItem = showViewDetailsMenuItem;
            ShowEditMenuItem = showEditMenuItem;
            ShowDeleteMenuItem = showDeleteMenuItem;
        }



        public string MarginTop { get; set; } = "280px";
        // Show table columns properties
        public bool Name { get; set; } = true;
        public bool Department { get; set; } = true;
        public bool Team { get; set; } = true;
        public bool Stage { get; set; } = true;
        public bool Benefit { get; set; } = true;
        public bool BenefitHours { get; set; }
        public bool IdeaScore { get; set; }

        public bool Goal { get; set; }

        public string TargetUrl { get; set; }


        // Show context Menu properties
        public bool ShowManageStagesMenuItem{ get; set; }
        public bool ShowViewDetailsMenuItem { get; set; }
        public bool ShowEditMenuItem { get; set; }
        public bool ShowDeleteMenuItem { get; set; }


        public bool ShowContextMenu =>
            ShowManageStagesMenuItem
            || ShowViewDetailsMenuItem
            || ShowEditMenuItem
            || ShowDeleteMenuItem;


        public List<Models.Business.Idea> Ideas { get; set; } = new List<Models.Business.Idea>();

        public int TotalIdeas { get; set; }
        public bool NoIdeas { get; set; }
        public List<KeyValuePair<string, string>> POList { get; set; }
        public List<KeyValuePair<string, string>> ISList { get; set; }
        public List<Models.Business.Department> Departments { get; set; } = new();
    }
}
