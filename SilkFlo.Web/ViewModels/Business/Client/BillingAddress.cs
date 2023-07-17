using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.Client
{
    public class BillingAddress
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string CountryId { get; set; }
        public List<Models.Shared.Country> Countries { get; set; }

        /// <summary>
        /// Element names have different prefixes based on the context.
        /// For example:
        /// name = "Account.SignUp. ..."
        /// name = "Business.Client. ..."
        /// </summary>
        public string ElementNamePrefix { get; set; }

        public bool IsReadOnly { get; set; }
    }
}
