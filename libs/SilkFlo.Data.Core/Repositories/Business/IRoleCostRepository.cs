using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IRoleCostRepository
  {
    bool IncludeDeleted { get; set; }

    Task<RoleCost> GetAsync(string id);

    Task<RoleCost> SingleOrDefaultAsync(Func<RoleCost, bool> predicate);

    Task<bool> AddAsync(RoleCost entity);

    Task<bool> AddRangeAsync(IEnumerable<RoleCost> entities);

    Task<IEnumerable<RoleCost>> GetAllAsync();

    Task<IEnumerable<RoleCost>> FindAsync(Func<RoleCost, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForRoleAsync(Role role);

    Task GetForRoleAsync(IEnumerable<Role> roles);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(RoleCost entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<RoleCost> entities);
  }
}
