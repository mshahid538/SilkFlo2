using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkFlo.Web.Controllers2.FileUpload;

namespace SilkFlo.Web.APIControllers.Settings
{
    public class PlatformSetupController : Controllers.AbstractAPI
    {
        protected IAzureStorage _storage;
        public PlatformSetupController(
            Data.Core.IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization, IAzureStorage storage) : base(unitOfWork, viewToString, authorization) 
        {
            _storage = storage;
        }

        public IAzureStorage AzureStorage => _storage;

        [HttpGet("api/settings/Tenant/platformSetup/BusinessUnits")]
        public async Task<IActionResult> GetBusinessUnits()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync();

                if (client == null)
                    return NegativeFeedback();

                await _unitOfWork.BusinessDepartments.GetForClientAsync(client);
                var model = new Models.Business.Client(client);

                string html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/BusinessUnit/_Index", model.Departments);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }

        [HttpGet("api/settings/Tenant/platformSetup/Documents")]
        public async Task<IActionResult> GetTemplateDocuments()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                // Check Authorization
                if (!(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
                    return NegativeFeedback("Unauthorised");

                //Getting the tenant
                var tenantCore = await GetClientAsync();
                //Getting docs
                var blobstorageDATA = await AzureStorage.GetTemplateDocuments(tenantCore.Id);
                var html = await _viewToString.PartialAsync(
                        "Shared/Business/Idea/Detail/Documentation/_TemplatesViewer.cshtml",
                        blobstorageDATA);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }



        [HttpGet("api/settings/Tenant/platformSetup/Applications/Summary")]
        public async Task<IActionResult> GetApplicationsSummary()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();



            try
            {
                var client = await GetClientAsync();

                if (client == null)
                    return NegativeFeedback();


                // Get data
                await _unitOfWork.BusinessVersions.GetForClientAsync(client);
                await _unitOfWork.BusinessApplications.GetApplicationForAsync(client.Versions);
                await _unitOfWork.BusinessIdeaApplicationVersions.GetForVersionAsync(client.Versions);





                // Sort
                var versions = client.Versions.OrderBy(x => x.Application.Name)
                                                                 .ThenBy(x => x.Name).ToList();


              


                // Render
                var modelVersions = Models.Business.Version.Create(versions);


                var viewModel = new ViewModels.Business.Version.Summary(
                    modelVersions);


                string html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/Applications/_Summary", viewModel);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data. Error Logged.");
            }
        }


        [HttpGet("api/settings/Tenant/platformSetup/Applications")]
        [HttpGet("api/settings/Tenant/platformSetup/Applications/Search/{text}")]
        public async Task<IActionResult> GetApplications(
            string text ="")
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();



            try
            {
                var client = await GetClientAsync();

                // Authorization Clause
                if (client == null)
                    return NegativeFeedback();


                // Get data
                await _unitOfWork.BusinessVersions.GetForClientAsync(client);
                await _unitOfWork.BusinessApplications.GetApplicationForAsync(client.Versions);
                await _unitOfWork.BusinessIdeaApplicationVersions.GetForVersionAsync(client.Versions);

                text = text.Trim().ToLower();




                // Sort
                var versions = client.Versions.OrderBy(x => x.Application.Name)
                                                                 .ThenBy(x => x.Name).ToList();

                if (!string.IsNullOrWhiteSpace(text))
                {
                    var exactMatchRequired = false;
                    if (text.IndexOf("\"", StringComparison.Ordinal) == 0
                        && text.IndexOf("\"", 1, StringComparison.Ordinal) > 1)
                    {
                        exactMatchRequired = true;
                        text = text.Substring(1);
                        text = text.Substring(0, text.Length - 1);
                    }


                    if (exactMatchRequired)
                    {
                        versions = versions.Where(x => x.Application.Name.ToLower() == text)
                            .ToList();
                    }
                    else
                    {
                        versions = versions.Where(x => x.Application.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                            .ToList();
                    }
                }




                // Render
                var viewModel = new ViewModels.Business.Version.Summary(
                    Models.Business.Version.Create(versions));


                string html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/Applications/_Table", viewModel);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }


        [HttpGet("api/settings/Tenant/platformSetup/Applications/NewRow")]
        public async Task<IActionResult> GetNewRow()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            var model = new Models.Business.Version();

            string html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/Applications/_Row", model);
            return Content(html);
        }




        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/Summary")]
        public async Task<IActionResult> GetCostSetup()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                var viewModel = new ViewModels.Settings.PlatformSetup("software-vendor", false);

                string html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/_Index", viewModel);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }



        [HttpGet("api/Settings/tenant/PlatformSetup/CostSetup/SoftwareVendor/Summary")]
        public async Task<IActionResult> GetCostSetupSoftwareVendor()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync();

                if (client == null)
                    return NegativeFeedback();

                await _unitOfWork.BusinessSoftwareVenders.GetForClientAsync(client);


                var models = Models.Business.SoftwareVender.Create(client.SoftwareVenders.ToList());


                var viewModel = new ViewModels.Business.SoftwareVender.Summary(
                    models);


                var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/SoftwareVender/_Summary", viewModel);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }


        [HttpGet("api/Settings/tenant/PlatformSetup/CostSetup/InitialCosts/Summary")]
        public async Task<IActionResult> GetCostSetupInitialCosts()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync();

                // Authorization Clause
                if (client == null)
                    return NegativeFeedback();


                await _unitOfWork.BusinessRoleCosts.GetForClientAsync(client);
                await _unitOfWork.BusinessRoles.GetRoleForAsync(client.RoleCosts);
                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);
                await _unitOfWork.BusinessRoles.GetForClientAsync(client);

                var roles = (await _unitOfWork.BusinessRoles.FindAsync(x => x.IsBuiltIn)).ToList();
                roles.AddRange(client.Roles);
                roles = roles.OrderBy(x => x.Name).ToList();



                var models = Models.Business.RoleCost.Create(client.RoleCosts.ToList());

                Models.Shop.Currency currency = null;
                if (client.Currency != null)
                    currency = new Models.Shop.Currency(client.Currency);

                foreach (var model in models)
                {
                    model.AverageWorkingDay = client.AverageWorkingDay;
                    model.Currency = currency;
                    model.Roles = Models.Business.Role.Create(roles);
                }



                var viewModel = new ViewModels.Business.InitialCosts.Summary(
                    models,
                    currency, 
                    client.AverageWorkingDay);


                var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/InitialCosts/_Summary", viewModel);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }



        [HttpGet("api/Settings/tenant/PlatformSetup/CostSetup/RunningCosts/Summary")]
        public async Task<IActionResult> GetCostSetupRunningCosts()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync();

                if (client == null)
                    return NegativeFeedback();

                await _unitOfWork.BusinessRunningCosts.GetForClientAsync(client);
                await _unitOfWork.BusinessSoftwareVenders.GetForClientAsync(client);
                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);

                var automationTypes = (await _unitOfWork.SharedAutomationTypes.GetAllAsync()).ToList();
                var frequencies = (await _unitOfWork.SharedPeriods.GetAllAsync()).ToList();



                var cores = client.RunningCosts.ToList();
                await _unitOfWork.BusinessSoftwareVenders.GetVenderForAsync(cores);
                await _unitOfWork.SharedAutomationTypes.GetAutomationTypeForAsync(cores);
                await _unitOfWork.SharedPeriods.GetFrequencyForAsync(cores);

                var models = Models.Business.RunningCost.Create(cores);

                Models.Shop.Currency currency = null;
                if (client.Currency != null)
                    currency = new Models.Shop.Currency(client.Currency);

                foreach (var model in models)
                {
                    model.Currency = currency;
                    model.Venders = Models.Business.SoftwareVender.Create(client.SoftwareVenders);
                    model.AutomationTypes = Models.Shared.AutomationType.Create(automationTypes);
                    model.Frequencies = Models.Shared.Period.Create(frequencies);
                }




                var softwareVenders = (await
                    _unitOfWork.BusinessSoftwareVenders
                        .FindAsync(x => x.ClientId == client.Id
                                                && x.IsLive)).ToArray();

                var viewModel = new ViewModels.Business.RunningCosts.Summary(
                    models,
                    currency,
                    softwareVenders.Any());




                var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/RunningCosts/_Summary", viewModel);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }


        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/RunningCosts/NewRow")]
        public async Task<IActionResult> CostSetup_RunningCosts_GetNewRow()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync();

                // Authorization Clause
                if (client == null)
                    return NegativeFeedback();

                await _unitOfWork.BusinessSoftwareVenders.GetForClientAsync(client);
                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);

                var automationTypes = (await _unitOfWork.SharedAutomationTypes.GetAllAsync()).ToList();
                var frequencies = (await _unitOfWork.SharedPeriods.GetAllAsync()).ToList();

                Models.Shop.Currency currency = null;
                if(client.Currency != null)
                    currency = new Models.Shop.Currency(client.Currency);

                var softwareVenders = (
                    from softwareVenderCore 
                    in client.SoftwareVenders 
                    where softwareVenderCore.IsLive 
                    select new Models.Business.SoftwareVender(softwareVenderCore)).ToList();


                var model = new Models.Business.RunningCost
                {
                    Venders = softwareVenders,
                    AutomationTypes = Models.Shared.AutomationType.Create(automationTypes),
                    Frequencies = Models.Shared.Period.Create(frequencies),
                    Currency = currency,
                    IsLive = true,
                };


                var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/RunningCosts/_Row", model);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }



        [HttpGet("api/Settings/tenant/PlatformSetup/CostSetup/OtherRunningCosts/Summary")]
        public async Task<IActionResult> GetCostSetupOtherRunningCosts()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync();

                if (client == null)
                    return NegativeFeedback();


                await _unitOfWork.BusinessOtherRunningCosts.GetForClientAsync(client);
                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);
                var costTypes = (await _unitOfWork.SharedCostTypes.GetAllAsync()).ToList();
                var frequencies = (await _unitOfWork.SharedPeriods.GetAllAsync()).ToList();


                var cores = client.OtherRunningCosts.ToList();
                await _unitOfWork.SharedCostTypes.GetCostTypeForAsync(cores);
                await _unitOfWork.SharedPeriods.GetFrequencyForAsync(cores);

                var models = Models.Business.OtherRunningCost.Create(client.OtherRunningCosts);


                Models.Shop.Currency currency = null;
                if (client.Currency != null)
                    currency = new Models.Shop.Currency(client.Currency);



                foreach (var model in models)
                {
                    model.CostTypes = Models.Shared.CostType.Create(costTypes);
                    model.Frequencies = Models.Shared.Period.Create(frequencies);
                    model.Currency = currency;
                }



                var viewModel = new ViewModels.Business.OtherRunningCosts.Summary(
                    models,
                    currency);


                var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/OtherRunningCosts/_Summary", viewModel);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }



        [HttpGet("api/settings/Tenant/platformSetup/ImportPipeline/Summary")]
        public async Task<IActionResult> GetImportPipeline()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync();

                if (client == null)
                    return NegativeFeedback();


                return Content("ToDo: Server message GetImportPipeline");
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }



        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/SoftwareVender/NewRow")]
        public async Task<IActionResult> CostSetup_SoftwareVender_GetNewRow()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            var model = new Models.Business.SoftwareVender
            {
                IsLive = true
            };

            var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/SoftwareVender/_Row", model);
            return Content(html);
        }


        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/SoftwareVenders")]
        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/SoftwareVenders/Search/{text}")]
        public async Task<IActionResult> GetCostSetupSoftwareVenders(
            string text = "")
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();


            try
            {
                var client = await GetClientAsync();

                // Authorization Clause
                if (client == null)
                    return NegativeFeedback();

                await _unitOfWork.BusinessSoftwareVenders.GetForClientAsync(client);

                text = text.Trim().ToLower();

                var softwareVenders = client.SoftwareVenders.ToList();


                if (!string.IsNullOrWhiteSpace(text))
                {
                    var exactMatchRequired = false;
                    if (text.IndexOf("\"", StringComparison.Ordinal) == 0
                        && text.IndexOf("\"", 1, StringComparison.Ordinal) > 1)
                    {
                        exactMatchRequired = true;
                        text = text.Substring(1);
                        text = text.Substring(0, text.Length - 1);
                    }


                    if (exactMatchRequired)
                    {
                        softwareVenders = softwareVenders.Where(x => x.Name.ToLower() == text)
                            .ToList();
                    }
                    else
                    {
                        softwareVenders = softwareVenders.Where(x => x.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                            .ToList();
                    }
                }




                // Render
                var modelSoftwareVenders = Models.Business.SoftwareVender.Create(softwareVenders);


                var viewModel = new ViewModels.Business.SoftwareVender.Summary(
                    modelSoftwareVenders);


                var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/SoftwareVender/_Table", viewModel);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }



        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/InitialCosts/NewRow")]
        public async Task<IActionResult> CostSetup_InitialCosts_GetNewRow()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync();

                // Authorization Clause
                if (client == null)
                    return NegativeFeedback();

                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);
                await _unitOfWork.BusinessRoles.GetForClientAsync(client);


                var roles = (await _unitOfWork.BusinessRoles.FindAsync(x => x.IsBuiltIn)).ToList();
                roles.AddRange(client.Roles);
                roles = roles.OrderBy(x => x.Name).ToList();


                Models.Shop.Currency currency = null;
                if (client.Currency != null)
                    currency = new Models.Shop.Currency(client.Currency);

                var model = new Models.Business.RoleCost
                {
                    Roles = Models.Business.Role.Create(roles),
                    Currency = currency,
                    AverageWorkingDay = client.AverageWorkingDay
                };


                string html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/InitialCosts/_Row", model);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }



        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/InitialCosts")]
        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/InitialCosts/Search/{text}")]
        public async Task<IActionResult> GetCostSetupInitialCosts(
            string text = "")
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();


            try
            {
                var client = await GetClientAsync();

                // Authorization Clause
                if (client == null)
                    return NegativeFeedback();

                await _unitOfWork.BusinessRoleCosts.GetForClientAsync(client);
                await _unitOfWork.BusinessRoles.GetRoleForAsync(client.RoleCosts);

                text = text.Trim().ToLower();

                var cores = client.RoleCosts.ToList();




                if (!string.IsNullOrWhiteSpace(text))
                {
                    var exactMatchRequired = false;
                    if (text.IndexOf("\"", StringComparison.Ordinal) == 0
                        && text.IndexOf("\"", 1, StringComparison.Ordinal) > 1)
                    {
                        exactMatchRequired = true;
                        text = text.Substring(1);
                        text = text.Substring(0, text.Length - 1);
                    }


                    if (exactMatchRequired)
                    {
                        cores = cores.Where(x => x.Role.Name.ToLower() == text)
                            .ToList();
                    }
                    else
                    {
                        cores = cores.Where(x => x.Role.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                            .ToList();
                    }
                }




                // Render
                var modelRoleCosts = Models.Business.RoleCost.Create(cores);


                var viewModel = new ViewModels.Business.InitialCosts.Summary(
                    modelRoleCosts,
                    new Models.Shop.Currency(client.Currency),
                    client.AverageWorkingDay);


                var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/InitialCosts/_Table", viewModel);
                return Content(html);
            } 
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }


        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/RunningCosts")]
        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/RunningCosts/Search/{text}")]
        public async Task<IActionResult> GetCostSetupRunningCosts(
            string text = "")
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync();

                // Authorization Clause
                if (client == null)
                    return NegativeFeedback();

                await _unitOfWork.BusinessRunningCosts.GetForClientAsync(client);

                var cores = client.RunningCosts.ToList();
                await _unitOfWork.BusinessSoftwareVenders.GetVenderForAsync(cores);


                if (!string.IsNullOrWhiteSpace(text))
                {
                    var exactMatchRequired = false;
                    if (text.IndexOf("\"", StringComparison.Ordinal) == 0
                        && text.IndexOf("\"", 1, StringComparison.Ordinal) > 1)
                    {
                        exactMatchRequired = true;
                        text = text.Substring(1);
                        text = text.Substring(0, text.Length - 1);
                    }

                    text = text.ToLower();


                    if (exactMatchRequired)
                    {
                        cores = cores.Where(x => x.Vender?.Name.ToLower() == text
                                                 || x.AutomationTypeId == text
                                                 || x.LicenceType == text
                                                 || x.FrequencyId == text)
                            .ToList();
                    }
                    else
                    {
                        cores = cores.Where(x => x.Vender?.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                 || x.AutomationTypeId.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                 || x.LicenceType.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                 || x.FrequencyId.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                            .ToList();
                    }
                }





                await _unitOfWork.BusinessSoftwareVenders.GetForClientAsync(client);
                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);
                var automationTypes = (await _unitOfWork.SharedAutomationTypes.GetAllAsync()).ToList();
                var frequencies = (await _unitOfWork.SharedPeriods.GetAllAsync()).ToList();

                Models.Shop.Currency currency = null;
                if (client.Currency != null)
                    currency = new Models.Shop.Currency(client.Currency);


                // Render
                var models = Models.Business.RunningCost.Create(cores);

                foreach (var model in models)
                {
                    model.Venders = Models.Business.SoftwareVender.Create(client.SoftwareVenders);
                    model.AutomationTypes = Models.Shared.AutomationType.Create(automationTypes);
                    model.Frequencies = Models.Shared.Period.Create(frequencies);
                    model.Currency = currency;
                }



                var viewModel = new ViewModels.Business.RunningCosts.Summary(
                    models,
                    new Models.Shop.Currency(client.Currency));


                var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/RunningCosts/_Table", viewModel);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }

        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/OtherRunningCosts")]
        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/OtherRunningCosts/Search/{text}")]
        public async Task<IActionResult> GetCostSetupOtherRunningCosts(
            string text = "")
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync();

                // Authorization Clause
                if (client == null)
                    return NegativeFeedback();

                await _unitOfWork.BusinessOtherRunningCosts.GetForClientAsync(client);

                var cores = client.OtherRunningCosts.ToList();

                if (!string.IsNullOrWhiteSpace(text))
                {
                    var exactMatchRequired = false;
                    if (text.IndexOf("\"", StringComparison.Ordinal) == 0
                        && text.IndexOf("\"", 1, StringComparison.Ordinal) > 1)
                    {
                        exactMatchRequired = true;
                        text = text.Substring(1);
                        text = text.Substring(0, text.Length - 1);
                    }

                    text = text.ToLower();
                    var noSpaces = text.Replace(" ", "");

                    if (exactMatchRequired)
                    {
                        cores = cores.Where(x => x.Name.ToLower() == text
                                                 || x.Description.ToLower() == text
                                                 || x.CostTypeId.ToLower() == noSpaces
                                                 || x.FrequencyId == text)
                            .ToList();
                    }
                    else
                    {
                        cores = cores.Where(x => x.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                 || x.Description.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                 || x.CostTypeId.IndexOf(noSpaces, StringComparison.InvariantCultureIgnoreCase) > -1
                                                 || x.FrequencyId.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1)
                            .ToList();
                    }
                }




                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);
                var costTypes = (await _unitOfWork.SharedCostTypes.GetAllAsync()).ToList();
                var frequencies = (await _unitOfWork.SharedPeriods.GetAllAsync()).ToList();

                Models.Shop.Currency currency = null;
                if (client.Currency != null)
                    currency = new Models.Shop.Currency(client.Currency);


                // Render
                var models = Models.Business.OtherRunningCost.Create(cores);

                foreach (var model in models)
                {
                    model.CostTypes = Models.Shared.CostType.Create(costTypes);
                    model.Frequencies = Models.Shared.Period.Create(frequencies);
                    model.Currency = currency;
                }



                var viewModel = new ViewModels.Business.OtherRunningCosts.Summary(
                    models,
                    new Models.Shop.Currency(client.Currency));


                var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/OtherRunningCosts/_Table", viewModel);
                return Content(html);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }




        [HttpGet("api/settings/Tenant/platformSetup/CostSetup/OtherRunningCosts/NewRow")]
        public async Task<IActionResult> CostSetup_OtherRunningCosts_GetNewRow()
        {
            // Authorization Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
                return NegativeFeedback();

            try
            {
                var client = await GetClientAsync();

                // Authorization Clause
                if (client == null)
                    return NegativeFeedback();

                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(client);
                var costTypes = (await _unitOfWork.SharedCostTypes.GetAllAsync()).ToList();
                var frequencies = (await _unitOfWork.SharedPeriods.GetAllAsync()).ToList();


                Models.Shop.Currency currency = null;
                if (client.Currency != null)
                    currency = new Models.Shop.Currency(client.Currency);

                var model = new Models.Business.OtherRunningCost
                {
                    CostTypes = Models.Shared.CostType.Create(costTypes),
                    Frequencies = Models.Shared.Period.Create(frequencies),
                    Currency = currency,
                    IsLive = true,
                };

                
                var html = await _viewToString.PartialAsync("Shared/Settings/PlatformSetup/CostSetup/OtherRunningCosts/_Row", model);
                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error fetching data");
            }
        }
    }
}