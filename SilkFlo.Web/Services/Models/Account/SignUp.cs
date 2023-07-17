using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.Services.Models.Account
{
    public partial class SignUp
    {
        private string _password;
        private string _firstName;
        private string _lastName;




        [Required]
        [StringLength(100,
                      ErrorMessage = "First name must be between 1 and 100 in length.")]
        [DisplayName("First Name")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                value = value?.Trim();
                _firstName = value;
            }
        }

        [Required]
        [StringLength(100,
                      ErrorMessage = "Last name must be between 1 and 100 in length.")]
        [DisplayName("Last Name")]
        public string LastName
        {
            get => _lastName;
            set
            {
                value = value?.Trim();
                _lastName = value;
            }
        }


        [Required]
        [StringLength(100,
            ErrorMessage = "Password must be between 1 and 100 in length.")]
        public string Password
        { 
            get => _password;
            set
            {
                value = value?.Trim();
                _password = value;
            }
        }
    }
}