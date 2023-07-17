using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.Services.Models.Account
{
    public partial class EmailChanged
    {
        private string _password;

        public string ReturnUrlIsSent { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string OldEmail { get; set; }

        [Required]
        [DisplayName("Password")]
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
    }
}