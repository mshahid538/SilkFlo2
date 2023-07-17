using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core.Domain.Shop;
using SilkFlo.Web.Controllers2.FileUpload;

namespace SilkFlo.Web.Controllers
{
    public abstract partial class AbstractClient : Abstract
    {

        #region Constructors
        protected AbstractClient(Data.Core.IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization)
        {
            Permission = new Permission(AuthorizeAsync);

        }

        protected AbstractClient(Data.Core.IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Permission = new Permission(AuthorizeAsync);
        }

        #endregion

        public Permission Permission { get; }




        #region Methods
        /// <summary>
        /// Get list of tenants,
        /// </summary>
        /// <returns>List of clients</returns>
        public async Task<List<Data.Core.Domain.Business.Client>> GetForUserValidatedAsync(
            Data.Core.Domain.User user)
        {
            // Guard Clause
            if (user == null)
                return new List<Data.Core.Domain.Business.Client>();



            // We have a winner! - An administrator
            var isAdministrator = await DataServices.User.CheckIfAdministrator(_unitOfWork, user);
            if (isAdministrator)
                return (await _unitOfWork.BusinessClients.FindAsync(x => !x.IsPractice))
                    .OrderBy(x => x.Name != Data.Core.Settings.ApplicationName)
                    .ToList();


            var clientCore = await GetSingleOrDefaultValidatedAsync(user);


            if (clientCore == null)
                return new List<Data.Core.Domain.Business.Client>();

            var client = new Models.Business.Client(clientCore);


            // is the client an agency?
            if (!client.IsAgency)
            {
                // Client is a TENANT 
                var clients = new List<Data.Core.Domain.Business.Client>
                {
                    clientCore
                };

                return clients;
            }



            if (client.Name == Data.Core.Settings.ApplicationName)
            {
                // Client is SilkFlo

                if (IsRoleMember(Data.Core.Enumerators.Role.AgencyUser))
                    return (await _unitOfWork.BusinessClients.FindAsync(x => !x.IsPractice))
                        .OrderBy(x => x.Name != Data.Core.Settings.ApplicationName)
                        .ToList();



                // Just return the current client
                return new List<Data.Core.Domain.Business.Client>
                {
                    clientCore
                };
            }


            // We have an agency
            // Get their tenants
            return await GetTenantsAsync(clientCore, user);
        }


        /// <summary>
        /// Get all the tenants for the selected agency
        /// </summary>
        /// <param name="agency"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<List<Data.Core.Domain.Business.Client>> GetTenantsAsync(
            Data.Core.Domain.Business.Client agency,
            Data.Core.Domain.User user)
        {
            if (!IsRoleMember(Data.Core.Enumerators.Role.Administrator)
                && !IsRoleMember(Data.Core.Enumerators.Role.AgencyAdministrator)
                && !IsRoleMember(Data.Core.Enumerators.Role.AgencyUser))
                return null;


            const string logPrefix = "SilkFlo.Web.DataServices.Business.GetTenantsAsync: ";

            // Guard Clause
            if (agency == null)
                throw new ArgumentNullException(logPrefix + "agency parameter missing.");

            // Guard Clause
            if (user == null)
                throw new ArgumentNullException(logPrefix + "user parameter missing.");


            // Do the business
            var now = DateTime.Now;
            var subscriptions = (await _unitOfWork.ShopSubscriptions
                .FindAsync(x => x.AgencyId == agency.Id
                                && x.DateCancelled == null
                                && x.DateStart <= now
                                && (x.DateEnd ?? now) >= now)).ToArray();
            await _unitOfWork.BusinessClients.GetTenantForAsync(subscriptions);

            var tenants = new List<Data.Core.Domain.Business.Client>();

            foreach (var subscription in subscriptions)
            {
                if (subscription.Tenant != null)
                    tenants.Add(subscription.Tenant);
            }



            tenants = tenants.OrderBy(x => x.Name)
                .ToList();

            if (agency.TypeId == Data.Core.Enumerators.ClientType.ResellerAgency45.ToString())
                tenants.Insert(0, agency);


            // Is Administrator? Return client.
            if (IsRoleMember(Data.Core.Enumerators.Role.Administrator)
                || IsRoleMember(Data.Core.Enumerators.Role.AgencyAdministrator))
                return tenants;


            // Filter the tenants for the agency user
            var manageTenants = await _unitOfWork.AgencyManageTenants.FindAsync(x => x.UserId == user.Id);

            tenants = tenants.Where(_ => manageTenants.All(c => c.Id == c.TenantId)).ToList();

            return tenants;
        }



        protected async Task<Currency> GetTenantCurrencyAsync(Data.Core.Domain.Business.Client tenant = null)
        {
            tenant ??= await GetClientAsync();

            if (tenant == null)
                return null;

            Currency currency;

            if (tenant.IsPractice)
            {
                var production = await _unitOfWork.BusinessClients.SingleOrDefaultAsync(x => x.PracticeId == tenant.Id);


                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(production);
                currency = production.Currency;
            }
            else
            {
                await _unitOfWork.ShopCurrencies.GetCurrencyForAsync(tenant);
                currency = tenant.Currency;
            }


            if (currency == null)
                return await _unitOfWork.ShopCurrencies
                    .SingleOrDefaultAsync(x => x.Symbol == "£");

            return currency;
        }


        protected async Task<string> CanAddStandardUser(
            Models.Business.Client client,
            Models.Role[] selectedRoles,
            string userId)
        {
            if (selectedRoles == null)
                return "";

            if (!selectedRoles.Any())
                return "";



            var standardUserSelectedRoles = new List<Models.Role>();
            foreach (var selectedRole in selectedRoles)
            {
                if (!selectedRole.IsSelected)
                    continue;

                if (selectedRole.Id == ((int)Data.Core.Enumerators.Role.StandardUser).ToString())
                    standardUserSelectedRoles.Add(selectedRole);
            }

            if (!standardUserSelectedRoles.Any())
                return "";



            var product = GetProductCookie();



            if (client == null
                || client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString()
                || client.TypeId == Data.Core.Enumerators.ClientType.ResellerAgency45.ToString()
                || product == null)
                return "Cannot add standard users";



            if (product.StandardUserLimit == null)
                return "";

            await _unitOfWork.Users.GetForClientAsync(client.GetCore());
            await _unitOfWork.UserRoles.GetForUserAsync(client.GetCore().Users);

            var users = new List<Models.User>();
            foreach (var user in client.Users)
            {
                if (user.Id == userId)
                    continue;


                var userRole =
                    user.UserRoles
                        .FirstOrDefault(x =>
                            x.RoleId == ((int)Data.Core.Enumerators.Role.StandardUser).ToString());

                if (userRole != null)
                    users.Add(user);
            }


            return product.AdminUserLimit > users.Count
                ? ""
                : $"Standard user limit has been reached. Your subscription is limited to {product.StandardUserLimit}.";
        }


        protected async Task<string> CanAddAuthorisedUser(
            Models.Business.Client client,
            Models.Role[] selectedRoles,
            string userId)
        {
            if (selectedRoles == null)
                return "";

            if (!selectedRoles.Any())
                return "";

            var authorisedUserSelectedRoles = new List<Models.Role>();
            foreach (var selectedRole in selectedRoles)
            {
                if (!selectedRole.IsSelected)
                    continue;

                if (selectedRole.Id == ((int)Data.Core.Enumerators.Role.AccountOwner).ToString()
                    || selectedRole.Id == ((int)Data.Core.Enumerators.Role.ProgramManager).ToString()
                    || selectedRole.Id == ((int)Data.Core.Enumerators.Role.IdeaApprover).ToString()
                    || selectedRole.Id == ((int)Data.Core.Enumerators.Role.AuthorisedUser).ToString()
                    || selectedRole.Id == ((int)Data.Core.Enumerators.Role.RPASponsor).ToString())
                {
                    authorisedUserSelectedRoles.Add(selectedRole);
                }
            }

            if (!authorisedUserSelectedRoles.Any())
                return "";


            if (client == null)
                return "Cannot add admin users";

            if (client.TypeId == Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString())
                return "Cannot add admin users";

            if (client.TypeId == Data.Core.Enumerators.ClientType.ResellerAgency45.ToString())
                return "";


            var product = GetProductCookie();

            if (product == null)
                return "Cannot add admin users";

            if (product.AdminUserLimit == null)
                return "";

            await _unitOfWork.Users.GetForClientAsync(client.GetCore());
            await _unitOfWork.UserRoles.GetForUserAsync(client.GetCore().Users);

            var users = new List<Models.User>();
            foreach (var user in client.Users)
            {
                if (user.Id == userId)
                    continue;

                var userRole =
                    user.UserRoles
                        .FirstOrDefault(x =>
                            x.RoleId == ((int)Data.Core.Enumerators.Role.AccountOwner).ToString()
                            || x.RoleId == ((int)Data.Core.Enumerators.Role.ProgramManager).ToString()
                            || x.RoleId == ((int)Data.Core.Enumerators.Role.IdeaApprover).ToString()
                            || x.RoleId == ((int)Data.Core.Enumerators.Role.AuthorisedUser).ToString()
                            || x.RoleId == ((int)Data.Core.Enumerators.Role.RPASponsor).ToString());

                if (userRole != null)
                    users.Add(user);
            }


            return product.AdminUserLimit > users.Count
                ? ""
                : $"Admin user limit has been reached. Your subscription is limited to {product.AdminUserLimit}.";
        }

        protected string CanAddCollaborator(
            List<Models.Business.Collaborator> selectedModels)
        {
            const string failMessage = "You cannot add any collaborators.";

            if (selectedModels == null)
                return failMessage;


            var product = GetProductCookie();

            if (product == null)
                return failMessage;

            if (product.CollaboratorLimit == null)
                return "";


            return product.CollaboratorLimit < selectedModels.Count
                ? $"Your subscription is limited to {product.CollaboratorLimit} collaborators per idea."
                : "";
        }



        protected int? GetCollaboratorLimit()
        {
            var product = GetProductCookie();

            return product == null ?
                0 :
                product.CollaboratorLimit;
        }

        protected async Task<string> CanAddProcess(
            Models.Business.Client client,
            string customMessage)
        {
            if (client == null)
                return "Client object is missing.";


            var product = GetProductCookie();

            if (product == null)
                return "You cannot add an idea.";

            if (product.IdeaLimit == null)
                return "";

            await _unitOfWork.BusinessIdeas.GetForClientAsync(client.GetCore());
            await _unitOfWork.BusinessIdeaStages.GetForIdeaAsync(client.GetCore().Ideas);



            var ideas =
                client.Ideas
                    .Where(x => x.LastIdeaStage?.Id != Data.Core.Enumerators.Stage.n00_Idea.ToString());

            return product.IdeaLimit < ideas.Count() ?
                $"{customMessage}<br/>Your current plan is limited to {product.IdeaLimit} processes." :
                "";
        }



        #endregion
    }
}