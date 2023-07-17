using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IVoteRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Vote> GetAsync(string id);

    Task<Vote> SingleOrDefaultAsync(Func<Vote, bool> predicate);

    Task<bool> AddAsync(Vote entity);

    Task<bool> AddRangeAsync(IEnumerable<Vote> entities);

    Task<IEnumerable<Vote>> GetAllAsync();

    Task<IEnumerable<Vote>> FindAsync(Func<Vote, bool> predicate);

    Task GetForIdeaAsync(Idea idea);

    Task GetForIdeaAsync(IEnumerable<Idea> ideas);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Vote entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Vote> entities);
  }
}
