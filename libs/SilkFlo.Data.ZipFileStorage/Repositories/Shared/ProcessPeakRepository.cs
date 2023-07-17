// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.ProcessPeakRepository
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
  public class ProcessPeakRepository : IProcessPeakRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public ProcessPeakRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public ProcessPeak Get(string id) => this.GetAsync(id).Result;

    public async Task<ProcessPeak> GetAsync(string id)
    {
      if (id == null)
        return (ProcessPeak) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedProcessPeaks.SingleOrDefault<ProcessPeak>((Func<ProcessPeak, bool>) (x => x.Id == id));
    }

    public ProcessPeak SingleOrDefault(Func<ProcessPeak, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<ProcessPeak> SingleOrDefaultAsync(Func<ProcessPeak, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedProcessPeaks.Where<ProcessPeak>(predicate).FirstOrDefault<ProcessPeak>();
    }

    public bool Add(ProcessPeak entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(ProcessPeak entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<ProcessPeak> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<ProcessPeak> entities)
    {
      if (entities == null)
        return false;
      foreach (ProcessPeak entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<ProcessPeak> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<ProcessPeak>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<ProcessPeak>) dataSetAsync.SharedProcessPeaks.OrderByDescending<ProcessPeak, Decimal>((Func<ProcessPeak, Decimal>) (m => m.Weighting)).ThenBy<ProcessPeak, string>((Func<ProcessPeak, string>) (m => m.Name));
    }

    public IEnumerable<ProcessPeak> Find(Func<ProcessPeak, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<ProcessPeak>> FindAsync(Func<ProcessPeak, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<ProcessPeak>) dataSetAsync.SharedProcessPeaks.Where<ProcessPeak>(predicate).OrderByDescending<ProcessPeak, Decimal>((Func<ProcessPeak, Decimal>) (m => m.Weighting)).ThenBy<ProcessPeak, string>((Func<ProcessPeak, string>) (m => m.Name));
    }

    public ProcessPeak GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<ProcessPeak> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (ProcessPeak) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedProcessPeaks.SingleOrDefault<ProcessPeak>((Func<ProcessPeak, bool>) (x => x.Name == name));
    }

    public void GetProcessPeakFor(IEnumerable<Idea> ideas) => this.GetProcessPeakForAsync(ideas).RunSynchronously();

    public async Task GetProcessPeakForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetProcessPeakForAsync(idea);
    }

    public void GetProcessPeakFor(Idea idea) => this.GetProcessPeakForAsync(idea).RunSynchronously();

    public async Task GetProcessPeakForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        idea1.ProcessPeak = dataSet.SharedProcessPeaks.SingleOrDefault<ProcessPeak>((Func<ProcessPeak, bool>) (x => x.Id == idea.ProcessPeakId));
        idea1 = (Idea) null;
        dataSet = (DataSet) null;
      }
    }

    public ProcessPeak GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<ProcessPeak> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (ProcessPeak) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedProcessPeaks.SingleOrDefault<ProcessPeak>((Func<ProcessPeak, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      ProcessPeak entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(ProcessPeak entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(ProcessPeak entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<ProcessPeak> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ProcessPeak> entities)
    {
      if (entities == null)
        throw new DuplicateException("The processPeaks are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
