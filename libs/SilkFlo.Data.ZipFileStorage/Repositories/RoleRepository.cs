// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.RoleRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories
{
  public class RoleRepository : IRoleRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public RoleRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Role Get(string id) => this.GetAsync(id).Result;

    public async Task<Role> GetAsync(string id)
    {
      if (id == null)
        return (Role) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.Roles.SingleOrDefault<Role>((Func<Role, bool>) (x => x.Id == id));
    }

    public Role SingleOrDefault(Func<Role, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Role> SingleOrDefaultAsync(Func<Role, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.Roles.Where<Role>(predicate).FirstOrDefault<Role>();
    }

    public bool Add(Role entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Role entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Role> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Role> entities)
    {
      if (entities == null)
        return false;
      foreach (Role entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Role> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Role>) dataSetAsync.Roles.OrderByDescending<Role, int>((Func<Role, int>) (m => m.Sort)).ThenBy<Role, string>((Func<Role, string>) (m => m.Name));
    }

    public IEnumerable<Role> Find(Func<Role, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Role>> FindAsync(Func<Role, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Role>) dataSetAsync.Roles.Where<Role>(predicate).OrderByDescending<Role, int>((Func<Role, int>) (m => m.Sort)).ThenBy<Role, string>((Func<Role, string>) (m => m.Name));
    }

    public Role GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Role> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Role) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.Roles.SingleOrDefault<Role>((Func<Role, bool>) (x => x.Name == name));
    }

    public void GetRoleFor(IEnumerable<UserRole> userRoles) => this.GetRoleForAsync(userRoles).RunSynchronously();

    public async Task GetRoleForAsync(IEnumerable<UserRole> userRoles)
    {
      if (userRoles == null)
        return;
      foreach (UserRole userRole in userRoles)
        await this.GetRoleForAsync(userRole);
    }

    public void GetRoleFor(UserRole userRole) => this.GetRoleForAsync(userRole).RunSynchronously();

    public async Task GetRoleForAsync(UserRole userRole)
    {
      if (userRole == null)
        ;
      else
      {
        UserRole userRole1 = userRole;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        userRole1.Role = dataSet.Roles.SingleOrDefault<Role>((Func<Role, bool>) (x => x.Id == userRole.RoleId));
        userRole1 = (UserRole) null;
        //dataSet = (DataSet) null;
      }
    }

    public Role GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Role> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Role) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.Roles.SingleOrDefault<Role>((Func<Role, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Role entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Role entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Role entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Role> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Role> entities)
    {
      if (entities == null)
        throw new DuplicateException("The roles are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }

    public bool IsMember(string userId, string roleName)
    {
      try
      {
        return this.IsMemberAsync(userId, roleName).Result;
      }
      catch
      {
        throw;
      }
    }

    public async Task<bool> IsMemberAsync(string userId, string roleName)
    {
      try
      {
        if (userId == null || string.IsNullOrWhiteSpace(roleName))
          return false;
        User user = await this._unitOfWork.Users.GetAsync(userId);
        return this.IsMember(user, roleName);
      }
      catch
      {
        throw;
      }
    }

    public bool IsMember(User user, string roleName)
    {
      try
      {
        return this.IsMemberAsync(user, roleName).Result;
      }
      catch
      {
        throw;
      }
    }

    public async Task<bool> IsMemberAsync(User user, string roleName)
    {
      try
      {
        if (user == null || string.IsNullOrWhiteSpace(roleName))
          return false;
        roleName = roleName.ToLower();
        await this._unitOfWork.UserRoles.GetForUserAsync(user);
        await this._unitOfWork.Roles.GetRoleForAsync((IEnumerable<UserRole>) user.UserRoles);
        foreach (UserRole userRole in user.UserRoles)
        {
          if (userRole.Role.Name.ToLower() == roleName)
            return true;
        }
        return false;
      }
      catch
      {
        throw;
      }
    }
  }
}
