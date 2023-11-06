// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shop.DiscountRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shop;
using SilkFlo.Data.Core.Repositories.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Shop
{
  public class DiscountRepository : IDiscountRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public DiscountRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Discount Get(string id) => this.GetAsync(id).Result;

    public async Task<Discount> GetAsync(string id)
    {
      if (id == null)
        return (Discount) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopDiscounts.SingleOrDefault<Discount>((Func<Discount, bool>) (x => x.Id == id));
    }

    public Discount SingleOrDefault(Func<Discount, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Discount> SingleOrDefaultAsync(Func<Discount, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopDiscounts.Where<Discount>(predicate).FirstOrDefault<Discount>();
    }

    public bool Add(Discount entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Discount entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Discount> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Discount> entities)
    {
      if (entities == null)
        return false;
      foreach (Discount entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Discount> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Discount>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Discount>) dataSetAsync.ShopDiscounts;
    }

    public IEnumerable<Discount> Find(Func<Discount, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Discount>> FindAsync(Func<Discount, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopDiscounts.Where<Discount>(predicate);
    }

    public Discount GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Discount> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Discount) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopDiscounts.SingleOrDefault<Discount>((Func<Discount, bool>) (x => x.Name == name));
    }

    public void GetAgencyDiscountFor(IEnumerable<Client> clients) => this.GetAgencyDiscountForAsync(clients).RunSynchronously();

    public async Task GetAgencyDiscountForAsync(IEnumerable<Client> clients)
    {
      if (clients == null)
        return;
      foreach (Client client in clients)
        await this.GetAgencyDiscountForAsync(client);
    }

    public void GetAgencyDiscountFor(Client client) => this.GetAgencyDiscountForAsync(client).RunSynchronously();

    public async Task GetAgencyDiscountForAsync(Client client)
    {
      if (client == null)
        ;
      else
      {
        Client client1 = client;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        client1.AgencyDiscount = dataSet.ShopDiscounts.SingleOrDefault<Discount>((Func<Discount, bool>) (x => x.Id == client.AgencyDiscountId));
        client1 = (Client) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetAgencyDiscountFor(IEnumerable<Subscription> subscriptions) => this.GetAgencyDiscountForAsync(subscriptions).RunSynchronously();

    public async Task GetAgencyDiscountForAsync(IEnumerable<Subscription> subscriptions)
    {
      if (subscriptions == null)
        return;
      foreach (Subscription subscription in subscriptions)
        await this.GetAgencyDiscountForAsync(subscription);
    }

    public void GetAgencyDiscountFor(Subscription subscription) => this.GetAgencyDiscountForAsync(subscription).RunSynchronously();

    public async Task GetAgencyDiscountForAsync(Subscription subscription)
    {
      if (subscription == null)
        ;
      else
      {
        Subscription subscription1 = subscription;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        subscription1.AgencyDiscount = dataSet.ShopDiscounts.SingleOrDefault<Discount>((Func<Discount, bool>) (x => x.Id == subscription.AgencyDiscountId));
        subscription1 = (Subscription) null;
        //dataSet = (DataSet) null;
      }
    }

    public Discount GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Discount> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Discount) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopDiscounts.SingleOrDefault<Discount>((Func<Discount, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Discount entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Discount entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Discount entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Discount> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Discount> entities)
    {
      if (entities == null)
        throw new DuplicateException("The discounts are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
