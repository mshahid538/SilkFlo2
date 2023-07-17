using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SilkFlo.Web.Services.Models.Account
{
    public abstract class PasswordAbstract
    {
        private string _password;
        private string _confirmPassword;

        [Required]
        [StringLength(100,
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password
        { 
            get => _password;
            set
            {
                if(value != null)
                    value = value.Trim();

                _password = value;
            }
        }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
                 ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword
        { 
            get => _confirmPassword;
            set
            {
                if(value != null)
                    value = value.Trim();

                _confirmPassword = value;
            }
        }

        public bool IsMatched()
        {
            return Password == ConfirmPassword;
        }

        public string IsPasswordValid(bool inLineOutput = false)
        {
            if (string.IsNullOrWhiteSpace(Password))
                return "No Password supplied";

            if (Password.Length < 8)
                return "Password is less than eight characters";

            var count = 0;

            var regEx = new Regex("[a-z]");
            if (regEx.IsMatch(Password))
                count++;


            regEx = new Regex("[A-Z]");
            if (regEx.IsMatch(Password))
                count++;


            regEx = new Regex("[0-9]");
            if (regEx.IsMatch(Password))
                count++;

            regEx = new Regex(@"[!@#\$%\^&\*]");
            if (regEx.IsMatch(Password))
                count++;

            if (count < 3)
            {
                
                var message =  "Contain at least 3 of the following:<br>" +
                       "* Lower case letters (a-z)<br>" +
                       "* Upper case letters (A-Z)<br>" +
                       "* Numbers (0-9)<br>" +
                       "* Special characters (ex. !@#$%^&*)";

                if (inLineOutput)
                {
                    message = message.Replace("<br>*", ", ");
                    message = message.Replace(":, ", ": ");
                }

                return message;
            }

            return "";
        }
    }
}