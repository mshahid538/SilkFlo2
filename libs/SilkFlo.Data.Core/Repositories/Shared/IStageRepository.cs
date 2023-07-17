using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IStageRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Stage> GetAsync(string id);

    Task<Stage> SingleOrDefaultAsync(Func<Stage, bool> predicate);

    Task<bool> AddAsync(Stage entity);

    Task<bool> AddRangeAsync(IEnumerable<Stage> entities);

    Task<IEnumerable<Stage>> GetAllAsync();

    Task<IEnumerable<Stage>> FindAsync(Func<Stage, bool> predicate);

    Task<Stage> GetUsingNameAsync(string name);

    Task GetForStageGroupAsync(StageGroup stageGroup);

    Task GetForStageGroupAsync(IEnumerable<StageGroup> stageGroups);

    Task GetStageForAsync(IdeaStage ideaStage);

    Task GetStageForAsync(IEnumerable<IdeaStage> ideaStages);

    Task GetStageForAsync(IdeaStatus ideaStatus);

    Task GetStageForAsync(IEnumerable<IdeaStatus> ideaStatuses);

    Task<Stage> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Stage entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Stage> entities);
  }
}
