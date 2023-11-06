// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.UserRoleRepository
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
  public class UserRoleRepository : IUserRoleRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public UserRoleRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public UserRole Get(string id) => this.GetAsync(id).Result;

    public async Task<UserRole> GetAsync(string id)
    {
      if (id == null)
        return (UserRole) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.UserRoles.SingleOrDefault<UserRole>((Func<UserRole, bool>) (x => x.Id == id));
    }

    public UserRole SingleOrDefault(Func<UserRole, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<UserRole> SingleOrDefaultAsync(Func<UserRole, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.UserRoles.Where<UserRole>(predicate).FirstOrDefault<UserRole>();
    }

    public bool Add(UserRole entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(UserRole entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<UserRole> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<UserRole> entities)
    {
      if (entities == null)
        return false;
      foreach (UserRole entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<UserRole> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<UserRole>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<UserRole>) dataSetAsync.UserRoles;
    }

    public IEnumerable<UserRole> Find(Func<UserRole, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<UserRole>> FindAsync(Func<UserRole, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.UserRoles.Where<UserRole>(predicate);
    }

    public void GetForRole(Role role) => this.GetForRoleAsync(role).RunSynchronously();

    public async Task GetForRoleAsync(Role role)
    {
      List<UserRole> lst;
      if (role == null)
        lst = (List<UserRole>) null;
      else if (string.IsNullOrWhiteSpace(role.Id))
      {
        lst = (List<UserRole>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.UserRoles.Where<UserRole>((Func<UserRole, bool>) (x => x.RoleId == role.Id)).ToList<UserRole>();
        //dataSet = (DataSet) null;
        foreach (UserRole item in lst)
        {
          item.RoleId = role.Id;
          item.Role = role;
        }
        role.UserRoles = lst;
        lst = (List<UserRole>) null;
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

    public void GetForUser(User user) => this.GetForUserAsync(user).RunSynchronously();

    public async Task GetForUserAsync(User user)
    {
      List<UserRole> lst;
      if (user == null)
        lst = (List<UserRole>) null;
      else if (string.IsNullOrWhiteSpace(user.Id))
      {
        lst = (List<UserRole>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.UserRoles.Where<UserRole>((Func<UserRole, bool>) (x => x.UserId == user.Id)).ToList<UserRole>();
        //dataSet = (DataSet) null;
        foreach (UserRole item in lst)
        {
          item.UserId = user.Id;
          item.User = user;
        }
        user.UserRoles = lst;
        lst = (List<UserRole>) null;
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
      UserRole entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(UserRole entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(UserRole entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<UserRole> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<UserRole> entities)
    {
      if (entities == null)
        throw new DuplicateException("The userRoles are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
