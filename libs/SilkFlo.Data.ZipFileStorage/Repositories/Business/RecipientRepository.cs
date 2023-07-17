// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.RecipientRepository
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
  public class RecipientRepository : IRecipientRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public RecipientRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Recipient Get(string id) => this.GetAsync(id).Result;

    public async Task<Recipient> GetAsync(string id)
    {
      if (id == null)
        return (Recipient) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRecipients.SingleOrDefault<Recipient>((Func<Recipient, bool>) (x => x.Id == id));
    }

    public Recipient SingleOrDefault(Func<Recipient, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Recipient> SingleOrDefaultAsync(Func<Recipient, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRecipients.Where<Recipient>(predicate).FirstOrDefault<Recipient>();
    }

    public bool Add(Recipient entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Recipient entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Recipient> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Recipient> entities)
    {
      if (entities == null)
        return false;
      foreach (Recipient entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Recipient> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Recipient>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Recipient>) dataSetAsync.BusinessRecipients;
    }

    public IEnumerable<Recipient> Find(Func<Recipient, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Recipient>> FindAsync(Func<Recipient, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRecipients.Where<Recipient>(predicate);
    }

    public void GetForComment(Comment comment) => this.GetForCommentAsync(comment).RunSynchronously();

    public async Task GetForCommentAsync(Comment comment)
    {
      List<Recipient> lst;
      if (comment == null)
        lst = (List<Recipient>) null;
      else if (string.IsNullOrWhiteSpace(comment.Id))
      {
        lst = (List<Recipient>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessRecipients.Where<Recipient>((Func<Recipient, bool>) (x => x.CommentId == comment.Id)).ToList<Recipient>();
        dataSet = (DataSet) null;
        foreach (Recipient item in lst)
        {
          item.CommentId = comment.Id;
          item.Comment = comment;
        }
        comment.Recipients = lst;
        lst = (List<Recipient>) null;
      }
    }

    public void GetForComment(IEnumerable<Comment> comments) => this.GetForCommentAsync(comments).RunSynchronously();

    public async Task GetForCommentAsync(IEnumerable<Comment> comments)
    {
      if (comments == null)
        return;
      foreach (Comment comment in comments)
        await this.GetForCommentAsync(comment);
    }

    public void GetForUser(User user) => this.GetForUserAsync(user).RunSynchronously();

    public async Task GetForUserAsync(User user)
    {
      List<Recipient> lst;
      if (user == null)
        lst = (List<Recipient>) null;
      else if (string.IsNullOrWhiteSpace(user.Id))
      {
        lst = (List<Recipient>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessRecipients.Where<Recipient>((Func<Recipient, bool>) (x => x.UserId == user.Id)).ToList<Recipient>();
        dataSet = (DataSet) null;
        foreach (Recipient item in lst)
        {
          item.UserId = user.Id;
          item.User = user;
        }
        user.Recipients = lst;
        lst = (List<Recipient>) null;
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
      Recipient entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Recipient entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Recipient entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Recipient> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Recipient> entities)
    {
      if (entities == null)
        throw new DuplicateException("The recipients are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
