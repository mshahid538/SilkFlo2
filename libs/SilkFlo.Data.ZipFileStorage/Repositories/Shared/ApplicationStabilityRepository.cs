// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.ApplicationStabilityRepository
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
  public class ApplicationStabilityRepository : IApplicationStabilityRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public ApplicationStabilityRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public ApplicationStability Get(string id) => this.GetAsync(id).Result;

    public async Task<ApplicationStability> GetAsync(string id)
    {
      if (id == null)
        return (ApplicationStability) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedApplicationStabilities.SingleOrDefault<ApplicationStability>((Func<ApplicationStability, bool>) (x => x.Id == id));
    }

    public ApplicationStability SingleOrDefault(Func<ApplicationStability, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<ApplicationStability> SingleOrDefaultAsync(
      Func<ApplicationStability, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedApplicationStabilities.Where<ApplicationStability>(predicate).FirstOrDefault<ApplicationStability>();
    }

    public bool Add(ApplicationStability entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(ApplicationStability entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<ApplicationStability> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<ApplicationStability> entities)
    {
      if (entities == null)
        return false;
      foreach (ApplicationStability entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<ApplicationStability> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<ApplicationStability>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<ApplicationStability>) dataSetAsync.SharedApplicationStabilities.OrderByDescending<ApplicationStability, Decimal>((Func<ApplicationStability, Decimal>) (m => m.Weighting)).ThenBy<ApplicationStability, string>((Func<ApplicationStability, string>) (m => m.Name));
    }

    public IEnumerable<ApplicationStability> Find(Func<ApplicationStability, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<ApplicationStability>> FindAsync(
      Func<ApplicationStability, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<ApplicationStability>) dataSetAsync.SharedApplicationStabilities.Where<ApplicationStability>(predicate).OrderByDescending<ApplicationStability, Decimal>((Func<ApplicationStability, Decimal>) (m => m.Weighting)).ThenBy<ApplicationStability, string>((Func<ApplicationStability, string>) (m => m.Name));
    }

    public ApplicationStability GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<ApplicationStability> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (ApplicationStability) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedApplicationStabilities.SingleOrDefault<ApplicationStability>((Func<ApplicationStability, bool>) (x => x.Name == name));
    }

    public void GetApplicationStabilityFor(IEnumerable<Idea> ideas) => this.GetApplicationStabilityForAsync(ideas).RunSynchronously();

    public async Task GetApplicationStabilityForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetApplicationStabilityForAsync(idea);
    }

    public void GetApplicationStabilityFor(Idea idea) => this.GetApplicationStabilityForAsync(idea).RunSynchronously();

    public async Task GetApplicationStabilityForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        idea1.ApplicationStability = dataSet.SharedApplicationStabilities.SingleOrDefault<ApplicationStability>((Func<ApplicationStability, bool>) (x => x.Id == idea.ApplicationStabilityId));
        idea1 = (Idea) null;
        dataSet = (DataSet) null;
      }
    }

    public ApplicationStability GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<ApplicationStability> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (ApplicationStability) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedApplicationStabilities.SingleOrDefault<ApplicationStability>((Func<ApplicationStability, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      ApplicationStability entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(ApplicationStability entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(ApplicationStability entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<ApplicationStability> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ApplicationStability> entities)
    {
      if (entities == null)
        throw new DuplicateException("The applicationStabilities are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
