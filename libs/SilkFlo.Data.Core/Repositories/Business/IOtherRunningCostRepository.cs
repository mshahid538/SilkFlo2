using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IOtherRunningCostRepository
  {
    bool IncludeDeleted { get; set; }

    Task<OtherRunningCost> GetAsync(string id);

    Task<OtherRunningCost> SingleOrDefaultAsync(Func<OtherRunningCost, bool> predicate);

    Task<bool> AddAsync(OtherRunningCost entity);

    Task<bool> AddRangeAsync(IEnumerable<OtherRunningCost> entities);

    Task<IEnumerable<OtherRunningCost>> GetAllAsync();

    Task<IEnumerable<OtherRunningCost>> FindAsync(Func<OtherRunningCost, bool> predicate);

    Task<OtherRunningCost> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForCostTypeAsync(CostType costType);

    Task GetForCostTypeAsync(IEnumerable<CostType> costTypes);

    Task GetForFrequencyAsync(Period frequency);

    Task GetForFrequencyAsync(IEnumerable<Period> frequencies);

    Task GetOtherRunningCostForAsync(IdeaOtherRunningCost ideaOtherRunningCost);

    Task GetOtherRunningCostForAsync(
      IEnumerable<IdeaOtherRunningCost> ideaOtherRunningCosts);

    Task<OtherRunningCost> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(OtherRunningCost entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<OtherRunningCost> entities);
  }
}
