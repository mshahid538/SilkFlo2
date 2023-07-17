using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Models.Business
{
    public partial class Idea
    {
        private string _currency;
        public string Currency
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_currency))
                {
                    _currency = Client?.Currency?.Symbol;
                    if (!string.IsNullOrWhiteSpace(_currency))
                        _currency += "&nbsp;";
                }

                return _currency;
            }
        }

        public List<Application> Applications { get; set; } = new List<Application>();
        public List<Shared.Language> Languages { get; set; } = new List<Shared.Language>();

        public decimal GetTotalProcessingTime()
        {
            if (AverageProcessingTime == null
            || ActivityVolumeAverage == null)
                return 0;

            // Get the per hour total processing time.
            var hours = (decimal)AverageProcessingTime / 60 * (decimal)ActivityVolumeAverage;

            var hoursPerYear = GetTotalHoursPerYear(hours);

            return hoursPerYear;
        }


        public string TotalProcessingTime
        {
            get
            {
                decimal n = GetTotalProcessingTime();

                return n.ToString("#,##0.00");
            }
        }

        public decimal GetTotalReworkTime()
        {
            if (ActivityVolumeAverage == null
            || AverageErrorRate == null
            || AverageReworkTime == null)
                return 0;



            // Calculation
            // Session error number = Session tasks / 100 * Average Error Rate
            var sessionErrorNumber = (decimal)ActivityVolumeAverage / 100 * (decimal)AverageErrorRate;

            // Session Rework Time = Session error number * Average Rework Time
            var sessionReworkTime = sessionErrorNumber * (decimal)AverageReworkTime / 60;

            // Get the total processing time per year.
            var totalHoursPerYear = this.GetTotalHoursPerYear(sessionReworkTime);

            return totalHoursPerYear;
        }
        public string TotalReworkTime => GetTotalReworkTime().ToString("#,##0.00");

        public decimal GetTotalReviewTime()
        { 
            if (ActivityVolumeAverage == null
            || AverageWorkToBeReviewed == null
            || AverageReviewTime == null)
                return 0;

            // Calculations

            // How many Session tasks will be reviewed based on the provided percentage (Average Work to be Reviewed/Audited)?
            // Session tasks for Review = Session tasks / 100 * Average Work to be Reviewed
            var sessionTasksForReview = (decimal)ActivityVolumeAverage / 100 * (decimal)AverageWorkToBeReviewed;

            // How long will the review take?
            // Session tasks for review time = Session tasks for Review * Average Review Time / 60
            var sessionTasksForReviewTime = sessionTasksForReview * (decimal)AverageReviewTime / 60;


            // Total Review Time per year = GetTotalPerYear(Session tasks for review time)
            var totalHoursPerYear = GetTotalHoursPerYear(sessionTasksForReviewTime);

            return totalHoursPerYear;
        }
        public string TotalReviewTime => GetTotalReviewTime().ToString("#,##0.00");


        public decimal GetTotalTimeNeededToPerformWorkWithoutAutomation()
        {
            var processingTime = GetTotalProcessingTime();
            var reworkTime = GetTotalReworkTime();
            var reviewTime = GetTotalReviewTime();

            var total = processingTime
                        + reworkTime
                        + reviewTime;


            TotalTimeNeededToPerformWorkWithoutAutomationNumber = Math.Round(total, 2);
            _totalTimeNeededToPerformWorkWithoutAutomation = total.ToString("#,##0.00");
            return total;
        }

        private string _totalTimeNeededToPerformWorkWithoutAutomation;
        public string TotalTimeNeededToPerformWorkWithoutAutomation
        {
            get
            {
                if(string.IsNullOrEmpty(_totalTimeNeededToPerformWorkWithoutAutomation))
                    GetTotalTimeNeededToPerformWorkWithoutAutomation();

                return _totalTimeNeededToPerformWorkWithoutAutomation;
            }
        }

        public decimal TotalTimeNeededToPerformWorkWithoutAutomationNumber { get; private set; }




        public decimal? GetFullTimeEquivalentsRequired()
        {
            var timeNeededToPerformWorkWithoutAutomation = GetTotalTimeNeededToPerformWorkWithoutAutomation();

            if (AverageWorkingDay == null
            || WorkingHour == null)
                return null;


            var totalHours = (decimal)AverageWorkingDay * (decimal)WorkingHour;

            if (totalHours == 0)
                return null;



            var value = timeNeededToPerformWorkWithoutAutomation
                        / totalHours;

            return value;
        }




        public string FullTimeEquivalentsRequired
        {
            get
            {
                decimal n = GetFullTimeEquivalentsRequired() ?? 0;

                return n.ToString("#,##0.00");
            }
        }


        public decimal GetCostPerYearForProcessBeforeAutomation()
        {
            var totalTimeNeededToPerformWorkWithoutAutomation = GetTotalTimeNeededToPerformWorkWithoutAutomation();
            
            if (EmployeeCount == null
                || AverageWorkingDay == null
                || AverageEmployeeFullCost == null
                || WorkingHour == null
                || AverageWorkingDay <= 0
                || WorkingHour <=0)
                return 0;

            var hourlyRate = (decimal)AverageEmployeeFullCost
                / (decimal)AverageWorkingDay
                / WorkingHour;

            var value = hourlyRate * totalTimeNeededToPerformWorkWithoutAutomation;

            //var automationPotential = (await GetAutomationPotentialAsync()) ?? 0;

            //value = value / 100 * automationPotential;

            value = Math.Round((decimal)value, 0);


            return (decimal) value;
        }
        public  string CostPerYearForProcessBeforeAutomation()
        {
            var value = GetCostPerYearForProcessBeforeAutomation();

            return value.ToString("#,##0.00");
        }


        public async Task<decimal> GetAutomationPotentialAsync()
        {
            // Guard Clauses
            if (string.IsNullOrWhiteSpace(DataInputPercentOfStructuredId)) return 0;
            if (string.IsNullOrWhiteSpace(InputId)) return 0;
            if (string.IsNullOrWhiteSpace(InputDataStructureId)) return 0;
            if (string.IsNullOrWhiteSpace(NumberOfWaysToCompleteProcessId)) return 0;
            if (string.IsNullOrWhiteSpace(RuleId)) return 0;


            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            // 1. Get DataInputPercentOfStructured Date
            var dataInputPercentOfStructured = await UnitOfWork.SharedDataInputPercentOfStructureds
                                                         .GetAsync(DataInputPercentOfStructuredId);
            // Guard Clause
            if (dataInputPercentOfStructured == null) return 0;




            // 2. Get Input Data
            var input = await UnitOfWork.SharedInputs
                                  .GetAsync(InputId);
            // Guard Clause
            if (input == null) return 0;




            // 3. Get InputDataStructure Data
            var inputDataStructure = await UnitOfWork.SharedInputDataStructures
                                                     .GetAsync(InputDataStructureId);
            // Guard Clause
            if (inputDataStructure == null) return 0;




            // 4.  Get NumberOfWaysToCompleteProcess Date
            var numberOfWaysToCompleteProcess = await UnitOfWork.SharedNumberOfWaysToCompleteProcesses
                                                          .GetAsync(NumberOfWaysToCompleteProcessId);
            // Guard Clause
            if (numberOfWaysToCompleteProcess == null) return 0;




            // 6.  Get Rule Data
            var rule = await UnitOfWork.SharedRules
                                 .GetAsync(RuleId);
            // Guard Clause
            if (rule == null)
                return 0;



            var value = (dataInputPercentOfStructured.Weighting +
                         input.Weighting +
                         inputDataStructure.Weighting +
                         numberOfWaysToCompleteProcess.Weighting + 
                         rule.Weighting) / 5 * 100;

            AutomationPotential = value.ToString("#,##0");
            AutomationPotentialValue = value;
            return value;
        }
        public string AutomationPotential { get; private set; }
        public decimal? AutomationPotentialValue { get; private set; }

        public async Task<decimal> GetEstimateAsync(decimal? value)
        {
            if(value == null)
                return 0;
            
            var automationPotential = (await GetAutomationPotentialAsync());


            return (decimal)value / 100 * automationPotential;
        }

        public async Task<decimal?> GetEaseOfImplementationAsync()
        {
            var dataFound = !(IdeaApplicationVersions.Count == 0 
                              || string.IsNullOrWhiteSpace(ApplicationStabilityId)
                              || string.IsNullOrWhiteSpace(AverageNumberOfStepId)
                              || string.IsNullOrWhiteSpace(DataInputPercentOfStructuredId)
                              || string.IsNullOrWhiteSpace(DocumentationPresentId)
                              || string.IsNullOrWhiteSpace(InputDataStructureId)
                              || string.IsNullOrWhiteSpace(NumberOfWaysToCompleteProcessId)
                              || string.IsNullOrWhiteSpace(ProcessPeakId)
                              || string.IsNullOrWhiteSpace(ProcessStabilityId)
                              || string.IsNullOrWhiteSpace(DecisionDifficultyId)
                              || string.IsNullOrWhiteSpace(DecisionCountId));

            // Guard Clause


            decimal? value = null;
            if (dataFound)
            {
                // Guard Clause
                if (UnitOfWork == null)
                    throw new NullReferenceException("UnitOfWork missing");


                var applicationStability =
                    await UnitOfWork.SharedApplicationStabilities.GetAsync(ApplicationStabilityId);
                if (applicationStability == null) return null;


                var averageNumberOfStep = await UnitOfWork.SharedAverageNumberOfSteps.GetAsync(AverageNumberOfStepId);
                if (averageNumberOfStep == null) return null;


                var dataInputPercentOfStructured =
                    await UnitOfWork.SharedDataInputPercentOfStructureds.GetAsync(DataInputPercentOfStructuredId);
                if (dataInputPercentOfStructured == null) return null;


                var documentationPresent =
                    await UnitOfWork.SharedDocumentationPresents.GetAsync(DocumentationPresentId);
                if (documentationPresent == null) return null;

                var inputDataStructure = await UnitOfWork.SharedInputDataStructures.GetAsync(InputDataStructureId);
                if (inputDataStructure == null) return null;

                var numberOfWaysToCompleteProcess =
                    await UnitOfWork.SharedNumberOfWaysToCompleteProcesses.GetAsync(NumberOfWaysToCompleteProcessId);
                if (numberOfWaysToCompleteProcess == null) return null;

                var processPeak = await UnitOfWork.SharedProcessPeaks.GetAsync(ProcessPeakId);
                if (processPeak == null) return null;

                var processStability = await UnitOfWork.SharedProcessStabilities.GetAsync(ProcessStabilityId);
                if (processStability == null) return null;



                var decisionDifficulty = await UnitOfWork.SharedDecisionDifficulties.GetAsync(DecisionDifficultyId);
                if (decisionDifficulty == null) return null;


                var decisionCount = await UnitOfWork.SharedDecisionCounts.GetAsync(DecisionCountId);
                if (decisionCount == null) return null;


                var applicationCountWeighting = GetApplicationCountWeighting();
                var thinClientApplicationCountWeighting = GetThinClientApplicationCountWeighting();


                decimal isAlternativeWeighting = 0;
                if (!IsAlternative)
                    isAlternativeWeighting = 1;


                decimal isDataInputScannedWeighting = 0;
                if (!IsDataInputScanned)
                    isDataInputScannedWeighting = 1;


                decimal isDataSensitiveWeighting = 0;
                if (!IsDataSensitive)
                    isDataSensitiveWeighting = 1;


                decimal isHighRiskWeighting = 0;
                if (!IsHighRisk)
                    isHighRiskWeighting = 1;


                decimal isHostUpgradeWeighting = 0;
                if (!IsHostUpgrade)
                    isHostUpgradeWeighting = 1;


                value = (
                    applicationCountWeighting +
                    thinClientApplicationCountWeighting +
                    applicationStability.Weighting +
                    averageNumberOfStep.Weighting +
                    dataInputPercentOfStructured.Weighting +
                    decisionCount.Weighting +
                    decisionDifficulty.Weighting +
                    documentationPresent.Weighting +
                    inputDataStructure.Weighting +
                    numberOfWaysToCompleteProcess.Weighting +
                    processPeak.Weighting +
                    processStability.Weighting +
                    isAlternativeWeighting +
                    isDataInputScannedWeighting +
                    isDataSensitiveWeighting +
                    isHighRiskWeighting +
                    isHostUpgradeWeighting) / 17 * 100;
            }

            EaseOfImplementationValue = value ??0;
            EaseOfImplementation = EaseOfImplementationValue.ToString("#,##0.00");

            EaseOfImplementationWord = EaseOfImplementationValue switch
            {
                >= 65 => "Easy",
                <= 35 => "Difficult",
                _ => "Medium"
            };

            return value;
        }

        public string GetColour(decimal value)
        {
            return value switch
            {
                >= 65 => "var(--bs-success)",
                <= 35 => "var(--bs-danger)",
                _ => "var(--bs-warning-lighter)"
            };
        }

        public string EaseOfImplementation { get; private set; }
        public decimal EaseOfImplementationValue { get; private set; }

        public string EaseOfImplementationWord { get; private set; }

        public string EaseOfImplementationColour => GetColour(EaseOfImplementationValue);


        public async Task<decimal?> GetFeasibilityScoreAsync()
        {
            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            if (ApplicationStabilityId == null) return null;
            if (InputDataStructureId == null) return null;
            if (ProcessStabilityId == null) return null;



            var applicationStability = await UnitOfWork.SharedApplicationStabilities.GetAsync(ApplicationStabilityId);
            if (applicationStability == null) return null;

            var inputDataStructure = await UnitOfWork.SharedInputDataStructures.GetAsync(InputDataStructureId);
            if (inputDataStructure == null) return null;

            var processStability = await UnitOfWork.SharedProcessStabilities.GetAsync(ProcessStabilityId);
            if (processStability == null) return null;





            var value = (applicationStability.Weighting +
                         inputDataStructure.Weighting +
                         processStability.Weighting) / 3 * 100;


            FeasibilityScore = value.ToString("#,##0.00");
            FeasibilityScoreValue = value;
            return value;
        }
        public decimal FeasibilityScoreValue { get; set; }
        public string FeasibilityScore { get; set; }

        //public string FeasibilityScoreGauge
        //{
        //    get
        //    {
        //        var gauge = new ViewModels.Gauge.Ring((double)FeasibilityScoreValue);
        //        return gauge.SVG;
        //    }
        //}

        public async Task<decimal?> GetPrimedScoreAsync()
        {
            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            if (DocumentationPresentId == null) return null;
            if (ProcessStabilityId == null) return null;

            var documentationPresent = await UnitOfWork.SharedDocumentationPresents.GetAsync(DocumentationPresentId);
            if (documentationPresent == null) return null;

            var processStability = await UnitOfWork.SharedProcessStabilities.GetAsync(ProcessStabilityId);
            if (processStability == null) return null;

            decimal value = (documentationPresent.Weighting + processStability.Weighting) / 2 * 100;

            PrimedScore = value.ToString("#,##0.00");


            PrimedScoreValue = value;

            return value;
        }
        public string PrimedScore { get; set; }
        public decimal PrimedScoreValue { get; set; }
        public string PrimedScoreGauge
        {
            get
            {
                var gauge = new ViewModels.Gauge.Ring(PrimedScoreValue);
                return gauge.SVG;
            }
        }

        //public async Task<decimal?> GetFitnessScoreAsync(Data.Core.IUnitOfWork unitOfWork)
        //{
        //    if (string.IsNullOrWhiteSpace(DataInputPercentOfStructuredId)) return null;
        //    if (string.IsNullOrWhiteSpace(InputId)) return null;
        //    if (string.IsNullOrWhiteSpace(InputDataStructureId)) return null;
        //    if (string.IsNullOrWhiteSpace(ProcessPeakId)) return null;
        //    if (string.IsNullOrWhiteSpace(RuleId)) return null;

        //    var dataInputPercentOfStructured = await unitOfWork.SharedDataInputPercentOfStructureds.GetAsync(DataInputPercentOfStructuredId);
        //    if (dataInputPercentOfStructured == null) return null;

        //    var input = await unitOfWork.SharedInputs.GetAsync(InputId);
        //    if (input == null) return null;

        //    var inputDataStructure = await unitOfWork.SharedInputDataStructures.GetAsync(InputDataStructureId);
        //    if (inputDataStructure == null) return null;

        //    decimal isDataInputScanned_Weighting = 1;
        //    if (IsDataInputScanned)
        //        isDataInputScanned_Weighting = 0;

        //    var processPeak = await unitOfWork.SharedProcessPeaks.GetAsync(ProcessPeakId);
        //    if (processPeak == null) return null;

        //    var rule = await unitOfWork.SharedRules.GetAsync(RuleId);
        //    if (rule == null) return null;

        //    decimal value = (dataInputPercentOfStructured.Weighting +
        //        input.Weighting +
        //        inputDataStructure.Weighting +
        //        isDataInputScanned_Weighting +
        //        processPeak.Weighting) / 5 * 100;

        //    FitnessScore = (value / 100).ToString("#,##0.00");


        //    FitnessScoreValue = value;
        //    return value;
        //}

        public async Task<decimal?> GetFitnessScoreAsync()
        {
            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            if (string.IsNullOrWhiteSpace(RuleId)) return null;
            if (string.IsNullOrWhiteSpace(InputId)) return null;
            if (string.IsNullOrWhiteSpace(InputDataStructureId)) return null;


            var rule = await UnitOfWork.SharedRules.GetAsync(RuleId);
            if (rule == null) return null;

            var input = await UnitOfWork.SharedInputs.GetAsync(InputId);
            if (input == null) return null;

            var inputDataStructure = await UnitOfWork.SharedInputDataStructures.GetAsync(InputDataStructureId);
            if (inputDataStructure == null) return null;



            decimal value = (rule.Weighting +
                             input.Weighting +
                             inputDataStructure.Weighting) / 3 * 100;

            FitnessScore = value.ToString("#,##0.00");


            FitnessScoreValue = value;
            return value;
        }


        public string FitnessScore { get; set; }
        public decimal FitnessScoreValue { get; set; }
        public string FitnessScoreGauge
        {
            get
            {
                var gauge = new ViewModels.Gauge.Ring(FitnessScoreValue);
                return gauge.SVG;
            }
        }

        public async Task<decimal?> GetIdeaScoreAsync()
        {
            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            IdeaScoreValue = (decimal)(Convert.ToDecimal(PrimedScore) + Convert.ToDecimal(FitnessScore)) / 2;
            IdeaScore = IdeaScoreValue.ToString("#,##0.00");

            if (string.IsNullOrWhiteSpace(ApplicationStabilityId)) return null;
            if (string.IsNullOrWhiteSpace(DataInputPercentOfStructuredId)) return null;
            if (string.IsNullOrWhiteSpace(DocumentationPresentId)) return null;
            if (string.IsNullOrWhiteSpace(InputId)) return null;
            if (string.IsNullOrWhiteSpace(InputDataStructureId)) return null;
            if (string.IsNullOrWhiteSpace(ProcessStabilityId)) return null;
            if (string.IsNullOrWhiteSpace(ProcessPeakId)) return null;
            if (string.IsNullOrWhiteSpace(RuleId)) return null;

            var applicationStability = await UnitOfWork.SharedApplicationStabilities.GetAsync(ApplicationStabilityId);
            if (applicationStability == null) return null;

            var dataInputPercentOfStructured = await UnitOfWork.SharedDataInputPercentOfStructureds.GetAsync(DataInputPercentOfStructuredId);
            if (dataInputPercentOfStructured == null) return null;

            var documentationPresent = await UnitOfWork.SharedDocumentationPresents.GetAsync(DocumentationPresentId);
            if (documentationPresent == null) return null;

            var input = await UnitOfWork.SharedInputs.GetAsync(InputId);
            if (input == null) return null;

            var inputDataStructure = await UnitOfWork.SharedInputDataStructures.GetAsync(InputDataStructureId);
            if (inputDataStructure == null) return null;

            var processStability = await UnitOfWork.SharedProcessStabilities.GetAsync(ProcessStabilityId);
            if (processStability == null) return null;

            var processPeak = await UnitOfWork.SharedProcessPeaks.GetAsync(ProcessPeakId);
            if (processPeak == null) return null;

            var rule = await UnitOfWork.SharedRules.GetAsync(RuleId);
            if (rule == null) return null;

            decimal? value = (applicationStability.Weighting +
                              dataInputPercentOfStructured.Weighting +
                              documentationPresent.Weighting +
                              input.Weighting +
                              inputDataStructure.Weighting +
                              processStability.Weighting +
                              processPeak.Weighting +
                              rule.Weighting) / 8 * 100;

            //IdeaScoreValue = (decimal)value;           

            BenefitScore = value switch
            {
                < 25 => "Low",
                >= 25 and < 50 => "Medium",
                >= 50 and < 75 => "High",
                _ => "Very High"
            };
            
            return value;
        }

        public async Task<decimal?> GetIdeaScoreModalAsync()
        {
            var fitnessScore = await GetFitnessScoreAsync();
            var primedScore = await GetPrimedScoreAsync();
            var value = (fitnessScore + primedScore) / 2;

            IdeaScoreValue = value ?? 0;
            IdeaScore = IdeaScoreValue.ToString("#,##0");

            return value;
        }

        public string IdeaScore { get; set; }

        public decimal IdeaScoreValue { get; set; }

        public string IdeaScoreColour => GetColour(IdeaScoreValue);

        public string IdeaScoreWord
        {
            get
            {
                return IdeaScoreValue switch
                {
                    >= 65 => "Good",
                    <= 35 => "Poor",
                    _ => "Average"
                };
            }
        }


        public string FitnessAndPrimedScoreColour => GetColour((PrimedScoreValue + FitnessScoreValue)/2);

        public string FitnessPrimedAndIdScoreColour => GetColour((PrimedScoreValue + FitnessScoreValue + IdeaScoreValue)/3);


        public string IdeaScoreGauge
        {
            get
            {
                var gauge = new ViewModels.Gauge.Ring(IdeaScoreValue);
                return gauge.SVG;
            }
        }

        public string BenefitScore { get; set; }

        public decimal GetBenefitPerEmployee_Hours()
        {
            var timeNeededToPerformWorkWithoutAutomation = GetTotalTimeNeededToPerformWorkWithoutAutomation();
            if (timeNeededToPerformWorkWithoutAutomation <= 0
                || EmployeeCount is null or <= 0)
                return 0;

            var value = timeNeededToPerformWorkWithoutAutomation/(decimal)EmployeeCount;


            BenefitPerEmployeeHoursValue = value;
            BenefitPerCompanyHoursValue = timeNeededToPerformWorkWithoutAutomation;

            return value;
        }
        public decimal BenefitPerEmployeeHoursValue { get; set; }
        public string BenefitPerEmployeeHours => BenefitPerEmployeeHoursValue.ToString("#,##0.00");

        public decimal? GetBenefitPerCompany_Hours()
        {
            var timeNeededToPerformWorkWithoutAutomation = GetTotalTimeNeededToPerformWorkWithoutAutomation();
            if (timeNeededToPerformWorkWithoutAutomation <= 0
                || EmployeeCount is null or <= 0)
                return 0;

                     BenefitPerCompanyHoursValue = Convert.ToDecimal(timeNeededToPerformWorkWithoutAutomation);

            return BenefitPerCompanyHoursValue;
        }
        public decimal? BenefitPerCompanyHoursValue { get; set; }
        public string BenefitPerCompanyHours => (BenefitPerCompanyHoursValue ?? 0).ToString("#,##0.00");


        public decimal? GetBenefitPerEmployee_Currency()
        {
            if (EmployeeCount is null or 0)
                return 0;


            var costPerYear = GetCostPerYearForProcessBeforeAutomation();


            var value = costPerYear / (decimal)EmployeeCount;

            BenefitPerEmployeeCurrencyValue = value;
            BenefitPerCompanyCurrencyValue = costPerYear;

            return value;
        }
        public decimal? GetBenefitPerCompany_Currency()
        {
            if (EmployeeCount is null or 0)
                return 0;


            var costPerYear = GetCostPerYearForProcessBeforeAutomation();

            var value = costPerYear / (decimal)EmployeeCount;

            BenefitPerEmployeeCurrencyValue = value;
            BenefitPerCompanyCurrencyValue = costPerYear;

            return BenefitPerCompanyCurrencyValue;
        }

        public async Task<decimal?> GetBenefitPerCompanyCurrencyForWorkshop()
        {
            if (EmployeeCount is null or 0)
                return 0;

            var costPerYear = GetCostPerYearForProcessBeforeAutomation();

            BenefitPerCompanyCurrencyValue = costPerYear;

            return await GetEstimateAsync(BenefitPerCompanyCurrencyValue);
        }
        public decimal BenefitPerEmployeeCurrencyValue { get; set; }
        public string BenefitPerEmployeeCurrency => BenefitPerEmployeeCurrencyValue.ToString("#,##0.00");
        public decimal BenefitPerCompanyCurrencyValue { get; set; }
        public string BenefitPerCompanyCurrency => BenefitPerCompanyCurrencyValue.ToString("#,##0.00");

        public decimal? GetFTE(decimal? automationPotential = 100)
        {
            // Guard Clause
            if (AverageWorkingDay is null or 0)
                return null;

            // Guard Clause
            if (WorkingHour is null or 0)
                return null;

            if (automationPotential == null || automationPotential < 0)
                automationPotential = 0;

            if (automationPotential > 100)
                automationPotential = 100;

            var totalTimeNeededToPerformWorkWithoutAutomation = GetTotalTimeNeededToPerformWorkWithoutAutomation();

            var value = totalTimeNeededToPerformWorkWithoutAutomation / AverageWorkingDay / WorkingHour;

            if (automationPotential != 100)
                value = value / 100 * automationPotential;

            return value;
        }

        public async Task<decimal?> GetEstimatedBenefitPerCompanyFTE()
        {
            var FTE = GetFTE();

            // Guard Clause
            if (FTE == null)
                return null;

            var automationPotential = await GetAutomationPotentialAsync();

            EstimatedBenefitPerCompanyFteValue = FTE /100 * automationPotential;

            return EstimatedBenefitPerCompanyFteValue;
        }


        public async Task<decimal?> GetEstimatedBenefitPerEmployeeFTE()
        {
            // Guard Clause
            if (EmployeeCount == null)
                return null;

            var fteEstimatedBenefitPerCompany = await GetEstimatedBenefitPerCompanyFTE();

            EstimatedBenefitPerEmployeeFteValue = fteEstimatedBenefitPerCompany / EmployeeCount;
            return EstimatedBenefitPerEmployeeFteValue;
        }

        public async Task CalculateAllEstimatedFTE()
        {
            await GetEstimatedBenefitPerEmployeeFTE();
        }

        public decimal? EstimatedBenefitPerEmployeeFteValue { get; set; }

        public string BenefitPerEmployeeFte => EstimatedBenefitPerEmployeeFteValue?.ToString("#,##0.00");
        public decimal? EstimatedBenefitPerCompanyFteValue { get; set; }
        public string EstimatedBenefitPerCompanyFte => EstimatedBenefitPerCompanyFteValue?.ToString("#,##0.00");

        private decimal? GetApplicationCountWeighting()
        {
            var count = IdeaApplicationVersions.Count;
            var weighting = count switch
            {
                4 => 0.25m,
                3 => 0.5m,
                2 => 0.75m,
                1 => 1m,
                _ => 0m
            };

            return weighting;
        }

        private decimal? GetThinClientApplicationCountWeighting()
        {
            var count = IdeaApplicationVersions.Count(x => x.IsThinClient);

            var weighting = count switch
            {
                4 => 0m,
                3 => 0.25m,
                2 => 0.5m,
                1 => 0.75m,
                _ => 1m
            };

            return weighting;
        }

        private decimal GetTotalHoursPerYear(decimal hours)
        {
            if (hours == 0)
                return 0;

            if (string.IsNullOrWhiteSpace(TaskFrequencyId))
                return 9;

            if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.BiAnnually.ToString())
            {
                // Bi-annually
                return hours * 2;
            }

            if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.Quarterly.ToString())
            {
                // Quarterly
                return hours * 4;
            }

            if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.Monthly.ToString())
            {
                // Monthly
                return hours * 12;
            }

            if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.Weekly.ToString())
            {
                // Weekly
                return hours * 52;
            }

            if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.Daily.ToString())
            {
                // Daily
                if (AverageWorkingDay == null)
                    return 0;

                return hours * (decimal)AverageWorkingDay;
            }

            return hours;
        }

        public async Task GetSummaryView_MetaDataAsync(Data.Core.Domain.Shop.Product product)
        {
            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            var core = GetCore();
            await UnitOfWork.BusinessDepartments.GetDepartmentForAsync(core);
            await UnitOfWork.BusinessTeams.GetTeamForAsync(core);
            await UnitOfWork.SharedAutomationGoals.GetAutomationGoalForAsync(core);
            await UnitOfWork.SharedStages.GetStageForAsync(core.IdeaStages);

            await GetIdeaScoreAsync();
        }


        public decimal? GetAverageHandlingTimeEmployeePerTransaction()
        {
            if(AverageWorkingDay is null or 0)
                return null;

            decimal activityVolumeAverage = ActivityVolumeAverage ?? 0;

            // Annually
            var processPerDay = activityVolumeAverage / (decimal)AverageWorkingDay;

            // Bi-Annually
            if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.BiAnnually.ToString())
                processPerDay *= 2;
            // Quarterly
            else if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.Quarterly.ToString())
                processPerDay *= 4;
            // Monthly
            else if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.Monthly.ToString())
                processPerDay *= 12;
            // Weekly
            else if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.Weekly.ToString())
                processPerDay *= 52;

            var totalProcessesPerYear = AverageWorkingDay * processPerDay;

            var totalTimeNeededToPerformWorkWithoutAutomation = GetTotalTimeNeededToPerformWorkWithoutAutomation();

            if (totalProcessesPerYear is null or 0)
                return null;

            var annualTran = AverageWorkingDay * ActivityVolumeAverage;

            var total = totalTimeNeededToPerformWorkWithoutAutomation / totalProcessesPerYear * 60;
            var totalMinutes = total * 60;
            var final= totalMinutes / annualTran;

            return final;
        }





        public async Task<decimal> GetOneTimeCostAsync(
            Client client)
        {
            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            var core = this.GetCore();

            decimal total = 0;

            await UnitOfWork.BusinessIdeaStages.GetForIdeaAsync(core);
            await UnitOfWork.BusinessImplementationCosts.GetForIdeaStageAsync(core.IdeaStages);
            foreach (var ideaStage in core.IdeaStages)
            {
                await UnitOfWork.BusinessRoles.GetRoleForAsync(ideaStage.ImplementationCosts);
                foreach (var implementationCost in ideaStage.ImplementationCosts)
                {
                    var roleCost =
                        await UnitOfWork.BusinessRoleCosts.SingleOrDefaultAsync(x => x.ClientId == client.Id && x.RoleId == implementationCost.Role.Id);

                    if (roleCost != null)
                        total += roleCost.Cost * implementationCost.Day/100 * implementationCost.Allocation;
                }
            }

            return total;
        }

        public async Task<decimal> GetOneTimeCost()
        {
            // Guard Clause
            if (UnitOfWork == null)
                throw new NullReferenceException("UnitOfWork missing");

            var core = this.GetCore();

            decimal total = 0;

            await UnitOfWork.BusinessIdeaStages.GetForIdeaAsync(core);
            await UnitOfWork.BusinessImplementationCosts.GetForIdeaStageAsync(core.IdeaStages);
            foreach (var ideaStage in core.IdeaStages)
            {
                await UnitOfWork.BusinessRoles.GetRoleForAsync(ideaStage.ImplementationCosts);
                foreach (var implementationCost in ideaStage.ImplementationCosts)
                {
                    var roleCost =
                        await UnitOfWork.BusinessRoleCosts.SingleOrDefaultAsync(x => x.RoleId == implementationCost.Role.Id);

                    if (roleCost != null)
                        total += roleCost.Cost * implementationCost.Day / 100 * implementationCost.Allocation;
                }
            }

            return total;
        }

        public async Task<decimal> GetTimeToBreakEvenAsync(
            Client client)
        {
            var oneTimeCosts = await GetOneTimeCostAsync(client);

            var benefit = GetCostPerYearForProcessBeforeAutomation();

            var runningCost = await GetRunningCostsAsync(UnitOfWork);

            if (benefit == 0)
                return 0;


            var value = await GetEstimateAsync(benefit) - runningCost;

            return oneTimeCosts / value;
        }

        public async Task<string> GetIdeaStatusByIdAsync(string ideaId)
        {

            var ideaStage = await Models.Business.IdeaStage.GatLast(UnitOfWork, ideaId);
            if (ideaStage == null)
                return "";

            await UnitOfWork.BusinessIdeaStageStatuses.GetForIdeaStageAsync(ideaStage.GetCore());
            var ideaStageStatuses = new List<IdeaStageStatus>(ideaStage.GetCore().IdeaStageStatuses.Select(x => new IdeaStageStatus(x)));
            ideaStageStatuses = ideaStageStatuses
                .OrderBy(x => x.CreatedDate)
                .ToList();

            if (ideaStageStatuses.Count == 0)
                return "";

            var ideaStageStatus = ideaStageStatuses.Last();
            await UnitOfWork.SharedIdeaStatuses.GetStatusForAsync(ideaStageStatus.GetCore());
            var content = $"<div class=\"pill {ideaStageStatus.Status?.ButtonClass}\" style=\"width: 100%; max-width: 190px;\">{ideaStageStatus.Status?.Name.Replace(" ", "\u00A0")}</div>";
            content = content.Replace("\"", "\\\"");

            var json = "{" +
                                "\"content\": \"" + content + "\"," +
                                "\"value\": \"" + ideaStageStatus.Status?.Name + "\"" +
                            "}";
            return json;
        }

        /// <summary>
        /// Get the total of all running costs for the year.
        /// Takes into account IdeaRunningCosts and IdeaOtherRunningCosts
        /// Requires: UnitOFWork
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public async Task<decimal> GetRunningCostsAsync(
            Data.Core.IUnitOfWork unitOfWork)
        {
            decimal total = 0;

            var core = GetCore();

            await unitOfWork.BusinessIdeaRunningCosts.GetForIdeaAsync(core);
            await unitOfWork.BusinessRunningCosts.GetRunningCostForAsync(core.IdeaRunningCosts);
            foreach (var ideaRunningCost in core.IdeaRunningCosts.Where(x => x.RunningCost.IsLive))
            {
                var cost = ideaRunningCost.RunningCost.Cost;
                if (ideaRunningCost.RunningCost.FrequencyId == "Monthly")
                    cost *= 12;

                total += cost;
            }


            await unitOfWork.BusinessIdeaOtherRunningCosts.GetForIdeaAsync(core);
            await unitOfWork.BusinessOtherRunningCosts.GetOtherRunningCostForAsync(core.IdeaOtherRunningCosts);
            foreach (var ideaOtherRunningCost in core.IdeaOtherRunningCosts.Where(x => x.OtherRunningCost.IsLive))
            {
                var cost = ideaOtherRunningCost.OtherRunningCost.Cost * ideaOtherRunningCost.Number;
                if (ideaOtherRunningCost.OtherRunningCost.FrequencyId == "Monthly")
                    cost *= 12;

                total += cost;

            }

            return total;
        }

    }
}
