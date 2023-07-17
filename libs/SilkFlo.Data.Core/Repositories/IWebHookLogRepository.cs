using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories
{
  public interface IWebHookLogRepository
  {
    bool IncludeDeleted { get; set; }

    Task<WebHookLog> GetAsync(string id);

    Task<WebHookLog> SingleOrDefaultAsync(Func<WebHookLog, bool> predicate);

    Task<bool> AddAsync(WebHookLog entity);

    Task<bool> AddRangeAsync(IEnumerable<WebHookLog> entities);

    Task<IEnumerable<WebHookLog>> GetAllAsync();

    Task<IEnumerable<WebHookLog>> FindAsync(Func<WebHookLog, bool> predicate);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(WebHookLog entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<WebHookLog> entities);
  }
}
