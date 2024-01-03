using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Persistence;
using SilkFlo.Web.ViewModels;
using SilkFlo.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using SilkFlo.Web.Controllers2.FileUpload;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using ExcelDataReader;
using System.Data.Entity;
using SilkFlo.Web.Models.Business;
using Newtonsoft.Json.Linq;
using SilkFlo.Web.Models;

namespace SilkFlo.Web.APIControllers.SilkFloApis
{
    public partial class ApisController:AbstractAPI
    {

        protected IAzureStorage _storage;
        protected IWebHostEnvironment _env;

        public ApisController(Data.Core.IUnitOfWork unitOfWork,
                              Services.ViewToString viewToString,
                              IAuthorizationService authorization, IAzureStorage storage, IWebHostEnvironment env) : base(unitOfWork, viewToString, authorization)
        {
            _storage = storage;
            _env = env;
        }





        /////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////    Api implementations    /////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////



        [HttpGet("/api/ApisController/GetIdeasByUserId")]
        public async Task<IActionResult> GetIdeasByUserId([FromForm] string UserId)
        {
            try
            {
                //if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                //    return Content("");

                //   var userId = "ea65f7fc-ad04-4fe6-ac6c-eb57d84e4217";

                // var user = await _unitOfWork.Users.GetAsync(userId);
                //  var userId = GetUserId();

                var user = await _unitOfWork.Users.GetAsync(UserId);
                if (await IsNewUser(user))
                    return Content("");


                var monthCount = 0;
                var lastMonthCount = 0;

                var date = DateTime.Now;
                var month = date.Month;
                var year = date.Year;

                date = date.AddMonths(-1);
                var monthLast = date.Month;
                var yearLast = date.Year;

                foreach (var createdDate in from idea
                             in user.Ideas
                                            where idea.CreatedDate != null
                                            select (DateTime)idea.CreatedDate)
                {
                    if (createdDate.Month == month && createdDate.Year == year)
                        monthCount++;

                    else if (createdDate.Month == monthLast && createdDate.Year == yearLast)
                        lastMonthCount++;
                }
                var ideas = user.Ideas.ToList();


                var total = ideas.Count;
                var totalChangeIn = GetChangeIn(lastMonthCount, monthCount);
                var result = new
                {
                    TotalIdeas = total.ToString(),
                    TotalChangeIn = totalChangeIn,
                    Ideas = ideas,
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("/api/ApisController/GetIdeaById")]
        public async Task<IActionResult> GetIdeaById([FromForm] string Id)
        {
            try
            {
                var obj = await _unitOfWork.BusinessIdeas.GetAsync(Id);
                if (obj != null)
                {
                    return Json(obj);

                }
                else
                {
                    return Json(new { });
                }

            }
            catch (Exception ex)
            {
                Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }






        [HttpPost("/api/ApisController/AddEmployeeIdea")]
        public async Task<IActionResult> AddEmployeeIdea([FromBody] ViewModels.Business.Idea.Modal model, [FromQuery] string UserId, [FromQuery] string ClientId, bool isPractice)
        {
            var feedback = new Feedback();

            // Guard Clause
            if (model == null)
            {
                feedback.DangerMessage("The model is missing");
                return BadRequest(feedback);
            }

            //// Permission Clause
            //if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
            //{
            //    feedback.DangerMessage("Unauthorised");
            //    return BadRequest(feedback);
            //}


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
            //if (!string.IsNullOrWhiteSpace(message))
            //{
            //    feedback.Message = message;
            //    return BadRequest(feedback);
            //}


            var tenant = await GetClient(ClientId, UserId, isPractice);

            //message = await CanAddProcess(
            //    new Models.Business.Client(tenant),
            //    "Cannot add additional process ideas.");

            //if (!string.IsNullOrWhiteSpace(message))
            //{
            //    feedback.WarningMessage(message);
            //    return BadRequest(feedback);
            //}


            var idea = new Data.Core.Domain.Business.Idea
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

            var uniqueMessage = await _unitOfWork.IsUniqueAsync(idea);// UnitOfWork.IsUniqueAsync(idea);
            if (!string.IsNullOrWhiteSpace(uniqueMessage))
            {
                feedback.WarningMessage(uniqueMessage);
                return BadRequest(feedback);
            }

            await _unitOfWork.AddAsync(idea);
            await _unitOfWork.CompleteAsync();

            // Models.Business.IdeaStage.AddWorkFlow(
            //    _unitOfWork, 
            //    idea);
            #region AddWorkflow
            var firstStage = Data.Core.Enumerators.Stage.n00_Idea;
            if (idea.SubmissionPathId == Data.Core.Enumerators.SubmissionPath.COEUser.ToString())
                firstStage = Data.Core.Enumerators.Stage.n01_Assess;

            var date = DateTime.Now;
            var ideaStage = new Data.Core.Domain.Business.IdeaStage
            {
                Idea = idea,
                StageId = firstStage.ToString(),
                DateStartEstimate = date,
                DateStart = date,
                IsInWorkFlow = true,
            };

            await _unitOfWork.AddAsync(ideaStage);
            await _unitOfWork.CompleteAsync();

            if (firstStage == Data.Core.Enumerators.Stage.n00_Idea)
            {
                var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
                {
                    IdeaStageId = ideaStage.Id,
                    StatusId = Data.Core.Enumerators.IdeaStatus.n00_Idea_AwaitingReview.ToString(),
                    Date = date
                };
                await _unitOfWork.AddAsync(ideaStageStatus);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
                {
                    IdeaStageId = ideaStage.Id,
                    StatusId = Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
                    Date = date
                };
                await _unitOfWork.AddAsync(ideaStageStatus);
                await _unitOfWork.CompleteAsync();
            }

            var stages = (await _unitOfWork.SharedStages.FindAsync(x => x.Id != firstStage.ToString())).ToArray();
            if (firstStage == Data.Core.Enumerators.Stage.n01_Assess)
                stages = stages.Where(x => x.Id != Data.Core.Enumerators.Stage.n00_Idea.ToString()).ToArray();

            var now = DateTime.Now;
            foreach (var stage in stages)
            {
                ideaStage = new Data.Core.Domain.Business.IdeaStage
                {
                    Idea = idea,
                    DateStartEstimate = now,
                    Stage = stage
                };

                now = now.AddSeconds(1);
                await _unitOfWork.AddAsync(ideaStage);
            }
            await _unitOfWork.CompleteAsync();
            #endregion

            //await Models.Business.Collaborator.UpdateAsync(
            //    _unitOfWork,
            //    model.Collaborators,
            //    idea.Id,
            //    userId);
            #region Collaborators UpdateAsync
            if (model.Collaborators == null || model.Collaborators.Count <= 0)
            {
                //await _unitOfWork.CompleteAsync();
                return Ok(new { Message = "Employee Idea saved successfully." });
            }

            //  var userId = GetUserId();

            // Get the existing.
            // We need some field content, before deleting them
            var cores = (await _unitOfWork
                    .BusinessCollaborators
                    .FindAsync(x => x.IdeaId == idea.Id))
                    .ToArray();



            // Prepare
            foreach (var modelC in model.Collaborators)
            {
                var core = cores.SingleOrDefault(x => x.UserId == modelC.UserId
                                                      && x.IdeaId == idea.Id);
                if (core == null)
                    continue;

                modelC.IsInvitationConfirmed = core.IsInvitationConfirmed;
                modelC.InvitedById = core.InvitedById;
            }



            // Remove existing
            // This will also remove connected Business.CollaboratorRole rows
            await _unitOfWork.BusinessCollaborators.RemoveRangeAsync(cores);

            // The content of this will be used to populate the Business.UserAuthorisation table.
            var newUserAuthorisation = new List<Data.Core.Domain.Business.UserAuthorisation>();

            foreach (var collaborator in model.Collaborators)
            {
                if (collaborator.CollaboratorRoles == null)
                    continue;

                var collaboratorRoles = new List<Models.Business.CollaboratorRole>();

                var core = collaborator.GetCore();
                core.IdeaId = idea.Id;

                if (string.IsNullOrWhiteSpace(core.InvitedById))
                    core.InvitedById = UserId;

                await _unitOfWork.AddAsync(core);
                foreach (var collaboratorRole in collaborator.CollaboratorRoles)
                {
                    var collaboratorRoleCore = collaboratorRole.GetCore();
                    collaboratorRoleCore.Collaborator = collaborator.GetCore();
                    await _unitOfWork.AddAsync(collaboratorRoleCore);

                    // This will be used to add userAuthorisations
                    if (collaboratorRoles.All(x => x.RoleId != collaboratorRole.RoleId))
                        collaboratorRoles.Add(collaboratorRole);
                }



                // Remove the userAuthorisation from the de-normalized table
                var userAuthorisations =
                    (await _unitOfWork.BusinessUserAuthorisations
                        .FindAsync(x => x.UserId == collaborator.UserId && x.IdeaId == idea.Id)).ToList();

                await _unitOfWork.BusinessUserAuthorisations.RemoveRangeAsync(userAuthorisations);


                // Create Business.UserAuthorisation records
                foreach (var collaboratorRole in collaboratorRoles)
                {
                    var roleIdeaAuthorisation =
                        await _unitOfWork
                            .BusinessRoleIdeaAuthorisations
                            .SingleOrDefaultAsync(x => x.RoleId == collaboratorRole.RoleId);

                    if (newUserAuthorisation
                        .Any(x => x.UserId == collaborator.UserId
                                  && x.IdeaId == idea.Id
                                  && x.IdeaAuthorisationId == roleIdeaAuthorisation.IdeaAuthorisationId))
                        continue;

                    var userAuthorisation = new Data.Core.Domain.Business.UserAuthorisation
                    {
                        UserId = collaborator.UserId,
                        IdeaId = idea.Id,
                        CollaboratorRoleId = collaboratorRole.Id,
                        IdeaAuthorisationId = roleIdeaAuthorisation.IdeaAuthorisationId
                    };

                    // Add the userAuthorisation to the de-normalized table
                    newUserAuthorisation.Add(userAuthorisation);
                }
            }

            // Add the userAuthorisations to the de-normalized table
            await _unitOfWork.AddAsync(newUserAuthorisation);
            #endregion

            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Employee Idea saved successfully." });
        }







        [HttpPost("/api/ApisController/AddCeoIdea")]
        public async Task<IActionResult> Post([FromBody] Models.Business.Idea model, [FromQuery] string UserId, [FromQuery] string ClientId, bool isPractice)
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

                //// Permission Clause
                //if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                //{
                //    feedback.DangerMessage("You are not authorised save an idea.");
                //    return BadRequest(feedback);
                //}

                // Permission Clause
                //if (string.IsNullOrWhiteSpace(model.Id)
                //    && !(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
                //{
                //    feedback.DangerMessage(
                //        "You do not have permission to save a centre of excellence driven automation idea.");
                //    return BadRequest(feedback);
                //}

                // Permission Clause
                //if (!(await AuthorizeAsync(Policy.ReviewNewIdeas)).Succeeded
                //    && !(await AuthorizeAsync(Policy.ReviewAssessedIdeas)).Succeeded
                //    && !(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
                //{
                //    feedback.DangerMessage("You do not have permission to save this idea.");
                //    return BadRequest(feedback);
                //}


                var tenant = await GetClient(ClientId, UserId, isPractice);


                // Can add process permissions Clause
                //var message = await CanAddProcess(
                //    new Models.Business.Client(tenant),
                //    "Cannot add additional process ideas.");

                //if (!string.IsNullOrWhiteSpace(message))
                //{
                //    feedback.WarningMessage(message);
                //    return BadRequest(feedback);
                //}



                //// Can add Collaborators permissions Clause
                //message = CanAddCollaborator(model.Collaborators);
                //if (!string.IsNullOrWhiteSpace(message))
                //{
                //    feedback.WarningMessage(message);
                //    return BadRequest(feedback);
                //}




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


                var uniqueMessage = await _unitOfWork.IsUniqueAsync(model.GetCore());
                if (!string.IsNullOrWhiteSpace(uniqueMessage))
                    feedback.Add("Name", "Your idea name is not unique");


                //if (!model.IsDraft
                //    && model.SubmissionPathId != Data.Core.Enumerators.SubmissionPath.StandardUser.ToString())
                //    feedback = Validate(model, feedback);



                //// Is NOT valid?
                //if (!feedback.IsValid)
                //    return BadRequest(feedback);



                var core = model.GetCore();

                if (!core.IsDraft)
                {
                    // Add stage if the idea is not draft
                    if (!core.IsNew)
                        await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(core);

                    if (!core.IdeaStages.Any())
                    {
                        //First save ideaa
                        await _unitOfWork.AddAsync(core);

                        //then it's stages and status
                        await Models.Business.IdeaStage.AddWorkFlow(_unitOfWork, core);


                        await _unitOfWork.CompleteAsync();

                        //continue
                        // Add Applications
                        await SaveApplicationsListAsync(
                            model.IdeaApplicationVersions,
                            model.Id);

                        // var userId = GetUserId();

                        await Models.Business.Collaborator.UpdateAsync(
                            _unitOfWork,
                            model.Collaborators,
                            model.Id,
                            UserId);


                        await _unitOfWork.CompleteAsync();
                    }
                }
                else
                {


                    await _unitOfWork.AddAsync(core);

                    await _unitOfWork.CompleteAsync();

                    // Add Applications
                    await SaveApplicationsListAsync(
                        model.IdeaApplicationVersions,
                        model.Id);

                    //var userId = GetUserId();

                    await Models.Business.Collaborator.UpdateAsync(
                        _unitOfWork,
                        model.Collaborators,
                        model.Id,
                        UserId);


                    await _unitOfWork.CompleteAsync();

                }

                return Ok(new { Message = "CEO Idea saved successfully." });
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
                feedback.Add(
                    "ApplicationStabilityId",
                    "Application Stability is missing");
            }


            if (model.AverageWorkingDay == null)
            {
                feedback.Add(
                    "AverageWorkingDay",
                    "Average Working Day is missing");
            }


            if (model.WorkingHour == null)
            {
                feedback.Add(
                    "WorkingHour",
                    "Working Hour is missing");
            }


            if (string.IsNullOrWhiteSpace(model.TaskFrequencyId))
            {
                feedback.Add(
                    "TaskFrequencyId",
                    "Task Frequency is missing");
            }


            if (model.ActivityVolumeAverage == null || model.ActivityVolumeAverage == 0)
            {
                feedback.Add(
                    "ActivityVolumeAverage",
                    "Activity Volume Average is missing");
            }


            if (model.EmployeeCount == null || model.EmployeeCount == 0)
            {
                feedback.Add(
                    "EmployeeCount",
                    "Employee Count is missing");
            }


            if (model.AverageProcessingTime == null || model.AverageProcessingTime == 0)
            {
                feedback.Add(
                    "AverageProcessingTime",
                    "Average Processing Time is missing");
            }


            if (string.IsNullOrWhiteSpace(model.ProcessPeakId))
            {
                feedback.Add(
                    "ProcessPeakId",
                    "Process Peak is missing");
            }


            if (string.IsNullOrWhiteSpace(model.AverageNumberOfStepId))
            {
                feedback.Add(
                    "AverageNumberOfStepId",
                    "Average Number of Step is missing");
            }


            if (string.IsNullOrWhiteSpace(model.DataInputPercentOfStructuredId))
            {
                feedback.Add(
                    "DataInputPercentOfStructuredId",
                    "Data Input Percent of Structured is missing");
            }


            if (string.IsNullOrWhiteSpace(model.NumberOfWaysToCompleteProcessId))
            {
                feedback.Add(
                    "NumberOfWaysToCompleteProcessId",
                    "Number of Ways to Complete Process is missing");
            }


            if (string.IsNullOrWhiteSpace(model.DecisionCountId))
            {
                feedback.Add(
                    "DecisionCountId",
                    "Decision Count is missing");
            }


            if (string.IsNullOrWhiteSpace(model.DecisionDifficultyId))
            {
                feedback.Add(
                    "DecisionDifficultyId",
                    "How difficult are the decisions that you must take to complete the process? is missing");
            }


            if (model.IdeaApplicationVersions.Count == 0)
            {
                feedback.Add(
                    "IdeaApplicationVersion",
                    "No applications are selected.");
            }

            if (string.IsNullOrWhiteSpace(model.ProcessOwnerId))
            {
                feedback.Add(
                    "ProcessOwnerId",
                    "Process Owner is missing");
            }
            return feedback;
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
            foreach (var ideaApplication in from application in ideaApplicationVersions
                                            where application.IsSelected
                                            select new Data.Core.Domain.Business.IdeaApplicationVersion
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











        public async Task<Data.Core.Domain.Business.Client> GetClient(string ClientId, string UserId, bool isPractice, bool checkSubscription = false)
        {

            if (string.IsNullOrWhiteSpace(ClientId))
            {
                // Cookies are missing; therefore, we must use the user.ClientId

                var user = await _unitOfWork.Users.GetAsync(UserId);

                // Guard Clause
                if (user == null)
                    return null;

                var client = await GetSingleOrDefaultValidatedAsync(user, ClientId);

                if (isPractice && client is { IsPractice: false })
                    client = await _unitOfWork.BusinessClients.SingleOrDefaultAsync(x => x.Id == client.PracticeId);

                return client;
            }
            else
            {
                // Guard Clause
                if (string.IsNullOrWhiteSpace(UserId))
                    return null;

                var user = await _unitOfWork.Users.GetAsync(UserId);

                // Guard Clause
                if (user == null)
                    return null;

                var client = await GetSingleOrDefaultValidatedAsync(user, ClientId, checkSubscription);


                //problem of cookies

                if (isPractice && client is { IsPractice: false })
                    client = await _unitOfWork.BusinessClients.SingleOrDefaultAsync(x => x.Id == client.PracticeId);

                return client;
            }
        }









        [HttpPost("/api/ApisController/UploadEmployeeBulkData")]

        public async Task<IActionResult> UploadEmployeeBulkData([FromForm] IFormFile File)
        {
            try
            {
                string fileName = File.FileName;
                string uniqueName = Guid.NewGuid().ToString() + "-" + fileName.Replace(' ', '_');
                string filePath = "wwwroot/IdeaFiles/" + uniqueName;
                using (FileStream stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await File.CopyToAsync(stream);
                }

                List<APIControllers.SilkFloApis.EmployeeBulkIdeaModel> rows = new List<APIControllers.SilkFloApis.EmployeeBulkIdeaModel>();
                if (File.FileName.ToLower().Contains(".xlsx") || File.FileName.ToLower().Contains(".xls"))
                {
                    rows = await UploadEmployeeExcelFile(File, filePath);
                }
                else
                {
                    return Ok(new { status = false, message = "Invalid Data!", });
                }


                var feedback = new Feedback();



                foreach(var x in rows)
                {


                    var id = Guid.NewGuid().ToString();

                    //foreach (var collaborator in model.Collaborators)
                    //{
                    //    collaborator.IdeaId = id;
                    //}

                    //ModelState.Clear();
                    //TryValidateModel(model);

                    //if (!ModelState.IsValid)
                    //{
                    //    feedback = GetFeedback(ModelState, feedback);
                    //    var messageElement = "<ul>";
                    //    foreach (var (_, value) in feedback.Elements)
                    //    {
                    //        messageElement += $"<li class=\"text-danger\">{value}</li>";
                    //    }

                    //    messageElement += "</ul";

                    //    feedback.Message = messageElement;
                    //    return BadRequest(feedback);
                    //}


                    //var message = CanAddCollaborator(model.Collaborators);
                    ////if (!string.IsNullOrWhiteSpace(message))
                    ////{
                    ////    feedback.Message = message;
                    ////    return BadRequest(feedback);
                    ////}


                    var tenant = await GetClient(x.ClientId, x.UserId, true);

                    //message = await CanAddProcess(
                    //    new Models.Business.Client(tenant),
                    //    "Cannot add additional process ideas.");

                    //if (!string.IsNullOrWhiteSpace(message))
                    //{
                    //    feedback.WarningMessage(message);
                    //    return BadRequest(feedback);
                    //}


                    var idea = new Data.Core.Domain.Business.Idea
                    {
                        Id = id,
                        IsDraft = false,
                        SubmissionPathId = Data.Core.Enumerators.SubmissionPath.StandardUser.ToString(),
                        ClientId = tenant.Id,
                        Name = x.Name,
                        Summary = x.Summary,
                        DepartmentId = x.DepartmentId,
                        TeamId = x.TeamId,
                        ProcessId = x.ProcessId,
                        RuleId = x.RuleId,
                        InputId = x.InputId,
                        InputDataStructureId = x.InputDataStructureId,
                        ProcessStabilityId = x.ProcessStabilityId,
                        DocumentationPresentId = x.DocumentationPresentId,
                        ProcessOwnerId = x.ProcessOwnerId,
                        Rating = x.Rating
                    };

                    //var uniqueMessage = await _unitOfWork.IsUniqueAsync(idea);// UnitOfWork.IsUniqueAsync(idea);
                    //if (!string.IsNullOrWhiteSpace(uniqueMessage))
                    //{
                    //    feedback.WarningMessage(uniqueMessage);
                    //    return BadRequest(feedback);
                    //}

                    await _unitOfWork.AddAsync(idea);
                    await _unitOfWork.CompleteAsync();

                    // Models.Business.IdeaStage.AddWorkFlow(
                    //    _unitOfWork, 
                    //    idea);
                    #region AddWorkflow
                    var firstStage = Data.Core.Enumerators.Stage.n00_Idea;
                    if (idea.SubmissionPathId == Data.Core.Enumerators.SubmissionPath.COEUser.ToString())
                        firstStage = Data.Core.Enumerators.Stage.n01_Assess;

                    var date = DateTime.Now;
                    var ideaStage = new Data.Core.Domain.Business.IdeaStage
                    {
                        Idea = idea,
                        StageId = firstStage.ToString(),
                        DateStartEstimate = date,
                        DateStart = date,
                        IsInWorkFlow = true,
                    };

                    await _unitOfWork.AddAsync(ideaStage);
                    await _unitOfWork.CompleteAsync();

                    if (firstStage == Data.Core.Enumerators.Stage.n00_Idea)
                    {
                        var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
                        {
                            IdeaStageId = ideaStage.Id,
                            StatusId = Data.Core.Enumerators.IdeaStatus.n00_Idea_AwaitingReview.ToString(),
                            Date = date
                        };
                        await _unitOfWork.AddAsync(ideaStageStatus);
                        await _unitOfWork.CompleteAsync();
                    }
                    else
                    {
                        var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
                        {
                            IdeaStageId = ideaStage.Id,
                            StatusId = Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
                            Date = date
                        };
                        await _unitOfWork.AddAsync(ideaStageStatus);
                        await _unitOfWork.CompleteAsync();
                    }

                    var stages = (await _unitOfWork.SharedStages.FindAsync(x => x.Id != firstStage.ToString())).ToArray();
                    if (firstStage == Data.Core.Enumerators.Stage.n01_Assess)
                        stages = stages.Where(x => x.Id != Data.Core.Enumerators.Stage.n00_Idea.ToString()).ToArray();

                    var now = DateTime.Now;
                    foreach (var stage in stages)
                    {
                        ideaStage = new Data.Core.Domain.Business.IdeaStage
                        {
                            Idea = idea,
                            DateStartEstimate = now,
                            Stage = stage
                        };

                        now = now.AddSeconds(1);
                        await _unitOfWork.AddAsync(ideaStage);
                    }
                    await _unitOfWork.CompleteAsync();
                    #endregion

                    //await Models.Business.Collaborator.UpdateAsync(
                    //    _unitOfWork,
                    //    model.Collaborators,
                    //    idea.Id,
                    //    userId);
                    //#region Collaborators UpdateAsync
                    //if (model.Collaborators == null || model.Collaborators.Count <= 0)
                    //{
                    //    //await _unitOfWork.CompleteAsync();
                    //    return Ok(new { Message = "Employee Idea saved successfully." });
                    //}

                    //  var userId = GetUserId();

                    // Get the existing.
                    // We need some field content, before deleting them
                    var cores = (await _unitOfWork
                            .BusinessCollaborators
                            .FindAsync(x => x.IdeaId == idea.Id))
                            .ToArray();



                    //// Prepare
                    //foreach (var modelC in model.Collaborators)
                    //{
                    //    var core = cores.SingleOrDefault(x => x.UserId == modelC.UserId
                    //                                          && x.IdeaId == idea.Id);
                    //    if (core == null)
                    //        continue;

                    //    modelC.IsInvitationConfirmed = core.IsInvitationConfirmed;
                    //    modelC.InvitedById = core.InvitedById;
                    //}



                    //// Remove existing
                    //// This will also remove connected Business.CollaboratorRole rows
                    //await _unitOfWork.BusinessCollaborators.RemoveRangeAsync(cores);

                    //// The content of this will be used to populate the Business.UserAuthorisation table.
                    //var newUserAuthorisation = new List<Data.Core.Domain.Business.UserAuthorisation>();

                    //foreach (var collaborator in model.Collaborators)
                    //{
                    //    if (collaborator.CollaboratorRoles == null)
                    //        continue;

                    //    var collaboratorRoles = new List<Models.Business.CollaboratorRole>();

                    //    var core = collaborator.GetCore();
                    //    core.IdeaId = idea.Id;

                    //    if (string.IsNullOrWhiteSpace(core.InvitedById))
                    //        core.InvitedById = UserId;

                    //    await _unitOfWork.AddAsync(core);
                    //    foreach (var collaboratorRole in collaborator.CollaboratorRoles)
                    //    {
                    //        var collaboratorRoleCore = collaboratorRole.GetCore();
                    //        collaboratorRoleCore.Collaborator = collaborator.GetCore();
                    //        await _unitOfWork.AddAsync(collaboratorRoleCore);

                    //        // This will be used to add userAuthorisations
                    //        if (collaboratorRoles.All(x => x.RoleId != collaboratorRole.RoleId))
                    //            collaboratorRoles.Add(collaboratorRole);
                    //    }



                    //    // Remove the userAuthorisation from the de-normalized table
                    //    var userAuthorisations =
                    //        (await _unitOfWork.BusinessUserAuthorisations
                    //            .FindAsync(x => x.UserId == collaborator.UserId && x.IdeaId == idea.Id)).ToList();

                    //    await _unitOfWork.BusinessUserAuthorisations.RemoveRangeAsync(userAuthorisations);


                    //    // Create Business.UserAuthorisation records
                    //    foreach (var collaboratorRole in collaboratorRoles)
                    //    {
                    //        var roleIdeaAuthorisation =
                    //            await _unitOfWork
                    //                .BusinessRoleIdeaAuthorisations
                    //                .SingleOrDefaultAsync(x => x.RoleId == collaboratorRole.RoleId);

                    //        if (newUserAuthorisation
                    //            .Any(x => x.UserId == collaborator.UserId
                    //                      && x.IdeaId == idea.Id
                    //                      && x.IdeaAuthorisationId == roleIdeaAuthorisation.IdeaAuthorisationId))
                    //            continue;

                    //        var userAuthorisation = new Data.Core.Domain.Business.UserAuthorisation
                    //        {
                    //            UserId = collaborator.UserId,
                    //            IdeaId = idea.Id,
                    //            CollaboratorRoleId = collaboratorRole.Id,
                    //            IdeaAuthorisationId = roleIdeaAuthorisation.IdeaAuthorisationId
                    //        };

                    //        // Add the userAuthorisation to the de-normalized table
                    //        newUserAuthorisation.Add(userAuthorisation);
                    //    }
                    //}

                    //// Add the userAuthorisations to the de-normalized table
                    //await _unitOfWork.AddAsync(newUserAuthorisation);
                    //#endregion

                    await _unitOfWork.CompleteAsync();

                }

              

                return Ok(new { Message = "Employee Ideas Added successfully." });

            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        public async Task<List<APIControllers.SilkFloApis.EmployeeBulkIdeaModel>> UploadEmployeeExcelFile(IFormFile File, string filepath)
        {
            List<APIControllers.SilkFloApis.EmployeeBulkIdeaModel> rows = new List<APIControllers.SilkFloApis.EmployeeBulkIdeaModel>();
            try
            {
                FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding.GetEncoding(1252);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(fileStream);
                System.Data.DataSet dataSet = reader.AsDataSet(
                    new ExcelDataSetConfiguration()
                    {
                        UseColumnDataType = false,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = false,
                        }
                    });

                for (int i = 1; i < dataSet.Tables[0].Rows.Count; i++)
                {

                    APIControllers.SilkFloApis.EmployeeBulkIdeaModel fileReader = new
                    APIControllers.SilkFloApis.EmployeeBulkIdeaModel
                    {
                        Name = dataSet.Tables[0].Rows[i].ItemArray[0] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[0]) : string.Empty,
                        Summary = dataSet.Tables[0].Rows[i].ItemArray[1] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[1]) : string.Empty,
                        DepartmentId = dataSet.Tables[0].Rows[i].ItemArray[2] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[2]) : string.Empty,
                        TeamId = dataSet.Tables[0].Rows[i].ItemArray[3] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[3]) : string.Empty,
                        ProcessId = dataSet.Tables[0].Rows[i].ItemArray[4] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[4]) : string.Empty,
                        RuleId = dataSet.Tables[0].Rows[i].ItemArray[5] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[5]) : string.Empty,
                        InputId = dataSet.Tables[0].Rows[i].ItemArray[6] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[6]) : string.Empty,
                        InputDataStructureId = dataSet.Tables[0].Rows[i].ItemArray[7] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[7]) : string.Empty,
                        ProcessStabilityId = dataSet.Tables[0].Rows[i].ItemArray[8] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[8]) : string.Empty,
                        DocumentationPresentId = dataSet.Tables[0].Rows[i].ItemArray[9] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[9]) : string.Empty,
                        ProcessOwnerId = dataSet.Tables[0].Rows[i].ItemArray[10] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[10]) : string.Empty,
                        Rating = dataSet.Tables[0].Rows[i].ItemArray[11] != null ? int.Parse(dataSet.Tables[0].Rows[i].ItemArray[11].ToString()) : 0,
                        ClientId = dataSet.Tables[0].Rows[i].ItemArray[12] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[12]) : string.Empty,
                        UserId = dataSet.Tables[0].Rows[i].ItemArray[13] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[13]) : string.Empty,
                    };

                    rows.Add(fileReader);

                }
                return rows;
            }
            catch (Exception ex)
            {
                return rows;
            }





        }
















        //[HttpPost("/api/ApisController/UploadCeoBulkData")]

        //public async Task<IActionResult> UploadCeoBulkData([FromForm] IFormFile File)
        //{
        //    try
        //    {
        //        string fileName = File.FileName;
        //        string uniqueName = Guid.NewGuid().ToString() + "-" + fileName.Replace(' ', '_');
        //        string filePath = "wwwroot/IdeaFiles/" + uniqueName;
        //        using (FileStream stream = new FileStream(filePath, FileMode.CreateNew))
        //        {
        //            await File.CopyToAsync(stream);
        //        }

        //        List<Models.Business.Idea> rows = new List<Models.Business.Idea>();
        //        if (File.FileName.ToLower().Contains(".xlsx") || File.FileName.ToLower().Contains(".xls"))
        //        {
        //            rows = await UploadCeoExcelFile(File, filePath);
        //        }
        //        else
        //        {
        //            return Ok(new { status = false, message = "Invalid Data!", });
        //        }

             


        //        var feedback = new Feedback();



        //        foreach (var model in rows)
        //        {

        //            // Guard Clause
        //            //if (model == null)
        //            //{
        //            //    feedback.DangerMessage("The model is missing.");
        //            //    return BadRequest(feedback);
        //            //}

        //            //// Permission Clause
        //            //if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
        //            //{
        //            //    feedback.DangerMessage("You are not authorised save an idea.");
        //            //    return BadRequest(feedback);
        //            //}

        //            //// Permission Clause
        //            //if (string.IsNullOrWhiteSpace(model.Id)
        //            //    && !(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
        //            //{
        //            //    feedback.DangerMessage(
        //            //        "You do not have permission to save a centre of excellence driven automation idea.");
        //            //    return BadRequest(feedback);
        //            //}

        //            //// Permission Clause
        //            //if (!(await AuthorizeAsync(Policy.ReviewNewIdeas)).Succeeded
        //            //    && !(await AuthorizeAsync(Policy.ReviewAssessedIdeas)).Succeeded
        //            //    && !(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
        //            //{
        //            //    feedback.DangerMessage("You do not have permission to save this idea.");
        //            //    return BadRequest(feedback);
        //            //}


        //            var tenant = await GetClient(model.ClientId, model.UserId, true);


        //            //// Can add process permissions Clause
        //            //var message = await CanAddProcess(
        //            //    new Models.Business.Client(tenant),
        //            //    "Cannot add additional process ideas.");

        //            //if (!string.IsNullOrWhiteSpace(message))
        //            //{
        //            //    feedback.WarningMessage(message);
        //            //    return BadRequest(feedback);
        //            //}



        //            //// Can add Collaborators permissions Clause
        //            //message = CanAddCollaborator(model.Collaborators);
        //            //if (!string.IsNullOrWhiteSpace(message))
        //            //{
        //            //    feedback.WarningMessage(message);
        //            //    return BadRequest(feedback);
        //            //}




        //            model.ClientId = tenant.Id;

        //            // Prepare the IdeaApplicationVersions for validation, by Idea.Id
        //            foreach (var ideaApplicationVersion in model.IdeaApplicationVersions)
        //                ideaApplicationVersion.IdeaId = model.Id;



        //            if (!model.IsNew)
        //            {
        //                // Not new? Check if the idea already on the database?
        //                var coreOnDataStore = await _unitOfWork.BusinessIdeas.GetAsync(model.Id);

        //                // The model was not found on the database.
        //                // Log a hack and return view.
        //                if (coreOnDataStore == null)
        //                {
        //                    _unitOfWork.Log("The user attempted to save an idea with an incorrect Id.");

        //                    feedback.DangerMessage("The id is not valid");
        //                    return BadRequest(feedback);
        //                }
        //            }


        //            // Validate
        //            if (string.IsNullOrWhiteSpace(model.Name))
        //                feedback.Add("Name", "The name of your idea is missing");
        //            else if (model.Name.Length > 100)
        //                feedback.Add("Name", "Name must be between 1 and 100 in length");


        //            if (string.IsNullOrWhiteSpace(model.Summary))
        //                feedback.Add("Summary", "Summary is missing");
        //            else if (model.Summary.Length > 750)
        //                feedback.Add("Summary", "Name must be between 1 and 750 in length");


        //            var uniqueMessage = await _unitOfWork.IsUniqueAsync(model.GetCore());
        //            if (!string.IsNullOrWhiteSpace(uniqueMessage))
        //                feedback.Add("Name", "Your idea name is not unique");


        //            //if (!model.IsDraft
        //            //    && model.SubmissionPathId != Data.Core.Enumerators.SubmissionPath.StandardUser.ToString())
        //            //    feedback = Validate(model, feedback);



        //            // Is NOT valid?
        //            if (!feedback.IsValid)
        //                return BadRequest(feedback);



        //            var core = model.GetCore();

        //            if (!core.IsDraft)
        //            {
        //                // Add stage if the idea is not draft
        //                if (!core.IsNew)
        //                    await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(core);

        //                if (!core.IdeaStages.Any())
        //                {
        //                    //First save ideaa
        //                    await _unitOfWork.AddAsync(core);

        //                    //then it's stages and status
        //                    await Models.Business.IdeaStage.AddWorkFlow(_unitOfWork, core);


        //                    await _unitOfWork.CompleteAsync();

        //                    //continue
        //                    // Add Applications
        //                    await SaveApplicationsListAsync(
        //                        model.IdeaApplicationVersions,
        //                        model.Id);

        //                    var userId = model.UserId;

        //                    await Models.Business.Collaborator.UpdateAsync(
        //                        _unitOfWork,
        //                        model.Collaborators,
        //                        model.Id,
        //                        userId);


        //                    await _unitOfWork.CompleteAsync();
        //                }
        //            }
        //            else
        //            {


        //                await _unitOfWork.AddAsync(core);

        //                await _unitOfWork.CompleteAsync();

        //                // Add Applications
        //                await SaveApplicationsListAsync(
        //                    model.IdeaApplicationVersions,
        //                    model.Id);

        //                var userId = model.UserId;

        //                await Models.Business.Collaborator.UpdateAsync(
        //                    _unitOfWork,
        //                    model.Collaborators,
        //                    model.Id,
        //                    userId);


        //                await _unitOfWork.CompleteAsync();

        //            }

        //        }

        //        return Ok(new { Message = "Ceo Ideas Added successfully." });

        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok();
        //    }
        //}




		[HttpPost("/api/ApisController/UploadCeoBulkData")]

		public async Task<IActionResult> UploadCeoBulkData([FromForm] IFormFile File)
		{
			try
			{
				string fileName = File.FileName;
				string uniqueName = Guid.NewGuid().ToString() + "-" + fileName.Replace(' ', '_');
				string filePath = "wwwroot/IdeaFiles/" + uniqueName;
				using (FileStream stream = new FileStream(filePath, FileMode.CreateNew))
				{
					await File.CopyToAsync(stream);
				}

				List<APIControllers.SilkFloApis.COEBulkIdeaModel> rows = new List<APIControllers.SilkFloApis.COEBulkIdeaModel>();
				if (File.FileName.ToLower().Contains(".xlsx") || File.FileName.ToLower().Contains(".xls"))
				{
					rows = await UploadCeoExcelFile(File, filePath);
				}
				else
				{
					return Ok(new { status = false, message = "Invalid Data!", });
				}

				var feedback = new Feedback();

				return Ok(new { status = true, message = "Ceo Ideas Added successfully." , data=rows});

			}
			catch (Exception ex)
			{
				return Ok();
			}
		}


		[HttpPost("/api/ApisController/SaveCOEData")]
       public async Task<IActionResult> SaveCOEData([FromBody] JArray tableData)
        { 
		    try
		    {

				//var isss = (await _unitOfWork.Users.GetUsingEmailAsync("jimmy@silkflo.com"))?.Id;

				var feedback = new Feedback();


				var rows = tableData.ToObject<List<APIControllers.SilkFloApis.COEBulkIdeaModel>>();

				List<Models.Business.Idea> idealist = new List<Models.Business.Idea>();

				foreach (var row in rows)
				{
					Models.Business.Idea idea = new Models.Business.Idea();


                    idea.Name = row.Name;
                    idea.CreatedDate = row.CreatedDate;
					idea.CreatedById = (await _unitOfWork.Users.GetUsingEmailAsync(row.SubmitterEmailAddress?.Trim()))?.Id;

					idea.ProcessOwnerId = string.IsNullOrWhiteSpace(row.ProcessOwnerEmailAddress?.Trim())
						? null
						: (await _unitOfWork.Users.GetUsingEmailAsync(row.ProcessOwnerEmailAddress.Trim()))?.Id;
					//idea.AutomationGoalId = row.AutomationId; //// not know
                    idea.Summary = row.Description;
     //               idea.DepartmentId = (await _unitOfWork.BusinessDepartments.GetByNameAsync(row.DepartmentId?.Trim()))?.Id;
					//idea.TeamId = (await _unitOfWork.BusinessTeams.GetByNameAsync(row.TeamId?.Trim()))?.Id;
					//idea.ProcessId = (await _unitOfWork.BusinessProcesses.GetByNameAsync(row.ProcessId?.Trim()))?.Id;
                    idea.DepartmentId=row.DepartmentId;
                    idea.TeamId=row.TeamId;
                    idea.ProcessId = row.ProcessId;

					idea.Stage = row.Stage;
                    idea.Status = row.Status;
                    //idea.DeployeementDate = row.DeployeementDate;   pending
                    idea.RuleId = (await _unitOfWork.SharedRules.GetByNameAsync(row.RuleId))?.Id;
					idea.InputId = (await _unitOfWork.SharedInputs.GetByNameAsync(row.InputId))?.Id;
					idea.InputDataStructureId = (await _unitOfWork.SharedInputDataStructures.GetByNameAsync(row.InputDataStructureId))?.Id;
					idea.ProcessStabilityId = (await _unitOfWork.SharedProcessStabilities.GetByNameAsync(row.ProcessStabilityId))?.Id;
					idea.DocumentationPresentId = (await _unitOfWork.SharedDocumentationPresents.GetByNameAsync(row.DocumentationPresentId))?.Id;
					idea.AutomationGoalId = (await _unitOfWork.SharedAutomationGoals.GetByNameAsync(row.AutomationGoalId))?.Id; ///// spelling mistake
					idea.ApplicationStabilityId = (await _unitOfWork.SharedApplicationStabilities.GetByNameAsync(row.ApplicationStabilityId))?.Id;
					idea.TaskFrequencyId = (await _unitOfWork.SharedTaskFrequencies.GetByNameAsync(row.TaskFrequencyId))?.Id;
					idea.AverageReviewTimeComment = row.AverageReviewTimeComment;
					idea.ProcessPeakId = (await _unitOfWork.SharedProcessPeaks.GetByNameAsync(row.ProcessPeakId))?.Id;
					idea.AverageNumberOfStepId = (await _unitOfWork.SharedAverageNumberOfSteps.GetByNameAsync(row.AverageNumberOfStepId))?.Id;
					idea.NumberOfWaysToCompleteProcessId = (await _unitOfWork.SharedNumberOfWaysToCompleteProcesses.GetByNameAsync(row.NumberOfWaysToCompleteProcessId))?.Id;
					idea.DataInputPercentOfStructuredId = (await _unitOfWork.SharedDataInputPercentOfStructureds.GetByNameAsync(row.DataInputPercentOfStructuredId))?.Id;
					idea.DecisionCountId = (await _unitOfWork.SharedDecisionCounts.GetByNameAsync(row.DecisionCountId))?.Id;
					idea.DecisionDifficultyId = (await _unitOfWork.SharedDecisionDifficulties.GetByNameAsync(row.DecisionDifficultyId))?.Id;
					idea.AverageWorkingDay = row.AverageWorkingDay;
					idea.AverageEmployeeFullCost = row.AverageEmployeeFullCost;
					idea.ActivityVolumeAverage = row.ActivityVolumeAverage;
					idea.EmployeeCount = row.EmployeeCount;
					idea.AverageErrorRate = row.AverageErrorRate;
					idea.WorkingHour = row.WorkingHour;
					idea.AverageProcessingTime = row.AverageProcessingTime;
					idea.AverageReworkTime = row.AverageReworkTime;
					idea.AverageWorkToBeReviewed = row.AverageWorkToBeReviewed;
					idea.PotentialFineAmount = row.PotentialFineAmount;
					idea.PotentialFineProbability = row.PotentialFineProbability;
					idea.IsHighRisk = (row.IsHighRisk?.ToLower()) == "yes";
					idea.IsDataSensitive = (row.IsDataSensitive?.ToLower()) == "yes";
                    idea.IsAlternative = (row.IsAlternative?.ToLower()) == "yes";
                    idea.IsHostUpgrade = (row.IsHostUpgrade?.ToLower()) == "yes";
                    idea.IsDataInputScanned = (row.IsDataInputScanned?.ToLower()) == "yes";
                    idea.SubmissionPathId = "COEUser";
					idealist.Add(idea);
				}



                foreach (var model in idealist)
                {

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

                    var dep = await _unitOfWork.BusinessDepartments.FindAsync(x => x.Name == model.DepartmentId && x.ClientString == tenant.Name);
                    model.DepartmentId = dep.FirstOrDefault().Id;
					var tea = await _unitOfWork.BusinessTeams.FindAsync(x => x.Name == model.TeamId && x.ClientString == tenant.Name);

					model.TeamId = tea.FirstOrDefault().Id;
					var pro = await _unitOfWork.BusinessProcesses.FindAsync(x => x.Name == model.ProcessId && x.ClientString == tenant.Name);

					model.ProcessId = pro.FirstOrDefault().Id;
					
                    
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


                    var uniqueMessage = await _unitOfWork.IsUniqueAsync(model.GetCore());
                    if (!string.IsNullOrWhiteSpace(uniqueMessage))
                        feedback.Add("Name", "Your idea name is not unique");


                    //if (!model.IsDraft
                    //    && model.SubmissionPathId != Data.Core.Enumerators.SubmissionPath.StandardUser.ToString())
                    //    feedback = Validate(model, feedback);



                    // Is NOT valid?
                    if (!feedback.IsValid)
                        return BadRequest(feedback);



                    var core = model.GetCore();

                    if (!core.IsDraft)
                    {
                        // Add stage if the idea is not draft
                        if (!core.IsNew)
                            await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(core);

                        if (!core.IdeaStages.Any())
                        {
                            //First save ideaa
                            await _unitOfWork.AddAsync(core);

                            //then it's stages and status
                            await AddWorkFlowByApi(_unitOfWork, core, model.Stage, model.Status);


                            await _unitOfWork.CompleteAsync();

                            //continue
                            // Add Applications
                            await SaveApplicationsListAsync(
                                model.IdeaApplicationVersions,
                                model.Id);

                            var userId = model.CreatedById;

                            await Models.Business.Collaborator.UpdateAsync(
                                _unitOfWork,
                                model.Collaborators,
                                model.Id,
                                userId);


                            await _unitOfWork.CompleteAsync();
                        }
                    }
                    else
                    {


                        await _unitOfWork.AddAsync(core);

                        await _unitOfWork.CompleteAsync();

                        // Add Applications
                        await SaveApplicationsListAsync(
                            model.IdeaApplicationVersions,
                            model.Id);

                        var userId = model.CreatedById;

                        await Models.Business.Collaborator.UpdateAsync(
                            _unitOfWork,
                            model.Collaborators,
                            model.Id,
                            userId);


                        await _unitOfWork.CompleteAsync();

                    }

                }

				return Ok(new { status = true, message = "Ceo Ideas Added successfully."});

			}
			catch (Exception ex)
		    {
		        return Ok();
		    }
		}







		public static async Task AddWorkFlowByApi(
		 Data.Core.IUnitOfWork unitOfWork,
		 Data.Core.Domain.Business.Idea idea, string FStage, string FStatus)
		{
            try
            {
                var SStage = await unitOfWork.SharedStages.GetByNameAsync(FStage);

                var SStatus = await unitOfWork.SharedIdeaStatuses.FindAsync(x => x.Name == FStatus && x.StageString == FStage);

                //var firstStage = Data.Core.Enumerators.Stage.n00_Idea;
                //if (idea.SubmissionPathId == Data.Core.Enumerators.SubmissionPath.COEUser.ToString())
                var firstStage = SStage;

                var date = DateTime.Now;
                var ideaStage = new Data.Core.Domain.Business.IdeaStage
                {
                    Idea = idea,
                    StageId = firstStage?.Id,
                    DateStartEstimate = date,
                    DateStart = date,
                    IsInWorkFlow = true,
                };

                await unitOfWork.AddAsync(ideaStage);
                await unitOfWork.CompleteAsync();

                //if (firstStage == Data.Core.Enumerators.Stage.n00_Idea)
                //{
                //	var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
                //	{
                //		IdeaStageId = ideaStage.Id,
                //		StatusId = Data.Core.Enumerators.IdeaStatus.n00_Idea_AwaitingReview.ToString(),
                //		Date = date
                //	};
                //	await unitOfWork.AddAsync(ideaStageStatus);
                //	await unitOfWork.CompleteAsync();
                //}
                //else
                //{

                var zz = SStatus.FirstOrDefault().Id;
                var tStatusId = Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString();

                var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
                {
                    IdeaStageId = ideaStage.Id,
                    StatusId = SStatus.FirstOrDefault().Id,
                    Date = date
                };
                await unitOfWork.AddAsync(ideaStageStatus);
                await unitOfWork.CompleteAsync();
                //}

                var stages = (await unitOfWork.SharedStages.FindAsync(x => x.Id != firstStage.ToString())).ToArray();
                //if (firstStage == Data.Core.Enumerators.Stage.n01_Assess)
                stages = stages.Where(x => x.Id != Data.Core.Enumerators.Stage.n00_Idea.ToString()).ToArray();

                var now = DateTime.Now;
                foreach (var stage in stages)
                {
                    ideaStage = new Data.Core.Domain.Business.IdeaStage
                    {
                        Idea = idea,
                        DateStartEstimate = now,
                        Stage = stage
                    };

                    now = now.AddSeconds(1);
                    await unitOfWork.AddAsync(ideaStage);
                }
                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
            }
		}














		public async Task<List<APIControllers.SilkFloApis.COEBulkIdeaModel>> UploadCeoExcelFile(IFormFile File, string filepath)
        {
            List<APIControllers.SilkFloApis.COEBulkIdeaModel> rows = new List<APIControllers.SilkFloApis.COEBulkIdeaModel>();
            try
            {
                FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding.GetEncoding(1252);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(fileStream);
                System.Data.DataSet dataSet = reader.AsDataSet(
                    new ExcelDataSetConfiguration()
                    {
                        UseColumnDataType = false,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = false,
                        }
                    });

				// for (int i = 4; i < dataSet.Tables[0].Rows.Count; i++)
				 for (int i = 3; i < dataSet.Tables[0].Rows.Count; i++)

				{

					APIControllers.SilkFloApis.COEBulkIdeaModel fileReader = new
				   APIControllers.SilkFloApis.COEBulkIdeaModel
					{
                        Name = dataSet.Tables[0].Rows[i].ItemArray[0] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[0]) : string.Empty,
						CreatedDate = DateTime.TryParse(Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[1]), out DateTime parsedDate) ? parsedDate : DateTime.MinValue,
						SubmitterEmailAddress = dataSet.Tables[0].Rows[i].ItemArray[2] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[2]) : string.Empty,
						ProcessOwnerEmailAddress = dataSet.Tables[0].Rows[i].ItemArray[3] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[3]) : string.Empty,
						AutomationId = dataSet.Tables[0].Rows[i].ItemArray[4] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[4]) : string.Empty,
						Description = dataSet.Tables[0].Rows[i].ItemArray[5] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[5]) : string.Empty,
                        DepartmentId = dataSet.Tables[0].Rows[i].ItemArray[6] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[6]) : string.Empty,
                        TeamId = dataSet.Tables[0].Rows[i].ItemArray[7] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[7]) : string.Empty,
                        ProcessId = dataSet.Tables[0].Rows[i].ItemArray[8] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[8]) : string.Empty,
						Stage = dataSet.Tables[0].Rows[i].ItemArray[9] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[9]) : string.Empty,
						Status = dataSet.Tables[0].Rows[i].ItemArray[10] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[10]) : string.Empty,
						DeployeementDate = DateTime.TryParse(Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[11]), out DateTime deployeementDate) ? deployeementDate : DateTime.MinValue,
						RuleId = dataSet.Tables[0].Rows[i].ItemArray[12] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[12]) : string.Empty,
                        InputId = dataSet.Tables[0].Rows[i].ItemArray[13] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[13]) : string.Empty,
                        InputDataStructureId = dataSet.Tables[0].Rows[i].ItemArray[14] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[14]) : string.Empty,
                        ProcessStabilityId = dataSet.Tables[0].Rows[i].ItemArray[15] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[15]) : string.Empty,
                        DocumentationPresentId = dataSet.Tables[0].Rows[i].ItemArray[16] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[16]) : string.Empty,
                        AutomationGoalId= dataSet.Tables[0].Rows[i].ItemArray[17] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[17]) : string.Empty,
						ApplicationStabilityId = dataSet.Tables[0].Rows[i].ItemArray[18] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[18]) : string.Empty,
						AverageWorkingDay = dataSet.Tables[0].Rows[i].ItemArray[19] != null ? int.Parse(dataSet.Tables[0].Rows[i].ItemArray[19].ToString()) : 0,
						WorkingHour = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[20]?.ToString(), out decimal workingHourResult) ? workingHourResult : 0.0m,
					    AverageEmployeeFullCost = dataSet.Tables[0].Rows[i].ItemArray[21] != null ? int.Parse(dataSet.Tables[0].Rows[i].ItemArray[21].ToString()) : 0,
						EmployeeCount = dataSet.Tables[0].Rows[i].ItemArray[22] != null ? int.Parse(dataSet.Tables[0].Rows[i].ItemArray[22].ToString()) : 0,
						TaskFrequencyId = dataSet.Tables[0].Rows[i].ItemArray[23] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[23]) : string.Empty,
						ActivityVolumeAverage= dataSet.Tables[0].Rows[i].ItemArray[24] != null ? int.Parse(dataSet.Tables[0].Rows[i].ItemArray[24].ToString()) : 0,
						AverageProcessingTime = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[25]?.ToString(), out decimal averageProcessingTimeResult) ? averageProcessingTimeResult : 0.0m,
					    AverageErrorRate = dataSet.Tables[0].Rows[i].ItemArray[26] != null ? int.Parse(dataSet.Tables[0].Rows[i].ItemArray[26].ToString()) : 0,
						AverageReworkTime = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[27]?.ToString(), out decimal averageReworkTimeResult) ? averageReworkTimeResult : 0.0m,
						AverageWorkToBeReviewed = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[28]?.ToString(), out decimal averageWorkToBeReviewedResult) ? averageWorkToBeReviewedResult : 0.0m,
					    AverageReviewTimeComment = dataSet.Tables[0].Rows[i].ItemArray[29] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[29]) : string.Empty,
					    ProcessPeakId = dataSet.Tables[0].Rows[i].ItemArray[30] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[30]) : string.Empty,
					    AverageNumberOfStepId= dataSet.Tables[0].Rows[i].ItemArray[31] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[31]) : string.Empty,
					    NumberOfWaysToCompleteProcessId= dataSet.Tables[0].Rows[i].ItemArray[32] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[32]) : string.Empty,
					    DataInputPercentOfStructuredId = dataSet.Tables[0].Rows[i].ItemArray[33] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[33]) : string.Empty,
					    DecisionCountId = dataSet.Tables[0].Rows[i].ItemArray[34] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[34]) : string.Empty,
					    DecisionDifficultyId = dataSet.Tables[0].Rows[i].ItemArray[35] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[35]) : string.Empty,
                        PotentialFineAmount = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[36]?.ToString(), out decimal potentialFineAmountResult) ? potentialFineAmountResult : 0.0m,
					    PotentialFineProbability = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[37]?.ToString(), out decimal potentialFineProbabilityResult) ? potentialFineProbabilityResult : 0.0m,
					    IsHighRisk = dataSet.Tables[0].Rows[i].ItemArray[38] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[38]) : string.Empty,
						IsDataSensitive = dataSet.Tables[0].Rows[i].ItemArray[39] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[39]) : string.Empty,
						IsAlternative = dataSet.Tables[0].Rows[i].ItemArray[40] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[40]) : string.Empty,
						IsHostUpgrade = dataSet.Tables[0].Rows[i].ItemArray[41] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[41]) : string.Empty,
						IsDataInputScanned = dataSet.Tables[0].Rows[i].ItemArray[42] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[42]) : string.Empty,




						//////////////
						///

						//ProcessOwnerId = dataSet.Tables[0].Rows[i].ItemArray[10] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[10]) : string.Empty,
						//                  Rating = dataSet.Tables[0].Rows[i].ItemArray[11] != null ? int.Parse(dataSet.Tables[0].Rows[i].ItemArray[11].ToString()) : 0,
						//                  ClientId = dataSet.Tables[0].Rows[i].ItemArray[12] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[12]) : string.Empty,                      
						//                  UserId = dataSet.Tables[0].Rows[i].ItemArray[13] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[13]) : string.Empty,
						//                  SubmissionPathId = dataSet.Tables[0].Rows[i].ItemArray[14] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[14]) : string.Empty,

					};

                    rows.Add(fileReader);

                }
                return rows;
            }
            catch (Exception ex)
            {
                return rows;
            }





        }




        [HttpGet("/ApisController/Upload-Pipeline/{tab}")]
        //[HttpGet("/Settings/tenant/Platform-Setup/{tab}/Software-Vendor")]
        //[HttpGet("/Settings/tenant/Platform-Setup/{tab}/Initial-Costs")]
        //[HttpGet("/Settings/tenant/Platform-Setup/{tab}/Running-Costs")]
        //[HttpGet("/Settings/tenant/Platform-Setup/{tab}/Other-Running-Costs")]
        public async Task<IActionResult> GetPipelinesetupcost(string tab)
        {
            return await CreateUploadPipelineSetupView(false, tab);
        }



        [HttpGet("/api/ApisController/Upload-Pipeline/{tab}")]
		public async Task<IActionResult> GetPipelineApi(string tab)
		{
			return await CreateUploadPipelineSetupView(true, tab);
		}


        [HttpGet("/ApisController/Upload-Pipeline")]
        public IActionResult GetPlatformSetup()
        {
            return Redirect("/ApisController/Upload-Pipeline/Pipeline-Units");
        }


        private async Task<IActionResult> CreateUploadPipelineSetupView(
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

				return View("../home/Page", "<h1 class=\"text-warning\">You do not have permissions to manage platform settings.</h1>");
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
					tab = "Pipeline-Units";

				var uploadPipeline = new ViewModels.Settings.UploadPipeline(
					tab,
					client.IsPractice);


				const string url = "/Views/Settings/UploadPipeline.cshtml";
				if (returnStringContent)
				{
					var html = await _viewToString.PartialAsync(url, uploadPipeline);
					return Content(html);
				}

				return View("/Views/Settings/UploadPipeline.cshtml", uploadPipeline);
			}
			catch (Exception ex)
			{
				Log(ex);

				if (returnStringContent)
					return Content("Error fetching data");


				return View("/Views/Home/maintenance.cshtml", "CreatePlatformSetupView");
			}
		}






		//[HttpGet("/api/ApisController/Upload-Pipeline")]
  //      public async Task<IActionResult> GetPipeline()
  //      {
  //          //// Authorization Clause
  //          //if (!(await AuthorizeAsync(Policy.ManageAgencySettings)).Succeeded)
  //          //    return NegativeFeedback();
       
  //          //return await CreateTenantsView(true);
  //          return View("/Views/Settings/UploadPipeline.cshtml");
  //      }


        private async Task<bool> IsNewUser(Data.Core.Domain.User user = null)
        {
            var userId = GetUserId();
            user ??= await _unitOfWork.Users.GetAsync(userId);

            await _unitOfWork.BusinessIdeas.GetForProcessOwnerAsync(user);


            if (!user.Ideas.Any())
            {
                await _unitOfWork.BusinessCollaborators.GetForUserAsync(user);

                user.Collaborators = (await _unitOfWork.BusinessCollaborators
                                                       .FindAsync(x => x.UserId == userId
                                                                    && x.IsInvitationConfirmed))
                                                       .ToList();

                if (!user.Collaborators.Any())
                    return true;
            }

            return false;
        }


        internal static decimal GetChangeIn(int previous, int current)
        {
            decimal chargeIn;

            if (previous == current)
                return 0;

            if (current > previous)
            {
                if (previous == 0)
                    return 100;

                chargeIn = previous / (decimal)current * 100;
            }
            else
            {
                if (current == 0)
                    return -100;

                chargeIn = current / (decimal)previous * -100;
            }

            return chargeIn;
        }




    }
}
