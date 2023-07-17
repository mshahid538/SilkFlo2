namespace SilkFlo.Web.ViewModels.User
{
    public class AccountOwner
    {
        public bool IsReadOnly { get; set; }
        public bool IsInValid { get; set; }

        public string FullnameId { get; set; }

        public string EmailId { get; set; }
        public string EmailName { get; set; }




        public string TargetId { get; set; } = "Business.Client.AccountOwnerId";
        public string TargetName { get; set; } = "Business.Client.AccountOwnerId";

        public string TargetFirstNameId { get; set; } = "Business.Client.AccountOwnerFirstName";
        public string TargetFirstNameName { get; set; } = "Business.Client.AccountOwnerFirstName";
        
        public string TargetLastNameId { get; set; } = "Business.Client.AccountOwnerLastName";
        public string TargetLastNameName { get; set; } = "Business.Client.AccountOwnerLastName";


        public string TargetValue { get; set; }

        public string TargetStatusId { get; set; } = "Business.Client.AccountOwnerStatus";
        public string TargetStatusName { get; set; } = "Business.Client.AccountOwnerStatus";


        public string ModalName { get; set; } = "modelManageAccountOwner";

        public string ErrorMessage { get; set; }


        public string URLPrefix { get; set; } = "/api/user/SearchAccountOwner/";

        public Models.User User { get; set; }

        public string Cls
        {
            get
            {
                string str = "form-control";

                if (IsInValid)
                    str += " is-invalid";

                return str;
            }
        }
    }
}
