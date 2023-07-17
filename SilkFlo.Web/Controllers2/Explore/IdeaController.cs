using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Web.Controllers.Explore
{
    public class IdeaController : AbstractAPI
    {
        public IdeaController(Data.Core.IUnitOfWork unitOfWork,
                              Services.ViewToString viewToString,
                              IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }

        [Route("/Explore/Ideas")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return await View(false);
        }

        [Route("/api/Explore/Ideas")]
        public async Task<IActionResult> IndexApi()
        {
            return await View(true);
        }

        private async Task<IActionResult> View(bool returnStringContent)
        {
            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var client = new Models.Business.Client(clientCore);


            // Prepare the viewmodel
            var viewModel = new ViewModels.Home
            {
                ClientId = client.Id
            };


            if (client.IsAgency)
            {
                viewModel.AgencyDashboard = new ViewModels.Dashboard.Agency
                {
                    Title = client.Name
                };
            }
            else
            {
                viewModel.TenantDashboard = new ViewModels.Dashboard.Tenant
                {
                    ClientName = client.Name,
                    IsPractice = client.IsPractice,
                };
            }


            // Return the view.
            const string url = "/Views/Explore/Ideas/Index.cshtml";
            if (!returnStringContent)
                return View(url);


            var html = await _viewToString.PartialAsync(url, viewModel);
            return Content(html);
        }
    }
}
