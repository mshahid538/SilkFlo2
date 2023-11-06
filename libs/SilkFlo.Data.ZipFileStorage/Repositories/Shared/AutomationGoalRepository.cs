// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.AutomationGoalRepository
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
  public class AutomationGoalRepository : IAutomationGoalRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public AutomationGoalRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public AutomationGoal Get(string id) => this.GetAsync(id).Result;

    public async Task<AutomationGoal> GetAsync(string id)
    {
      if (id == null)
        return (AutomationGoal) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAutomationGoals.SingleOrDefault<AutomationGoal>((Func<AutomationGoal, bool>) (x => x.Id == id));
    }

    public AutomationGoal SingleOrDefault(Func<AutomationGoal, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<AutomationGoal> SingleOrDefaultAsync(Func<AutomationGoal, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAutomationGoals.Where<AutomationGoal>(predicate).FirstOrDefault<AutomationGoal>();
    }

    public bool Add(AutomationGoal entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(AutomationGoal entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<AutomationGoal> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<AutomationGoal> entities)
    {
      if (entities == null)
        return false;
      foreach (AutomationGoal entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<AutomationGoal> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<AutomationGoal>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<AutomationGoal>) dataSetAsync.SharedAutomationGoals.OrderBy<AutomationGoal, int>((Func<AutomationGoal, int>) (m => m.Sort)).ThenBy<AutomationGoal, string>((Func<AutomationGoal, string>) (m => m.Name));
    }

    public IEnumerable<AutomationGoal> Find(Func<AutomationGoal, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<AutomationGoal>> FindAsync(Func<AutomationGoal, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<AutomationGoal>) dataSetAsync.SharedAutomationGoals.Where<AutomationGoal>(predicate).OrderBy<AutomationGoal, int>((Func<AutomationGoal, int>) (m => m.Sort)).ThenBy<AutomationGoal, string>((Func<AutomationGoal, string>) (m => m.Name));
    }

    public AutomationGoal GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<AutomationGoal> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (AutomationGoal) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAutomationGoals.SingleOrDefault<AutomationGoal>((Func<AutomationGoal, bool>) (x => x.Name == name));
    }

    public void GetAutomationGoalFor(IEnumerable<Idea> ideas) => this.GetAutomationGoalForAsync(ideas).RunSynchronously();

    public async Task GetAutomationGoalForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetAutomationGoalForAsync(idea);
    }

    public void GetAutomationGoalFor(Idea idea) => this.GetAutomationGoalForAsync(idea).RunSynchronously();

    public async Task GetAutomationGoalForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        idea1.AutomationGoal = dataSet.SharedAutomationGoals.SingleOrDefault<AutomationGoal>((Func<AutomationGoal, bool>) (x => x.Id == idea.AutomationGoalId));
        idea1 = (Idea) null;
        //dataSet = (DataSet) null;
      }
    }

    public AutomationGoal GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<AutomationGoal> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (AutomationGoal) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAutomationGoals.SingleOrDefault<AutomationGoal>((Func<AutomationGoal, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      AutomationGoal entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(AutomationGoal entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(AutomationGoal entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<AutomationGoal> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<AutomationGoal> entities)
    {
      if (entities == null)
        throw new DuplicateException("The automationGoals are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
