// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.IdeaStatusRepository
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
  public class IdeaStatusRepository : IIdeaStatusRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public IdeaStatusRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public IdeaStatus Get(string id) => this.GetAsync(id).Result;

    public async Task<IdeaStatus> GetAsync(string id)
    {
      if (id == null)
        return (IdeaStatus) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedIdeaStatuses.SingleOrDefault<IdeaStatus>((Func<IdeaStatus, bool>) (x => x.Id == id));
    }

    public IdeaStatus SingleOrDefault(Func<IdeaStatus, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<IdeaStatus> SingleOrDefaultAsync(Func<IdeaStatus, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedIdeaStatuses.Where<IdeaStatus>(predicate).FirstOrDefault<IdeaStatus>();
    }

    public bool Add(IdeaStatus entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(IdeaStatus entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<IdeaStatus> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<IdeaStatus> entities)
    {
      if (entities == null)
        return false;
      foreach (IdeaStatus entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<IdeaStatus> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<IdeaStatus>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<IdeaStatus>) dataSetAsync.SharedIdeaStatuses.OrderBy<IdeaStatus, int>((Func<IdeaStatus, int>) (m => m.Sort)).ThenBy<IdeaStatus, string>((Func<IdeaStatus, string>) (m => m.Name));
    }

    public IEnumerable<IdeaStatus> Find(Func<IdeaStatus, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<IdeaStatus>> FindAsync(Func<IdeaStatus, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<IdeaStatus>) dataSetAsync.SharedIdeaStatuses.Where<IdeaStatus>(predicate).OrderBy<IdeaStatus, int>((Func<IdeaStatus, int>) (m => m.Sort)).ThenBy<IdeaStatus, string>((Func<IdeaStatus, string>) (m => m.Name));
    }

    public IdeaStatus GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<IdeaStatus> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (IdeaStatus) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedIdeaStatuses.SingleOrDefault<IdeaStatus>((Func<IdeaStatus, bool>) (x => x.Name == name));
    }

    public void GetForStage(Stage stage) => this.GetForStageAsync(stage).RunSynchronously();

    public async Task GetForStageAsync(Stage stage)
    {
      List<IdeaStatus> lst;
      if (stage == null)
        lst = (List<IdeaStatus>) null;
      else if (string.IsNullOrWhiteSpace(stage.Id))
      {
        lst = (List<IdeaStatus>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.SharedIdeaStatuses.Where<IdeaStatus>((Func<IdeaStatus, bool>) (x => x.StageId == stage.Id)).OrderBy<IdeaStatus, int>((Func<IdeaStatus, int>) (x => x.Sort)).ThenBy<IdeaStatus, string>((Func<IdeaStatus, string>) (x => x.Name)).ToList<IdeaStatus>();
        //dataSet = (DataSet) null;
        foreach (IdeaStatus item in lst)
        {
          item.StageId = stage.Id;
          item.Stage = stage;
        }
        stage.IdeaStatuses = lst;
        lst = (List<IdeaStatus>) null;
      }
    }

    public void GetForStage(IEnumerable<Stage> stages) => this.GetForStageAsync(stages).RunSynchronously();

    public async Task GetForStageAsync(IEnumerable<Stage> stages)
    {
      if (stages == null)
        return;
      foreach (Stage stage in stages)
        await this.GetForStageAsync(stage);
    }

    public void GetStatusFor(IEnumerable<IdeaStageStatus> ideaStageStatuses) => this.GetStatusForAsync(ideaStageStatuses).RunSynchronously();

    public async Task GetStatusForAsync(IEnumerable<IdeaStageStatus> ideaStageStatuses)
    {
      if (ideaStageStatuses == null)
        return;
      foreach (IdeaStageStatus ideaStageStatus in ideaStageStatuses)
        await this.GetStatusForAsync(ideaStageStatus);
    }

    public void GetStatusFor(IdeaStageStatus ideaStageStatus) => this.GetStatusForAsync(ideaStageStatus).RunSynchronously();

    public async Task GetStatusForAsync(IdeaStageStatus ideaStageStatus)
    {
      if (ideaStageStatus == null)
        ;
      else
      {
        IdeaStageStatus ideaStageStatus1 = ideaStageStatus;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        ideaStageStatus1.Status = dataSet.SharedIdeaStatuses.SingleOrDefault<IdeaStatus>((Func<IdeaStatus, bool>) (x => x.Id == ideaStageStatus.StatusId));
        ideaStageStatus1 = (IdeaStageStatus) null;
        //dataSet = (DataSet) null;
      }
    }

    public IdeaStatus GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<IdeaStatus> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (IdeaStatus) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedIdeaStatuses.SingleOrDefault<IdeaStatus>((Func<IdeaStatus, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      IdeaStatus entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(IdeaStatus entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(IdeaStatus entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<IdeaStatus> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaStatus> entities)
    {
      if (entities == null)
        throw new DuplicateException("The ideaStatuses are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
