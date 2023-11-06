using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IRoleIdeaAuthorisationRepository
  {
    bool IncludeDeleted { get; set; }

    Task<RoleIdeaAuthorisation> GetAsync(string id);

    Task<RoleIdeaAuthorisation> SingleOrDefaultAsync(Func<RoleIdeaAuthorisation, bool> predicate);

    Task<bool> AddAsync(RoleIdeaAuthorisation entity);

    Task<bool> AddRangeAsync(IEnumerable<RoleIdeaAuthorisation> entities);

    Task<IEnumerable<RoleIdeaAuthorisation>> GetAllAsync();

    Task<IEnumerable<RoleIdeaAuthorisation>> FindAsync(Func<RoleIdeaAuthorisation, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForIdeaAuthorisationAsync(IdeaAuthorisation ideaAuthorisation);

    Task GetForIdeaAuthorisationAsync(IEnumerable<IdeaAuthorisation> ideaAuthorisations);

    Task GetForRoleAsync(BusinessRole role);

    Task GetForRoleAsync(IEnumerable<BusinessRole> roles);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(RoleIdeaAuthorisation entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<RoleIdeaAuthorisation> entities);
  }
}
