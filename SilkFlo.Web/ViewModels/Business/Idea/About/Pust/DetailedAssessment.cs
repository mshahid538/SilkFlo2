using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.ViewModels.Business.Idea.About.Put
{
    public class DetailedAssessment
    {
        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Id cannot be greater than 255 characters in length.")]
        [Required]
        public string Id { get; set; }

        [StringLength(100,
            MinimumLength = 1,
            ErrorMessage = "Name cannot be greater than 100 characters in length.")]
        [Required]
        public string Name { get; set; }

        [StringLength(100,
            MinimumLength = 0,
            ErrorMessage = "Sub Title cannot be greater than 100 characters in length.")]
        public string SubTitle { get; set; }

        [StringLength(750,
            MinimumLength = 1,
            ErrorMessage = "Summary cannot be greater than 750 characters in length.")]
        [Required]
        public string Summary { get; set; }

        [StringLength(750,
            MinimumLength = 1,
            ErrorMessage = "Pain Points cannot be greater than 750 characters in length.")]
        [DisplayName("Pain Points")]
        [Required]
        public string PainPointComment { get; set; }

        [StringLength(750,
            MinimumLength = 1,
            ErrorMessage = "Negative Impact cannot be greater than 750 characters in length.")]
        [DisplayName("Negative Impact")]
        [Required]
        public string NegativeImpactComment { get; set; }


        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Business Unit cannot be greater than 255 characters in length.")]
        [DisplayName("Business Unit")]
        [Required]
        public string DepartmentId { get; set; }

        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Area cannot be greater than 255 characters in length.")]
        public string TeamId { get; set; }

        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Sub-Area cannot be greater than 255 characters in length.")]
        public string ProcessId { get; set; }


        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Rule cannot be greater than 255 characters in length.")]
        [DisplayName("Rule")]
        [Required]
        public string RuleId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Rule) cannot be greater than 750 characters in length.")]
        public string RuleComment { get; set; }


        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Input cannot be greater than 255 characters in length.")]
        [DisplayName("Input")]
        [Required]
        public string InputId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Input) cannot be greater than 750 characters in length.")]
        public string InputComment { get; set; }


        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Input Data Structure cannot be greater than 255 characters in length.")]
        [DisplayName("Input Data Structure")]
        [Required]
        public string InputDataStructureId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Input Data Structure) cannot be greater than 750 characters in length.")]
        public string StructureComment { get; set; }


        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Process Change cannot be greater than 255 characters in length.")]
        [DisplayName("Process Change")]
        [Required]
        public string ProcessStabilityId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Process Change) cannot be greater than 750 characters in length.")]
        public string ProcessStabilityComment { get; set; }


        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Documentation Present cannot be greater than 255 characters in length.")]
        [DisplayName("Documentation Present")]
        [Required]
        public string DocumentationPresentId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Documentation Present) cannot be greater than 750 characters in length.")]
        public string DocumentationPresentComment { get; set; }


        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Benefit Expected cannot be greater than 750 characters in length.")]
        public string BenefitExpected { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Benefit Actual cannot be greater than 750 characters in length.")]
        public string BenefitActual { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Challenge Expected cannot be greater than 750 characters in length.")]
        public string ChallengeExpected { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Challenge Actual cannot be greater than 750 characters in length.")]
        public string ChallengeActual { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Lessen Learnt cannot be greater than 750 characters in length.")]
        public string LessenLearnt { get; set; }


        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Application Stability cannot be greater than 255 characters in length.")]
        [DisplayName("Application Stability")]
        [Required]
        public string ApplicationStabilityId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Application Stability) cannot be greater than 750 characters in length.")]
        public string ApplicationStabilityComment { get; set; }


        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Automation Goal cannot be greater than 255 characters in length.")]
        [DisplayName("Automation Goal")]
        [Required]
        public string AutomationGoalId { get; set; }


        [Required]
        [DisplayName("Average Working Day")]
        public int? AverageWorkingDay { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Average Working Day) cannot be greater than 750 characters in length.")]
        public string AverageWorkingDayComment { get; set; }

        [Required]
        [DisplayName("Working Hours")]
        public decimal? WorkingHour { get; set; }
        
        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Working Hour) cannot be greater than 750 characters in length.")]
        public string WorkingHourComment { get; set; }
 


        public int? AverageEmployeeFullCost { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Average Employee Full Cost) cannot be greater than 750 characters in length.")]
        public string AverageEmployeeFullCostComment { get; set; }



        [Required]
        [DisplayName("Employee Count")]
        public int? EmployeeCount { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Employee Count) cannot be greater than 750 characters in length.")]
        public string EmployeeCountComment { get; set; }



        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Task Frequency cannot be greater than 255 characters in length.")]
        [DisplayName("Task Frequency")]
        [Required]
        public string TaskFrequencyId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Task Frequency) cannot be greater than 750 characters in length.")]
        public string TaskFrequencyComment { get; set; }


        [DisplayName("Activity Volume Average")]
        [Required]
        public int? ActivityVolumeAverage { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Activity Volume Average) cannot be greater than 750 characters in length.")]
        public string ActivityVolumeAverageComment { get; set; }


        [DisplayName("Average Processing Time")]
        [Required]
        public decimal? AverageProcessingTime { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Average Processing Time) cannot be greater than 750 characters in length.")]
        public string AverageProcessingTimeComment { get; set; }


        public int? AverageErrorRate { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Average Error Rate) cannot be greater than 750 characters in length.")]
        public string AverageErrorRateComment { get; set; }


        public decimal? AverageReworkTime { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Average Rework Time) cannot be greater than 750 characters in length.")]
        public string AverageReworkTimeComment { get; set; }



        public decimal? AverageWorkToBeReviewed { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Average Work to be Reviewed) cannot be greater than 750 characters in length.")]
        public string AverageWorkToBeReviewedComment { get; set; }



        public decimal? AverageReviewTime { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Average Review Time) cannot be greater than 750 characters in length.")]
        public string AverageReviewTimeComment { get; set; }


        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Process Peak cannot be greater than 255 characters in length.")]
        [DisplayName("Process Peak")]
        [Required]
        public string ProcessPeakId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Process Peak) cannot be greater than 750 characters in length.")]
        public string ProcessPeakComment { get; set; }



        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Average Number of Steps cannot be greater than 255 characters in length.")]
        [DisplayName("Average Number of Steps")]
        [Required]
        public string AverageNumberOfStepId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Average Number of Step) cannot be greater than 750 characters in length.")]
        public string AverageNumberOfStepComment { get; set; }



        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Data Input Percent of Structured cannot be greater than 255 characters in length.")]
        [DisplayName("Data Input Percent of Structured")]
        [Required]
        public string DataInputPercentOfStructuredId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Data Input Percent of Structured) cannot be greater than 750 characters in length.")]
        public string DataInputPercentOfStructuredComment { get; set; }



        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Number of Ways To Complete Process cannot be greater than 255 characters in length.")]
        [DisplayName("Number of Ways To Complete Process")]
        [Required]
        public string NumberOfWaysToCompleteProcessId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Number of Ways to Complete Process) cannot be greater than 750 characters in length.")]
        public string NumberOfWaysToCompleteProcessComment { get; set; }



        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Decision Count cannot be greater than 255 characters in length.")]
        [DisplayName("Decision Count")]
        [Required]
        public string DecisionCountId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Decision Count) cannot be greater than 750 characters in length.")]
        public string DecisionCountComment { get; set; }



        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Decision Difficulty cannot be greater than 255 characters in length.")]
        [DisplayName("Decision Difficulty")]
        [Required]
        public string DecisionDifficultyId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Decision Difficulty) cannot be greater than 750 characters in length.")]
        public string DecisionDifficultyComment { get; set; }


        public decimal? PotentialFineAmount { get; set; }
        public decimal? PotentialFineProbability { get; set; }
        public bool IsDataInputScanned { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Data Input Scanned) cannot be greater than 750 characters in length.")]
        public string DataInputScannedComment { get; set; }
        public bool IsDataSensitive { get; set; }
        public bool IsHighRisk { get; set; }
        public bool IsAlternative { get; set; }
        public bool IsHostUpgrade { get; set; }

        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Process Owner cannot be greater than 255 characters in length.")]
        public string ProcessOwnerId { get; set; }


        public List<Models.Business.IdeaApplicationVersion> IdeaApplicationVersions { get; set; } = new();

        public ManageStageAndStatus.Poster ManageStageAndStatus { get; set; }

        public void UpdateCore(
            Data.Core.Domain.Business.Idea idea,
            bool updateProcessOwner)
        {
            idea.Name = Name;
            idea.SubTitle = SubTitle;
            idea.Summary = Summary;
            idea.PainPointComment = PainPointComment;
            idea.NegativeImpactComment = NegativeImpactComment;
            idea.DepartmentId = DepartmentId;
            idea.TeamId = TeamId;
            idea.ProcessId = ProcessId;
            idea.RuleId = RuleId;
            idea.RuleComment = RuleComment;
            idea.InputId = InputId;
            idea.InputComment = InputComment;
            idea.InputDataStructureId = InputDataStructureId;
            idea.StructureComment = StructureComment;
            idea.ProcessStabilityId = ProcessStabilityId;
            idea.ProcessStabilityComment = ProcessStabilityComment;
            idea.DocumentationPresentId = DocumentationPresentId;
            idea.DocumentationPresentComment = DocumentationPresentComment;
            idea.BenefitExpected = BenefitExpected;
            idea.BenefitActual = BenefitActual;
            idea.ChallengeExpected = ChallengeExpected;
            idea.ChallengeActual = ChallengeActual;
            idea.LessenLearnt = LessenLearnt;
            idea.ApplicationStabilityId = ApplicationStabilityId;
            idea.ApplicationStabilityComment = ApplicationStabilityComment;
            idea.AutomationGoalId = AutomationGoalId;
            idea.AverageWorkingDay = AverageWorkingDay;
            idea.AverageWorkingDayComment = AverageWorkingDayComment;
            idea.WorkingHour = WorkingHour;
            idea.WorkingHourComment = WorkingHourComment;
            idea.AverageEmployeeFullCost = AverageEmployeeFullCost;
            idea.AverageEmployeeFullCostComment = AverageEmployeeFullCostComment;
            idea.EmployeeCount = EmployeeCount;
            idea.EmployeeCountComment = EmployeeCountComment;
            idea.TaskFrequencyId = TaskFrequencyId;
            idea.TaskFrequencyComment = TaskFrequencyComment;
            idea.ActivityVolumeAverage = ActivityVolumeAverage;
            idea.ActivityVolumeAverageComment = ActivityVolumeAverageComment;
            idea.AverageProcessingTime = AverageProcessingTime;
            idea.AverageProcessingTimeComment = AverageProcessingTimeComment;
            idea.AverageErrorRate = AverageErrorRate;
            idea.AverageErrorRateComment = AverageErrorRateComment;
            idea.AverageReworkTime = AverageReworkTime;
            idea.AverageReworkTimeComment = AverageReworkTimeComment;
            idea.AverageWorkToBeReviewed = AverageWorkToBeReviewed;
            idea.AverageWorkToBeReviewedComment = AverageWorkToBeReviewedComment;
            idea.AverageReviewTime = AverageReviewTime;
            idea.AverageReviewTimeComment = AverageReviewTimeComment;
            idea.ProcessPeakId = ProcessPeakId;
            idea.ProcessPeakComment = ProcessPeakComment;
            idea.AverageNumberOfStepId = AverageNumberOfStepId;
            idea.AverageNumberOfStepComment = AverageNumberOfStepComment;
            idea.DataInputPercentOfStructuredId = DataInputPercentOfStructuredId;
            idea.DataInputPercentOfStructuredComment = DataInputPercentOfStructuredComment;
            idea.NumberOfWaysToCompleteProcessId = NumberOfWaysToCompleteProcessId;
            idea.NumberOfWaysToCompleteProcessComment = NumberOfWaysToCompleteProcessComment;
            idea.DecisionCountId = DecisionCountId;
            idea.DecisionCountComment = DecisionCountComment;
            idea.DecisionDifficultyId = DecisionDifficultyId;
            idea.DecisionDifficultyComment = DecisionDifficultyComment;
            idea.PotentialFineAmount = PotentialFineAmount;
            idea.PotentialFineProbability = PotentialFineProbability;
            idea.IsDataInputScanned = IsDataInputScanned;
            idea.DataInputScannedComment = DataInputScannedComment;
            idea.IsDataSensitive = IsDataSensitive;
            idea.IsHighRisk = IsHighRisk;
            idea.IsAlternative = IsAlternative;
            idea.IsHostUpgrade = IsHostUpgrade;

            if (updateProcessOwner)
                idea.ProcessOwnerId = ProcessOwnerId;
        }
    }
}