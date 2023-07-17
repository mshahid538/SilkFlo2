using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IPeriodRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Period> GetAsync(string id);

    Task<Period> SingleOrDefaultAsync(Func<Period, bool> predicate);

    Task<bool> AddAsync(Period entity);

    Task<bool> AddRangeAsync(IEnumerable<Period> entities);

    Task<IEnumerable<Period>> GetAllAsync();

    Task<IEnumerable<Period>> FindAsync(Func<Period, bool> predicate);

    Task<Period> GetUsingNameAsync(string name);

    Task GetFrequencyForAsync(OtherRunningCost otherRunningCost);

    Task GetFrequencyForAsync(IEnumerable<OtherRunningCost> otherRunningCosts);

    Task GetPeriodForAsync(Price price);

    Task GetPeriodForAsync(IEnumerable<Price> prices);

    Task GetFrequencyForAsync(RunningCost runningCost);

    Task GetFrequencyForAsync(IEnumerable<RunningCost> runningCosts);

    Task<Period> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Period entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Period> entities);
  }
}
