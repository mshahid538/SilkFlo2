using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.Services.Models.Account
{
    public class BillingAddress
    {
        [Required]
        public string ClientId { get; set; }


        [Required]
        [StringLength(50,
            ErrorMessage = "Company name must be between 1 and 100 in length.")]
        public string Name { get; set; }

        [StringLength(100,
            ErrorMessage = "Address 1 must be between 1 and 100 characters in length.")]
        [DisplayName("Address 1")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [StringLength(100,
            ErrorMessage = "Post Code must be between 1 and 100 characters in length.")]
        [DisplayName("Post Code")]
        public string PostCode { get; set; }

        public string CountryId { get; set; }
    }
}