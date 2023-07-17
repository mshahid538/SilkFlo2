using System;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.ViewModels.Business.ManageStageAndStatus
{
    public class Poster : Abstract
    {
        public NextIdeaStatus NextIdeaStatus { get; set; }
        public CurrentIdeaStage CurrentIdeaStage { get; set; }
        public DateTime? MinDate { get; set; }
        [Required]
        public string IdeaId { get; set; }
    }
}
