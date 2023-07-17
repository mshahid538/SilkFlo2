using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface ICostTypeRepository
  {
    bool IncludeDeleted { get; set; }

    Task<CostType> GetAsync(string id);

    Task<CostType> SingleOrDefaultAsync(Func<CostType, bool> predicate);

    Task<bool> AddAsync(CostType entity);

    Task<bool> AddRangeAsync(IEnumerable<CostType> entities);

    Task<IEnumerable<CostType>> GetAllAsync();

    Task<IEnumerable<CostType>> FindAsync(Func<CostType, bool> predicate);

    Task<CostType> GetUsingNameAsync(string name);

    Task GetCostTypeForAsync(OtherRunningCost otherRunningCost);

    Task GetCostTypeForAsync(IEnumerable<OtherRunningCost> otherRunningCosts);

    Task<CostType> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(CostType entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<CostType> entities);
  }
}
