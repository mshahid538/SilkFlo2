using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.Services.Models.Account
{
    public partial class SignIn
    {
        private string _email;
        private string _password;
    
        [Required(ErrorMessage = "Email address required")]
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

        [Required(ErrorMessage = "Password required")]
        public string Password
        { 
            get
            {
                return _password;
            }
            set
            {
                if(value != null)
                    value = value.Trim();

                if (_password != value)
                    _password = value;
            }
        }

        public bool RememberMe { get; set; }
        public bool StaySignedIn { get; set; }
    }
}