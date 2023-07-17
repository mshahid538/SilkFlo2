using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SilkFlo.Web.ViewModels.Workshop
{
    public class PipeLineDeployed
    {
        public PipeLineDeployed(
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

        public List<Models.Business.Idea> Ideas { get; set; } = new();

        public int? FirstPage { get; set; }
        public int LastPage { get; set; }
        public int? Page { get; set; }
        public string SearchText { get; set; }

        // Show context Menu properties
        public bool ShowManageStagesMenuItem { get; set; }
        public bool ShowViewDetailsMenuItem { get; set; }
        public bool ShowEditMenuItem { get; set; }
        public bool ShowDeleteMenuItem { get; set; }


        public bool ShowContextMenu =>
            ShowManageStagesMenuItem
            || ShowViewDetailsMenuItem
            || ShowEditMenuItem
            || ShowDeleteMenuItem;

        public static int PageSize => 4;


        public static async Task<PipeLineDeployed> GetPipeLineDeployedAsync(
            Data.Core.IUnitOfWork unitOfWork,
            IAuthorizationService authorization,
            ClaimsPrincipal user,
            Models.Business.Client client,
            Data.Core.Domain.Shop.Product product,
            string searchText = "",
            int pageNumber = 1)
        {
            var cores = (await unitOfWork.BusinessIdeas.FindAsync(x => !x.IsDraft && x.ClientId == client.Id)).ToList();
            var ideas = Models.Business.Idea.Create(cores);

            ideas = ideas.Where(x =>
                x.LastIdeaStage?.Stage?.StageGroupId == Data.Core.Enumerators.StageGroup.n04_Deployed.ToString()).ToList();



            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.Trim().ToLower();
                var exactMatchRequired = false;
                if (searchText.IndexOf("\"", StringComparison.Ordinal) == 0
                    && searchText.IndexOf("\"", 1, StringComparison.Ordinal) > 1)
                {
                    exactMatchRequired = true;
                    searchText = searchText.Substring(1);
                    searchText = searchText.Substring(0, searchText.Length - 1);
                }


                ideas = exactMatchRequired ? 
                    ideas.Where(x => x.Name.ToLower() == searchText).ToList() : 
                    ideas.Where(x => x.Name.IndexOf(searchText, StringComparison.InvariantCultureIgnoreCase) > -1).ToList();
            }


            // Get the stage groups
            foreach (var idea in ideas)
            {
                var ideaStages =
                    (await (unitOfWork.BusinessIdeaStages
                        .FindAsync(x => x.IdeaId == idea.Id && x.IsInWorkFlow)))
                    .ToList();

                await unitOfWork.SharedStages.GetStageForAsync(ideaStages);
                idea.UnitOfWork = unitOfWork;
                idea.IdeaStages = Models.Business.IdeaStage.Create(ideaStages);

                idea.IdeaStages = idea.IdeaStages.OrderBy(x => x.CreatedDate).ToList();

                idea.GetBenefitPerEmployee_Hours();
                await idea.GetIdeaScoreAsync();
                await idea.GetAutomationPotentialAsync();
                await idea.ManageStageAndStatus.GetAsync(unitOfWork, idea, product, client);
            }



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

            var count = ideas.Count;
            var lastPage = count / PageSize;

            if (count % PageSize > 0)
                lastPage++;


            if (pageNumber > 1)
                ideas = ideas.Skip((pageNumber - 1) * PageSize)
                    .Take(PageSize).ToList();
            else
                ideas = ideas.Take(PageSize).ToList();


            var pipeLineDeployed = new PipeLineDeployed(
                showManageStagesMenuItem,
                showViewDetailsMenuItem,
                showEditMenuItem,
                showDeleteMenuItem)
            {
                Ideas = ideas,
                FirstPage = 1,
                LastPage = lastPage,
                Page = pageNumber,
            };

            return pipeLineDeployed;
        }

        public static async Task<string> GetHtmlAsync(
            Data.Core.IUnitOfWork unitOfWork,
            IAuthorizationService authorization,
            ClaimsPrincipal user,
            Models.Business.Client client,
            Services.ViewToString viewToString,
            Data.Core.Domain.Shop.Product product,
            string searchText = "",
            int pageNumber = 1)
        {
            var pipeLineDeployed = await GetPipeLineDeployedAsync(
                unitOfWork,
                authorization,
                user,
                client,
                product,
                searchText,
                pageNumber);

            var html = await viewToString.PartialAsync("Shared/Workshop/Deployed/_Summary.cshtml", pipeLineDeployed);
            return html;
        }

        public static async Task<string> GetTableAsync(
            Data.Core.IUnitOfWork unitOfWork,
            IAuthorizationService authorization,
            ClaimsPrincipal user,
            Models.Business.Client client,
            Services.ViewToString viewToString,
            Data.Core.Domain.Shop.Product product,
            string searchText = "",
            int pageNumber = 1)
        {
            var pipeLineDeployed = await GetPipeLineDeployedAsync(
                unitOfWork,
                authorization,
                user,
                client,
                product,
                searchText,
                pageNumber);

            var html = await viewToString.PartialAsync("Shared/Workshop/Deployed/_Table.cshtml", pipeLineDeployed);
            return html;
        }
    }
}
