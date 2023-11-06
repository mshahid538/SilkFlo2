// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.CostTypeRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Shared
{
  public class CostTypeRepository : ICostTypeRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public CostTypeRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public CostType Get(string id) => this.GetAsync(id).Result;

    public async Task<CostType> GetAsync(string id)
    {
      if (id == null)
        return (CostType) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedCostTypes.SingleOrDefault<CostType>((Func<CostType, bool>) (x => x.Id == id));
    }

    public CostType SingleOrDefault(Func<CostType, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<CostType> SingleOrDefaultAsync(Func<CostType, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedCostTypes.Where<CostType>(predicate).FirstOrDefault<CostType>();
    }

    public bool Add(CostType entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(CostType entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<CostType> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<CostType> entities)
    {
      if (entities == null)
        return false;
      foreach (CostType entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<CostType> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<CostType>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<CostType>) dataSetAsync.SharedCostTypes;
    }

    public IEnumerable<CostType> Find(Func<CostType, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<CostType>> FindAsync(Func<CostType, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedCostTypes.Where<CostType>(predicate);
    }

    public CostType GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<CostType> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (CostType) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedCostTypes.SingleOrDefault<CostType>((Func<CostType, bool>) (x => x.Name == name));
    }

    public void GetCostTypeFor(IEnumerable<OtherRunningCost> otherRunningCosts) => this.GetCostTypeForAsync(otherRunningCosts).RunSynchronously();

    public async Task GetCostTypeForAsync(IEnumerable<OtherRunningCost> otherRunningCosts)
    {
      if (otherRunningCosts == null)
        return;
      foreach (OtherRunningCost otherRunningCost in otherRunningCosts)
        await this.GetCostTypeForAsync(otherRunningCost);
    }

    public void GetCostTypeFor(OtherRunningCost otherRunningCost) => this.GetCostTypeForAsync(otherRunningCost).RunSynchronously();

    public async Task GetCostTypeForAsync(OtherRunningCost otherRunningCost)
    {
      if (otherRunningCost == null)
        ;
      else
      {
        OtherRunningCost otherRunningCost1 = otherRunningCost;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        otherRunningCost1.CostType = dataSet.SharedCostTypes.SingleOrDefault<CostType>((Func<CostType, bool>) (x => x.Id == otherRunningCost.CostTypeId));
        otherRunningCost1 = (OtherRunningCost) null;
        //dataSet = (DataSet) null;
      }
    }

    public CostType GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<CostType> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (CostType) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedCostTypes.SingleOrDefault<CostType>((Func<CostType, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      CostType entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(CostType entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(CostType entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<CostType> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<CostType> entities)
    {
      if (entities == null)
        throw new DuplicateException("The costTypes are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
