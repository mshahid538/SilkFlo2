using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Persistence;
using SilkFlo.Web.ViewModels;

namespace SilkFlo.Web.Controllers.Business
{
    public partial class IdeaController
    {
        [HttpGet("/api/business/Idea/GetSubmitIdeaButton")]
        public async Task<IActionResult> GetSubmitIdeaButton()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return BadRequest("Error: Unauthorised");


            var client = await GetClientAsync();


            // Guard Clause
            if (client == null
                || client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                return Content("");



            var result = await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas);

            var html = "";
            if (result.Succeeded)
            {
                // Submit CoE Driven Ideas 
                var viewModel = new SubmitIdeaButton("btnSubmit", "Submit Idea");
                html = await _viewToString.PartialAsync("Shared/_SubmitIdeaButtonCoE.cshtml",
                                                    viewModel);
            }
            else
            {
                result = await AuthorizeAsync(Policy.ShareEmployeeDrivenIdeas);
                if(result.Succeeded)
                {
                    var viewModel = new SubmitIdeaButton("btnSubmit",
                                                        "Submit Idea",
                                                        "",
                                                        false);
                    html = await _viewToString.PartialAsync("Shared/_SubmitIdeaButton.cshtml",
                                                      viewModel);
                }
            }



            if (string.IsNullOrWhiteSpace(html))
                html = $"<input type=\"hidden\" value=\"You do not have the following permission: '{Policy.SubmitCoEDrivenIdeas}' or '{Policy.ShareEmployeeDrivenIdeas}'\"/>";

            return Content(html);
        }


        [HttpGet("/api/business/Idea/GetSubmitIdeaDropDown/id/{id}")]
        public async Task<IActionResult> GetSubmitIdeaDropDown(string id)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return BadRequest("Error: Unauthorised");

            var tenantCore = await GetClientAsync();

            if (tenantCore == null)
                return NegativeFeedback();

            var tenant = new Models.Business.Client(tenantCore);

            if (tenant.IsAgency)
                return Content("");


            const string title = "Submit Your First Idea";

            var result = await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas);

            string html = "";
            if (result.Succeeded)
            {
                // Dropdown
                html = await this.PartialAsync("_SubmitIdeaDropDown.cshtml");
            }
            else
            {
                result = await AuthorizeAsync("Share Employee Driven Ideas");
                if (result.Succeeded)
                {
                    var viewModel = new SubmitIdeaButton(id,
                                                        title,
                                                        "",
                                                        false);
                    html = await _viewToString.PartialAsync("Shared/_SubmitIdeaButton.cshtml",
                                                      viewModel);

                }
            }

            return Content(html);
        }


        [HttpDelete("/api/business/Idea/Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return BadRequest("Error: Unauthorised");

            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    return BadRequest();

                var tenant = await GetClientAsync();

                var idea = await _unitOfWork.BusinessIdeas
                                      .SingleOrDefaultAsync(x => x.Id == id);

                if (idea.ClientId != tenant.Id)
                    return BadRequest();


                var result = await _unitOfWork.BusinessIdeas.RemoveAsync(idea);

                if (result == Data.Core.DataStoreResult.NotFound)
                    return BadRequest("Not Found");

                await _unitOfWork.CompleteAsync();
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet("/api/Business/Idea/GetManageStageAndStatus/modal/ideaId/{ideaId}")]
        public async Task<IActionResult> GetManageStageAndStatus(string ideaId)
        {
            const string deniedPath = "/Views/Shared/_AccessDenied_Modal.cshtml";


            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return await ViewOrContent(
                    deniedPath,
                    "<span class=\"text-danger\">Unauthorised</span>");

            try
            {
                var core = await _unitOfWork.BusinessIdeas.GetAsync(ideaId);

                if (core == null)
                    return BadRequest();


                var client = await GetClientAsync();
                var clientModel = new Models.Business.Client(client);

                var model = new Models.Business.Idea(core);

                var product = GetProductCookie();
                await model.ManageStageAndStatus.GetAsync(_unitOfWork, model, product, clientModel);


                if (!string.IsNullOrWhiteSpace(model.ManageStageAndStatus.ErrorMessage))
                {
                    return await ViewOrContent(
                        deniedPath,
                        $"<span class=\"text-warning\">{model.ManageStageAndStatus.ErrorMessage}</span>");
                }

                var html = await _viewToString.PartialAsync("Shared/Business/Idea/ManageStageAndStatus/_Modal.cshtml",
                                                         model.ManageStageAndStatus);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return await ViewOrContent(
                    deniedPath,
                    "<span class=\"text-danger\">System error logged</span>");

            }
        }





        [HttpPost("/api/Business/Idea/SaveStatus")]
        public async Task<IActionResult> PutStatusAsync([FromBody] ViewModels.Business.ManageStageAndStatus.Poster model)
        {
            var client = await GetClientAsync();

            var (feedback, saveMe) = await SaveStatusAsync(model, client);

            if(saveMe)
                await _unitOfWork.CompleteAsync();

            if (feedback.IsValid)
                return Ok();

            return BadRequest(feedback);
        }


        public async Task<(Feedback, bool)> SaveStatusAsync(
            ViewModels.Business.ManageStageAndStatus.Poster model,
            Client client)
        {
            var feedback = new Feedback
            {
                NamePrefix = "Business.IdeaStage."
            };


            if (!(await AuthorizeAsync(Policy.EditIdeasStageAndStatus)).Succeeded)
            {
                feedback.DangerMessage("Unauthorised");
                feedback.IsValid = false;
                return (feedback, false);
            }

            try
            {
                // Guard Clause
                if (model == null)
                {
                    feedback.DangerMessage("Model missing");
                    feedback.IsValid = false;
                    return (feedback, false);
                }

                // Guard Clause
                if (model.CurrentIdeaStage == null)
                {
                    feedback.DangerMessage("Model.CurrentIdeaStage missing");
                    feedback.IsValid = false;
                    return (feedback, false);
                }

                // Guard Clause
                if (model.NextIdeaStatus == null)
                {
                    feedback.DangerMessage("Model.NextIdeaStatus missing");
                    feedback.IsValid = false;
                    return (feedback, false);
                }


                var tenant = await GetClientAsync();

                // Guard Clause               
                if (tenant == null)
                {
                    feedback.DangerMessage("Client missing");
                    feedback.IsValid = false;
                    return (feedback, false);
                }


                if (!model.CurrentIdeaStage.IsAutomaticDate)
                {
                    if (model.CurrentIdeaStage.DateStartEstimate == null)
                        feedback.Add("DateStartEstimate", "Required");

                    
                    if (model.CurrentIdeaStage.DateEndEstimate == null 
                        && !model.CurrentIdeaStage.IsDeployedGroup)
                        feedback.Add("DateEndEstimate", "Required");

                    if (model.CurrentIdeaStage.DateStart == null)
                        feedback.Add("DateStart", "Required");

                    if (model.CurrentIdeaStage.DateEnd == null
                        && !model.CurrentIdeaStage.IsDeployedGroup)
                        feedback.Add("DateEnd", "Required");


                    if (feedback.IsValid)
                    {
                        if (model.CurrentIdeaStage.DateEndEstimate < model.CurrentIdeaStage.DateStartEstimate 
                            && !model.CurrentIdeaStage.IsDeployedGroup)
                            feedback.Add("DateEndEstimate", "The estimated end date is before estimated start date.");

                        if (model.CurrentIdeaStage.DateEnd < model.CurrentIdeaStage.DateStart
                            && !model.CurrentIdeaStage.IsDeployedGroup)
                            feedback.Add("DateEnd", "The end date is before start date.");
                    }
                }


                // Guard Clause
                if (!feedback.IsValid)
                    return (feedback, false);

                var lastIdeaStage = await Models.Business.IdeaStage.GatLast(_unitOfWork, model.IdeaId);
                var product = GetProductCookie();

                var isIdeaLimitReached = await model.IsIdeaLimitReached(
                                                _unitOfWork,
                                                lastIdeaStage.GetCore(),
                                                product,
                                                new Models.Business.Client(client));

                if (isIdeaLimitReached)
                {
                    feedback.IsValid = false;
                    feedback.Message = model.ErrorMessage;
                    return (feedback, false);
                }


                // Add records
                var ideaStage = await AddIdeaStageToWorkFlowAsync(model);

                var ideaStageStatus = await SaveIdeaStageStatusAsync(
                    model.NextIdeaStatus,
                    ideaStage);

                // Is save required?
                return (feedback, ideaStageStatus != null);
            }
            catch (Exceptions.IdeaStageNullException ex)
            {
                feedback.DangerMessage(ex.Message);
                return (feedback, false);
            }
            catch (Exceptions.DateBeforeException ex)
            {
                feedback.WarningMessage(ex.Message);
                return (feedback, false);
            }
            catch (Exception ex)
            {
                feedback.DangerMessage(ex.Message);
                return (feedback, false);
            }
        }




        private async Task<IdeaStage> AddIdeaStageToWorkFlowAsync(ViewModels.Business.ManageStageAndStatus.Poster model)
        {
            var ideaStage = (await _unitOfWork.BusinessIdeaStages
                    .FindAsync(x => x.IdeaId == model.IdeaId && x.StageId == model.CurrentIdeaStage.StageId && x.IsInWorkFlow))
                    .LastOrDefault() 
                ?? (await _unitOfWork.BusinessIdeaStages
                    .FindAsync(x => x.IdeaId == model.IdeaId && x.StageId == model.CurrentIdeaStage.StageId && !x.IsInWorkFlow))
                    .LastOrDefault();

            if (ideaStage == null)
                throw new Exceptions.IdeaStageNullException();

            if (!model.CurrentIdeaStage.IsAutomaticDate)
            {
                var ideaStagePrevious = (await _unitOfWork.BusinessIdeaStages
                        .FindAsync(x => x.IdeaId == model.IdeaId && x.IsInWorkFlow && x.Id != ideaStage.Id))
                    .OrderBy(x => x.Date)
                    .LastOrDefault();

                if (ideaStagePrevious != null
                    && ideaStagePrevious.DateStartEstimate > model.CurrentIdeaStage.DateStartEstimate)
                    throw new Exceptions.DateBeforeException(
                        "The estimated start date comes before the previous stages estimated start date.");

                if (model.CurrentIdeaStage.DateStartEstimate != null)
                    ideaStage.DateStartEstimate = (DateTime) model.CurrentIdeaStage.DateStartEstimate;
                
                ideaStage.DateStart = model.CurrentIdeaStage.DateStart;
                
                if (!model.CurrentIdeaStage.IsDeployedGroup)
                {
                    ideaStage.DateEndEstimate = model.CurrentIdeaStage.DateEndEstimate;
                    ideaStage.DateEnd = model.CurrentIdeaStage.DateEnd;
                }
            }



            if (ideaStage.StageId != model.NextIdeaStatus.StageId)
            {
                ideaStage = (await _unitOfWork.BusinessIdeaStages
                        .FindAsync(x => x.IdeaId == model.IdeaId && x.StageId == model.NextIdeaStatus.StageId && !x.IsInWorkFlow))
                        .FirstOrDefault();
            }

            if(ideaStage != null)
                ideaStage.IsInWorkFlow = true;
            
            return ideaStage;
        }


        private async Task<IdeaStageStatus> SaveIdeaStageStatusAsync(ViewModels.Business.ManageStageAndStatus.NextIdeaStatus nextIdeaStatus,
                                                                     IdeaStage ideaStage)
        {
            // Guard Clause
            if (nextIdeaStatus == null)
                throw new ArgumentNullException(nameof(nextIdeaStatus));


            // Guard Clause
            if (ideaStage == null)
                throw new ArgumentNullException(nameof(ideaStage));





            var ideaStageStatuses = (await _unitOfWork.BusinessIdeaStageStatuses
                .FindAsync(x => x.IdeaStageId == ideaStage.Id)).ToArray();

            var addNew = false;
            IdeaStageStatus ideaStageStatus;
            if (!ideaStageStatuses.Any())
                addNew = true;
            else
            {
                ideaStageStatus = ideaStageStatuses.OrderBy(x => x.Date).Last();
                if (ideaStageStatus.StatusId != nextIdeaStatus.StatusId)
                    addNew = true;
            }


            if (!addNew)
                return null;


            ideaStageStatus = new IdeaStageStatus
            {
                StatusId = nextIdeaStatus.StatusId,
                IdeaStageId = ideaStage.Id,
                Date = DateTime.Now
            };

            await _unitOfWork.AddAsync(ideaStageStatus);
            return ideaStageStatus;
        }




        [HttpGet("/api/Business/Idea/GetStageName/{ideaId}")]
        public async Task<IActionResult> GetStageName(string ideaId)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return BadRequest("Error: Unauthorised");

            try
            {
                if (string.IsNullOrWhiteSpace(ideaId))
                    return BadRequest();


                var ideaStage = await Models.Business.IdeaStage.GatLast(_unitOfWork, ideaId);
                if (ideaStage == null)
                    return Content("");

                await _unitOfWork.SharedStages.GetStageForAsync(ideaStage.GetCore());

                return Content(ideaStage.Stage?.Name ?? string.Empty);
            }
            catch (Exception ex)
            {
                Log(ex);
                return BadRequest();
            }
        }


        [HttpGet("/api/Business/Idea/GetStatusName/{ideaId}")]
        public async Task<IActionResult> GetStatusName(string ideaId)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return BadRequest("Error: Unauthorised");

            try
            {
                if (string.IsNullOrWhiteSpace(ideaId))
                    return BadRequest();


                var ideaStage = await Models.Business.IdeaStage.GatLast(_unitOfWork, ideaId);
                if (ideaStage == null)
                    return Content("");


                await _unitOfWork.BusinessIdeaStageStatuses.GetForIdeaStageAsync(ideaStage.GetCore());


                var ideaStageStatuses = new List<IdeaStageStatus>(ideaStage.GetCore().IdeaStageStatuses);
                ideaStageStatuses = ideaStageStatuses
                    .OrderBy(x => x.CreatedDate)
                    .ToList();

                if (ideaStageStatuses.Count == 0)
                    return Content("");

                var ideaStageStatus = ideaStageStatuses.Last();

                await _unitOfWork.SharedIdeaStatuses.GetStatusForAsync(ideaStageStatus);

                var content = $"<div class=\"pill {ideaStageStatus.Status?.ButtonClass}\" style=\"width: 100%; max-width: 190px;\">{ideaStageStatus.Status?.Name.Replace(" ", "\u00A0")}</div>";
                content = content.Replace("\"", "\\\"");

                var json = "{" +
                                    "\"content\": \"" + content + "\"," +
                                    "\"value\": \"" + ideaStageStatus.Status?.Name + "\"" +
                                "}";

                return Content(json);
            }
            catch (Exception ex)
            {
                Log(ex);
                return BadRequest();
            }
        }



        [HttpGet("/api/Business/Idea/GetSummary")]
        public async Task<IActionResult> GetSummary(DateTime? startDate, DateTime? endDate, bool? isWeekly, bool? isMonthly, bool? isYearly, string processOwners, string ideaSubmitters, string departmentsId, string teamsId)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return BadRequest("Error: Unauthorised");


            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var client = new Models.Business.Client(clientCore);

            await _unitOfWork.BusinessIdeas.GetForClientAsync(client.GetCore());


            return await RenderSummary(
                client.GetCore().Ideas, 
                false, 
                false, 
                "",
				startDate, endDate, isWeekly, isMonthly, isYearly, processOwners, ideaSubmitters, departmentsId, teamsId);
        }


        [HttpGet("api/business/idea/Detail/GetMeta/IdeaId/{ideaId}")]
        public async Task<IActionResult> GetMeta(string ideaId)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback("Error: Unauthorised");

            var core = await _unitOfWork.BusinessIdeas.GetAsync(ideaId);

            if(core == null)
                return NegativeFeedback("Error: Unauthorised");

            var model = new Models.Business.Idea(core);

            await _unitOfWork.SharedSubmissionPaths.GetSubmissionPathForAsync(core);
            await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(core);

            if (model.LastIdeaStage != null)
            {
                await _unitOfWork.BusinessIdeaStageStatuses.GetForIdeaStageAsync(model.LastIdeaStage.GetCore());
                var ideaStageStatus = model.LastIdeaStage.IdeaStageStatuses.OrderBy(x => x.CreatedDate).LastOrDefault();
                if (ideaStageStatus != null)
                {
                    await _unitOfWork.SharedIdeaStatuses.GetStatusForAsync(ideaStageStatus.GetCore());
                    model.LastIdeaStage.Status = ideaStageStatus.Status;
                }

                await _unitOfWork.SharedStages.GetStageForAsync(model.LastIdeaStage.GetCore());
            }

            var html = await _viewToString.PartialAsync("Shared/Business/Idea/Detail/_Meta", model);
            return Content(html);
        }

        private async Task<IActionResult> RenderSummary(
            IEnumerable<Idea> ideas,
            bool benefitHours,
            bool goal,
            string targetUrl,
			DateTime? startDate = null, DateTime? endDate = null, bool? isWeekly = null, bool? isMonthly = null, bool? isYearly = null, string processOwners = "", string ideaSubmitters = "", string departmentsId = "", string teamsId = "")
        {

            var summary = await ViewModels.Business.Idea.Summary.BuildAsync(
                _authorization,
                User);

            //var models = new List<Models.Business.Idea>();
            var product = GetProductCookie();

			var models = Models.Business.Idea.Create(ideas);

			if (isWeekly.HasValue && isWeekly.Value)
			{
				var previousWeekStartDate = DateTime.Now - TimeSpan.FromDays(7);
				models = models.Where(x => x.CreatedDate.Value.Date >= previousWeekStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
			}
			else if (isMonthly.HasValue && isMonthly.Value)
			{
				var previousMonthStartDate = DateTime.Now.AddMonths(-1);
				models = models.Where(x => x.CreatedDate.Value.Date >= previousMonthStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
			}
			else if (isYearly.HasValue && isYearly.Value)
			{
				var previousYearStartDate = DateTime.Now.AddYears(-1);
				models = models.Where(x => x.CreatedDate.Value.Date >= previousYearStartDate && x.CreatedDate.Value.Date <= DateTime.Now.Date).ToList();
			}
			else if (startDate.HasValue && endDate.HasValue)
			{
				models = models.Where(x => x.CreatedDate.Value.Date >= startDate.Value.Date && x.CreatedDate.Value.Date <= endDate.Value.Date).ToList();
			}

            if (!String.IsNullOrWhiteSpace(ideaSubmitters))
            {
                var isList = ideaSubmitters.Split(",");
                models = models.Where(x => isList.Contains(x.ProcessOwnerId)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(processOwners))
            {
                var poList = processOwners.Split(",");
                models = models.Where(x => poList.Contains(x.ProcessOwnerId)).ToList();
            }

			if (!String.IsNullOrWhiteSpace(departmentsId))
			{
				var departmentsIdList = departmentsId.Split(",");
				models = models.Where(x => departmentsIdList.Contains(x.DepartmentId)).ToList();
			}

			if (!String.IsNullOrWhiteSpace(teamsId))
			{
				var teamsIdList = teamsId.Split(",");
				models = models.Where(x => teamsIdList.Contains(x.TeamId)).ToList();
			}


			foreach (var model in models) //ideas)
            {
                //var model = new Models.Business.Idea(core)
                //{
                //    UnitOfWork = _unitOfWork
                //};
                model.UnitOfWork = _unitOfWork;
                await model.GetSummaryView_MetaDataAsync(product);

                //var isTeamMember = model.ProcessOwnerId == userId || 
                //                   model.Collaborators.Any(x => x.UserId == userId);
                //var isInIdeaStage = model.IsDraft ||
                //                    model.LastIdeaStage.StageId == Data.Core.Enumerators.Stage.n00_Idea.ToString();

                //var isInAccessStage = model.LastIdeaStage.StageId != Data.Core.Enumerators.Stage.n00_Idea.ToString();

                model.ShowManageStagesMenuItem = summary.ShowManageStagesMenuItem;
                model.ShowViewDetailsMenuItem = summary.ShowViewDetailsMenuItem;
                //model.ShowViewDetailsMenuItem = summary.ShowViewDetailsMenuItem || isTeamMember;

                //if ((isInIdeaStage
                //     && !(await AuthorizeAsync(Policy.ReviewNewIdeas)).Succeeded)
                //    || (isInAccessStage
                //        && !(await AuthorizeAsync(Policy.ReviewAssessedIdeas)).Succeeded))
                //{
                //    model.ShowViewDetailsMenuItem = false;
                //}


                model.ShowEditMenuItem = summary.ShowEditMenuItem;
                model.ShowDeleteMenuItem = summary.ShowDeleteMenuItem;

                //if (model.Show)
                //    models.Add(model);
            }

            summary.Ideas = models;
            summary.BenefitHours = benefitHours;
            summary.Goal = goal;
            summary.TargetUrl = targetUrl;

            var html = await _viewToString.PartialAsync(
                "Shared/Business/Idea/_Summary.cshtml",
                summary);

            return Content(html);
        }


        [HttpGet("/api/Business/Idea/CheckUniqueName/{name}")]
        public async Task<IActionResult> CheckUniqueName(string name)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return BadRequest("Error: Unauthorised");



            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return Content("false");

            var client = new Models.Business.Client(clientCore);



            var clone = await _unitOfWork.BusinessIdeas
                                            .SingleOrDefaultAsync(x => x.ClientId == client.Id
                                                                    && x.Name.ToLower() == name.ToLower());
            return Content(clone == null ? "true" : "false");
        }


        [Route("/api/Business/Idea/GetSummaryCards")]
        public async Task<IActionResult> GetSummaryCards()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();


            var filterCriteria = new ViewModels.Business.Idea.FilterCriteria
            {
                IsDeployedOnly = false,
                SortById = "NameAtoZ"
            };

            return await GetSummaryCards(filterCriteria, "<div class=\"TitleBar\">\r\n    <h3 style=\"margin-bottom: 2rem; color:#FF8A00\">Explore > Ideas</h3>\r\n</div>\r\n\t<div class=\"d-flex justify-content-between\" style=\"width: 441px;height: 25px;left: 263px;top: 173px;font-style: normal;font-weight: 650;font-size: 32px;line-height: 48px;display: flex;align-items: center;color: #363853; margin: 20px 0px 20px 0px;\">\r\n\t\t<h1 style=\"font-weight: bold;\">Ideas</h1>\r\n\t</div>");
        }


        [Route("/api/Business/Idea/GetDeployedSummaryCards")]
        public async Task<IActionResult> GetDeployedSummaryCards()
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return BadRequest("Error: Unauthorised");


            var filterCriteria = new ViewModels.Business.Idea.FilterCriteria
            {
                IsDeployedOnly = true,
                SortById = "NameAtoZ"
            };

            return await GetSummaryCards(filterCriteria, "<div class=\"TitleBar\">\r\n    <h3 style=\"margin-bottom: 2rem; color:#FF8A00\">Explore > Automations</h3>\r\n</div>\r\n\t<div class=\"d-flex justify-content-between\" style=\"width: 441px;height: 25px;left: 263px;top: 173px;font-style: normal;font-weight: 650;font-size: 32px;line-height: 48px;display: flex;align-items: center;color: #363853; margin: 20px 0px 20px 0px;\">\r\n\t\t<h1 style=\"font-weight: bold;\">Automations</h1>\r\n\t</div>");
		}

        private async Task<IActionResult> GetSummaryCards(ViewModels.Business.Idea.FilterCriteria filterCriteria,
                                                          string title)
        {
            try
            {
                var tenant = await GetClientAsync();

                if (tenant == null)
                    return Content("");



                var viewModel = new ViewModels.Business.Idea.Cards
                {
                    Title = title,
                    FilterCriteria = filterCriteria
                };

                var ideas = await Models.Business
                                        .Idea
                                        .GetForCardsAsync(_unitOfWork,
                                                          GetUserId(),
                                                          tenant,
                                                          filterCriteria,
                                                          this);

                viewModel.Ideas = ideas;

                // Get departments
                var departmentsCore = await _unitOfWork.BusinessDepartments.FindAsync(x => x.ClientId == tenant.Id);
                foreach (var department in departmentsCore)
                    viewModel.Departments.Add(new Models.Business.Department(department));

                // Get submissionPaths
                var submissionPaths = await _unitOfWork.SharedSubmissionPaths.GetAllAsync();
                foreach (var submissionPath in submissionPaths)
                    viewModel.SubmissionPaths.Add(new Models.Shared.SubmissionPath(submissionPath));


                // Get Versions
                var versions = (await _unitOfWork.BusinessVersions.FindAsync(x => x.ClientId == tenant.Id && x.IsLive)).ToArray();
                await _unitOfWork.BusinessApplications.GetApplicationForAsync(versions);
                versions = versions.OrderBy(x => x.Application.Name).ToArray();
                foreach (var version in versions)
                {
                    version.IdeaApplicationVersions = version.IdeaApplicationVersions.OrderBy(x => x.Version.Name).ToList();
                    viewModel.Versions.Add(new Models.Business.Version(version));
                }



                // Get Stages
                var stages = await _unitOfWork.SharedStages.GetAllAsync();
                foreach (var stage in stages)
                {
                    viewModel.Stages.Add(new Models.Shared.Stage(stage));
                }


                if (filterCriteria.IsDeployedOnly)
                {
                    var statuses = await _unitOfWork.SharedIdeaStatuses.FindAsync(x => x.StageId == "n7_Deployed");
                    foreach (var item in statuses)
                    {
                        viewModel.DeployedStatuses.Add(new Models.Shared.IdeaStatus(item));
                    }
                }

                var html = await _viewToString.PartialAsync("Shared/Business/Idea/_CardsContainer.cshtml",
                                                         viewModel);

                return Content(html);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                return Content("Error: Failed to compose content.");
            }
        }

        [HttpPost("/api/Business/Idea/ValidateFiltered")]
        public async Task<IActionResult> ValidateFiltered([FromBody] ViewModels.Business.Idea.FilterCriteria filterCriteria)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return BadRequest("Error: Unauthorised");

            try
            {
                var tenant = await GetClientAsync();
                if (tenant == null)
                    return Ok(new { Message = "No tenant selected" });

                var isFilterByTextApplicable = await Models.Business
                                        .Idea
                                        .GetForCardsByTextAsync(_unitOfWork,
                                                          GetUserId(),
                                                          tenant,
                                                          filterCriteria,
                                                          this);

                
                return Ok(new { payload = filterCriteria, isFilterByTextApplicable });
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                return Content("Error retrieving ideas");
            }
        }


        [HttpPost("/api/Business/Idea/GetFiltered")]
        public async Task<IActionResult> GetFiltered([FromBody] ViewModels.Business.Idea.FilterCriteria filterCriteria)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return BadRequest("Error: Unauthorised");

            try
            {
                var tenant = await GetClientAsync();

                if (tenant == null)
                    return Content("No tenant selected");


                //var filterCriteria = await GetModelAsync<ViewModels.Business.Idea.FilterCriteria>();


                var ideas = await Models.Business
                                        .Idea
                                        .GetForCardsAsync(_unitOfWork,
                                                          GetUserId(),
                                                          tenant,
                                                          filterCriteria,
                                                          this);


                var viewModel = new ViewModels.Business.Idea.Cards
                {
                    Count = ideas.Count,
                    Ideas = ideas,
                    FilterCriteria = filterCriteria
                };
                var html = await _viewToString.PartialAsync("Shared/Business/Idea/_Cards.cshtml",
                                                         viewModel);

                return Content(html);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                return Content("Error retrieving ideas");
            }
        }


        [HttpPost("/api/Business/Idea/Modal/Post")]
        public async Task<IActionResult> ModalPost([FromBody] ViewModels.Business.Idea.Modal model)
        {
            var feedback = new Feedback();


            // Guard Clause
            if (model == null)
            {
                feedback.DangerMessage("The model is missing");
                return BadRequest(feedback);
            }

            // Permission Clause
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
            {
                feedback.DangerMessage("Unauthorised");
                return BadRequest(feedback);
            }


            var id = Guid.NewGuid().ToString();

            foreach (var collaborator in model.Collaborators)
            {
                collaborator.IdeaId = id;
            }

            ModelState.Clear();
            TryValidateModel(model);

            if (!ModelState.IsValid)
            {
                feedback = GetFeedback(ModelState, feedback);
                var messageElement = "<ul>";
                foreach (var (_, value) in feedback.Elements)
                {
                    messageElement += $"<li class=\"text-danger\">{value}</li>";
                }

                messageElement += "</ul";

                feedback.Message = messageElement;
                return BadRequest(feedback);
            }


            var message = CanAddCollaborator(model.Collaborators);
            if (!string.IsNullOrWhiteSpace(message))
            {
                feedback.Message = message;
                return BadRequest(feedback);
            }


            var tenant = await GetClientAsync();

            message = await CanAddProcess(
                new Models.Business.Client(tenant),
                "Cannot add additional process ideas.");

            if (!string.IsNullOrWhiteSpace(message))
            {
                feedback.WarningMessage(message);
                return BadRequest(feedback);
            }


            var idea = new Idea
            {
                Id = id,
                IsDraft = false,
                SubmissionPathId = Data.Core.Enumerators.SubmissionPath.StandardUser.ToString(),
                ClientId = tenant.Id,
                Name = model.Name,
                Summary = model.Summary,
                DepartmentId = model.DepartmentId,
                TeamId = model.TeamId,
                ProcessId = model.ProcessId,
                RuleId = model.RuleId,
                InputId = model.InputId,
                InputDataStructureId = model.InputDataStructureId,
                ProcessStabilityId = model.ProcessStabilityId,
                DocumentationPresentId = model.DocumentationPresentId,
                ProcessOwnerId = model.ProcessOwnerId,
                Rating = model.Rating
            };

            var uniqueMessage = await UnitOfWork.IsUniqueAsync(idea);
            if (!string.IsNullOrWhiteSpace(uniqueMessage))
            {
                feedback.WarningMessage(uniqueMessage);
                return BadRequest(feedback);
            }


            await Models.Business.IdeaStage.AddWorkFlow(
                _unitOfWork, 
                idea);

            await _unitOfWork.AddAsync(idea);

            var userId = GetUserId();

            await Models.Business.Collaborator.UpdateAsync(
                _unitOfWork,
                model.Collaborators,
                idea.Id,
                userId);

            await _unitOfWork.CompleteAsync();

            return Ok();
        }



        [HttpPost("/api/Business/Idea/Post")]
        public async Task<IActionResult> Post([FromBody] Models.Business.Idea model)
        {
            //var errors = new List<FieldError>();
            var feedback = new Feedback
            {
                NamePrefix = "Business.Idea."
            };

            try
            {
                // Guard Clause
                if (model == null)
                {
                    feedback.DangerMessage("The model is missing.");
                    return BadRequest(feedback);
                }

                // Permission Clause
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                {
                    feedback.DangerMessage("You are not authorised save an idea.");
                    return BadRequest(feedback);
                }

                // Permission Clause
                if (string.IsNullOrWhiteSpace(model.Id)
                    && !(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
                {
                    feedback.DangerMessage(
                        "You do not have permission to save a centre of excellence driven automation idea.");
                    return BadRequest(feedback);
                }

                // Permission Clause
                if (!(await AuthorizeAsync(Policy.ReviewNewIdeas)).Succeeded
                    && !(await AuthorizeAsync(Policy.ReviewAssessedIdeas)).Succeeded
                    && !(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
                {
                    feedback.DangerMessage("You do not have permission to save this idea.");
                    return BadRequest(feedback);
                }


                var tenant = await GetClientAsync();


                // Can add process permissions Clause
                var message = await CanAddProcess(
                    new Models.Business.Client(tenant),
                    "Cannot add additional process ideas.");

                if (!string.IsNullOrWhiteSpace(message))
                {
                    feedback.WarningMessage(message);
                    return BadRequest(feedback);
                }



                // Can add Collaborators permissions Clause
                message = CanAddCollaborator(model.Collaborators);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    feedback.WarningMessage(message);
                    return BadRequest(feedback);
                }




                model.ClientId = tenant.Id;

                // Prepare the IdeaApplicationVersions for validation, by Idea.Id
                foreach (var ideaApplicationVersion in model.IdeaApplicationVersions)
                    ideaApplicationVersion.IdeaId = model.Id;



                if (!model.IsNew)
                {
                    // Not new? Check if the idea already on the database?
                    var coreOnDataStore = await _unitOfWork.BusinessIdeas.GetAsync(model.Id);

                    // The model was not found on the database.
                    // Log a hack and return view.
                    if (coreOnDataStore == null)
                    {
                        _unitOfWork.Log("The user attempted to save an idea with an incorrect Id.");

                        feedback.DangerMessage("The id is not valid");
                        return BadRequest(feedback);
                    }
                }


                // Validate
                if (string.IsNullOrWhiteSpace(model.Name))
                    feedback.Add("Name", "The name of your idea is missing");
                else if (model.Name.Length > 100)
                    feedback.Add("Name", "Name must be between 1 and 100 in length");


                if (string.IsNullOrWhiteSpace(model.Summary))
                    feedback.Add("Summary", "Summary is missing");
                else if (model.Summary.Length > 750)
                    feedback.Add("Summary", "Name must be between 1 and 750 in length");


                var uniqueMessage = await UnitOfWork.IsUniqueAsync(model.GetCore());
                if (!string.IsNullOrWhiteSpace(uniqueMessage))
                    feedback.Add("Name", "Your idea name is not unique");


                if (!model.IsDraft
                    && model.SubmissionPathId != Data.Core.Enumerators.SubmissionPath.StandardUser.ToString())
                    feedback = Validate(model, feedback);



                // Is NOT valid?
                if (!feedback.IsValid)
                    return BadRequest(feedback);



                var core = model.GetCore();

                if (!core.IsDraft)
                {
                    // Add stage if the idea is not draft
                    if(!core.IsNew)
                        await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(core);

                    if (!core.IdeaStages.Any())
                        await Models.Business.IdeaStage.AddWorkFlow(
                            _unitOfWork,
                            core);
                }



                await _unitOfWork.AddAsync(core);



                // Add Applications
                await SaveApplicationsListAsync(
                    model.IdeaApplicationVersions,
                    model.Id);

                var userId = GetUserId();

                await Models.Business.Collaborator.UpdateAsync(
                    _unitOfWork,
                    model.Collaborators, 
                    model.Id, 
                    userId);


                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var message = Security.Settings.GetEnvironment() == Security.Environment.Production
                    ? "Error saving ideas. Error logged."
                    : ex.Message;

                feedback.DangerMessage(message);
                return BadRequest(feedback);
            }
        }






        private Feedback Validate(
            Models.Business.Idea model,
            Feedback feedback)
        {
            if (string.IsNullOrWhiteSpace(model.PainPointComment))
            {
                feedback.Add(
                    "PainPointComment",
                    "Pain Point Comment is missing");
            }

            if (string.IsNullOrWhiteSpace(model.NegativeImpactComment))
            {
                feedback.Add(
                    "NegativeImpactComment",
                    "Negative Impact Comment is missing");
            }


            if (string.IsNullOrWhiteSpace(model.DepartmentId))
            {
                feedback.Add(
                    "DepartmentId",
                    "Business Unit is missing");
            }


            if (string.IsNullOrWhiteSpace(model.RuleId))
            {
                feedback.Add(
                    "RuleId",
                    "Rule is missing");
            }

            if (string.IsNullOrWhiteSpace(model.InputId))
            {
                feedback.Add(
                    "InputId",
                    "Input Data Structure is missing");
            }

            if (string.IsNullOrWhiteSpace(model.InputDataStructureId))
            {
                feedback.Add(
                    "InputDataStructureId",
                    "Input is missing");
            }

            if (string.IsNullOrWhiteSpace(model.ProcessStabilityId))
            {
                feedback.Add(
                    "ProcessStabilityId",
                    "Process Stability is missing");
            }

            if (string.IsNullOrWhiteSpace(model.DocumentationPresentId))
            {
                feedback.Add(
                    "DocumentationPresentId",
                    "Documentation Present is missing");
            }


            if (string.IsNullOrWhiteSpace(model.AutomationGoalId))
            {
                feedback.Add(
                    "AutomationGoalId",
                    "Automation Goal is missing");
            }




            if (string.IsNullOrWhiteSpace(model.ApplicationStabilityId))
            {
                feedback.Add (
                    "ApplicationStabilityId",
                    "Application Stability is missing");
            }


            if (model.AverageWorkingDay == null)
            {
                feedback.Add (
                    "AverageWorkingDay",
                    "Average Working Day is missing");
            }


            if (model.WorkingHour == null)
            {
                feedback.Add (
                    "WorkingHour",
                    "Working Hour is missing");
            }


            if (string.IsNullOrWhiteSpace(model.TaskFrequencyId))
            {
                feedback.Add (
                    "TaskFrequencyId",
                    "Task Frequency is missing");
            }


            if (model.ActivityVolumeAverage == null || model.ActivityVolumeAverage == 0)
            {
                feedback.Add (
                    "ActivityVolumeAverage",
                    "Activity Volume Average is missing");
            }


            if (model.EmployeeCount == null || model.EmployeeCount == 0)
            {
                feedback.Add (
                    "EmployeeCount",
                    "Employee Count is missing");
            }


            if (model.AverageProcessingTime == null || model.AverageProcessingTime == 0)
            {
                feedback.Add (
                    "AverageProcessingTime",
                    "Average Processing Time is missing");
            }


            if (string.IsNullOrWhiteSpace(model.ProcessPeakId))
            {
                feedback.Add (
                    "ProcessPeakId",
                    "Process Peak is missing");
            }


            if (string.IsNullOrWhiteSpace(model.AverageNumberOfStepId))
            {
                feedback.Add (
                    "AverageNumberOfStepId",
                    "Average Number of Step is missing");
            }


            if (string.IsNullOrWhiteSpace(model.DataInputPercentOfStructuredId))
            {
                feedback.Add (
                    "DataInputPercentOfStructuredId",
                    "Data Input Percent of Structured is missing");
            }


            if (string.IsNullOrWhiteSpace(model.NumberOfWaysToCompleteProcessId))
            {
                feedback.Add (
                    "NumberOfWaysToCompleteProcessId",
                    "Number of Ways to Complete Process is missing");
            }


            if (string.IsNullOrWhiteSpace(model.DecisionCountId))
            {
                feedback.Add (
                    "DecisionCountId",
                    "Decision Count is missing");
            }


            if (string.IsNullOrWhiteSpace(model.DecisionDifficultyId))
            {
                feedback.Add (
                    "DecisionDifficultyId",
                    "How difficult are the decisions that you must take to complete the process? is missing");
            }


            if (model.IdeaApplicationVersions.Count == 0)
            {
                feedback.Add (
                    "IdeaApplicationVersion",
                    "No applications are selected.");
            }

            if (string.IsNullOrWhiteSpace(model.ProcessOwnerId))
            {
                feedback.Add (
                    "ProcessOwnerId",
                    "Process Owner is missing");
            }
            return feedback;
        }



        [HttpGet("/api/Business/Idea/Section/About/Edit/{id}")]
        public async Task<IActionResult> AboutEdit(string id)
        {
            // Permission Clause
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            var userId = GetUserId();

            // Permission Clause
            if (!(await AuthorizeAsync(Policy.ReviewNewIdeas)).Succeeded
             && !(await AuthorizeAsync(Policy.ReviewAssessedIdeas)).Succeeded
             && !(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded
             && !IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditAbout, id, userId))
                return Content("<h1 class=\"text-warning\">You do not have permission to edit this idea.</h1>");

            // Permission Clause
            var client = await GetClientAsync();
            if (client == null
                || client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                return await PageApiAsync("<h1 class=\"text-warning\">Unauthorised</h1>", 412);


            // Guard Clause
            if (string.IsNullOrWhiteSpace(id) || id == "undefined")
                return await PageApiAsync("<h1 class=\"text-danger\">Error: Id missing</h1>");


            var model = await GetEditIdeaAsync(
                id, 
                true,
                false);


            // Guard Clause
            if (model == null)
                return await PageApiAsync($"<h1 class=\"text-danger\">Error: Idea with Id '{id}' not found</h1>");

            if(model.IsDraft)
                return await PageApiAsync($"<h1 class=\"text-warning\">Cannot edit the about section when the idea is in draft.</h1>");


            // Permission Clause
            model.ShowDetailedAssessmentFields = (await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded;

            return await ViewOrContent("/Views/Shared/Business/Idea/Edit/_About.cshtml", model);
        }


        [HttpGet("/api/Business/Idea/Section/About/Detail/{id}")]
        public async Task<IActionResult> AboutDetail(string id = "")
        {
            // Check Authorization
            const string unauthorizedMessage = "<h1 class=\"text-danger\">Error: Unauthorised</h1>";

            // Permission Clause
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return await PageApiAsync(unauthorizedMessage);

            // Permission Clause
            if (string.IsNullOrWhiteSpace(id) || id == "undefined")
                return await PageApiAsync("<h1 class=\"text-danger\">Error: Id missing</h1>");

            // Permission Clause
            var client = await GetClientAsync();
            if (client == null
                || client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                return await PageApiAsync("<h1 class=\"text-warning\">Unauthorised</h1>", 412);



            var model = await GetDetailIdeaAsync(id);
            if (model == null)
                return await PageApiAsync($"<h1 class=\"text-danger\">Error: Idea with Id '{id}' not found</h1>");

            return await ViewOrContent("/Views/Shared/Business/Idea/Detail/_About.cshtml", model);
        }

        [HttpPut("/api/Business/Idea/Section/Overview/Put")]
        public async Task<IActionResult> OverviewPut(
            [FromBody] ViewModels.Business.Idea.About.Put.Overview viewModel)
        {
            var feedback = new Feedback
            {
                NamePrefix = "Business.Idea."
            };


            try
            {
                // Guard Clause
                if (viewModel == null)
                {
                    feedback.DangerMessage("Model missing.");
                    return BadRequest(feedback);
                }


                // Permission Clause
                var client = await GetClientAsync();
                if (client == null)
                {
                    feedback.DangerMessage("Unauthorised.");
                    return BadRequest(feedback);
                }


                // Permission Clause
                if (client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                {
                    feedback.DangerMessage("Referrer agencies cannot add ideas.");
                    return BadRequest(feedback);
                }



                if ((await AuthorizeAsync(Policy.AssignProcessOwner)).Succeeded
                    && string.IsNullOrWhiteSpace(viewModel.ProcessOwnerId))
                {
                    feedback.Add("ProcessOwnerId", "Select a process owner, by using the search feature.");
                }

                if (!feedback.IsValid)
                    return BadRequest(feedback);


                var userId = GetUserId();
                var core = await _unitOfWork.BusinessIdeas.GetAsync(viewModel.Id);

                // Guard Clause
                if (core == null)
                {
                    feedback.DangerMessage("Could not find idea on the database.");
                    return BadRequest(feedback);
                }


                if (core.IsDraft)
                {
                    feedback.WarningMessage("Cannot save Overview section while the idea is in draft.");
                    return BadRequest(feedback);
                }


                if ((await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded
                    && IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditAbout, viewModel.Id, userId)
                    && core.ProcessOwnerId == userId)
                {
                    feedback = GetFeedback(ModelState, feedback);
                }

                if (!feedback.IsValid)
                    return BadRequest(feedback);







                // Permission Clause
                if (!(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded
                    && !IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditAbout, viewModel.Id, userId)
                    && core.ProcessOwnerId != userId 
                    && !(await AuthorizeAsync(Policy.AssignProcessOwner)).Succeeded)
                {
                    feedback.DangerMessage(
                        "You do not have permission to edit this idea, as you are not a process owner.");
                    return BadRequest(feedback);
                }


                viewModel.UpdateCore(
                    core,
                    (await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded,
                    (await AuthorizeAsync(Policy.AssignProcessOwner)).Succeeded);


                if (viewModel.ManageStageAndStatus != null)
                {
                    var (feedback2, _) = await SaveStatusAsync(viewModel.ManageStageAndStatus, client);

                    if (!feedback2.IsValid)
                        return BadRequest(feedback2);
                }

                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            catch (UniqueConstraintException ex)
            {
                feedback.DangerMessage(ex.Message);
                return BadRequest(feedback);
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Unknown error while saving idea. Error was logged.");
                return BadRequest(feedback);
            }
        }


        [HttpPut("/api/Business/Idea/Section/DetailedAssessment/Put")]
        public async Task<IActionResult> DetailedAssessmentPut(
            [FromBody] ViewModels.Business.Idea.About.Put.DetailedAssessment viewModel)
        {
            var feedback = new Feedback
            {
                NamePrefix = "Business.Idea."
            };

            try
            {
                // Guard Clause
                if (viewModel == null)
                {
                    feedback.DangerMessage("Model missing.");
                    return BadRequest(feedback);
                }

                // Permission Clause
                var client = await GetClientAsync();
                if (client == null)
                {
                    feedback.DangerMessage("Unauthorised.");
                    return BadRequest(feedback);
                }


                // Permission Clause
                if (client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                {
                    feedback.DangerMessage("Referrer agencies cannot add ideas.");
                    return BadRequest(feedback);
                }



                if (viewModel.IdeaApplicationVersions != null)
                    foreach (var ideaApplicationVersion in viewModel.IdeaApplicationVersions)
                        ideaApplicationVersion.IdeaId = viewModel.Id;

                ModelState.Clear();
                TryValidateModel(viewModel);
                
                feedback = GetFeedback(ModelState, feedback);


                var isFound = viewModel.IdeaApplicationVersions != null 
                              && viewModel.IdeaApplicationVersions.Any(x => x.IsSelected);
                if (!isFound)
                {
                    feedback.Add("IdeaApplicationVersion", "Select at least one application from the table.");
                }
                



                if (!feedback.IsValid)
                    return BadRequest(feedback);


                var userId = GetUserId();

                var core = await _unitOfWork.BusinessIdeas.GetAsync(viewModel.Id);


                // Guard Clause
                if (core == null)
                {
                    feedback.DangerMessage("Could not find idea on the database.");
                    return BadRequest(feedback);
                }


                // Permission Clause
                if (!(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded
                    && !IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditAbout, viewModel.Id, userId)
                    && core.ProcessOwnerId != userId)
                {
                    feedback.DangerMessage(
                        "You do not have permission to edit this idea, as you are not a process owner.");
                    return BadRequest(feedback);
                }


                var updateProcessOwner = (await AuthorizeAsync(Policy.AssignProcessOwner)).Succeeded
                                         && !string.IsNullOrWhiteSpace(viewModel.ProcessOwnerId);

                // Update the core
                viewModel.UpdateCore(core, updateProcessOwner);

                await SaveApplicationsListAsync(
                    viewModel.IdeaApplicationVersions,
                    viewModel.Id);


                if (viewModel.ManageStageAndStatus != null)
                {
                    var (feedback2, _) = await SaveStatusAsync(viewModel.ManageStageAndStatus, client);

                    if (!feedback2.IsValid)
                        return BadRequest(feedback2);
                }


                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            catch (UniqueConstraintException ex)
            {
                feedback.DangerMessage(ex.Message);
                return BadRequest(feedback);
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Unknown error while saving idea. Error was logged.");
                return BadRequest(feedback);
            }
        }



        private async Task SaveApplicationsListAsync(
            IReadOnlyCollection<Models.Business.IdeaApplicationVersion> ideaApplicationVersions,
            string id)
        {
            // Guard Clause
            if (ideaApplicationVersions == null)
                return;

            // Guard Clause
            if (string.IsNullOrWhiteSpace(id))
                return;

            if (!(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
                return;

            // Remove old
            var oldItems =
                (await _unitOfWork.BusinessIdeaApplicationVersions.FindAsync(x => x.IdeaId == id)).ToArray();
            
            await _unitOfWork.BusinessIdeaApplicationVersions.RemoveRangeAsync(oldItems);



            // Add new
            foreach (var ideaApplication in from application in ideaApplicationVersions where application.IsSelected select new IdeaApplicationVersion
            {
                IdeaId = id,
                VersionId = application.VersionId,
                LanguageId = application.LanguageId,
                IsThinClient = application.IsThinClient,
            })
            {
                await _unitOfWork.AddAsync(ideaApplication);
            }
        }





        [HttpGet("/api/Business/Idea/Section/CostBenefit/Detail/{id}")]
        public async Task<IActionResult> CostBenefitDetail(string id)
        {
            // Check Authorization
            const string unauthorizedMessage = "<h1 class=\"text-danger\">Error: Unauthorised</h1>";

            // Permission Clause
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return await PageApiAsync(unauthorizedMessage);

            // Permission Clause
            if (string.IsNullOrWhiteSpace(id) || id == "undefined")
                return await PageApiAsync("<h1 class=\"text-danger\">Error: Id missing</h1>");


            // Permission Clause
            var client = await GetClientAsync();
            if (client == null
                || client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                return await PageApiAsync("<h1 class=\"text-warning\">Unauthorised</h1>", 412);


            var idea = await _unitOfWork.BusinessIdeas.GetAsync(id);

            if(idea == null)
                return await PageApiAsync("<h1 class=\"text-warning\">ideaId is invalid</h1>", 412);

            return await ViewOrContent(
                "/Views/Shared/Business/Idea/Detail/_CostBenefit.cshtml",
                new ViewModels.Business.Idea.Detail.CostBenefit
                {
                    IdeaId = id,
                    CanEditStages = (await AuthorizeAsync(Policy.EditIdeasStageAndStatus)).Succeeded 
                                    && !idea.IsDraft
                });
        }


        [HttpGet("/api/Business/Idea/Section/CostBenefit/Pill/GetOneTimeCost/IdeaId/{id}")]
        public async Task<IActionResult> GetOneTimeCost(string id)
        {
            // Permission Clause
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Ok("Unauthorised");

            // Permission Clause
            var client = await GetClientAsync();
            if (client == null
                || client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                return Ok("Unauthorised");

            var core = await _unitOfWork.BusinessIdeas.GetAsync(id);

            // Permission Clause
            if (core == null)
                return Ok("Unauthorised");

            await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);
            var model = new Models.Business.Idea(core)
            {
                Client = new Models.Business.Client(client),
                UnitOfWork = _unitOfWork
            };

            var total = await model.GetOneTimeCostAsync(model.Client);


            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("One time cost",
                        model.Currency + total.ToString("#,##0.00"),
                        "var(--bs-primary)",
                        "/Icons/RobotHead.svg",
                        "",
                        "",
                        null,
                        "One-Time-Cost",
                        "230px"));
            return Content(html);

        }

        [HttpGet("/api/Business/Idea/Section/CostBenefit/Pill/GetRunningCosts/IdeaId/{id}")]
        public async Task<IActionResult> GetRunningCosts(string id)
        {
            // Permission Clause
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Ok("Unauthorised");

            // Permission Clause
            var client = await GetClientAsync();
            if (client == null
                || client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                return Ok("Unauthorised");

            var core = await _unitOfWork.BusinessIdeas.GetAsync(id);

            // Permission Clause
            if (core == null)
                return Ok("Unauthorised");

            await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);
            var model = new Models.Business.Idea(core)
            {
                Client = new Models.Business.Client(client)
            };

            var total = await model.GetRunningCostsAsync(_unitOfWork);


            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Running Costs",
                        model.Currency + total.ToString("#,##0.00"),
                        "var(--bs-warning)",
                        "/Icons/Spanner.svg",
                        "",
                        "",
                        null,
                        "Running-Costs",
                        "230px"));
            return Content(html);
        }


        [HttpGet("/api/Business/Idea/Section/CostBenefit/Pill/GetEstimatedBenefitsAnnual/IdeaId/{id}")]
        public async Task<IActionResult> GetEstimatedBenefitsAnnual(string id)
        {
            // Permission Clause
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Ok("Unauthorised");

            // Permission Clause
            var client = await GetClientAsync();
            if (client == null
                || client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                return Ok("Unauthorised");

            var core = await _unitOfWork.BusinessIdeas.GetAsync(id);

            // Permission Clause
            if (core == null)
                return Ok("Unauthorised");

            await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);
            var model = new Models.Business.Idea(core)
            {
                Client = new Models.Business.Client(client),
                UnitOfWork = _unitOfWork
            };


            var value = model.GetCostPerYearForProcessBeforeAutomation();

            value = await model.GetEstimateAsync(value);



            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Benefits (annual)",
                        model.Currency + value.ToString("#,##0.00"),
                        "var(--bs-success)",
                        "/Icons/Storage.svg",
                        "",
                        "",
                        null,
                        "Benefits-Annual",
                        "230px"));
            return Content(html);
        }


        [HttpGet("/api/Business/Idea/Section/CostBenefit/Pill/GetTimeToROI/IdeaId/{id}")]
        public async Task<IActionResult> GetTimeToRoi(string id)
        {
            // Permission Clause
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Ok("Unauthorised");

            // Permission Clause
            var client = await GetClientAsync();
            if (client == null
                || client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                return Ok("Unauthorised");

            var core = await _unitOfWork.BusinessIdeas.GetAsync(id);

            if (core == null)
                return Ok("Unauthorised");


            await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);
            var model = new Models.Business.Idea(core)
            {
                Client = new Models.Business.Client(client),
                UnitOfWork = _unitOfWork
            };

            var value = await model.GetTimeToBreakEvenAsync(new Models.Business.Client(client));

            string total;
            if (value < 1)
                total = Math.Round((value * 12), 1).ToString("#,##0.0") + "&nbsp;Months";
            else
                total = Math.Round(value, 1).ToString("#,##0.0") + "&nbsp;Years";

            var html = await _viewToString.PartialAsync("Shared/Dashboard/_SummaryButton.cshtml",
                new ViewModels.Dashboard
                    .SummaryButton("Time to ROI",
                        total,
                        "var(--bs-info)",
                        "/Icons/Alarm Clock.svg",
                        "",
                        "",
                        null,
                        "Time-to-ROI",
                        "230px"));
            return Content(html);
        }


        [HttpGet("/api/Business/Idea/Section/CostBenefit/Chart/GetEaseOfImplementation/IdeaId/{id}")]
        public async Task<IActionResult> GetEaseOfImplementation(string id)
        {
            // Permission Clause
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Ok("Unauthorised");



            // Permission Clause
            var client = await GetClientAsync();
            if (client == null
                || client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                return Ok("Unauthorised");

            var core = await _unitOfWork.BusinessIdeas.GetAsync(id);

            if(core == null || core.ClientId != client.Id)
                return Ok("Unauthorised");

            var model = new Models.Business.Idea(core)
            {
                UnitOfWork = _unitOfWork
            };

            var value = await model.GetEaseOfImplementationAsync();
            //Estimated by Algorithm
            var easeOfImplementationWord = model.EaseOfImplementationFinal;



            // 0	17.5	35	52.5	65	82.5
            var easeOfImplementationFinal = easeOfImplementationWord switch
            {
                "Easy" => 82,
                "Medium" => 52,
                "Difficult" => 17,
                _ => 0
            };





            const decimal weight = 40;
            const int fps = 240;
            const decimal seconds = 0;



            var colourEstimate = model.EaseOfImplementationWord switch
            {
                "Easy" => "var(--bs-green)",
                "Difficult" => "var(--bs-danger)",
                _ => "var(--bs-warning)"
            };

            var angle = (360 / (decimal)100 * (value ?? 0));
            var paperEstimate = new SVGChartTools.PaperPieChart();
            var circleSectionEstimate = new SVGChartTools.CircleSection(
                paperEstimate.Centre,
                paperEstimate.Radius,
                angle,
                90,
                weight,
                "")
            {
                Colour = colourEstimate,
                IsAntiClockwise = true,
                AnimateFPS = fps,
                AnimateSeconds = seconds
            };

            paperEstimate.Add(circleSectionEstimate);

            var estimate = new ViewModels.Business.Idea.Gauge.EaseOfImplementation
            {
                Value = value??0,
                ValueTitle = new[] {"Ease of", "Implementation"},
                ValueUnits = "%",
                Title = new[] {"Implementation", "(Estimate)"},
                Difficulty = model.EaseOfImplementationWord,
                Paper = paperEstimate,
                Colour = colourEstimate
            };



            var colourActual = easeOfImplementationFinal switch
            {
                >= 65 => "var(--bs-green)",
                <= 35 => "var(--bs-danger)",
                _ => "var(--bs-warning)"
            };

            angle = (360 / (decimal)100 * easeOfImplementationFinal);
            var paperActual = new SVGChartTools.PaperPieChart();
            var circleSectionActual = new SVGChartTools.CircleSection(
                paperActual.Centre,
                paperActual.Radius,
                angle,
                90,
                weight,
                "")
            {
                Colour = colourActual,
                IsAntiClockwise = true,
                AnimateFPS = fps,
                AnimateSeconds = seconds
            };

            paperActual.Add(circleSectionActual);


            var actual = new ViewModels.Business.Idea.Gauge.EaseOfImplementation
            {
                Value = easeOfImplementationFinal,
                ValueTitle = new[] { "Ease of","Implementation"},
                ValueUnits = "%",
                Title = new[] { "Implementation","(Actual)"},
                Difficulty = string.IsNullOrWhiteSpace(easeOfImplementationWord)? "	\u00A0" : easeOfImplementationWord,
                Paper = paperActual,
                Colour = colourActual
            };

            var tuple = (estimate, actual);

            var html = await _viewToString.PartialAsync(
                "Shared/Business/Idea/Detail/_EaseOfImplementationCharts.cshtml",
                tuple);

            return Content(html);
        }


        [HttpGet("/api/Business/Idea/Section/CostBenefit/Chart/GetEstimatedBreakeven/IdeaId/{id}")]
        public async Task<IActionResult> GetEstimatedBreakeven(string id)
        {
            // Permission Clause
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return Ok("Unauthorised");

            // Permission Clause
            var client = await GetClientAsync();
            if (client == null
                || client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                return Ok("Unauthorised");

            var idea = await _unitOfWork.BusinessIdeas.GetAsync(id);

            if (idea == null || idea.ClientId != client.Id)
                return Ok("Unauthorised");

            var model = new Models.Business.Idea(idea)
            {
                UnitOfWork = _unitOfWork
            };

            var runningCost = await model.GetRunningCostsAsync(_unitOfWork);

            var benefit = model.GetCostPerYearForProcessBeforeAutomation();

            benefit = await model.GetEstimateAsync(benefit);




            var totalOneTimeCosts = await model.GetOneTimeCostAsync(new Models.Business.Client(client));



            var chartData = new SVGChartTools.DataSet.Chart
            {
                XAxisLabels = new[] {"0", "1", "2", "3" }
            };

            // Running Costs
            var cssStyle = "stroke: var(--bs-warning); stroke-width: 2px";
            var dataSet = new SVGChartTools.DataSet.Data[]
            {
                new("data", runningCost) { CssStyle = cssStyle },
                new("data", runningCost) { CssStyle = cssStyle },
                new("data", runningCost) { CssStyle = cssStyle },
                new("data", runningCost) { CssStyle = cssStyle },
            };
            chartData.DataSets.Add(dataSet);

            // Total Costs
            cssStyle = "stroke: var(--bs-red); stroke-width: 2px";
            dataSet = new SVGChartTools.DataSet.Data[]
            {
                new("data", totalOneTimeCosts) { CssStyle = cssStyle },
                new("data", (totalOneTimeCosts + runningCost) ) { CssStyle = cssStyle },
                new("data", (totalOneTimeCosts + runningCost*2)) { CssStyle = cssStyle },
                new("data", (totalOneTimeCosts + runningCost*3)) { CssStyle = cssStyle },
            };
            chartData.DataSets.Add(dataSet);


            // Total Benefit
            cssStyle = "stroke: var(--bs-green); stroke-width: 2px";
            dataSet = new SVGChartTools.DataSet.Data[]
            {
                new("data", 0) { CssStyle = cssStyle },
                new("data", benefit) { CssStyle = cssStyle },
                new("data", benefit*2) { CssStyle = cssStyle },
                new("data", benefit*3) { CssStyle = cssStyle },
            };
            chartData.DataSets.Add(dataSet);

            var timeToBreakEven = await model.GetTimeToBreakEvenAsync(new Models.Business.Client(client));
            var benefitBreakEvent = benefit * timeToBreakEven;
            //chartData.SpecialValues = new[] { (timeToBreakEven, benefitBreakEvent, "stroke: var(--bs-green);") };


            var paper = new SVGChartTools.Paper12();
            var chart = new SVGChartTools.Chart.EstimatedBreakEven(
                new SVGChartTools.Point(0, 0),
                paper.Width,
                paper.Height,
                10,
                chartData)
            {
                BreakEven = (timeToBreakEven, benefitBreakEvent),
                ShowXAxis = false,
                XAxisCssStyle = "stroke: gray;",
                ShowYAxis = false,
                YAxisCssStyle = "stroke: gray;",
                ShowYBars = false,
                YBarCssStyle = "stroke: gray;"
            };


            paper.Add(chart);

            var estimatedBreakEven = new ViewModels.Business.Idea.Chart.EstimatedBreakEven(paper);


            var html = await _viewToString.PartialAsync("Shared/Business/Idea/Detail/_EstimatedBreakevenChart.cshtml", estimatedBreakEven);

            return Content(html);

        }



        [HttpGet("/api/Business/Idea/Section/CostBenefit/Edit/{id}")]
        public async Task<IActionResult> CostBenefitEdit(string id)
        {
            // Check Authorization
            const string unauthorizedMessage = "<h1 class=\"text-danger\">Error: Unauthorised</h1>";


            // Permission Clause
            if (!(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
                return await PageApiAsync(unauthorizedMessage);


            // Permission Clause
            if (string.IsNullOrWhiteSpace(id) || id == "undefined")
                return await PageApiAsync("<h1 class=\"text-danger\">Error: Id missing</h1>");

            // Permission Clause
            var client = await GetClientAsync();
            if (client == null
                || client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                return await PageApiAsync("<h1 class=\"text-warning\">Unauthorised</h1>", 412);


            var model = await GetEditIdeaAsync(
                id, 
                false,
                false);


            if (model == null)
                return await PageApiAsync($"<h1 class=\"text-danger\">Error: Idea with Id '{id}' not found</h1>");

            if (model.IsDraft)
                return await PageApiAsync("Cannot edit cost benefit section while the idea is in draft.");


            var runningCosts = (await _unitOfWork.BusinessRunningCosts.FindAsync(x => x.ClientId == client.Id
                                                                                            && x.IsLive)).ToArray();
            await _unitOfWork.BusinessSoftwareVenders.GetVenderForAsync(runningCosts);
            await _unitOfWork.SharedAutomationTypes.GetAutomationTypeForAsync(runningCosts);

            var runningCostModels = Models.Business.RunningCost.Create(runningCosts);
            model.RunningCosts.Add(new Models.Business.RunningCost());
            model.RunningCosts.AddRange(runningCostModels);
            model.RunningCost = model.RunningCosts.SingleOrDefault(x => x.Id == model.RunningCostId);

            model.UnitOfWork = _unitOfWork;

            await _unitOfWork.BusinessIdeaOtherRunningCosts.GetForIdeaAsync(model.GetCore());
            await _unitOfWork.BusinessOtherRunningCosts.GetOtherRunningCostForAsync(model.GetCore().IdeaOtherRunningCosts);
            model.GetOtherRunningCostPerYearAndPerMonthAndTransaction();
            await model.CalculateAllEstimatedFTE();

            await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);


            var viewModel = new ViewModels.Business.Idea.CostBenefit.Summary
            {
                Id = model.Id,
                Currency = client.Currency.Symbol,
                EaseOfImplementationWord = model.EaseOfImplementationWord,
                EaseOfImplementationFinal = model.EaseOfImplementationFinal,
                IdeaStageGanttComponent = await _viewToString.PartialAsync(
                    "Shared/Business/Idea/_IdeaStageGantt.cshtml",
                new ViewModels.Business.Idea.IdeaStageGantt {
                        IsReadOnly = model.IsReadOnly,
                        GanttIdeaStages = model.GanttIdeaStages
                    }),
                RobotEstimationComponent = await _viewToString.PartialAsync("Shared/Business/Idea/Edit/CostBenefit/_RobotEstimation.cshtml", model),
                OneTimeCostsComponent = await GetOnTimeCostsAsync(model, client),
                RPASoftwareCostsComponent = await GetRpaSoftwareCosts(model, client),
                IdeaOtherRunningCostComponent = await GetOtherRunningCosts(model, client),
                FooterComponent = await _viewToString.PartialAsync(
                    "Shared/_FormFooter.cshtml", 
                    new FooterSaveCancel
                    {
                        JavaScriptCancel = $"SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Cancel('{model.Id}');",
                        JavaScriptSave = "SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Save();"
                    })
            };

            return await ViewOrContent("Shared/Business/Idea/Edit/CostBenefit/_Summary.cshtml", viewModel);
        }

        private async Task<string> GetOnTimeCostsAsync(Models.Business.Idea model, Client client)
        {
            var idea = model.GetCore();
            
            var viewModel = new ViewModels.Business.Idea.CostBenefit.OneTimeCosts();



            // Get RollCosts
            await _unitOfWork.BusinessRoleCosts.GetForClientAsync(client);

            if (!client.RoleCosts.Any())
                return await PartialAsync("Shared/Business/Idea/Edit/CostBenefit/CostEstimates/OneTimeCosts/_SummaryNoRows.cshtml");

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

            // Get previously selected AllocationCosts
            await _unitOfWork.BusinessImplementationCosts.GetForIdeaStageAsync(ideaStages);


            foreach (var ideaStage in ideaStages)
            {
                foreach (var core in ideaStage.ImplementationCosts)
                {
                    core.IdeaStage = ideaStage;
                    viewModel.ImplementationCosts.Add(
                        new Models.Business.ImplementationCost(core)
                        {
                            IdeaStages = Models.Business.IdeaStage.Create(ideaStages),
                            Role = roleModels.SingleOrDefault(x => x.Id == core.RoleId),
                            Roles = roleModels
                        });
                }
            }

            // Stages that can have costs assigned to them. They will be used in the sub totals
            var stages = await _unitOfWork.SharedStages.FindAsync(x => x.CanAssignCost);
            viewModel.Stages = Models.Shared.Stage.Create(stages);
            viewModel.IdeaId = idea.Id;
            viewModel.Roles = roleModels;
            viewModel.CurrencySymbol = client.Currency.Symbol;
            model.OneTimeCosts = viewModel;

            return await _viewToString.PartialAsync("Shared/Business/Idea/Edit/CostBenefit/CostEstimates/OneTimeCosts/_Summary.cshtml", viewModel);
        }

        private async Task<string> GetRpaSoftwareCosts(Models.Business.Idea model, Client client)
        {
            var idea = model.GetCore();

            var runningCostCores = (await _unitOfWork.BusinessRunningCosts
                .FindAsync(x => x.ClientId == client.Id
                                && x.IsLive)).ToList();

            await _unitOfWork.BusinessSoftwareVenders.GetVenderForAsync(runningCostCores);
            var runningCosts = Models.Business.RunningCost.Create(runningCostCores);


            await _unitOfWork.BusinessIdeaRunningCosts.GetForIdeaAsync(idea);

            var ideaRunningCosts = Models.Business.IdeaRunningCost.Create(idea.IdeaRunningCosts);
            foreach (var ideaRunningCost in ideaRunningCosts)
            {
                ideaRunningCost.RunningCosts = runningCosts;
                ideaRunningCost.RunningCost = runningCosts.FirstOrDefault(x => x.Id == ideaRunningCost.RunningCostId);
            }


            var viewModel = new ViewModels.Business.Idea.CostBenefit.RPASoftwareCosts
            {
                RunningCosts = runningCosts,
                IdeaRunningCosts = ideaRunningCosts,
                CurrencySymbol = client.Currency.Symbol,
            };

            model.RPASoftwareCosts = viewModel;

            return await _viewToString.PartialAsync("Shared/Business/Idea/Edit/CostBenefit/CostEstimates/RPASoftwareCosts/_Summary.cshtml", viewModel);
        }

        private async Task<string> GetOtherRunningCosts(Models.Business.Idea model, Client client)
        {

            var idea = model.GetCore();

            var otherRunningCostCores = (await _unitOfWork.BusinessOtherRunningCosts
                .FindAsync(x => x.ClientId == client.Id
                                && x.IsLive)).ToArray();

            if (!otherRunningCostCores.Any())
            {
                var str = await PartialAsync("Shared/Business/Idea/Edit/CostBenefit/CostEstimates/IdeaOtherRunningCost/_SummaryNoOtherRunningCosts.cshtml");
                return str;
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

            return html;
        }

        public async Task<string> GetSummaryHtml(
            Idea idea,
            IEnumerable<OtherRunningCost> allOtherRunningCosts,
            Client client,
            string costTypeId,
            string title,
            string numberTitle,
            string totalTitle)
        {
            const string tableName = "Business.IdeaOtherRunningCosts";
            const string javascriptNamespace = "SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.";

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
                ideaOtherRunningCost.CostTypeId = costTypeId;
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





        // /api/Business/Idea/Section/CostBenefit/Post
        [HttpPut("/api/Business/Idea/Section/CostBenefit/Post")]
        public async Task<IActionResult> CostBenefitPut([FromBody] ViewModels.Business.Idea.CostBenefit.Post model)
        {
            var feedback = new Feedback
            {
                NamePrefix = "Business.Idea."
            };

            try
            {
                // Permission Clause
                if (!(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
                {
                    feedback.DangerMessage("You do not have permission to save a centre of excellence driven automation idea.");
                    return BadRequest(feedback);
                }

                if (model == null)
                {
                    feedback.DangerMessage("Unauthorized");
                    return BadRequest(feedback);
                }


                var client = await GetClientAsync();

                var core = await _unitOfWork.BusinessIdeas.GetAsync(model.Id);


                if (core == null
                    || core.ClientId != client.Id)
                {
                    feedback.DangerMessage("Unauthorized");
                    return BadRequest(feedback);
                }

                if (core.IsDraft)
                {
                    feedback.WarningMessage("Cannot save cost benefit section while the idea is in draft.");
                    return BadRequest(feedback);
                }

                var ideaStages = (await _unitOfWork.BusinessIdeaStages.FindAsync(x => x.IdeaId == model.Id)).ToArray();
                foreach (var ideaStage in model.IdeaStages)
                {
                    ideaStage.IdeaId = model.Id;
                    ideaStage.StageId = ideaStages.SingleOrDefault(x => x.Id == ideaStage.Id)?.StageId;
                }

                foreach (var ideaStage in model.IdeaStageEstimates)
                {
                    ideaStage.IdeaId = model.Id;
                    ideaStage.StageId = ideaStages.SingleOrDefault(x => x.Id == ideaStage.Id)?.StageId;
                }


                foreach (var implementationCost in model.ImplementationCosts)
                    implementationCost.ClientId = client.Id;

                foreach (var ideaRunningCost in model.IdeaRunningCosts)
                {
                    ideaRunningCost.IdeaId = model.Id;
                    ideaRunningCost.ClientId = client.Id;
                }

                foreach (var ideaOtherRunningCost in model.IdeaOtherRunningCosts)
                {
                    ideaOtherRunningCost.IdeaId = model.Id;
                    ideaOtherRunningCost.ClientId = client.Id;
                }

                ModelState.Clear();
                TryValidateModel(model);

                if (!ModelState.IsValid)
                {
                    foreach (var key in ModelState.Keys)
                    {
                        var value = ViewData.ModelState[key];

                        if (value == null)
                            continue;

                        foreach (var error in value.Errors)
                            feedback.Add(key, error.ErrorMessage);
                    }

                    return BadRequest(feedback);
                }


                //Update IdeaStages
                foreach (var ideaStage in ideaStages)
                {
                    var estimate = model.IdeaStageEstimates.SingleOrDefault(x => x.Id == ideaStage.Id);
                    if (estimate != null)
                    {
                        ideaStage.DateStartEstimate = estimate.DateStart??DateTime.MinValue;
                        ideaStage.DateEndEstimate = estimate.DateEnd;
                    }

                    var current = model.IdeaStages.SingleOrDefault(x => x.Id == ideaStage.Id);
                    if (current == null) continue;

                    ideaStage.DateStart = current.DateStart;
                    ideaStage.DateEnd = current.DateEnd;
                }


                // Update ImplementationCosts
                var implementationCostsOld = (await _unitOfWork.BusinessImplementationCosts
                    .FindAsync(x => x.ClientId == client.Id && ideaStages.Any(y => y.Id == x.IdeaStageId))).ToArray();

                await _unitOfWork.BusinessImplementationCosts.RemoveRangeAsync(implementationCostsOld);

                foreach (var implementationCost in model.ImplementationCosts)
                    await _unitOfWork.AddAsync(implementationCost.GetCore());


                // Update IdeaRunningCosts
                var ideaRunningCostsOld = (await _unitOfWork
                        .BusinessIdeaRunningCosts
                        .FindAsync(x => x.IdeaId == model.Id && x.ClientId == client.Id))
                    .ToArray();

                await _unitOfWork.BusinessIdeaRunningCosts.RemoveRangeAsync(ideaRunningCostsOld);

                foreach (var ideaRunningCost in model.IdeaRunningCosts)
                    await _unitOfWork.AddAsync(ideaRunningCost.GetCore());



                // Update IdeaOtherRunningCosts
                var ideaOtherRunningCostsOld = (await _unitOfWork
                        .BusinessIdeaOtherRunningCosts
                        .FindAsync(x => x.IdeaId == model.Id && x.ClientId == client.Id))
                    .ToArray();

                await _unitOfWork.BusinessIdeaOtherRunningCosts.RemoveRangeAsync(ideaOtherRunningCostsOld);

                foreach (var ideaOtherRunningCost in model.IdeaOtherRunningCosts)
                    await _unitOfWork.AddAsync(ideaOtherRunningCost.GetCore());


                core.EaseOfImplementationFinal = model.EaseOfImplementationFinal;
                core.RunningCostId = model.RunningCostId;
                core.ProcessVolumetryPerYear = model.ProcessVolumetryPerYear;
                core.ProcessVolumetryPerMonth = model.ProcessVolumetryPerMonth;
                core.EmployeeCount = model.EmployeeCount;
                core.RobotWorkHourDay = model.RobotWorkHourDay;
                core.RobotWorkDayYear = model.RobotWorkDayYear;
                core.RobotSpeedMultiplier = model.RobotSpeedMultiplier;
                core.AHTRobot = model.AHTRobot;
                core.WorkloadSplit = model.WorkloadSplit;

                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                feedback.DangerMessage("Error saving ideas");
                return BadRequest(feedback);
            }
        }


        [HttpPost("/api/Business/Idea/Section/Collaborators/Post/IdeaId/{ideaId}")]
        public async Task<IActionResult> CollaboratorsPost(
            [FromBody] List<Models.Business.Collaborator> models,
            string ideaId)
        {
            var feedback = new Feedback();

            try
            {
                // Permission Clause
                if (!(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
                {
                    feedback.DangerMessage("You do not have permission to save details");
                    return BadRequest(feedback);
                }

                var message = CanAddCollaborator(models);


                // Guard Clause
                if (!string.IsNullOrWhiteSpace(message))
                {
                    feedback.WarningMessage(message);
                    return BadRequest(feedback);
                }


                var ideaCore = await _unitOfWork.BusinessIdeas.GetAsync(ideaId);

                // Guard Clause
                if (ideaCore == null)
                {
                    feedback.DangerMessage("Error: Invalid idea Id.");
                    return BadRequest(feedback);
                }


                var userId = GetUserId();

                await Models.Business.Collaborator.UpdateAsync(
                    _unitOfWork,
                    models,
                    ideaId,
                    userId);

                await _unitOfWork.CompleteAsync();
        

                await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(ideaCore);
                var idea = new Models.Business.Idea(ideaCore);



                var users = await Models.Business.Collaborator.GetUsersAsync(_unitOfWork, ideaId);
                var collaboratorList = new ViewModels.Business.Idea.ManageCollaborator.CollaboratorList
                {
                    Idea = idea,
                    CollaboratorLimit = GetCollaboratorLimit(),
                    CanEditCollaborators = (await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded,
                    CollaboratingUsers = users,
                };

                return await ViewOrContent(
                    "/Views/Shared/Business/Idea/Detail/Collaborators/_Page.cshtml",
                    collaboratorList);
            }
            catch (Exception e)
            {
                _unitOfWork.Log(e);
                feedback.DangerMessage("Err save information");
                return BadRequest(feedback);
            }
        }

        [HttpGet("/api/Business/Idea/Section/Collaborators/Detail/ideaid/{ideaId}")]
        public async Task<IActionResult> GetCollaborators(
            string ideaId)
        {
            var feedback = new Feedback();


            // Permission Clause
            if (!(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
            {
                feedback.DangerMessage("You do not have permission to save details");
                return BadRequest(feedback);
            }

            var model = await GetEditIdeaAsync(
                ideaId, 
                false,
                false);

            if (model == null)
            {
                feedback.DangerMessage("Idea missing");
                return BadRequest(feedback);
            }


            return await ViewOrContent(
                "/Views/Shared/Business/Idea/Detail/Collaborators/_Page.cshtml",
                model);
        }


        [HttpPost("api/Business/Idea/FilterSummary")]
        public async Task<IActionResult> FilterSummary([FromBody] string idsConcatenated)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();


            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var client = new Models.Business.Client(clientCore);
            await _unitOfWork.BusinessIdeas.GetForClientAsync(client.GetCore());
            var ideas = client.GetCore().Ideas.Where(x => !x.IsDraft);


            if (string.IsNullOrWhiteSpace(idsConcatenated)) 
                return await RenderSummary(
                    ideas, 
                    false, 
                    false, 
                    "");


            var ids = idsConcatenated.Split(',');

            ideas = ideas
                .Where(x => ids.Any(y => y == x.Id))
                .ToList();



            return await RenderSummary(
                ideas, 
                false, 
                false,
                "/api/Business/Idea/FilterSummary");
        }


        [HttpPost("api/Business/Idea/Build/FilterSummary")]
        public async Task<IActionResult> BuildFilterSummary([FromBody] string idsConcatenated)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();


            var clientCore = await GetClientAsync();

            if (clientCore == null)
                return NegativeFeedback();

            var client = new Models.Business.Client(clientCore);
            await _unitOfWork.BusinessIdeas.GetForClientAsync(client.GetCore());

            var ideas = new List<Idea>();
            ideas.AddRange(client.GetCore().Ideas);

            if (string.IsNullOrWhiteSpace(idsConcatenated))
                return await RenderSummary(
                    ideas, 
                    true, 
                    true,
                    "");


            var ids = idsConcatenated.Split(',');

            ideas = ideas
                .Where(x => ids.Any(y => y == x.Id))
                .ToList();


            return await RenderSummary(
                ideas, 
                true, 
                true, 
                "/api/Business/Idea/Build/FilterSummary");
        }
    }
}