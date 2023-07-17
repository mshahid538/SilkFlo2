using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories
{
  public interface IMessageRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Message> GetAsync(string id);

    Task<Message> SingleOrDefaultAsync(Func<Message, bool> predicate);

    Task<bool> AddAsync(Message entity);

    Task<bool> AddRangeAsync(IEnumerable<Message> entities);

    Task<IEnumerable<Message>> GetAllAsync();

    Task<IEnumerable<Message>> FindAsync(Func<Message, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Message entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Message> entities);
  }
}
