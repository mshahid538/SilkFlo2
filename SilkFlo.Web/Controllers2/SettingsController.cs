using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SilkFlo.Web.Models;

namespace SilkFlo.Web.Controllers
{
    public partial class SettingsController : AbstractAPI
    {
        public SettingsController(Data.Core.IUnitOfWork unitOfWork,
                                   Services.ViewToString viewToString,
                                   IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }



        [HttpGet("/Settings/Tenant/People")]
        [HttpGet("/Settings/Agency/People")]
        public async Task<IActionResult> GetPeople()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantUsers)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageAgencyUsers)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageAgencyUserRoles)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageTenantUserRoles)).Succeeded)
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Redirect("/account/signin");

                return View("../home/Page", "<h1 class=\"text-warning\">You do not have permissions to manage users.</h1>");
            }


            return await CreatePeopleView(false);
        }

        [HttpGet("/api/Settings/Tenant/People")]
        [HttpGet("/api/Settings/Agency/People")]
        public async Task<IActionResult> PeopleApi()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantUsers)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageAgencyUsers)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageAgencyUserRoles)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageTenantUserRoles)).Succeeded)
                return NegativeFeedback();

            return await CreatePeopleView(true);
        }


        [HttpGet("/Settings/Tenant/Guests")]
        public async Task<IActionResult> GetGuests()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantUsers)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageAgencyUsers)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageAgencyUserRoles)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageTenantUserRoles)).Succeeded)
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Redirect("/account/signin");

                return View("../home/Page", "<h1 class=\"text-warning\">You do not have permissions to manage users.</h1>");
            }


            return await CreatePeopleView(false, true);
        }

        private async Task<IActionResult> CreatePeopleView(
            bool returnStringContent,
            bool guestsOnly = false)
        {
            try
            {
                var client = await GetClientAsync();

                if (client == null)
                {
                    if (returnStringContent)
                        return NegativeFeedback();
                    
                    return Redirect("/account/signin");
                }


                // Agencies cannot have guests
                if (client.TypeId != Data.Core.Enumerators.ClientType.Client39.ToString())
                    guestsOnly = false;



                var viewModel = new ViewModels.Settings.People
                {
                    Client = new Models.Business.Client(client),
                    GuestOnly = guestsOnly
                };                

                const string url = "/Views/Settings/People.cshtml";
                if (returnStringContent)
                {
                    var html = await _viewToString.PartialAsync(url, viewModel);
                    return Content(html);
                }



                if (guestsOnly)
                    viewModel.TableUrl = "Settings/Guest/Table";

                return View(url, viewModel);
            }
            catch (Exception ex)
            {
                Log(ex);

                if (returnStringContent)
                    return Content("Error fetching data");

                //return Redirect("/Maintenance");
                return View("/Views/Home/maintenance.cshtml", "CreatePeopleView");

            }
        }



        [HttpGet("Settings/agency/tenants")]
        [Authorize]
        public async Task<IActionResult> GetTenants()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageAgencySettings)).Succeeded)
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                    return Redirect("/account/signin");

                return View("../home/Page", "<h1 class=\"text-warning\">You do not have permissions to manage platform settings.</h1>");
            }


            return await CreateTenantsView(false);
        }

        [HttpGet("/api/Settings/agency/tenants")]
        public async Task<IActionResult> TenantsApi()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageAgencySettings)).Succeeded)
                return NegativeFeedback();

            return await CreateTenantsView(true);
        }



        private async Task<IActionResult> CreateTenantsView(bool returnStringContent)
        {
            try
            {
                var agencyCore = await GetClientAsync();

                if (agencyCore == null)
                {
                    if (returnStringContent)
                        return Content("<h1 class=\"text-danger\">Unauthorised</h1>");

                    return Redirect("/account/signin");
                }

                var model = new Models.Business.Client(agencyCore);


                if ( !model.IsAgency )
                {
                    if (returnStringContent)
                        return NegativeFeedback();

                    return Redirect("/account/signin");
                }



                var environment = Security.Settings.GetEnvironment();
                var subscriptions = await ViewModels.Subscriptions.GetAsync(
                    _unitOfWork,
                    environment, 
                    "", 
                    false,
                    false,
                    "",
                    "");


                var discount = await model.GetDiscountPercentAsync(_unitOfWork);

                subscriptions.Periods =
                    subscriptions
                        .Periods
                        .Where(x => x.Id == Data.Core.Enumerators.Period.Annual.ToString())
                        .ToList();

                foreach (var price in subscriptions.Periods.SelectMany(period => period.Prices))
                    price.DiscountPercent = discount;



                var viewModel = new ViewModels.Settings.Tenants
                {
                    Client = model,
                    Subscriptions = subscriptions,
                    ShowSubscriptionButton = agencyCore.Name != Data.Core.Settings.ApplicationName
                };

                const string url = "/Views/Settings/Tenants.cshtml";

                if (!returnStringContent)
                    return View(url, viewModel);


                var html = await _viewToString.PartialAsync(url, viewModel);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);

                if (returnStringContent)
                    return Content("Error fetching data");

                //return Redirect("/Maintenance");
                return View("/Views/Home/maintenance.cshtml", "CreateTenantsView");
            }
        }

        [HttpGet("/api/Settings/tenant/Platform-Setup/{tab}")]
        public async Task<IActionResult> GetPlatformSetupApi(string tab)
        {
            return await CreatePlatformSetupView(true, tab);
        }




        [HttpGet("/Settings/tenant/Platform-Setup")]
        public IActionResult GetPlatformSetup()
        {
            return Redirect("/Settings/tenant/Platform-Setup/Business-Units");
        }


        [HttpGet("/Settings/tenant/Platform-Setup/{tab}")]
        [HttpGet("/Settings/tenant/Platform-Setup/{tab}/Software-Vendor")]
        [HttpGet("/Settings/tenant/Platform-Setup/{tab}/Initial-Costs")]
        [HttpGet("/Settings/tenant/Platform-Setup/{tab}/Running-Costs")]
        [HttpGet("/Settings/tenant/Platform-Setup/{tab}/Other-Running-Costs")]
        public async Task<IActionResult> GetPlatformSetupCostSetup(string tab)
        {
            return await CreatePlatformSetupView(false, tab);
        }


        private async Task<IActionResult> CreatePlatformSetupView(
            bool returnStringContent,
            string tab)
        {
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                {
                    if (returnStringContent)
                        return NegativeFeedback();

                    return Redirect("/account/signin");
                }

                return View("../home/Page","<h1 class=\"text-warning\">You do not have permissions to manage platform settings.</h1>");
            }

            try
            {
                var client = await GetClientAsync();

                if (client == null)
                {
                    if (returnStringContent)
                        return NegativeFeedback();

                    return Redirect("/account/signin");
                }


                if (string.IsNullOrWhiteSpace(tab))
                    tab = "business-units";

                var platformSetup = new ViewModels.Settings.PlatformSetup(
                    tab,
                    client.IsPractice);

                
                const string url = "/Views/Settings/PlatformSetup.cshtml";
                if (returnStringContent)
                {
                    var html = await _viewToString.PartialAsync(url, platformSetup);
                    return Content(html);
                }

                return View("/Views/Settings/PlatformSetup.cshtml", platformSetup);
            }
            catch (Exception ex)
            {
                Log(ex);

                if (returnStringContent)
                    return Content("Error fetching data");


                return View("/Views/Home/maintenance.cshtml", "CreatePlatformSetupView");
            }
        }


        [HttpGet("/api/Settings/PlatformSetup/BusinessUnits/GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
           

            try
            {
                var client = await GetClientAsync();

                if (client == null)
                    return NegativeFeedback();

                await _unitOfWork.BusinessDepartments.GetForClientAsync(client);
                var model = new Models.Business.Client(client);

                string html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/BusinessUnit/_BusinessUnits", model.Departments);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }

        [HttpGet("/api/Settings/PlatformSetup/BusinessUnits/GetAreas/departmentId/{departmentId}")]
        public async Task<IActionResult> GetAreas(string departmentId)
        {
            try
            {
                // Authorization Clause
                if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                    return NegativeFeedback();

                if (string.IsNullOrWhiteSpace(departmentId))
                {
                    return Content("");
                }

                var cores = await (_unitOfWork.BusinessTeams.FindAsync(x => x.DepartmentId == departmentId));
                var model = Models.Business.Team.Create(cores);



                var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/BusinessUnit/_Areas.cshtml", model);
                return Content(html);
            }
            catch (Exception e)
            {
                Log(e);
                return Content("Server Error");
            }
        }

        [HttpGet("/api/Settings/PlatformSetup/BusinessUnits/GetSubAreas/teamId/{teamId}")]
        public async Task<IActionResult> GetSubAreas(string teamId)
        {
            try
            {
                // Authorization Clause
                if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                    return NegativeFeedback();

                if (string.IsNullOrWhiteSpace(teamId))
                {
                    return Content("");
                }

                var cores = await (_unitOfWork.BusinessProcesses.FindAsync(x => x.TeamId == teamId));
                var model = Models.Business.Process.Create(cores);



                var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/BusinessUnit/_SubAreas.cshtml", model);
                return Content(html);
            }
            catch (Exception e)
            {
                Log(e);
                return Content("Server Error");
            }
        }
    }
}
