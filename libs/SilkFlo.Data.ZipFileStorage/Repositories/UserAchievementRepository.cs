// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.UserAchievementRepository
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
  public class UserAchievementRepository : IUserAchievementRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public UserAchievementRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public UserAchievement Get(string id) => this.GetAsync(id).Result;

    public async Task<UserAchievement> GetAsync(string id)
    {
      if (id == null)
        return (UserAchievement) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.UserAchievements.SingleOrDefault<UserAchievement>((Func<UserAchievement, bool>) (x => x.Id == id));
    }

    public UserAchievement SingleOrDefault(Func<UserAchievement, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<UserAchievement> SingleOrDefaultAsync(Func<UserAchievement, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.UserAchievements.Where<UserAchievement>(predicate).FirstOrDefault<UserAchievement>();
    }

    public bool Add(UserAchievement entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(UserAchievement entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<UserAchievement> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<UserAchievement> entities)
    {
      if (entities == null)
        return false;
      foreach (UserAchievement entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<UserAchievement> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<UserAchievement>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<UserAchievement>) dataSetAsync.UserAchievements;
    }

    public IEnumerable<UserAchievement> Find(Func<UserAchievement, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<UserAchievement>> FindAsync(Func<UserAchievement, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.UserAchievements.Where<UserAchievement>(predicate);
    }

    public void GetForAchievement(Achievement achievement) => this.GetForAchievementAsync(achievement).RunSynchronously();

    public async Task GetForAchievementAsync(Achievement achievement)
    {
      List<UserAchievement> lst;
      if (achievement == null)
        lst = (List<UserAchievement>) null;
      else if (string.IsNullOrWhiteSpace(achievement.Id))
      {
        lst = (List<UserAchievement>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.UserAchievements.Where<UserAchievement>((Func<UserAchievement, bool>) (x => x.AchievementId == achievement.Id)).ToList<UserAchievement>();
        dataSet = (DataSet) null;
        foreach (UserAchievement item in lst)
        {
          item.AchievementId = achievement.Id;
          item.Achievement = achievement;
        }
        achievement.UserAchievements = lst;
        lst = (List<UserAchievement>) null;
      }
    }

    public void GetForAchievement(IEnumerable<Achievement> achievements) => this.GetForAchievementAsync(achievements).RunSynchronously();

    public async Task GetForAchievementAsync(IEnumerable<Achievement> achievements)
    {
      if (achievements == null)
        return;
      foreach (Achievement achievement in achievements)
        await this.GetForAchievementAsync(achievement);
    }

    public void GetForUser(User user) => this.GetForUserAsync(user).RunSynchronously();

    public async Task GetForUserAsync(User user)
    {
      List<UserAchievement> lst;
      if (user == null)
        lst = (List<UserAchievement>) null;
      else if (string.IsNullOrWhiteSpace(user.Id))
      {
        lst = (List<UserAchievement>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.UserAchievements.Where<UserAchievement>((Func<UserAchievement, bool>) (x => x.UserId == user.Id)).ToList<UserAchievement>();
        dataSet = (DataSet) null;
        foreach (UserAchievement item in lst)
        {
          item.UserId = user.Id;
          item.User = user;
        }
        user.UserAchievements = lst;
        lst = (List<UserAchievement>) null;
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
      UserAchievement entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(UserAchievement entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(UserAchievement entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<UserAchievement> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<UserAchievement> entities)
    {
      if (entities == null)
        throw new DuplicateException("The userAchievements are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
