using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.ViewModels.Application
{
    public class Setting
    {
        public bool PracticeAccountCanSignIn { get; set; }

        [Required]
        [StringLength(50,
            ErrorMessage = "Practice Account Password must be between 1 and 100 characters in length.")]
        public string PracticeAccountPassword { get; set; }


        [Required]
        [StringLength(50,
            ErrorMessage = "Test Email Account must be between 1 and 100 characters in length.")]
        public string TestEmailAccount { get; set; }


        [Required]
        public int TrialPeriod { get; set; }


        public List<string> Errors { get; set; } = new();
    }
}
