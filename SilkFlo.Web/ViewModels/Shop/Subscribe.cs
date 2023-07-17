using System.ComponentModel;

namespace SilkFlo.Web.ViewModels.Shop
{
    public class Subscribe : Services.Models.Account.SignUp
    {
        public Models.Shop.Product Product { get; set; }
        public bool IsSignedIn { get; set; }

        [DisplayName("Full name")]
        public string Fullname { get; set; }
        public string Terms { get; set; }
        public string StripePublicKey { get; set; }
        public string Coupon { get; set; }
        public bool ReceiveMarketing { get; set; }
        public string ReferrerCode { get; set; }

        public string CustomerStripeId { get; set; }
        public bool ShoBillingAddressNumber { get; set; }
    }
}