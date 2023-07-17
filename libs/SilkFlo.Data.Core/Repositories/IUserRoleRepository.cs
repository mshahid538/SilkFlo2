using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories
{
  public interface IUserRoleRepository
  {
    bool IncludeDeleted { get; set; }

    Task<UserRole> GetAsync(string id);

    Task<UserRole> SingleOrDefaultAsync(Func<UserRole, bool> predicate);

    Task<bool> AddAsync(UserRole entity);

    Task<bool> AddRangeAsync(IEnumerable<UserRole> entities);

    Task<IEnumerable<UserRole>> GetAllAsync();

    Task<IEnumerable<UserRole>> FindAsync(Func<UserRole, bool> predicate);

    Task GetForRoleAsync(Role role);

    Task GetForRoleAsync(IEnumerable<Role> roles);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(UserRole entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<UserRole> entities);
  }
}
