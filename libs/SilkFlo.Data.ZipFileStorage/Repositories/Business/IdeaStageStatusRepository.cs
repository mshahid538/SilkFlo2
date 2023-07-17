// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.IdeaStageStatusRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Repositories.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class IdeaStageStatusRepository : IIdeaStageStatusRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public IdeaStageStatusRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public IdeaStageStatus Get(string id) => this.GetAsync(id).Result;

    public async Task<IdeaStageStatus> GetAsync(string id)
    {
      if (id == null)
        return (IdeaStageStatus) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaStageStatuses.SingleOrDefault<IdeaStageStatus>((Func<IdeaStageStatus, bool>) (x => x.Id == id));
    }

    public IdeaStageStatus SingleOrDefault(Func<IdeaStageStatus, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<IdeaStageStatus> SingleOrDefaultAsync(Func<IdeaStageStatus, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaStageStatuses.Where<IdeaStageStatus>(predicate).FirstOrDefault<IdeaStageStatus>();
    }

    public bool Add(IdeaStageStatus entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(IdeaStageStatus entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<IdeaStageStatus> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<IdeaStageStatus> entities)
    {
      if (entities == null)
        return false;
      foreach (IdeaStageStatus entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<IdeaStageStatus> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<IdeaStageStatus>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<IdeaStageStatus>) dataSetAsync.BusinessIdeaStageStatuses.OrderBy<IdeaStageStatus, string>((Func<IdeaStageStatus, string>) (m => m.IdeaStageId)).ThenBy<IdeaStageStatus, DateTime>((Func<IdeaStageStatus, DateTime>) (m => m.Date));
    }

    public IEnumerable<IdeaStageStatus> Find(Func<IdeaStageStatus, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<IdeaStageStatus>> FindAsync(Func<IdeaStageStatus, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<IdeaStageStatus>) dataSetAsync.BusinessIdeaStageStatuses.Where<IdeaStageStatus>(predicate).OrderBy<IdeaStageStatus, string>((Func<IdeaStageStatus, string>) (m => m.IdeaStageId)).ThenBy<IdeaStageStatus, DateTime>((Func<IdeaStageStatus, DateTime>) (m => m.Date));
    }

    public void GetForIdeaStage(IdeaStage ideaStage) => this.GetForIdeaStageAsync(ideaStage).RunSynchronously();

    public async Task GetForIdeaStageAsync(IdeaStage ideaStage)
    {
      List<IdeaStageStatus> lst;
      if (ideaStage == null)
        lst = (List<IdeaStageStatus>) null;
      else if (string.IsNullOrWhiteSpace(ideaStage.Id))
      {
        lst = (List<IdeaStageStatus>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaStageStatuses.Where<IdeaStageStatus>((Func<IdeaStageStatus, bool>) (x => x.IdeaStageId == ideaStage.Id)).OrderBy<IdeaStageStatus, string>((Func<IdeaStageStatus, string>) (x => x.IdeaStageId)).ThenBy<IdeaStageStatus, DateTime>((Func<IdeaStageStatus, DateTime>) (x => x.Date)).ToList<IdeaStageStatus>();
        dataSet = (DataSet) null;
        foreach (IdeaStageStatus item in lst)
        {
          item.IdeaStageId = ideaStage.Id;
          item.IdeaStage = ideaStage;
        }
        ideaStage.IdeaStageStatuses = lst;
        lst = (List<IdeaStageStatus>) null;
      }
    }

    public void GetForIdeaStage(IEnumerable<IdeaStage> ideaStages) => this.GetForIdeaStageAsync(ideaStages).RunSynchronously();

    public async Task GetForIdeaStageAsync(IEnumerable<IdeaStage> ideaStages)
    {
      if (ideaStages == null)
        return;
      foreach (IdeaStage ideaStage in ideaStages)
        await this.GetForIdeaStageAsync(ideaStage);
    }

    public void GetForStatus(IdeaStatus status) => this.GetForStatusAsync(status).RunSynchronously();

    public async Task GetForStatusAsync(IdeaStatus status)
    {
      List<IdeaStageStatus> lst;
      if (status == null)
        lst = (List<IdeaStageStatus>) null;
      else if (string.IsNullOrWhiteSpace(status.Id))
      {
        lst = (List<IdeaStageStatus>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaStageStatuses.Where<IdeaStageStatus>((Func<IdeaStageStatus, bool>) (x => x.StatusId == status.Id)).OrderBy<IdeaStageStatus, string>((Func<IdeaStageStatus, string>) (x => x.IdeaStageId)).ThenBy<IdeaStageStatus, DateTime>((Func<IdeaStageStatus, DateTime>) (x => x.Date)).ToList<IdeaStageStatus>();
        dataSet = (DataSet) null;
        foreach (IdeaStageStatus item in lst)
        {
          item.StatusId = status.Id;
          item.Status = status;
        }
        status.IdeaStageStatuses = lst;
        lst = (List<IdeaStageStatus>) null;
      }
    }

    public void GetForStatus(IEnumerable<IdeaStatus> statuses) => this.GetForStatusAsync(statuses).RunSynchronously();

    public async Task GetForStatusAsync(IEnumerable<IdeaStatus> statuses)
    {
      if (statuses == null)
        return;
      foreach (IdeaStatus status in statuses)
        await this.GetForStatusAsync(status);
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      IdeaStageStatus entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(IdeaStageStatus entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(IdeaStageStatus entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<IdeaStageStatus> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaStageStatus> entities)
    {
      if (entities == null)
        throw new DuplicateException("The ideaStageStatuses are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
