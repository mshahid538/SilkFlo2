// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.PeriodRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using SilkFlo.Data.Core.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Shared
{
  public class PeriodRepository : IPeriodRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public PeriodRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Period Get(string id) => this.GetAsync(id).Result;

    public async Task<Period> GetAsync(string id)
    {
      if (id == null)
        return (Period) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedPeriods.SingleOrDefault<Period>((Func<Period, bool>) (x => x.Id == id));
    }

    public Period SingleOrDefault(Func<Period, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Period> SingleOrDefaultAsync(Func<Period, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedPeriods.Where<Period>(predicate).FirstOrDefault<Period>();
    }

    public bool Add(Period entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Period entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Period> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Period> entities)
    {
      if (entities == null)
        return false;
      foreach (Period entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Period> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Period>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Period>) dataSetAsync.SharedPeriods.OrderBy<Period, int>((Func<Period, int>) (m => m.Sort)).ThenBy<Period, string>((Func<Period, string>) (m => m.Name));
    }

    public IEnumerable<Period> Find(Func<Period, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Period>> FindAsync(Func<Period, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Period>) dataSetAsync.SharedPeriods.Where<Period>(predicate).OrderBy<Period, int>((Func<Period, int>) (m => m.Sort)).ThenBy<Period, string>((Func<Period, string>) (m => m.Name));
    }

    public Period GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Period> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Period) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedPeriods.SingleOrDefault<Period>((Func<Period, bool>) (x => x.Name == name));
    }

    public void GetFrequencyFor(IEnumerable<OtherRunningCost> otherRunningCosts) => this.GetFrequencyForAsync(otherRunningCosts).RunSynchronously();

    public async Task GetFrequencyForAsync(IEnumerable<OtherRunningCost> otherRunningCosts)
    {
      if (otherRunningCosts == null)
        return;
      foreach (OtherRunningCost otherRunningCost in otherRunningCosts)
        await this.GetFrequencyForAsync(otherRunningCost);
    }

    public void GetFrequencyFor(OtherRunningCost otherRunningCost) => this.GetFrequencyForAsync(otherRunningCost).RunSynchronously();

    public async Task GetFrequencyForAsync(OtherRunningCost otherRunningCost)
    {
      if (otherRunningCost == null)
        ;
      else
      {
        OtherRunningCost otherRunningCost1 = otherRunningCost;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        otherRunningCost1.Frequency = dataSet.SharedPeriods.SingleOrDefault<Period>((Func<Period, bool>) (x => x.Id == otherRunningCost.FrequencyId));
        otherRunningCost1 = (OtherRunningCost) null;
      }
    }

    public void GetPeriodFor(IEnumerable<Price> prices) => this.GetPeriodForAsync(prices).RunSynchronously();

    public async Task GetPeriodForAsync(IEnumerable<Price> prices)
    {
      if (prices == null)
        return;
      foreach (Price price in prices)
        await this.GetPeriodForAsync(price);
    }

    public void GetPeriodFor(Price price) => this.GetPeriodForAsync(price).RunSynchronously();

    public async Task GetPeriodForAsync(Price price)
    {
      if (price == null)
        ;
      else
      {
        Price price1 = price;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        price1.Period = dataSet.SharedPeriods.SingleOrDefault<Period>((Func<Period, bool>) (x => x.Id == price.PeriodId));
        price1 = (Price) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetFrequencyFor(IEnumerable<RunningCost> runningCosts) => this.GetFrequencyForAsync(runningCosts).RunSynchronously();

    public async Task GetFrequencyForAsync(IEnumerable<RunningCost> runningCosts)
    {
      if (runningCosts == null)
        return;
      foreach (RunningCost runningCost in runningCosts)
        await this.GetFrequencyForAsync(runningCost);
    }

    public void GetFrequencyFor(RunningCost runningCost) => this.GetFrequencyForAsync(runningCost).RunSynchronously();

    public async Task GetFrequencyForAsync(RunningCost runningCost)
    {
      if (runningCost == null)
        ;
      else
      {
        RunningCost runningCost1 = runningCost;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        runningCost1.Frequency = dataSet.SharedPeriods.SingleOrDefault<Period>((Func<Period, bool>) (x => x.Id == runningCost.FrequencyId));
        runningCost1 = (RunningCost) null;
        //dataSet = (DataSet) null;
      }
    }

    public Period GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Period> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Period) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedPeriods.SingleOrDefault<Period>((Func<Period, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Period entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Period entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Period entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Period> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Period> entities)
    {
      if (entities == null)
        throw new DuplicateException("The periods are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
