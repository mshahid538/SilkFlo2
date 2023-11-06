using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using Stripe;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilkFlo.Web.Services2.Models
{
    public class PaymentManager
    {
        public static string StripePublicKey => "pk_test_51KUSwbAzFT9dgqIm48jQQT7P7GTYB9kaWFtLyyVbm0s7CXkAoXFds5OxFsamBhNZUNx9qpn2qruLdCufCqvt2LXy00OyXhEWVR";

        private static string StripeKey => "sk_test_51KUSwbAzFT9dgqImObwaKoK6aYfwAvQLDvGnNJNgyHa8UgTqar2portOfGNzGpU4pOHLyXxVqVGhorjUpG8p3Odh00EWMGJpMY";

        public static string WebHookSecret => "whsec_29c409cbb889b8aa71426be2cff4eda89220ebfc8ce9fca92c02c260bb69e505";

        public static async Task<SetupIntent> CreateSetupIntent(Client client)
        {
            if (string.IsNullOrWhiteSpace(client.StripeId))
                return (SetupIntent)null;
            StripeConfiguration.ApiKey = PaymentManager.StripeKey;
            SetupIntentCreateOptions options = new SetupIntentCreateOptions()
            {
                Customer = client.StripeId,
                PaymentMethodTypes = new List<string>() { "card" }
            };
            SetupIntentService service = new SetupIntentService();
            SetupIntent setupIntent = await service.CreateAsync(options);
            return setupIntent;
        }

        public static async Task UpdatePricesAsync(IUnitOfWork unitOfWork)
        {
            StripeConfiguration.ApiKey = PaymentManager.StripeKey;
            PriceService service = new PriceService();
            StripeList<Stripe.Price> prices = await service.ListAsync((PriceListOptions)null, (RequestOptions)null, new CancellationToken());
            bool isSaved = true;
            foreach (Stripe.Price price1 in prices)
            {
                SilkFlo.Data.Core.Domain.Shop.Price core = await unitOfWork.ShopPrices.GetAsync(price1.Id);
                PriceRecurring recurring = price1.Recurring;
                string interval = recurring.Interval;
                string periodId = "Monthly";
                if (interval == "year")
                    periodId = "Annual";
                if (core == null)
                {
                    SilkFlo.Data.Core.Domain.Shop.Price price2 = new SilkFlo.Data.Core.Domain.Shop.Price();
                    price2.ProductId = price1.ProductId;
                    price2.CurrencyId = price1.Currency;
                    Decimal? unitAmountDecimal = price1.UnitAmountDecimal;
                    Decimal num = (Decimal)100;
                    price2.Amount = unitAmountDecimal.HasValue ? new Decimal?(unitAmountDecimal.GetValueOrDefault() / num) : new Decimal?();
                    price2.IsLive = price1.Livemode;
                    price2.IsActive = price1.Active;
                    price2.PeriodId = periodId;
                    core = price2;
                    await unitOfWork.AddAsync(core);
                    core.Id = price1.Id;
                }
                else
                {
                    core.ProductId = price1.ProductId;
                    core.CurrencyId = price1.Currency;
                    SilkFlo.Data.Core.Domain.Shop.Price price3 = core;
                    Decimal? unitAmountDecimal = price1.UnitAmountDecimal;
                    Decimal num = (Decimal)100;
                    Decimal? nullable = unitAmountDecimal.HasValue ? new Decimal?(unitAmountDecimal.GetValueOrDefault() / num) : new Decimal?();
                    price3.Amount = nullable;
                    core.IsLive = price1.Livemode;
                    core.IsActive = price1.Active;
                    core.PeriodId = periodId;
                }
                if (!core.IsSaved)
                    isSaved = false;
                core = (SilkFlo.Data.Core.Domain.Shop.Price)null;
                recurring = (PriceRecurring)null;
                interval = (string)null;
                periodId = (string)null;
            }
            if (isSaved)
            {
                service = (PriceService)null;
                prices = (StripeList<Stripe.Price>)null;
            }
            else
            {
                int num = (int)await unitOfWork.CompleteAsync();
                service = (PriceService)null;
                prices = (StripeList<Stripe.Price>)null;
            }
        }

        public static async Task<Customer> SaveAsync(Client client)
        {
            StripeConfiguration.ApiKey = PaymentManager.StripeKey;
            CustomerService customerService = new CustomerService();
            if (string.IsNullOrWhiteSpace(client.StripeId))
            {
                CustomerCreateOptions options = new CustomerCreateOptions()
                {
                    Name = client.Name,
                    Address = new AddressOptions()
                    {
                        Line1 = client.Address1,
                        Line2 = client.Address2,
                        PostalCode = client.PostCode,
                        Country = client.CountryId,
                        City = client.City,
                        State = client.State
                    }
                };
                if (client.AccountOwner != null)
                    options.Email = client.AccountOwner.Email;
                Customer customer = await customerService.CreateAsync(options);
                if (customer != null)
                    client.StripeId = customer.Id;
                return customer;
            }
            CustomerUpdateOptions options1 = new CustomerUpdateOptions()
            {
                Name = client.Name,
                Address = new AddressOptions()
                {
                    Line1 = client.Address1,
                    Line2 = client.Address2,
                    PostalCode = client.PostCode,
                    Country = client.CountryId,
                    City = client.City,
                    State = client.State
                }
            };
            if (client.AccountOwner != null)
                options1.Email = client.AccountOwner.Email;
            Customer customer1 = await customerService.UpdateAsync(client.StripeId, options1);
            return customer1;
        }

        public static async Task<Customer> GetCustomerAsync(string stripePrimaryKey)
        {
            StripeConfiguration.ApiKey = PaymentManager.StripeKey;
            CustomerService customerService = new CustomerService();
            Customer async = await customerService.GetAsync(stripePrimaryKey, (CustomerGetOptions)null, (RequestOptions)null, new CancellationToken());
            customerService = (CustomerService)null;
            return async;
        }

        public static async Task<PaymentIntent> CreatePaymentIntent(long amount, string currency)
        {
            StripeConfiguration.ApiKey = PaymentManager.StripeKey;
            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntentService paymentIntentService1 = paymentIntentService;
            PaymentIntentCreateOptions options = new PaymentIntentCreateOptions();
            options.Amount = new long?(100L);
            options.Currency = "eur";
            options.AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions()
            {
                Enabled = new bool?(true)
            };
            CancellationToken cancellationToken = new CancellationToken();
            PaymentIntent async = await paymentIntentService1.CreateAsync(options, cancellationToken: cancellationToken);
            paymentIntentService = (PaymentIntentService)null;
            return async;
        }

        public static async Task<Stripe.Subscription> CreateSubscription(string priceId, string customerId, DateTime? trialEnd)
        {
            StripeConfiguration.ApiKey = PaymentManager.StripeKey;
            List<SubscriptionItemOptions> items = new List<SubscriptionItemOptions>()
      {
        new SubscriptionItemOptions() { Price = priceId }
      };
            SubscriptionPaymentSettingsOptions paymentSettings = new SubscriptionPaymentSettingsOptions()
            {
                SaveDefaultPaymentMethod = "on_subscription"
            };
            SubscriptionCreateOptions subscriptionOptions = new SubscriptionCreateOptions()
            {
                Customer = customerId,
                Items = items,
                PaymentSettings = paymentSettings,
                PaymentBehavior = "default_incomplete",
                TrialEnd = (AnyOf<DateTime?, SubscriptionTrialEnd>)trialEnd
            };
            subscriptionOptions.AddExpand("latest_invoice.payment_intent");
            SubscriptionService subscriptionService = new SubscriptionService();
            Stripe.Subscription subscription1;
            try
            {
                Stripe.Subscription subscription = await subscriptionService.CreateAsync(subscriptionOptions);
                subscription1 = subscription;
            }
            catch (Exception ex)
            {
                Console.WriteLine((object)ex);
                throw;
            }
            items = (List<SubscriptionItemOptions>)null;
            paymentSettings = (SubscriptionPaymentSettingsOptions)null;
            subscriptionOptions = (SubscriptionCreateOptions)null;
            subscriptionService = (SubscriptionService)null;
            return subscription1;
        }

        public static async Task<Invoice> GetInvoiceAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return (Invoice)null;
            StripeConfiguration.ApiKey = PaymentManager.StripeKey;
            InvoiceService invoiceService = new InvoiceService();
            Invoice async = await invoiceService.GetAsync(id, (InvoiceGetOptions)null, (RequestOptions)null, new CancellationToken());
            return async;
        }

        public static DateTime GetDateTime(long seconds) => new DateTime(1970, 1, 1).AddDays((double)(seconds / 60L / 60L / 24L));

        public static void SetUpWebHooks()
        {
        }

    }
}
