using Stripe;
using System.Threading.Tasks;

namespace SilkFlo.Web.Insert
{
    public class SubscriptionData
    {
        public static async Task ShopProductsAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var sort = 0;

            sort++;
            await ProductAsync(
                unitOfWork,
                "prod_LAsEprC1zSlWer",
                "Standard",
                5,
                "1-5 Admin members",
                null,
                "Unlimited standard users",
                2,
                "2 collaborators per process",
                50,
                "50 Processes",
                null,
                null,
                sort,
                "",
                true);

            sort++;
            await ProductAsync(
                unitOfWork,
                "prod_LAt79ST4qHkrk4",
                "Professional",
                10,
                "6-10 Admin members",
                null,
                "Unlimited standard users",
                5,
                "5 collaborators per process",
                150,
                "150 processes",
                null,
                "",
                sort,
                "",
                true);


            sort++;
            await ProductAsync(
                unitOfWork,
                "prod_LAt9DFVQ3njlzW",
                "Enterprise",
                15,
                "11-15 Admin members",
                null,
                "Unlimited standard users",
                10,
                "10 collaborators per process",
                null,
                "Unlimited processes",
                null,
                "",
                sort,
                "",
                true);




            // Test
            sort++;
            await ProductAsync(
                unitOfWork,
                "prod_LGVtDetPz33mDi",
                "Standard",
                5,
                "1-5 Admin members",
                null,
                "Unlimited standard users",
                2,
                "2 collaborators per process",
                50,
                "50 Processes",
                null,
                null,
                sort,
                "",
                false);



            sort++;
            await ProductAsync(
                unitOfWork,
                "prod_LGVvwACi9gLK4i",
                "Professional",
                10,
                "6-10 Admin members",
                null,
                "Unlimited standard users",
                5,
                "5 collaborators per process",
                150,
                "150 processes",
                null,
                "",
                sort,
                "",
                false);


            sort++;
            await ProductAsync(
                unitOfWork,
                "prod_LGVyS16xysDED3",
                "Enterprise",
                15,
                "11-15 Admin members",
                null,
                "Unlimited standard users",
                10,
                "10 collaborators per process",
                null,
                "Unlimited processes",
                null,
                "",
                sort,
                "",
                false);



            sort++;
            await ProductAsync(
                unitOfWork,
                "silkFlo-agency",
                "Agency",
                15,
                "11-15 Admin members",
                null,
                "Unlimited standard users",
                10,
                "10 collaborators per process",
                null,
                "Unlimited processes",
                null,
                "",
                sort,
                "",
                false,
                true);
        }

        public static async Task ProductAsync(
            Data.Core.IUnitOfWork unitOfWork,
            string id,
            string name,
            int? adminUserLimit,
            string adminUserText,
            int? standardUserLimit,
            string standardUserText,
            int? collaboratorLimit,
            string collaboratorText,
            int? ideaLimit,
            string ideaText,
            int? storageLimit,
            string storageText,
            int sort,
            string note,
            bool isLive,
            bool noPrice = false)
        {
            var core = await unitOfWork
                .ShopProducts
                .GetAsync(id);

            if (core == null)
            {
                core = new Data.Core.Domain.Shop.Product
                {
                    Name = name,
                    AdminUserLimit = adminUserLimit,
                    AdminUserText = adminUserText,
                    StandardUserLimit = standardUserLimit,
                    StandardUserText = standardUserText,
                    CollaboratorLimit = collaboratorLimit,
                    CollaboratorText = collaboratorText,
                    IdeaLimit = ideaLimit,
                    IdeaText = ideaText,
                    StorageLimit = storageLimit,
                    StorageText = storageText,
                    Sort = sort,
                    IsVisible = true,
                    Note = note,
                    IsLive = isLive,
                    NoPrice = noPrice
                };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }
        }
    }
}
