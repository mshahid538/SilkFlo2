using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SilkFlo.Data.Core;
using SilkFlo.Data.Persistence;
using SilkFlo.Web.ViewModels;

namespace SilkFlo.Web.APIControllers.Business
{
    public class APIDepartmentController : Controllers.AbstractAPI
    {
        public APIDepartmentController(Data.Core.IUnitOfWork unitOfWork,
                                    Services.ViewToString viewToString,
                                    IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }


        [HttpPost("/api/Business/Department/Post")]
        public async Task<IActionResult> Post([FromBody] Models.Business.Department model)
        {
            var feedback = new Feedback();
            feedback.DangerMessage("Unauthorised");

            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return BadRequest(feedback);

            if (model == null)
            {
                feedback.Message = "Business Unit missing.";
                return BadRequest(feedback);
            }


            var client = await GetClientAsync();

            if(client == null)
                return BadRequest(feedback);

            model.ClientId = client.Id;

            if (string.IsNullOrWhiteSpace(model.Id))
            {
                try
                {
                    await _unitOfWork.AddAsync(model.GetCore());
                    await _unitOfWork.CompleteAsync();

                    return Ok(model.Id);
                }
                catch (InvalidFieldsException ex)
                {
                    feedback.Message = ex.Message;
                    return BadRequest(feedback);
                }
                catch (UniqueConstraintException ex)
                {
                    feedback.Message = ex.Message;
                    return BadRequest(feedback);
                }
                catch (Exception ex)
                {
                    Log(ex);
                    feedback.Message = "Save Failed";
                    return BadRequest(feedback);
                }
            }



            var department = await _unitOfWork.BusinessDepartments.GetAsync(model.Id);

            if(department.ClientId != client.Id)
                return BadRequest(feedback);

            if (department.Name == model.Name)
                return Ok("Saved Department");

            try
            {
                await _unitOfWork.AddAsync(model.GetCore());
                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            catch (InvalidFieldsException ex)
            {
                feedback.Message = ex.Message;
                return BadRequest(feedback);
            }
            catch (UniqueConstraintException ex)
            {
                feedback.Message = ex.Message;
                return BadRequest(feedback);
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.Message = "Save Failed";
                return BadRequest(feedback);
            }
        }

        [HttpGet("api/Business/Department/GetForBusinessUnits/SearchText/{text}")]
        [HttpGet("api/Business/Department/GetForBusinessUnits/SearchText")]
        public async Task<IActionResult> GetForBusinessUnits(string text = "")
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            var tenant = await GetClientAsync();

            // Guard Clause
            if (tenant == null)
                return NegativeFeedback();


            text = text.Trim().ToLower();


            List<Data.Core.Domain.Business.Department> cores;

            if (string.IsNullOrWhiteSpace(text))
            {
                cores = (await _unitOfWork
                    .BusinessDepartments
                    .FindAsync(x => x.ClientId == tenant.Id)).ToList();
            }
            else
            {
                var exactMatchRequired = false;
                if (text.IndexOf("\"", StringComparison.Ordinal) == 0
                    && text.IndexOf("\"", 1, StringComparison.Ordinal) > 1)
                {
                    exactMatchRequired = true;
                    text = text.Substring(1);
                    text = text.Substring(0, text.Length - 1);
                }




                if (exactMatchRequired)
                {
                    cores = (await _unitOfWork
                        .BusinessDepartments
                        .FindAsync(x => x.Name == text
                                        && x.ClientId == tenant.Id)).ToList();
                }
                else
                {
                    cores = (await _unitOfWork
                        .BusinessDepartments
                        .FindAsync(x => x.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) > -1
                                        && x.ClientId == tenant.Id)).ToList();
                }
            }


            if (!cores.Any())
            {
                return Content("");
            }

            var models = Models.Business.Department.Create(cores);



            var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/BusinessUnit/_BusinessUnits.cshtml", models);
            return Content(html);
        }

        [HttpDelete("/api/Models/Business/Department/Delete/Id/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var feedback = new Feedback();
            feedback.DangerMessage("Unauthorised");

            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return BadRequest(feedback);


            id = id.Trim();

            try
            {
                var result = await _unitOfWork.BusinessDepartments.RemoveAsync(id);

                if (result == Data.Core.DataStoreResult.NotFound)
                {
                    feedback.Message = "Not Found";
                    return BadRequest(feedback);
                }

                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            catch (ChildDependencyException ex)
            {
                feedback.Message = ex.Message;
                return BadRequest(feedback);
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.Message = "Delete Failed";
                return BadRequest(feedback);
            }
        }
    }
}