using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IDecisionCountRepository
  {
    bool IncludeDeleted { get; set; }

    Task<DecisionCount> GetAsync(string id);

    Task<DecisionCount> SingleOrDefaultAsync(Func<DecisionCount, bool> predicate);

    Task<bool> AddAsync(DecisionCount entity);

    Task<bool> AddRangeAsync(IEnumerable<DecisionCount> entities);

    Task<IEnumerable<DecisionCount>> GetAllAsync();

    Task<IEnumerable<DecisionCount>> FindAsync(Func<DecisionCount, bool> predicate);

    Task<DecisionCount> GetUsingNameAsync(string name);

    Task GetDecisionCountForAsync(Idea idea);

    Task GetDecisionCountForAsync(IEnumerable<Idea> ideas);

    Task<DecisionCount> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(DecisionCount entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<DecisionCount> entities);
  }
}
