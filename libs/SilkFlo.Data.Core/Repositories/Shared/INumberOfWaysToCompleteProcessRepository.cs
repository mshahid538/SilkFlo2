using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface INumberOfWaysToCompleteProcessRepository
  {
    bool IncludeDeleted { get; set; }

    Task<NumberOfWaysToCompleteProcess> GetAsync(string id);

    Task<NumberOfWaysToCompleteProcess> SingleOrDefaultAsync(
      Func<NumberOfWaysToCompleteProcess, bool> predicate);

    Task<bool> AddAsync(NumberOfWaysToCompleteProcess entity);

    Task<bool> AddRangeAsync(
      IEnumerable<NumberOfWaysToCompleteProcess> entities);

    Task<IEnumerable<NumberOfWaysToCompleteProcess>> GetAllAsync();

    Task<IEnumerable<NumberOfWaysToCompleteProcess>> FindAsync(
      Func<NumberOfWaysToCompleteProcess, bool> predicate);

    Task<NumberOfWaysToCompleteProcess> GetUsingNameAsync(string name);

    Task GetNumberOfWaysToCompleteProcessForAsync(Idea idea);

    Task GetNumberOfWaysToCompleteProcessForAsync(IEnumerable<Idea> ideas);

    Task<NumberOfWaysToCompleteProcess> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(NumberOfWaysToCompleteProcess entity);

    Task<DataStoreResult> RemoveRangeAsync(
      IEnumerable<NumberOfWaysToCompleteProcess> entities);
  }
}
