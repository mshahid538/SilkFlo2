// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.AnalyticRepository
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
  public class AnalyticRepository : IAnalyticRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public AnalyticRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Analytic Get(string id) => this.GetAsync(id).Result;

    public async Task<Analytic> GetAsync(string id)
    {
      if (id == null)
        return (Analytic) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.Analytics.SingleOrDefault<Analytic>((Func<Analytic, bool>) (x => x.Id == id));
    }

    public Analytic SingleOrDefault(Func<Analytic, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Analytic> SingleOrDefaultAsync(Func<Analytic, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.Analytics.Where<Analytic>(predicate).FirstOrDefault<Analytic>();
    }

    public bool Add(Analytic entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Analytic entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Analytic> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Analytic> entities)
    {
      if (entities == null)
        return false;
      foreach (Analytic entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Analytic> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Analytic>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Analytic>) dataSetAsync.Analytics.OrderBy<Analytic, DateTime>((Func<Analytic, DateTime>) (m => m.Date)).ThenBy<Analytic, string>((Func<Analytic, string>) (m => m.UserId));
    }

    public IEnumerable<Analytic> Find(Func<Analytic, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Analytic>> FindAsync(Func<Analytic, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Analytic>) dataSetAsync.Analytics.Where<Analytic>(predicate).OrderBy<Analytic, DateTime>((Func<Analytic, DateTime>) (m => m.Date)).ThenBy<Analytic, string>((Func<Analytic, string>) (m => m.UserId));
    }

    public void GetForUser(User user) => this.GetForUserAsync(user).RunSynchronously();

    public async Task GetForUserAsync(User user)
    {
      List<Analytic> lst;
      if (user == null)
        lst = (List<Analytic>) null;
      else if (string.IsNullOrWhiteSpace(user.Id))
      {
        lst = (List<Analytic>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.Analytics.Where<Analytic>((Func<Analytic, bool>) (x => x.UserId == user.Id)).OrderBy<Analytic, DateTime>((Func<Analytic, DateTime>) (x => x.Date)).ThenBy<Analytic, string>((Func<Analytic, string>) (x => x.UserId)).ToList<Analytic>();
        dataSet = (DataSet) null;
        foreach (Analytic item in lst)
        {
          item.UserId = user.Id;
          item.User = user;
        }
        user.Analytics = lst;
        lst = (List<Analytic>) null;
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
      Analytic entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Analytic entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Analytic entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Analytic> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Analytic> entities)
    {
      if (entities == null)
        throw new DuplicateException("The analytics are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
