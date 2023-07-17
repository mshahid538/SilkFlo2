using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SilkFlo.Web.ViewModels.Dashboard;
using SilkFlo.Web.ViewModels.Settings;

namespace SilkFlo.Web.Controllers
{
    public partial class DashboardController : AbstractResubscribe
    {
        public DashboardController(Data.Core.IUnitOfWork unitOfWork,
                                   Services.ViewToString viewToString,
                                   IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [Route("Dashboard/clientid/{clientId}")]
        [Route("Dashboard")]
        public async Task<IActionResult> Dashboard(string clientId = "")
        {
            if ((await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return await View(
                    false,
                    "",
                    clientId);

            if (!(await AuthorizeAsync(Policy.Subscriber, false)).Succeeded)
                return Redirect("/account/signin");
            

            return Redirect("Pricing");
        }


        [Route("api/Dashboard")]
        [Route("api/Dashboard/clientid/{clientId}")]
        public async Task<IActionResult> DashboardAPI(string clientId = "")
        {
            var returnStringContent = true;

            if ((await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return await View(
                    returnStringContent,
                    "",
                    clientId);

            if (!(await AuthorizeAsync(Policy.Subscriber, false)).Succeeded)
                return NegativeFeedback();


            return Content($"<span class=\"text-info\" >See <a href=\"/pricing\">pricing</a><span>");
        }



        [Route("Dashboard/Personal/clientid/{clientId}")]
        [Route("Dashboard/Personal")]
        public async Task<IActionResult> GetPersonal()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Redirect("/account/signin");

            return await View(false, "Dashboard/Personal");
        }


        [Route("api/Dashboard/Personal/clientid/{clientId}")]
        [Route("api/Dashboard/Personal")]
        public async Task<IActionResult> GetPersonalApi()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            return await PersonalViewAsync(true);
        }



        [Route("Dashboard/Performance/clientid/{clientId}")]
        [Route("Dashboard/Performance")]
        public async Task<IActionResult> GetPerformance(string clientId = "")
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Redirect("/account/signin");

            return await View(false, "Dashboard/Performance");
        }

        [Route("api/Dashboard/Performance/clientid/{clientId}")]
        [Route("api/Dashboard/Performance")]
        public async Task<IActionResult> GetPerformanceApi()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            return await PerformanceViewAsync(true);
        }

        [Route("AgencyDashboard/clientid/{clientId}")]
        [Route("AgencyDashboard")]
        [Route("Dashboard/Reseller/clientid/{clientId}")]
        [Route("Dashboard/Reseller")]
        [Authorize]
        public async Task<IActionResult> DashboardAgency(string clientId)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Redirect("/account/signin");

            return await ViewAgency(false, clientId);
        }

        [Route("api/AgencyDashboard/clientid/{clientId}")]
        [Route("api/AgencyDashboard")]
        [Route("api/Dashboard/Reseller/clientid/{clientId}")]
        [Route("api/Dashboard/Reseller")]
        public async Task<IActionResult> DashboardAgencyAPI(string clientId)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            return await ViewAgency(true, clientId);
        }



        private async Task<IActionResult> ViewAgency(
            bool returnStringContent,
            string clientId = "")
        {
            // Permission Clause
            var isAuthorized = (!(await AuthorizeAsync(Policy.ManageAgencySettings)).Succeeded
                                 && !(await AuthorizeAsync(Policy.Administrator)).Succeeded);

            // Guard Clause
            if (!isAuthorized)
                return returnStringContent ? NegativeFeedback()
                                           : ViewDanger();



            Data.Core.Domain.Business.Client client = null;


            // SilkFlo administrators can view other client details.
            if ((await AuthorizeAsync(Policy.Administrator)).Succeeded)
                client = await _unitOfWork.BusinessClients.GetAsync(clientId);


            client ??= await GetClientAsync();


            // Permission Clause
            if (client == null
                || client.TypeId != Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString()
                && client.TypeId != Data.Core.Enumerators.ClientType.ResellerAgency45.ToString())
            {
                return returnStringContent ? NegativeFeedback()
                    : ViewDanger();
            }



            const string url = "/Views/Shared/Dashboard/Page/_Agency.cshtml";
            var home = new ViewModels.Home
            {
                AgencyDashboard = new Agency
                {
                    Title = client.Name
                }
            };

            // Return the view
            if (returnStringContent)
            {
                var html = await _viewToString.PartialAsync(url, home);
                return Content(html);
            }

            return View(url, home);
        }




        private async Task<IActionResult> View(
            bool returnStringContent,
            string currentPath = "",
            string clientId = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(clientId))
                    await SaveProductCookieAsync(clientId);


                var client = await GetClientAsync();

                // Permission Clause
                if (client == null)
                    return returnStringContent ? NegativeFeedback()
                        : ViewDanger();
                

                var viewModel = new ViewModels.Dashboard.Page.Home();

                // Set tab visibility
                if (client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                {
                    const string displayPath = "Dashboard/Referrer";
                    var isActive = string.IsNullOrWhiteSpace(currentPath)
                                   || string.Equals(
                                       currentPath, 
                                       displayPath, 
                                       StringComparison.CurrentCultureIgnoreCase);
                    viewModel.TabBar.Tabs.Add(new ViewModels.Elements.TabBar.Tab
                    {
                        Title = "Agency",
                        Name = "Dashboard.Referrer",
                        IsActive = isActive,
                        GetOnSelect = !isActive,
                        DisplayPath = displayPath,
                        ParentId = "dashboard",
                        LoadOnce = true,
                        Sort = 2
                    });
                }
                else if ((client.TypeId == Data.Core.Enumerators.ClientType.ResellerAgency45.ToString()))
                {
                    var displayPath = "Dashboard/Personal";
                    var isActive = string.IsNullOrWhiteSpace(currentPath)
                                   || string.Equals(
                                       currentPath,
                                       displayPath,
                                       StringComparison.CurrentCultureIgnoreCase);
                    viewModel.TabBar.Tabs.Add(new ViewModels.Elements.TabBar.Tab
                    {
                        Title = "Personal",
                        Name = "Dashboard.Personal",
                        IsActive = isActive,
                        GetOnSelect = !isActive,
                        DisplayPath = "Dashboard/Personal",
                        ParentId = "dashboard",
                        LoadOnce = true,
                        Sort = 0
                    });

                    displayPath = "Dashboard/Reseller";
                    isActive = string.Equals(
                        currentPath,
                        displayPath,
                        StringComparison.CurrentCultureIgnoreCase);
                    viewModel.TabBar.Tabs.Add(new ViewModels.Elements.TabBar.Tab
                    {
                        Title = "Agency",
                        Name = "Dashboard.Reseller",
                        IsActive = isActive,
                        GetOnSelect = !isActive,
                        DisplayPath = "Dashboard/Reseller",
                        ParentId = "dashboard",
                        LoadOnce = true,
                        Sort = 2
                    });

                    displayPath = "Dashboard/Performance";
                    isActive = string.Equals(
                        currentPath,
                        displayPath,
                        StringComparison.CurrentCultureIgnoreCase);
                    viewModel.TabBar.Tabs.Add(new ViewModels.Elements.TabBar.Tab
                    {
                        Title = "Performance",
                        Name = "Dashboard.Performance",
                        IsActive = isActive,
                        GetOnSelect = !isActive,
                        DisplayPath = "Dashboard/Performance",
                        ParentId = "dashboard",
                        LoadOnce = true,
                        Sort = 1
                    });
                }
                else
                {
                    var displayPath = "Dashboard/Personal";
                    var isActive = string.IsNullOrWhiteSpace(currentPath)
                                   || string.Equals(
                                       currentPath,
                                       displayPath,
                                       StringComparison.CurrentCultureIgnoreCase);
                    viewModel.TabBar.Tabs.Add(new ViewModels.Elements.TabBar.Tab
                    {
                        Title = "Personal",
                        Name = "Dashboard.Personal",
                        IsActive = isActive,
                        GetOnSelect = !isActive,
                        DisplayPath = "Dashboard/Personal",
                        ParentId = "dashboard",
                        LoadOnce = true,
                        Sort = 0
                    });
                    if ((await AuthorizeAsync(Policy.ViewTenantDashboards)).Succeeded)
                    {
                        displayPath = "Dashboard/Performance";
                        isActive = string.Equals(
                                           currentPath,
                                           displayPath,
                                           StringComparison.CurrentCultureIgnoreCase);
                        viewModel.TabBar.Tabs.Add(new ViewModels.Elements.TabBar.Tab
                        {
                            Title = "Performance",
                            Name = "Dashboard.Performance",
                            IsActive = isActive,
                            GetOnSelect = !isActive,
                            DisplayPath = "Dashboard/Performance",
                            ParentId = "dashboard",
                            LoadOnce = true,
                            Sort= 1
                        });
                    }
                }

                viewModel.TabBar.Tabs = viewModel.TabBar.Tabs.OrderBy(x => x.Sort).ToList();

                const string url = "/Views/Shared/Dashboard/Page/_Home.cshtml";

                // Return the view
                if (!returnStringContent)
                    return View(url, viewModel);


                var html = await _viewToString.PartialAsync(url, viewModel);
                return Content(html);
            }
            catch (Exception e)
            {
                _unitOfWork.Log(e);
                const string message = "Error: Get data from data store.";
                return returnStringContent ? 
                    NegativeFeedback(message) :
                    ViewDanger(message);
            }
        }




        private async Task<IActionResult> PersonalViewAsync(
            bool returnStringContent,
            string clientId = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(clientId))
                    await SaveProductCookieAsync(clientId);

                var client = await GetClientAsync();


                // Permission Clause
                if (client == null)
                {
                    return returnStringContent
                        ? NegativeFeedback()
                        : ViewDanger();
                }


                // Permission Clause
                if (client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                {
                    const string message = "Error: Referrer agencies cannot have personal views.";
                    return returnStringContent
                        ? NegativeFeedback(message)
                        : ViewDanger(message);
                }



                // Do the business
                const string url = "/Views/Shared/Dashboard/Page/_Personal.cshtml";

                if (!returnStringContent) 
                    return View(url);

                var html = await PartialAsync(url);
                return Content(html);
            }
            catch (Exception e)
            {
                _unitOfWork.Log(e);
                const string message = "Error: Get data from data store.";
                return returnStringContent ?
                    NegativeFeedback(message) :
                    ViewDanger(message);
            }
        }


        private async Task<IActionResult> PerformanceViewAsync(
            bool returnStringContent)
        {
            try
            {
                var client = await GetClientAsync();

                // Permission Clause
                if (client == null)
                {
                    const string message = "<h1 class=\"text-danger\">Error: Unauthorised</h1>";
                    return returnStringContent
                        ? Content(message)
                        : PageView(message);
                }


                // Permission Clause
                if (client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                {
                    const string message = "<h1 class=\"text-danger\">Error: Referrer agencies cannot have personal views.</h1>";
                    return returnStringContent
                        ? Content(message)
                        : PageView(message);
                }

				var tenant = await GetClientAsync();

				if (tenant == null)
					return Content("");

				// Do the business
				var url = "/Views/Shared/Dashboard/Page/_Tenant.cshtml";

                await _unitOfWork.BusinessIdeas.GetForClientAsync(client);
                var models = Models.Business.Idea.Create(client.Ideas);
                foreach (var model in models)
                {
                    var ideaStages =
                        (await (_unitOfWork.BusinessIdeaStages
                            .FindAsync(x => x.IdeaId == model.Id && x.IsInWorkFlow)))
                        .ToList();

                    await _unitOfWork.SharedStages.GetStageForAsync(ideaStages);
                    model.IdeaStages = Models.Business.IdeaStage.Create(ideaStages);
                    model.IdeaStages = model.IdeaStages.OrderBy(x => x.DateStartEstimate).ToList();
                }

                models = models.Where(x => !x.IsDraft).ToList();

                var years = new List<int>();

                foreach (var model in models)
                {
                    var ideaStage = model.LastIdeaStage;
                    if (ideaStage == null)
                        continue;

                    var date = ideaStage.DateStart ?? ideaStage.DateStartEstimate;
                    var year = date.Year;
                    if (years.All(x => x != year))
                        years.Add(year);
                }

                if (!years.Any())
                    years.Add(DateTime.Now.Year);

                var processOwnerList = new List<KeyValuePair<string, string>>();
				var ideaSubmitterList = new List<KeyValuePair<string, string>>();
				foreach (var x in models)
                {
                    var idea = x.GetCore();
					await _unitOfWork.Users.GetProcessOwnerForAsync(idea);
					
                    if(!processOwnerList.Any(x => x.Value == idea.ProcessOwnerId))
                        processOwnerList.Add(new KeyValuePair<string, string>(idea.ProcessOwner.Fullname, idea.ProcessOwnerId));

					if (!ideaSubmitterList.Any(x => x.Value == idea.ProcessOwnerId))
						ideaSubmitterList.Add(new KeyValuePair<string, string>(idea.ProcessOwner.Fullname, idea.ProcessOwnerId));
				}

				// models.Select(x => new KeyValuePair<string, string>(x.CreatedBy, x.CreatedById)).ToList();
				//foreach (var y in models)
				//{
				//	var idea = await _unitOfWork.BusinessIdeas.GetAsync(y.GetCore().Id);
				//	processOwnerList.Add(new KeyValuePair<string, string>(idea.CreatedBy.Fullname, idea.CreatedById));
				//}

				var viewModel = new Tenant
                {
                    ClientName = client.Name,
                    IsPractice = client.IsPractice,
                    Years = years,
                    POList = processOwnerList,
                    ISList = ideaSubmitterList
				};

				// Get departments
				var departmentsCore = await _unitOfWork.BusinessDepartments.FindAsync(x => x.ClientId == tenant.Id);
				foreach (var department in departmentsCore)
					viewModel.Departments.Add(new Models.Business.Department(department));

				if (client.Ideas.Count == 0)
                    viewModel.NoIdeas = true;


                // Return the view
                if (!returnStringContent)
                    return View(url, viewModel);


                var html = await _viewToString.PartialAsync(url, viewModel);
                return Content(html);
            }
            catch (Exception e)
            {
                _unitOfWork.Log(e);
                const string message = "<h1 class=\"text-danger\">Error: Get data from data store.</h1>";
                return returnStringContent ?
                    Content(message) :
                    PageView(message);
            }
        }


        [Route("api/Dashboard/GetMyIdeas")]
        public async Task<IActionResult> GetMyIdeas()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Content("");

            var tenant = await GetClientAsync();


            var filter = new ViewModels.Business.Idea.FilterCriteria
            {
                UserRelationship = ViewModels.Business.Idea.UserRelationship.MyIdeas
            };

            var ideas = await Models.Business
                .Idea
                .GetForCardsAsync(_unitOfWork,
                    GetUserId(),
                    tenant,
                    filter,
                    this,
                    true);



            var viewModel = new ViewModels.Business.Idea.Cards
            {
                Title = "My Ideas",
                Ideas = ideas,
                ShowFilter = false,
                ShowSort = false,
                Wrap = false,
                HotSpotId = "My-Ideas-Cards"
            };

            var html = await _viewToString.PartialAsync("Shared/Business/Idea/_CardsContainer.cshtml",
                viewModel);

            return Content(html);
        }



        [Route("api/Dashboard/GetMyCollaborations")]
        public async Task<IActionResult> GetMyCollaborations()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Content("");

            var tenant = await GetClientAsync();

            var filter = new ViewModels.Business.Idea.FilterCriteria
            {
                UserRelationship = ViewModels.Business.Idea.UserRelationship.MyCollaborations
            };

            var ideas = await Models.Business
                .Idea
                .GetForCardsAsync(_unitOfWork,
                    GetUserId(),
                    tenant,
                    filter,
                    this);

            var viewModel = new ViewModels.Business.Idea.Cards
            {
                Title = "My Collaborations",
                Ideas = ideas,
                ShowFilter = false,
                ShowSort = false,
                Wrap = false,
                ShowNoIdeasCard = false,
                HotSpotId= "My-Collaborations-Cards"
            };

            var html = await _viewToString.PartialAsync("Shared/Business/Idea/_CardsContainer.cshtml",
                viewModel);

            return Content(html);
        }
    }
}
