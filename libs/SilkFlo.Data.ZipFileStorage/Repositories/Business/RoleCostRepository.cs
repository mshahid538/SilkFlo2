// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.RoleCostRepository
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
  public class RoleCostRepository : IRoleCostRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public RoleCostRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public RoleCost Get(string id) => this.GetAsync(id).Result;

    public async Task<RoleCost> GetAsync(string id)
    {
      if (id == null)
        return (RoleCost) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRoleCosts.SingleOrDefault<RoleCost>((Func<RoleCost, bool>) (x => x.Id == id));
    }

    public RoleCost SingleOrDefault(Func<RoleCost, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<RoleCost> SingleOrDefaultAsync(Func<RoleCost, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRoleCosts.Where<RoleCost>(predicate).FirstOrDefault<RoleCost>();
    }

    public bool Add(RoleCost entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(RoleCost entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<RoleCost> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<RoleCost> entities)
    {
      if (entities == null)
        return false;
      foreach (RoleCost entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<RoleCost> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<RoleCost>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<RoleCost>) dataSetAsync.BusinessRoleCosts;
    }

    public IEnumerable<RoleCost> Find(Func<RoleCost, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<RoleCost>> FindAsync(Func<RoleCost, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRoleCosts.Where<RoleCost>(predicate);
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<RoleCost> lst;
      if (client == null)
        lst = (List<RoleCost>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<RoleCost>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessRoleCosts.Where<RoleCost>((Func<RoleCost, bool>) (x => x.ClientId == client.Id)).ToList<RoleCost>();
        dataSet = (DataSet) null;
        foreach (RoleCost item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.RoleCosts = lst;
        lst = (List<RoleCost>) null;
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

    public void GetForRole(Role role) => this.GetForRoleAsync(role).RunSynchronously();

    public async Task GetForRoleAsync(Role role)
    {
      List<RoleCost> lst;
      if (role == null)
        lst = (List<RoleCost>) null;
      else if (string.IsNullOrWhiteSpace(role.Id))
      {
        lst = (List<RoleCost>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessRoleCosts.Where<RoleCost>((Func<RoleCost, bool>) (x => x.RoleId == role.Id)).ToList<RoleCost>();
        dataSet = (DataSet) null;
        foreach (RoleCost item in lst)
        {
          item.RoleId = role.Id;
          item.Role = role;
        }
        role.RoleCosts = lst;
        lst = (List<RoleCost>) null;
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
      RoleCost entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(RoleCost entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(RoleCost entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<RoleCost> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<RoleCost> entities)
    {
      if (entities == null)
        throw new DuplicateException("The roleCosts are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
