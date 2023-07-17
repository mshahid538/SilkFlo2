// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shop.ProductRepository
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
  public class ProductRepository : IProductRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public ProductRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Product Get(string id) => this.GetAsync(id).Result;

    public async Task<Product> GetAsync(string id)
    {
      if (id == null)
        return (Product) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopProducts.SingleOrDefault<Product>((Func<Product, bool>) (x => x.Id == id));
    }

    public Product SingleOrDefault(Func<Product, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Product> SingleOrDefaultAsync(Func<Product, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopProducts.Where<Product>(predicate).FirstOrDefault<Product>();
    }

    public bool Add(Product entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Product entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Product> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Product> entities)
    {
      if (entities == null)
        return false;
      foreach (Product entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Product> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Product>) dataSetAsync.ShopProducts.OrderBy<Product, int>((Func<Product, int>) (m => m.Sort)).ThenBy<Product, string>((Func<Product, string>) (m => m.Name));
    }

    public IEnumerable<Product> Find(Func<Product, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Product>> FindAsync(Func<Product, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Product>) dataSetAsync.ShopProducts.Where<Product>(predicate).OrderBy<Product, int>((Func<Product, int>) (m => m.Sort)).ThenBy<Product, string>((Func<Product, string>) (m => m.Name));
    }

    public Product GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Product> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Product) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopProducts.SingleOrDefault<Product>((Func<Product, bool>) (x => x.Name == name));
    }

    public void GetProductFor(IEnumerable<Price> prices) => this.GetProductForAsync(prices).RunSynchronously();

    public async Task GetProductForAsync(IEnumerable<Price> prices)
    {
      if (prices == null)
        return;
      foreach (Price price in prices)
        await this.GetProductForAsync(price);
    }

    public void GetProductFor(Price price) => this.GetProductForAsync(price).RunSynchronously();

    public async Task GetProductForAsync(Price price)
    {
      if (price == null)
        ;
      else
      {
        Price price1 = price;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        price1.Product = dataSet.ShopProducts.SingleOrDefault<Product>((Func<Product, bool>) (x => x.Id == price.ProductId));
        price1 = (Price) null;
        dataSet = (DataSet) null;
      }
    }

    public Product GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Product> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Product) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ShopProducts.SingleOrDefault<Product>((Func<Product, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Product entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Product entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Product entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Product> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Product> entities)
    {
      if (entities == null)
        throw new DuplicateException("The products are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
