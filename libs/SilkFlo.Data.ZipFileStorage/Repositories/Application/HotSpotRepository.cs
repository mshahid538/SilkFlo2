// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Application.HotSpotRepository
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
  public class HotSpotRepository : IHotSpotRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public HotSpotRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public HotSpot Get(string id) => this.GetAsync(id).Result;

    public async Task<HotSpot> GetAsync(string id)
    {
      if (id == null)
        return (HotSpot) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ApplicationHotSpots.SingleOrDefault<HotSpot>((Func<HotSpot, bool>) (x => x.Id == id));
    }

    public HotSpot SingleOrDefault(Func<HotSpot, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<HotSpot> SingleOrDefaultAsync(Func<HotSpot, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ApplicationHotSpots.Where<HotSpot>(predicate).FirstOrDefault<HotSpot>();
    }

    public bool Add(HotSpot entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(HotSpot entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<HotSpot> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<HotSpot> entities)
    {
      if (entities == null)
        return false;
      foreach (HotSpot entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<HotSpot> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<HotSpot>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<HotSpot>) dataSetAsync.ApplicationHotSpots;
    }

    public IEnumerable<HotSpot> Find(Func<HotSpot, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<HotSpot>> FindAsync(Func<HotSpot, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ApplicationHotSpots.Where<HotSpot>(predicate);
    }

    public HotSpot GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<HotSpot> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (HotSpot) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ApplicationHotSpots.SingleOrDefault<HotSpot>((Func<HotSpot, bool>) (x => x.Name == name));
    }

    public HotSpot GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<HotSpot> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (HotSpot) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.ApplicationHotSpots.SingleOrDefault<HotSpot>((Func<HotSpot, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      HotSpot entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(HotSpot entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(HotSpot entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<HotSpot> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<HotSpot> entities)
    {
      if (entities == null)
        throw new DuplicateException("The hotSpots are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
