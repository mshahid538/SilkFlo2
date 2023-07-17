using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkFlo.Data.Core;
using SilkFlo.Data.Persistence;
using SilkFlo.Web.ViewModels;

namespace SilkFlo.Web.Controllers.Business
{
    public class OtherRunningCostController : AbstractAPI
    {
        public OtherRunningCostController(IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }

        [HttpPost("/api/Business/otherRunningCost/Post")]
        public async Task<IActionResult> Post([FromBody] Models.Business.OtherRunningCost model)
        {
            var feedback = new Feedback();
            feedback.DangerMessage("Unauthorised");


            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return BadRequest(feedback);


            // Guard Clause
            if (model == null)
            {
                feedback.Message = "Other Running cost missing.";
                return BadRequest(feedback);
            }

            var client = await GetClientAsync();


            // Authorization Clause
            if (client == null)
                return BadRequest(feedback);



            model.ClientId = client.Id;


            // Is there a name conflict?

            var message = await UnitOfWork.IsUniqueAsync(model.GetCore());

            if (!string.IsNullOrWhiteSpace(message))
            {
                feedback.Message = "A record for this other running cost already exists.";
                return BadRequest(feedback);
            }


            var saveMe = false;

            var core = await _unitOfWork.BusinessOtherRunningCosts.GetAsync(model.Id);
            if (core == null)
            {
                // New record
                core = model.GetCore();
                core.Client = client;
                saveMe = true;
            }
            else
            {
                if (core.Name != model.Name)
                {
                    core.Name = model.Name;
                    saveMe = true;
                }

                if (core.CostTypeId != model.CostTypeId)
                {
                    core.CostTypeId = model.CostTypeId;
                    saveMe = true;
                }

                if (core.Description != model.Description)
                {
                    core.Description = model.Description;
                    saveMe = true;
                }

                if (core.FrequencyId != model.FrequencyId)
                {
                    core.FrequencyId = model.FrequencyId;
                    saveMe = true;
                }


                if (core.Cost != model.Cost)
                {
                    core.Cost = model.Cost;
                    saveMe = true;
                }

                if (core.IsLive != model.IsLive)
                {
                    core.IsLive = model.IsLive;
                    saveMe = true;
                }
            }


            // Save
            if (!saveMe)
                return Ok();



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
                feedback.Message = "Save Failed";
                return BadRequest(feedback);
            }
        }


        [HttpDelete("/api/Business/otherRunningCost/Delete/{id}")]
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
                var result = await _unitOfWork.BusinessOtherRunningCosts.RemoveAsync(id);

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
