using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SilkFlo.Web.Controllers.Business
{
    public class IdeaRunningCostController : AbstractAPI
    {
        public IdeaRunningCostController(Data.Core.IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }

        [HttpGet("api/Business/ImplementationCost/GetRPSSoftwareCosts/NewRow")]
        public async Task<IActionResult> NewRow()
        {
            // Permission Clause
            if (!(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
                return Ok("unauthorised");

            var client = await GetClientAsync();

            // Permission Clause
            if (client == null)
                return Ok("unauthorised");

            await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);

            var runningCostCores = (await _unitOfWork.BusinessRunningCosts
                .FindAsync(x => x.ClientId == client.Id
                                && x.IsLive)).ToList();

            await _unitOfWork.BusinessSoftwareVenders.GetVenderForAsync(runningCostCores);
            
            var runningCosts = Models.Business.RunningCost.Create(runningCostCores);


            var ideaRunningCost = new Models.Business.IdeaRunningCost
            {
                RunningCosts = runningCosts,
                CurrencySymbol = client.Currency == null ? "£" : client.Currency.Symbol
            };

            var html = await _viewToString.PartialAsync(
                "Shared/Business/Idea/Edit/CostBenefit/CostEstimates/RPASoftwareCosts/_Row.cshtml",
                ideaRunningCost);

            return Content(html);
        }
    }
}
