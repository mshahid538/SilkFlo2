using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IUserAuthorisationRepository
  {
    bool IncludeDeleted { get; set; }

    Task<UserAuthorisation> GetAsync(string id);

    Task<UserAuthorisation> SingleOrDefaultAsync(Func<UserAuthorisation, bool> predicate);

    Task<bool> AddAsync(UserAuthorisation entity);

    Task<bool> AddRangeAsync(IEnumerable<UserAuthorisation> entities);

    Task<IEnumerable<UserAuthorisation>> GetAllAsync();

    Task<IEnumerable<UserAuthorisation>> FindAsync(Func<UserAuthorisation, bool> predicate);

    Task GetForCollaboratorRoleAsync(CollaboratorRole collaboratorRole);

    Task GetForCollaboratorRoleAsync(IEnumerable<CollaboratorRole> collaboratorRoles);

    Task GetForIdeaAuthorisationAsync(IdeaAuthorisation ideaAuthorisation);

    Task GetForIdeaAuthorisationAsync(IEnumerable<IdeaAuthorisation> ideaAuthorisations);

    Task GetForIdeaAsync(Idea idea);

    Task GetForIdeaAsync(IEnumerable<Idea> ideas);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(UserAuthorisation entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<UserAuthorisation> entities);
  }
}
