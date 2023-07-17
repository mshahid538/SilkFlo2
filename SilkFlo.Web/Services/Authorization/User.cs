using System;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Services.Authorization
{
    public class User
    {
        public static async Task<Data.Core.Domain.User> CreateAsync(
            string firstName,
            string lastName,
            string email,
            string password,
            Data.Core.Enumerators.Role roleId,
			Data.Core.IUnitOfWork unitOfWork,
            bool isEmailConfirmed,
            Data.Core.Domain.Business.Client client = null)
        {
            return await CreateAsync(
                firstName,
                lastName,
                email,
                password,
                new Data.Core.Enumerators.Role[] { roleId },
				unitOfWork,
                isEmailConfirmed,
                client);
        }

        public static async Task<Data.Core.Domain.User> CreateAsync(
            string firstName,
            string lastName,
            string email,
            string password,
            Data.Core.Enumerators.Role[] roleIds,
			Data.Core.IUnitOfWork unitOfWork,
            bool isEmailConfirmed,
            Data.Core.Domain.Business.Client client = null)
        {
            var user = await unitOfWork.Users.GetUsingEmailAsync(email);

            if (user != null)
                return user;

            user =
                await
                CreateAsync(
                    firstName,
                    lastName,
                    email,
                    password,
                    unitOfWork,
                    isEmailConfirmed,
                    client);

            foreach (var roleId in roleIds)
                await AddRoleAsync(
                    user,
                    roleId,
                    unitOfWork);


            return user;
        }

        private static async Task<Data.Core.Domain.User> CreateAsync(
            string firstName,
            string lastName,
            string email,
            string password,
            Data.Core.IUnitOfWork unitOfWork,
            bool isEmailConfirmed,
            Data.Core.Domain.Business.Client client = null)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            if (string.IsNullOrEmpty(password))
                return null;

            if (unitOfWork == null)
                return null;

            try
            {
                var user = await unitOfWork.Users.GetUsingEmailAsync(email);
                if (user == null)
                {
                    user = new Data.Core.Domain.User
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        PasswordHash = unitOfWork.GeneratePasswordHash(password),
                        Email = email,
                        IsEmailConfirmed = isEmailConfirmed,
                        Client = client,
                    };

                    if(!isEmailConfirmed)
                        user.EmailConfirmationToken = Guid.NewGuid().ToString();

                    await unitOfWork.Users.AddAsync(user);
                }

                return user;
            }
            catch { }
            return null;
        }

        private static async Task AddRoleAsync(
            Data.Core.Domain.User user,
            Data.Core.Enumerators.Role roleId,
            Data.Core.IUnitOfWork unitOfWork)
        {
            if (user == null)
                return;


            if (unitOfWork == null)
                return;


            var id = ((int)roleId).ToString();
            var userRole = 
                await unitOfWork.UserRoles
                          .SingleOrDefaultAsync(x => x.RoleId == id
                                             && x.UserId == user.Id
                                             && x.IsDeleted == false);
            if (userRole == null)
            {
                userRole = new Data.Core.Domain.UserRole()
                {
                    User = user,
                    RoleId = id,
                    CreatedBy = user
                };

                await unitOfWork.UserRoles.AddAsync(userRole);
            }
        }
    }
}