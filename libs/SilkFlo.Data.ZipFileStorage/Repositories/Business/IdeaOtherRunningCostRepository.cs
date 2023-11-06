// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.IdeaOtherRunningCostRepository
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
  public class IdeaOtherRunningCostRepository : IIdeaOtherRunningCostRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public IdeaOtherRunningCostRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public IdeaOtherRunningCost Get(string id) => this.GetAsync(id).Result;

    public async Task<IdeaOtherRunningCost> GetAsync(string id)
    {
      if (id == null)
        return (IdeaOtherRunningCost) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaOtherRunningCosts.SingleOrDefault<IdeaOtherRunningCost>((Func<IdeaOtherRunningCost, bool>) (x => x.Id == id));
    }

    public IdeaOtherRunningCost SingleOrDefault(Func<IdeaOtherRunningCost, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<IdeaOtherRunningCost> SingleOrDefaultAsync(
      Func<IdeaOtherRunningCost, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaOtherRunningCosts.Where<IdeaOtherRunningCost>(predicate).FirstOrDefault<IdeaOtherRunningCost>();
    }

    public bool Add(IdeaOtherRunningCost entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(IdeaOtherRunningCost entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<IdeaOtherRunningCost> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<IdeaOtherRunningCost> entities)
    {
      if (entities == null)
        return false;
      foreach (IdeaOtherRunningCost entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<IdeaOtherRunningCost> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<IdeaOtherRunningCost>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<IdeaOtherRunningCost>) dataSetAsync.BusinessIdeaOtherRunningCosts;
    }

    public IEnumerable<IdeaOtherRunningCost> Find(Func<IdeaOtherRunningCost, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<IdeaOtherRunningCost>> FindAsync(
      Func<IdeaOtherRunningCost, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaOtherRunningCosts.Where<IdeaOtherRunningCost>(predicate);
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<IdeaOtherRunningCost> lst;
      if (client == null)
        lst = (List<IdeaOtherRunningCost>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<IdeaOtherRunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaOtherRunningCosts.Where<IdeaOtherRunningCost>((Func<IdeaOtherRunningCost, bool>) (x => x.ClientId == client.Id)).ToList<IdeaOtherRunningCost>();
        //dataSet = (DataSet) null;
        foreach (IdeaOtherRunningCost item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.IdeaOtherRunningCosts = lst;
        lst = (List<IdeaOtherRunningCost>) null;
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
      List<IdeaOtherRunningCost> lst;
      if (idea == null)
        lst = (List<IdeaOtherRunningCost>) null;
      else if (string.IsNullOrWhiteSpace(idea.Id))
      {
        lst = (List<IdeaOtherRunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaOtherRunningCosts.Where<IdeaOtherRunningCost>((Func<IdeaOtherRunningCost, bool>) (x => x.IdeaId == idea.Id)).ToList<IdeaOtherRunningCost>();
        //dataSet = (DataSet) null;
        foreach (IdeaOtherRunningCost item in lst)
        {
          item.IdeaId = idea.Id;
          item.Idea = idea;
        }
        idea.IdeaOtherRunningCosts = lst;
        lst = (List<IdeaOtherRunningCost>) null;
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

    public void GetForOtherRunningCost(OtherRunningCost otherRunningCost) => this.GetForOtherRunningCostAsync(otherRunningCost).RunSynchronously();

    public async Task GetForOtherRunningCostAsync(OtherRunningCost otherRunningCost)
    {
      List<IdeaOtherRunningCost> lst;
      if (otherRunningCost == null)
        lst = (List<IdeaOtherRunningCost>) null;
      else if (string.IsNullOrWhiteSpace(otherRunningCost.Id))
      {
        lst = (List<IdeaOtherRunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaOtherRunningCosts.Where<IdeaOtherRunningCost>((Func<IdeaOtherRunningCost, bool>) (x => x.OtherRunningCostId == otherRunningCost.Id)).ToList<IdeaOtherRunningCost>();
        //dataSet = (DataSet) null;
        foreach (IdeaOtherRunningCost item in lst)
        {
          item.OtherRunningCostId = otherRunningCost.Id;
          item.OtherRunningCost = otherRunningCost;
        }
        otherRunningCost.IdeaOtherRunningCosts = lst;
        lst = (List<IdeaOtherRunningCost>) null;
      }
    }

    public void GetForOtherRunningCost(IEnumerable<OtherRunningCost> otherRunningCosts) => this.GetForOtherRunningCostAsync(otherRunningCosts).RunSynchronously();

    public async Task GetForOtherRunningCostAsync(IEnumerable<OtherRunningCost> otherRunningCosts)
    {
      if (otherRunningCosts == null)
        return;
      foreach (OtherRunningCost otherRunningCost in otherRunningCosts)
        await this.GetForOtherRunningCostAsync(otherRunningCost);
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      IdeaOtherRunningCost entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(IdeaOtherRunningCost entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(IdeaOtherRunningCost entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<IdeaOtherRunningCost> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaOtherRunningCost> entities)
    {
      if (entities == null)
        throw new DuplicateException("The ideaOtherRunningCosts are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
