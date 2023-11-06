// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.IdeaRunningCostRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Repositories.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class IdeaRunningCostRepository : IIdeaRunningCostRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public IdeaRunningCostRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public IdeaRunningCost Get(string id) => this.GetAsync(id).Result;

    public async Task<IdeaRunningCost> GetAsync(string id)
    {
      if (id == null)
        return (IdeaRunningCost) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaRunningCosts.SingleOrDefault<IdeaRunningCost>((Func<IdeaRunningCost, bool>) (x => x.Id == id));
    }

    public IdeaRunningCost SingleOrDefault(Func<IdeaRunningCost, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<IdeaRunningCost> SingleOrDefaultAsync(Func<IdeaRunningCost, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaRunningCosts.Where<IdeaRunningCost>(predicate).FirstOrDefault<IdeaRunningCost>();
    }

    public bool Add(IdeaRunningCost entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(IdeaRunningCost entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<IdeaRunningCost> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<IdeaRunningCost> entities)
    {
      if (entities == null)
        return false;
      foreach (IdeaRunningCost entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<IdeaRunningCost> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<IdeaRunningCost>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<IdeaRunningCost>) dataSetAsync.BusinessIdeaRunningCosts;
    }

    public IEnumerable<IdeaRunningCost> Find(Func<IdeaRunningCost, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<IdeaRunningCost>> FindAsync(Func<IdeaRunningCost, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaRunningCosts.Where<IdeaRunningCost>(predicate);
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<IdeaRunningCost> lst;
      if (client == null)
        lst = (List<IdeaRunningCost>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<IdeaRunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaRunningCosts.Where<IdeaRunningCost>((Func<IdeaRunningCost, bool>) (x => x.ClientId == client.Id)).ToList<IdeaRunningCost>();
        //dataSet = (DataSet) null;
        foreach (IdeaRunningCost item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.IdeaRunningCosts = lst;
        lst = (List<IdeaRunningCost>) null;
      }
    }

    public void GetForClient(IEnumerable<Client> clients) => this.GetForClientAsync(clients).RunSynchronously();

    public async Task GetForClientAsync(IEnumerable<Client> clients)
    {
      if (clients == null)
        return;
      foreach (Client client in clients)
        await this.GetForClientAsync(client);
    }

    public void GetForIdea(Idea idea) => this.GetForIdeaAsync(idea).RunSynchronously();

    public async Task GetForIdeaAsync(Idea idea)
    {
      List<IdeaRunningCost> lst;
      if (idea == null)
        lst = (List<IdeaRunningCost>) null;
      else if (string.IsNullOrWhiteSpace(idea.Id))
      {
        lst = (List<IdeaRunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaRunningCosts.Where<IdeaRunningCost>((Func<IdeaRunningCost, bool>) (x => x.IdeaId == idea.Id)).ToList<IdeaRunningCost>();
        //dataSet = (DataSet) null;
        foreach (IdeaRunningCost item in lst)
        {
          item.IdeaId = idea.Id;
          item.Idea = idea;
        }
        idea.IdeaRunningCosts = lst;
        lst = (List<IdeaRunningCost>) null;
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

    public void GetForRunningCost(RunningCost runningCost) => this.GetForRunningCostAsync(runningCost).RunSynchronously();

    public async Task GetForRunningCostAsync(RunningCost runningCost)
    {
      List<IdeaRunningCost> lst;
      if (runningCost == null)
        lst = (List<IdeaRunningCost>) null;
      else if (string.IsNullOrWhiteSpace(runningCost.Id))
      {
        lst = (List<IdeaRunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaRunningCosts.Where<IdeaRunningCost>((Func<IdeaRunningCost, bool>) (x => x.RunningCostId == runningCost.Id)).ToList<IdeaRunningCost>();
        //dataSet = (DataSet) null;
        foreach (IdeaRunningCost item in lst)
        {
          item.RunningCostId = runningCost.Id;
          item.RunningCost = runningCost;
        }
        runningCost.IdeaRunningCosts = lst;
        lst = (List<IdeaRunningCost>) null;
      }
    }

    public void GetForRunningCost(IEnumerable<RunningCost> runningCosts) => this.GetForRunningCostAsync(runningCosts).RunSynchronously();

    public async Task GetForRunningCostAsync(IEnumerable<RunningCost> runningCosts)
    {
      if (runningCosts == null)
        return;
      foreach (RunningCost runningCost in runningCosts)
        await this.GetForRunningCostAsync(runningCost);
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      IdeaRunningCost entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(IdeaRunningCost entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(IdeaRunningCost entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<IdeaRunningCost> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaRunningCost> entities)
    {
      if (entities == null)
        throw new DuplicateException("The ideaRunningCosts are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
