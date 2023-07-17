using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IRuleRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Rule> GetAsync(string id);

    Task<Rule> SingleOrDefaultAsync(Func<Rule, bool> predicate);

    Task<bool> AddAsync(Rule entity);

    Task<bool> AddRangeAsync(IEnumerable<Rule> entities);

    Task<IEnumerable<Rule>> GetAllAsync();

    Task<IEnumerable<Rule>> FindAsync(Func<Rule, bool> predicate);

    Task<Rule> GetUsingNameAsync(string name);

    Task GetRuleForAsync(Idea idea);

    Task GetRuleForAsync(IEnumerable<Idea> ideas);

    Task<Rule> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Rule entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Rule> entities);
  }
}
