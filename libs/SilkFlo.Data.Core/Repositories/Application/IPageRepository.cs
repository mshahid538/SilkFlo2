using SilkFlo.Data.Core.Domain.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Application
{
  public interface IPageRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Page> GetAsync(string id);

    Task<Page> SingleOrDefaultAsync(Func<Page, bool> predicate);

    Task<bool> AddAsync(Page entity);

    Task<bool> AddRangeAsync(IEnumerable<Page> entities);

    Task<IEnumerable<Page>> GetAllAsync();

    Task<IEnumerable<Page>> FindAsync(Func<Page, bool> predicate);

    Task<Page> GetUsingNameAsync(string name);

    Task<Page> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Page entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Page> entities);
  }
}
