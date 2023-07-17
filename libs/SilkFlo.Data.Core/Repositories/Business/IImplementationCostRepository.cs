using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IImplementationCostRepository
  {
    bool IncludeDeleted { get; set; }

    Task<ImplementationCost> GetAsync(string id);

    Task<ImplementationCost> SingleOrDefaultAsync(Func<ImplementationCost, bool> predicate);

    Task<bool> AddAsync(ImplementationCost entity);

    Task<bool> AddRangeAsync(IEnumerable<ImplementationCost> entities);

    Task<IEnumerable<ImplementationCost>> GetAllAsync();

    Task<IEnumerable<ImplementationCost>> FindAsync(Func<ImplementationCost, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForIdeaStageAsync(IdeaStage ideaStage);

    Task GetForIdeaStageAsync(IEnumerable<IdeaStage> ideaStages);

    Task GetForRoleAsync(Role role);

    Task GetForRoleAsync(IEnumerable<Role> roles);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(ImplementationCost entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ImplementationCost> entities);
  }
}
