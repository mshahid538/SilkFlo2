// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.DecisionDifficultyRepository
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
  public class DecisionDifficultyRepository : IDecisionDifficultyRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public DecisionDifficultyRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public DecisionDifficulty Get(string id) => this.GetAsync(id).Result;

    public async Task<DecisionDifficulty> GetAsync(string id)
    {
      if (id == null)
        return (DecisionDifficulty) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDecisionDifficulties.SingleOrDefault<DecisionDifficulty>((Func<DecisionDifficulty, bool>) (x => x.Id == id));
    }

    public DecisionDifficulty SingleOrDefault(Func<DecisionDifficulty, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<DecisionDifficulty> SingleOrDefaultAsync(
      Func<DecisionDifficulty, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDecisionDifficulties.Where<DecisionDifficulty>(predicate).FirstOrDefault<DecisionDifficulty>();
    }

    public bool Add(DecisionDifficulty entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(DecisionDifficulty entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<DecisionDifficulty> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<DecisionDifficulty> entities)
    {
      if (entities == null)
        return false;
      foreach (DecisionDifficulty entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<DecisionDifficulty> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<DecisionDifficulty>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<DecisionDifficulty>) dataSetAsync.SharedDecisionDifficulties.OrderByDescending<DecisionDifficulty, Decimal>((Func<DecisionDifficulty, Decimal>) (m => m.Weighting)).ThenBy<DecisionDifficulty, string>((Func<DecisionDifficulty, string>) (m => m.Name));
    }

    public IEnumerable<DecisionDifficulty> Find(Func<DecisionDifficulty, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<DecisionDifficulty>> FindAsync(
      Func<DecisionDifficulty, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<DecisionDifficulty>) dataSetAsync.SharedDecisionDifficulties.Where<DecisionDifficulty>(predicate).OrderByDescending<DecisionDifficulty, Decimal>((Func<DecisionDifficulty, Decimal>) (m => m.Weighting)).ThenBy<DecisionDifficulty, string>((Func<DecisionDifficulty, string>) (m => m.Name));
    }

    public DecisionDifficulty GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<DecisionDifficulty> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (DecisionDifficulty) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDecisionDifficulties.SingleOrDefault<DecisionDifficulty>((Func<DecisionDifficulty, bool>) (x => x.Name == name));
    }

    public void GetDecisionDifficultyFor(IEnumerable<Idea> ideas) => this.GetDecisionDifficultyForAsync(ideas).RunSynchronously();

    public async Task GetDecisionDifficultyForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetDecisionDifficultyForAsync(idea);
    }

    public void GetDecisionDifficultyFor(Idea idea) => this.GetDecisionDifficultyForAsync(idea).RunSynchronously();

    public async Task GetDecisionDifficultyForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        idea1.DecisionDifficulty = dataSet.SharedDecisionDifficulties.SingleOrDefault<DecisionDifficulty>((Func<DecisionDifficulty, bool>) (x => x.Id == idea.DecisionDifficultyId));
        idea1 = (Idea) null;
        //dataSet = (DataSet) null;
      }
    }

    public DecisionDifficulty GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<DecisionDifficulty> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (DecisionDifficulty) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDecisionDifficulties.SingleOrDefault<DecisionDifficulty>((Func<DecisionDifficulty, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      DecisionDifficulty entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(DecisionDifficulty entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(DecisionDifficulty entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<DecisionDifficulty> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<DecisionDifficulty> entities)
    {
      if (entities == null)
        throw new DuplicateException("The decisionDifficulties are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
