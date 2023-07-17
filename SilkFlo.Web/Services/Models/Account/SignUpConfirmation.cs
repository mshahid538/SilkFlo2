using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.Services.Models.Account
{
    public class SignUpConfirmation
    {
        private string _email;

        public SignUpConfirmation(string email,
                                  string returnURL)
        {
            Email = email;
            ReturnURL = returnURL;
        }

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

        public string ReturnURL { get; set; }
    }
}