using System;
using System.Collections.Generic;

namespace SilkFlo.Web.Models.Shop
{
    public partial class Coupon
    {
        /// <summary>
        /// Get the discount amount using the <c>Discount</c> value if not null.
        /// <para>Otherwise the discount is calculated using the <c>DiscountPercent</c> property and the supplied amount.</para>
        /// </summary>
        /// <param name="amount">Teh amount the current price.</param>
        /// <returns>Content of <c>DiscountAmount</c> property.</returns>
        public decimal GetDiscount(decimal? amount)
        {
            // Guard Cause
            if (amount is null or 0)
            {
                DiscountAmount = 0;
                return DiscountAmount;
            }

            DiscountAmount = Discount is > 0 ? 
                Discount ?? 0 : 
                (amount ?? 0) / 100 * (DiscountPercent ?? 0);

            return DiscountAmount;
        }

        public decimal DiscountAmount { get; set; }

        public bool IsValid { get; set; } = true;

        public string InValidMessage { get; set; }



        /// <summary>
        /// Check that the coupon is valid based on the following checks:
        /// <para>* Is the coupon expiry date is in the future.</para>
        /// <para>* UseCount is less than the use total.</para>
        /// <para>Assigns the result to <c>IsValid</c> property</para>
        /// </summary>
        /// <param name="subscription">The current subscription</param>
        /// <returns>Content of <c>InValidMessage</c> property.</returns>
        public string CheckValid(Subscription subscription = null)
        {
            var now = DateTime.Now.Date;

            IsValid = true;
            if (DateExpiry != null
                && (DateExpiry??now) < now)
            {
                IsValid = false;
                InValidMessage = "Coupon Expired.";
                return InValidMessage;
            }

            if (UseTotal != null 
                && UseTotal <= UseCount)
            {
                if (subscription == null)
                {
                    IsValid = false;
                    InValidMessage = "Coupon has been used too many times.";
                    return InValidMessage;
                }

                if (subscription.CouponId != Id)
                {
                    IsValid = false;
                    InValidMessage = "Coupon has been used too many times.";
                    return InValidMessage;
                }
            }

            InValidMessage = "";
            return InValidMessage;
        }
    }
}