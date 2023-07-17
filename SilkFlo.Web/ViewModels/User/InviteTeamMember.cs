using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.ViewModels.User
{
    public class InviteTeamMember
    {
        [Required]
        [StringLength(100,
                      ErrorMessage = "First Name must be between 1 and 100 characters in length.")]
        [DisplayName("First Name")]

        public string FirstName { get; set; }

        [Required]
        [StringLength(100,
                      ErrorMessage = "Last Name must be between 1 and 100 characters in length.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }


        [Required]
        [StringLength(100,
            ErrorMessage = "Email must be between 1 and 100 characters in length.")]
        [DisplayName("Email")]
        public string EmailPrefix { get; set; }
        public string EmailSuffix { get; set; }
    }
}
