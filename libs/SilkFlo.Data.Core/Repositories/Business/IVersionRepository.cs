using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IVersionRepository
  {
    bool IncludeDeleted { get; set; }

    Task<SilkFlo.Data.Core.Domain.Business.Version> GetAsync(string id);

    Task<SilkFlo.Data.Core.Domain.Business.Version> SingleOrDefaultAsync(
      Func<SilkFlo.Data.Core.Domain.Business.Version, bool> predicate);

    Task<bool> AddAsync(SilkFlo.Data.Core.Domain.Business.Version entity);

    Task<bool> AddRangeAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> entities);

    Task<IEnumerable<SilkFlo.Data.Core.Domain.Business.Version>> GetAllAsync();

    Task<IEnumerable<SilkFlo.Data.Core.Domain.Business.Version>> FindAsync(
      Func<SilkFlo.Data.Core.Domain.Business.Version, bool> predicate);

    Task<SilkFlo.Data.Core.Domain.Business.Version> GetUsingNameAsync(string name);

    Task GetForApplicationAsync(SilkFlo.Data.Core.Domain.Business.Application application);

    Task GetForApplicationAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> applications);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetVersionForAsync(IdeaApplicationVersion ideaApplicationVersion);

    Task GetVersionForAsync(
      IEnumerable<IdeaApplicationVersion> ideaApplicationVersions);

    Task<SilkFlo.Data.Core.Domain.Business.Version> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(SilkFlo.Data.Core.Domain.Business.Version entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> entities);
  }
}
