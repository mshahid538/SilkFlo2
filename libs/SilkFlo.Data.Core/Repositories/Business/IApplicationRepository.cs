using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IApplicationRepository
  {
    bool IncludeDeleted { get; set; }

    Task<SilkFlo.Data.Core.Domain.Business.Application> GetAsync(string id);

    Task<SilkFlo.Data.Core.Domain.Business.Application> SingleOrDefaultAsync(Func<SilkFlo.Data.Core.Domain.Business.Application, bool> predicate);

    Task<bool> AddAsync(SilkFlo.Data.Core.Domain.Business.Application entity);

    Task<bool> AddRangeAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> entities);

    Task<IEnumerable<SilkFlo.Data.Core.Domain.Business.Application>> GetAllAsync();

    Task<IEnumerable<SilkFlo.Data.Core.Domain.Business.Application>> FindAsync(Func<SilkFlo.Data.Core.Domain.Business.Application, bool> predicate);

    Task<SilkFlo.Data.Core.Domain.Business.Application> GetUsingNameAsync(string name);

    Task GetForClientAsync(SilkFlo.Data.Core.Domain.Business.Client client);

    Task GetForClientAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Client> clients);

    Task GetApplicationForAsync(SilkFlo.Data.Core.Domain.Business.Version version);

    Task GetApplicationForAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions);

    Task<SilkFlo.Data.Core.Domain.Business.Application> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(SilkFlo.Data.Core.Domain.Business.Application entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> entities);
  }
}
