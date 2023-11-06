using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SilkFlo.Web.Controllers.Shop
{
    public partial class SubscribeController
    {
        [HttpGet("shop/subscribe/priceId/{priceId}")]
        [HttpGet("shop/subscribe/priceId/{priceId}/referrerCode/{referrerCode}")]
        public async Task<IActionResult> ClientSubscribe(
            string priceId,
            string referrerCode = "",
            string entity = "")
        {
            var feedback = new ViewModels.Feedback();
            var base64EncodedBytes = System.Convert.FromBase64String(entity);
            var email = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            try
            {
                // Guard Clause
                if (priceId == null)
                {
                    feedback.DangerMessage("priceId parameter missing");
                    return BadRequest(feedback);
                }

                var price = await _unitOfWork.ShopPrices.GetAsync(priceId);

                // Guard Clause
                if (price == null)
                {
                    feedback.DangerMessage("Price not found");
                    return BadRequest(feedback);
                }


                await _unitOfWork.ShopProducts.GetProductForAsync(price);

                // Guard Clause
                if (price.Product == null)
                {
                    feedback.DangerMessage("Product not found");
                    return BadRequest(feedback);
                }

                await _unitOfWork.SharedPeriods.GetPeriodForAsync(price);

                // Guard Clause
                if (price.Period == null)
                {
                    feedback.DangerMessage("Period not found");
                    return BadRequest(feedback);
                }


                // Guard Clause - Check for Practice Account
                var userId = GetUserId();
                Data.Core.Domain.User user = null;
                if (userId != null)
                {
                    user = await _unitOfWork.Users.GetAsync(userId);

                    if (user == null)
                        return PageView("<h1 style=\"margin: 2rem;\" class=\"text-info\">User missing.<h1>");

                    await _unitOfWork.BusinessClients.GetClientForAsync(user);

                    if (user.Client is { IsPractice: true })
                        return PageView("<h1 style=\"margin: 2rem;\" class=\"text-info\">You cannot resubscribe with a Practice Account.<h1>");
                }

                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(price);


                var terms = await _unitOfWork.ApplicationPages.GetAsync(Data.Core.Enumerators.Page.PlatformTerms.ToString());

                var viewModel = new ViewModels.Shop.Subscribe
                {
                    Product = new Models.Shop.Product(price.Product),
                    Terms = terms?.Text,
                    ReferrerCode = referrerCode,
                    CustomerStripeId = user?.Client.StripeId
                };

                viewModel.Product.Price = new Models.Shop.Price(price);
                if (!String.IsNullOrEmpty(email))
                {
                    viewModel.Email = email;
                }


                bool addFreeTrial;
                if (user == null)
                    addFreeTrial = true;
                else
                {
                    viewModel.IsSignedIn = true;
                    viewModel.Fullname = user.Fullname;

                    await _unitOfWork.SharedCountries.GetCountryForAsync(user.Client);


                    var clientModel = new Models.Business.Client(user.Client);
                    var subscription = await clientModel.GetLastSubscriptionAsync(_unitOfWork, false);

                    if (subscription != null)
                        viewModel.Product.Refund = subscription.GetRefund();

                    addFreeTrial = subscription == null;

                    if (user.Client != null)
                    {
                        viewModel.ClientId = user.Client.Id;
                        viewModel.Name = user.Client.Name;
                        viewModel.Address1 = user.Client.Address1;
                        viewModel.Address2 = user.Client.Address2;
                        viewModel.City = user.Client.City;
                        viewModel.State = user.Client.State;
                        viewModel.PostCode = user.Client.PostCode;
                        viewModel.CountryId = user.Client.CountryId;
                        if (user.Client.Country != null)
                            viewModel.Country = new Models.Shared.Country(user.Client.Country);
                    }
                }


                if (addFreeTrial)
                {
                    var sTrialPeriod = await GetApplicationSettingsAsync(Data.Core.Enumerators.Setting.TrialPeriod);
                    int.TryParse(sTrialPeriod, out var trialDays);
                    viewModel.Product.TrialDays = trialDays;
                }


                viewModel.StripePublicKey = SilkFlo.Web.Services2.Models.PaymentManager.StripePublicKey; // Payment.Manager.StripePublicKey;
                viewModel.Countries.Add(new Models.Shared.Country());
                var countries = await _unitOfWork.SharedCountries.GetAllAsync();
                foreach (var core in countries)
                {
                    var model = new Models.Shared.Country(core)
                    {
                        DisplayText = core.Name
                    };
                    viewModel.Countries.Add(model);
                }

                //return View("/Views/Shop/Subscribe/Page.cshtml", viewModel);
                return View("/Views/Account/SignUp_1.cshtml", viewModel); //, viewModel);
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Could not load product");
                return BadRequest(feedback);
            }
        }

        [HttpGet("FreeTrial")]
        public async Task<IActionResult> FreeTrial()
        {
            ViewBag.HideNavigation = true;
            var client = await GetClientAsync(false);
            
            var terms = await _unitOfWork.ApplicationPages.GetAsync(Data.Core.Enumerators.Page.PlatformTerms.ToString());
            var viewModel = new ViewModels.Shop.Subscribe
            {
                Terms = terms?.Text,
                ShowAddress = false
            };

            if (client == null)
                return View("/Views/Shop/Subscribe/FreeTrail.cshtml", viewModel);

            if (client.IsPractice)
            {

                return View(
                    "/Views/MessagePage.cshtml",
                    new ViewModels.MessagePage
                    {
                        Title = "Free Trail",
                        Message = "Practice accounts cannot subscribe."
                    });
            }

            if (client.TypeId == Data.Core.Enumerators.ClientType.ResellerAgency45.ToString())
                return View(
                    "/Views/MessagePage.cshtml",
                    new ViewModels.MessagePage
                    {
                        Title = "Free Trail",
                        Message = "Your are a reseller agency and therefore you are automatically subscribed."
                    });

            var model = new Models.Business.Client(client);
            var subscription = await model.GetLastSubscriptionAsync(_unitOfWork, false);

            if (subscription == null)
            {
                await _unitOfWork.Users.GetAccountOwnerForAsync(client);

                viewModel.IsSignedIn = true;
                viewModel.Fullname = client.AccountOwner.Fullname;
                viewModel.ClientId = client.Id;
                viewModel.Name = client.Name;
                viewModel.Address1 = client.Address1;
                viewModel.Address2 = client.Address2;
                viewModel.City = client.City;
                viewModel.State = client.State;
                viewModel.PostCode = client.PostCode;
                viewModel.CountryId = client.CountryId;


                if (client.Country != null)
                    viewModel.Country = new Models.Shared.Country(client.Country);

                viewModel.Countries.Add(new Models.Shared.Country());
                var countries = await _unitOfWork.SharedCountries.GetAllAsync();
                foreach (var core in countries)
                {
                    var country = new Models.Shared.Country(core)
                    {
                        DisplayText = core.Name
                    };
                    viewModel.Countries.Add(country);
                }

                await _unitOfWork.SharedCountries.GetCountryForAsync(client);

                return View("/Views/Shop/Subscribe/FreeTrail.cshtml", viewModel);
            }


            var message = subscription.IsActive ? 
                "Because you are currently subscribed." :
                "Because you have been a previous subscriber you are not eligible to a free trial.";

            return View(
                "/Views/MessagePage.cshtml",
                new ViewModels.MessagePage
                {
                    Title = "Free Trail Not Available",
                    Message = message
                });
        }

        [HttpGet("Welcome")]
        [Authorize]
        public async Task<IActionResult> Welcome()
        {
            ViewBag.HideNavigation = true;
            var client = await GetClientAsync();

            // Guard Clause
            if (client == null)
                return Redirect("/account/Signin");

            var model = new Models.Business.Client(client);
            var subscription = await model.GetLastSubscriptionAsync(_unitOfWork);


            // Guard Clause
            if(subscription == null)
                return Redirect("/account/Signin");


            var viewModel = new ViewModels.MessagePage
            {
                Title = "Welcome to " + Data.Core.Settings.ApplicationName,
                Message = $"You are now subscribed to {Data.Core.Settings.ApplicationName}.</br>If you are new with us, please check your email for an email confirmation message."
            };

            return View("../MessagePage", viewModel);
        }
    }
}