using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.Services.Models.Account
{
    public partial class ChangePassword : PasswordAbstract
    {
        public string Id { get; set; }
        public string ReturnUrl { get; set; }

        [Required]
        [StringLength(
            1000,
            ErrorMessage = "The old password is required.",
            MinimumLength = 1)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Display(
            Name = "Old Password")]
        public string OldPassword { get; set; }
    }
}