using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface ISubmissionPathRepository
  {
    bool IncludeDeleted { get; set; }

    Task<SubmissionPath> GetAsync(string id);

    Task<SubmissionPath> SingleOrDefaultAsync(Func<SubmissionPath, bool> predicate);

    Task<bool> AddAsync(SubmissionPath entity);

    Task<bool> AddRangeAsync(IEnumerable<SubmissionPath> entities);

    Task<IEnumerable<SubmissionPath>> GetAllAsync();

    Task<IEnumerable<SubmissionPath>> FindAsync(Func<SubmissionPath, bool> predicate);

    Task<SubmissionPath> GetUsingNameAsync(string name);

    Task GetSubmissionPathForAsync(Idea idea);

    Task GetSubmissionPathForAsync(IEnumerable<Idea> ideas);

    Task<SubmissionPath> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(SubmissionPath entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<SubmissionPath> entities);
  }
}
