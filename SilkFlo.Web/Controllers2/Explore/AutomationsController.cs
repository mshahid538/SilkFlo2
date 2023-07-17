using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SilkFlo.Web.Controllers.Explore
{
    public class AutomationsController : AbstractClient
    {
        public AutomationsController(Data.Core.IUnitOfWork unitOfWork,
                                      Services.ViewToString viewToString,
                                      IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }

        [Route("/Explore/Automations")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return await View(false);
        }

        [Route("/api/Explore/Automations")]
        [Authorize]
        public async Task<IActionResult> IndexApi()
        {
            return await View(true);
        }

        private async Task<IActionResult> View(bool returnStringContent)
        {
            try
            {
                var clientCore = await GetClientAsync();


                if (clientCore == null)
                {
                    const string unauthorizedMessage = "<h1 class=\"text-danger\">Error: Unauthorised</h1>";
                    if (returnStringContent)
                        return await PageApiAsync(unauthorizedMessage);

                    return Redirect("/account/signin");
                }


                var client = new Models.Business.Client(clientCore);


                // Prepare the ViewModel
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
                var url = "/Views/Explore/Automations/Index.cshtml";
                if (returnStringContent)
                {
                    var html = await _viewToString.PartialAsync(url, viewModel);
                    return Content(html);
                }

                return View(url);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                return Content("Error returning Improvements information");
            }
        }
    }
}
