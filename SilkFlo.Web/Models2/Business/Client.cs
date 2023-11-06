using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Shop;

//using sib_api_v3_sdk.Api;
//using sib_api_v3_sdk.Client;
//using sib_api_v3_sdk.Model;
//using Task = System.Threading.Tasks.Task;

namespace SilkFlo.Web.Models.Business
{
    public partial class Client
    {
        public bool IsAgency =>
            TypeId == Enumerators.ClientType.ReferrerAgency41.ToString()
            || TypeId == Enumerators.ClientType.ResellerAgency45.ToString();

        public Shop.Subscription Subscription { get; set; }

        public SubscriptionStatus SubscriptionStatus { get; set; } = SubscriptionStatus.NoSubscription;

        public string StatusMessage { get; set; }

        public string Tab { get; set; }

        public async Task SetStatusAsync(IUnitOfWork unitOfWork)
        {
            var core = GetCore();
            await unitOfWork.ShopSubscriptions.GetForTenantAsync(core);

            if (!core.TenantSubscriptions.Any())
            {
                SubscriptionStatus = SubscriptionStatus.NoSubscription;
                StatusMessage = "There are no subscriptions assigned to this client";
                return;
            }

            var last = core.TenantSubscriptions.Last();

            Subscription = new Shop.Subscription(last);


            if (Subscription is not { IsActive: true })
            {
                SubscriptionStatus = SubscriptionStatus.NoSubscription;
                StatusMessage = "The subscription was cancelled or has expired";
                return;
            }



            if (string.IsNullOrWhiteSpace(Subscription.InvoiceId))
            {
                var setting = new Models.Application.Setting(unitOfWork);

                if (Subscription.IsFree)
                {
                    if (Subscription.DateEnd == null)
                        SubscriptionStatus = SubscriptionStatus.Demo;
                    else
                        SubscriptionStatus = SubscriptionStatus.FreeTrial;

                    StatusMessage = "The client in trial period";
                    return;
                }

                SubscriptionStatus = SubscriptionStatus.PaymentRequired;
                StatusMessage = "Awaiting client payment";
                return;
            }

            SubscriptionStatus = SubscriptionStatus.Subscribed;
            StatusMessage = "The client is up and running";
        }



        public async Task SetupTenantAsync(IUnitOfWork unitOfWork)
        {
            var businessRoles = await unitOfWork.BusinessRoles.FindAsync(x => x.ClientId == Id);

            await unitOfWork.BusinessRoles.RemoveRangeAsync(businessRoles);

            // Delete old
            var old = await unitOfWork.BusinessRoleIdeaAuthorisations.FindAsync(x => x.ClientId == Id);
            await unitOfWork.BusinessRoleIdeaAuthorisations.RemoveRangeAsync(old);


            var businessRoleCustom = new Data.Core.Domain.Business.BusinessRole
            {
                Name = "Business Analyst",
                Sort = 0,
                Client = this.GetCore(),
            };
            await unitOfWork.AddAsync(businessRoleCustom);
            await AddRoleIdeaAuthorisationAsync(businessRoleCustom,
                                    Enumerators.IdeaAuthorization.EditAbout.ToString(),
                                    unitOfWork);

            await AddRoleIdeaAuthorisationAsync(businessRoleCustom,
                                                Enumerators.IdeaAuthorization.EditDocumentation.ToString(),
                                                unitOfWork);


            businessRoleCustom = new Data.Core.Domain.Business.BusinessRole
            {
                Name = "Solution Architect",
                Sort = 0,
                Client = this.GetCore(),
            };
            await unitOfWork.AddAsync(businessRoleCustom);
            await AddRoleIdeaAuthorisationAsync(businessRoleCustom,
                                                Enumerators.IdeaAuthorization.EditDocumentation.ToString(),
                                                unitOfWork);



            businessRoleCustom = new Data.Core.Domain.Business.BusinessRole
            {
                Name = "Automation Developer",
                Sort = 0,
                Client = this.GetCore(),
            };
            await unitOfWork.AddAsync(businessRoleCustom);
            await AddRoleIdeaAuthorisationAsync(businessRoleCustom,
                                                Enumerators.IdeaAuthorization.EditDocumentation.ToString(),
                                                unitOfWork);




            businessRoles = await unitOfWork.BusinessRoles.FindAsync(x => x.IsBuiltIn);




            foreach (var businessRole in businessRoles)
            {
                switch (businessRole.Name)
                {
                    case "Employee Idea Submitter":
                        await AddRoleIdeaAuthorisationAsync(businessRole,
                            Enumerators.IdeaAuthorization.EditDocumentation.ToString(),
                            unitOfWork);
                        break;
                    case "Process Owner":
                        await AddRoleIdeaAuthorisationAsync(businessRole,
                            Enumerators.IdeaAuthorization.EditAbout.ToString(),
                            unitOfWork);

                        await AddRoleIdeaAuthorisationAsync(businessRole,
                            Enumerators.IdeaAuthorization.StageAndStatus.ToString(),
                            unitOfWork);

                        await AddRoleIdeaAuthorisationAsync(businessRole,
                            Enumerators.IdeaAuthorization.EditDocumentation.ToString(),
                            unitOfWork);

                        await AddRoleIdeaAuthorisationAsync(businessRole,
                            Enumerators.IdeaAuthorization.ManageCollaborators.ToString(),
                            unitOfWork);
                        break;
                    case "Project Manager":
                        await AddRoleIdeaAuthorisationAsync(businessRole,
                            Enumerators.IdeaAuthorization.EditAbout.ToString(),
                            unitOfWork);

                        await AddRoleIdeaAuthorisationAsync(businessRole,
                            Enumerators.IdeaAuthorization.EditAdvancedSettings.ToString(),
                            unitOfWork);

                        await AddRoleIdeaAuthorisationAsync(businessRole,
                            Enumerators.IdeaAuthorization.StageAndStatus.ToString(),
                            unitOfWork);

                        await AddRoleIdeaAuthorisationAsync(businessRole,
                            Enumerators.IdeaAuthorization.ViewCostBenefit.ToString(),
                            unitOfWork);

                        await AddRoleIdeaAuthorisationAsync(businessRole,
                            Enumerators.IdeaAuthorization.EditCostBenefit.ToString(),
                            unitOfWork);

                        await AddRoleIdeaAuthorisationAsync(businessRole,
                            Enumerators.IdeaAuthorization.EditDocumentation.ToString(),
                            unitOfWork);

                        await AddRoleIdeaAuthorisationAsync(businessRole,
                            Enumerators.IdeaAuthorization.ManageCollaborators.ToString(),
                            unitOfWork);
                        break;
                }
            }



        }

        private async Task AddRoleIdeaAuthorisationAsync(Data.Core.Domain.Business.BusinessRole businessRole,
                                                         string ideaAuthorisationId,
                                                         IUnitOfWork unitOfWork)
        {
            var core = await unitOfWork.BusinessRoleIdeaAuthorisations
                           .SingleOrDefaultAsync(x => x.IdeaAuthorisationId == ideaAuthorisationId
                                                   && x.RoleId == businessRole.Id
                                                   && x.ClientId == Id);
            if (core == null)
            {
                core = new Data.Core.Domain.Business.RoleIdeaAuthorisation
                {
                    IdeaAuthorisationId = ideaAuthorisationId,
                    Role = businessRole,
                    Client = this.GetCore(),
                };

                await unitOfWork.AddAsync(core);
            }
        }



        public List<Shop.Discount> ReferrerDiscountTiers { get; set; } = new();
        public List<Shop.Discount> ResellerDiscountTiers { get; set; } = new();


        public string ReferrerDiscountId { get; set; }
        public string ResellerDiscountId { get; set; }

        public string AffiliateURL { get; set; }
        public string AffiliateLink { get; set; }

        public string AccountOwnerEmail { get; set; }
        public bool AccountOwnerEmail_IsInValid { get; set; }
        public string AccountOwnerEmail_ErrorMessage { get; set; } = "Required";


        public string AccountOwnerFirstName { get; set; }
        public bool AccountOwnerFirstName_IsInValid { get; set; }
        public string AccountOwnerFirstName_ErrorMessage { get; set; } = "Required";


        public string AccountOwnerLastName { get; set; }
        public bool AccountOwnerLastName_IsInValid { get; set; }
        public string AccountOwnerLastName_ErrorMessage { get; set; } = "Required";



        public async Task<ViewModels.Feedback> ValidateAdvancedAsync(
            IUnitOfWork unitOfWork,
            ViewModels.Feedback feedback)
        {
            if (unitOfWork == null)
                throw new NullReferenceException(nameof(unitOfWork));


            if (string.Equals(Name, Data.Core.Settings.ApplicationName, StringComparison.CurrentCultureIgnoreCase))
                feedback.Elements.Add(
                    "Name",
                    "The client cannot be named " + Data.Core.Settings.ApplicationName + ".");

            if (IsNew)
            {
                // Remove website prefix
                if (!string.IsNullOrWhiteSpace(Website))
                {
                    var position = Website.ToLower()
                                        .IndexOf("http://",
                                                 StringComparison.Ordinal);
                    if (position > -1)
                        Website = Website.Substring(position,
                                                    Website.Length - position);

                    position = Website.ToLower()
                                      .IndexOf("https://",
                                               StringComparison.Ordinal);
                    if (position > -1)
                        Website = Website.Substring(position,
                                                    Website.Length - position);

                    position = Website.ToLower()
                                      .IndexOf("www",
                                               StringComparison.Ordinal);
                    if (position > -1)
                        Website = Website.Substring(position,
                                                    Website.Length - position);
                }



                // Is the website online
                if (!string.IsNullOrWhiteSpace(Website)
                    && !string.IsNullOrWhiteSpace(AccountOwnerEmail)
                    && IsNew)
                {
                    AccountOwnerEmail = AccountOwnerEmail.ToLower() + "@" + Website.ToLower();

                    // Validate the Email
                    try
                    {
                        var emailValidationMessage = await Email.Service.ValidateEmailAsync(AccountOwnerEmail);
                        if (!string.IsNullOrWhiteSpace(emailValidationMessage))
                            feedback.Elements.Add(
                                "AccountOwnerEmail",
                                emailValidationMessage);
                    }
                    catch
                    {
                        feedback.Elements.Add(
                            "Website",
                            "Email validation was not possible.");
                    }
                }


                // Validate Account Owner First Name Length
                if (string.IsNullOrWhiteSpace(AccountOwnerFirstName))
                    feedback.Elements.Add(
                        "AccountOwnerFirstName",
                        "Account Owner First Name is required.");
                else if (AccountOwnerFirstName.Length > 100)
                    feedback.Elements.Add(
                        "AccountOwnerFirstName",
                        "Account Owner First Name cannot be greater than 100.");



                // Validate Account Owner Lastname Length
                if (string.IsNullOrWhiteSpace(AccountOwnerLastName))
                    feedback.Elements.Add(
                        "AccountOwnerLastName",
                        "Account Owner Lastname is required.");
                else if (AccountOwnerLastName.Length > 100)
                    feedback.Elements.Add(
                        "AccountOwnerLastName",
                        "Account Owner Lastname cannot be greater than 100.");

                // Validate Account Owner Email Length
                if (string.IsNullOrWhiteSpace(AccountOwnerEmail))
                    feedback.Elements.Add(
                        "AccountOwnerEmail",
                        "Account Owner Email is required.");
                else if (AccountOwnerEmail.Length > 100)
                    feedback.Elements.Add(
                        "AccountOwnerEmail",
                        "Account Owner Email cannot be greater than 100.");
            }
            else
            {
                // Validate Account Owner Id
                if (!string.IsNullOrWhiteSpace(AccountOwnerId))
                {
                    var user = await unitOfWork.Users.GetAsync(AccountOwnerId);

                    if (user == null)
                        feedback.Elements.Add(
                            "AccountOwnerId",
                            "The Account Owner is invalid.");
                }
                else
                {
                    feedback.Elements.Add(
                        "AccountOwnerId",
                        "Account Owner required.");
                }
            }



            if (AverageWorkingDay is < 0 or > 365)
                feedback.Elements.Add(
                    "AverageWorkingDay",
                    "Average Working Day must be greater than 0 and less then 365.");


            if (AverageWorkingHour is < 0 or > 24)
                feedback.Elements.Add(
                    "AverageWorkingHour",
                    "Average Working Hour must be greater than 0 and less then 365.");

            if (feedback.Elements.Any())
                feedback.IsValid = false;

            return feedback;
        }

        public async Task<ViewModels.Feedback> Save(
            IUnitOfWork unitOfWork,
            bool isSilkFloUser,
            bool sendInvite,
            string newAccountOwnerId = null,
            Client agency = null,
            ViewModels.Feedback feedback = null)
        {
            // Guard Clause
            if (IsNew
            && agency == null)
                throw new ArgumentNullException(nameof(agency));


            var isValidState = false;
            if (feedback == null)
                feedback = new ViewModels.Feedback
                {
                    Message = ""
                };
            else
                isValidState = feedback.IsValid;


            Data.Core.Domain.Business.Client core;

            if (IsNew)
            {
                core = new Data.Core.Domain.Business.Client();
            }
            else
            {
                core = await unitOfWork.BusinessClients.GetAsync(Id);

                // Guard Clause
                if (core == null)
                {
                    feedback.DangerMessage("Unauthorised");
                    feedback.IsValid = false;
                    return feedback;
                }
            }





            if (TypeId == Enumerators.ClientType.ReferrerAgency41.ToString())
                core.AgencyDiscountId = ReferrerDiscountId;

            if (TypeId == Enumerators.ClientType.ResellerAgency45.ToString())
                core.AgencyDiscountId = ResellerDiscountId;

            if (TypeId == Enumerators.ClientType.Client39.ToString()
                && isSilkFloUser)
                core.IsDemo = IsDemo;



            core.Name = Name;
            core.IndustryId = IndustryId;
            core.Address1 = Address1;
            core.Address2 = Address2;
            core.City = City;
            core.State = State;
            core.CountryId = CountryId;
            core.PostCode = PostCode;
            core.AverageWorkingDay = AverageWorkingDay;
            core.AverageWorkingHour = AverageWorkingHour;
            core.Website = Website;
            core.CurrencyId = "gbp";
            core.LanguageId = "en-gb";

            if (isSilkFloUser)
            {
                core.IsActive = IsActive; // Only SilkFlo user can change IsActive
                core.TypeId = TypeId;
            }

            if (!IsNew)
                core.AccountOwnerId = AccountOwnerId;

            core.IndustryId = IndustryId;
            core.AverageWorkingDay = AverageWorkingDay;
            core.AverageWorkingHour = AverageWorkingHour;



            // Add/Update Account Owner
            Data.Core.Domain.User user = null;

            if (IsNew && isSilkFloUser)
            {
                user = new Data.Core.Domain.User
                {
                    Email = AccountOwnerEmail,
                    FirstName = AccountOwnerFirstName,
                    LastName = AccountOwnerLastName,
                    Client = core,
                };

                await unitOfWork.AddAsync(user);
                user.Id = newAccountOwnerId;
                user.Client = core;
                core.AccountOwnerId = user.Id;
            }



            user ??= await unitOfWork.Users.GetAsync(AccountOwnerId);

            if (user == null)
            {
                feedback.Elements.Add("AccountOwnerId", "The Account Owner is invalid");
                feedback.IsValid = false;
                return feedback;
            }

            AccountOwner = new User(user);





            if (!IsAgency
             && isSilkFloUser
             && IsNew
             && agency != null)
            {
                var subscription = new Subscription
                {
                    Tenant = core,
                    Agency = agency.GetCore(),
                    DateStart = DateTime.Now.Date,
                    PriceId = null,
                };

                await unitOfWork.AddAsync(subscription);
            }


            var roleId = ((int)Enumerators.Role.AccountOwner).ToString();
            var userRoleFound = false;
            if (!string.IsNullOrWhiteSpace(AccountOwnerId)
                && !IsNew)
            {
                var userRole = await unitOfWork.UserRoles.SingleOrDefaultAsync(x =>
                    x.RoleId == roleId && x.UserId == AccountOwnerId);

                if (userRole != null)
                    userRoleFound = true;
            }


            if (!userRoleFound)
            {
                if (IsNew)
                    await unitOfWork.AddAsync(new Data.Core.Domain.UserRole
                    {
                        RoleId = roleId,
                        User = user
                    });
                else
                    await unitOfWork.AddAsync(new Data.Core.Domain.UserRole
                    {
                        RoleId = roleId,
                        UserId = AccountOwnerId
                    });
            }



            // Add Agency Administrator is agency
            if (TypeId != Enumerators.ClientType.Client39.ToString())
            {
                roleId = ((int)Enumerators.Role.AgencyAdministrator).ToString();
                userRoleFound = false;
                if (!string.IsNullOrWhiteSpace(AccountOwnerId)
                    && !IsNew)
                {
                    var userRole = await unitOfWork.UserRoles.SingleOrDefaultAsync(x =>
                        x.RoleId == roleId && x.UserId == AccountOwnerId);

                    if (userRole != null)
                        userRoleFound = true;
                }


                if (!userRoleFound)
                {
                    if (IsNew)
                        await unitOfWork.AddAsync(new Data.Core.Domain.UserRole
                        {
                            RoleId = roleId,
                            User = user
                        });
                    else
                        await unitOfWork.AddAsync(new Data.Core.Domain.UserRole
                        {
                            RoleId = roleId,
                            UserId = AccountOwnerId
                        });
                }
            }


            var isNew = core.IsNew;

            await unitOfWork.AddAsync(core);
            _core = core;


            if (isNew
                && TypeId != Enumerators.ClientType.ReferrerAgency41.ToString())
            {
                await Insert.PracticeData.CreatePracticeAccountAsync(
                    core,
                    unitOfWork,
                    false,
                    AccountOwner.GetCore());
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(core.StripeId))
                    await SilkFlo.Web.Services2.Models.PaymentManager.SaveAsync(core); // Payment.Manager.SaveAsync(core);
            }

            await unitOfWork.CompleteAsync();

            if (sendInvite)
                await SendInvitation(
                    new User(user));

            feedback.IsValid = feedback.Elements.Count == 0 && isValidState;

            return feedback;
        }



        public async Task<string> SendInvitation(
            User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (user.Client == null)
                throw new ArgumentNullException(nameof(user.Client));


            // Get Template Id
            var templateId = Email.Template.TenantInvitation;
            if (user.Client.TypeId == Enumerators.ClientType.ReferrerAgency41.ToString())
                templateId = Email.Template.ReferrerInvitation;
            else if (user.Client.TypeId == Enumerators.ClientType.ResellerAgency45.ToString())
                templateId = Email.Template.ResellerInvitation;




            // Create Bookmarks
            Email.BookMark[] bookmarks =
            {
                new ("FULLNAME",user.Fullname),
                new ("FIRSTNAME",user.FirstName),
                new ("COMPANYNAME", user.Client.Name),
                new Email.BookmarkLink (
                    "PATH",
                    "/SignUp/userId/" + user.Id,
                    "Click to complete sign up"),
                new Email.BookmarkLink (
                "REFERRER_PATH",
                "/referrer/" + user.Client.Id,
                "")

            };


            // Send
            var message = await Email.Service.SendAsync(
                "Invitation to Complete Sign Up into SilkFlo",
                templateId,
                new Email.MailBox(Data.Core.Settings.ApplicationName,
                    Email.Service.ApplicationEmailAddress),
                new Email.MailBox(user.Fullname,
                    user.Email),
                bookmarks, true);


            return message;
        }

        public async Task<int> GetDiscountPercentAsync(IUnitOfWork unitOfWork)
        {
            if (!IsAgency)
                return 0;

            // Has the Agency Discount been overriden?
            if (!string.IsNullOrWhiteSpace(AgencyDiscountId))
            {
                await unitOfWork.ShopDiscounts.GetAgencyDiscountForAsync(GetCore());

                if (GetCore().AgencyDiscount != null)
                {
                    if (TypeId == Enumerators.ClientType.ResellerAgency45.ToString())
                        return GetCore().AgencyDiscount.PercentReseller ?? 0;

                    return GetCore().AgencyDiscount.PercentReferrer ?? 0;
                }
            }


            // No override therefore calculate

            // Referrer
            if (TypeId == Enumerators.ClientType.ReferrerAgency41.ToString())
            {
                var percent = await unitOfWork
                    .ShopDiscounts
                    .SingleOrDefaultAsync(x => x.Name == "Bronze");


                if (percent == null)
                    return 0;

                return percent.PercentReferrer ?? 0;

            }

            // Reseller
            await unitOfWork.ShopSubscriptions.GetForAgencyAsync(GetCore());

            if (!AgencySubscriptions.Any())
            {
                var percent2 = await unitOfWork.ShopDiscounts.SingleOrDefaultAsync(x => x.Name == "Bronze");

                if (percent2 == null)
                    return 0;

                return percent2.PercentReseller ?? 0;
            }




            decimal amount = 0;
            foreach (var agencySubscription in AgencySubscriptions)
            {
                var stripeInvoice = await SilkFlo.Web.Services2.Models.PaymentManager.GetInvoiceAsync(agencySubscription.InvoiceId); // Payment.Manager.GetInvoiceAsync(agencySubscription.InvoiceId);
                if (stripeInvoice == null)
                    continue;

                amount += stripeInvoice.AmountPaid;
            }

            var percent3 = await unitOfWork.ShopDiscounts
                .SingleOrDefaultAsync(x => (x.Min ?? 0) < amount && (x.Max ?? amount) >= amount);

            if (percent3 == null)
                return 0;

            return percent3.PercentReseller ?? 0;

        }

        /// <summary>
        /// Get the latest active subscription.
        /// This is achieved by checking the IsCancelled and DateEnd fields.
        /// </summary>
        /// <param name="unitOfWork">as instance of IUnitOfWork</param>
        /// <param name="isActive"></param>
        /// <returns>The active Shop.Subscription</returns>
        public async Task<Shop.Subscription> GetLastSubscriptionAsync(
            IUnitOfWork unitOfWork,
            bool isActive = true)
        {
            // Guard Clause
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            // If the client a reseller agency? 
            if (TypeId == Enumerators.ClientType.ResellerAgency45.ToString())
                return null;


            // Do the business
            var now = DateTime.Now;
            await unitOfWork.ShopSubscriptions.GetForTenantAsync(GetCore());

            var subscriptions =
                Shop.Subscription
                    .Create(GetCore()
                    .TenantSubscriptions);

            if (isActive)
            {
                var subscription =
                    subscriptions
                        .LastOrDefault(x =>
                            x.IsActive);

                return subscription;
            }


            var item =
                subscriptions
                    .LastOrDefault(x =>
                        x.InDate);

            return item;
        }

        public static async Task<Client> CreateAsync(
            IUnitOfWork unitOfWork,
            string id,
            string name,
            string firstName,
            string lastName,
            string email,
            string password,
            string address1,
            string address2,
            string city,
            string state,
            string postCode,
            int freeTrialDay,
            Client agency,
            bool isEmailConfirmed,
            Subscription msSubscription = null)
        {
            #region Guard Clauses
            // Guard Clause
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            // Guard Clause
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            // Guard Clause
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));

            // Guard Clause
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));

            // Guard Clause
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            // Guard Clause
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            // Guard Clause
            if (agency.TypeId != Enumerators.ClientType.ResellerAgency45.ToString())
                throw new ArgumentException(
                    "Supplied agency is not a reseller agency.",
                    nameof(agency));
            #endregion


            var client = await unitOfWork.BusinessClients.GetAsync(id);
            if (client != null)
                return null;



            var message = await Email.Service.ValidateEmailAsync(email);
            if (!string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(
                    message,
                    nameof(email));



            var emailParts = email.Split("@");


            client = new Data.Core.Domain.Business.Client
            {
                Name = name,
                Address1 = address1,
                Address2 = address2,
                City = city,
                State = state,
                PostCode = postCode,
                Website = emailParts[1],
                FreeTrialDay = freeTrialDay,
                CurrencyId = "gbp",
                TypeId = Enumerators.ClientType.Client39.ToString(),
                LanguageId = "en-gb",
                IsActive = true,
            };

            if (msSubscription != null)
            {
                if (client.TenantSubscriptions == null || client.TenantSubscriptions.Count <= 0)
                    client.TenantSubscriptions = new List<Subscription>();

                client.TenantSubscriptions.Add(msSubscription);
            }

            message = await unitOfWork.IsUniqueAsync(client); //Data.Persistence.UnitOfWork.IsUniqueAsync(client);
            if (!string.IsNullOrWhiteSpace(message))
                throw new Exception(
                    message);


            var user = new Data.Core.Domain.User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
            };

            message = await unitOfWork.IsUniqueAsync(user);// Data.Persistence.UnitOfWork.IsUniqueAsync(user);
            if (!string.IsNullOrWhiteSpace(message))
                throw new Exception(
                    message);

            user = await Services.Authorization
                .User
                .CreateAsync(firstName,
                    lastName,
                    email,
                    password,
                    Enumerators.Role.AccountOwner,
                    unitOfWork,
                    isEmailConfirmed);


            client.AccountOwner = user;
            user.Client = client;
            await unitOfWork.AddAsync(client);
            if (!string.IsNullOrWhiteSpace(id))
                client.Id = id;

            await unitOfWork.CompleteAsync();

            return new Client(client);
        }
    }
}