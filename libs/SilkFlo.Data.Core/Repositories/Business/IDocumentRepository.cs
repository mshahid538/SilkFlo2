using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IDocumentRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Document> GetAsync(string id);

    Task<Document> SingleOrDefaultAsync(Func<Document, bool> predicate);

    Task<bool> AddAsync(Document entity);

    Task<bool> AddRangeAsync(IEnumerable<Document> entities);

    Task<IEnumerable<Document>> GetAllAsync();

    Task<IEnumerable<Document>> FindAsync(Func<Document, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForIdeaAsync(Idea idea);

    Task GetForIdeaAsync(IEnumerable<Idea> ideas);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Document entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Document> entities);
  }
}
