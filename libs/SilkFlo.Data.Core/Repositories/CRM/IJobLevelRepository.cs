using SilkFlo.Data.Core.Domain.CRM;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.CRM
{
  public interface IJobLevelRepository
  {
    bool IncludeDeleted { get; set; }

    Task<JobLevel> GetAsync(string id);

    Task<JobLevel> SingleOrDefaultAsync(Func<JobLevel, bool> predicate);

    Task<bool> AddAsync(JobLevel entity);

    Task<bool> AddRangeAsync(IEnumerable<JobLevel> entities);

    Task<IEnumerable<JobLevel>> GetAllAsync();

    Task<IEnumerable<JobLevel>> FindAsync(Func<JobLevel, bool> predicate);

    Task<JobLevel> GetUsingNameAsync(string name);

    Task GetJobLevelForAsync(Prospect prospect);

    Task GetJobLevelForAsync(IEnumerable<Prospect> prospects);

    Task<JobLevel> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(JobLevel entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<JobLevel> entities);
  }
}
