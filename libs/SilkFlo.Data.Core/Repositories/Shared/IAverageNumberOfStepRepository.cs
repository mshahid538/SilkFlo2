using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IAverageNumberOfStepRepository
  {
    bool IncludeDeleted { get; set; }

    Task<AverageNumberOfStep> GetAsync(string id);

    Task<AverageNumberOfStep> SingleOrDefaultAsync(Func<AverageNumberOfStep, bool> predicate);

    Task<bool> AddAsync(AverageNumberOfStep entity);

    Task<bool> AddRangeAsync(IEnumerable<AverageNumberOfStep> entities);

    Task<IEnumerable<AverageNumberOfStep>> GetAllAsync();

    Task<IEnumerable<AverageNumberOfStep>> FindAsync(Func<AverageNumberOfStep, bool> predicate);

    Task<AverageNumberOfStep> GetUsingNameAsync(string name);

    Task GetAverageNumberOfStepForAsync(Idea idea);

    Task GetAverageNumberOfStepForAsync(IEnumerable<Idea> ideas);

    Task<AverageNumberOfStep> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(AverageNumberOfStep entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<AverageNumberOfStep> entities);
  }
}
