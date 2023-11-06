// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shop.PriceRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using SilkFlo.Data.Core.Repositories.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Shop
{
  public class PriceRepository : IPriceRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public PriceRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Price Get(string id) => this.GetAsync(id).Result;

    public async Task<Price> GetAsync(string id)
    {
      if (id == null)
        return (Price) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopPrices.SingleOrDefault<Price>((Func<Price, bool>) (x => x.Id == id));
    }

    public Price SingleOrDefault(Func<Price, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Price> SingleOrDefaultAsync(Func<Price, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopPrices.Where<Price>(predicate).FirstOrDefault<Price>();
    }

    public bool Add(Price entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Price entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Price> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Price> entities)
    {
      if (entities == null)
        return false;
      foreach (Price entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Price> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Price>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Price>) dataSetAsync.ShopPrices.OrderBy<Price, string>((Func<Price, string>) (m => m.PeriodId)).ThenBy<Price, string>((Func<Price, string>) (m => m.ProductId));
    }

    public IEnumerable<Price> Find(Func<Price, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Price>> FindAsync(Func<Price, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Price>) dataSetAsync.ShopPrices.Where<Price>(predicate).OrderBy<Price, string>((Func<Price, string>) (m => m.PeriodId)).ThenBy<Price, string>((Func<Price, string>) (m => m.ProductId));
    }

    public void GetForCurrency(Currency currency) => this.GetForCurrencyAsync(currency).RunSynchronously();

    public async Task GetForCurrencyAsync(Currency currency)
    {
      List<Price> lst;
      if (currency == null)
        lst = (List<Price>) null;
      else if (string.IsNullOrWhiteSpace(currency.Id))
      {
        lst = (List<Price>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.ShopPrices.Where<Price>((Func<Price, bool>) (x => x.CurrencyId == currency.Id)).OrderBy<Price, string>((Func<Price, string>) (x => x.PeriodId)).ThenBy<Price, string>((Func<Price, string>) (x => x.ProductId)).ToList<Price>();
        //dataSet = (DataSet) null;
        foreach (Price item in lst)
        {
          item.CurrencyId = currency.Id;
          item.Currency = currency;
        }
        currency.Prices = lst;
        lst = (List<Price>) null;
      }
    }

    public void GetForCurrency(IEnumerable<Currency> currencies) => this.GetForCurrencyAsync(currencies).RunSynchronously();

    public async Task GetForCurrencyAsync(IEnumerable<Currency> currencies)
    {
      if (currencies == null)
        return;
      foreach (Currency currency in currencies)
        await this.GetForCurrencyAsync(currency);
    }

    public void GetForPeriod(Period period) => this.GetForPeriodAsync(period).RunSynchronously();

    public async Task GetForPeriodAsync(Period period)
    {
      List<Price> lst;
      if (period == null)
        lst = (List<Price>) null;
      else if (string.IsNullOrWhiteSpace(period.Id))
      {
        lst = (List<Price>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.ShopPrices.Where<Price>((Func<Price, bool>) (x => x.PeriodId == period.Id)).OrderBy<Price, string>((Func<Price, string>) (x => x.PeriodId)).ThenBy<Price, string>((Func<Price, string>) (x => x.ProductId)).ToList<Price>();
        //dataSet = (DataSet) null;
        foreach (Price item in lst)
        {
          item.PeriodId = period.Id;
          item.Period = period;
        }
        period.Prices = lst;
        lst = (List<Price>) null;
      }
    }

    public void GetForPeriod(IEnumerable<Period> periods) => this.GetForPeriodAsync(periods).RunSynchronously();

    public async Task GetForPeriodAsync(IEnumerable<Period> periods)
    {
      if (periods == null)
        return;
      foreach (Period period in periods)
        await this.GetForPeriodAsync(period);
    }

    public void GetForProduct(Product product) => this.GetForProductAsync(product).RunSynchronously();

    public async Task GetForProductAsync(Product product)
    {
      List<Price> lst;
      if (product == null)
        lst = (List<Price>) null;
      else if (string.IsNullOrWhiteSpace(product.Id))
      {
        lst = (List<Price>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.ShopPrices.Where<Price>((Func<Price, bool>) (x => x.ProductId == product.Id)).OrderBy<Price, string>((Func<Price, string>) (x => x.PeriodId)).ThenBy<Price, string>((Func<Price, string>) (x => x.ProductId)).ToList<Price>();
        //dataSet = (DataSet) null;
        foreach (Price item in lst)
        {
          item.ProductId = product.Id;
          item.Product = product;
        }
        product.Prices = lst;
        lst = (List<Price>) null;
      }
    }

    public void GetForProduct(IEnumerable<Product> products) => this.GetForProductAsync(products).RunSynchronously();

    public async Task GetForProductAsync(IEnumerable<Product> products)
    {
      if (products == null)
        return;
      foreach (Product product in products)
        await this.GetForProductAsync(product);
    }

    public void GetPriceFor(IEnumerable<Subscription> subscriptions) => this.GetPriceForAsync(subscriptions).RunSynchronously();

    public async Task GetPriceForAsync(IEnumerable<Subscription> subscriptions)
    {
      if (subscriptions == null)
        return;
      foreach (Subscription subscription in subscriptions)
        await this.GetPriceForAsync(subscription);
    }

    public void GetPriceFor(Subscription subscription) => this.GetPriceForAsync(subscription).RunSynchronously();

    public async Task GetPriceForAsync(Subscription subscription)
    {
      if (subscription == null)
        ;
      else
      {
        Subscription subscription1 = subscription;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        subscription1.Price = dataSet.ShopPrices.SingleOrDefault<Price>((Func<Price, bool>) (x => x.Id == subscription.PriceId));
        subscription1 = (Subscription) null;
        //dataSet = (DataSet) null;
      }
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Price entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Price entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Price entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Price> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Price> entities)
    {
      if (entities == null)
        throw new DuplicateException("The prices are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
