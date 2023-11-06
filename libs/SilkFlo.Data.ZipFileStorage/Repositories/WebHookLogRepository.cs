// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.WebHookLogRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories
{
  public class WebHookLogRepository : IWebHookLogRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public WebHookLogRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public WebHookLog Get(string id) => this.GetAsync(id).Result;

    public async Task<WebHookLog> GetAsync(string id)
    {
      if (id == null)
        return (WebHookLog) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.WebHookLogs.SingleOrDefault<WebHookLog>((Func<WebHookLog, bool>) (x => x.Id == id));
    }

    public WebHookLog SingleOrDefault(Func<WebHookLog, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<WebHookLog> SingleOrDefaultAsync(Func<WebHookLog, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.WebHookLogs.Where<WebHookLog>(predicate).FirstOrDefault<WebHookLog>();
    }

    public bool Add(WebHookLog entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(WebHookLog entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<WebHookLog> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<WebHookLog> entities)
    {
      if (entities == null)
        return false;
      foreach (WebHookLog entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<WebHookLog> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<WebHookLog>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<WebHookLog>) dataSetAsync.WebHookLogs;
    }

    public IEnumerable<WebHookLog> Find(Func<WebHookLog, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<WebHookLog>> FindAsync(Func<WebHookLog, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.WebHookLogs.Where<WebHookLog>(predicate);
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      WebHookLog entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(WebHookLog entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(WebHookLog entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<WebHookLog> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<WebHookLog> entities)
    {
      if (entities == null)
        throw new DuplicateException("The webHookLogs are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
