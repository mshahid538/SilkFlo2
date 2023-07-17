namespace SilkFlo.Web.ViewModels.Settings
{
    public class Tenants
    {
        public Models.Business.Client Client { get; set; }
        public Subscriptions Subscriptions { get; set; }

        public bool ShowSubscriptionButton { get; set; }
    }
}