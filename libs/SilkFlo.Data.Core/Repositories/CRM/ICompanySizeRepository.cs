using SilkFlo.Data.Core.Domain.CRM;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.CRM
{
  public interface ICompanySizeRepository
  {
    bool IncludeDeleted { get; set; }

    Task<CompanySize> GetAsync(string id);

    Task<CompanySize> SingleOrDefaultAsync(Func<CompanySize, bool> predicate);

    Task<bool> AddAsync(CompanySize entity);

    Task<bool> AddRangeAsync(IEnumerable<CompanySize> entities);

    Task<IEnumerable<CompanySize>> GetAllAsync();

    Task<IEnumerable<CompanySize>> FindAsync(Func<CompanySize, bool> predicate);

    Task<CompanySize> GetUsingNameAsync(string name);

    Task GetCompanySizeForAsync(Prospect prospect);

    Task GetCompanySizeForAsync(IEnumerable<Prospect> prospects);

    Task<CompanySize> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(CompanySize entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<CompanySize> entities);
  }
}
