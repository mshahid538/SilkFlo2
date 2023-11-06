using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkFlo.Data.Core;
using SilkFlo.Data.Persistence;
using SilkFlo.Web.ViewModels;

namespace SilkFlo.Web.Controllers.Business
{
    public class RoleCostController : AbstractAPI
    {
        public RoleCostController(Data.Core.IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }

        [HttpPost("/api/Business/RoleCost/Post")]
        public async Task<IActionResult> Post([FromBody] Models.Business.RoleCost model)
        {
            var feedback = new Feedback();
            feedback.DangerMessage("Unauthorised");

            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return BadRequest(feedback);


            // Guard Clause
            if (model == null
                || string.IsNullOrWhiteSpace(model.RoleId))
            {
                feedback.Message = "Role missing.";
                return BadRequest(feedback);
            }


            var client = await GetClientAsync();


            // Authorization Clause
            if (client == null)
                return BadRequest(feedback);



            model.ClientId = client.Id;


            // Is there a name conflict?

            var message = await _unitOfWork.IsUniqueAsync(model.GetCore());

            if (!string.IsNullOrWhiteSpace(message))
            {
                feedback.Message = "A record for this role already exists.";
                return BadRequest(feedback);
            }



            var saveMe = false;

            var core = await _unitOfWork.BusinessRoleCosts.GetAsync(model.Id);
            if (core == null)
            {
                // New record
                core = model.GetCore();
                core.Client = client;
                saveMe = true;
            }
            else if (core.RoleId != model.RoleId)
            {
                // Role changed
                core.RoleId = model.RoleId;
                saveMe = true;
            }


            if (core.Cost != model.Cost)
            {
                core.Cost = model.Cost;
                saveMe = true;
            }



            // Save
            if (!saveMe) return Ok();


            try
            {
                await _unitOfWork.AddAsync(core);
                await _unitOfWork.CompleteAsync();

                return Ok(core.Id);
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
                feedback.Message = "Save Failed.";
                return BadRequest(feedback);
            }
        }


        [HttpDelete("/api/Business/RoleCost/Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var feedback = new Feedback();
            feedback.DangerMessage("Unauthorised");

            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return BadRequest(feedback);

            try
            {
                id = id.Trim();
                var result = await _unitOfWork.BusinessRoleCosts.RemoveAsync(id);

                if (result == DataStoreResult.NotFound)
                {
                    feedback.Message = "Not Found";
                    return BadRequest(feedback);
                }

                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            catch (ChildDependencyException ex)
            {
                return BadRequest(ex.Message);
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