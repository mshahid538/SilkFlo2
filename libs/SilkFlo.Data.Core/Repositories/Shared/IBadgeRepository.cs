using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IBadgeRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Badge> GetAsync(string id);

    Task<Badge> SingleOrDefaultAsync(Func<Badge, bool> predicate);

    Task<bool> AddAsync(Badge entity);

    Task<bool> AddRangeAsync(IEnumerable<Badge> entities);

    Task<IEnumerable<Badge>> GetAllAsync();

    Task<IEnumerable<Badge>> FindAsync(Func<Badge, bool> predicate);

    Task<Badge> GetUsingNameAsync(string name);

    Task GetBadgeForAsync(UserBadge userBadge);

    Task GetBadgeForAsync(IEnumerable<UserBadge> userBadges);

    Task<Badge> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Badge entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Badge> entities);
  }
}
