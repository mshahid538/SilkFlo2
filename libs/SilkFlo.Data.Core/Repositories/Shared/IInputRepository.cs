using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IInputRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Input> GetAsync(string id);

    Task<Input> SingleOrDefaultAsync(Func<Input, bool> predicate);

    Task<bool> AddAsync(Input entity);

    Task<bool> AddRangeAsync(IEnumerable<Input> entities);

    Task<IEnumerable<Input>> GetAllAsync();

    Task<IEnumerable<Input>> FindAsync(Func<Input, bool> predicate);

    Task<Input> GetUsingNameAsync(string name);

    Task GetInputForAsync(Idea idea);

    Task GetInputForAsync(IEnumerable<Idea> ideas);

    Task<Input> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Input entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Input> entities);
  }
}
