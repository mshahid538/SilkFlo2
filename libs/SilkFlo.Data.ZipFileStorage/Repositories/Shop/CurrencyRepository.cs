// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shop.CurrencyRepository
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
  public class CurrencyRepository : ICurrencyRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public CurrencyRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Currency Get(string id) => this.GetAsync(id).Result;

    public async Task<Currency> GetAsync(string id)
    {
      if (id == null)
        return (Currency) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopCurrencies.SingleOrDefault<Currency>((Func<Currency, bool>) (x => x.Id == id));
    }

    public Currency SingleOrDefault(Func<Currency, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Currency> SingleOrDefaultAsync(Func<Currency, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopCurrencies.Where<Currency>(predicate).FirstOrDefault<Currency>();
    }

    public bool Add(Currency entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Currency entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Currency> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Currency> entities)
    {
      if (entities == null)
        return false;
      foreach (Currency entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Currency> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Currency>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Currency>) dataSetAsync.ShopCurrencies;
    }

    public IEnumerable<Currency> Find(Func<Currency, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Currency>> FindAsync(Func<Currency, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopCurrencies.Where<Currency>(predicate);
    }

    public void GetCurrencyFor(IEnumerable<Client> clients) => this.GetCurrencyForAsync(clients).RunSynchronously();

    public async Task GetCurrencyForAsync(IEnumerable<Client> clients)
    {
      if (clients == null)
        return;
      foreach (Client client in clients)
        await this.GetCurrencyForAsync(client);
    }

    public void GetCurrencyFor(Client client) => this.GetCurrencyForAsync(client).RunSynchronously();

    public async Task GetCurrencyForAsync(Client client)
    {
      if (client == null)
        ;
      else
      {
        Client client1 = client;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        client1.Currency = dataSet.ShopCurrencies.SingleOrDefault<Currency>((Func<Currency, bool>) (x => x.Id == client.CurrencyId));
        client1 = (Client) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetCurrencyFor(IEnumerable<Price> prices) => this.GetCurrencyForAsync(prices).RunSynchronously();

    public async Task GetCurrencyForAsync(IEnumerable<Price> prices)
    {
      if (prices == null)
        return;
      foreach (Price price in prices)
        await this.GetCurrencyForAsync(price);
    }

    public void GetCurrencyFor(Price price) => this.GetCurrencyForAsync(price).RunSynchronously();

    public async Task GetCurrencyForAsync(Price price)
    {
      if (price == null)
        ;
      else
      {
        Price price1 = price;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        price1.Currency = dataSet.ShopCurrencies.SingleOrDefault<Currency>((Func<Currency, bool>) (x => x.Id == price.CurrencyId));
        price1 = (Price) null;
        //dataSet = (DataSet) null;
      }
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Currency entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Currency entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Currency entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Currency> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Currency> entities)
    {
      if (entities == null)
        throw new DuplicateException("The currencies are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
