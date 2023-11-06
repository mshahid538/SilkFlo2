// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.CommentRepository
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
  public class CommentRepository : ICommentRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public CommentRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Comment Get(string id) => this.GetAsync(id).Result;

    public async Task<Comment> GetAsync(string id)
    {
      if (id == null)
        return (Comment) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessComments.SingleOrDefault<Comment>((Func<Comment, bool>) (x => x.Id == id));
    }

    public Comment SingleOrDefault(Func<Comment, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Comment> SingleOrDefaultAsync(Func<Comment, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessComments.Where<Comment>(predicate).FirstOrDefault<Comment>();
    }

    public bool Add(Comment entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Comment entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Comment> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Comment> entities)
    {
      if (entities == null)
        return false;
      foreach (Comment entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Comment> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Comment>) dataSetAsync.BusinessComments.OrderBy<Comment, string>((Func<Comment, string>) (m => m.Text));
    }

    public IEnumerable<Comment> Find(Func<Comment, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Comment>> FindAsync(Func<Comment, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Comment>) dataSetAsync.BusinessComments.Where<Comment>(predicate).OrderBy<Comment, string>((Func<Comment, string>) (m => m.Text));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<Comment> lst;
      if (client == null)
        lst = (List<Comment>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<Comment>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessComments.Where<Comment>((Func<Comment, bool>) (x => x.ClientId == client.Id)).OrderBy<Comment, string>((Func<Comment, string>) (x => x.Text)).ToList<Comment>();
        //dataSet = (DataSet) null;
        foreach (Comment item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Comments = lst;
        lst = (List<Comment>) null;
      }
    }

    public void GetForClient(IEnumerable<Client> clients) => this.GetForClientAsync(clients).RunSynchronously();

    public async Task GetForClientAsync(IEnumerable<Client> clients)
    {
      if (clients == null)
        return;
      foreach (Client client in clients)
        await this.GetForClientAsync(client);
    }

    public void GetForIdea(Idea idea) => this.GetForIdeaAsync(idea).RunSynchronously();

    public async Task GetForIdeaAsync(Idea idea)
    {
      List<Comment> lst;
      if (idea == null)
        lst = (List<Comment>) null;
      else if (string.IsNullOrWhiteSpace(idea.Id))
      {
        lst = (List<Comment>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessComments.Where<Comment>((Func<Comment, bool>) (x => x.IdeaId == idea.Id)).OrderBy<Comment, string>((Func<Comment, string>) (x => x.Text)).ToList<Comment>();
        //dataSet = (DataSet) null;
        foreach (Comment item in lst)
        {
          item.IdeaId = idea.Id;
          item.Idea = idea;
        }
        idea.Comments = lst;
        lst = (List<Comment>) null;
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

    public void GetForSender(User sender) => this.GetForSenderAsync(sender).RunSynchronously();

    public async Task GetForSenderAsync(User sender)
    {
      List<Comment> lst;
      if (sender == null)
        lst = (List<Comment>) null;
      else if (string.IsNullOrWhiteSpace(sender.Id))
      {
        lst = (List<Comment>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessComments.Where<Comment>((Func<Comment, bool>) (x => x.SenderId == sender.Id)).OrderBy<Comment, string>((Func<Comment, string>) (x => x.Text)).ToList<Comment>();
        //dataSet = (DataSet) null;
        foreach (Comment item in lst)
        {
          item.SenderId = sender.Id;
          item.Sender = sender;
        }
        sender.CommentsSend = lst;
        lst = (List<Comment>) null;
      }
    }

    public void GetForSender(IEnumerable<User> senders) => this.GetForSenderAsync(senders).RunSynchronously();

    public async Task GetForSenderAsync(IEnumerable<User> senders)
    {
      if (senders == null)
        return;
      foreach (User sender in senders)
        await this.GetForSenderAsync(sender);
    }

    public void GetCommentFor(IEnumerable<Recipient> recipients) => this.GetCommentForAsync(recipients).RunSynchronously();

    public async Task GetCommentForAsync(IEnumerable<Recipient> recipients)
    {
      if (recipients == null)
        return;
      foreach (Recipient recipient in recipients)
        await this.GetCommentForAsync(recipient);
    }

    public void GetCommentFor(Recipient recipient) => this.GetCommentForAsync(recipient).RunSynchronously();

    public async Task GetCommentForAsync(Recipient recipient)
    {
      if (recipient == null)
        ;
      else
      {
        Recipient recipient1 = recipient;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        recipient1.Comment = dataSet.BusinessComments.SingleOrDefault<Comment>((Func<Comment, bool>) (x => x.Id == recipient.CommentId));
        recipient1 = (Recipient) null;
        //dataSet = (DataSet) null;
      }
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Comment entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Comment entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Comment entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Comment> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Comment> entities)
    {
      if (entities == null)
        throw new DuplicateException("The comments are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
