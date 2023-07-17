using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IIdeaStatusRepository
  {
    bool IncludeDeleted { get; set; }

    Task<IdeaStatus> GetAsync(string id);

    Task<IdeaStatus> SingleOrDefaultAsync(Func<IdeaStatus, bool> predicate);

    Task<bool> AddAsync(IdeaStatus entity);

    Task<bool> AddRangeAsync(IEnumerable<IdeaStatus> entities);

    Task<IEnumerable<IdeaStatus>> GetAllAsync();

    Task<IEnumerable<IdeaStatus>> FindAsync(Func<IdeaStatus, bool> predicate);

    Task<IdeaStatus> GetUsingNameAsync(string name);

    Task GetForStageAsync(Stage stage);

    Task GetForStageAsync(IEnumerable<Stage> stages);

    Task GetStatusForAsync(IdeaStageStatus ideaStageStatus);

    Task GetStatusForAsync(IEnumerable<IdeaStageStatus> ideaStageStatuses);

    Task<IdeaStatus> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(IdeaStatus entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaStatus> entities);
  }
}
