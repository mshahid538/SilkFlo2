using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkFlo.Web.Controllers;

namespace SilkFlo.Web.APIControllers.Business
{
    public class ImplementationCostController : AbstractAPI
    {
        public ImplementationCostController(Data.Core.IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }


        [HttpGet("/api/Business/ImplementationCost/GetOneTimeCosts/NewRow/IdeaId/{id}")]
        public async Task<IActionResult> GetNewRow(string id)
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


            // Get RollCosts
            await _unitOfWork.BusinessRoleCosts.GetForClientAsync(client);

            // Get Roles for RoleCosts
            await _unitOfWork.BusinessRoles.GetRoleForAsync(client.RoleCosts);

            // Get the role list
            var roleModels = client.RoleCosts.Select(roleCost => new Models.Business.Role(roleCost.Role)
            {
                RoleCost = new Models.Business.RoleCost(roleCost)
            }).ToList();

            // Get IdeaStages where stages can be assigned a cost
            await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(idea);
            await _unitOfWork.SharedStages.GetStageForAsync(idea.IdeaStages);
            var ideaStages = idea.IdeaStages.Where(x => x.Stage.CanAssignCost).ToList();


            var model = new Models.Business.ImplementationCost()
            {
                Roles = roleModels,
                IdeaStages = Models.Business.IdeaStage.Create(ideaStages),
                CurrencySymbol = client.Currency == null ? "£" : client.Currency.Symbol
            };


            if (roleModels.Any())
                model.Role = roleModels[0];

            if (ideaStages.Any())
            {
                var ideaStage = ideaStages[0];
                model.IdeaStageId = ideaStage.Id;
            }

            var html = await _viewToString.PartialAsync("Shared/Business/Idea/Edit/CostBenefit/CostEstimates/OneTimeCosts/_Row.cshtml", model);

            return Content(html);
        }
    }
}
