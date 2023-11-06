using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IRoleRepository
  {
    bool IncludeDeleted { get; set; }

    Task<BusinessRole> GetAsync(string id);

    Task<BusinessRole> SingleOrDefaultAsync(Func<BusinessRole, bool> predicate);

    Task<bool> AddAsync(BusinessRole entity);

    Task<bool> AddRangeAsync(IEnumerable<BusinessRole> entities);

    Task<IEnumerable<BusinessRole>> GetAllAsync();

    Task<IEnumerable<BusinessRole>> FindAsync(Func<BusinessRole, bool> predicate);

    Task<BusinessRole> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetRoleForAsync(CollaboratorRole collaboratorRole);

    Task GetRoleForAsync(IEnumerable<CollaboratorRole> collaboratorRoles);

    Task GetRoleForAsync(ImplementationCost implementationCost);

    Task GetRoleForAsync(
      IEnumerable<ImplementationCost> implementationCosts);

    Task GetRoleForAsync(RoleCost roleCost);

    Task GetRoleForAsync(IEnumerable<RoleCost> roleCosts);

    Task GetRoleForAsync(RoleIdeaAuthorisation roleIdeaAuthorisation);

    Task GetRoleForAsync(
      IEnumerable<RoleIdeaAuthorisation> roleIdeaAuthorisations);

    Task<BusinessRole> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(BusinessRole entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<BusinessRole> entities);
  }
}
