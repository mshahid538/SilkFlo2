using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkFlo.Data.Core;
using SilkFlo.Security.API.ReCaptcha.Interfaces;
using Stripe;

namespace SilkFlo.Web.Controllers.Shop
{
    public partial class SubscribeController : AbstractAPI
    {
        private readonly ISignUpService _signUpService;
        public SubscribeController(IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorisation,
            ISignUpService signUpService) : base(unitOfWork, viewToString, authorisation)
        {
            _signUpService = signUpService;
        }

        [HttpGet("Subscribe/Continue")]
        [Authorize]
        public async Task<IActionResult> Continue(string clientId)
        {
            return Ok("Continue");
        }


        [HttpGet("/api/shop/Subscribe/GetCoupon")]
        public async Task<IActionResult> GetCoupon()
        {
            var feedback = new ViewModels.Feedback
            {
                NamePrefix = "Account.SignUp."
            };

            var couponName = HttpContext.Request.Cookies["CouponName"];
            var clientId = HttpContext.Request.Cookies["clientId"];
            var priceId = HttpContext.Request.Cookies["PriceId"];

            // Guard Clause
            if (string.IsNullOrWhiteSpace(couponName))
            {
                feedback.Message = "Please supply a coupon code.";
                return BadRequest(feedback);
            }


            // Guard Clause
            if (string.IsNullOrWhiteSpace(priceId))
            {
                feedback.Message = "priceId missing.";
                return BadRequest(feedback);
            }


            var price = await _unitOfWork.ShopPrices.GetAsync(priceId);

            // Guard Clause
            if (price == null)
            {
                feedback.Message = "price missing.";
                return BadRequest(feedback);
            }


            var coupon = await _unitOfWork.ShopCoupons.GetByNameAsync(couponName);

            // Guard Clause
            if (coupon == null)
            {
                feedback.Message = "Cannot find coupon."; 
                return BadRequest(feedback);
            }

            var model = new Models.Shop.Coupon(coupon);
            var client = await _unitOfWork.BusinessClients.GetAsync(clientId);
            if (client != null)
            {
                var clientModel = new Models.Business.Client(client);
                var subscription = await clientModel.GetLastSubscriptionAsync(_unitOfWork, true);

                model.CheckValid(subscription);
            }
            else
                model.CheckValid();

            if (model.IsValid)
            {
                model.GetDiscount(price.Amount);
                return Ok(model);
            }


            feedback.Message = model.InValidMessage;
            return BadRequest(feedback);
        }




        [HttpPost("api/shop/Subscribe/ClientDetails/Post")]
        public async Task<IActionResult> PostClientAccountDetails([FromBody] Services.Models.Account.SignUp model)
        {
            var feedback = new ViewModels.Feedback
            {
                NamePrefix = "Account.SignUp."
            };

            try
            {
                // Guard Clause
                if (model == null)
                {
                    feedback.DangerMessage("Error: Model missing.");
                    return BadRequest(feedback);
                }

                if (model.IsMsUser)
                {
                    model.Password = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{model.Email}-SILKFLOUserSecretKey-{model.IsMsUser}"));
                }

                // Check the ReCaptcha Token
                var response = await _signUpService.SignUp(model.ReCaptchaToken);

                // Guard Clause
                if (!response.Success)
                {
                    feedback.IsValid = false;
                    feedback.DangerMessage(response.Error );

                    return BadRequest(feedback);
                }


                //feedback = GetFeedback(ModelState, feedback);
                model.CompareEmail(feedback);

                if (!model.TermsAgreed)
                {
                    feedback.Elements.Add("TermsAgreed", "");
                    feedback.IsValid = false;
                }



                var client = await _unitOfWork.BusinessClients.GetByNameAsync(model.Name);
                if(client != null)
                {
                    feedback.Elements.Add("Name", "This company name is already in use.");
                    feedback.IsValid = false;
                }


                if (!feedback.IsValid)
                    return BadRequest(feedback);


                // Validate email
                var user = await _unitOfWork.Users.GetByEmailAsync(model.Email);

                if (user == null)
                {
                    model.Website = model.Website.Replace("https://www.", "", StringComparison.OrdinalIgnoreCase);
                    model.Website = model.Website.Replace("http://www.", "", StringComparison.OrdinalIgnoreCase);

                    if (model.Email.IndexOf("@", StringComparison.Ordinal) == -1)
                    {
                        feedback.IsValid = false;
                        feedback.Elements.Add("Email", "The email address is invalid.");
                    }
                    else
                    {
                        var emailParts = model.Email.Split("@");
                        if (emailParts[1].ToLower() != model.Website.ToLower())
                        {
                            feedback.IsValid = false;
                            feedback.Elements.Add("Email", "The email address does not match the websites URL.");
                            feedback.Elements.Add("Website", "The website address does not match the email address.");
                        }
                    }

                    if (feedback.IsValid)
                    {
                        var message = await Email.Service.ValidateEmailAsync(model.Email, true);
                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            feedback.Elements.Add("Email", message);
                            feedback.IsValid = false;
                        }
                    }
                }
                else
                {
                    await _unitOfWork.BusinessClients.GetForAccountOwnerAsync(user);
                    if (user.AccountOwners.Any())
                    {
                        client = user.AccountOwners[0];
                        if (client.TypeId == Enumerators.ClientType.ResellerAgency45.ToString())
                        {
                            feedback.Message = "<span class=\"text-info\">As a reseller your subscription is ongoing.<br/>" +
                                               "Visit your <a href=\"/Settings/agency/tenants\">tenants</a> page to add a new tenant.<br/>" +
                                               "<span style=\"font-size: 0.8rem;\">(By clicking the link, you will first be redirected to the sign in page)</span>" + 
                                               "</span>";
                            return BadRequest(feedback);
                        }

                        var clientModel = new Models.Business.Client(client);
                        var subscription = await clientModel.GetLastSubscriptionAsync(_unitOfWork);
                        if (subscription != null)
                        {
                            feedback.Message = $"<span class=\"text-success\">{client.Name} already has an active subscription. Please <a style=\"color: black\" href=\"/account/signin\">Sign&nbsp;in</a> or <a style=\"color: black\" href=\"/Account/ForgotPassword\">Reset&nbsp;password</a>.</span>";
                            return BadRequest(feedback);
                        }
                    }

                    feedback.Elements.Add(
                        "Email",
                        "There is already an account with this email address. <a style=\"color: black\" href=\"/account/signin\">Sign&nbsp;in</a> or <a style=\"color: black\" href=\"/Account/ForgotPassword\">Reset&nbsp;password</a>.");
                    feedback.IsValid = false;
                }



                if (model.Password.Length < 8)
                {
                    feedback.Elements.Add("Password", "Password must be at least 8 characters in length.");
                    feedback.IsValid = false;
                }





                if (!feedback.IsValid)
                    return BadRequest(feedback);


                //Save the content
                var clientSilkFlo = await _unitOfWork.BusinessClients.GetByNameAsync(Data.Core.Settings.ApplicationName);

                var tenant = await Models.Business.Client.CreateAsync(
                    _unitOfWork,
                    "",
                    model.Name,
                    model.FirstName,
                    model.LastName,
                    model.Email,
                    model.Password,
                    model.Address1,
                    model.Address2,
                    model.City,
                    model.State,
                    model.PostCode,
                    0,
                    new Models.Business.Client(clientSilkFlo),
                    false);

                if (tenant == null)
                    throw new ArgumentNullException(nameof(tenant));

                if (tenant.AccountOwner == null)
                    throw new ArgumentNullException(nameof(tenant.AccountOwner));





                // Send confirm email message
                tenant.AccountOwner.EmailNew = tenant.AccountOwner.Email;
                await SendEmailConfirmationMessageAsync(tenant.AccountOwner.GetCore());



                // Send welcome email
                var callbackUrl =
                    Url.CompleteSignUp(
                        tenant.Id,
                        Request.Scheme);

                await SendWelcomeEmailAsync(tenant.GetCore());


                // Save customer to Stripe
                //var customer = await Payment.Manager.SaveAsync(tenant.GetCore());
                var customer = await SilkFlo.Web.Services2.Models.PaymentManager.SaveAsync(tenant.GetCore());

                if (customer == null)
                {
                    const string message = "Cannot create a Stripe customer.";
                    Log(message);
                    feedback.DangerMessage(message);
                    return BadRequest(feedback);
                }


                tenant.StripeId = customer.Id;

                await _unitOfWork.CompleteAsync();

                #region Subscription against priceId
                var clientforSubscription = await _unitOfWork.BusinessClients.GetAsync(tenant.Id);
                var priceforSubscription = await _unitOfWork.ShopPrices.GetAsync(model.PriceId);

                Data.Core.Domain.Shop.Coupon coupon = null;
                //if (!string.IsNullOrWhiteSpace(couponName))
                //{
                //    coupon = await _unitOfWork.ShopCoupons.GetByNameAsync(couponName);

                //    if (coupon == null)
                //        return ViewDanger($"Coupon with name {couponName} missing.");
                //}

                var sTrialPeriod = await GetApplicationSettingsAsync(Enumerators.Setting.TrialPeriod);
                int.TryParse(sTrialPeriod, out var trialPeriod);

                await Models.Shop.Subscription.CreateAsync(
                    _unitOfWork,
                    clientforSubscription,
                    priceforSubscription,
                    trialPeriod,
                    coupon);
                #endregion


                return Ok(true); // tenant.Id);
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Error: Could not save client account details");
                return BadRequest(feedback);
            }
        }


        [HttpPost("/api/shop/Subscribe/TermsAgreedValidate")]
        public IActionResult ValidateTermsAgreedValidate([FromBody] bool termsAgreed)
        {
            var feedback = new ViewModels.Feedback
            {
                NamePrefix = "Account.SignUp."
            };


            try
            {
                if (!termsAgreed)
                {
                    feedback.Elements.Add("TermsAgreed", "");
                    feedback.IsValid = false;
                }

                if (!feedback.IsValid)
                    return BadRequest(feedback);

                return Ok();
            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Error: could not validate terms agreed validate");
                return BadRequest(feedback);
            }
        }


        [HttpPost("/api/shop/Subscribe/SaveBillingAddress")]
        public async Task<IActionResult> SaveBillingAddress([FromBody] Services.Models.Account.BillingAddress model)
        {
            var feedback = new ViewModels.Feedback
            {
                NamePrefix = "Account.SignUp."
            };



            try
            {
                if (model == null)
                {
                    feedback.DangerMessage("Model missing");
                    return BadRequest(feedback);
                }

                feedback = GetFeedback(ModelState, feedback);


                if (!feedback.IsValid)
                {
                    foreach (var kv in feedback.Elements)
                        feedback.Message += kv.Value + "</br>";

                    if (!string.IsNullOrWhiteSpace(feedback.Message))
                        feedback.Message = feedback.Message.Substring(0, feedback.Message.Length - 5);

                    return BadRequest(feedback);
                }




                var core = new Data.Core.Domain.Business.Client
                {
                    Id = model.ClientId,
                    Name = model.Name
                };



                var message = await _unitOfWork.IsUniqueAsync(core);// Data.Persistence.UnitOfWork.IsUniqueAsync(core);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    feedback.DangerMessage(message);
                    return BadRequest(feedback);
                }

                core = await _unitOfWork.BusinessClients.GetAsync(model.ClientId);

                if (core == null)
                {
                    feedback.DangerMessage("Cannot find client");
                    return BadRequest(feedback);
                }


                core.Name = model.Name;
                core.Address1 = model.Address1 ?? "";
                core.Address2 = model.Address2 ?? "";
                core.City = model.City ?? "";
                core.State = model.State ?? "";
                core.PostCode = model.PostCode ?? "";
                core.CountryId = model.CountryId;
                if (core.IsSaved)
                    return Ok();


                try
                {
                    if (!string.IsNullOrWhiteSpace(core.StripeId))
                        await SilkFlo.Web.Services2.Models.PaymentManager.SaveAsync(core); //await Payment.Manager.SaveAsync(core);

                    await _unitOfWork.AddAsync(core);
                    await _unitOfWork.CompleteAsync();

                    if (!string.IsNullOrWhiteSpace(core.StripeId))
                    {
                        await SilkFlo.Web.Services2.Models.PaymentManager.SaveAsync(core); //Payment.Manager.SaveAsync(core);
                    }
                }
                catch (Exception ex)
                {
                    feedback.DangerMessage(ex.Message);
                    return BadRequest(feedback);
                }

                return Ok();

            }
            catch (Exception ex)
            {
                Log(ex);
                feedback.DangerMessage("Error: Saving billing address");
                return BadRequest(feedback);
            }
        }




        [HttpPost("api/shop/Subscribe/FreeTrial/Post")]
        public async Task<IActionResult> PostFreeTrial([FromBody] ViewModels.Shop.FreeTrial model)
        {
            var feedback = new ViewModels.Feedback
            {
                NamePrefix = "Account.SignUp."
            };


            try
            {
                // Guard Clause
                if (model == null)
                {
                    feedback.DangerMessage("Error: Model missing.");
                    return BadRequest(feedback);
                }

                // Check the ReCaptcha Token
                var response = await _signUpService.SignUp(model.ReCaptchaToken);

                // Guard Clause - ReCaptcha Token
                if (!response.Success)
                {
                    feedback.IsValid = false;
                    feedback.DangerMessage(response.Error);

                    return BadRequest(feedback);
                }




                Data.Core.Domain.Business.Client client;
                Data.Core.Domain.User accountOwner = null;

                // Check for new client
                if (string.IsNullOrWhiteSpace(model.ClientId))
                {
                    // New client
                    feedback = GetFeedback(ModelState, feedback);
                    model.CompareEmail(feedback);


                    if (!model.TermsAgreed)
                    {
                        feedback.Elements.Add("TermsAgreed", "");
                        feedback.IsValid = false;
                    }

                    // Guard Clause
                    if (!feedback.IsValid)
                        return BadRequest(feedback);

                    client = new Data.Core.Domain.Business.Client
                    {
                        Name = model.Name,
                        Website = model.Website,
                        TermsOfUse = model.TermsAgreed,
                        ReceiveMarketing = model.ReceiveMarketing,
                        IsActive = true,
                        TypeId = Enumerators.ClientType.Client39.ToString(),
                        CurrencyId = "gbp",
                        LanguageId = "en-gb"
                    };


                    // Check unique client
                    var clientModel = new Models.Business.Client(client);
                    await clientModel.CheckUniqueAsync(_unitOfWork, feedback);

                    // Guard Clause
                    if (!feedback.IsValid)
                        return BadRequest(feedback);

                    accountOwner = new Data.Core.Domain.User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PasswordHash = _unitOfWork.GeneratePasswordHash(model.Password),
                    };


                    // Check unique user
                    var accountOwnerModel = new Models.User(accountOwner);
                    await accountOwnerModel.CheckUniqueAsync(_unitOfWork, feedback);


                    // Guard Clause
                    if (!feedback.IsValid)
                        return BadRequest(feedback);

                    var userRole = new Data.Core.Domain.UserRole
                    {
                        User = accountOwner,
                        RoleId = ((int) Enumerators.Role.AccountOwner).ToString()
                    };


                    // Check unique userRole
                    var userRoleModel = new Models.UserRole(userRole);
                    await userRoleModel.CheckUniqueAsync(_unitOfWork, feedback);


                    // Guard Clause
                    if (!feedback.IsValid)
                        return BadRequest(feedback);


                    client.AccountOwner = accountOwner;
                    accountOwner.Client = client;

                    await _unitOfWork.AddAsync(userRole);
                    await _unitOfWork.AddAsync(client);
                    await _unitOfWork.AddAsync(accountOwner);
                }
                else
                {
                    // Potentially an existing client
                    client = await _unitOfWork.BusinessClients.GetAsync(model.ClientId);

                    // Guard Clause
                    if (client == null)
                    {
                        feedback.DangerMessage($"Error: There are no clients with clientId {model.ClientId}.");
                        feedback.IsValid = false;
                    }


                    if (!model.TermsAgreed)
                    {
                        feedback.Elements.Add("TermsAgreed", "");
                        feedback.IsValid = false;
                    }

                    // Guard Clause
                    if (!feedback.IsValid)
                        return BadRequest(feedback);

                    if (client != null)
                    {
                        client.TermsOfUse = model.TermsAgreed;
                        client.ReceiveMarketing = model.ReceiveMarketing;

                        await _unitOfWork.Users.GetAccountOwnerForAsync(client);
                        accountOwner = client.AccountOwner;

                        var clientModel = new Models.Business.Client(client);
                        var subscription = await clientModel.GetLastSubscriptionAsync(_unitOfWork);
                        if (subscription != null)
                        {
                            feedback.Message =
                                "You have already previously subscribed, therefore you are not eligible to a free trial.";
                            return BadRequest(feedback);
                        }
                    }
                }


                var id = Enumerators.Setting.TrialPeriod.ToString();
                var setting = await _unitOfWork.ApplicationSettings.GetAsync(id);

                var trialDays = 30;
                if (setting != null && !string.IsNullOrWhiteSpace(setting.Value))
                {
                    int.TryParse(setting.Value, out trialDays);
                }

                var now = DateTime.Now.Date;
                var dateEnd = now.AddDays(trialDays);
                var subscriptionNew = new Models.Shop.Subscription
                {
                    Tenant = new Models.Business.Client(client),
                    DateStart = now,
                    DateEnd = dateEnd
                };


                await Insert.PracticeData.CreatePracticeAccountAsync(
                    client,
                    _unitOfWork,
                    false,
                    accountOwner);



                // Send welcome email
                if (client != null
                    && client.AccountOwner != null)
                    await SendEmailConfirmationMessageAsync(
                        client.AccountOwner);

                await _unitOfWork.AddAsync(subscriptionNew.GetCore());
                await _unitOfWork.CompleteAsync();


                var signIn = new Services.Models.Account.SignIn
                {
                    Email = client.AccountOwner.Email,
                    RememberMe = true
                };

                await _unitOfWork.UserRoles.GetForUserAsync(client.AccountOwner);
                await _unitOfWork.Roles.GetRoleForAsync(client.AccountOwner.UserRoles);

                await SignInAsync(client.AccountOwner, signIn, "", false);

                return Ok();
            }
            catch (Exception e)
            {
                Log(e);
                feedback.Message =
                    "Error creating subscription.";
                return BadRequest(feedback);
            }
        }




        [HttpGet("/api/shop/CreateSetupIntent/ClientId/{clientId}")]
        public async Task<IActionResult> CreateSetupIntent(string clientId)
        {
            var feedback = new ViewModels.Feedback();

            if (string.IsNullOrWhiteSpace(clientId))
            {
                feedback.DangerMessage("clientId is null");
                return BadRequest(feedback);

            }

            var client = await _unitOfWork.BusinessClients.GetAsync(clientId);

            if (client == null)
                return Ok();

            var setupIntent = await SilkFlo.Web.Services2.Models.PaymentManager.CreateSetupIntent(client); //Payment.Manager.CreateSetupIntent(client);

            if (setupIntent == null)
            {
                feedback.DangerMessage("Stripe SetupIntent failed");
                return BadRequest(feedback);
            }

            var clientSecret = setupIntent.ClientSecret;


            await _unitOfWork.AddAsync(client);
            await _unitOfWork.CompleteAsync();

            return Ok(clientSecret);
        }



        //[HttpPost("api/stripe/GetCustomer")]
        //[Authorize]
        //public async Task<ActionResult<ViewModels.Stripe.CreateCustomerResponse>> GetCustomer()
        //{
        //    var userId = GetUserId();
        //    var user = await _unitOfWork.Users.GetAsync(userId);
        //    await _unitOfWork.BusinessClients.GetClientForAsync(user);
        //    user.Client.AccountOwnerId = user.Id;
        //    user.Client.AccountOwner = user;

        //    Stripe.Customer customer;

        //    if (string.IsNullOrWhiteSpace(user.Client.StripeId))
        //        customer = await Payment.Manager.SaveAsync(user.Client);
        //    else
        //        customer = await Payment.Manager.GetCustomerAsync(user.Client.StripeId);

        //    if (!user.Client.IsSaved)
        //    {
        //        await _unitOfWork.AddAsync(user.Client);
        //        await _unitOfWork.CompleteAsync();
        //    }

        //    if(customer != null)
        //        HttpContext.Response.Cookies.Append("customerId", customer.Id);

        //    return new ViewModels.Stripe.CreateCustomerResponse()
        //    {
        //        Customer = customer,
        //    };
        //}

        [HttpPost("api/shop/CreateSubscription")]
        public async Task<ActionResult> CreateSubscription(
            [FromBody]  ViewModels.Stripe.CreateSubscriptionRequest subscriptionRequest)
        {
            if (subscriptionRequest == null)
                return BadRequest("Body missing");


            if (string.IsNullOrWhiteSpace(subscriptionRequest.PriceId))
                return BadRequest("PriceId missing");

            if (string.IsNullOrWhiteSpace(subscriptionRequest.CustomerId))
                return BadRequest("CustomerId missing");


            // Get the trial Days
            int? trialDay = null;

            // Get trials from coupon
            if (string.IsNullOrWhiteSpace(subscriptionRequest.CouponName))
            {
                var coupon = await _unitOfWork.ShopCoupons.GetByNameAsync(subscriptionRequest.CouponName);
                if (coupon?.TrialDay != null)
                    trialDay = coupon.TrialDay;
            }

            if (trialDay == null)
            {
                var sTrialPeriod = await GetApplicationSettingsAsync(Enumerators.Setting.TrialPeriod);
                int.TryParse(sTrialPeriod, out var trialPeriod);
                trialDay = trialPeriod;
            }


            var trialEnd = DateTime.Now.Date.AddDays(trialDay??0);

            /*
             * Three days before the trial period is up,
             * a customer.subscription.trial_will_end event is sent to your webhook endpoint.
             */


            var subscription = await SilkFlo.Web.Services2.Models.PaymentManager.CreateSubscription( //Payment.Manager.CreateSubscription(
                subscriptionRequest.PriceId,
                subscriptionRequest.CustomerId,
                trialEnd);

            //return Json(new { clientSecret = subscription.LatestInvoice.PaymentIntent.ClientSecret });

            return Ok();
        }


        [HttpGet("shop/SubscriptionComplete/clientId/{clientId}/priceId/{priceId}")]
        [HttpGet("shop/SubscriptionComplete/clientId/{clientId}/priceId/{priceId}/coupon/{couponName}")]
        public async Task<IActionResult> SubscriptionComplete(
            string clientId,
            string priceId,
            string couponName = "")
        {
            var client = await _unitOfWork.BusinessClients.GetAsync(clientId);

            if (client == null)
                return ViewDanger($"Client with id {clientId} missing.");

            var price = await _unitOfWork.ShopPrices.GetAsync(priceId);

            if (price == null)
                return ViewDanger($"Price with id {priceId} missing.");

            Data.Core.Domain.Shop.Coupon coupon = null;
            if (!string.IsNullOrWhiteSpace(couponName))
            {
                coupon = await _unitOfWork.ShopCoupons.GetByNameAsync(couponName);

                if (coupon == null)
                    return ViewDanger($"Coupon with name {couponName} missing.");
            }

            var sTrialPeriod = await GetApplicationSettingsAsync(Enumerators.Setting.TrialPeriod);
            int.TryParse(sTrialPeriod, out var trialPeriod);

            await Models.Shop.Subscription.CreateAsync(
                _unitOfWork,
                client,
                price,
                trialPeriod,
                coupon);

            return View("/Views/Shop/Subscribe/Complete.cshtml");
        }




        [Route("stripe/WebHook")]
        public async Task<IActionResult> StripeWebHooks()
        {
            // https://stripe.com/docs/webhooks
            // https://stripe.com/docs/webhooks/quickstart
            // https://stripe.com/docs/stripe-cli
            // https://stripe.com/docs/api/events/types
            // https://stripe.com/docs/development/checklist
            // https://stripe.com/docs/webhooks/go-live

            // Copy stripe.exe to c:\
            // Open cmd window and type:
            //   cd .. (back to c:\)
            //   stripe listen --forward-to https://localhost:5001/webhook
            //
            // Open a second cmd window and type:
            //   cd .. (back to c:\)
            //   stripe trigger payment_intent.succeeded
            // The following code should be executed.
            //
            // Type the following to see other commands that can be ran:
            //   stripe trigger --help
            //
            // Test a trigger. For example: invoice.paid by using the following:
            //   stripe trigger invoice.paid
            try
            {
                // Get the object
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                var signatureHeader = Request.Headers["Stripe-Signature"];
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    signatureHeader, SilkFlo.Web.Services2.Models.PaymentManager.WebHookSecret); //Payment.Manager.WebHookSecret);


                // Guard Clause
                if (stripeEvent.Data.Object == null)
                    return Ok();


                // Guard Clause - Check environment
                var environment = Security.Settings.GetEnvironment();
                if (stripeEvent.Livemode
                    && environment != Security.Environment.Production)
                {
                    _unitOfWork.Log(
                        $"The invoice is in live mode, but the environment is {environment.ToString()} mode.");
                    return Ok();
                }

                // Guard Clause - Check already processed
                var webHook =
                    await _unitOfWork.WebHookLogs.SingleOrDefaultAsync(x =>
                        x.SourceId == Enumerators.WebHookSource.Stripe.ToString() && x.KeyId == stripeEvent.Id);

                // Guard Clause - Already processed
                if (webHook != null)
                    return Ok();


                // Do the business
                bool isProcessed = false;
                switch (stripeEvent.Type)
                {

                    case Events.InvoicePaid:
                    {
                        isProcessed = await ProcessInvoicePaid(stripeEvent);
                        break;
                    }
                    case Events.InvoicePaymentActionRequired:
                    {
                        //ToDo: Send Invoice payment action required message
                        break;
                    }
                    case Events.InvoicePaymentFailed:
                    {
                        //ToDo: Send Invoice payment failed message
                        break;
                    }
                    default:
                        Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                        break;
                }

                webHook = new Data.Core.Domain.WebHookLog
                {
                    SourceId = Enumerators.WebHookSource.Stripe.ToString(),
                    KeyId = stripeEvent.Id
                };

                await _unitOfWork.AddAsync(webHook);

                await _unitOfWork.CompleteAsync();

                if (isProcessed)
                    return Ok();

                return BadRequest();
            }
            catch (StripeException e)
            {
                _unitOfWork.Log("Stripe: " + e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                _unitOfWork.Log(e);
                return StatusCode(500);
            }
        }

        private async Task<bool> ProcessInvoicePaid(Stripe.Event stripeEvent)
        {
            // https://stripe.com/docs/api/invoices/object
            // https://stripe.com/docs/api/invoices/line_item

            // Guard Clause
            if (stripeEvent == null)
                throw new ArgumentNullException(nameof(stripeEvent));


            // Guard Clause
            if (stripeEvent.Type != Events.InvoicePaid)
                return true;

            try
            {
                // Guard Clause - Get the invoice with guard clause
                if (stripeEvent.Data.Object is not Invoice invoice)
                {
                    _unitOfWork.Log(
                        "The Stripe event with of type invoice, but the data was not of type invoice.");
                    return true;
                }


                // Guard Clause - AmountRemaining > 0
                if (invoice.AmountRemaining > 0)
                {
                    // ToDo: Send invoice partially paid message
                    _unitOfWork.Log($"Invoice {invoice.Number}, with id {invoice.Id} was not completely paid. The amount standing is {invoice.AmountRemaining} {invoice.Currency}.");
                    return true;
                }


                // Get the client paying the invoice
                var client =
                    await _unitOfWork.BusinessClients.SingleOrDefaultAsync(
                        x => x.StripeId == invoice.CustomerId);


                // Guard Clause
                if (client == null)
                {
                    _unitOfWork.Log($"Could not find client with id {invoice.CustomerId} from invoice {invoice.Number}, with id {invoice.Id}");
                    return true;
                }



                // Get the agency and tenant
                var agencyId = "";
                var tenantId = "";
                var agencyTypeId = "";
                if (client.TypeId == Enumerators.ClientType.ResellerAgency45.ToString())
                {
                    agencyId = client.Id;
                    agencyTypeId = client.TypeId;
                }
                else
                {
                    tenantId = client.Id;

                    // Get agencyTypeId
                    var model = new Models.Business.Client(client);
                    var lastSubscription = await model.GetLastSubscriptionAsync(_unitOfWork, false);
                    if (lastSubscription != null
                        && !string.IsNullOrWhiteSpace(lastSubscription.AgencyId) )
                        agencyTypeId = lastSubscription.AgencyTypeId;
                }





                // Create subscription
                var isCreated = false;

                var subscriptions = new List<Data.Core.Domain.Shop.Subscription>();

                foreach (var lineItem in invoice.Lines.Data)
                {
                    if(lineItem.Price == null)
                        continue;

                    if (lineItem.Period == null)
                        continue;

                    if (string.IsNullOrWhiteSpace(lineItem.Subscription))
                        continue;


                    isCreated = true;
                    var subscriptionId = lineItem.Subscription;
                    var priceId = lineItem.Price.Id;
                    var dateStart = lineItem.Period.Start.Date;
                    var dateEnd = lineItem.Period.End.Date;

                    var subscription = new Data.Core.Domain.Shop.Subscription
                    {
                        TenantId = tenantId,
                        AgencyId = agencyId,
                        DateStart = dateStart,
                        DateEnd = dateEnd,
                        PriceId = priceId,
                        AgencyTypeId = agencyTypeId,
                        Amount = invoice.AmountPaid,
                        InvoiceId = invoice.Id,
                        InvoiceUrl = invoice.InvoicePdf,
                        InvoiceNumber = invoice.Number
                    };


                    await _unitOfWork.AddAsync(subscription);
                    subscription.Id = subscriptionId;

                    subscriptions.Add(subscription);
                }


                // Guard Clause
                if (!isCreated)
                {
                    _unitOfWork.Log($"No subscriptions where created for invoice {invoice.Number}, with id {invoice.Id}.");
                    return true;
                }

                //ToDo: Send Invoice paid thank you message
                if (agencyTypeId != Enumerators.ClientType.ResellerAgency45.ToString())
                {
                    foreach (var subscription in subscriptions)
                    {
                        await _unitOfWork.Users.GetAccountOwnerForAsync(client);

                        var dateStart = subscription.DateStart.ToString(Data.Core.Settings.DateFormatShort);
                        var dateEnd = subscription.DateEnd?.ToString(Data.Core.Settings.DateFormatShort);

                        Email.BookMark[] bookmarks =
                        {
                            new("FIRSTNAME", client.AccountOwner.FirstName),
                            new("DATE_START",  dateStart),
                            new("DATE_END", dateEnd),
                            new Email.BookmarkLink("PATH", subscription.InvoiceUrl, "")
                        };

                        await Email.Service.SendAsync(
                            $"{Data.Core.Settings.ApplicationName} - Invoice Paid for period {dateStart} - {dateEnd}",
                            Email.Template.InvoicePaid,
                            new Email.MailBox(Data.Core.Settings.ApplicationName, "hello@silkflo.com"),
                            new Email.MailBox(client.AccountOwner.Fullname, client.AccountOwner.Email),
                            bookmarks);
                    }

                }

                return true;
            }
            catch (StripeException e)
            {
                _unitOfWork.Log("Stripe: " + e.Message);
                return false;
            }
            catch (Exception e)
            {
                _unitOfWork.Log(e);
                return true;
            }
        }
    }
}