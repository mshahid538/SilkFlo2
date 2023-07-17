using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SilkFlo.Data.Core.Repositories
{
  public interface IRoleRepository
  {
    bool IncludeDeleted { get; set; }
    Task<Role> GetAsync(string id);

    Task<Role> SingleOrDefaultAsync(Func<Role, bool> predicate);

    Task<bool> AddAsync(Role entity);

    Task<bool> AddRangeAsync(IEnumerable<Role> entities);

    Task<IEnumerable<Role>> GetAllAsync();

    Task<IEnumerable<Role>> FindAsync(Func<Role, bool> predicate);

    Task<Role> GetUsingNameAsync(string name);

    Task GetRoleForAsync(UserRole userRole);

    Task GetRoleForAsync(IEnumerable<UserRole> userRoles);

    Task<Role> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Role entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Role> entities);

    Task<bool> IsMemberAsync(string userId, string roleName);

    Task<bool> IsMemberAsync(User user, string roleName);
  }
}
