using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface ISoftwareVenderRepository
  {
    bool IncludeDeleted { get; set; }

    Task<SoftwareVender> GetAsync(string id);

    Task<SoftwareVender> SingleOrDefaultAsync(Func<SoftwareVender, bool> predicate);

    Task<bool> AddAsync(SoftwareVender entity);

    Task<bool> AddRangeAsync(IEnumerable<SoftwareVender> entities);

    Task<IEnumerable<SoftwareVender>> GetAllAsync();

    Task<IEnumerable<SoftwareVender>> FindAsync(Func<SoftwareVender, bool> predicate);

    Task<SoftwareVender> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetVenderForAsync(RunningCost runningCost);

    Task GetVenderForAsync(IEnumerable<RunningCost> runningCosts);

    Task<SoftwareVender> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(SoftwareVender entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<SoftwareVender> entities);
  }
}
