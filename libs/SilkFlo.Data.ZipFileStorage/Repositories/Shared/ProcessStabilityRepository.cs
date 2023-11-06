// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.ProcessStabilityRepository
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
  public class ProcessStabilityRepository : IProcessStabilityRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public ProcessStabilityRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public ProcessStability Get(string id) => this.GetAsync(id).Result;

    public async Task<ProcessStability> GetAsync(string id)
    {
      if (id == null)
        return (ProcessStability) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedProcessStabilities.SingleOrDefault<ProcessStability>((Func<ProcessStability, bool>) (x => x.Id == id));
    }

    public ProcessStability SingleOrDefault(Func<ProcessStability, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<ProcessStability> SingleOrDefaultAsync(Func<ProcessStability, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedProcessStabilities.Where<ProcessStability>(predicate).FirstOrDefault<ProcessStability>();
    }

    public bool Add(ProcessStability entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(ProcessStability entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<ProcessStability> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<ProcessStability> entities)
    {
      if (entities == null)
        return false;
      foreach (ProcessStability entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<ProcessStability> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<ProcessStability>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<ProcessStability>) dataSetAsync.SharedProcessStabilities.OrderByDescending<ProcessStability, Decimal>((Func<ProcessStability, Decimal>) (m => m.Weighting)).ThenBy<ProcessStability, string>((Func<ProcessStability, string>) (m => m.Name));
    }

    public IEnumerable<ProcessStability> Find(Func<ProcessStability, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<ProcessStability>> FindAsync(
      Func<ProcessStability, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<ProcessStability>) dataSetAsync.SharedProcessStabilities.Where<ProcessStability>(predicate).OrderByDescending<ProcessStability, Decimal>((Func<ProcessStability, Decimal>) (m => m.Weighting)).ThenBy<ProcessStability, string>((Func<ProcessStability, string>) (m => m.Name));
    }

    public ProcessStability GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<ProcessStability> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (ProcessStability) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedProcessStabilities.SingleOrDefault<ProcessStability>((Func<ProcessStability, bool>) (x => x.Name == name));
    }

    public void GetProcessStabilityFor(IEnumerable<Idea> ideas) => this.GetProcessStabilityForAsync(ideas).RunSynchronously();

    public async Task GetProcessStabilityForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetProcessStabilityForAsync(idea);
    }

    public void GetProcessStabilityFor(Idea idea) => this.GetProcessStabilityForAsync(idea).RunSynchronously();

    public async Task GetProcessStabilityForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        idea1.ProcessStability = dataSet.SharedProcessStabilities.SingleOrDefault<ProcessStability>((Func<ProcessStability, bool>) (x => x.Id == idea.ProcessStabilityId));
        idea1 = (Idea) null;
        //dataSet = (DataSet) null;
      }
    }

    public ProcessStability GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<ProcessStability> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (ProcessStability) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedProcessStabilities.SingleOrDefault<ProcessStability>((Func<ProcessStability, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      ProcessStability entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(ProcessStability entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(ProcessStability entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<ProcessStability> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ProcessStability> entities)
    {
      if (entities == null)
        throw new DuplicateException("The processStabilities are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
