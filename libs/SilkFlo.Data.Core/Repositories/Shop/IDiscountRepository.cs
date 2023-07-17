using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shop
{
  public interface IDiscountRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Discount> GetAsync(string id);

    Task<Discount> SingleOrDefaultAsync(Func<Discount, bool> predicate);

    Task<bool> AddAsync(Discount entity);

    Task<bool> AddRangeAsync(IEnumerable<Discount> entities);

    Task<IEnumerable<Discount>> GetAllAsync();

    Task<IEnumerable<Discount>> FindAsync(Func<Discount, bool> predicate);

    Task<Discount> GetUsingNameAsync(string name);

    Task GetAgencyDiscountForAsync(Client client);

    Task GetAgencyDiscountForAsync(IEnumerable<Client> clients);

    Task GetAgencyDiscountForAsync(Subscription subscription);

    Task GetAgencyDiscountForAsync(IEnumerable<Subscription> subscriptions);

    Task<Discount> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Discount entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Discount> entities);
  }
}
