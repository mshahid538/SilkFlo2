using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.Services.Models.Account
{
    public partial class ResetPassword: PasswordAbstract
    {
        private string _email;
        private string _resetToken;
    
        [Required]
        [EmailAddress]
        public string Email
        { 
            get
            {
                return _email;
            }
            set
            {
                if(value != null)
                    value = value.Trim();

                if (_email != value)
                    _email = value;
            }
        }

        public string ResetToken
        { 
            get
            {
                return _resetToken;
            }
            set
            {
                if(value != null)
                    value = value.Trim();

                if (_resetToken != value)
                    _resetToken = value;
            }
        }
    }
}