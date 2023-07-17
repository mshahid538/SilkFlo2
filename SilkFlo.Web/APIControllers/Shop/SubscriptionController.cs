using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SilkFlo.Email;


namespace SilkFlo.Web.Controllers.Shop
{
    public class SubscriptionController : AbstractAPI
    {
        public SubscriptionController(Data.Core.IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/api/shop/subscription/GetModal/clientId/{clientId}")]
        [HttpGet("/api/shop/subscription/GetModal")]
        public async Task<IActionResult> Get(string clientId = "")
        {
            // Check Authorization
            if (!(await AuthorizeAsync(Policy.ManageAgencySettings)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();


            Data.Core.Domain.Business.Client client;
            if (string.IsNullOrWhiteSpace(clientId))
                client = await GetClientAsync();
            else
                client = await _unitOfWork.BusinessClients.GetAsync(clientId);

            await _unitOfWork.Users.GetAccountOwnerForAsync(client);

            if (client == null)
                return NegativeFeedback();


            await _unitOfWork.ShopSubscriptions.GetForTenantAsync(client);
            await _unitOfWork.ShopPrices.GetPriceForAsync(client.TenantSubscriptions);
            foreach (var subscription in client.TenantSubscriptions)
            {
                await _unitOfWork.ShopProducts.GetProductForAsync(subscription.Price);
                await _unitOfWork.SharedPeriods.GetPeriodForAsync(subscription.Price);
            }

            var model = new Models.Business.Client(client);


            var html = await _viewToString.PartialAsync(
                "Shared/Shop/Subscription/_Summary.cshtml",
                model);
            
            return Content(html);
        }



        [HttpPut("/api/Shop/Subscription/SendCancellationMessage")]
        public async Task<IActionResult> SendCancellationMessage(string clientId)
        {
            var feedback = new ViewModels.Feedback();

            
            // Guard Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageAgencySettings)).Succeeded)
            {
                feedback.DangerMessage("Unauthorised");
                return BadRequest(feedback);
            }

            // Guard Clause
            if (string.IsNullOrWhiteSpace(clientId))
            {
                feedback.DangerMessage("clientId missing");
                return BadRequest(feedback);
            }


            var client = await _unitOfWork.BusinessClients.GetAsync(clientId);

            // Guard Clause
            if (client == null)
            {
                feedback.DangerMessage($"Client with id {clientId} missing.");
                return BadRequest(feedback);
            }


            if (client.TypeId != Data.Core.Enumerators.ClientType.Client39.ToString())
            {
                feedback.DangerMessage($"Client is an agency.");
                return BadRequest(feedback);
            }


            // Get the current and future subscriptions.
            // We do this because the client could have a trial and also have a future paid for subscription
            var now = DateTime.Now.Date;
            await _unitOfWork.ShopSubscriptions.GetForTenantAsync(client);
            var subscriptions = Models.Shop.Subscription.Create(client.TenantSubscriptions);
            subscriptions = subscriptions
                .Where(x => (x.DateEnd?? now) >= now 
                            && !x.IsCancelled 
                            && !string.IsNullOrWhiteSpace(x.PriceId)).ToList();



            // Guard Clause
            if (!subscriptions.Any())
            {
                feedback.DangerMessage("Subscription has already been cancelled");
                return BadRequest(feedback);
            }


            await _unitOfWork.Users.GetAccountOwnerForAsync(client);
            if (client.AccountOwner == null)
            {
                feedback.DangerMessage("Account owner missing. Subscription process cancelled");
                return BadRequest(feedback);
            }








            var cancelToken = Guid.NewGuid().ToString();
            var agencyIds = new ListExtended<string>();
            foreach (var subscription in subscriptions)
            {
                subscription.CancelToken = cancelToken;
                await _unitOfWork.AddAsync(subscription.GetCore());



                if (string.IsNullOrWhiteSpace(subscription.AgencyId))
                    continue;

                var agencyId = subscription.AgencyId;
                if (agencyIds.All(x => x != agencyId))
                    agencyIds.Add(agencyId);
            }

            await _unitOfWork.CompleteAsync();

            BookMark[] bookmarks =
            {
                new ("FIRSTNAME", client.AccountOwner.FirstName),
                new BookmarkLink (
                    "PATH",
                    $"/Shop/Subscription/Cancel/Token/{cancelToken}/userId/{client.AccountOwner.Id}",
                    "Click to cancel")
            };

            // Send cancellation message
            await Service.SendAsync(
                "Cancel Subscription",
                Template.CancelSubscription,
                new MailBox(Data.Core.Settings.ApplicationName,
                    Service.ApplicationEmailAddress),
                new MailBox(client.AccountOwner.Fullname, client.AccountOwner.Email),
                bookmarks);


            // Send cancellation message to agencies
            if (!agencyIds.Any())
                return Ok();


            foreach (var agencyId in agencyIds)
            {
                var agency = await _unitOfWork.BusinessClients.GetAsync(agencyId);

                // Guard Clause
                if (agency == null)
                    continue;

                await _unitOfWork.Users.GetAccountOwnerForAsync(agency);

                // Guard Clause
                if (agency.AccountOwner == null)
                    continue;


                BookMark[] bookmarks2 =
                {
                    new("FIRSTNAME", agency.AccountOwner.FirstName),
                    new("COMPANYNAME", client.Name),
                };

                // Send message
                await Service.SendAsync(
                    "Client Cancelled Subscription",
                    Template.CancelSubscriptionAgency,
                    new MailBox(Data.Core.Settings.ApplicationName,
                        Service.ApplicationEmailAddress),
                    new MailBox(agency.AccountOwner.Fullname, agency.AccountOwner.Email),
                    bookmarks2);
            }


            return Ok();
        }


        //ToDo: Test Shop/Subscription/Cancel/Token
        [Route("/Shop/Subscription/Cancel/Token/{token}/userId/{accountOwnerId}")]
        [Authorize]
        public async Task<IActionResult> Cancel(
            string token,
            string accountOwnerId)
        {
            // Guard Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded
                && !(await AuthorizeAsync(Policy.ManageAgencySettings)).Succeeded)
            {
                return NegativeFeedback("You are not authorised to cancel subscription.");
            }




            var clients = await _unitOfWork.BusinessClients.FindAsync(x => x.AccountOwnerId == accountOwnerId);
            if (clients == null)
                return Ok();

            var subscriptions = new List<Data.Core.Domain.Shop.Subscription>();
            foreach (var client in clients)
            {
                if (client.TypeId != Data.Core.Enumerators.ClientType.Client39.ToString())
                    continue;

                await _unitOfWork.ShopSubscriptions.GetForTenantAsync(client);

                subscriptions.AddRange(client.TenantSubscriptions.Where(x => x.CancelToken == token));
            }


            if (!subscriptions.Any())
                return Ok();

            var now = DateTime.Now.Date;
            decimal refund = 0;
            foreach (var subscription in subscriptions)
            {
                subscription.DateCancelled = now;
                var model = new Models.Shop.Subscription(subscription);

                if (model.DateEnd == null
                    || model.Cost == 0)
                    continue;

                var totalDays = ((DateTime)model.DateEnd - model.DateStart).TotalDays;

                if (totalDays == 0)
                    continue;

                var remainingDays = totalDays;

                if (subscription.DateStart < now)
                    remainingDays = ((DateTime)model.DateEnd - now).TotalDays;


                var percent = remainingDays / totalDays;

                refund += model.Cost * (decimal)percent;
            }

            await _unitOfWork.CompleteAsync();


            // ToDo: Stripe: Create a refund.
            Console.WriteLine("Refund: " + refund);

            SignOutProcess();
            return Redirect("/Unsubscribed");
        }


        [Route("/Unsubscribed")]
        public IActionResult Unsubscribed()
        {
            return View("/Views/Shop/Unsubscribed.cshtml");
        }
    }
}