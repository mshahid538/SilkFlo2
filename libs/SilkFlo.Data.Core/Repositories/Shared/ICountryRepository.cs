using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface ICountryRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Country> GetAsync(string id);

    Task<Country> SingleOrDefaultAsync(Func<Country, bool> predicate);

    Task<bool> AddAsync(Country entity);

    Task<bool> AddRangeAsync(IEnumerable<Country> entities);

    Task<IEnumerable<Country>> GetAllAsync();

    Task<IEnumerable<Country>> FindAsync(Func<Country, bool> predicate);

    Task<Country> GetUsingNameAsync(string name);

    Task GetCountryForAsync(Client client);

    Task GetCountryForAsync(IEnumerable<Client> clients);

    Task GetCountryForAsync(Prospect prospect);

    Task GetCountryForAsync(IEnumerable<Prospect> prospects);

    Task<Country> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Country entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Country> entities);
  }
}
