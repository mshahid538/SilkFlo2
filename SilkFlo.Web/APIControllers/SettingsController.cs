using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SilkFlo.Web.Controllers
{
    public partial class SettingsController
    {
        [HttpGet("api/Settings/People/Table")]
        [HttpGet("api/Settings/Guest/Table")]
        public async Task<IActionResult> GetTable()
        {
            return await GetTableWithSearch(null, false);
        }


        //[HttpGet("api/Settings/People/Table/FirstId/{id}")]
        //[HttpGet("api/Settings/People/Table/Page/{pageNumber}")]
        //[HttpGet("api/Settings/People/Table/Page/{pageNumber}/FirstId/{id}")]
        //[HttpGet("api/Settings/People/Table/Search/{text}")]
        //[HttpGet("api/Settings/People/Table/Search/{text}/FirstId/{id}")]
        //[HttpGet("api/Settings/People/Table/Search/{text}/Page/{pageNumber}")]
        //[HttpGet("api/Settings/People/Table/Search/{text}/Page/{pageNumber}/FirstId/{id}")]
        [HttpPost("api/Settings/People/GetRows")]
        public async Task<IActionResult> GetRows([FromBody] ViewModels.Settings.PeopleSearch peopleSearch)
        {
            return await GetTableWithSearch(peopleSearch, true);
        }


        private async Task<IActionResult> GetTableWithSearch(
            [FromBody] ViewModels.Settings.PeopleSearch peopleSearch,
            bool onlyRows)
        {
            // Guard Clause
            if (!(await AuthorizeAsync(Policy.ManageTenantUsers)).Succeeded
            && !(await AuthorizeAsync( Policy.ManageAgencyUsers)).Succeeded)
                return NegativeFeedback();


            try
            {
                var client = await GetClientAsync();

                // Guard Clause
                if(client == null)
                    return NegativeFeedback();

                var clientModel = new Models.Business.Client();

                // Guard Clause
                if ((await AuthorizeAsync(Policy.ManageTenantUsers)).Succeeded 
                    && clientModel.IsAgency
                    && (await AuthorizeAsync(Policy.ManageAgencyUsers)).Succeeded
                    && !clientModel.IsAgency)
                    return NegativeFeedback();



                List<Data.Core.Domain.User> users;
                
                if((peopleSearch != null && peopleSearch.GuestOnly)
                   || Request.Path.Value.Contains("Guest", StringComparison.OrdinalIgnoreCase))
                    users = (await _unitOfWork.Users.FindAsync(x => x.ClientId == client.Id && !x.Email.Contains("@" + client.Website, StringComparison.OrdinalIgnoreCase))).ToList();
                else
                    users = (await _unitOfWork.Users.FindAsync(x => x.ClientId == client.Id)).ToList();


                // Get the UserRoles and Roles
                await _unitOfWork.UserRoles.GetForUserAsync(users);
                foreach (var user in users)
                    await _unitOfWork.Roles.GetRoleForAsync(user.UserRoles);

                // Get Departments
                await _unitOfWork.BusinessDepartments.GetDepartmentForAsync(users);

                // Get Collaborators
                await _unitOfWork.BusinessCollaborators.GetForUserAsync(users);


                var models = Models.User.Create(users);


                var suppliedText = peopleSearch?.Text ?? "";
                var text = peopleSearch?.Text;
                var pageNumber = peopleSearch?.PageNumber ?? 0;

                if (!string.IsNullOrWhiteSpace(text))
                {
                    text = text.Trim().ToLower();

                    var exactMatchRequired = false;
                    if (text.IndexOf("\"", StringComparison.Ordinal) == 0
                        && text.IndexOf("\"", 1, StringComparison.Ordinal) > 1)
                    {
                        exactMatchRequired = true;
                        text = text.Substring(1);
                        text = text.Substring(0, text.Length-1);
                    }


                    if(exactMatchRequired)
                        models = models.Where(x => x.Fullname.ToLower() == text
                                              || x.About.ToLower() == text
                                              || x.Note.ToLower() == text
                                              || x.Status.ToString().ToLower() == text
                                              || x.Department?.Name.ToLower() == text).ToList();
                    else
                        models = models.Where(x => x.Fullname.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.About.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.Note.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.Status.ToString().IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.Department?.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1).ToList();

                }


                if(!string.IsNullOrWhiteSpace(peopleSearch?.FirstUserId))
                {
                    var newModel = models.SingleOrDefault(x => x.Id == peopleSearch?.FirstUserId);
                    if(newModel != null)
                    {
                        models.Remove(newModel);
                        newModel.IsSelected = true;
                        models.Insert(0, newModel);
                    }
                }






                var viewModel = new ViewModels.UserSearchResult
                {
                    Users = models,
                    SearchText = suppliedText.Replace("\"", "&quot;")
                };


                var views = onlyRows ? "Shared/user/_SummaryRows.cshtml" : "Shared/user/_Summary.cshtml";

                var html = await _viewToString.PartialAsync(views, viewModel);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return Content("Error Logged");
            }
        }

        [HttpGet("/api/Settings/Tenant/People/TableRow/{id}")]
        public async Task<IActionResult> GetTableRow(string id)
        {
            if (!(await AuthorizeAsync("Manage Tenant Users")).Succeeded
            && !(await AuthorizeAsync("Manage Agency Users")).Succeeded)
                return NegativeFeedback();

            if(string.IsNullOrWhiteSpace(id))
                return Content("Error: id is missing");


            try
            {
                var clientCore = await GetClientAsync();

                if (clientCore == null)
                    return NegativeFeedback();

                var client = new Models.Business.Client(clientCore);


                if ((await AuthorizeAsync("Manage Tenant Users")).Succeeded && client.IsAgency
                && (await AuthorizeAsync("Manage Agency Users")).Succeeded && !client.IsAgency)
                    return NegativeFeedback();


                var core = await _unitOfWork.Users.SingleOrDefaultAsync(x => x.Id == id);

                if(core == null)
                    return Content("Error: No user found");


                await _unitOfWork.UserRoles.GetForUserAsync(core);
                await _unitOfWork.Roles.GetRoleForAsync(core.UserRoles);
                await _unitOfWork.BusinessDepartments.GetDepartmentForAsync(core);

                var model = new Models.User(core);
                model.IsSelected = true;

                string html = await _viewToString.PartialAsync("Shared/user/_SummaryRowContent.cshtml", model);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return BadRequest();
            }
        }


        [HttpGet("/api/Settings/People/TableRow/EmailPrefix/{emailPrefix}")]
        public async Task<IActionResult> GetTableRowByEmailPrefix(string emailPrefix)
        {
            if (!(await AuthorizeAsync("Manage Tenant Users")).Succeeded
            && !(await AuthorizeAsync("Manage Agency Users")).Succeeded)
                return NegativeFeedback();

            if (string.IsNullOrWhiteSpace(emailPrefix))
                return Content("Error: emailPrefix is missing");


            try
            {
                var clientCore = await GetClientAsync();

                if (clientCore == null)
                    return NegativeFeedback();

                var client = new Models.Business.Client(clientCore);


                if ((await AuthorizeAsync("Manage Tenant Users")).Succeeded && client.IsAgency
                && (await AuthorizeAsync("Manage Agency Users")).Succeeded && !client.IsAgency)
                    return NegativeFeedback();


                var email =  emailPrefix.ToLower() + "@" + client.Website;

                var core = await _unitOfWork.Users.SingleOrDefaultAsync(x => x.Email == email);

                if (core == null)
                    return Content("Error: No user found");


                await _unitOfWork.UserRoles.GetForUserAsync(core);
                await _unitOfWork.Roles.GetRoleForAsync(core.UserRoles);

                var model = new Models.User(core);

                string html = await _viewToString.PartialAsync("Shared/user/_SummaryRow.cshtml", model);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return BadRequest();
            }
        }





        [HttpGet("/api/Settings/Tenants/Table")]
        [HttpGet("/api/Settings/Tenants/Table/FirstId/{id}")]
        [HttpGet("/api/Settings/Tenants/Table/Page/{pageIndex}")]
        [HttpGet("/api/Settings/Tenants/Table/Page/{pageIndex}/FirstId/{id}")]
        [HttpGet("/api/Settings/Tenants/Table/Search/{text}")]
        [HttpGet("/api/Settings/Tenants/Table/Search/{text}/FirstId/{id}")]
        [HttpGet("/api/Settings/Tenants/Table/Search/{text}/Page/{pageIndex}")]
        [HttpGet("/api/Settings/Tenants/Table/Search/{text}/Page/{pageIndex}/FirstId/{id}")]
        public async Task<IActionResult> GetTenantsTable(string text = "",
                                                    int pageIndex = 1,
                                                    string id = "")
        {
            var pageSize = 10;

            if (!(await AuthorizeAsync(Policy.ManageAgencySettings)).Succeeded)
                return NegativeFeedback();


            try
            {

                var agencyCore = await GetClientAsync();

                if (agencyCore == null)
                    return Content("<h1 class=\"text-danger\">Unauthorised</h1>");

                var agency = new Models.Business.Client(agencyCore);
                if (!agency.IsAgency)
                        return Content("<h1 class=\"text-danger\">Unauthorised</h1>");



                var models = new List<Models.Business.Client>();

                if (agencyCore.Name == Data.Core.Settings.ApplicationName)
                {
                    var cores = await _unitOfWork.BusinessClients.FindAsync(x =>
                        !x.IsPractice && x.Name != Data.Core.Settings.ApplicationName);

                    models = Models.Business.Client.Create(cores);
                }
                else
                {
                    await _unitOfWork.BusinessClients.GetForAgencyAsync(agencyCore);
                    await _unitOfWork.ShopSubscriptions.GetForTenantAsync(agencyCore.Customers);

                    models = agency.Customers;
                }



                var suppliedText = text;

                if (!string.IsNullOrWhiteSpace(text))
                {
                    text = text.Trim().ToLower();

                    var exactMatchRequired = false;
                    if (text.IndexOf("\"", StringComparison.Ordinal) == 0
                        && text.IndexOf("\"", 1, StringComparison.Ordinal) > 1)
                    {
                        exactMatchRequired = true;
                        text = text.Substring(1);
                        text = text.Substring(0, text.Length - 1);
                    }


                    if (exactMatchRequired)
                        models = models.Where(x => x.Name.ToLower() == text
                                              || x.Address1.ToLower() == text
                                              || x.Address2.ToLower() == text
                                              || x.City.ToLower() == text
                                              || x.State.ToLower() == text
                                              || x.PostCode.ToLower() == text
                                              || x.Industry?.Name.ToLower() == text
                                              || x.Country?.Name.ToLower() == text
                                              || x.SubscriptionStatus.ToString().ToLower() == text).ToList();
                    else
                        models = models.Where(x => x.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.Address1.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.Address2.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.City.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.State.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.PostCode.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.Industry?.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.Country?.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1
                                                   || x.SubscriptionStatus.ToString().IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1).ToList();

                }




                if (!string.IsNullOrWhiteSpace(id))
                {
                    var newModel = models.SingleOrDefault(x => x.Id == id);
                    if(newModel != null)
                    {
                        newModel.IsSelected = true;
                        models.Remove(newModel);
                        models.Insert(0, newModel);
                    }
                }




                var count = models.Count();
                var pageCount = count / pageSize;

                if (count % pageSize > 0)
                    pageCount++;



                if (pageIndex > 1)
                    models = models.Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize).ToList();
                else
                    models = models.Take(pageSize).ToList();


                foreach (var model in models)
                {
                    await _unitOfWork.SharedIndustries.GetIndustryForAsync(model.GetCore());
                    await _unitOfWork.ShopSubscriptions.GetForTenantAsync(model.GetCore());


                    if (model.TenantSubscriptions.Count > 0)
                    {
                        if (agency.Name == SilkFlo.Data.Core.Settings.ApplicationName)
                        {
                            var subscriptions = model.TenantSubscriptions
                                .ToArray();

                            if (subscriptions.Any())
                                model.Subscription = subscriptions.Last();
                        }
                        else
                        {
                            var subscriptions = model.TenantSubscriptions
                                .Where(x => x.AgencyId == agency.Id)
                                .ToArray();

                            if (subscriptions.Any())
                                model.Subscription = subscriptions.Last();
                        }
                    }

                    await model.SetStatusAsync(_unitOfWork);

                    // Only clients have subscriptions
                    if (model.TypeId == Data.Core.Enumerators.ClientType.Client39.ToString())
                    {
                        if (model.Subscription != null)
                        {
                            await _unitOfWork.ShopPrices.GetPriceForAsync(model.Subscription.GetCore());
                            if (model.Subscription.PriceId != null)
                            {
                                await _unitOfWork.ShopProducts.GetProductForAsync(model.Subscription.Price.GetCore());
                                await _unitOfWork.SharedPeriods.GetPeriodForAsync(model.Subscription.Price.GetCore());
                            }
                        }
                    }

                    await _unitOfWork.SharedClientTypes.GetTypeForAsync(model.GetCore());
                }


                var viewModel = new ViewModels.Business.ClientSearchResult()
                {
                    Clients = models
                };


                if (pageCount > 1)
                {
                    viewModel.FirstPage = 1;
                    viewModel.Page = pageIndex;
                    viewModel.SearchText = suppliedText.Replace("\"", "&quot;");
                    viewModel.LastPage = pageCount;
                }


                var html = await _viewToString.PartialAsync("Shared/Business/Client/_Summary.cshtml", viewModel);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return NegativeFeedback("Error Logged");
            }
        }

        [HttpGet("/api/Settings/Tenant/TableRow/{id}")]
        public async Task<IActionResult> GetTenantTableRow(string id)
        {
            if (!(await AuthorizeAsync("Manage Agency Settings")).Succeeded)
                return NegativeFeedback();

            if (string.IsNullOrWhiteSpace(id))
                return Content("Error: id is missing");


            try
            {
                var core = await _unitOfWork.BusinessClients.SingleOrDefaultAsync(x => x.Id == id);

                if (core == null)
                    return Content("Error: No tenant found");


                await _unitOfWork.SharedIndustries.GetIndustryForAsync(core);
                await _unitOfWork.ShopSubscriptions.GetForTenantAsync(core);




                var model = new Models.Business.Client(core)
                {
                    IsSelected = true
                };


                var agencyCore = await GetClientAsync();

                if (agencyCore == null)
                    return Content("<h1 class=\"text-danger\">Unauthorised</h1>");

                var agency = new Models.Business.Client(agencyCore);



                if (core.AgencySubscriptions.Count > 0)
                {
                    var subscriptions = model.AgencySubscriptions.Where(x => x.AgencyId == agency.Id);
                    model.Subscription = subscriptions.Last();

                    if (!string.IsNullOrWhiteSpace(model.Subscription.PriceId))
                    {
                        await _unitOfWork.ShopPrices.GetPriceForAsync(model.Subscription.GetCore());
                        await _unitOfWork.SharedPeriods.GetPeriodForAsync(model.Subscription.Price.GetCore());
                        await _unitOfWork.ShopProducts.GetProductForAsync(model.Subscription.Price.GetCore());
                    }
                }



                await model.SetStatusAsync(_unitOfWork);



                string html = await _viewToString.PartialAsync("Shared/Business/Client/_SummaryRowContent.cshtml", model);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return BadRequest();
            }
        }
    }
}