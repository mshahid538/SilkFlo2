// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.UserBadgeRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories
{
  public class UserBadgeRepository : IUserBadgeRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public UserBadgeRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public UserBadge Get(string id) => this.GetAsync(id).Result;

    public async Task<UserBadge> GetAsync(string id)
    {
      if (id == null)
        return (UserBadge) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.UserBadges.SingleOrDefault<UserBadge>((Func<UserBadge, bool>) (x => x.Id == id));
    }

    public UserBadge SingleOrDefault(Func<UserBadge, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<UserBadge> SingleOrDefaultAsync(Func<UserBadge, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.UserBadges.Where<UserBadge>(predicate).FirstOrDefault<UserBadge>();
    }

    public bool Add(UserBadge entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(UserBadge entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<UserBadge> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<UserBadge> entities)
    {
      if (entities == null)
        return false;
      foreach (UserBadge entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<UserBadge> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<UserBadge>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<UserBadge>) dataSetAsync.UserBadges;
    }

    public IEnumerable<UserBadge> Find(Func<UserBadge, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<UserBadge>> FindAsync(Func<UserBadge, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.UserBadges.Where<UserBadge>(predicate);
    }

    public void GetForBadge(Badge badge) => this.GetForBadgeAsync(badge).RunSynchronously();

    public async Task GetForBadgeAsync(Badge badge)
    {
      List<UserBadge> lst;
      if (badge == null)
        lst = (List<UserBadge>) null;
      else if (string.IsNullOrWhiteSpace(badge.Id))
      {
        lst = (List<UserBadge>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.UserBadges.Where<UserBadge>((Func<UserBadge, bool>) (x => x.BadgeId == badge.Id)).ToList<UserBadge>();
        dataSet = (DataSet) null;
        foreach (UserBadge item in lst)
        {
          item.BadgeId = badge.Id;
          item.Badge = badge;
        }
        badge.UserBadges = lst;
        lst = (List<UserBadge>) null;
      }
    }

    public void GetForBadge(IEnumerable<Badge> badges) => this.GetForBadgeAsync(badges).RunSynchronously();

    public async Task GetForBadgeAsync(IEnumerable<Badge> badges)
    {
      if (badges == null)
        return;
      foreach (Badge badge in badges)
        await this.GetForBadgeAsync(badge);
    }

    public void GetForUser(User user) => this.GetForUserAsync(user).RunSynchronously();

    public async Task GetForUserAsync(User user)
    {
      List<UserBadge> lst;
      if (user == null)
        lst = (List<UserBadge>) null;
      else if (string.IsNullOrWhiteSpace(user.Id))
      {
        lst = (List<UserBadge>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.UserBadges.Where<UserBadge>((Func<UserBadge, bool>) (x => x.UserId == user.Id)).ToList<UserBadge>();
        dataSet = (DataSet) null;
        foreach (UserBadge item in lst)
        {
          item.UserId = user.Id;
          item.User = user;
        }
        user.UserBadges = lst;
        lst = (List<UserBadge>) null;
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
      UserBadge entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(UserBadge entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(UserBadge entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<UserBadge> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<UserBadge> entities)
    {
      if (entities == null)
        throw new DuplicateException("The userBadges are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
