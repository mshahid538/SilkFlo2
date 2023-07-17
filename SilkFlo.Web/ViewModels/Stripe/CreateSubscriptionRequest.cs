namespace SilkFlo.Web.ViewModels.Stripe
{
    public class CreateSubscriptionRequest
    {
        public string PriceId { get; set; }
        public string CustomerId { get; set; }
        public string CouponName { get; set; }
    }
}
