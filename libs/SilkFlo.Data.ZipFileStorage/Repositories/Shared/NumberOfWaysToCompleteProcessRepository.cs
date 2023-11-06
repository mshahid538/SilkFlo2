// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.NumberOfWaysToCompleteProcessRepository
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
  public class NumberOfWaysToCompleteProcessRepository : INumberOfWaysToCompleteProcessRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public NumberOfWaysToCompleteProcessRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public NumberOfWaysToCompleteProcess Get(string id) => this.GetAsync(id).Result;

    public async Task<NumberOfWaysToCompleteProcess> GetAsync(string id)
    {
      if (id == null)
        return (NumberOfWaysToCompleteProcess) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedNumberOfWaysToCompleteProcesses.SingleOrDefault<NumberOfWaysToCompleteProcess>((Func<NumberOfWaysToCompleteProcess, bool>) (x => x.Id == id));
    }

    public NumberOfWaysToCompleteProcess SingleOrDefault(
      Func<NumberOfWaysToCompleteProcess, bool> predicate)
    {
      return this.SingleOrDefaultAsync(predicate).Result;
    }

    public async Task<NumberOfWaysToCompleteProcess> SingleOrDefaultAsync(
      Func<NumberOfWaysToCompleteProcess, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedNumberOfWaysToCompleteProcesses.Where<NumberOfWaysToCompleteProcess>(predicate).FirstOrDefault<NumberOfWaysToCompleteProcess>();
    }

    public bool Add(NumberOfWaysToCompleteProcess entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(NumberOfWaysToCompleteProcess entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(
      IEnumerable<NumberOfWaysToCompleteProcess> entities)
    {
      return this.AddRangeAsync(entities).Result;
    }

    public async Task<bool> AddRangeAsync(
      IEnumerable<NumberOfWaysToCompleteProcess> entities)
    {
      if (entities == null)
        return false;
      foreach (NumberOfWaysToCompleteProcess entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<NumberOfWaysToCompleteProcess> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<NumberOfWaysToCompleteProcess>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<NumberOfWaysToCompleteProcess>) dataSetAsync.SharedNumberOfWaysToCompleteProcesses.OrderByDescending<NumberOfWaysToCompleteProcess, Decimal>((Func<NumberOfWaysToCompleteProcess, Decimal>) (m => m.Weighting)).ThenBy<NumberOfWaysToCompleteProcess, string>((Func<NumberOfWaysToCompleteProcess, string>) (m => m.Name));
    }

    public IEnumerable<NumberOfWaysToCompleteProcess> Find(
      Func<NumberOfWaysToCompleteProcess, bool> predicate)
    {
      return this.FindAsync(predicate).Result;
    }

    public async Task<IEnumerable<NumberOfWaysToCompleteProcess>> FindAsync(
      Func<NumberOfWaysToCompleteProcess, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<NumberOfWaysToCompleteProcess>) dataSetAsync.SharedNumberOfWaysToCompleteProcesses.Where<NumberOfWaysToCompleteProcess>(predicate).OrderByDescending<NumberOfWaysToCompleteProcess, Decimal>((Func<NumberOfWaysToCompleteProcess, Decimal>) (m => m.Weighting)).ThenBy<NumberOfWaysToCompleteProcess, string>((Func<NumberOfWaysToCompleteProcess, string>) (m => m.Name));
    }

    public NumberOfWaysToCompleteProcess GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<NumberOfWaysToCompleteProcess> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (NumberOfWaysToCompleteProcess) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedNumberOfWaysToCompleteProcesses.SingleOrDefault<NumberOfWaysToCompleteProcess>((Func<NumberOfWaysToCompleteProcess, bool>) (x => x.Name == name));
    }

    public void GetNumberOfWaysToCompleteProcessFor(IEnumerable<Idea> ideas) => this.GetNumberOfWaysToCompleteProcessForAsync(ideas).RunSynchronously();

    public async Task GetNumberOfWaysToCompleteProcessForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetNumberOfWaysToCompleteProcessForAsync(idea);
    }

    public void GetNumberOfWaysToCompleteProcessFor(Idea idea) => this.GetNumberOfWaysToCompleteProcessForAsync(idea).RunSynchronously();

    public async Task GetNumberOfWaysToCompleteProcessForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        idea1.NumberOfWaysToCompleteProcess = dataSet.SharedNumberOfWaysToCompleteProcesses.SingleOrDefault<NumberOfWaysToCompleteProcess>((Func<NumberOfWaysToCompleteProcess, bool>) (x => x.Id == idea.NumberOfWaysToCompleteProcessId));
        idea1 = (Idea) null;
        //dataSet = (DataSet) null;
      }
    }

    public NumberOfWaysToCompleteProcess GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<NumberOfWaysToCompleteProcess> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (NumberOfWaysToCompleteProcess) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedNumberOfWaysToCompleteProcesses.SingleOrDefault<NumberOfWaysToCompleteProcess>((Func<NumberOfWaysToCompleteProcess, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      NumberOfWaysToCompleteProcess entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(NumberOfWaysToCompleteProcess entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(NumberOfWaysToCompleteProcess entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(
      IEnumerable<NumberOfWaysToCompleteProcess> entities)
    {
      return this.RemoveRangeAsync(entities).Result;
    }

    public async Task<DataStoreResult> RemoveRangeAsync(
      IEnumerable<NumberOfWaysToCompleteProcess> entities)
    {
      if (entities == null)
        throw new DuplicateException("The numberOfWaysToCompleteProcesses are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
