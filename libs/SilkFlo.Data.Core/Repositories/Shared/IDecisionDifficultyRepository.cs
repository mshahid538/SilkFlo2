using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IDecisionDifficultyRepository
  {
    bool IncludeDeleted { get; set; }

    Task<DecisionDifficulty> GetAsync(string id);

    Task<DecisionDifficulty> SingleOrDefaultAsync(Func<DecisionDifficulty, bool> predicate);

    Task<bool> AddAsync(DecisionDifficulty entity);

    Task<bool> AddRangeAsync(IEnumerable<DecisionDifficulty> entities);

    Task<IEnumerable<DecisionDifficulty>> GetAllAsync();

    Task<IEnumerable<DecisionDifficulty>> FindAsync(Func<DecisionDifficulty, bool> predicate);

    Task<DecisionDifficulty> GetUsingNameAsync(string name);

    Task GetDecisionDifficultyForAsync(Idea idea);

    Task GetDecisionDifficultyForAsync(IEnumerable<Idea> ideas);

    Task<DecisionDifficulty> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(DecisionDifficulty entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<DecisionDifficulty> entities);
  }
}
