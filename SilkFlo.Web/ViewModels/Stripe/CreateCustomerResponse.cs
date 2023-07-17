using Newtonsoft.Json;
using Stripe;

namespace SilkFlo.Web.ViewModels.Stripe
{
    public class CreateCustomerResponse
    {
        [JsonProperty("customer")]
        public Customer Customer { get; set; }
    }
}