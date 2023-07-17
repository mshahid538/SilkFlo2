
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.ViewModels.Business.Idea.About.Put
{
    public class Overview
    {
        [StringLength(255,
         MinimumLength = 1,
         ErrorMessage = "Id cannot be greater than 255 characters in length.")]
        [Required]
        public string Id { get; set; }

        [StringLength(255,
            MinimumLength = 1,
            ErrorMessage = "Business Unit cannot be greater than 255 characters in length.")]
        [DisplayName("Department")]
        [Required]
        public string DepartmentId { get; set; }

        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Area cannot be greater than 255 characters in length.")]
        [DisplayName("Team")]
        public string TeamId { get; set; }


        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Sub-Area cannot be greater than 255 characters in length.")]
        [DisplayName("Process")]
        public string ProcessId { get; set; }


        [StringLength(100,
            MinimumLength = 1,
            ErrorMessage = "Name cannot be greater than 100 characters in length.")]
        [Required]
        public string Name { get; set; }


        [StringLength(100,
            MinimumLength = 0,
            ErrorMessage = "Sub Title cannot be greater than 100 characters in length.")]
        [DisplayName("Sub Title")]
        public string SubTitle { get; set; }
        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Summary cannot be greater than 750 characters in length.")]


        public string Summary { get; set; }
        [StringLength(750,
            MinimumLength = 1,
            ErrorMessage = "Pain Points cannot be greater than 750 characters in length.")]
        [DisplayName("Pain Point Comment")]
        [Required]
        public string PainPointComment { get; set; }


        [StringLength(750,
            MinimumLength = 1,
            ErrorMessage = "Negative Impact cannot be greater than 750 characters in length.")]
        [DisplayName("Negative Impact Comment")]
        [Required]
        public string NegativeImpactComment { get; set; }


        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Rule cannot be greater than 255 characters in length.")]
        [DisplayName("Rule")]
        public string RuleId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Rule) cannot be greater than 750 characters in length.")]
        [DisplayName("Rule Comment")]
        public string RuleComment { get; set; }
        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Input cannot be greater than 255 characters in length.")]
        public string InputId { get; set; }


        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Input) cannot be greater than 750 characters in length.")]
        [DisplayName("Input Comment")]
        public string InputComment { get; set; }

        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Input Data Structure cannot be greater than 255 characters in length.")]
        [DisplayName("Input Data Structure")]
        public string InputDataStructureId { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Input Data Structure) cannot be greater than 750 characters in length.")]
        [DisplayName("Structure Comment")]
        public string StructureComment { get; set; }


        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Process Change cannot be greater than 255 characters in length.")]
        [DisplayName("Process Stability")]
        public string ProcessStabilityId { get; set; }


        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Process Change) cannot be greater than 750 characters in length.")]
        [DisplayName("Process Stability Comment")]
        public string ProcessStabilityComment { get; set; }


        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Documentation Present cannot be greater than 255 characters in length.")]
        [DisplayName("Documentation Present")]
        public string DocumentationPresentId { get; set; }


        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Comment (Documentation Present) cannot be greater than 750 characters in length.")]
        [DisplayName("Documentation Present Comment")]
        public string DocumentationPresentComment { get; set; }


        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Benefit Expected cannot be greater than 750 characters in length.")]
        [DisplayName("Benefit Expected")]
        public string BenefitExpected { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Benefit Actual cannot be greater than 750 characters in length.")]
        [DisplayName("Benefit Actual")]
        public string BenefitActual { get; set; }


        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Challenge Expected cannot be greater than 750 characters in length.")]
        [DisplayName("Challenge Expected")]
        public string ChallengeExpected { get; set; }


        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Challenge Actual cannot be greater than 750 characters in length.")]
        [DisplayName("Challenge Actual")]
        public string ChallengeActual { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Lessen Learnt cannot be greater than 750 characters in length.")]
        [DisplayName("Lessen Learnt")]
        public string LessenLearnt { get; set; }
        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Process Owner cannot be greater than 255 characters in length.")]

        [DisplayName("Process Owner")]
        public string ProcessOwnerId { get; set; }


        public ManageStageAndStatus.Poster ManageStageAndStatus { get; set; }



        public void UpdateCore(
            Data.Core.Domain.Business.Idea idea,
            bool editAllIdeaFields,
            bool updateProcessOwner)
        {
            if (editAllIdeaFields)
            {
                idea.DepartmentId = DepartmentId;
                idea.TeamId = TeamId;
                idea.ProcessId = ProcessId;
                idea.Name = Name;
                idea.SubTitle = SubTitle;
                idea.Summary = Summary;
                idea.PainPointComment = PainPointComment;
                idea.NegativeImpactComment = NegativeImpactComment;
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
            }

            if (updateProcessOwner)
                idea.ProcessOwnerId = ProcessOwnerId;
        }
    }
}