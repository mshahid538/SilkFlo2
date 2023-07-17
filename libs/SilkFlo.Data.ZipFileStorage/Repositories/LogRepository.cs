// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.LogRepository
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
  public class LogRepository : ILogRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public LogRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Log Get(string id) => this.GetAsync(id).Result;

    public async Task<Log> GetAsync(string id)
    {
      if (id == null)
        return (Log) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.Logs.SingleOrDefault<Log>((Func<Log, bool>) (x => x.Id == id));
    }

    public Log SingleOrDefault(Func<Log, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Log> SingleOrDefaultAsync(Func<Log, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.Logs.Where<Log>(predicate).FirstOrDefault<Log>();
    }

    public bool Add(Log entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Log entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Log> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Log> entities)
    {
      if (entities == null)
        return false;
      foreach (Log entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Log> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Log>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Log>) dataSetAsync.Logs;
    }

    public IEnumerable<Log> Find(Func<Log, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Log>> FindAsync(Func<Log, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.Logs.Where<Log>(predicate);
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Log entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Log entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Log entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Log> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Log> entities)
    {
      if (entities == null)
        throw new DuplicateException("The logs are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
