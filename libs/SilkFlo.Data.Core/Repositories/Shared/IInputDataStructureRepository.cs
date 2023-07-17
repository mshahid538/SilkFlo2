using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IInputDataStructureRepository
  {
    bool IncludeDeleted { get; set; }

    Task<InputDataStructure> GetAsync(string id);

    Task<InputDataStructure> SingleOrDefaultAsync(Func<InputDataStructure, bool> predicate);

    Task<bool> AddAsync(InputDataStructure entity);

    Task<bool> AddRangeAsync(IEnumerable<InputDataStructure> entities);

    Task<IEnumerable<InputDataStructure>> GetAllAsync();

    Task<IEnumerable<InputDataStructure>> FindAsync(Func<InputDataStructure, bool> predicate);

    Task<InputDataStructure> GetUsingNameAsync(string name);

    Task GetInputDataStructureForAsync(Idea idea);

    Task GetInputDataStructureForAsync(IEnumerable<Idea> ideas);

    Task<InputDataStructure> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(InputDataStructure entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<InputDataStructure> entities);
  }
}
