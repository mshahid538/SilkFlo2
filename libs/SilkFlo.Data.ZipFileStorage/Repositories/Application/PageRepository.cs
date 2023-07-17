// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Application.PageRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Application;
using SilkFlo.Data.Core.Repositories.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SilkFlo.Data.Persistence.Repositories.Application
{
  public class PageRepository : IPageRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public PageRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Page Get(string id) => this.GetAsync(id).Result;

    public async Task<Page> GetAsync(string id)
    {
      if (id == null)
        return (Page) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ApplicationPages.SingleOrDefault<Page>((Func<Page, bool>) (x => x.Id == id));
    }

    public Page SingleOrDefault(Func<Page, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Page> SingleOrDefaultAsync(Func<Page, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ApplicationPages.Where<Page>(predicate).FirstOrDefault<Page>();
    }

    public bool Add(Page entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Page entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Page> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Page> entities)
    {
      if (entities == null)
        return false;
      foreach (Page entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Page> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Page>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Page>) dataSetAsync.ApplicationPages.OrderBy<Page, int>((Func<Page, int>) (m => m.Sort)).ThenBy<Page, string>((Func<Page, string>) (m => m.Name));
    }

    public IEnumerable<Page> Find(Func<Page, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Page>> FindAsync(Func<Page, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Page>) dataSetAsync.ApplicationPages.Where<Page>(predicate).OrderBy<Page, int>((Func<Page, int>) (m => m.Sort)).ThenBy<Page, string>((Func<Page, string>) (m => m.Name));
    }

    public Page GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Page> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Page) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ApplicationPages.SingleOrDefault<Page>((Func<Page, bool>) (x => x.Name == name));
    }

    public Page GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Page> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Page) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ApplicationPages.SingleOrDefault<Page>((Func<Page, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Page entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Page entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Page entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Page> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Page> entities)
    {
      if (entities == null)
        throw new DuplicateException("The pages are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
