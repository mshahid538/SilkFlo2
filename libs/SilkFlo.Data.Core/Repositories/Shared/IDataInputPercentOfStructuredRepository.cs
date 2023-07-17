using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IDataInputPercentOfStructuredRepository
  {
    bool IncludeDeleted { get; set; }

    Task<DataInputPercentOfStructured> GetAsync(string id);

    Task<DataInputPercentOfStructured> SingleOrDefaultAsync(
      Func<DataInputPercentOfStructured, bool> predicate);

    Task<bool> AddAsync(DataInputPercentOfStructured entity);

    Task<bool> AddRangeAsync(IEnumerable<DataInputPercentOfStructured> entities);

    Task<IEnumerable<DataInputPercentOfStructured>> GetAllAsync();

    Task<IEnumerable<DataInputPercentOfStructured>> FindAsync(
      Func<DataInputPercentOfStructured, bool> predicate);

    Task<DataInputPercentOfStructured> GetUsingNameAsync(string name);

    Task GetDataInputPercentOfStructuredForAsync(Idea idea);

    Task GetDataInputPercentOfStructuredForAsync(IEnumerable<Idea> ideas);

    Task<DataInputPercentOfStructured> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(DataInputPercentOfStructured entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<DataInputPercentOfStructured> entities);
  }
}
