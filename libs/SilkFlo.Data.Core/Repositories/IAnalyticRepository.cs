using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories
{
  public interface IAnalyticRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Analytic> GetAsync(string id);

    Task<Analytic> SingleOrDefaultAsync(Func<Analytic, bool> predicate);

    Task<bool> AddAsync(Analytic entity);

    Task<bool> AddRangeAsync(IEnumerable<Analytic> entities);

    Task<IEnumerable<Analytic>> GetAllAsync();

    Task<IEnumerable<Analytic>> FindAsync(Func<Analytic, bool> predicate);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Analytic entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Analytic> entities);
  }
}
