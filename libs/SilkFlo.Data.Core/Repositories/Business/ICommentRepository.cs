using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface ICommentRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Comment> GetAsync(string id);

    Task<Comment> SingleOrDefaultAsync(Func<Comment, bool> predicate);

    Task<bool> AddAsync(Comment entity);

    Task<bool> AddRangeAsync(IEnumerable<Comment> entities);

    Task<IEnumerable<Comment>> GetAllAsync();

    Task<IEnumerable<Comment>> FindAsync(Func<Comment, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForIdeaAsync(Idea idea);

    Task GetForIdeaAsync(IEnumerable<Idea> ideas);

    Task GetForSenderAsync(User sender);

    Task GetForSenderAsync(IEnumerable<User> senders);

    Task GetCommentForAsync(Recipient recipient);

    Task GetCommentForAsync(IEnumerable<Recipient> recipients);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Comment entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Comment> entities);
  }
}
