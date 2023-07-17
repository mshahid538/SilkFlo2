using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IProcessRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Process> GetAsync(string id);

    Task<Process> SingleOrDefaultAsync(Func<Process, bool> predicate);

    Task<bool> AddAsync(Process entity);

    Task<bool> AddRangeAsync(IEnumerable<Process> entities);

    Task<IEnumerable<Process>> GetAllAsync();

    Task<IEnumerable<Process>> FindAsync(Func<Process, bool> predicate);

    Task<Process> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForTeamAsync(Team team);

    Task GetForTeamAsync(IEnumerable<Team> teams);

    Task GetProcessForAsync(Idea idea);

    Task GetProcessForAsync(IEnumerable<Idea> ideas);

    Task<Process> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Process entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Process> entities);
  }
}
