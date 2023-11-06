// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.StageRepository
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
  public class StageRepository : IStageRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public StageRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Stage Get(string id) => this.GetAsync(id).Result;

    public async Task<Stage> GetAsync(string id)
    {
      if (id == null)
        return (Stage) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedStages.SingleOrDefault<Stage>((Func<Stage, bool>) (x => x.Id == id));
    }

    public Stage SingleOrDefault(Func<Stage, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Stage> SingleOrDefaultAsync(Func<Stage, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedStages.Where<Stage>(predicate).FirstOrDefault<Stage>();
    }

    public bool Add(Stage entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Stage entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Stage> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Stage> entities)
    {
      if (entities == null)
        return false;
      foreach (Stage entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Stage> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Stage>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Stage>) dataSetAsync.SharedStages.OrderBy<Stage, int>((Func<Stage, int>) (m => m.Sort)).ThenBy<Stage, string>((Func<Stage, string>) (m => m.Name));
    }

    public IEnumerable<Stage> Find(Func<Stage, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Stage>> FindAsync(Func<Stage, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Stage>) dataSetAsync.SharedStages.Where<Stage>(predicate).OrderBy<Stage, int>((Func<Stage, int>) (m => m.Sort)).ThenBy<Stage, string>((Func<Stage, string>) (m => m.Name));
    }

    public Stage GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Stage> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Stage) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedStages.SingleOrDefault<Stage>((Func<Stage, bool>) (x => x.Name == name));
    }

    public void GetForStageGroup(StageGroup stageGroup) => this.GetForStageGroupAsync(stageGroup).RunSynchronously();

    public async Task GetForStageGroupAsync(StageGroup stageGroup)
    {
      List<Stage> lst;
      if (stageGroup == null)
        lst = (List<Stage>) null;
      else if (string.IsNullOrWhiteSpace(stageGroup.Id))
      {
        lst = (List<Stage>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.SharedStages.Where<Stage>((Func<Stage, bool>) (x => x.StageGroupId == stageGroup.Id)).OrderBy<Stage, int>((Func<Stage, int>) (x => x.Sort)).ThenBy<Stage, string>((Func<Stage, string>) (x => x.Name)).ToList<Stage>();
        //dataSet = (DataSet) null;
        foreach (Stage item in lst)
        {
          item.StageGroupId = stageGroup.Id;
          item.StageGroup = stageGroup;
        }
        stageGroup.Stages = lst;
        lst = (List<Stage>) null;
      }
    }

    public void GetForStageGroup(IEnumerable<StageGroup> stageGroups) => this.GetForStageGroupAsync(stageGroups).RunSynchronously();

    public async Task GetForStageGroupAsync(IEnumerable<StageGroup> stageGroups)
    {
      if (stageGroups == null)
        return;
      foreach (StageGroup stageGroup in stageGroups)
        await this.GetForStageGroupAsync(stageGroup);
    }

    public void GetStageFor(IEnumerable<IdeaStage> ideaStages) => this.GetStageForAsync(ideaStages).RunSynchronously();

    public async Task GetStageForAsync(IEnumerable<IdeaStage> ideaStages)
    {
      if (ideaStages == null)
        return;
      foreach (IdeaStage ideaStage in ideaStages)
        await this.GetStageForAsync(ideaStage);
    }

    public void GetStageFor(IdeaStage ideaStage) => this.GetStageForAsync(ideaStage).RunSynchronously();

    public async Task GetStageForAsync(IdeaStage ideaStage)
    {
      if (ideaStage == null)
        ;
      else
      {
        IdeaStage ideaStage1 = ideaStage;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        ideaStage1.Stage = dataSet.SharedStages.SingleOrDefault<Stage>((Func<Stage, bool>) (x => x.Id == ideaStage.StageId));
        ideaStage1 = (IdeaStage) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetStageFor(IEnumerable<IdeaStatus> ideaStatuses) => this.GetStageForAsync(ideaStatuses).RunSynchronously();

    public async Task GetStageForAsync(IEnumerable<IdeaStatus> ideaStatuses)
    {
      if (ideaStatuses == null)
        return;
      foreach (IdeaStatus ideaStatus in ideaStatuses)
        await this.GetStageForAsync(ideaStatus);
    }

    public void GetStageFor(IdeaStatus ideaStatus) => this.GetStageForAsync(ideaStatus).RunSynchronously();

    public async Task GetStageForAsync(IdeaStatus ideaStatus)
    {
      if (ideaStatus == null)
        ;
      else
      {
        IdeaStatus ideaStatus1 = ideaStatus;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        ideaStatus1.Stage = dataSet.SharedStages.SingleOrDefault<Stage>((Func<Stage, bool>) (x => x.Id == ideaStatus.StageId));
        ideaStatus1 = (IdeaStatus) null;
        //dataSet = (DataSet) null;
      }
    }

    public Stage GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Stage> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Stage) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedStages.SingleOrDefault<Stage>((Func<Stage, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Stage entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Stage entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Stage entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Stage> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Stage> entities)
    {
      if (entities == null)
        throw new DuplicateException("The stages are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
