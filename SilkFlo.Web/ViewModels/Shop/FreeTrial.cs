using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.ViewModels.Shop
{
    public class FreeTrial : Services2.Models.Account.EmailContainer
    {
        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "ClientId cannot be greater than 255 characters in length.")]
        public string ClientId { get; set; }

        [Required]
        [StringLength(100,
            MinimumLength = 0,
            ErrorMessage = "First Name must be between 1 and 100 characters in length.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100,
            ErrorMessage = "Last Name must be between 1 and 100 characters in length.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }


        [Required]
        [StringLength(255,
            MinimumLength = 0,
            ErrorMessage = "Password cannot be greater than 255 characters in length.")]
        public string Password { get; set; }

        [Required]
        [StringLength(100,
            ErrorMessage = "Name must be between 1 and 100 characters in length.")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100,
            ErrorMessage = "Website must be between 1 and 100 characters in length.")]
        [DisplayName("Website")]
        public string Website { get; set; }
        public bool TermsAgreed { get; set; }
        public bool ReceiveMarketing { get; set; }


        [Required]
        [StringLength(1000,
            MinimumLength = 0,
            ErrorMessage = "ReCaptchaToken cannot be greater than 1000 characters in length.")]
        public string ReCaptchaToken { get; set; }
    }
}