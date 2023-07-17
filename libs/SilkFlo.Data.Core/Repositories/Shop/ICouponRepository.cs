using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shop
{
  public interface ICouponRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Coupon> GetAsync(string id);

    Task<Coupon> SingleOrDefaultAsync(Func<Coupon, bool> predicate);

    Task<bool> AddAsync(Coupon entity);

    Task<bool> AddRangeAsync(IEnumerable<Coupon> entities);

    Task<IEnumerable<Coupon>> GetAllAsync();

    Task<IEnumerable<Coupon>> FindAsync(Func<Coupon, bool> predicate);

    Task<Coupon> GetUsingNameAsync(string name);

    Task GetCouponForAsync(Subscription subscription);

    Task GetCouponForAsync(IEnumerable<Subscription> subscriptions);

    Task<Coupon> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Coupon entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Coupon> entities);
  }
}
