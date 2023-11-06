// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.DecisionCountRepository
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
  public class DecisionCountRepository : IDecisionCountRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public DecisionCountRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public DecisionCount Get(string id) => this.GetAsync(id).Result;

    public async Task<DecisionCount> GetAsync(string id)
    {
      if (id == null)
        return (DecisionCount) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDecisionCounts.SingleOrDefault<DecisionCount>((Func<DecisionCount, bool>) (x => x.Id == id));
    }

    public DecisionCount SingleOrDefault(Func<DecisionCount, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<DecisionCount> SingleOrDefaultAsync(Func<DecisionCount, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDecisionCounts.Where<DecisionCount>(predicate).FirstOrDefault<DecisionCount>();
    }

    public bool Add(DecisionCount entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(DecisionCount entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<DecisionCount> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<DecisionCount> entities)
    {
      if (entities == null)
        return false;
      foreach (DecisionCount entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<DecisionCount> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<DecisionCount>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<DecisionCount>) dataSetAsync.SharedDecisionCounts.OrderByDescending<DecisionCount, Decimal>((Func<DecisionCount, Decimal>) (m => m.Weighting)).ThenBy<DecisionCount, string>((Func<DecisionCount, string>) (m => m.Name));
    }

    public IEnumerable<DecisionCount> Find(Func<DecisionCount, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<DecisionCount>> FindAsync(Func<DecisionCount, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<DecisionCount>) dataSetAsync.SharedDecisionCounts.Where<DecisionCount>(predicate).OrderByDescending<DecisionCount, Decimal>((Func<DecisionCount, Decimal>) (m => m.Weighting)).ThenBy<DecisionCount, string>((Func<DecisionCount, string>) (m => m.Name));
    }

    public DecisionCount GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<DecisionCount> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (DecisionCount) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDecisionCounts.SingleOrDefault<DecisionCount>((Func<DecisionCount, bool>) (x => x.Name == name));
    }

    public void GetDecisionCountFor(IEnumerable<Idea> ideas) => this.GetDecisionCountForAsync(ideas).RunSynchronously();

    public async Task GetDecisionCountForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetDecisionCountForAsync(idea);
    }

    public void GetDecisionCountFor(Idea idea) => this.GetDecisionCountForAsync(idea).RunSynchronously();

    public async Task GetDecisionCountForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        idea1.DecisionCount = dataSet.SharedDecisionCounts.SingleOrDefault<DecisionCount>((Func<DecisionCount, bool>) (x => x.Id == idea.DecisionCountId));
        idea1 = (Idea) null;
        //dataSet = (DataSet) null;
      }
    }

    public DecisionCount GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<DecisionCount> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (DecisionCount) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDecisionCounts.SingleOrDefault<DecisionCount>((Func<DecisionCount, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      DecisionCount entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(DecisionCount entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(DecisionCount entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<DecisionCount> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<DecisionCount> entities)
    {
      if (entities == null)
        throw new DuplicateException("The decisionCounts are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
