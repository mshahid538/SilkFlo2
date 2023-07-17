using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IDepartmentRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Department> GetAsync(string id);

    Task<Department> SingleOrDefaultAsync(Func<Department, bool> predicate);

    Task<bool> AddAsync(Department entity);

    Task<bool> AddRangeAsync(IEnumerable<Department> entities);

    Task<IEnumerable<Department>> GetAllAsync();

    Task<IEnumerable<Department>> FindAsync(Func<Department, bool> predicate);

    Task<Department> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetDepartmentForAsync(Idea idea);

    Task GetDepartmentForAsync(IEnumerable<Idea> ideas);

    Task GetDepartmentForAsync(Team team);

    Task GetDepartmentForAsync(IEnumerable<Team> teams);

    Task GetDepartmentForAsync(User user);

    Task GetDepartmentForAsync(IEnumerable<User> users);

    Task<Department> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Department entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Department> entities);
  }
}
