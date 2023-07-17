using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shop
{
  public interface IProductRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Product> GetAsync(string id);

    Task<Product> SingleOrDefaultAsync(Func<Product, bool> predicate);

    Task<bool> AddAsync(Product entity);

    Task<bool> AddRangeAsync(IEnumerable<Product> entities);

    Task<IEnumerable<Product>> GetAllAsync();

    Task<IEnumerable<Product>> FindAsync(Func<Product, bool> predicate);

    Task<Product> GetUsingNameAsync(string name);

    Task GetProductForAsync(Price price);

    Task GetProductForAsync(IEnumerable<Price> prices);

    Task<Product> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Product entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Product> entities);
  }
}
