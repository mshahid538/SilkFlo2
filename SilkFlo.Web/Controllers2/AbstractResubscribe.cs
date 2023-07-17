using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SilkFlo.Web.Controllers
{
    public abstract class AbstractResubscribe : AbstractAPI
    {
        #region Constructors

        protected AbstractResubscribe(Data.Core.IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }

        protected AbstractResubscribe(Data.Core.IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion



        [Authorize]
        public async Task<IActionResult> ResubscribeView(
            bool returnStringContent,
            string bookDemoButtonText,
            Models.Business.Client client,
            string headerMessage,
            Models.Shop.Subscription lastSubscription,
            string referrerCode,
            string productButtonText)
        {
            var subscriptions = await ViewModels.Subscriptions.GetAsync(
                _unitOfWork,
                Security.Settings.GetEnvironment(),
                bookDemoButtonText,
                true,
                false,
                headerMessage,
                referrerCode);

            

            
            foreach (var period in subscriptions.Periods)
                foreach (var price in period.Prices)
                {
                    if (lastSubscription == null)
                        price.Product.SelectButtonText = productButtonText;
                    else
                    {
                        if (lastSubscription.Price == null)
                        {
                            price.Product.SelectButtonText = productButtonText;
                        }
                        else
                        {
                            if (lastSubscription.Price.Product.Sort < price.Product.Sort)
                                price.Product.SelectButtonText = "Upgrade";
                            else if (lastSubscription.Price.Product.Sort > price.Product.Sort)
                                price.Product.SelectButtonText = "Downgrade";
                            else if (lastSubscription.Price.PeriodId == price.PeriodId)
                                price.Product.IsCurrent = true;
                        }
                    }
                }




            const string url = "/Views/Shared/Shop/Product/_Products.cshtml";

            // Return the view
            if (!returnStringContent)
                return View(url, subscriptions);


            var html = await _viewToString.PartialAsync(url, subscriptions);
            return Content(html);
        }
    }
}