// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shop.SubscriptionRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using SilkFlo.Data.Core.Repositories.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Shop
{
  public class SubscriptionRepository : ISubscriptionRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public SubscriptionRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Subscription Get(string id) => this.GetAsync(id).Result;

    public async Task<Subscription> GetAsync(string id)
    {
      if (id == null)
        return (Subscription) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopSubscriptions.SingleOrDefault<Subscription>((Func<Subscription, bool>) (x => x.Id == id));
    }

    public Subscription SingleOrDefault(Func<Subscription, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Subscription> SingleOrDefaultAsync(Func<Subscription, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopSubscriptions.Where<Subscription>(predicate).FirstOrDefault<Subscription>();
    }

    public bool Add(Subscription entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Subscription entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Subscription> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Subscription> entities)
    {
      if (entities == null)
        return false;
      foreach (Subscription entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Subscription> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Subscription>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Subscription>) dataSetAsync.ShopSubscriptions.OrderBy<Subscription, DateTime>((Func<Subscription, DateTime>) (m => m.DateStart));
    }

    public IEnumerable<Subscription> Find(Func<Subscription, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Subscription>> FindAsync(Func<Subscription, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Subscription>) dataSetAsync.ShopSubscriptions.Where<Subscription>(predicate).OrderBy<Subscription, DateTime>((Func<Subscription, DateTime>) (m => m.DateStart));
    }

    public void GetForAgencyDiscount(Discount agencyDiscount) => this.GetForAgencyDiscountAsync(agencyDiscount).RunSynchronously();

    public async Task GetForAgencyDiscountAsync(Discount agencyDiscount)
    {
      List<Subscription> lst;
      if (agencyDiscount == null)
        lst = (List<Subscription>) null;
      else if (string.IsNullOrWhiteSpace(agencyDiscount.Id))
      {
        lst = (List<Subscription>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.ShopSubscriptions.Where<Subscription>((Func<Subscription, bool>) (x => x.AgencyDiscountId == agencyDiscount.Id)).OrderBy<Subscription, DateTime>((Func<Subscription, DateTime>) (x => x.DateStart)).ToList<Subscription>();
        dataSet = (DataSet) null;
        foreach (Subscription item in lst)
        {
          item.AgencyDiscountId = agencyDiscount.Id;
          item.AgencyDiscount = agencyDiscount;
        }
        agencyDiscount.Subscriptions = lst;
        lst = (List<Subscription>) null;
      }
    }

    public void GetForAgencyDiscount(IEnumerable<Discount> agencyDiscounts) => this.GetForAgencyDiscountAsync(agencyDiscounts).RunSynchronously();

    public async Task GetForAgencyDiscountAsync(IEnumerable<Discount> agencyDiscounts)
    {
      if (agencyDiscounts == null)
        return;
      foreach (Discount agencyDiscount in agencyDiscounts)
        await this.GetForAgencyDiscountAsync(agencyDiscount);
    }

    public void GetForAgency(Client agency) => this.GetForAgencyAsync(agency).RunSynchronously();

    public async Task GetForAgencyAsync(Client agency)
    {
      List<Subscription> lst;
      if (agency == null)
        lst = (List<Subscription>) null;
      else if (string.IsNullOrWhiteSpace(agency.Id))
      {
        lst = (List<Subscription>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.ShopSubscriptions.Where<Subscription>((Func<Subscription, bool>) (x => x.AgencyId == agency.Id)).OrderBy<Subscription, DateTime>((Func<Subscription, DateTime>) (x => x.DateStart)).ToList<Subscription>();
        dataSet = (DataSet) null;
        foreach (Subscription item in lst)
        {
          item.AgencyId = agency.Id;
          item.Agency = agency;
        }
        agency.AgencySubscriptions = lst;
        lst = (List<Subscription>) null;
      }
    }

    public void GetForAgency(IEnumerable<Client> agencies) => this.GetForAgencyAsync(agencies).RunSynchronously();

    public async Task GetForAgencyAsync(IEnumerable<Client> agencies)
    {
      if (agencies == null)
        return;
      foreach (Client agency in agencies)
        await this.GetForAgencyAsync(agency);
    }

    public void GetForAgencyType(ClientType agencyType) => this.GetForAgencyTypeAsync(agencyType).RunSynchronously();

    public async Task GetForAgencyTypeAsync(ClientType agencyType)
    {
      List<Subscription> lst;
      if (agencyType == null)
        lst = (List<Subscription>) null;
      else if (string.IsNullOrWhiteSpace(agencyType.Id))
      {
        lst = (List<Subscription>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.ShopSubscriptions.Where<Subscription>((Func<Subscription, bool>) (x => x.AgencyTypeId == agencyType.Id)).OrderBy<Subscription, DateTime>((Func<Subscription, DateTime>) (x => x.DateStart)).ToList<Subscription>();
        dataSet = (DataSet) null;
        foreach (Subscription item in lst)
        {
          item.AgencyTypeId = agencyType.Id;
          item.AgencyType = agencyType;
        }
        agencyType.Subscriptions = lst;
        lst = (List<Subscription>) null;
      }
    }

    public void GetForAgencyType(IEnumerable<ClientType> agencyTypes) => this.GetForAgencyTypeAsync(agencyTypes).RunSynchronously();

    public async Task GetForAgencyTypeAsync(IEnumerable<ClientType> agencyTypes)
    {
      if (agencyTypes == null)
        return;
      foreach (ClientType agencyType in agencyTypes)
        await this.GetForAgencyTypeAsync(agencyType);
    }

    public void GetForCoupon(Coupon coupon) => this.GetForCouponAsync(coupon).RunSynchronously();

    public async Task GetForCouponAsync(Coupon coupon)
    {
      List<Subscription> lst;
      if (coupon == null)
        lst = (List<Subscription>) null;
      else if (string.IsNullOrWhiteSpace(coupon.Id))
      {
        lst = (List<Subscription>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.ShopSubscriptions.Where<Subscription>((Func<Subscription, bool>) (x => x.CouponId == coupon.Id)).OrderBy<Subscription, DateTime>((Func<Subscription, DateTime>) (x => x.DateStart)).ToList<Subscription>();
        dataSet = (DataSet) null;
        foreach (Subscription item in lst)
        {
          item.CouponId = coupon.Id;
          item.Coupon = coupon;
        }
        coupon.Subscriptions = lst;
        lst = (List<Subscription>) null;
      }
    }

    public void GetForCoupon(IEnumerable<Coupon> coupons) => this.GetForCouponAsync(coupons).RunSynchronously();

    public async Task GetForCouponAsync(IEnumerable<Coupon> coupons)
    {
      if (coupons == null)
        return;
      foreach (Coupon coupon in coupons)
        await this.GetForCouponAsync(coupon);
    }

    public void GetForPrice(Price price) => this.GetForPriceAsync(price).RunSynchronously();

    public async Task GetForPriceAsync(Price price)
    {
      List<Subscription> lst;
      if (price == null)
        lst = (List<Subscription>) null;
      else if (string.IsNullOrWhiteSpace(price.Id))
      {
        lst = (List<Subscription>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.ShopSubscriptions.Where<Subscription>((Func<Subscription, bool>) (x => x.PriceId == price.Id)).OrderBy<Subscription, DateTime>((Func<Subscription, DateTime>) (x => x.DateStart)).ToList<Subscription>();
        dataSet = (DataSet) null;
        foreach (Subscription item in lst)
        {
          item.PriceId = price.Id;
          item.Price = price;
        }
        price.Subscriptions = lst;
        lst = (List<Subscription>) null;
      }
    }

    public void GetForPrice(IEnumerable<Price> prices) => this.GetForPriceAsync(prices).RunSynchronously();

    public async Task GetForPriceAsync(IEnumerable<Price> prices)
    {
      if (prices == null)
        return;
      foreach (Price price in prices)
        await this.GetForPriceAsync(price);
    }

    public void GetForTenant(Client tenant) => this.GetForTenantAsync(tenant).RunSynchronously();

    public async Task GetForTenantAsync(Client tenant)
    {
      List<Subscription> lst;
      if (tenant == null)
        lst = (List<Subscription>) null;
      else if (string.IsNullOrWhiteSpace(tenant.Id))
      {
        lst = (List<Subscription>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.ShopSubscriptions.Where<Subscription>((Func<Subscription, bool>) (x => x.TenantId == tenant.Id)).OrderBy<Subscription, DateTime>((Func<Subscription, DateTime>) (x => x.DateStart)).ToList<Subscription>();
        dataSet = (DataSet) null;
        foreach (Subscription item in lst)
        {
          item.TenantId = tenant.Id;
          item.Tenant = tenant;
        }
        tenant.TenantSubscriptions = lst;
        lst = (List<Subscription>) null;
      }
    }

    public void GetForTenant(IEnumerable<Client> tenants) => this.GetForTenantAsync(tenants).RunSynchronously();

    public async Task GetForTenantAsync(IEnumerable<Client> tenants)
    {
      if (tenants == null)
        return;
      foreach (Client tenant in tenants)
        await this.GetForTenantAsync(tenant);
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Subscription entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Subscription entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Subscription entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Subscription> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Subscription> entities)
    {
      if (entities == null)
        throw new DuplicateException("The subscriptions are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
