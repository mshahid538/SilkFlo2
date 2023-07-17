using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IDocumentationPresentRepository
  {
    bool IncludeDeleted { get; set; }

    Task<DocumentationPresent> GetAsync(string id);

    Task<DocumentationPresent> SingleOrDefaultAsync(Func<DocumentationPresent, bool> predicate);

    Task<bool> AddAsync(DocumentationPresent entity);

    Task<bool> AddRangeAsync(IEnumerable<DocumentationPresent> entities);

    Task<IEnumerable<DocumentationPresent>> GetAllAsync();

    Task<IEnumerable<DocumentationPresent>> FindAsync(Func<DocumentationPresent, bool> predicate);

    Task<DocumentationPresent> GetUsingNameAsync(string name);

    Task GetDocumentationPresentForAsync(Idea idea);

    Task GetDocumentationPresentForAsync(IEnumerable<Idea> ideas);

    Task<DocumentationPresent> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(DocumentationPresent entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<DocumentationPresent> entities);
  }
}
