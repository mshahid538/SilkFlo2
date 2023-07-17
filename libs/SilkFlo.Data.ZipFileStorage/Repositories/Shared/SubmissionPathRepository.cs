// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.SubmissionPathRepository
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
  public class SubmissionPathRepository : ISubmissionPathRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public SubmissionPathRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public SubmissionPath Get(string id) => this.GetAsync(id).Result;

    public async Task<SubmissionPath> GetAsync(string id)
    {
      if (id == null)
        return (SubmissionPath) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedSubmissionPaths.SingleOrDefault<SubmissionPath>((Func<SubmissionPath, bool>) (x => x.Id == id));
    }

    public SubmissionPath SingleOrDefault(Func<SubmissionPath, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<SubmissionPath> SingleOrDefaultAsync(Func<SubmissionPath, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedSubmissionPaths.Where<SubmissionPath>(predicate).FirstOrDefault<SubmissionPath>();
    }

    public bool Add(SubmissionPath entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(SubmissionPath entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<SubmissionPath> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<SubmissionPath> entities)
    {
      if (entities == null)
        return false;
      foreach (SubmissionPath entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<SubmissionPath> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<SubmissionPath>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<SubmissionPath>) dataSetAsync.SharedSubmissionPaths.OrderBy<SubmissionPath, string>((Func<SubmissionPath, string>) (m => m.Name));
    }

    public IEnumerable<SubmissionPath> Find(Func<SubmissionPath, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<SubmissionPath>> FindAsync(Func<SubmissionPath, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<SubmissionPath>) dataSetAsync.SharedSubmissionPaths.Where<SubmissionPath>(predicate).OrderBy<SubmissionPath, string>((Func<SubmissionPath, string>) (m => m.Name));
    }

    public SubmissionPath GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<SubmissionPath> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (SubmissionPath) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedSubmissionPaths.SingleOrDefault<SubmissionPath>((Func<SubmissionPath, bool>) (x => x.Name == name));
    }

    public void GetSubmissionPathFor(IEnumerable<Idea> ideas) => this.GetSubmissionPathForAsync(ideas).RunSynchronously();

    public async Task GetSubmissionPathForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetSubmissionPathForAsync(idea);
    }

    public void GetSubmissionPathFor(Idea idea) => this.GetSubmissionPathForAsync(idea).RunSynchronously();

    public async Task GetSubmissionPathForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        idea1.SubmissionPath = dataSet.SharedSubmissionPaths.SingleOrDefault<SubmissionPath>((Func<SubmissionPath, bool>) (x => x.Id == idea.SubmissionPathId));
        idea1 = (Idea) null;
        dataSet = (DataSet) null;
      }
    }

    public SubmissionPath GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<SubmissionPath> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (SubmissionPath) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedSubmissionPaths.SingleOrDefault<SubmissionPath>((Func<SubmissionPath, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      SubmissionPath entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(SubmissionPath entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(SubmissionPath entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<SubmissionPath> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<SubmissionPath> entities)
    {
      if (entities == null)
        throw new DuplicateException("The submissionPaths are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
