using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SilkFlo.Web.APIControllers.Business
{
    public partial class IdeaController : Controllers.AbstractAPI
    {
        public IdeaController(Data.Core.IUnitOfWork unitOfWork,
                              Services.ViewToString viewToString,
                              IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }




        [HttpPost("/api/Business/Ideas/GetTotalProcessingTime")]
        public async Task<IActionResult> GetTotalProcessingTime([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            var value = model.GetTotalProcessingTime();

            return Ok(model.GetTotalProcessingTime());
        }

        [HttpPost("/api/Business/Ideas/GetTotalReworkTime")]
        public async Task<IActionResult> GetTotalReworkTime([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            return Ok(model.GetTotalReworkTime());
        }


        [HttpPost("/api/Business/Ideas/GetTotalReviewTime")]
        public async Task<IActionResult> GetTotalReviewTime([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            return Ok(model.GetTotalReviewTime());
        }


        [HttpPost("/api/Business/Ideas/GetTimeNeededToPerformWorkWithoutAutomation")]
        public async Task<IActionResult> GetTotalTimeNeededToPerformWorkWithoutAutomation([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            return Ok(model.GetTotalTimeNeededToPerformWorkWithoutAutomation());
        }



        [HttpPost("/api/Business/Ideas/GetFullTimeEquivalentsRequired")]
        public async Task<IActionResult> GetFullTimeEquivalentsRequired([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            return Ok(model.GetFullTimeEquivalentsRequired());
        }


        [HttpPost("/api/Business/Ideas/GetCostPerYear")]
        public async Task<IActionResult> GetCostPerYear([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            model.UnitOfWork = _unitOfWork;

            var value = model.GetCostPerYearForProcessBeforeAutomation();

            value = await model.GetEstimateAsync(value);

            return Ok(value);
        }


        [HttpPost("/api/Business/Ideas/GetFeasibilityScore")]
        public async Task<IActionResult> GetFeasibilityScore([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            model.UnitOfWork = _unitOfWork;

            return Ok((await model.GetFeasibilityScoreAsync()));
        }



        [HttpPost("/api/Business/Ideas/GetAutomationPotential")]
        public async Task<IActionResult> GetAutomationPotential([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            model.UnitOfWork = _unitOfWork;

            return Ok((await model.GetAutomationPotentialAsync()));
        }

        [HttpPost("/api/Business/Ideas/GetEaseOfImplementation")]
        public async Task<IActionResult> GetEaseOfImplementation([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            model.UnitOfWork = _unitOfWork;

            return Ok((await model.GetEaseOfImplementationAsync()));
        }

        [HttpPost("/api/Business/Ideas/GetPrimedScore")]
        public async Task<IActionResult> GetPrimedScore([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            model.UnitOfWork = _unitOfWork;

            return Ok((await model.GetPrimedScoreAsync()));
        }

        [HttpPost("/api/Business/Ideas/GetFitnessScore")]
        public async Task<IActionResult> GetFitnessScore([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            model.UnitOfWork = _unitOfWork;

            return Ok((await model.GetFitnessScoreAsync()));
        }


        [HttpPost("/api/Business/Ideas/GetIdeaScore")]
        public async Task<IActionResult> GetIdeaScore([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            model.UnitOfWork = _unitOfWork;

            return Ok((await model.GetIdeaScoreAsync()));
        }


        [HttpPost("/api/Business/Ideas/GetEstimatedBenefitPerCompany_Hours")]
        public async Task<IActionResult> GetEstimatedBenefitPerCompany_Hours([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            model.UnitOfWork = _unitOfWork;

            model.GetBenefitPerEmployee_Hours();
            var value = model.BenefitPerCompanyHoursValue;

            if (value == 0)
                return Ok(0);

            value = await model.GetEstimateAsync(value);


            return Ok(value ?? 0);
        }

        [HttpPost("/api/Business/Ideas/GetEstimatedBenefitPerCompany_Currency")]
        public async Task<IActionResult> GetEstimatedBenefitPerCompany_Currency([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            model.UnitOfWork = _unitOfWork;

            model.GetBenefitPerEmployee_Currency();

            var value = model.BenefitPerCompanyCurrencyValue;

            value = await model.GetEstimateAsync(value);


            return Ok(value);
        }

        [HttpPost("/api/Business/Ideas/GetEstimatedBenefitPerCompany_FTE")]
        public async Task<IActionResult> GetEstimatedBenefitPerCompany_FTE([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");
            model.UnitOfWork = _unitOfWork;
            await model.GetEstimatedBenefitPerCompanyFTE();
            var value = model.EstimatedBenefitPerCompanyFteValue;

            value = await model.GetEstimateAsync(value);

            return Ok(value);
        }



        [HttpPost("/api/Business/Ideas/GetEstimatedBenefitPerEmployee_Hours")]
        public async Task<IActionResult> GetBenefitPerEmployee_Hours([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            model.UnitOfWork = _unitOfWork;

            return Ok(await model.GetEstimateAsync(model.GetBenefitPerEmployee_Hours()));
        }

        [HttpPost("/api/Business/Ideas/GetEstimatedBenefitPerEmployee_Currency")]
        public async Task<IActionResult> GetBenefitPerEmployee_Currency([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            model.UnitOfWork = _unitOfWork;
            var value = model.GetBenefitPerEmployee_Currency();

            if (value == null || value == 0)
                return Ok(0);

            value = await model.GetEstimateAsync(value);

            return Ok(value);
        }

        [HttpPost("/api/Business/Ideas/GetBenefitPerEmployee_FTE")]
        public async Task<IActionResult> GetBenefitPerEmployee_FTE([FromBody] Models.Business.Idea model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            model.UnitOfWork = _unitOfWork;

            await model.GetEstimatedBenefitPerEmployeeFTE();

            return Ok(model.EstimatedBenefitPerEmployeeFteValue);
        }


        [HttpGet("/api/Business/Ideas/GetTotalFTR/ideaId/{ideaId}/AutomationTypeId/{AutomationTypeId}/EmployeeCount/{EmployeeCount}/RobotSpeedMultiplier/{robotSpeedMultiplier}")]
        public async Task<IActionResult> GetTotalFTR(string ideaId,
                                                     string automationTypeId,
                                                     int employeeCount,
                                                     decimal robotSpeedMultiplier = 0)
        {
            // Permission Clause
            if (!(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
                return NegativeFeedback();

            var core = await _unitOfWork.BusinessIdeas.GetAsync(ideaId);

            if (core == null)
                return NegativeFeedback();

            var model = new Models.Business.Idea(core);

            return Ok(model.GetTotalFTR(automationTypeId, employeeCount));
        }




        [HttpPost("/api/Business/Ideas/GetFeasibilityGaugeComponent")]
        public async Task<IActionResult> GetFeasibilityGaugeComponent([FromBody] ViewModels.Business.Idea.Gauge.ComponentGet model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            var ideaModel = new Models.Business.Idea
            {
                RuleId = model.RuleId,
                InputId = model.InputId,
                InputDataStructureId = model.InputDataStructureId,
                ProcessStabilityId = model.ProcessStabilityId,
                DocumentationPresentId = model.DocumentationPresentId,
                UnitOfWork = _unitOfWork
            };

            var score = await ideaModel.GetFitnessScoreAsync();

            var gauge = new ViewModels.Business.Idea.Gauge.Basic
            {
                Title = "Feasibility",
                Value = score ?? 0,
                GaugeGraphic = ideaModel.FitnessScoreGauge,
                HotSpotId = "Feasibility Gauge"
            };

            var html = await _viewToString.PartialAsync("Shared/Business/Idea/_Gauge.cshtml",
                                                     gauge);
            return Content(html);
        }


        [HttpPost("/api/Business/Ideas/GetReadinessGaugeComponent")]
        public async Task<IActionResult> GetReadinessGaugeComponent([FromBody] ViewModels.Business.Idea.Gauge.ComponentGet model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            var ideaModel = new Models.Business.Idea
            {
                RuleId = model.RuleId,
                InputId = model.InputId,
                InputDataStructureId = model.InputDataStructureId,
                ProcessStabilityId = model.ProcessStabilityId,
                DocumentationPresentId = model.DocumentationPresentId,
                UnitOfWork = _unitOfWork
            };

            var score = await ideaModel.GetPrimedScoreAsync();

            var gauge = new ViewModels.Business.Idea.Gauge.Basic
            {
                Title = "Readiness",
                Value = score ?? 0,
                GaugeGraphic = ideaModel.PrimedScoreGauge,
                HotSpotId = "Readiness Gauge"
            };

            string html = await _viewToString.PartialAsync("Shared/Business/Idea/_Gauge.cshtml",
                                                     gauge);
            return Content(html);
        }


        [HttpPost("/api/Business/Ideas/GetIdeaGaugeComponent")]
        public async Task<IActionResult> GetIdeaGaugeComponent([FromBody] ViewModels.Business.Idea.Gauge.ComponentGet model)
        {
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback();

            if (model == null)
                return NegativeFeedback("Error: The model is missing");

            var ideaModel = new Models.Business.Idea
            {
                RuleId = model.RuleId,
                InputId = model.InputId,
                InputDataStructureId = model.InputDataStructureId,
                ProcessStabilityId = model.ProcessStabilityId,
                DocumentationPresentId = model.DocumentationPresentId,
                UnitOfWork = _unitOfWork
            };

            var score = await ideaModel.GetIdeaScoreModalAsync();

            var gauge = new ViewModels.Business.Idea.Gauge.Basic
            {
                Title = "Idea Score",
                Value = score ?? 0,
                GaugeGraphic = ideaModel.IdeaScoreGauge,
                HotSpotId = "Idea Score Gauge"
            };

            var html = await _viewToString.PartialAsync("Shared/Business/Idea/_Gauge.cshtml",
                                                        gauge);
            return Content(html);
        }
    }
}