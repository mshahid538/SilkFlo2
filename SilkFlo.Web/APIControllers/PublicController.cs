using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SilkFlo.Web.Controllers
{
    public class PublicController : AbstractResubscribe
    {
        public PublicController(Data.Core.IUnitOfWork unitOfWork,
                              Services.ViewToString viewToString,
                              IAuthorizationService authorization) : 
            base(
            unitOfWork, 
            viewToString, 
            authorization) { }




        [Route("Pricing")]
        [Route("Pricing/referrerCode/{referrerCode}")]
        public async Task<IActionResult> GetPricing(string referrerCode = "")
        {
            var headerText = "";
            var referrerMessage = "";

            // Guard Clause
            if (!string.IsNullOrWhiteSpace(referrerCode))
            {
                var referrer = await _unitOfWork.BusinessClients.GetAsync(referrerCode);
                referrerMessage = referrer == null ?
                    "<h2 class=\"text-danger\">No referrer agencies were found with the supplied referrer code.</h2>" :
                    $"<h2>{referrer.Name} has referred you to {Data.Core.Settings.ApplicationName}.</h2>";
            }


            var client = await GetClientAsync(false);

            if (client != null)
            {

                // Check subscription is handled by agency
                if (!string.IsNullOrWhiteSpace(client.AgencyId))
                {
                    await _unitOfWork.BusinessClients.GetAgencyForAsync(client);
                    if (client.Agency != null)
                    {
                        var text = client.Agency.Name;
                        await _unitOfWork.Users.GetAccountOwnerForAsync(client.Agency);

                        if (client.Agency.AccountOwner != null)
                            text =
                                $"{client.Agency.AccountOwner.Fullname} (<a href=\"{client.Agency.AccountOwner.Email}\">{client.Agency.AccountOwner.Email}</a>) from {text}";
                                      

                        text = "Please contact " + text + " to update your subscription.";

                        return View(
                            "/Views/Home/Page.cshtml",
                            $"<h1 style=\"margin: 2rem 2rem 0 2rem;\" class=\"text-info\">{text}<h1>");
                    }

                }

                // Guard Clause - Check Practice account
                if (client.IsPractice)
                    return View(
                        "/Views/Home/Page.cshtml",
                        "<h1 style=\"margin: 2rem 2rem 0 2rem;\" class=\"text-info\">You cannot resubscribe with a Practice Account.<h1>");


                // Guard Clause - Check Reseller Agency
                if (client.TypeId == Data.Core.Enumerators.ClientType.ResellerAgency45.ToString())
                    return View(
                        "/Views/Home/Page.cshtml",
                        "<h1 style=\"margin: 2rem 2rem 0 2rem;\" class=\"text-info\">Reseller are automatically subscribed.<h1>");



                var titleSuffix = "";
                var subscriptionState = "";
                var subscriptionOptions = "";
                Models.Shop.Subscription lastSubscription = null;
                var productButtonText = "Try for free";

                if ((await _authorization.AuthorizeAsync(User, Policy.Subscriber)).Succeeded)
                {
                    productButtonText = "Select";
                    var model = new Models.Business.Client(client);
                    lastSubscription = await model.GetLastSubscriptionAsync(_unitOfWork, false);

                    if (lastSubscription != null
                        && lastSubscription.DateEnd == null)
                        return PageView("<h1>Gratis! Your free subscription in on us!</h1>");

                    if (lastSubscription == null)
                    {
                        productButtonText = "Try for free";
                        subscriptionState = $"<h2>{client.Name} is currently unsubscribed.</h2>";
                        subscriptionOptions =
                            "<h2>You can subscribe at any time by selecting one of the products below and completing any payments.</h2>";
                    }






                    var tense = "will end on";
                    if (lastSubscription != null
                        && lastSubscription.DateEnd < DateTime.Now)
                        tense = "ended";

                    var type = "free trial";
                    if (lastSubscription != null 
                        && !string.IsNullOrWhiteSpace(lastSubscription.PriceId))
                    {
                        type = "unknown";
                        await _unitOfWork.ShopPrices.GetPriceForAsync(lastSubscription.GetCore());
                        if (lastSubscription.Price != null)
                        {
                            await _unitOfWork.ShopProducts.GetProductForAsync(lastSubscription.Price.GetCore());
                            if (lastSubscription.Price.Product != null)
                                type = $"{lastSubscription.Price.Product.Name} ({lastSubscription.Price.PeriodId})";
                        }
                    }


                    if(lastSubscription != null)
                    {
                        subscriptionState = $"<h2>Your {type} subscription {tense} on {lastSubscription.DateEnd?.ToString(Data.Core.Settings.DateFormatShort)}.</h2>";

                        if (tense == "ended")
                        {
                            titleSuffix = " (Resubscribe)";
                            subscriptionOptions =
                                "<h2>You can resubscribe at anytime by selecting one of the products below and completing any payments.</h2>";                    }
                        else
                        {
                            titleSuffix = " (Change Subscription)";
                            subscriptionOptions =
                                "<h2>You can change your subscription at anytime and we will change or refund the difference, based on your remaining time.</h2>";
                        }
                    }
                }

                headerText = $"<h1>Pricing{titleSuffix}</h1>"
                             + referrerMessage
                             + subscriptionState
                             + subscriptionOptions
                             + "<h2>SilkFlo offers a transparent, 3-tiered, subscription model.</h2>";

                if (lastSubscription == null)
                    headerText += Settings.FreeTrialMessage;

                return await ResubscribeView(
                    false,
                    "Contact Us",
                    new Models.Business.Client(client),
                    headerText,
                    lastSubscription,
                    referrerCode,
                    productButtonText);
            }



#if RELEASE
            var showSelectButton = false;
#else
            var showSelectButton = true;
#endif
            headerText = "<h1>Pricing</h1>"
                         + referrerMessage
                         + "<h2>SilkFlo offers a transparent, 3-tiered, subscription model.</h2>";

            if (showSelectButton)
                headerText += Settings.FreeTrialMessage;

            var viewModel = await ViewModels.Subscriptions.GetAsync(
                _unitOfWork,
                Security.Settings.GetEnvironment(),
                "Book a Demo",
                showSelectButton,
                true,
                headerText,
                referrerCode);

            return View("/Views/Home/Pricing.cshtml", viewModel);
        }
    }
}