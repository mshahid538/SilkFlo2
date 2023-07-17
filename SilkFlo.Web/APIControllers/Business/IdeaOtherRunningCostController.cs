using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkFlo.Web.Controllers;

namespace SilkFlo.Web.APIControllers.Business
{
    public class IdeaOtherRunningCostController : AbstractAPI
    {
        public IdeaOtherRunningCostController(Data.Core.IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }

        [HttpGet("api/Business/ImplementationCost/GetOtherRunningCosts/IdeaId/{id}")]
        public async Task<IActionResult> GetOtherRunningCosts(string id)
        {
            // Permission Clause
            if (string.IsNullOrWhiteSpace(id)
                && !(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
                return Ok("unauthorised");

            var client = await GetClientAsync();

            // Permission Clause
            if (client == null)
                return Ok("unauthorised");

            var idea = await _unitOfWork.BusinessIdeas.GetAsync(id);

            if (idea.ClientId != client.Id)
                idea = null;


            // Permission Clause
            if (idea == null)
                return Ok("unauthorised");


            await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);


            var otherRunningCostCores = (await _unitOfWork.BusinessOtherRunningCosts
                .FindAsync(x => x.ClientId == client.Id
                                && x.IsLive)).ToArray();

            if (!otherRunningCostCores.Any())
            {
                var str = await PartialAsync("Shared/Business/Idea/Edit/CostBenefit/CostEstimates/IdeaOtherRunningCost/_SummaryNoOtherRunningCosts.cshtml");
                return Content(str);
            }

            await _unitOfWork.BusinessIdeaOtherRunningCosts.GetForIdeaAsync(idea);
            await _unitOfWork.BusinessOtherRunningCosts.GetOtherRunningCostForAsync(idea.IdeaOtherRunningCosts);


            var html = await GetSummaryHtml(
                idea,
                otherRunningCostCores,
                client,
                Data.Core.Enumerators.CostType.SoftwareLicence.ToString(),
                "Other Software Costs",
                "#\u00A0Licences",
                "Total Other Software Costs");

            html += await GetSummaryHtml(
                idea,
                otherRunningCostCores,
                client,
                Data.Core.Enumerators.CostType.Support.ToString(),
                "Support Team",
                "#\u00A0FTE",
                "Total Support Team Costs");

            html += await GetSummaryHtml(
                idea,
                otherRunningCostCores,
                client,
                Data.Core.Enumerators.CostType.Infrastructure.ToString(),
                "Infrastructure",
                "#\u00A0Items",
                "Total Infrastructure Costs");

            html += await GetSummaryHtml(
                idea,
                otherRunningCostCores,
                client,
                Data.Core.Enumerators.CostType.Other.ToString(),
                "Other",
                "#\u00A0Items",
                "Total Other Costs");

            html += await _viewToString.PartialAsync(
                "Shared/Business/Idea/Edit/CostBenefit/_Totals.cshtml", 
                new ViewModels.Business.Idea.CostBenefit.GrandTotals());

            return Content(html);
        }

        public async Task<string> GetSummaryHtml(
            Data.Core.Domain.Business.Idea idea,
            IEnumerable<Data.Core.Domain.Business.OtherRunningCost> allOtherRunningCosts,
            Data.Core.Domain.Business.Client client,
            string costTypeId,
            string title,
            string numberTitle,
            string totalTitle)
        {
            string tableName = "Business.IdeaOtherRunningCosts";
            string javascriptNamespace = "SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.";

            var otherRunningCostCores = allOtherRunningCosts
                .Where(x => x.ClientId == client.Id
                                && x.CostTypeId == costTypeId
                                && x.IsLive).ToList();


            var otherRunningCosts = Models.Business.OtherRunningCost.Create(otherRunningCostCores);




            var allIdeaOtherRunningCosts = Models.Business.IdeaOtherRunningCost.Create(idea.IdeaOtherRunningCosts);
            foreach (var ideaOtherRunningCost in allIdeaOtherRunningCosts)
            {
                ideaOtherRunningCost.OtherRunningCosts = otherRunningCosts;
                ideaOtherRunningCost.JavascriptNamespace = javascriptNamespace;
            }

            var ideaOtherRunningCosts = allIdeaOtherRunningCosts.Where(x => x.OtherRunningCost?.CostTypeId == costTypeId).ToList();

            var html = await _viewToString.PartialAsync(
                "Shared/Business/Idea/Edit/CostBenefit/CostEstimates/IdeaOtherRunningCost/_Summary.cshtml",
                new ViewModels.Business.Idea.CostBenefit.IdeaOtherRunningCost
                {
                    OtherRunningCosts = otherRunningCosts,
                    IdeaOtherRunningCosts = ideaOtherRunningCosts,
                    Title = title,
                    CurrencySymbol = client.Currency.Symbol,
                    JavascriptNamespace = javascriptNamespace,
                    TableName = tableName,
                    NumberTitle = numberTitle,
                    TotalTitle = totalTitle,
                    CostTypeId = costTypeId
                });

            return html;

        }

        [HttpGet("api/Business/ImplementationCost/GetOtherRunningCosts/NewRow/{costTypeId}")]
        public async Task<IActionResult> NewRow(string costTypeId)
        {
            // Permission Clause
            if (!(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
                return Ok("unauthorised");

            var client = await GetClientAsync();

            // Permission Clause
            if (client == null)
                return Ok("unauthorised");

            await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);


            var otherRunningCostCores = (await _unitOfWork.BusinessOtherRunningCosts
                .FindAsync(x => x.ClientId == client.Id
                                && x.CostTypeId == costTypeId
                                && x.IsLive)).ToList();


            var otherRunningCostCore = otherRunningCostCores.FirstOrDefault();
            Models.Business.OtherRunningCost otherRunningCostModel = null;
            if (otherRunningCostCore != null)
            {
                otherRunningCostModel = new Models.Business.OtherRunningCost(otherRunningCostCore);
            }

            var model = new Models.Business.IdeaOtherRunningCost
            {
                OtherRunningCosts = Models.Business.OtherRunningCost.Create(otherRunningCostCores),
                JavascriptNamespace = "SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.",
                CurrencySymbol = client.Currency == null ? "£" : client.Currency.Symbol,
                CostTypeId = costTypeId,
                OtherRunningCost = otherRunningCostModel
            };

            var html = await _viewToString.PartialAsync(
                "Shared/Business/Idea/Edit/CostBenefit/CostEstimates/IdeaOtherRunningCost/_Row.cshtml",
                model);

            return Content(html);
        }
    }
}