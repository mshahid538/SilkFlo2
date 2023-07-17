using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.ViewModels.Business.ManageStageAndStatus
{
    public class NextIdeaStatus
    {
        [Required]
        public string StatusId { get; set; }
        [Required]
        public string StageId { get; set; }
    }
}