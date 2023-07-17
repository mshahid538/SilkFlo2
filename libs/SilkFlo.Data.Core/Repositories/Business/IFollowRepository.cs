using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IFollowRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Follow> GetAsync(string id);

    Task<Follow> SingleOrDefaultAsync(Func<Follow, bool> predicate);

    Task<bool> AddAsync(Follow entity);

    Task<bool> AddRangeAsync(IEnumerable<Follow> entities);

    Task<IEnumerable<Follow>> GetAllAsync();

    Task<IEnumerable<Follow>> FindAsync(Func<Follow, bool> predicate);

    Task GetForIdeaAsync(Idea idea);

    Task GetForIdeaAsync(IEnumerable<Idea> ideas);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Follow entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Follow> entities);
  }
}
