// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.BadgeRepository
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
  public class BadgeRepository : IBadgeRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public BadgeRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Badge Get(string id) => this.GetAsync(id).Result;

    public async Task<Badge> GetAsync(string id)
    {
      if (id == null)
        return (Badge) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedBadges.SingleOrDefault<Badge>((Func<Badge, bool>) (x => x.Id == id));
    }

    public Badge SingleOrDefault(Func<Badge, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Badge> SingleOrDefaultAsync(Func<Badge, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedBadges.Where<Badge>(predicate).FirstOrDefault<Badge>();
    }

    public bool Add(Badge entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Badge entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Badge> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Badge> entities)
    {
      if (entities == null)
        return false;
      foreach (Badge entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Badge> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Badge>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Badge>) dataSetAsync.SharedBadges.OrderBy<Badge, int>((Func<Badge, int>) (m => m.Sort)).ThenBy<Badge, string>((Func<Badge, string>) (m => m.Name));
    }

    public IEnumerable<Badge> Find(Func<Badge, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Badge>> FindAsync(Func<Badge, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Badge>) dataSetAsync.SharedBadges.Where<Badge>(predicate).OrderBy<Badge, int>((Func<Badge, int>) (m => m.Sort)).ThenBy<Badge, string>((Func<Badge, string>) (m => m.Name));
    }

    public Badge GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Badge> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Badge) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedBadges.SingleOrDefault<Badge>((Func<Badge, bool>) (x => x.Name == name));
    }

    public void GetBadgeFor(IEnumerable<UserBadge> userBadges) => this.GetBadgeForAsync(userBadges).RunSynchronously();

    public async Task GetBadgeForAsync(IEnumerable<UserBadge> userBadges)
    {
      if (userBadges == null)
        return;
      foreach (UserBadge userBadge in userBadges)
        await this.GetBadgeForAsync(userBadge);
    }

    public void GetBadgeFor(UserBadge userBadge) => this.GetBadgeForAsync(userBadge).RunSynchronously();

    public async Task GetBadgeForAsync(UserBadge userBadge)
    {
      if (userBadge == null)
        ;
      else
      {
        UserBadge userBadge1 = userBadge;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        userBadge1.Badge = dataSet.SharedBadges.SingleOrDefault<Badge>((Func<Badge, bool>) (x => x.Id == userBadge.BadgeId));
        userBadge1 = (UserBadge) null;
        //dataSet = (DataSet) null;
      }
    }

    public Badge GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Badge> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Badge) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedBadges.SingleOrDefault<Badge>((Func<Badge, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Badge entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Badge entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Badge entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Badge> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Badge> entities)
    {
      if (entities == null)
        throw new DuplicateException("The badges are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
