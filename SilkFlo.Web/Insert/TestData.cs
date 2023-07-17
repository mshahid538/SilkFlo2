using System;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core;

namespace SilkFlo.Web.Insert
{
    public class TestData
    {
        public static async Task InsertClients(
            IUnitOfWork unitOfWork,
            Data.Core.Domain.Business.Client clientSilkFlo,
            Data.Core.Domain.User powerUser)
        {
            try
            {
                var agency = await ClientAsync(
                    unitOfWork,
                    clientSilkFlo,
                    "Reseller Agency",
                    Enumerators.ClientType.ResellerAgency45,
                    null,
                    "reseller.com",
                    5,
                    "");

                var client = await TenantAsync(
                    "-1", 
                    "Lion's Heart", 
                    unitOfWork, 
                    agency, 
                    false,
                    powerUser);

                await TenantAsync(
                    "-2", 
                    "Delaneys.space", 
                    unitOfWork, 
                    agency, 
                    false,
                    powerUser);


                const string email = "Sharpe@reseller.com";
                var user = await unitOfWork.Users.GetByEmailAsync(email);

                var password = Security.Settings.Password;

                if (user == null)
                {
                    var roles = new[]
                    {
                        Enumerators.Role.AgencyUser,
                        Enumerators.Role.AccountOwner,
                        Enumerators.Role.AgencyAdministrator
                    };
                    user = await Services.Authorization
                        .User
                        .CreateAsync("Richard",
                            "Sharpe",
                            email,
                            password,
                            roles,
                            unitOfWork,
                            true);

                    user.Client = agency;

                    await unitOfWork.AddAsync(user);
                }



                await ClientAsync(
                    unitOfWork,
                    clientSilkFlo, 
                    "Referrer Agency",
                    Enumerators.ClientType.ReferrerAgency41,
                    null, 
                    "referrer.com",
                    5,
                    "");

                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
            }
        }




        public static async Task<Data.Core.Domain.Business.Client> ClientAsync(IUnitOfWork unitOfWork,
                                                                           Data.Core.Domain.Business.Client silkFLoClient,
                                                                           string name,
                                                                           Enumerators.ClientType typeId,
                                                                           Data.Core.Domain.User accountOwner,
                                                                           string domainName,
                                                                           int userCount,
                                                                           string priceId)
        {
            if (string.IsNullOrWhiteSpace(domainName))
                throw new ArgumentNullException(nameof(domainName));

            try
            {
                string[] streetNames = {
                    "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez",
                    "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor",
                    "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez",
                    "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young", "Allen", "King", "Wright",
                    "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams", "Nelson", "Baker",
                    "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts" };
                var streetNamesCount = streetNames.Length - 1;

                var random = new Random();
                var streetNameIndex = random.Next(0, streetNamesCount);
                var streetName = streetNames[streetNameIndex];

                var buildingNumber = random.Next(1, 500);

                const string currencyId = "gbp";
                const string languageId = "en-gb";
                var client = await unitOfWork.BusinessClients
                                             .SingleOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
                if (client == null)
                {
                    var country = await unitOfWork.SharedCountries
                                                  .SingleOrDefaultAsync(x => x.Name.IndexOf("united kingdom", StringComparison.InvariantCultureIgnoreCase) > -1);


                    client = new Data.Core.Domain.Business.Client
                    {
                        Name = name,
                        Country = country,
                        CurrencyId = currencyId,
                        LanguageId = languageId,
                        TypeId = typeId.ToString(),
                        Website = domainName,
                        Address1 = buildingNumber + " " + streetName + " Street",
                        PostCode = "1234",
                        IndustryId = "Other",
                        AccountOwner = accountOwner,
                        IsActive = true,
                        IsDemo = true
                    };
                    await unitOfWork.BusinessClients.AddAsync(client);

                    if(typeId != Enumerators.ClientType.ReferrerAgency41)
                        await PracticeData.CreatePracticeAccountAsync(
                            client,
                            unitOfWork, 
                            false,
                            accountOwner);
                }


                if (typeId == Enumerators.ClientType.Client39)
                {
                    var subscription = await unitOfWork.ShopSubscriptions
                        .SingleOrDefaultAsync(x => x.TenantId == client.Id);

                    if (subscription == null)
                        await Models.Shop.Subscription.CreateAsync(
                            unitOfWork,
                            new Models.Business.Client(client),
                            priceId);
                }

                if(userCount > 0)
                    await PracticeData.AddUsersAsync(
                        client,
                        unitOfWork,
                        userCount,
                        domainName,
                        false);




                return client;

            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
                throw;
            }
        }


        public static async Task<Data.Core.Domain.Business.Client> TenantAsync(string tenantId,
                                         string name,
                                         IUnitOfWork unitOfWork,
                                         Data.Core.Domain.Business.Client agency,
                                         bool useTestUsers,
                                         Data.Core.Domain.User powerUser)
        {
            try
            {
                const string currencyId = "gbp";
                const string languageId = "en-gb";
                var tenant = await unitOfWork.BusinessClients
                                             .SingleOrDefaultAsync(x => x.Id == tenantId);
                if (tenant == null)
                {
                    string[] streetNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts" };
                    var streetNamesCount = streetNames.Length - 1;

                    var random = new Random();
                    var streetNameIndex = random.Next(0, streetNamesCount);
                    var streetName = streetNames[streetNameIndex];

                    var buildingNumber = random.Next(1, 500);

                    var country = await unitOfWork.SharedCountries.GetByNameAsync("united kingdom");

                    var industries = (await unitOfWork.SharedIndustries.GetAllAsync()).ToList();
                    var industryIndex = random.Next(0, industries.Count());
                    var industryId = industries[industryIndex].Id;

                    tenant = new Data.Core.Domain.Business.Client
                    {
                        Name = name,
                        Country = country,
                        CurrencyId = currencyId,
                        LanguageId = languageId,
                        IndustryId = industryId,
                        Address1 = buildingNumber + " " + streetName + " Street",
                        PostCode = "1234",
                        TypeId = "Client39",
                        Website = "TEST",
                        IsActive = true,
                    };

                    var model = new Models.Business.Client(tenant);

                    await unitOfWork.AddAsync(tenant);
                    await model.SetupTenantAsync(unitOfWork);
                    await PracticeData.CreatePracticeAccountAsync(
                        tenant,
                        unitOfWork,
                        useTestUsers,
                        powerUser);

                    tenant.Id = tenantId;
                }


                var subscription = await unitOfWork.ShopSubscriptions
                                                   .SingleOrDefaultAsync(x => x.TenantId == tenantId
                                                                           && x.DateCancelled == null
                                                                           && x.AgencyId == agency.Id);

                if (subscription != null)
                    return tenant;


                subscription = new Data.Core.Domain.Shop.Subscription
                {
                    Tenant = tenant,
                    Agency = agency,
                    DateStart = DateTime.Now,
                };
                await unitOfWork.AddAsync(subscription);

                return tenant;
            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
                throw;
            }
        }
    }
}
