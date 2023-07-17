using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Application;

namespace SilkFlo.Web.Controllers
{
    public abstract partial class Abstract
    {
        protected bool IsIdeaAuthorisationMember(
            Data.Core.IUnitOfWork unitOfWork,
            Enumerators.IdeaAuthorization ideaAuthorisationId,
            string ideaId,
            string userId)
        {
            var item =
                unitOfWork
                    .BusinessUserAuthorisations
                    .SingleOrDefaultAsync(x => 
                            x.IdeaId == ideaId
                         && x.UserId == userId
                         && x.IdeaAuthorisationId == ideaAuthorisationId.ToString());

            return item != null;
        }

        protected string GetDomainName()
        {
            var domainName = Request.Host.ToString().ToLower();
            return domainName;
        }

        protected async Task<string> GetBodyAsync()
        {
            string body;
            using (var reader
                      = new StreamReader(Request.Body,
                                         System.Text.Encoding.UTF8,
                                         true,
                                         1024,
                                         true))
            {
                body = await reader.ReadToEndAsync();
            }

            return body;
        }

        protected T GetModelFromJSON<T>(string json)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<T>(json);
                return model;
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);
                throw;
            }
            
        }

        protected async Task<T> GetModelAsync<T>()
        {
            var body = await GetBodyAsync();
            var model = GetModelFromJSON<T>(body);
            return model;
        }

        protected async Task<ViewModels.Application.Setting> GetApplicationSettingsAsync()
        {
            var settings = new Models.Application.Setting(_unitOfWork);
            var viewModel = new ViewModels.Application.Setting
            {
                PracticeAccountCanSignIn = await settings.GetPracticeAccountCanSignIn(),
                PracticeAccountPassword = await settings.GetPracticeAccountPassword(),
                TestEmailAccount = await settings.GetTestEmailAccount(),
                TrialPeriod = await settings.GetTrialPeriod()
            };

            return viewModel;
        }


        protected async Task<string> GetApplicationSettingsAsync(
            Enumerators.Setting name,
            string defaultValue = "")
        {
            var id = name.ToString();
            var setting = await _unitOfWork.ApplicationSettings.GetAsync(id);

            return setting == null ? defaultValue : setting.Value;
        }


       

        protected async Task SaveApplicationSettingsAsync(ViewModels.Application.Setting setting)
        {
            var saveMe = await SaveSetting(
                setting.PracticeAccountCanSignIn.ToString(),
                Enumerators.Setting.PracticeAccountCanSignIn);


            if (await SaveSetting(
                    setting.PracticeAccountPassword ?? "",
                    Enumerators.Setting.PracticeAccountPassword))
                saveMe = true;


            if (await SaveSetting(
                    setting.TestEmailAccount??"",
                    Enumerators.Setting.TestEmailAccount))
                saveMe = true;


            if (await SaveSetting(
                    setting.TrialPeriod.ToString(),
                    Enumerators.Setting.TrialPeriod))
                saveMe = true;


            if (saveMe)
                await _unitOfWork.CompleteAsync();
        }

        private async Task<bool> SaveSetting(string value, Enumerators.Setting name)
        {
            var id = name.ToString();
            var setting = await _unitOfWork.ApplicationSettings.GetAsync(id);
            if (setting == null)
            {
                if (string.IsNullOrWhiteSpace(value))
                    return false;

                setting = new Setting
                {
                    Value = value,
                };

                await _unitOfWork.AddAsync(setting);
                setting.Id = id;
                return true;
            }


            if (string.IsNullOrWhiteSpace(value))
            {
                await _unitOfWork.ApplicationSettings.RemoveAsync(setting);
                return true;
            }


            if (setting.Value == value)
                return false;


            setting.Value = value;
            return true;
        }


        protected async Task SaveProductCookieAsync(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                await SaveProductCookieAsync((Data.Core.Domain.Business.Client)null);

            var client = await _unitOfWork.BusinessClients.GetAsync(clientId);
            await SaveProductCookieAsync(client);
        }


        protected async Task SaveProductCookieAsync(Data.Core.Domain.Business.Client client)
        {
            if (client == null)
            {
                Delete(Services.Cookie.SilkFloData);
                return;
            }


            if (client.IsPractice)
            {
                await _unitOfWork.BusinessClients.GetForPracticeAccountAsync(client);
                if(client.ProductionAccounts.Count == 0)
                {
                    Delete(Services.Cookie.SilkFloData);
                    return;
                }

                client = client.ProductionAccounts[0];
            }


            if (client.TypeId == Enumerators.ClientType.Client39.ToString()
                || client.TypeId == Enumerators.ClientType.ResellerAgency45.ToString())
            {
                Data.Core.Domain.Shop.Product product = null;

                if (client.TypeId == Enumerators.ClientType.Client39.ToString())
                {
                    var clientModel = new Models.Business.Client(client);

                    var subscription = await clientModel.GetLastSubscriptionAsync(_unitOfWork);


                    if (subscription != null)
                    {
                        await _unitOfWork.ShopPrices.GetPriceForAsync(subscription.GetCore());

                        if (subscription.Price != null)
                        {
                            await _unitOfWork.ShopProducts.GetProductForAsync(subscription.Price.GetCore());
                            if (subscription.Price.Product != null)
                                product = subscription.Price.Product.GetCore();
                        }
                    }

                    product ??= await _unitOfWork.ShopProducts.SingleOrDefaultAsync(x => x.Name == "Standard" && x.IsLive);
                }
                else
                {
                    product = await _unitOfWork.ShopProducts.GetAsync("silkFlo-agency");
                }


                if (product == null)
                {
                    Delete(Services.Cookie.SilkFloData);
                }
                else
                {
                    var json = JsonConvert.SerializeObject(product);
                    var encrypted = Security.Encryption.Encrypt(json);

                    Add(Services.Cookie.SilkFloData,
                        encrypted,
                        DateTime.Now.AddDays(1000),
                        true);
                }
            }
            else
            {
                Delete(Services.Cookie.SilkFloData);
            }
        }

        protected Data.Core.Domain.Shop.Product GetProductCookie()
        {
            try
            {
                var cookie = Request.Cookies[Services.Cookie.SilkFloData.ToString()];

                if (string.IsNullOrWhiteSpace(cookie))
                    return null;

                var json = Security.Encryption.DecryptString(cookie);


                return string.IsNullOrWhiteSpace(json) ? null : JsonConvert.DeserializeObject<Data.Core.Domain.Shop.Product>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}