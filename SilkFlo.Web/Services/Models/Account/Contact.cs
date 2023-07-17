using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.Services.Models.Account
{
    public partial class Contact
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _subject;
        private string _text;
        private string _result;

        [DisplayName("First Name")]
        [StringLength(50)]
        public string FirstName
        { 
            get
            {
                return _firstName;
            }
            set
            {
                if(value != null)
                    value = value.Trim();

                if (_firstName != value)
                    _firstName = value;
            }
        }

        [DisplayName("Last Name")]
        [StringLength(50)]
        public string LastName
        { 
            get
            {
                return _lastName;
            }
            set
            {
                if(value != null)
                    value = value.Trim();

                if (_lastName != value)
                    _lastName = value;
            }
        }

        [StringLength(50)]
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

        [StringLength(256)]
        public string Subject
        { 
            get
            {
                return _subject;
            }
            set
            {
                if(value != null)
                    value = value.Trim();

                if (_subject != value)
                    _subject = value;
            }
        }

        [StringLength(256)]
        public string Text
        { 
            get
            {
                return _text;
            }
            set
            {
                if(value != null)
                    value = value.Trim();

                if (_text != value)
                    _text = value;
            }
        }

        public string Result
        { 
            get
            {
                return _result;
            }
            set
            {
                if(value != null)
                    value = value.Trim();

                if (_result != value)
                    _result = value;
            }
        }

        public void Replace()
        {
            Email = Services.JSON_Tools.Replace(Email);
            Subject = Services.JSON_Tools.Replace(Subject);
            Text = Services.JSON_Tools.Replace(Text);
        }
    }
}