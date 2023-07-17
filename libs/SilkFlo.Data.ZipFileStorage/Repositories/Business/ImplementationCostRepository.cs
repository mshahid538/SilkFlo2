// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.ImplementationCostRepository
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
  public class ImplementationCostRepository : IImplementationCostRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public ImplementationCostRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public ImplementationCost Get(string id) => this.GetAsync(id).Result;

    public async Task<ImplementationCost> GetAsync(string id)
    {
      if (id == null)
        return (ImplementationCost) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessImplementationCosts.SingleOrDefault<ImplementationCost>((Func<ImplementationCost, bool>) (x => x.Id == id));
    }

    public ImplementationCost SingleOrDefault(Func<ImplementationCost, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<ImplementationCost> SingleOrDefaultAsync(
      Func<ImplementationCost, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessImplementationCosts.Where<ImplementationCost>(predicate).FirstOrDefault<ImplementationCost>();
    }

    public bool Add(ImplementationCost entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(ImplementationCost entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<ImplementationCost> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<ImplementationCost> entities)
    {
      if (entities == null)
        return false;
      foreach (ImplementationCost entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<ImplementationCost> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<ImplementationCost>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<ImplementationCost>) dataSetAsync.BusinessImplementationCosts;
    }

    public IEnumerable<ImplementationCost> Find(Func<ImplementationCost, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<ImplementationCost>> FindAsync(
      Func<ImplementationCost, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessImplementationCosts.Where<ImplementationCost>(predicate);
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<ImplementationCost> lst;
      if (client == null)
        lst = (List<ImplementationCost>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<ImplementationCost>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessImplementationCosts.Where<ImplementationCost>((Func<ImplementationCost, bool>) (x => x.ClientId == client.Id)).ToList<ImplementationCost>();
        dataSet = (DataSet) null;
        foreach (ImplementationCost item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.ImplementationCosts = lst;
        lst = (List<ImplementationCost>) null;
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

    public void GetForIdeaStage(IdeaStage ideaStage) => this.GetForIdeaStageAsync(ideaStage).RunSynchronously();

    public async Task GetForIdeaStageAsync(IdeaStage ideaStage)
    {
      List<ImplementationCost> lst;
      if (ideaStage == null)
        lst = (List<ImplementationCost>) null;
      else if (string.IsNullOrWhiteSpace(ideaStage.Id))
      {
        lst = (List<ImplementationCost>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessImplementationCosts.Where<ImplementationCost>((Func<ImplementationCost, bool>) (x => x.IdeaStageId == ideaStage.Id)).ToList<ImplementationCost>();
        dataSet = (DataSet) null;
        foreach (ImplementationCost item in lst)
        {
          item.IdeaStageId = ideaStage.Id;
          item.IdeaStage = ideaStage;
        }
        ideaStage.ImplementationCosts = lst;
        lst = (List<ImplementationCost>) null;
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

    public void GetForRole(Role role) => this.GetForRoleAsync(role).RunSynchronously();

    public async Task GetForRoleAsync(Role role)
    {
      List<ImplementationCost> lst;
      if (role == null)
        lst = (List<ImplementationCost>) null;
      else if (string.IsNullOrWhiteSpace(role.Id))
      {
        lst = (List<ImplementationCost>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessImplementationCosts.Where<ImplementationCost>((Func<ImplementationCost, bool>) (x => x.RoleId == role.Id)).ToList<ImplementationCost>();
        dataSet = (DataSet) null;
        foreach (ImplementationCost item in lst)
        {
          item.RoleId = role.Id;
          item.Role = role;
        }
        role.ImplementationCosts = lst;
        lst = (List<ImplementationCost>) null;
      }
    }

    public void GetForRole(IEnumerable<Role> roles) => this.GetForRoleAsync(roles).RunSynchronously();

    public async Task GetForRoleAsync(IEnumerable<Role> roles)
    {
      if (roles == null)
        return;
      foreach (Role role in roles)
        await this.GetForRoleAsync(role);
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      ImplementationCost entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(ImplementationCost entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(ImplementationCost entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<ImplementationCost> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ImplementationCost> entities)
    {
      if (entities == null)
        throw new DuplicateException("The implementationCosts are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
