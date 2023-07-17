using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IAutomationTypeRepository
  {
    bool IncludeDeleted { get; set; }

    Task<AutomationType> GetAsync(string id);

    Task<AutomationType> SingleOrDefaultAsync(Func<AutomationType, bool> predicate);

    Task<bool> AddAsync(AutomationType entity);

    Task<bool> AddRangeAsync(IEnumerable<AutomationType> entities);

    Task<IEnumerable<AutomationType>> GetAllAsync();

    Task<IEnumerable<AutomationType>> FindAsync(Func<AutomationType, bool> predicate);

    Task<AutomationType> GetUsingNameAsync(string name);

    Task GetAutomationTypeForAsync(RunningCost runningCost);

    Task GetAutomationTypeForAsync(IEnumerable<RunningCost> runningCosts);

    Task<AutomationType> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(AutomationType entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<AutomationType> entities);
  }
}
