using System.Collections.Generic;


namespace SilkFlo.Web.Models
{
    public partial class User
    {
        public string RoleSummary
        {
            get
            {
                return UserRoles.Count switch
                {
                    0 => "",
                    > 1 => UserRoles.Count + " Roles",
                    _ => UserRoles[0].Role?.Name
                };
            }
        }

        /// <summary>
        /// Populate the UserRoles list
        /// </summary>
        public Status Status
        {
            get
            {
                if (!IsSubscriber())
                    return Status.Inactive;

                return IsEmailConfirmed ? Status.Active : Status.Pending;
            }
        }

        private bool IsSubscriber()
        {
            return UserRoles.Count != 0;
        }


        private string _emailPrefix = "";
        public string EmailPrefix
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_emailPrefix))
                    return _emailPrefix;

                if (string.IsNullOrWhiteSpace(Email))
                    _emailPrefix = "";
                else
                {
                    var parts = Email.Split("@");
                    _emailPrefix = parts[0];
                }

                return _emailPrefix;
            }
            set => _emailPrefix = value;
        }

        public string EmailSuffix { get; set; }

        private string _emailNewPrefix = "";
        public string EmailNewPrefix
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_emailNewPrefix))
                    return _emailNewPrefix;

                if (string.IsNullOrWhiteSpace(EmailNew))
                    _emailNewPrefix = "";
                else
                {
                    var parts = EmailNew.Split("@");
                    _emailNewPrefix = parts[0];
                }

                return _emailNewPrefix;
            }
            set => _emailNewPrefix = value;
        }

        public List<Role> Roles { get; set; } = new();

        public List<Business.Role> BusinessRoles { get; set; } = new();


        public string GuestEmail { get; set; }
        public bool IsBusinessEmail { get; set; }
    }
}