using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shop
{
  public interface ICurrencyRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Currency> GetAsync(string id);

    Task<Currency> SingleOrDefaultAsync(Func<Currency, bool> predicate);

    Task<bool> AddAsync(Currency entity);

    Task<bool> AddRangeAsync(IEnumerable<Currency> entities);

    Task<IEnumerable<Currency>> GetAllAsync();

    Task<IEnumerable<Currency>> FindAsync(Func<Currency, bool> predicate);

    Task GetCurrencyForAsync(Client client);

    Task GetCurrencyForAsync(IEnumerable<Client> clients);

    Task GetCurrencyForAsync(Price price);

    Task GetCurrencyForAsync(IEnumerable<Price> prices);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Currency entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Currency> entities);
  }
}
