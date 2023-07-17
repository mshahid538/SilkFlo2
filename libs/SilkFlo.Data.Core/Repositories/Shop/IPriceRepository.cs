using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shop
{
  public interface IPriceRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Price> GetAsync(string id);

    Task<Price> SingleOrDefaultAsync(Func<Price, bool> predicate);

    Task<bool> AddAsync(Price entity);

    Task<bool> AddRangeAsync(IEnumerable<Price> entities);

    Task<IEnumerable<Price>> GetAllAsync();

    Task<IEnumerable<Price>> FindAsync(Func<Price, bool> predicate);

    Task GetForCurrencyAsync(Currency currency);

    Task GetForCurrencyAsync(IEnumerable<Currency> currencies);

    Task GetForPeriodAsync(Period period);

    Task GetForPeriodAsync(IEnumerable<Period> periods);

    Task GetForProductAsync(Product product);

    Task GetForProductAsync(IEnumerable<Product> products);

    Task GetPriceForAsync(Subscription subscription);

    Task GetPriceForAsync(IEnumerable<Subscription> subscriptions);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Price entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Price> entities);
  }
}
