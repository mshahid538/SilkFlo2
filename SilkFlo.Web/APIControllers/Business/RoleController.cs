using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Web.Controllers;
using SilkFlo.Web.ViewModels;

namespace SilkFlo.Web.APIControllers.Business
{
    public class RoleController : AbstractAPI
    {
        [HttpGet("/api/Business/Role/SummaryDetail")]
        public async Task<IActionResult> GetSummaryDetail()
        {
            if (!(await AuthorizeAsync(Policy.ManageTenantUserRoles)).Succeeded)
                return NegativeFeedback();

            try
            {
                var tenant = await GetClientAsync();

                var cores = (await _unitOfWork.BusinessRoles.FindAsync(x => x.IsBuiltIn || x.ClientId == tenant.Id))
                                    .OrderByDescending(x => x.IsBuiltIn)
                                    .ThenBy(x => x.Sort).ToArray();

                await _unitOfWork.BusinessRoleIdeaAuthorisations.GetForRoleAsync(cores);

                foreach (var role in cores)
                    await _unitOfWork.SharedIdeaAuthorisations.GetIdeaAuthorisationForAsync(role.RoleIdeaAuthorisations);


                var models = Models.Business.Role.Create(cores);

                var ideaAuthorization = await _unitOfWork.SharedIdeaAuthorisations.GetAllAsync();
                await PrepareCollaboratorRole(models[0], tenant, ideaAuthorization);



                var html = await _viewToString.PartialAsync("Shared/Business/Role/_Index.cshtml", models);

                return Content(html);

            }
            catch (Exception ex)
            {
                Log(ex);
                return BadRequest();
            }
        }


        [HttpGet("/api/Business/Role/Table")]
        [HttpGet("/api/Business/Role/Table/SelectedId/{id}")]
        [HttpGet("/api/Business/Role/Table/Search/{text}")]
        [HttpGet("/api/Business/Role/Table/Search/{text}/SelectedId/{id}")]
        public async Task<IActionResult> GetTable(string text = "",
                                                  string id = "")
        {
            if (!(await AuthorizeAsync(Policy.ManageTenantUserRoles)).Succeeded)
                return NegativeFeedback();

            try
            {
                var tenant = await GetClientAsync();

                var cores = (await _unitOfWork.BusinessRoles.FindAsync(x => x.IsBuiltIn || x.ClientId == tenant.Id))
                                    .OrderByDescending(x => x.IsBuiltIn)
                                    .ThenBy(x => x.Sort);

                var models = Models.Business.Role.Create(cores);


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
                        models = models.Where(x => x.Name.ToLower() == text
                                                || x.Description.ToLower() == text).ToList();
                    else
                        models = models.Where(x => x.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.Description.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1).ToList();
                }


                if (!string.IsNullOrWhiteSpace(id))
                {
                    var newModel = models.SingleOrDefault(x => x.Id == id);
                    if (newModel != null)
                    {
                        models.Remove(newModel);
                        newModel.IsSelected = true;
                        models.Insert(0, newModel);
                    }
                }

                string html = await _viewToString.PartialAsync("Shared/Business/Role/_Summary.cshtml", models);

                return Content(html);

            }
            catch (Exception ex)
            {
                Log(ex);
                return BadRequest();
            }
        }

        [HttpGet("/api/Business/Role/TableRow/{id}")]
        public async Task<IActionResult> GetTableRow(string id)
        {
            if (!(await AuthorizeAsync(Policy.ManageTenantUserRoles)).Succeeded)
                return  NegativeFeedback();

            if (string.IsNullOrWhiteSpace(id))
                return NegativeFeedback("Error: id is missing");


            try
            {

                var core = await _unitOfWork.BusinessRoles.SingleOrDefaultAsync(x => x.Id == id);

                if (core == null)
                    return NegativeFeedback("Error: No role found");



                var model = new Models.Business.Role(core)
                {
                    IsSelected = true
                };

                //var html = await _viewToString.PartialAsync("Shared/business/role/_SummaryRowContent.cshtml", model);
                //return Content(html);

                return Content(core.Name);
            }
            catch (Exception ex)
            {
                Log(ex);
                return NegativeFeedback("Failed to get row");
            }
        }




        [HttpGet("/api/Business/Role/GetDetail/id/{id}")]
        [HttpGet("/api/Business/Role/GetDetail")]
        public async Task<IActionResult> GetCollaboratorRole(string id)
        {
            if (!(await AuthorizeAsync(Policy.ManageTenantUserRoles)).Succeeded)
                return NegativeFeedback();

            try
            {
                var tenant = await GetClientAsync();

                Models.Business.Role model;
                if (string.IsNullOrWhiteSpace(id))
                    model = new Models.Business.Role();
                else
                {
                    var core = await _unitOfWork.BusinessRoles.GetAsync(id);

                    if(core == null)
                        return NegativeFeedback($"Cannot find role with id {id}.");

                    model = new Models.Business.Role(core);
                }


                var ideaAuthorisation = await _unitOfWork.SharedIdeaAuthorisations.GetAllAsync();

                await PrepareCollaboratorRole(model, tenant, ideaAuthorisation);


                var html = await _viewToString.PartialAsync(
                    "Shared/Business/Role/_Detail.cshtml", 
                    model);

                return Content(html);

            }
            catch (Exception ex)
            {
                Log(ex);
                return NegativeFeedback("Error getting role details");
            }
        }



        public async Task PrepareCollaboratorRole(Models.Business.Role role,
                                                  Data.Core.Domain.Business.Client tenant,
                                                  IEnumerable<Data.Core.Domain.Shared.IdeaAuthorisation> ideaAuthorisations)
        {
            await _unitOfWork.BusinessCollaboratorRoles.GetForRoleAsync(role.GetCore());


            role.IdeaAuthorisations = Models.Shared.IdeaAuthorisation.Create(ideaAuthorisations);

            foreach (var ideaAuthorisation in role.IdeaAuthorisations)
            {
                var core = await _unitOfWork.BusinessRoleIdeaAuthorisations
                                            .SingleOrDefaultAsync(x => x.IdeaAuthorisationId == ideaAuthorisation.Id
                                                                    && x.RoleId == role.Id
                                                                    && x.ClientId == tenant.Id);
                if (core != null)
                    ideaAuthorisation.IsSelected = true;
            }
        }



        public RoleController(Data.Core.IUnitOfWork unitOfWork,
                              Services.ViewToString viewToString,
                              IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }




        [HttpPost("/api/Business/Role/Post")]
        public async Task<IActionResult> Post([FromBody] Models.Business.Role model)
        {
            var feedback = new Feedback
            {
                NamePrefix = "Business.Role."
            };

            if (!(await AuthorizeAsync(Policy.ManageTenantUserRoles)).Succeeded)
            {
                feedback.DangerMessage("Unauthorised");
                return BadRequest(feedback);
            }

            try
            {
                if (model == null)
                {
                    feedback.DangerMessage("No content supplied");
                    return BadRequest(feedback);
                }

                feedback.Message = "";

                var tenant = await GetClientAsync();


                var core = await _unitOfWork.BusinessRoles.SingleOrDefaultAsync(x => x.Id == model.Id
                    && (x.ClientId == tenant.Id || x.IsBuiltIn)) ?? new Data.Core.Domain.Business.BusinessRole();


                model.IdeaAuthorisations = model.IdeaAuthorisations
                    .Where(x => x.IsSelected)
                    .ToList();

                foreach (var ideaAuthorisation in model.IdeaAuthorisations)
                    ideaAuthorisation.Name = "Validation Fix - Not saved";

                ModelState.Clear();

                if (!ModelState.IsValid
                 && !core.IsBuiltIn)
                {
                    TryValidateModel(model);
                    feedback = GetFeedback(ModelState, feedback);
                    feedback = await model.CheckUniqueAsync(_unitOfWork, feedback);
                }



                if (model.IdeaAuthorisations.Any())
                    feedback.Elements.Add("IdeaAuthorisations", "");
                else
                {
                    feedback.Elements.Add("IdeaAuthorisations", "Please select items from the list.");
                    feedback.IsValid = false;
                }


                if (!feedback.IsValid)
                    return BadRequest(feedback);


                // Add or update the role
                if (!core.IsBuiltIn)
                {
                    var checkBuiltInName = 
                        await _unitOfWork.BusinessRoles
                                         .SingleOrDefaultAsync(x => x.IsBuiltIn 
                                                                && string.Equals(x.Name, 
                                                                                 model.Name, 
                                                                                 StringComparison.CurrentCultureIgnoreCase));

                    if (checkBuiltInName == null)
                    {
                        core.ClientId = tenant.Id;
                        core.Name = model.Name;
                        core.Description = model.Description;

                        await _unitOfWork.AddAsync(core);
                    }
                    else
                    {
                        feedback.Elements.Add("Name", "The role name is the same as a built in role.");
                        return BadRequest(feedback);
                    }
                }





                // Process authorisations
                if (!string.IsNullOrWhiteSpace(model.Id))
                {
                    var roleIdeaAuthorisations = await _unitOfWork.BusinessRoleIdeaAuthorisations.FindAsync(x => x.RoleId == model.Id
                                                                                                              && x.ClientId == tenant.Id);
                    await _unitOfWork.BusinessRoleIdeaAuthorisations.RemoveRangeAsync(roleIdeaAuthorisations);
                }

                foreach (var ideaAuthorisation in model.IdeaAuthorisations)
                {
                    var roleIdeaAuthorisationNew = new Data.Core.Domain.Business.RoleIdeaAuthorisation
                    {
                        ClientId = tenant.Id,
                        Role = core,
                        IdeaAuthorisationId = ideaAuthorisation.Id,
                    };

                    await _unitOfWork.AddAsync(roleIdeaAuthorisationNew);
                }

                await _unitOfWork.CompleteAsync();
                return Ok(core.Id);
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Role with the same name already exists.");
                return BadRequest(feedback);
            }
        }




        [HttpDelete("/api/Model/Business/Role/Delete/Id/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var feedback = new Feedback
            {
                NamePrefix = "Business.Role."
            };

            if (!(await AuthorizeAsync(Policy.ManageTenantUserRoles)).Succeeded)
            {
                feedback.DangerMessage("Unauthorised");
                return BadRequest(feedback);
            }

            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    feedback.DangerMessage("Role Id missing");
                    return BadRequest(feedback);
                }

                var result = await _unitOfWork.BusinessRoles.RemoveAsync(id);
                switch (result)
                {
                    case Data.Core.DataStoreResult.Success:
                    {
                        await _unitOfWork.CompleteAsync();
                        return Ok(id);
                    }
                    case Data.Core.DataStoreResult.NotFound:
                        feedback.DangerMessage("Record not found");
                        return BadRequest(feedback);
                    default:
                        feedback.DangerMessage("Failed to delete role.");
                        return BadRequest(feedback);
                }
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Error saving the role");
                return BadRequest(feedback);
            }
        }
    }
}
