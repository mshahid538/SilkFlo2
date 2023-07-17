// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Application.SettingRepository
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
  public class SettingRepository : ISettingRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public SettingRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Setting Get(string id) => this.GetAsync(id).Result;

    public async Task<Setting> GetAsync(string id)
    {
      if (id == null)
        return (Setting) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ApplicationSettings.SingleOrDefault<Setting>((Func<Setting, bool>) (x => x.Id == id));
    }

    public Setting SingleOrDefault(Func<Setting, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Setting> SingleOrDefaultAsync(Func<Setting, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ApplicationSettings.Where<Setting>(predicate).FirstOrDefault<Setting>();
    }

    public bool Add(Setting entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Setting entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Setting> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Setting> entities)
    {
      if (entities == null)
        return false;
      foreach (Setting entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Setting> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Setting>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Setting>) dataSetAsync.ApplicationSettings;
    }

    public IEnumerable<Setting> Find(Func<Setting, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Setting>> FindAsync(Func<Setting, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.ApplicationSettings.Where<Setting>(predicate);
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Setting entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Setting entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Setting entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Setting> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Setting> entities)
    {
      if (entities == null)
        throw new DuplicateException("The settings are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
