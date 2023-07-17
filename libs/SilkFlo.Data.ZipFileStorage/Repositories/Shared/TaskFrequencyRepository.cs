// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.TaskFrequencyRepository
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
  public class TaskFrequencyRepository : ITaskFrequencyRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public TaskFrequencyRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public TaskFrequency Get(string id) => this.GetAsync(id).Result;

    public async Task<TaskFrequency> GetAsync(string id)
    {
      if (id == null)
        return (TaskFrequency) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedTaskFrequencies.SingleOrDefault<TaskFrequency>((Func<TaskFrequency, bool>) (x => x.Id == id));
    }

    public TaskFrequency SingleOrDefault(Func<TaskFrequency, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<TaskFrequency> SingleOrDefaultAsync(Func<TaskFrequency, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedTaskFrequencies.Where<TaskFrequency>(predicate).FirstOrDefault<TaskFrequency>();
    }

    public bool Add(TaskFrequency entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(TaskFrequency entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<TaskFrequency> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<TaskFrequency> entities)
    {
      if (entities == null)
        return false;
      foreach (TaskFrequency entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<TaskFrequency> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<TaskFrequency>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<TaskFrequency>) dataSetAsync.SharedTaskFrequencies.OrderBy<TaskFrequency, int>((Func<TaskFrequency, int>) (m => m.Sort)).ThenBy<TaskFrequency, string>((Func<TaskFrequency, string>) (m => m.Name));
    }

    public IEnumerable<TaskFrequency> Find(Func<TaskFrequency, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<TaskFrequency>> FindAsync(Func<TaskFrequency, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<TaskFrequency>) dataSetAsync.SharedTaskFrequencies.Where<TaskFrequency>(predicate).OrderBy<TaskFrequency, int>((Func<TaskFrequency, int>) (m => m.Sort)).ThenBy<TaskFrequency, string>((Func<TaskFrequency, string>) (m => m.Name));
    }

    public TaskFrequency GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<TaskFrequency> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (TaskFrequency) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedTaskFrequencies.SingleOrDefault<TaskFrequency>((Func<TaskFrequency, bool>) (x => x.Name == name));
    }

    public void GetTaskFrequencyFor(IEnumerable<Idea> ideas) => this.GetTaskFrequencyForAsync(ideas).RunSynchronously();

    public async Task GetTaskFrequencyForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetTaskFrequencyForAsync(idea);
    }

    public void GetTaskFrequencyFor(Idea idea) => this.GetTaskFrequencyForAsync(idea).RunSynchronously();

    public async Task GetTaskFrequencyForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        idea1.TaskFrequency = dataSet.SharedTaskFrequencies.SingleOrDefault<TaskFrequency>((Func<TaskFrequency, bool>) (x => x.Id == idea.TaskFrequencyId));
        idea1 = (Idea) null;
        dataSet = (DataSet) null;
      }
    }

    public TaskFrequency GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<TaskFrequency> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (TaskFrequency) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedTaskFrequencies.SingleOrDefault<TaskFrequency>((Func<TaskFrequency, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      TaskFrequency entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(TaskFrequency entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(TaskFrequency entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<TaskFrequency> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<TaskFrequency> entities)
    {
      if (entities == null)
        throw new DuplicateException("The taskFrequencies are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
