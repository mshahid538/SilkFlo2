// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.AverageNumberOfStepRepository
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
  public class AverageNumberOfStepRepository : IAverageNumberOfStepRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public AverageNumberOfStepRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public AverageNumberOfStep Get(string id) => this.GetAsync(id).Result;

    public async Task<AverageNumberOfStep> GetAsync(string id)
    {
      if (id == null)
        return (AverageNumberOfStep) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAverageNumberOfSteps.SingleOrDefault<AverageNumberOfStep>((Func<AverageNumberOfStep, bool>) (x => x.Id == id));
    }

    public AverageNumberOfStep SingleOrDefault(Func<AverageNumberOfStep, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<AverageNumberOfStep> SingleOrDefaultAsync(
      Func<AverageNumberOfStep, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAverageNumberOfSteps.Where<AverageNumberOfStep>(predicate).FirstOrDefault<AverageNumberOfStep>();
    }

    public bool Add(AverageNumberOfStep entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(AverageNumberOfStep entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<AverageNumberOfStep> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<AverageNumberOfStep> entities)
    {
      if (entities == null)
        return false;
      foreach (AverageNumberOfStep entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<AverageNumberOfStep> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<AverageNumberOfStep>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<AverageNumberOfStep>) dataSetAsync.SharedAverageNumberOfSteps.OrderByDescending<AverageNumberOfStep, Decimal>((Func<AverageNumberOfStep, Decimal>) (m => m.Weighting)).ThenBy<AverageNumberOfStep, string>((Func<AverageNumberOfStep, string>) (m => m.Name));
    }

    public IEnumerable<AverageNumberOfStep> Find(Func<AverageNumberOfStep, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<AverageNumberOfStep>> FindAsync(
      Func<AverageNumberOfStep, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<AverageNumberOfStep>) dataSetAsync.SharedAverageNumberOfSteps.Where<AverageNumberOfStep>(predicate).OrderByDescending<AverageNumberOfStep, Decimal>((Func<AverageNumberOfStep, Decimal>) (m => m.Weighting)).ThenBy<AverageNumberOfStep, string>((Func<AverageNumberOfStep, string>) (m => m.Name));
    }

    public AverageNumberOfStep GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<AverageNumberOfStep> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (AverageNumberOfStep) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAverageNumberOfSteps.SingleOrDefault<AverageNumberOfStep>((Func<AverageNumberOfStep, bool>) (x => x.Name == name));
    }

    public void GetAverageNumberOfStepFor(IEnumerable<Idea> ideas) => this.GetAverageNumberOfStepForAsync(ideas).RunSynchronously();

    public async Task GetAverageNumberOfStepForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetAverageNumberOfStepForAsync(idea);
    }

    public void GetAverageNumberOfStepFor(Idea idea) => this.GetAverageNumberOfStepForAsync(idea).RunSynchronously();

    public async Task GetAverageNumberOfStepForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        idea1.AverageNumberOfStep = dataSet.SharedAverageNumberOfSteps.SingleOrDefault<AverageNumberOfStep>((Func<AverageNumberOfStep, bool>) (x => x.Id == idea.AverageNumberOfStepId));
        idea1 = (Idea) null;
        //dataSet = (DataSet) null;
      }
    }

    public AverageNumberOfStep GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<AverageNumberOfStep> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (AverageNumberOfStep) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAverageNumberOfSteps.SingleOrDefault<AverageNumberOfStep>((Func<AverageNumberOfStep, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      AverageNumberOfStep entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(AverageNumberOfStep entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(AverageNumberOfStep entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<AverageNumberOfStep> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<AverageNumberOfStep> entities)
    {
      if (entities == null)
        throw new DuplicateException("The averageNumberOfSteps are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
