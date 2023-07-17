using System;

namespace SilkFlo.Web.Services.Models
{
    public class ActivateSubscriptionRequest
    {
        public string planId { get; set; }
        public int quantity { get; set; }
    }

    public class ActivateSubscriptionResponse
    {
        public bool IsSucceed { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ResolveSubscriptionResponse
    {
        public string id { get; set; }
        public string subscriptionName { get; set; }
        public string offerId { get; set; }
        public string planId { get; set; }
        public int quantity { get; set; }
        public SubscriptionDetails subscription { get; set; }
    }

    public class SubscriptionDetails
    {
        public string id { get; set; }
        public string publisherId { get; set; }
        public string offerId { get; set; }
        public string name { get; set; }
        public string saasSubscriptionStatus { get; set; }
        public Beneficiary beneficiary { get; set; }
        public Purchaser purchaser { get; set; }
        public string planId { get; set; }
        public TermDetails term { get; set; }
        public bool autoRenew { get; set; }
        public bool isTest { get; set; }
        public bool isFreeTrial { get; set; }
        public string[] allowedCustomerOperations { get; set; }
        public string sandboxType { get; set; }
        public string lastModified { get; set; }
        public int quantity { get; set; }
        public string sessionMode { get; set; }
    }

    public class Beneficiary
    {
        public string emailId { get; set; }
        public string objectId { get; set; }
        public string tenantId { get; set; }
        public string puid { get; set; }
    }

    public class Purchaser
    {
        public string emailId { get; set; }
        public string objectId { get; set; }
        public string tenantId { get; set; }
        public string puid { get; set; }
    }

    public class TermDetails
    {
        public string termUnit { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }

    public class TokenResponse
    {
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string ext_expires_in { get; set; }
        public string expires_on { get; set; }
        public string not_before { get; set; }
        public string resource { get; set; }
        public string access_token { get; set; }
    }
}
