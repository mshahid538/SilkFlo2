using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.Services.Models.Account
{
    public partial class SignUp : Services2.Models.Account.EmailContainer
    {
        private string _name;

        [Required]
        [StringLength(50,
              ErrorMessage = "Company must be between 1 and 100 in length.")]
        public string Name
        {
            get => _name;
            set
            {
                value = value?.Trim();
                _name = value;
            }
        }

        public string ClientId { get; set; }


        [StringLength(100,
            MinimumLength = 0,
            ErrorMessage = "Address 1 cannot be greater than 100 characters in length.")]
        [DisplayName("Address 1")]
        public string Address1 { get; set; }

        [StringLength(100,
            MinimumLength = 0,
            ErrorMessage = "Address 2 cannot be greater than 100 characters in length.")]
        [DisplayName("Address 2")]
        public string Address2 { get; set; }

        [StringLength(100,
            MinimumLength = 0,
            ErrorMessage = "City / Town cannot be greater than 100 characters in length.")]
        [DisplayName("City / Town")]
        public string City { get; set; }

        [StringLength(100,
            MinimumLength = 0,
            ErrorMessage = "County / State cannot be greater than 100 characters in length.")]
        [DisplayName("County / State")]
        public string State { get; set; }

        [StringLength(100,
            MinimumLength = 0,
            ErrorMessage = "Post Code cannot be greater than 100 characters in length.")]
        [DisplayName("Post Code")]
        public string PostCode { get; set; }

        public string BillingAddress
        {
            get
            {
                var s = Name;
                if (!string.IsNullOrWhiteSpace(Address1))
                    s += "<br/>" + Address1;

                if (!string.IsNullOrWhiteSpace(Address2))
                    s += "<br/>" + Address2;

                if (!string.IsNullOrWhiteSpace(City))
                    s += "<br/>" + City;

                if (!string.IsNullOrWhiteSpace(State))
                    s += "<br/>" + State;

                if (!string.IsNullOrWhiteSpace(PostCode))
                    s += "<br/>" + PostCode;

                if(Country != null)
                    s += "<br/>" + Country.Name;


                return s;
            }
        }


        [DisplayName("Country")]
        public string CountryId { get; set; }

        public Web.Models.Shared.Country Country{ get; set; }

        public bool TermsAgreed { get; set; }

        public List<Web.Models.Shared.Country> Countries { get; set; } = new();





        [Required]
        public string Website { get; set; }


        [Required]
        public string ReCaptchaToken { get; set; }

        public bool ShowAddress { get; set; } = true;
        public string PriceId { get; set; }
        public bool IsMsUser { get; set; }
    }
}