using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface ICollaboratorRoleRepository
  {
    bool IncludeDeleted { get; set; }

    Task<CollaboratorRole> GetAsync(string id);

    Task<CollaboratorRole> SingleOrDefaultAsync(Func<CollaboratorRole, bool> predicate);

    Task<bool> AddAsync(CollaboratorRole entity);

    Task<bool> AddRangeAsync(IEnumerable<CollaboratorRole> entities);

    Task<IEnumerable<CollaboratorRole>> GetAllAsync();

    Task<IEnumerable<CollaboratorRole>> FindAsync(Func<CollaboratorRole, bool> predicate);

    Task GetForCollaboratorAsync(Collaborator collaborator);

    Task GetForCollaboratorAsync(IEnumerable<Collaborator> collaborators);

    Task GetForRoleAsync(Role role);

    Task GetForRoleAsync(IEnumerable<Role> roles);

    Task GetCollaboratorRoleForAsync(UserAuthorisation userAuthorisation);

    Task GetCollaboratorRoleForAsync(IEnumerable<UserAuthorisation> userAuthorisations);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(CollaboratorRole entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<CollaboratorRole> entities);
  }
}
