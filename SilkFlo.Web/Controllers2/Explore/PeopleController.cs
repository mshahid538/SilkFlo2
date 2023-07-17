using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Controllers.Explore
{
    public class PeopleController : AbstractAPI
    {
        public PeopleController(Data.Core.IUnitOfWork unitOfWork,
                              Services.ViewToString viewToString,
                              IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }


        [Route("/Explore/People")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return await View(false);
        }

        [Route("/api/Explore/People")]
        public async Task<IActionResult> IndexApi()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Content("Unauthorised");

            return await View(true);
        }

        private async Task<IActionResult> View(bool returnStringContent)
        {
            var clientCore = await GetClientAsync();

            if (clientCore == null)
            {
                if(returnStringContent)
                    return NegativeFeedback();

                return Redirect("/account/signin");
            }

            var client = new Models.Business.Client(clientCore);

            var locations = (await _unitOfWork.BusinessLocations.FindAsync(x => x.ClientId == client.Id)).ToArray();
            var departments = (await _unitOfWork.BusinessDepartments.FindAsync(x => x.ClientId == client.Id)).ToArray();

            // Prepare the viewmodel
            var viewModel = new ViewModels.Explore.People
            {
                IsPractice = client.IsPractice,
                NoLocations = !locations.Any(),
                NoBusinessUnits = !departments.Any(),
            };


            var users = (await _unitOfWork.Users.FindAsync(x => x.ClientId == client.Id)).ToArray();
            await _unitOfWork.UserRoles.GetForUserAsync(users);

            users = users.Where(x => x.UserRoles.Count > 0).ToArray();

            await _unitOfWork.BusinessIdeas.GetForProcessOwnerAsync(users);
            await _unitOfWork.BusinessDepartments.GetDepartmentForAsync(users);

            viewModel.Users = Models.User.Create(users);
            viewModel.Users = viewModel.Users.Where(x => x.Status == Models.Status.Active).ToArray();



            // Return the view.
            string url = "/Views/Explore/People/Index.cshtml";
            if (returnStringContent)
            {
                string html = await _viewToString.PartialAsync(url, viewModel);
                return Content(html);
            }

            return View(url, viewModel);
        }


        [HttpGet("api/Explore/People/Table")]
        [HttpGet("api/Explore/People/Table/FirstId/{id}")]
        [HttpGet("api/Explore/People/Table/Search/{text}")]
        public async Task<IActionResult> GetTable(
            string text = "")
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync();

                if (client == null)
                    return NegativeFeedback();

                var locations = (await _unitOfWork.BusinessLocations.FindAsync(x => x.ClientId == client.Id)).ToArray();
                var departments = (await _unitOfWork.BusinessDepartments.FindAsync(x => x.ClientId == client.Id)).ToArray();


                var users = (await _unitOfWork.Users.FindAsync(x => x.ClientId == client.Id)).ToArray();
                await _unitOfWork.UserRoles.GetForUserAsync(users);

                users = users.Where(x => x.UserRoles.Count > 0).ToArray();

                await _unitOfWork.BusinessIdeas.GetForProcessOwnerAsync(users);
                await _unitOfWork.BusinessDepartments.GetDepartmentForAsync(users);

                var models = Models.User.Create(users);
                models = models.Where(x => x.Status == Models.Status.Active).ToArray();

                if (!string.IsNullOrWhiteSpace(text))
                {
                    text = text.Trim().ToLower();
                    var exactMatchRequired = false;
                    if (text.IndexOf("\"", StringComparison.Ordinal) == 0
                        && text.IndexOf("\"", 1, StringComparison.Ordinal) > 1)
                    {
                        exactMatchRequired = true;
                        text = text.Substring(1);
                        text = text.Substring(0, text.Length - 1);
                    }


                    if (exactMatchRequired)
                        models = models.Where(x => x.Fullname.ToLower() == text
                                                   || x.About.ToLower() == text
                                                   || x.Note.ToLower() == text
                                                   || x.Department?.Name.ToLower() == text).ToArray();
                    else
                        models = models.Where(x => x.Fullname.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.About.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.Note.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.Department?.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1).ToArray();
                }






                // Prepare the viewmodel
                var viewModel = new ViewModels.Explore.People
                {
                    IsPractice = client.IsPractice,
                    NoLocations = !locations.Any(),
                    NoBusinessUnits = !departments.Any(),
                    Users = models,
                };

                var html = await _viewToString.PartialAsync("Shared/Explore/People/_Table", viewModel);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error Logged");
            }
        }
    }
}