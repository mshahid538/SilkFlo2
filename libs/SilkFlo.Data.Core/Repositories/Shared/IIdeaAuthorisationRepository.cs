using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IIdeaAuthorisationRepository
  {
    bool IncludeDeleted { get; set; }

    Task<IdeaAuthorisation> GetAsync(string id);

    Task<IdeaAuthorisation> SingleOrDefaultAsync(Func<IdeaAuthorisation, bool> predicate);

    Task<bool> AddAsync(IdeaAuthorisation entity);

    Task<bool> AddRangeAsync(IEnumerable<IdeaAuthorisation> entities);

    Task<IEnumerable<IdeaAuthorisation>> GetAllAsync();

    Task<IEnumerable<IdeaAuthorisation>> FindAsync(Func<IdeaAuthorisation, bool> predicate);

    Task<IdeaAuthorisation> GetUsingNameAsync(string name);

    Task GetIdeaAuthorisationForAsync(RoleIdeaAuthorisation roleIdeaAuthorisation);

    Task GetIdeaAuthorisationForAsync(
      IEnumerable<RoleIdeaAuthorisation> roleIdeaAuthorisations);

    Task GetIdeaAuthorisationForAsync(UserAuthorisation userAuthorisation);

    Task GetIdeaAuthorisationForAsync(IEnumerable<UserAuthorisation> userAuthorisations);

    Task<IdeaAuthorisation> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(IdeaAuthorisation entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaAuthorisation> entities);
  }
}
