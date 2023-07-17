using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SilkFlo.Web.ViewModels;
using SilkFlo.Security.API.ReCaptcha.Interfaces;

namespace SilkFlo.Web.Controllers.CRM
{
    public class ProspectController : AbstractAPI
    {
        private readonly ISignUpService _signUpService;

        public ProspectController(Data.Core.IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization,
            ISignUpService signUpService) : base(unitOfWork, viewToString, authorization)
        {
            _signUpService = signUpService;
        }


        [HttpPost("/api/CRM/Prospect/Save")]
        public async Task<IActionResult> Save([FromBody] Models.CRM.Prospect model)
        {
            var feedback = new Feedback();

            if (model == null)
            {
                feedback.DangerMessage("No content supplied");
                return BadRequest(feedback);
            }

            model.ClientTypeId = Data.Core.Enumerators.ClientType.Client39.ToString();
            ModelState.Clear();
            TryValidateModel(model);
            feedback = GetFeedback(ModelState);

            if (!model.TermsAgreed)
            {
                feedback.IsValid = false;
                feedback.Elements.Add("TermsAgreed", "Please confirm the privacy policy.");
            }



            if(!feedback.IsValid)
               return BadRequest(feedback);

            if (string.IsNullOrWhiteSpace(feedback.Message))
                feedback.InfoMessage("Once Completed, you will be redirected to our calendly.com page.");

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                var message = await Email.Service.ValidateEmailAsync(model.Email, false);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    feedback.IsValid = false;
                    feedback.Elements.Add("Email", message);
                    return BadRequest(feedback);
                }
            }

            var response = await _signUpService.SignUp(model.ReCaptchaToken);

            if (!response.Success)
            {
                feedback.IsValid = false;
                feedback.DangerMessage( response.Error );

                return BadRequest(feedback);
            }



            await _unitOfWork.AddAsync(model.GetCore());
            await _unitOfWork.CompleteAsync();



            feedback.Elements = null;
            feedback.Message =
                "<span class=\"text-info\">Thank you.</br>" + 
                "You will shortly be redirected to calendly.com to book an appointment.</span";
            return Ok(feedback);
        }
    }
}
