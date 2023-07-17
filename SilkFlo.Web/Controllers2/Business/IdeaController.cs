using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SilkFlo.Web.Controllers2.FileUpload;
//using SilkFlo.ThirdPartyServices.AzureServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Controllers.Business
{
    public partial class IdeaController : AbstractAPI
    {
        protected IAzureStorage _storage;
        protected IWebHostEnvironment _env;

        public IdeaController(Data.Core.IUnitOfWork unitOfWork,
                              Services.ViewToString viewToString,
                              IAuthorizationService authorization, IAzureStorage storage, IWebHostEnvironment env) : base(unitOfWork, viewToString, authorization)
        {
            _storage = storage;
            _env = env;
        }

        public IAzureStorage AzureStorage => _storage;

        [HttpGet("/Idea/Detail/{id}")]
        [HttpGet("/api/Idea/Detail/{id}")]
        [HttpGet("/Idea/Detail/IdeaId/{id}")]
        [HttpGet("/Idea/Detail/IdeaId/{id}/{tab}")]

        public async Task<IActionResult> Detail(string id, string tab = "")
        {
            try
            {
                // Check Authorization
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                {
                    if (Request.Path.ToString().ToLower().IndexOf("/api", StringComparison.Ordinal) == 0)
                        return await PageApiAsync("<h1 class=\"text-danger\">Error: Unauthorised</h1>");

                    return Redirect("/account/signin");
                }

                var idea = await GetDetailIdeaAsync(id);

                await _unitOfWork.BusinessDocuments.GetForIdeaAsync(idea.GetCore());

                if (idea == null)
                    return Redirect("~/");

                if ((idea.LastIdeaStage == null || idea.LastIdeaStage.StageId == Data.Core.Enumerators.Stage.n00_Idea.ToString())
                   && !(await AuthorizeAsync(Policy.ReviewNewIdeas)).Succeeded)
                    return await PageApiAsync("<h1 class=\"text-danger\">You do not have permissions to review new ideas.</h1>");

                if (tab.ToLower() == "costbenefitanalysis"
                   && !(await AuthorizeAsync(Policy.ViewCostInfoInAutomationPipeline)).Succeeded)
                    return await PageApiAsync("<h1 class=\"text-danger\">You do not have to view cost info in automation pipeline.</h1>");

                idea.IsReadOnly = true;

                var viewModel = new ViewModels.Business.Idea.Detail.Page(
                    idea,
                    tab);


                return await ViewOrContent("/Views/Business/Idea/Detail.cshtml", viewModel);
            }
            catch (Exception e)
            {
                Log(e);
                throw;
            }

        }

        [HttpGet("/Idea/Edit")]
        [HttpGet("/Idea/Edit/{id}")]
        [HttpGet("/api/Idea/Edit")]
        [HttpGet("/api/Idea/Edit/{id}")]
        public async Task<IActionResult> Edit(string id = "")
        {
            // Check Authorization
            const string unauthorizedMessage = "<h1 class=\"text-danger\">Unauthorised</h1>";

            // Permission Clause
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
            {
                if (Request.Path.ToString().ToLower().IndexOf("/api", StringComparison.Ordinal) == 0)
                    return await PageApiAsync(unauthorizedMessage);

                return Redirect("/account/signin");
            }


            // Permission Clause
            if (string.IsNullOrWhiteSpace(id)
                && !(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
            {
                const string message = "<h1 class=\"text-warning\">You do not have permission to create an centre of excellence driven automation idea.</h1>";
                if (Request.Path.ToString().ToLower().IndexOf("/api", StringComparison.Ordinal) == 0)
                    return await PageApiAsync(message);

                return PageView(message);
            }

            var model = await GetEditIdeaAsync(
                id,
                false,
                true);

            if (model == null)
            {
                const string message = "<h1 class=\"text-warning\">Idea not found</h1>";
                if (Request.Path.ToString().ToLower().IndexOf("/api", StringComparison.Ordinal) == 0)
                    return await PageApiAsync(message);

                return PageView(message);
            }


            // Permission Clause
            if (!(await AuthorizeAsync(Policy.ReviewNewIdeas)).Succeeded
                && !(await AuthorizeAsync(Policy.ReviewAssessedIdeas)).Succeeded
                && !(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded
                && !model.IsNew)
            {
                const string message = "<h1 class=\"text-warning\">You do not have permission to access this idea.</h1>";
                if (Request.Path.ToString().ToLower().IndexOf("/api", StringComparison.Ordinal) == 0)
                    return await PageApiAsync(message);

                return PageView(message);
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                var client = await GetClientAsync();
                var clientModel = new Models.Business.Client(client);

                var message = await CanAddProcess(
                    clientModel,
                    "Cannot add additional process ideas.");

                if (!string.IsNullOrWhiteSpace(message))
                {
                    message = $"<span class=\"text-warning\">{message}</span";

                    if (Request.Path.ToString().ToLower().IndexOf("/api", StringComparison.Ordinal) == 0)
                        return await PageApiAsync(message);

                    return PageView(message);
                }
            }


            model.ReturnURL = GetReturnApiUrl();
            model.SubmissionPathId = Data.Core.Enumerators.SubmissionPath.COEUser.ToString();


            return await ViewOrContent("/Views/Business/Idea/Edit.cshtml", model);
        }

        [HttpGet("api/Idea/Collaborator/Edit/{id}")]
        public async Task<IActionResult> CollaboratorEdit(string id)
        {
            try
            {
                // Check Authorization
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                {
                    return NegativeFeedback("Error: Unauthorised");
                }

                var model = await GetEditIdeaAsync(
                    id,
                    false,
                    false);

                if (model == null)
                    return NegativeFeedback("Error: Unauthorised");



                var collaborator = new ViewModels.Business.Idea.ManageCollaborator.CollaboratorList
                {
                    Idea = model,
                    CollaboratorLimit = model.CollaboratorLimit,
                    CollaboratingUsers = model.CollaboratingUsers,
                    ParentFormId = "Detail_BusinessIdea",
                    CanScroll = false
                };

                return await ViewOrContent(
                    "/Views/Shared/Business/Idea/Edit/_CollaboratorContainer.cshtml",
                    collaborator);

            }
            catch (Exception e)
            {
                _unitOfWork.Log(e);
                return NegativeFeedback("Error: Getting the collaborator from the data store."); ;
            }
        }

        private string GetReturnApiUrl()
        {
            var returnUrl = Request.Headers["Referer"].ToString();
            returnUrl = returnUrl.Replace("https://", "");
            returnUrl = returnUrl.Replace("http://", "");


            if (returnUrl.Length < 2)
                return "/dashboard";
            else
            {
                var start = returnUrl.IndexOf("/", StringComparison.Ordinal);
                returnUrl = returnUrl[start..];

                return returnUrl;
            }
        }

        private async Task<Models.Business.Idea> GetEditIdeaAsync(
            string id,
            bool includeStatusHistory,
            bool createIfNotFound)
        {
            var idea = await GetIdeaAsync(
                id,
                includeStatusHistory,
                createIfNotFound);

            // Guard Clause
            if (idea == null)
                return null;

            await idea.GetLists();
            await GetGanttChartStages(idea);
            return idea;
        }

        private async Task<Models.Business.Idea> GetDetailIdeaAsync(string id)
        {
            var model = await GetIdeaAsync(
                id,
                false,
                false);

            // Guard Clause
            if (model == null)
                return null;


            if (!model.IsDraft)
                await _unitOfWork.SharedSubmissionPaths.GetSubmissionPathForAsync(model.GetCore());

            await GetAutomationMatrixData(model);
            await GetGanttChartStages(model);

            return model;
        }

        private async Task GetAutomationMatrixData(Models.Business.Idea model)
        {
            await GetBenefitPerCompany_Hours_RelativePercent(model);
            model.UnitOfWork = _unitOfWork;

            await model.GetEaseOfImplementationAsync();
            await model.GetAutomationPotentialAsync();

            if (model.EaseOfImplementationValue < 45)
                model.BenefitPerCompanyHoursCssStyle = "fill: var(--bs-danger); stroke: none; opacity:0.5;";
            else if (model.EaseOfImplementationValue < 55)
                model.BenefitPerCompanyHoursCssStyle = "fill: var(--bs-warning); stroke: none; opacity:0.3;";
            else
                model.BenefitPerCompanyHoursCssStyle = "fill: var(--bs-success); stroke: none; opacity:0.5;";

        }

        private async Task GetGanttChartStages(Models.Business.Idea model)
        {
            model.GanttIdeaStages = await GetGanttChartStages(model.Id);
        }

        private async Task<List<Models.Business.IdeaStage>> GetGanttChartStages(string ideaId)
        {
            var stages = (await _unitOfWork.SharedStages.FindAsync(x => !x.SetDateAutomatically)).ToArray();


            var cores = (await _unitOfWork.BusinessIdeaStages
                                                               .FindAsync(x => x.IdeaId == ideaId
                                                                        && stages.Any(y => y.Id == x.StageId))).ToList();



            var modelStages = Models.Shared.Stage.Create(stages);

            var ideaStages = Models.Business.IdeaStage.Create(cores);

            foreach (var ideaStage in ideaStages)
                foreach (var modelStage in modelStages.Where(modelStage => ideaStage.StageId == modelStage.Id))
                    ideaStage.Stage = modelStage;

            ideaStages = ideaStages.OrderBy(x => x.Stage.Sort).ToList();

            return ideaStages;
        }


        private async Task GetBenefitPerCompany_Hours_RelativePercent(Models.Business.Idea model)
        {
            await _unitOfWork.BusinessClients.GetClientForAsync(model.GetCore());
            var client = model.Client;

            if (client == null)
                return;


            await _unitOfWork.BusinessIdeas.GetForClientAsync(client.GetCore());

            var ideas = client.Ideas;

            GetBenefitPerCompany_Hours_RelativePercent(model, ideas);
        }

        private void GetBenefitPerCompany_Hours_RelativePercent(Models.Business.Idea model,
                                                                List<Models.Business.Idea> ideas)
        {
            decimal max = 0;
            foreach (var idea in ideas)
            {
                var value = idea.GetBenefitPerEmployee_Hours();
                if (max < value)
                    max = value;
            }

            var hours = model.BenefitPerCompanyHoursValue ?? 0;

            if (max != 0)
                model.BenefitPerCompanyHoursRelativePercent = hours / max * 100;
        }

        private async Task<Models.Business.Idea> GetIdeaAsync(
            string id,
            bool includeStatusHistory,
            bool createIfNotFound)
        {
            var userId = GetUserId();

            // Guard Clause
            if (userId == null)
                return null;


            var user = await _unitOfWork.Users
                                        .SingleOrDefaultAsync(x => x.Id == userId);

            // Guard Clause
            if (user == null)
                return null;


            var client = await GetClientAsync();


            // Guard Clause
            if (client == null)
                return null;

            Data.Core.Domain.Business.Idea idea = null;


            if (!string.IsNullOrWhiteSpace(id))
                idea = await _unitOfWork.BusinessIdeas
                                        .SingleOrDefaultAsync(x => x.Id == id);


            if (idea == null)
            {
                if (createIfNotFound)
                {
                    idea = new Data.Core.Domain.Business.Idea
                    {
                        Client = client,
                        IsDraft = true,
                        ProcessOwner = user
                    };
                }
                else
                    return null;
            }

            var model = new Models.Business.Idea(idea)
            {
                UnitOfWork = _unitOfWork
            };

            var product = GetProductCookie();
            await model.GetMetaData_Detail(
                product,
                includeStatusHistory,
                userId);

            await model.GetLikes(userId);

            model.CanEditStages = (await AuthorizeAsync(Policy.EditIdeasStageAndStatus)).Succeeded;



            model.CanEditAbout = (await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded
                                 || IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditAbout, idea.Id, userId)
                                 || model.ProcessOwnerId == user.Id;

            model.CanEditProcessOwner = (await AuthorizeAsync(Policy.AssignProcessOwner)).Succeeded;


            model.ShowDetailedAssessmentFields = (await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded;

            model.CollaboratorLimit = GetCollaboratorLimit();

            return model;
        }



        [HttpGet("/api/Idea/Modal")]
        public async Task<IActionResult> Modal()
        {
            const string deniedPath = "/Views/Shared/_AccessDenied_Modal.cshtml";


            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return await ViewOrContent(
                    deniedPath,
                    "<span class=\"text-danger\">Unauthorised</span>");


            var client = await GetClientAsync();

            if (client == null)
                return await ViewOrContent(
                    deniedPath,
                    "<span class=\"text-danger\">Could not find the client on the database.</span>");


            await _unitOfWork.BusinessDepartments.GetForClientAsync(client);
            if (client.Departments.Count == 0)
                return await ViewOrContent(
                    deniedPath,
                    "<div class=\"text-warning\" style=\"margin-bottom: 1rem !important;\">Please add some departments before adding an idea.</div><div><a href=\"/Settings/tenant/Platform-Setup/Business-Units\">Settings > Platform-Setup >Business-Units</a></div>");


            var userId = GetUserId();


            var user = await _unitOfWork.Users.GetAsync(userId);

            if (user == null)
                return await ViewOrContent(
                    deniedPath,
                    "<span class=\"text-danger\">Could not find the user on the database.</span>");


            var idea = new Data.Core.Domain.Business.Idea
            {
                ClientId = client.Id,
                IsDraft = true,
                ProcessOwner = user,
            };

            var collaboratorLimit = GetCollaboratorLimit();

            var model = new Models.Business.Idea(idea);
            await model.GetMetaData_Modal(
                _unitOfWork,
                collaboratorLimit);


            return await ViewOrContent("/Views/Shared/Business/Idea/_SubmitIdea_Modal.cshtml", model);
        }

        [HttpPost("/api/Idea/PostFollow")]
        [Authorize]
        public async Task<IActionResult> PostFollow([FromBody] dynamic data)
        {
            try
            {
                if (data == null)
                    return Ok();

                var userId = GetUserId();

                if (userId == null)
                    return Ok();


                var ideaIdNode = data["ideaId"];
                var isSelectedNode = data["isSelected"];
                var ideaId = ideaIdNode.ToString();
                bool.TryParse(isSelectedNode.ToString(), out bool isSelected);

                var follow = await _unitOfWork.BusinessFollows
                                              .SingleOrDefaultAsync(x => x.IdeaId == ideaId
                                              && x.UserId == userId);
                if (isSelected)
                {
                    if (follow != null) return Ok();

                    var core = new Data.Core.Domain.Business.Follow
                    {
                        IdeaId = ideaId,
                        UserId = userId,
                    };

                    await _unitOfWork.AddAsync(core);
                    await _unitOfWork.CompleteAsync();
                }
                else
                {
                    if (follow == null) return Ok();

                    await _unitOfWork.BusinessFollows.RemoveAsync(follow);
                    await _unitOfWork.CompleteAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                return Ok();
            }
        }

        [HttpPost("/api/Idea/PostVote")]
        [Authorize]
        public async Task<IActionResult> PostVote([FromBody] dynamic data)
        {
            try
            {
                if (data == null)
                    return Ok();

                var userId = GetUserId();

                if (userId == null)
                    return Ok();

                var ideaIdNode = data["ideaId"];
                var isSelectedNode = data["isSelected"];
                var ideaId = ideaIdNode.ToString();
                bool.TryParse(isSelectedNode.ToString(), out bool isSelected);

                var vote = await _unitOfWork.BusinessVotes
                                              .SingleOrDefaultAsync(x => x.IdeaId == ideaId
                                              && x.UserId == userId);
                if (isSelected)
                {
                    if (vote == null)
                    {
                        var core = new Data.Core.Domain.Business.Vote
                        {
                            IdeaId = ideaId,
                            UserId = userId,
                        };

                        await _unitOfWork.AddAsync(core);
                        await _unitOfWork.CompleteAsync();
                    }
                }
                else
                {
                    if (vote != null)
                    {
                        await _unitOfWork.BusinessVotes.RemoveAsync(vote);
                        await _unitOfWork.CompleteAsync();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                return Ok();
            }
        }
    }
}