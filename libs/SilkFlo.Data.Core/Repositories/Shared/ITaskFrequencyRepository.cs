using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface ITaskFrequencyRepository
  {
    bool IncludeDeleted { get; set; }

    Task<TaskFrequency> GetAsync(string id);

    Task<TaskFrequency> SingleOrDefaultAsync(Func<TaskFrequency, bool> predicate);

    Task<bool> AddAsync(TaskFrequency entity);

    Task<bool> AddRangeAsync(IEnumerable<TaskFrequency> entities);

    Task<IEnumerable<TaskFrequency>> GetAllAsync();

    Task<IEnumerable<TaskFrequency>> FindAsync(Func<TaskFrequency, bool> predicate);

    Task<TaskFrequency> GetUsingNameAsync(string name);

    Task GetTaskFrequencyForAsync(Idea idea);

    Task GetTaskFrequencyForAsync(IEnumerable<Idea> ideas);

    Task<TaskFrequency> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(TaskFrequency entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<TaskFrequency> entities);
  }
}
