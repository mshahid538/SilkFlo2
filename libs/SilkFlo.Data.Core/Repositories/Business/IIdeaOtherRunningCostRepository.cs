using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IIdeaOtherRunningCostRepository
  {
    bool IncludeDeleted { get; set; }

    Task<IdeaOtherRunningCost> GetAsync(string id);

    Task<IdeaOtherRunningCost> SingleOrDefaultAsync(Func<IdeaOtherRunningCost, bool> predicate);

    Task<bool> AddAsync(IdeaOtherRunningCost entity);

    Task<bool> AddRangeAsync(IEnumerable<IdeaOtherRunningCost> entities);

    Task<IEnumerable<IdeaOtherRunningCost>> GetAllAsync();

    Task<IEnumerable<IdeaOtherRunningCost>> FindAsync(Func<IdeaOtherRunningCost, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForIdeaAsync(Idea idea);

    Task GetForIdeaAsync(IEnumerable<Idea> ideas);

    Task GetForOtherRunningCostAsync(OtherRunningCost otherRunningCost);

    Task GetForOtherRunningCostAsync(IEnumerable<OtherRunningCost> otherRunningCosts);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(IdeaOtherRunningCost entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaOtherRunningCost> entities);
  }
}
