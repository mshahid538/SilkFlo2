// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.FollowRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Repositories.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class FollowRepository : IFollowRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public FollowRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Follow Get(string id) => this.GetAsync(id).Result;

    public async Task<Follow> GetAsync(string id)
    {
      if (id == null)
        return (Follow) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessFollows.SingleOrDefault<Follow>((Func<Follow, bool>) (x => x.Id == id));
    }

    public Follow SingleOrDefault(Func<Follow, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Follow> SingleOrDefaultAsync(Func<Follow, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessFollows.Where<Follow>(predicate).FirstOrDefault<Follow>();
    }

    public bool Add(Follow entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Follow entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Follow> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Follow> entities)
    {
      if (entities == null)
        return false;
      foreach (Follow entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Follow> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Follow>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Follow>) dataSetAsync.BusinessFollows;
    }

    public IEnumerable<Follow> Find(Func<Follow, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Follow>> FindAsync(Func<Follow, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessFollows.Where<Follow>(predicate);
    }

    public void GetForIdea(Idea idea) => this.GetForIdeaAsync(idea).RunSynchronously();

    public async Task GetForIdeaAsync(Idea idea)
    {
      List<Follow> lst;
      if (idea == null)
        lst = (List<Follow>) null;
      else if (string.IsNullOrWhiteSpace(idea.Id))
      {
        lst = (List<Follow>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessFollows.Where<Follow>((Func<Follow, bool>) (x => x.IdeaId == idea.Id)).ToList<Follow>();
        dataSet = (DataSet) null;
        foreach (Follow item in lst)
        {
          item.IdeaId = idea.Id;
          item.Idea = idea;
        }
        idea.Follows = lst;
        lst = (List<Follow>) null;
      }
    }

    public void GetForIdea(IEnumerable<Idea> ideas) => this.GetForIdeaAsync(ideas).RunSynchronously();

    public async Task GetForIdeaAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetForIdeaAsync(idea);
    }

    public void GetForUser(User user) => this.GetForUserAsync(user).RunSynchronously();

    public async Task GetForUserAsync(User user)
    {
      List<Follow> lst;
      if (user == null)
        lst = (List<Follow>) null;
      else if (string.IsNullOrWhiteSpace(user.Id))
      {
        lst = (List<Follow>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessFollows.Where<Follow>((Func<Follow, bool>) (x => x.UserId == user.Id)).ToList<Follow>();
        dataSet = (DataSet) null;
        foreach (Follow item in lst)
        {
          item.UserId = user.Id;
          item.User = user;
        }
        user.Follows = lst;
        lst = (List<Follow>) null;
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
      Follow entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Follow entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Follow entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Follow> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Follow> entities)
    {
      if (entities == null)
        throw new DuplicateException("The follows are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
