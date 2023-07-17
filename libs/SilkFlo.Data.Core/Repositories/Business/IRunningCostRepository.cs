using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IRunningCostRepository
  {
    bool IncludeDeleted { get; set; }

    Task<RunningCost> GetAsync(string id);

    Task<RunningCost> SingleOrDefaultAsync(Func<RunningCost, bool> predicate);

    Task<bool> AddAsync(RunningCost entity);

    Task<bool> AddRangeAsync(IEnumerable<RunningCost> entities);

    Task<IEnumerable<RunningCost>> GetAllAsync();

    Task<IEnumerable<RunningCost>> FindAsync(Func<RunningCost, bool> predicate);

    Task GetForAutomationTypeAsync(AutomationType automationType);

    Task GetForAutomationTypeAsync(IEnumerable<AutomationType> automationTypes);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForFrequencyAsync(Period frequency);

    Task GetForFrequencyAsync(IEnumerable<Period> frequencies);

    Task GetForVenderAsync(SoftwareVender vender);

    Task GetForVenderAsync(IEnumerable<SoftwareVender> venders);

    Task GetRunningCostForAsync(Idea idea);

    Task GetRunningCostForAsync(IEnumerable<Idea> ideas);

    Task GetRunningCostForAsync(IdeaRunningCost ideaRunningCost);

    Task GetRunningCostForAsync(IEnumerable<IdeaRunningCost> ideaRunningCosts);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(RunningCost entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<RunningCost> entities);
  }
}
