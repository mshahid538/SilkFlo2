using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories
{
  public interface ILogRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Log> GetAsync(string id);

    Task<Log> SingleOrDefaultAsync(Func<Log, bool> predicate);

    Task<bool> AddAsync(Log entity);

    Task<bool> AddRangeAsync(IEnumerable<Log> entities);

    Task<IEnumerable<Log>> GetAllAsync();

    Task<IEnumerable<Log>> FindAsync(Func<Log, bool> predicate);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Log entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Log> entities);
  }
}
