using System;
using System.ComponentModel.DataAnnotations;


namespace SilkFlo.Web.ViewModels.Business.ManageStageAndStatus
{
    public class CurrentIdeaStage
    {
        public bool IsAutomaticDate { get; set; }
        [Required]
        public string StageId { get; set; }
        public DateTime? DateStartEstimate { get; set; }
        public DateTime? DateEndEstimate { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public bool IsDeployedGroup { get; set; }
    }
}
