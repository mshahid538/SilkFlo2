// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.IdeaStageRepository
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
  public class IdeaStageRepository : IIdeaStageRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public IdeaStageRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public IdeaStage Get(string id) => this.GetAsync(id).Result;

    public async Task<IdeaStage> GetAsync(string id)
    {
      if (id == null)
        return (IdeaStage) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaStages.SingleOrDefault<IdeaStage>((Func<IdeaStage, bool>) (x => x.Id == id));
    }

    public IdeaStage SingleOrDefault(Func<IdeaStage, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<IdeaStage> SingleOrDefaultAsync(Func<IdeaStage, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaStages.Where<IdeaStage>(predicate).FirstOrDefault<IdeaStage>();
    }

    public bool Add(IdeaStage entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(IdeaStage entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<IdeaStage> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<IdeaStage> entities)
    {
      if (entities == null)
        return false;
      foreach (IdeaStage entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<IdeaStage> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<IdeaStage>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<IdeaStage>) dataSetAsync.BusinessIdeaStages.OrderBy<IdeaStage, string>((Func<IdeaStage, string>) (m => m.IdeaId)).ThenBy<IdeaStage, DateTime>((Func<IdeaStage, DateTime>) (m => m.DateStartEstimate)).ThenBy<IdeaStage, string>((Func<IdeaStage, string>) (m => m.StageId));
    }

    public IEnumerable<IdeaStage> Find(Func<IdeaStage, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<IdeaStage>> FindAsync(Func<IdeaStage, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<IdeaStage>) dataSetAsync.BusinessIdeaStages.Where<IdeaStage>(predicate).OrderBy<IdeaStage, string>((Func<IdeaStage, string>) (m => m.IdeaId)).ThenBy<IdeaStage, DateTime>((Func<IdeaStage, DateTime>) (m => m.DateStartEstimate)).ThenBy<IdeaStage, string>((Func<IdeaStage, string>) (m => m.StageId));
    }

    public void GetForIdea(Idea idea) => this.GetForIdeaAsync(idea).RunSynchronously();

    public async Task GetForIdeaAsync(Idea idea)
    {
      List<IdeaStage> lst;
      if (idea == null)
        lst = (List<IdeaStage>) null;
      else if (string.IsNullOrWhiteSpace(idea.Id))
      {
        lst = (List<IdeaStage>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaStages.Where<IdeaStage>((Func<IdeaStage, bool>) (x => x.IdeaId == idea.Id)).OrderBy<IdeaStage, string>((Func<IdeaStage, string>) (x => x.IdeaId)).ThenBy<IdeaStage, DateTime>((Func<IdeaStage, DateTime>) (x => x.DateStartEstimate)).ThenBy<IdeaStage, string>((Func<IdeaStage, string>) (x => x.StageId)).ToList<IdeaStage>();
        //dataSet = (DataSet) null;
        foreach (IdeaStage item in lst)
        {
          item.IdeaId = idea.Id;
          item.Idea = idea;
        }
        idea.IdeaStages = lst;
        lst = (List<IdeaStage>) null;
      }
    }

    public void GetForIdea(IEnumerable<Idea> ideas) => this.GetForIdeaAsync(ideas).RunSynchronously();

    public async Task GetForIdeaAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetForIdeaAsync(idea);
    }

    public void GetForStage(Stage stage) => this.GetForStageAsync(stage).RunSynchronously();

    public async Task GetForStageAsync(Stage stage)
    {
      List<IdeaStage> lst;
      if (stage == null)
        lst = (List<IdeaStage>) null;
      else if (string.IsNullOrWhiteSpace(stage.Id))
      {
        lst = (List<IdeaStage>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaStages.Where<IdeaStage>((Func<IdeaStage, bool>) (x => x.StageId == stage.Id)).OrderBy<IdeaStage, string>((Func<IdeaStage, string>) (x => x.IdeaId)).ThenBy<IdeaStage, DateTime>((Func<IdeaStage, DateTime>) (x => x.DateStartEstimate)).ThenBy<IdeaStage, string>((Func<IdeaStage, string>) (x => x.StageId)).ToList<IdeaStage>();
        //dataSet = (DataSet) null;
        foreach (IdeaStage item in lst)
        {
          item.StageId = stage.Id;
          item.Stage = stage;
        }
        stage.IdeaStages = lst;
        lst = (List<IdeaStage>) null;
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

    public void GetIdeaStageFor(IEnumerable<IdeaStageStatus> ideaStageStatuses) => this.GetIdeaStageForAsync(ideaStageStatuses).RunSynchronously();

    public async Task GetIdeaStageForAsync(IEnumerable<IdeaStageStatus> ideaStageStatuses)
    {
      if (ideaStageStatuses == null)
        return;
      foreach (IdeaStageStatus ideaStageStatus in ideaStageStatuses)
        await this.GetIdeaStageForAsync(ideaStageStatus);
    }

    public void GetIdeaStageFor(IdeaStageStatus ideaStageStatus) => this.GetIdeaStageForAsync(ideaStageStatus).RunSynchronously();

    public async Task GetIdeaStageForAsync(IdeaStageStatus ideaStageStatus)
    {
      if (ideaStageStatus == null)
        ;
      else
      {
        IdeaStageStatus ideaStageStatus1 = ideaStageStatus;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        ideaStageStatus1.IdeaStage = dataSet.BusinessIdeaStages.SingleOrDefault<IdeaStage>((Func<IdeaStage, bool>) (x => x.Id == ideaStageStatus.IdeaStageId));
        ideaStageStatus1 = (IdeaStageStatus) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetIdeaStageFor(
      IEnumerable<ImplementationCost> implementationCosts)
    {
      this.GetIdeaStageForAsync(implementationCosts).RunSynchronously();
    }

    public async Task GetIdeaStageForAsync(
      IEnumerable<ImplementationCost> implementationCosts)
    {
      if (implementationCosts == null)
        return;
      foreach (ImplementationCost implementationCost in implementationCosts)
        await this.GetIdeaStageForAsync(implementationCost);
    }

    public void GetIdeaStageFor(ImplementationCost implementationCost) => this.GetIdeaStageForAsync(implementationCost).RunSynchronously();

    public async Task GetIdeaStageForAsync(ImplementationCost implementationCost)
    {
      if (implementationCost == null)
        ;
      else
      {
        ImplementationCost implementationCost1 = implementationCost;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        implementationCost1.IdeaStage = dataSet.BusinessIdeaStages.SingleOrDefault<IdeaStage>((Func<IdeaStage, bool>) (x => x.Id == implementationCost.IdeaStageId));
        implementationCost1 = (ImplementationCost) null;
        //dataSet = (DataSet) null;
      }
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      IdeaStage entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(IdeaStage entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(IdeaStage entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<IdeaStage> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaStage> entities)
    {
      if (entities == null)
        throw new DuplicateException("The ideaStages are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
