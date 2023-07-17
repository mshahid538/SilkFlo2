// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.VoteRepository
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
  public class VoteRepository : IVoteRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public VoteRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Vote Get(string id) => this.GetAsync(id).Result;

    public async Task<Vote> GetAsync(string id)
    {
      if (id == null)
        return (Vote) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessVotes.SingleOrDefault<Vote>((Func<Vote, bool>) (x => x.Id == id));
    }

    public Vote SingleOrDefault(Func<Vote, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Vote> SingleOrDefaultAsync(Func<Vote, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessVotes.Where<Vote>(predicate).FirstOrDefault<Vote>();
    }

    public bool Add(Vote entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Vote entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Vote> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Vote> entities)
    {
      if (entities == null)
        return false;
      foreach (Vote entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Vote> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Vote>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Vote>) dataSetAsync.BusinessVotes;
    }

    public IEnumerable<Vote> Find(Func<Vote, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Vote>> FindAsync(Func<Vote, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessVotes.Where<Vote>(predicate);
    }

    public void GetForIdea(Idea idea) => this.GetForIdeaAsync(idea).RunSynchronously();

    public async Task GetForIdeaAsync(Idea idea)
    {
      List<Vote> lst;
      if (idea == null)
        lst = (List<Vote>) null;
      else if (string.IsNullOrWhiteSpace(idea.Id))
      {
        lst = (List<Vote>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessVotes.Where<Vote>((Func<Vote, bool>) (x => x.IdeaId == idea.Id)).ToList<Vote>();
        dataSet = (DataSet) null;
        foreach (Vote item in lst)
        {
          item.IdeaId = idea.Id;
          item.Idea = idea;
        }
        idea.Votes = lst;
        lst = (List<Vote>) null;
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
      List<Vote> lst;
      if (user == null)
        lst = (List<Vote>) null;
      else if (string.IsNullOrWhiteSpace(user.Id))
      {
        lst = (List<Vote>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessVotes.Where<Vote>((Func<Vote, bool>) (x => x.UserId == user.Id)).ToList<Vote>();
        dataSet = (DataSet) null;
        foreach (Vote item in lst)
        {
          item.UserId = user.Id;
          item.User = user;
        }
        user.Votes = lst;
        lst = (List<Vote>) null;
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
      Vote entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Vote entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Vote entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Vote> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Vote> entities)
    {
      if (entities == null)
        throw new DuplicateException("The votes are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
