using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.Services.Models.Account
{
    public class SignUpShort : PasswordAbstract
    {
        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Id cannot be greater than 255 characters in length.")]
        [DisplayName("Id")]
        public string Id { get; set; }

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



        public string Email { get; set; }

        public bool StaySignedIn { get; set; }
        public bool RememberMe { get; set; }
    }
}
