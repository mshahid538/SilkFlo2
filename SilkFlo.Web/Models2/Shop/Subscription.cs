using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core;

namespace SilkFlo.Web.Models.Shop
{
    public partial class Subscription
    {
        public string Name
        {
            get
            {
                var value = "";

                if (Price != null)
                {
                    if (Price.Product == null)
                        throw new ArgumentNullException(nameof(Price.Product));

                    if (Price.Period == null)
                        throw new ArgumentNullException(nameof(Price.Period));


                    value = Price.Product.Name + "&nbsp;(" + Price.Period.NamePlural + ")";
                }

                if (string.IsNullOrWhiteSpace(value))
                    value = DateEnd == null ? "Gratis" : "Free&nbsp;Trial";

                return value;
            }
        }

        public double? RemainingDays
        {
            get
            {
                if (DateEnd == null)
                    return null;

                return ((DateTime)DateEnd - DateTime.Now.Date).TotalDays;
            }
        }

        public string BillingPeriod
        {
            get
            {
                var value = "";

                if (Price != null)
                {
                    if (Price.Product == null)
                        throw new ArgumentNullException(nameof(Price.Product));

                    if (Price.Period == null)
                        throw new ArgumentNullException(nameof(Price.Period));


                    value = Price.Period.NamePlural;
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    value = DateEnd == null ? 
                        "Free" : 
                        "Trial Period";
                }


                return value;
            }
        }

        public decimal Cost => Amount??0 - Discount??0;

        public string Currency
        {
            get
            {
                if (Price == null)
                    return "";

                if (Price.Currency == null)
                    return "";

                return Price.Currency.Symbol;
            }
        }

        public bool IsCancelled => DateCancelled != null;

        public bool IsActive => !IsCancelled && InDate;

        public bool IsFree => string.IsNullOrWhiteSpace(PriceId);


        public bool InDate
        {
            get
            {
                var now = DateTime.Now.Date;
                var dateStart = DateStart.Date;
                var dateEnd = (DateEnd ?? now).Date;

                return dateStart <= now
                    && dateEnd >= now;
            }
        }


        public decimal GetRefund()
        {
            if (!IsActive)
                return 0;

            if (DateEnd == null)
                return 0;

            var now = DateTime.Now.Date;
            var dateEnd = (DateTime)DateEnd;
            var remainingDays = (dateEnd - now).Days;
            Console.WriteLine("remainingDays: " + remainingDays);

            if (remainingDays == 0)
                return 0;

            var totalDays = (dateEnd - DateStart).Days;
            Console.WriteLine("totalDays: " + totalDays);


            if (Amount is null or 0)
                return 0;

            var amount = Amount??0 - Discount??0;
            Console.WriteLine("amount: " + amount);

            var amountPerDay = amount / totalDays;
            Console.WriteLine("amountPerDay: " + amountPerDay);

            return Math.Round(remainingDays * amountPerDay, 2);
        }


        /// <summary>
        /// Create a subscription with Stripe.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="client"></param>
        /// <param name="price"></param>
        /// <param name="defaultTrailPeriod"></param>
        /// <param name="coupon"></param>
        /// <returns></returns>
        public static async Task<Subscription> CreateAsync(
            IUnitOfWork unitOfWork,
            Data.Core.Domain.Business.Client client,
            Data.Core.Domain.Shop.Price price,
            int defaultTrailPeriod,
            Data.Core.Domain.Shop.Coupon coupon = null)
        {
            #region Guard Clauses
            // Guard Clause
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            // Guard Clause
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            // Guard Clause
            if (string.IsNullOrWhiteSpace(client.StripeId))
                throw new ArgumentNullException(nameof(client.StripeId));

            // Guard Clause
            if (price == null)
                throw new ArgumentNullException(nameof(price));
            #endregion



            var clientModel = new Models.Business.Client(client);
            clientModel.Subscription = await clientModel.GetLastSubscriptionAsync(
                unitOfWork);

            // Get the trial Days
            int? trialDay = null;
            decimal? discount = null;
            if (coupon == null)
            {
                if (clientModel.Subscription == null)
                    trialDay = defaultTrailPeriod;
            }
            else
            {
                if (coupon.TrialDay != null)
                    trialDay = coupon.TrialDay;

                var modelCoupon = new Models.Shop.Coupon(coupon);
                modelCoupon.CheckValid();
                if (modelCoupon.IsValid)
                {
                    modelCoupon.GetDiscount(price.Amount);
                    discount = modelCoupon.DiscountAmount;
                }
            }


            DateTime? trialEnd = null;
            var dateStart = DateTime.Now.Date;
            if(trialDay != null)
                trialEnd = dateStart.AddDays((double) trialDay);


            var stripeSubscription = await SilkFlo.Web.Services2.Models.PaymentManager.CreateSubscription( //Payment.Manager.CreateSubscription(
                price.Id,
                client.StripeId,
                trialEnd);


            var subscription = new Data.Core.Domain.Shop.Subscription
            {
                TenantId = client.Id,
                DateStart = stripeSubscription.StartDate,
                DateEnd = stripeSubscription.CurrentPeriodEnd.AddDays(1),
                Coupon = coupon,
                Discount = discount
            };

            if (trialEnd != null)
            {
                subscription.Amount = price.Amount;
                subscription.Price = price;
            }

            await unitOfWork.AddAsync(subscription);
            await unitOfWork.CompleteAsync();

            //ToDo: Add practice account, if required

            return new Subscription(subscription);
        }



        /// <summary>
        /// Create a subscription, but not on Stripe, this is used for demos and free trials
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="client"></param>
        /// <param name="priceId"></param>
        /// <param name="couponId"></param>
        /// <param name="agency"></param>
        /// <param name="agencyDiscountId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<List<Subscription>> CreateAsync(
            IUnitOfWork unitOfWork,
            Business.Client client,
            string priceId,
            string couponId = "",
            Business.Client agency = null,
            string agencyDiscountId = "")
        {
            // Guard Clause
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            // Guard Clause
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            // Guard Clause
            if (string.IsNullOrWhiteSpace(priceId))
                throw new ArgumentNullException(nameof(priceId));

            // Guard Clause
            if (client.TypeId != Enumerators.ClientType.Client39.ToString())
                throw new ArgumentException("Client cannot be an agency.");


            var price = await unitOfWork.ShopPrices.GetAsync(priceId);

            // Guard Clause
            if (price == null)
                throw new ArgumentException($"Price with id {priceId} nor found.");

            
            // Guard Clause
            if (!price.IsActive)
                throw new ArgumentException("Price is not active.");


            await unitOfWork.SharedPeriods.GetPeriodForAsync(price);


            // Guard Clause
            if (price.Period == null)
                throw new ArgumentException("No period assigned to price.");




            var dateStart = DateTime.Now.Date;
            var dateEnd = dateStart.AddMonths(price.Period.MonthCount);

            int trialPeriod = 0;

            await unitOfWork.ShopSubscriptions.GetForTenantAsync(client.GetCore());

            if (!client.TenantSubscriptions.Any())
            {
                var setting = new Application.Setting(unitOfWork);
                trialPeriod = await setting.GetTrialPeriod();
            }


            var agencyTypeId = "";
            if (agency != null)
                agencyTypeId = agency.TypeId;


            decimal discount = 0;
            if (!string.IsNullOrWhiteSpace(couponId))
            {
                var coupon = await unitOfWork.ShopCoupons.GetAsync(couponId);
                if (coupon != null)
                {
                    var couponModel = new Coupon(coupon);
                    discount = couponModel.GetDiscount(price.Amount);

                    if(coupon.TrialDay != null)
                        trialPeriod = coupon.TrialDay??0;
                }
            }


            var subscriptions = new List<Subscription>();
            if (trialPeriod > 0)
            {
                dateEnd = dateEnd.AddDays(trialPeriod);

                var subscriptionTrial = new Subscription
                {
                    Tenant = client,
                    Agency = agency,
                    DateStart = dateStart,
                    DateEnd = dateEnd,
                    AgencyDiscountId = agencyDiscountId,
                    AgencyTypeId = agencyTypeId,
                    CouponId = couponId,
                    Discount = discount,
                };

                subscriptions.Add(subscriptionTrial);
                await unitOfWork.AddAsync(subscriptionTrial.GetCore());

                dateStart = dateEnd.AddDays(1);
                dateEnd = dateStart.AddMonths(price.Period.MonthCount);
            }



            var subscription = new Subscription
            {
                Tenant = client,
                Agency = agency,
                DateStart = dateStart,
                DateEnd = dateEnd,
                Price = new Price(price),
                AgencyDiscountId = agencyDiscountId,
                AgencyTypeId = agencyTypeId,
                CouponId = couponId,
            };

            subscriptions.Add(subscription);
            await unitOfWork.AddAsync(subscription.GetCore());



            return subscriptions;
        }
    }
}