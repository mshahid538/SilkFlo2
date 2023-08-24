using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core;

namespace SilkFlo.Web.ViewModels
{
    public class Subscriptions
    {
        public Subscriptions(
            List<Models.Shared.Period> periods,
            List<Models.Shop.Product> specialSubscriptionProducts,
            string headerText)
        {
            Periods = periods;
            SpecialProducts = specialSubscriptionProducts;
            HeaderText = headerText;
        }

        public string HeaderText { get; set; }

        public string ClientId { get; set; }

        public Models.CRM.Prospect Prospect { get; set; }

        public List<Models.Shared.Period> Periods { get; set; }
        public List<Models.Shop.Product> SpecialProducts { get; set; }

        public bool ShowTabs => Periods is {Count: > 1};



        public bool IsBookDemo
        {
            get
            {
                if (Periods == null 
                    && SpecialProducts == null)
                    return false;

                bool isBookDemo = false;
                if (Periods != null)
                    foreach (var period in Periods)
                    {
                        if (period.Prices.Any(price => price.Product.ShowBookDemoButton))
                            isBookDemo = true;

                        if (isBookDemo)
                            break;
                    }

                if (SpecialProducts != null)
                    isBookDemo = true;

                return isBookDemo;
            }
        }

        public static async Task<Subscriptions> GetAsync(
            IUnitOfWork unitOfWork, 
            Security.Environment environment,
            string bookDemoButtonText,
            bool showSelectButton,
            bool includeSpecialProducts,
            string headerText,
            string referrerCode)
        {
            var periods = await GetPeriodsAsync(
                unitOfWork,
                environment,
                bookDemoButtonText,
                showSelectButton,
                referrerCode);

            


            if (!includeSpecialProducts)
                return new Subscriptions(
                    periods,
                    null,
                    headerText);


            var specialProductsCores = (await unitOfWork.ShopProducts
                    .FindAsync(x => x.NoPrice && x.IsVisible))
                .ToList();


            var specialProducts = Models.Shop.Product.Create(specialProductsCores);

            foreach (var product in specialProducts)
                product.BookDemoButtonText = bookDemoButtonText;

            return new Subscriptions(
                periods,
                specialProducts,
                headerText);
        }


        private static async Task<List<Models.Shared.Period>> GetPeriodsAsync(
            IUnitOfWork unitOfWork,
            Security.Environment environment,
            string bookDemoButtonText,
            bool showSelectButton,
            string referrerCode)
        {
            var isLive = environment == Security.Environment.Production;

            await Payment.Manager.UpdatePricesAsync(unitOfWork);


            var cores = (await unitOfWork.SharedPeriods.GetAllAsync()).ToList();

            var pricesAnnual = (await unitOfWork.ShopPrices.FindAsync(x => x.PeriodId == Enumerators.Period.Annual.ToString()
                                                                     && x.IsActive
                                                                     && x.IsLive == isLive)).ToList();

            var pricesAnnualModel = Models.Shop.Price.Create(pricesAnnual);

            var pricesMonthly = (await unitOfWork.ShopPrices.FindAsync(x => x.PeriodId == Enumerators.Period.Monthly.ToString()
                                                                            && x.IsActive
                                                                            && x.IsLive == isLive)).ToList();
            var pricesMonthlyModel = Models.Shop.Price.Create(pricesMonthly);


            // Get the Products and currencies for all the prices.
            // Therefore combine lists and fetch from database.
            var prices = new List<Data.Core.Domain.Shop.Price>();
            prices.AddRange(pricesAnnual);
            prices.AddRange(pricesMonthly);
            await unitOfWork.ShopProducts.GetProductForAsync(prices);
            await unitOfWork.ShopCurrencies.GetCurrencyForAsync(prices);


            var models = Models.Shared.Period.Create(cores);
            foreach (var model in models)
            {
                var pricesModel = model.Id == Enumerators.Period.Annual.ToString() ? pricesAnnualModel : pricesMonthlyModel;
                if (model.Id == Enumerators.Period.Annual.ToString())
                {
                    foreach (var price in pricesModel)
                    {
                        var priceOther = pricesMonthly.FirstOrDefault(x => x.ProductId == price.ProductId);
                        if (priceOther == null)
                            continue;

                        price.AmountOther = priceOther.Amount;
                    }
                }
                else
                {
                    foreach (var price in pricesModel)
                    {
                        var priceOther = pricesAnnual.FirstOrDefault(x => x.ProductId == price.ProductId);
                        if (priceOther == null)
                            continue;

                        price.AmountOther = priceOther.Amount;
                    }
                }




                foreach (var price in pricesModel)
                {

                    price.Product.BookDemoButtonText = bookDemoButtonText;
                    price.Product.ShowSelectButton = showSelectButton;
                    price.ReferrerCode = referrerCode;
                }

                model.Prices = pricesModel;
            }

            var modelYearly = models.FirstOrDefault(x => x.Id == Enumerators.Period.Annual.ToString());
            var modelMonthly = models.FirstOrDefault(x => x.Id == Enumerators.Period.Monthly.ToString());
            if (modelYearly == null || modelMonthly == null) 
                return models;

            foreach (var priceYearly in modelYearly.Prices)
            {
                var priceMonthly = modelMonthly.Prices.FirstOrDefault(x => x.ProductId == priceYearly.ProductId);
                if (priceMonthly == null)
                    continue;


                //if (priceMonthly.Amount != null)
                //    priceYearly.MonthlyAmount = priceMonthly.Amount.Value;

                if (priceYearly.Amount != null)
                    priceMonthly.YearlyAmount = priceYearly.Amount.Value;
            }

            return models;
        }
    }
}