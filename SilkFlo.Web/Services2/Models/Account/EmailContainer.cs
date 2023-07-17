using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.Services2.Models.Account
{
    public class EmailContainer
    {
        private string _email;

        [Required]
        [StringLength(256,
            ErrorMessage = "Email must be between 1 and 256 in length.")]
        public string Email
        {
            get => _email;
            set
            {
                value = value?.Trim();
                _email = value;
            }
        }

        [Required]
        [StringLength(256,
            ErrorMessage = "Email confirmation must be between 1 and 256 in length.")]
        [DisplayName("Email Confirmation")]
        public string EmailConfirmation { get; set; }

        public void CompareEmail(ViewModels.Feedback feedback)
        {
            if (string.IsNullOrWhiteSpace(Email)
                || string.IsNullOrWhiteSpace(EmailConfirmation))
                return;

            if (string.Equals(
                    Email,
                    EmailConfirmation,
                    StringComparison.CurrentCultureIgnoreCase))
                return;

            const string message = "Confirmation email does not match the email.";

            if (feedback.Elements.ContainsKey("Email"))
                feedback.Elements["Email"] = message;
            else
                feedback.Elements.Add("Email", message);

            feedback.IsValid = false;
        }
    }
}
