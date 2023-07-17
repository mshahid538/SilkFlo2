// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.RuleRepository
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
  public class RuleRepository : IRuleRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public RuleRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Rule Get(string id) => this.GetAsync(id).Result;

    public async Task<Rule> GetAsync(string id)
    {
      if (id == null)
        return (Rule) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedRules.SingleOrDefault<Rule>((Func<Rule, bool>) (x => x.Id == id));
    }

    public Rule SingleOrDefault(Func<Rule, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Rule> SingleOrDefaultAsync(Func<Rule, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedRules.Where<Rule>(predicate).FirstOrDefault<Rule>();
    }

    public bool Add(Rule entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Rule entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Rule> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Rule> entities)
    {
      if (entities == null)
        return false;
      foreach (Rule entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Rule> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Rule>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Rule>) dataSetAsync.SharedRules.OrderByDescending<Rule, Decimal>((Func<Rule, Decimal>) (m => m.Weighting)).ThenBy<Rule, string>((Func<Rule, string>) (m => m.Name));
    }

    public IEnumerable<Rule> Find(Func<Rule, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Rule>> FindAsync(Func<Rule, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Rule>) dataSetAsync.SharedRules.Where<Rule>(predicate).OrderByDescending<Rule, Decimal>((Func<Rule, Decimal>) (m => m.Weighting)).ThenBy<Rule, string>((Func<Rule, string>) (m => m.Name));
    }

    public Rule GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Rule> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Rule) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedRules.SingleOrDefault<Rule>((Func<Rule, bool>) (x => x.Name == name));
    }

    public void GetRuleFor(IEnumerable<Idea> ideas) => this.GetRuleForAsync(ideas).RunSynchronously();

    public async Task GetRuleForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetRuleForAsync(idea);
    }

    public void GetRuleFor(Idea idea) => this.GetRuleForAsync(idea).RunSynchronously();

    public async Task GetRuleForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        idea1.Rule = dataSet.SharedRules.SingleOrDefault<Rule>((Func<Rule, bool>) (x => x.Id == idea.RuleId));
        idea1 = (Idea) null;
        dataSet = (DataSet) null;
      }
    }

    public Rule GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Rule> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Rule) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedRules.SingleOrDefault<Rule>((Func<Rule, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Rule entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Rule entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Rule entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Rule> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Rule> entities)
    {
      if (entities == null)
        throw new DuplicateException("The rules are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
