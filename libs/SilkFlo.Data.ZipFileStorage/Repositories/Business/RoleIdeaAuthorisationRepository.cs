// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.RoleIdeaAuthorisationRepository
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
  public class RoleIdeaAuthorisationRepository : IRoleIdeaAuthorisationRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public RoleIdeaAuthorisationRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public RoleIdeaAuthorisation Get(string id) => this.GetAsync(id).Result;

    public async Task<RoleIdeaAuthorisation> GetAsync(string id)
    {
      if (id == null)
        return (RoleIdeaAuthorisation) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRoleIdeaAuthorisations.SingleOrDefault<RoleIdeaAuthorisation>((Func<RoleIdeaAuthorisation, bool>) (x => x.Id == id));
    }

    public RoleIdeaAuthorisation SingleOrDefault(Func<RoleIdeaAuthorisation, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<RoleIdeaAuthorisation> SingleOrDefaultAsync(
      Func<RoleIdeaAuthorisation, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRoleIdeaAuthorisations.Where<RoleIdeaAuthorisation>(predicate).FirstOrDefault<RoleIdeaAuthorisation>();
    }

    public bool Add(RoleIdeaAuthorisation entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(RoleIdeaAuthorisation entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<RoleIdeaAuthorisation> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<RoleIdeaAuthorisation> entities)
    {
      if (entities == null)
        return false;
      foreach (RoleIdeaAuthorisation entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<RoleIdeaAuthorisation> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<RoleIdeaAuthorisation>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<RoleIdeaAuthorisation>) dataSetAsync.BusinessRoleIdeaAuthorisations;
    }

    public IEnumerable<RoleIdeaAuthorisation> Find(Func<RoleIdeaAuthorisation, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<RoleIdeaAuthorisation>> FindAsync(
      Func<RoleIdeaAuthorisation, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRoleIdeaAuthorisations.Where<RoleIdeaAuthorisation>(predicate);
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<RoleIdeaAuthorisation> lst;
      if (client == null)
        lst = (List<RoleIdeaAuthorisation>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<RoleIdeaAuthorisation>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessRoleIdeaAuthorisations.Where<RoleIdeaAuthorisation>((Func<RoleIdeaAuthorisation, bool>) (x => x.ClientId == client.Id)).ToList<RoleIdeaAuthorisation>();
        dataSet = (DataSet) null;
        foreach (RoleIdeaAuthorisation item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.RoleIdeaAuthorisations = lst;
        lst = (List<RoleIdeaAuthorisation>) null;
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

    public void GetForIdeaAuthorisation(IdeaAuthorisation ideaAuthorisation) => this.GetForIdeaAuthorisationAsync(ideaAuthorisation).RunSynchronously();

    public async Task GetForIdeaAuthorisationAsync(IdeaAuthorisation ideaAuthorisation)
    {
      List<RoleIdeaAuthorisation> lst;
      if (ideaAuthorisation == null)
        lst = (List<RoleIdeaAuthorisation>) null;
      else if (string.IsNullOrWhiteSpace(ideaAuthorisation.Id))
      {
        lst = (List<RoleIdeaAuthorisation>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessRoleIdeaAuthorisations.Where<RoleIdeaAuthorisation>((Func<RoleIdeaAuthorisation, bool>) (x => x.IdeaAuthorisationId == ideaAuthorisation.Id)).ToList<RoleIdeaAuthorisation>();
        dataSet = (DataSet) null;
        foreach (RoleIdeaAuthorisation item in lst)
        {
          item.IdeaAuthorisationId = ideaAuthorisation.Id;
          item.IdeaAuthorisation = ideaAuthorisation;
        }
        ideaAuthorisation.RoleIdeaAuthorisations = lst;
        lst = (List<RoleIdeaAuthorisation>) null;
      }
    }

    public void GetForIdeaAuthorisation(IEnumerable<IdeaAuthorisation> ideaAuthorisations) => this.GetForIdeaAuthorisationAsync(ideaAuthorisations).RunSynchronously();

    public async Task GetForIdeaAuthorisationAsync(IEnumerable<IdeaAuthorisation> ideaAuthorisations)
    {
      if (ideaAuthorisations == null)
        return;
      foreach (IdeaAuthorisation ideaAuthorisation in ideaAuthorisations)
        await this.GetForIdeaAuthorisationAsync(ideaAuthorisation);
    }

    public void GetForRole(Role role) => this.GetForRoleAsync(role).RunSynchronously();

    public async Task GetForRoleAsync(Role role)
    {
      List<RoleIdeaAuthorisation> lst;
      if (role == null)
        lst = (List<RoleIdeaAuthorisation>) null;
      else if (string.IsNullOrWhiteSpace(role.Id))
      {
        lst = (List<RoleIdeaAuthorisation>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessRoleIdeaAuthorisations.Where<RoleIdeaAuthorisation>((Func<RoleIdeaAuthorisation, bool>) (x => x.RoleId == role.Id)).ToList<RoleIdeaAuthorisation>();
        dataSet = (DataSet) null;
        foreach (RoleIdeaAuthorisation item in lst)
        {
          item.RoleId = role.Id;
          item.Role = role;
        }
        role.RoleIdeaAuthorisations = lst;
        lst = (List<RoleIdeaAuthorisation>) null;
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
      RoleIdeaAuthorisation entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(RoleIdeaAuthorisation entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(RoleIdeaAuthorisation entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<RoleIdeaAuthorisation> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<RoleIdeaAuthorisation> entities)
    {
      if (entities == null)
        throw new DuplicateException("The roleIdeaAuthorisations are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
