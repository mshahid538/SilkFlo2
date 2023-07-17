using System;
using System.Threading.Tasks;

namespace SilkFlo.Web.Insert
{
    public class Demo
    {

        public static async Task<Data.Core.Domain.Business.Client> Client(
            Data.Core.IUnitOfWork unitOfWork,
            Data.Core.Domain.Business.Client clientSilkFlo,
            string accountOwnerFirstName,
            string accountOwnerLastName,
            string accountOwnerPassword,
            string accountOwnerEmailSuffix,
            string clientName,
            Data.Core.Enumerators.ClientType clientType,
            string domain,
            string priceId)
        {
            var email = accountOwnerEmailSuffix + "@" + domain;
            var accountOwner = await unitOfWork.Users.GetByEmailAsync(email) ?? 
                                   await Services.Authorization
                                        .User
                                        .CreateAsync(accountOwnerFirstName,
                                            accountOwnerLastName,
                                            email,
                                            accountOwnerPassword,
                                            Data.Core.Enumerators.Role.AccountOwner,
                                            unitOfWork,
                                            true);


            if (accountOwner == null)
                return null;



            var client = await TestData.ClientAsync(
                unitOfWork,
                clientSilkFlo,
                clientName,
                clientType,
                accountOwner,
                domain,
                0,
                priceId);

            client.AccountOwner = accountOwner;

            accountOwner.Client = client;


            await unitOfWork.CompleteAsync();
            return client;
        }
    }
}
