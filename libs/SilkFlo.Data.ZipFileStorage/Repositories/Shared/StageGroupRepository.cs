// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.StageGroupRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Shared
{
  public class StageGroupRepository : IStageGroupRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public StageGroupRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public StageGroup Get(string id) => this.GetAsync(id).Result;

    public async Task<StageGroup> GetAsync(string id)
    {
      if (id == null)
        return (StageGroup) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedStageGroups.SingleOrDefault<StageGroup>((Func<StageGroup, bool>) (x => x.Id == id));
    }

    public StageGroup SingleOrDefault(Func<StageGroup, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<StageGroup> SingleOrDefaultAsync(Func<StageGroup, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedStageGroups.Where<StageGroup>(predicate).FirstOrDefault<StageGroup>();
    }

    public bool Add(StageGroup entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(StageGroup entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<StageGroup> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<StageGroup> entities)
    {
      if (entities == null)
        return false;
      foreach (StageGroup entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<StageGroup> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<StageGroup>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<StageGroup>) dataSetAsync.SharedStageGroups.OrderBy<StageGroup, int>((Func<StageGroup, int>) (m => m.Sort)).ThenBy<StageGroup, string>((Func<StageGroup, string>) (m => m.Name));
    }

    public IEnumerable<StageGroup> Find(Func<StageGroup, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<StageGroup>> FindAsync(Func<StageGroup, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<StageGroup>) dataSetAsync.SharedStageGroups.Where<StageGroup>(predicate).OrderBy<StageGroup, int>((Func<StageGroup, int>) (m => m.Sort)).ThenBy<StageGroup, string>((Func<StageGroup, string>) (m => m.Name));
    }

    public StageGroup GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<StageGroup> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (StageGroup) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedStageGroups.SingleOrDefault<StageGroup>((Func<StageGroup, bool>) (x => x.Name == name));
    }

    public void GetStageGroupFor(IEnumerable<Stage> stages) => this.GetStageGroupForAsync(stages).RunSynchronously();

    public async Task GetStageGroupForAsync(IEnumerable<Stage> stages)
    {
      if (stages == null)
        return;
      foreach (Stage stage in stages)
        await this.GetStageGroupForAsync(stage);
    }

    public void GetStageGroupFor(Stage stage) => this.GetStageGroupForAsync(stage).RunSynchronously();

    public async Task GetStageGroupForAsync(Stage stage)
    {
      if (stage == null)
        ;
      else
      {
        Stage stage1 = stage;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        stage1.StageGroup = dataSet.SharedStageGroups.SingleOrDefault<StageGroup>((Func<StageGroup, bool>) (x => x.Id == stage.StageGroupId));
        stage1 = (Stage) null;
        //dataSet = (DataSet) null;
      }
    }

    public StageGroup GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<StageGroup> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (StageGroup) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedStageGroups.SingleOrDefault<StageGroup>((Func<StageGroup, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      StageGroup entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(StageGroup entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(StageGroup entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<StageGroup> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<StageGroup> entities)
    {
      if (entities == null)
        throw new DuplicateException("The stageGroups are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
