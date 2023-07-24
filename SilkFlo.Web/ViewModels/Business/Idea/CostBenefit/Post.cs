using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.ViewModels.Business.Idea.CostBenefit
{
    public class Post
    {
        [Required]
        [StringLength(255,
            ErrorMessage = "Id must be between 1 and 255 characters in length.")]
        public string Id { get; set; }


        public string EaseOfImplementationFinal { get; set; }


        [StringLength(255,
            ErrorMessage = "Running Cost Id must be between 1 and 255 characters in length.")]
        public string RunningCostId { get; set; }
        

        [Range(0, int.MaxValue)]
        public decimal? ProcessVolumetryPerYear { get; set; }


        [Range(0, int.MaxValue)]
        public decimal? ProcessVolumetryPerMonth { get; set; }


        [Range(0, int.MaxValue)]
        public int? EmployeeCount { get; set; }

        [Range(0, 24,
            ErrorMessage = "Automation Working Hours/Day must be between 0 and 24.")]
        public int? RobotWorkHourDay { get; set; }

        [Range(0, 365,
            ErrorMessage = "Automation Working Days/Year must be between 0 and 365.")]
        public int? RobotWorkDayYear { get; set; }

        [Range(0, int.MaxValue)]
        public decimal? RobotSpeedMultiplier { get; set; }

        [Range(0, int.MaxValue)]
        public decimal? AHTRobot { get; set; }
        
        [Range(0, int.MaxValue)]
        public decimal? WorkloadSplit { get; set; }


        public List<Models.Business.IdeaStage> IdeaStageEstimates { get; set; }

        public List<Models.Business.IdeaStage> IdeaStages { get; set; }

        //This is holds the contents of the table 'One Time Costs' in the UI
        public List<Models.Business.ImplementationCost> ImplementationCosts { get; set; }

        //This is holds the contents of the table 'RPA Software Costs' in the UI
        public List<Models.Business.IdeaRunningCost> IdeaRunningCosts { get; set; }

        public List<Models.Business.IdeaOtherRunningCost> IdeaOtherRunningCosts { get; set; }
    }
}
