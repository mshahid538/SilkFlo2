using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IIndustryRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Industry> GetAsync(string id);

    Task<Industry> SingleOrDefaultAsync(Func<Industry, bool> predicate);

    Task<bool> AddAsync(Industry entity);

    Task<bool> AddRangeAsync(IEnumerable<Industry> entities);

    Task<IEnumerable<Industry>> GetAllAsync();

    Task<IEnumerable<Industry>> FindAsync(Func<Industry, bool> predicate);

    Task<Industry> GetUsingNameAsync(string name);

    Task GetIndustryForAsync(Client client);

    Task GetIndustryForAsync(IEnumerable<Client> clients);

    Task<Industry> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Industry entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Industry> entities);
  }
}
