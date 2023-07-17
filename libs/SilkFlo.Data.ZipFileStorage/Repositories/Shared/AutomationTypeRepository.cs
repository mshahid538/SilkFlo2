// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.AutomationTypeRepository
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
  public class AutomationTypeRepository : IAutomationTypeRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public AutomationTypeRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public AutomationType Get(string id) => this.GetAsync(id).Result;

    public async Task<AutomationType> GetAsync(string id)
    {
      if (id == null)
        return (AutomationType) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAutomationTypes.SingleOrDefault<AutomationType>((Func<AutomationType, bool>) (x => x.Id == id));
    }

    public AutomationType SingleOrDefault(Func<AutomationType, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<AutomationType> SingleOrDefaultAsync(Func<AutomationType, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAutomationTypes.Where<AutomationType>(predicate).FirstOrDefault<AutomationType>();
    }

    public bool Add(AutomationType entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(AutomationType entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<AutomationType> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<AutomationType> entities)
    {
      if (entities == null)
        return false;
      foreach (AutomationType entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<AutomationType> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<AutomationType>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<AutomationType>) dataSetAsync.SharedAutomationTypes;
    }

    public IEnumerable<AutomationType> Find(Func<AutomationType, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<AutomationType>> FindAsync(Func<AutomationType, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAutomationTypes.Where<AutomationType>(predicate);
    }

    public AutomationType GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<AutomationType> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (AutomationType) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAutomationTypes.SingleOrDefault<AutomationType>((Func<AutomationType, bool>) (x => x.Name == name));
    }

    public void GetAutomationTypeFor(IEnumerable<RunningCost> runningCosts) => this.GetAutomationTypeForAsync(runningCosts).RunSynchronously();

    public async Task GetAutomationTypeForAsync(IEnumerable<RunningCost> runningCosts)
    {
      if (runningCosts == null)
        return;
      foreach (RunningCost runningCost in runningCosts)
        await this.GetAutomationTypeForAsync(runningCost);
    }

    public void GetAutomationTypeFor(RunningCost runningCost) => this.GetAutomationTypeForAsync(runningCost).RunSynchronously();

    public async Task GetAutomationTypeForAsync(RunningCost runningCost)
    {
      if (runningCost == null)
        ;
      else
      {
        RunningCost runningCost1 = runningCost;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        runningCost1.AutomationType = dataSet.SharedAutomationTypes.SingleOrDefault<AutomationType>((Func<AutomationType, bool>) (x => x.Id == runningCost.AutomationTypeId));
        runningCost1 = (RunningCost) null;
        dataSet = (DataSet) null;
      }
    }

    public AutomationType GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<AutomationType> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (AutomationType) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAutomationTypes.SingleOrDefault<AutomationType>((Func<AutomationType, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      AutomationType entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(AutomationType entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(AutomationType entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<AutomationType> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<AutomationType> entities)
    {
      if (entities == null)
        throw new DuplicateException("The automationTypes are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
