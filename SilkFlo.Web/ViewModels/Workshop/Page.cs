using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SilkFlo.Web.ViewModels.Workshop
{
    public class Page
    {
        public static async Task<Page> BuildAsync(
            string stageGroupName,
            List<Models.Shared.StageGroup> stageGroups,
            Data.Core.Domain.Business.Client client,
            Data.Core.IUnitOfWork unitOfWork,
            IAuthorizationService authorization,
            System.Security.Claims.ClaimsPrincipal user,
            Data.Core.Domain.Shop.Product product,
			DateTime? startDate = null, DateTime? endDate = null, bool? isWeekly = null, bool? isMonthly = null, bool? isYearly = null, string processOwners = "", string ideaSubmitters = "", string departmentsId = "", string teamsId = "")
        {
            var stageGroup = stageGroups.SingleOrDefault(x => string.Equals(x.Name, stageGroupName, StringComparison.CurrentCultureIgnoreCase));

            var tileUrls = new List<string>();
			switch (stageGroupName.ToLower())
			{
				case "review":
					tileUrls.Add("StageGroup/" + stageGroup + "/TotalIdeas");
					tileUrls.Add("StageGroup/" + stageGroup + "/AwaitingReview");
					break;
				case "assess":
					tileUrls.Add("StageGroup/" + stageGroup + "/TotalIdeas");
					tileUrls.Add("StageGroup/" + stageGroup + "/PotentialBenefit");
					tileUrls.Add("StageGroup/" + stageGroup + "/PotentialHourSavings");
					tileUrls.Add("StageGroup/" + stageGroup + "/AwaitingReview");
					break;
				case "decision":
					tileUrls.Add("StageGroup/" + stageGroup + "/PotentialBenefit");
					tileUrls.Add("StageGroup/" + stageGroup + "/EstimatedOneTimeCost");
					tileUrls.Add("StageGroup/" + stageGroup + "/EstimatedRunningCosts");
					break;
				case "build":
					tileUrls.Add("StageGroup/" + stageGroup + "/TotalInBuild");
					tileUrls.Add("StageGroup/" + stageGroup + "/PotentialBenefit");
					tileUrls.Add("StageGroup/" + stageGroup + "/TotalAtRisk");
					tileUrls.Add("StageGroup/" + stageGroup + "/TotalBenefitAtRisk");
					tileUrls.Add("StageGroup/" + stageGroup + "/EstimatedOneTimeCost");
					//tileUrls.Add("StageGroup/" + stageGroup + "/AverageBuildTime");
					break;
				case "deployed":
					tileUrls.Add("StageGroup/" + stageGroup + "/TotalInDeployed");
					tileUrls.Add("StageGroup/" + stageGroup + "/PotentialBenefit");
					tileUrls.Add("StageGroup/" + stageGroup + "/PotentialHourSavings");
					break;
			}
			var pipeLine = await PipeLine.BuildAsync(
                unitOfWork,
                authorization,
                stageGroup,
                user,
                client,
                tileUrls);

            pipeLine.FilterTargetId = "Business.Idea.Summary";
            pipeLine.TargetUrl = "/api/Business/Idea/Build/FilterSummary";
            await pipeLine.GetIdeas(product);


			//adding List of pOwn & iSub
			var processOwnerList = new List<KeyValuePair<string, string>>();
			var ideaSubmitterList = new List<KeyValuePair<string, string>>();
			foreach (var x in pipeLine.IdeaSummary.Ideas)
			{
				var idea = x.GetCore();
				await unitOfWork.Users.GetProcessOwnerForAsync(idea);

				if (!processOwnerList.Any(x => x.Value == idea.ProcessOwnerId))
					processOwnerList.Add(new KeyValuePair<string, string>(idea.ProcessOwner.Fullname, idea.ProcessOwnerId));

				if (!ideaSubmitterList.Any(x => x.Value == idea.ProcessOwnerId))
					ideaSubmitterList.Add(new KeyValuePair<string, string>(idea.ProcessOwner.Fullname, idea.ProcessOwnerId));
			}

			pipeLine.IdeaSummary.POList = processOwnerList;
			pipeLine.IdeaSummary.ISList = ideaSubmitterList;

			// Get departments
			var departmentsCore = await unitOfWork.BusinessDepartments.FindAsync(x => x.ClientId == client.Id);
			foreach (var department in departmentsCore)
				pipeLine.IdeaSummary.Departments.Add(new Models.Business.Department(department));

			if (client.Ideas.Count == 0)
				pipeLine.IdeaSummary.NoIdeas = true;


			#region IdeaFiltersBehaviour
			if (isWeekly.HasValue && isWeekly.Value)
			{
				var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
				pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
			}
			else if (isMonthly.HasValue && isMonthly.Value)
			{
				var previousMonthStartDate = DateTime.Now.AddMonths(-1);
				pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
			}
			else if (isYearly.HasValue && isYearly.Value)
			{
				var previousYearStartDate = DateTime.Now.AddYears(-1);
				pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
			}
			else if (startDate.HasValue && endDate.HasValue)
			{
				pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
			}

			if (!String.IsNullOrWhiteSpace(ideaSubmitters))
			{
				var isList = ideaSubmitters.Split(",");
				pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
			}

			if (!String.IsNullOrWhiteSpace(processOwners))
			{
				var poList = processOwners.Split(",");
				pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
			}

			if (!String.IsNullOrWhiteSpace(departmentsId))
			{
				var departmentsIdList = departmentsId.Split(",");
				pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
			}

			if (!String.IsNullOrWhiteSpace(teamsId))
			{
				var teamsIdList = teamsId.Split(",");
				pipeLine.IdeaSummary.Ideas = pipeLine.IdeaSummary.Ideas.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
			}

			#endregion


			return new Page(
                stageGroup?.Name, 
                stageGroups, 
                client, 
                pipeLine,
                product);
        }

        private Page(string stageGroup,
                    List<Models.Shared.StageGroup> stageGroups,
                    Data.Core.Domain.Business.Client client,
                    PipeLine pipeLine,
                    Data.Core.Domain.Shop.Product product)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));



            IsPractice = client.IsPractice;
            ClientId = client.Id;

            if (string.IsNullOrWhiteSpace(stageGroup))
                stageGroup = "All";

            StageGroup = stageGroup;

            StageGroups.Add("All");
            foreach (var item in stageGroups)
                StageGroups.Add(item.Name);


            PipeLine = pipeLine;
            Product = product;
        }


        public string StageGroup { get; }

        public List<string> StageGroups { get; set; } = new();


        public bool IsPractice { get; set; }
        public string ClientId { get; set; }

        public PipeLine PipeLine { get; }

        public Data.Core.Domain.Shop.Product Product { get; set; }
    }
}
