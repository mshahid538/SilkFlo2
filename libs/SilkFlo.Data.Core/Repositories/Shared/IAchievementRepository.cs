using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IAchievementRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Achievement> GetAsync(string id);

    Task<Achievement> SingleOrDefaultAsync(Func<Achievement, bool> predicate);

    Task<bool> AddAsync(Achievement entity);

    Task<bool> AddRangeAsync(IEnumerable<Achievement> entities);

    Task<IEnumerable<Achievement>> GetAllAsync();

    Task<IEnumerable<Achievement>> FindAsync(Func<Achievement, bool> predicate);

    Task<Achievement> GetUsingNameAsync(string name);

    Task GetAchievementForAsync(UserAchievement userAchievement);

    Task GetAchievementForAsync(IEnumerable<UserAchievement> userAchievements);

    Task<Achievement> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Achievement entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Achievement> entities);
  }
}
