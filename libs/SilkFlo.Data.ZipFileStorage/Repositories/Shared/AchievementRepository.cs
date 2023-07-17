// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.AchievementRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Shared
{
  public class AchievementRepository : IAchievementRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public AchievementRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Achievement Get(string id) => this.GetAsync(id).Result;

    public async Task<Achievement> GetAsync(string id)
    {
      if (id == null)
        return (Achievement) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAchievements.SingleOrDefault<Achievement>((Func<Achievement, bool>) (x => x.Id == id));
    }

    public Achievement SingleOrDefault(Func<Achievement, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Achievement> SingleOrDefaultAsync(Func<Achievement, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAchievements.Where<Achievement>(predicate).FirstOrDefault<Achievement>();
    }

    public bool Add(Achievement entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Achievement entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Achievement> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Achievement> entities)
    {
      if (entities == null)
        return false;
      foreach (Achievement entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Achievement> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Achievement>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Achievement>) dataSetAsync.SharedAchievements.OrderBy<Achievement, int>((Func<Achievement, int>) (m => m.Sort)).ThenBy<Achievement, string>((Func<Achievement, string>) (m => m.Name));
    }

    public IEnumerable<Achievement> Find(Func<Achievement, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Achievement>> FindAsync(Func<Achievement, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Achievement>) dataSetAsync.SharedAchievements.Where<Achievement>(predicate).OrderBy<Achievement, int>((Func<Achievement, int>) (m => m.Sort)).ThenBy<Achievement, string>((Func<Achievement, string>) (m => m.Name));
    }

    public Achievement GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Achievement> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Achievement) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAchievements.SingleOrDefault<Achievement>((Func<Achievement, bool>) (x => x.Name == name));
    }

    public void GetAchievementFor(IEnumerable<UserAchievement> userAchievements) => this.GetAchievementForAsync(userAchievements).RunSynchronously();

    public async Task GetAchievementForAsync(IEnumerable<UserAchievement> userAchievements)
    {
      if (userAchievements == null)
        return;
      foreach (UserAchievement userAchievement in userAchievements)
        await this.GetAchievementForAsync(userAchievement);
    }

    public void GetAchievementFor(UserAchievement userAchievement) => this.GetAchievementForAsync(userAchievement).RunSynchronously();

    public async Task GetAchievementForAsync(UserAchievement userAchievement)
    {
      if (userAchievement == null)
        ;
      else
      {
        UserAchievement userAchievement1 = userAchievement;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        userAchievement1.Achievement = dataSet.SharedAchievements.SingleOrDefault<Achievement>((Func<Achievement, bool>) (x => x.Id == userAchievement.AchievementId));
        userAchievement1 = (UserAchievement) null;
        dataSet = (DataSet) null;
      }
    }

    public Achievement GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Achievement> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Achievement) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedAchievements.SingleOrDefault<Achievement>((Func<Achievement, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Achievement entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Achievement entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Achievement entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Achievement> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Achievement> entities)
    {
      if (entities == null)
        throw new DuplicateException("The achievements are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
