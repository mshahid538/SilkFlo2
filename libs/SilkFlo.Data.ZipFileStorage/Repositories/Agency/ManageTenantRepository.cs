// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Agency.ManageTenantRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Repositories.Agency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SilkFlo.Data.Persistence.Repositories.Agency
{
  public class ManageTenantRepository : IManageTenantRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public ManageTenantRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public ManageTenant Get(string id) => this.GetAsync(id).Result;

    public async Task<ManageTenant> GetAsync(string id)
    {
      if (id == null)
        return (ManageTenant) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.AgencyManageTenants.SingleOrDefault<ManageTenant>((Func<ManageTenant, bool>) (x => x.Id == id));
    }

    public ManageTenant SingleOrDefault(Func<ManageTenant, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<ManageTenant> SingleOrDefaultAsync(Func<ManageTenant, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.AgencyManageTenants.Where<ManageTenant>(predicate).FirstOrDefault<ManageTenant>();
    }

    public bool Add(ManageTenant entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(ManageTenant entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<ManageTenant> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<ManageTenant> entities)
    {
      if (entities == null)
        return false;
      foreach (ManageTenant entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<ManageTenant> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<ManageTenant>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<ManageTenant>) dataSetAsync.AgencyManageTenants;
    }

    public IEnumerable<ManageTenant> Find(Func<ManageTenant, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<ManageTenant>> FindAsync(Func<ManageTenant, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.AgencyManageTenants.Where<ManageTenant>(predicate);
    }

    public void GetForTenant(Client tenant) => this.GetForTenantAsync(tenant).RunSynchronously();

    public async Task GetForTenantAsync(Client tenant)
    {
      List<ManageTenant> lst;
      if (tenant == null)
        lst = (List<ManageTenant>) null;
      else if (string.IsNullOrWhiteSpace(tenant.Id))
      {
        lst = (List<ManageTenant>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.AgencyManageTenants.Where<ManageTenant>((Func<ManageTenant, bool>) (x => x.TenantId == tenant.Id)).ToList<ManageTenant>();
        dataSet = (DataSet) null;
        foreach (ManageTenant item in lst)
        {
          item.TenantId = tenant.Id;
          item.Tenant = tenant;
        }
        tenant.ManageTenants = lst;
        lst = (List<ManageTenant>) null;
      }
    }

    public void GetForTenant(IEnumerable<Client> tenants) => this.GetForTenantAsync(tenants).RunSynchronously();

    public async Task GetForTenantAsync(IEnumerable<Client> tenants)
    {
      if (tenants == null)
        return;
      foreach (Client tenant in tenants)
        await this.GetForTenantAsync(tenant);
    }

    public void GetForUser(User user) => this.GetForUserAsync(user).RunSynchronously();

    public async Task GetForUserAsync(User user)
    {
      List<ManageTenant> lst;
      if (user == null)
        lst = (List<ManageTenant>) null;
      else if (string.IsNullOrWhiteSpace(user.Id))
      {
        lst = (List<ManageTenant>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.AgencyManageTenants.Where<ManageTenant>((Func<ManageTenant, bool>) (x => x.UserId == user.Id)).ToList<ManageTenant>();
        dataSet = (DataSet) null;
        foreach (ManageTenant item in lst)
        {
          item.UserId = user.Id;
          item.User = user;
        }
        user.ManageTenants = lst;
        lst = (List<ManageTenant>) null;
      }
    }

    public void GetForUser(IEnumerable<User> users) => this.GetForUserAsync(users).RunSynchronously();

    public async Task GetForUserAsync(IEnumerable<User> users)
    {
      if (users == null)
        return;
      foreach (User user in users)
        await this.GetForUserAsync(user);
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      ManageTenant entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(ManageTenant entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(ManageTenant entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<ManageTenant> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ManageTenant> entities)
    {
      if (entities == null)
        throw new DuplicateException("The manageTenants are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
