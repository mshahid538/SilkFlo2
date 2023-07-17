using SilkFlo.Data.Core.Domain.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Application
{
  public interface IHotSpotRepository
  {
    bool IncludeDeleted { get; set; }

    Task<HotSpot> GetAsync(string id);

    Task<HotSpot> SingleOrDefaultAsync(Func<HotSpot, bool> predicate);

    Task<bool> AddAsync(HotSpot entity);

    Task<bool> AddRangeAsync(IEnumerable<HotSpot> entities);

    Task<IEnumerable<HotSpot>> GetAllAsync();

    Task<IEnumerable<HotSpot>> FindAsync(Func<HotSpot, bool> predicate);

    Task<HotSpot> GetUsingNameAsync(string name);

    Task<HotSpot> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(HotSpot entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<HotSpot> entities);
  }
}
