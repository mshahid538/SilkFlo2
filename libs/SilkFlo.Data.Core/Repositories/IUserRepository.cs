using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories
{
  public interface IUserRepository
  {
    bool IncludeDeleted { get; set; }

    Task<User> GetAsync(string id);

    Task<User> SingleOrDefaultAsync(Func<User, bool> predicate);

    Task<bool> AddAsync(User entity);

    Task<bool> AddRangeAsync(IEnumerable<User> entities);

    Task<IEnumerable<User>> GetAllAsync();

    Task<IEnumerable<User>> FindAsync(Func<User, bool> predicate);

    Task<User> GetUsingEmailAsync(string email);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForDepartmentAsync(Department department);

    Task GetForDepartmentAsync(IEnumerable<Department> departments);

    Task GetForLocationAsync(Location location);

    Task GetForLocationAsync(IEnumerable<Location> locations);

    Task GetForManagerAsync(User manager);

    Task GetForManagerAsync(IEnumerable<User> managers);

    Task GetUserForAsync(Analytic analytic);

    Task GetUserForAsync(IEnumerable<Analytic> analytics);

    Task GetAccountOwnerForAsync(Client client);

    Task GetAccountOwnerForAsync(IEnumerable<Client> clients);

    Task GetUserForAsync(Collaborator collaborator);

    Task GetUserForAsync(IEnumerable<Collaborator> collaborators);

    Task GetInvitedByForAsync(Collaborator collaborator);

    Task GetInvitedByForAsync(IEnumerable<Collaborator> collaborators);

    Task GetSenderForAsync(Comment comment);

    Task GetSenderForAsync(IEnumerable<Comment> comments);

    Task GetUserForAsync(Follow follow);

    Task GetUserForAsync(IEnumerable<Follow> follows);

    Task GetProcessOwnerForAsync(Idea idea);

    Task GetProcessOwnerForAsync(IEnumerable<Idea> ideas);

    Task GetUserForAsync(ManageTenant manageTenant);

    Task GetUserForAsync(IEnumerable<ManageTenant> manageTenants);

    Task GetUserForAsync(Message message);

    Task GetUserForAsync(IEnumerable<Message> messages);

    Task GetUserForAsync(Recipient recipient);

    Task GetUserForAsync(IEnumerable<Recipient> recipients);

    Task GetManagerForAsync(User user);

    Task GetManagerForAsync(IEnumerable<User> users);

    Task GetUserForAsync(UserAchievement userAchievement);

    Task GetUserForAsync(IEnumerable<UserAchievement> userAchievements);

    Task GetUserForAsync(UserAuthorisation userAuthorisation);

    Task GetUserForAsync(IEnumerable<UserAuthorisation> userAuthorisations);

    Task GetUserForAsync(UserBadge userBadge);

    Task GetUserForAsync(IEnumerable<UserBadge> userBadges);

    Task GetUserForAsync(UserRole userRole);

    Task GetUserForAsync(IEnumerable<UserRole> userRoles);

    Task GetUserForAsync(Vote vote);

    Task GetUserForAsync(IEnumerable<Vote> votes);

    Task<User> GetByEmailAsync(string email);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(User entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<User> entities);
  }
}
