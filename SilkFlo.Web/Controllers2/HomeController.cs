using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Web.ViewModels;

namespace SilkFlo.Web.Controllers
{
    public partial class HomeController
    {

        private IActionResult IndexView(bool returnStringContent)
        {
            return Redirect(returnStringContent ? 
                "./api/Dashboard" :
                "./Dashboard");
        }

        [Route("/api/Explore/Leaderboard")]
        [Route("/Explore/Leaderboard")]
        [Route("/api/Explore/Component")]
        [Route("/Explore/Component")]
        [Authorize]
        public async Task<IActionResult> UnderConstruction()
        {
            const string url = "/Views/Home/UnderConstruction.cshtml";
            if (Request.Path.ToString()
                    .ToLower()
                    .IndexOf("/api",
                        StringComparison.Ordinal) != 0)
                return View(url,
                    "");


            var html = await _viewToString.PartialAsync(url,
                "");
            return Content(html);
        }


        [HttpGet("/Application/Settings")]
        [HttpGet("/api/Application/Settings")]
        public async Task<IActionResult> GetSetting()
        {
            if (!(await AuthorizeAsync(Policy.Administrator)).Succeeded)
                return NegativeFeedback();


            // Check Authorisation
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
            {
                if (Request.Path.ToString().IndexOf("/api", StringComparison.InvariantCultureIgnoreCase) == 0)
                    return  NegativeFeedback();
                
                return Redirect("/account/signin");
            }



            var model = await GetApplicationSettingsAsync();


            return await ViewOrContent(
                "/Views/Application/Settings.cshtml",
                model);
        }


        [HttpPost("/api/Application/Settings/Post")]
        public async Task<IActionResult> PostSetting([FromBody] ViewModels.Application.Setting viewModel)
        {
            var feedback = new Feedback();

            if (!(await AuthorizeAsync(Policy.Administrator)).Succeeded)
            {
                feedback.Message = "Unauthorised";
                return BadRequest(feedback);
            }


            if (viewModel.PracticeAccountCanSignIn)
            {
                if (string.IsNullOrWhiteSpace(viewModel.PracticeAccountPassword))
                {
                    feedback.Elements.Add(
                        "PracticeAccountPassword",
                        "Password required if can sign in is true.");

                    feedback.IsValid = false;
                }
                else
                {
                    if (viewModel.PracticeAccountPassword.Length > 100)
                    {
                        feedback.Elements.Add(
                            "PracticeAccountPassword", 
                            "Practice Account Password must be between 1 and 100 characters in length.");

                        feedback.IsValid = false;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(viewModel.TestEmailAccount))
            {
                if (viewModel.TestEmailAccount.Length > 100)
                {
                    feedback.Elements.Add(
                        "TestEmailAccount", 
                        "Email address must be less then 100 characters in length.");

                    feedback.IsValid = false;
                }
                else if (!Email.Service.IsEmailFormatValid(viewModel.TestEmailAccount))
                {
                    feedback.Elements.Add(
                        "TestEmailAccount",
                        "Email address is not in a valid format.");

                    feedback.IsValid = false;
                }
            }

            if (viewModel.TrialPeriod < 0)
            {
                feedback.Elements.Add(
                    "TrialPeriod",
                    "Trial period must be greater than 0.");

                feedback.IsValid = false;
            }

            if (!feedback.IsValid)
                return BadRequest(feedback);



            Email.Service.TestEmailAddress = viewModel.TestEmailAccount;
            await SaveApplicationSettingsAsync(viewModel);

            // Stay awesome!
             return Ok();
        }


        [HttpGet("/Application/Prospects")]
        [HttpGet("/api/Application/Prospects")]
        public async Task<IActionResult> GetProspects()
        {
            if (!(await AuthorizeAsync(Policy.Administrator)).Succeeded)
                return NegativeFeedback();

            var prospects = (await _unitOfWork.CRMProspects.GetAllAsync()).ToList();
            await _unitOfWork.SharedCountries.GetCountryForAsync(prospects);
            await _unitOfWork.CRMCompanySizes.GetCompanySizeForAsync(prospects);
            await _unitOfWork.CRMJobLevels.GetJobLevelForAsync(prospects);
            await _unitOfWork.SharedClientTypes.GetClientTypeForAsync(prospects);

            var models = Models.CRM.Prospect.Create(prospects);

            return await ViewOrContent("/Views/Shared/CRM/Prospect/_Table.cshtml", models);
        }
    }
}