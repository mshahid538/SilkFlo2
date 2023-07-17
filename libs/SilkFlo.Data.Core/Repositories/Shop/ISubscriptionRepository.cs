using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shop
{
  public interface ISubscriptionRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Subscription> GetAsync(string id);

    Task<Subscription> SingleOrDefaultAsync(Func<Subscription, bool> predicate);

    Task<bool> AddAsync(Subscription entity);

    Task<bool> AddRangeAsync(IEnumerable<Subscription> entities);

    Task<IEnumerable<Subscription>> GetAllAsync();

    Task<IEnumerable<Subscription>> FindAsync(Func<Subscription, bool> predicate);

    Task GetForAgencyDiscountAsync(Discount agencyDiscount);

    Task GetForAgencyDiscountAsync(IEnumerable<Discount> agencyDiscounts);

    Task GetForAgencyAsync(Client agency);

    Task GetForAgencyAsync(IEnumerable<Client> agencies);

    Task GetForAgencyTypeAsync(ClientType agencyType);

    Task GetForAgencyTypeAsync(IEnumerable<ClientType> agencyTypes);

    Task GetForCouponAsync(Coupon coupon);

    Task GetForCouponAsync(IEnumerable<Coupon> coupons);

    Task GetForPriceAsync(Price price);

    Task GetForPriceAsync(IEnumerable<Price> prices);

    Task GetForTenantAsync(Client tenant);

    Task GetForTenantAsync(IEnumerable<Client> tenants);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Subscription entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Subscription> entities);
  }
}
