using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IIdeaRunningCostRepository
  {
    bool IncludeDeleted { get; set; }

    Task<IdeaRunningCost> GetAsync(string id);

    Task<IdeaRunningCost> SingleOrDefaultAsync(Func<IdeaRunningCost, bool> predicate);

    Task<bool> AddAsync(IdeaRunningCost entity);

    Task<bool> AddRangeAsync(IEnumerable<IdeaRunningCost> entities);

    Task<IEnumerable<IdeaRunningCost>> GetAllAsync();

    Task<IEnumerable<IdeaRunningCost>> FindAsync(Func<IdeaRunningCost, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForIdeaAsync(Idea idea);

    Task GetForIdeaAsync(IEnumerable<Idea> ideas);

    Task GetForRunningCostAsync(RunningCost runningCost);

    Task GetForRunningCostAsync(IEnumerable<RunningCost> runningCosts);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(IdeaRunningCost entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaRunningCost> entities);
  }
}
