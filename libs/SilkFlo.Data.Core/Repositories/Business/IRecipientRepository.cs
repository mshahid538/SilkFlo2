using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IRecipientRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Recipient> GetAsync(string id);

    Task<Recipient> SingleOrDefaultAsync(Func<Recipient, bool> predicate);

    Task<bool> AddAsync(Recipient entity);

    Task<bool> AddRangeAsync(IEnumerable<Recipient> entities);

    Task<IEnumerable<Recipient>> GetAllAsync();

    Task<IEnumerable<Recipient>> FindAsync(Func<Recipient, bool> predicate);

    Task GetForCommentAsync(Comment comment);

    Task GetForCommentAsync(IEnumerable<Comment> comments);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Recipient entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Recipient> entities);
  }
}
