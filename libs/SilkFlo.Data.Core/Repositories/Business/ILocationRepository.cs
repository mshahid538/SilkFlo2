using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface ILocationRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Location> GetAsync(string id);

    Task<Location> SingleOrDefaultAsync(Func<Location, bool> predicate);

    Task<bool> AddAsync(Location entity);

    Task<bool> AddRangeAsync(IEnumerable<Location> entities);

    Task<IEnumerable<Location>> GetAllAsync();

    Task<IEnumerable<Location>> FindAsync(Func<Location, bool> predicate);

    Task<Location> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetLocationForAsync(User user);

    Task GetLocationForAsync(IEnumerable<User> users);

    Task<Location> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Location entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Location> entities);
  }
}
