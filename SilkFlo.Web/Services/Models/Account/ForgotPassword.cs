using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.Services.Models.Account
{
    public partial class ForgotPassword
    {
        private string _email;

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
    }
}