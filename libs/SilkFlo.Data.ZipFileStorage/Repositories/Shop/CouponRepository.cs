// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shop.CouponRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Shop;
using SilkFlo.Data.Core.Repositories.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Shop
{
  public class CouponRepository : ICouponRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public CouponRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Coupon Get(string id) => this.GetAsync(id).Result;

    public async Task<Coupon> GetAsync(string id)
    {
      if (id == null)
        return (Coupon) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopCoupons.SingleOrDefault<Coupon>((Func<Coupon, bool>) (x => x.Id == id));
    }

    public Coupon SingleOrDefault(Func<Coupon, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Coupon> SingleOrDefaultAsync(Func<Coupon, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopCoupons.Where<Coupon>(predicate).FirstOrDefault<Coupon>();
    }

    public bool Add(Coupon entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Coupon entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Coupon> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Coupon> entities)
    {
      if (entities == null)
        return false;
      foreach (Coupon entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Coupon> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Coupon>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Coupon>) dataSetAsync.ShopCoupons;
    }

    public IEnumerable<Coupon> Find(Func<Coupon, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Coupon>> FindAsync(Func<Coupon, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopCoupons.Where<Coupon>(predicate);
    }

    public Coupon GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Coupon> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Coupon) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopCoupons.SingleOrDefault<Coupon>((Func<Coupon, bool>) (x => x.Name == name));
    }

    public void GetCouponFor(IEnumerable<Subscription> subscriptions) => this.GetCouponForAsync(subscriptions).RunSynchronously();

    public async Task GetCouponForAsync(IEnumerable<Subscription> subscriptions)
    {
      if (subscriptions == null)
        return;
      foreach (Subscription subscription in subscriptions)
        await this.GetCouponForAsync(subscription);
    }

    public void GetCouponFor(Subscription subscription) => this.GetCouponForAsync(subscription).RunSynchronously();

    public async Task GetCouponForAsync(Subscription subscription)
    {
      if (subscription == null)
        ;
      else
      {
        Subscription subscription1 = subscription;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        subscription1.Coupon = dataSet.ShopCoupons.SingleOrDefault<Coupon>((Func<Coupon, bool>) (x => x.Id == subscription.CouponId));
        subscription1 = (Subscription) null;
        dataSet = (DataSet) null;
      }
    }

    public Coupon GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Coupon> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Coupon) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopCoupons.SingleOrDefault<Coupon>((Func<Coupon, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Coupon entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Coupon entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Coupon entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Coupon> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Coupon> entities)
    {
      if (entities == null)
        throw new DuplicateException("The coupons are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
