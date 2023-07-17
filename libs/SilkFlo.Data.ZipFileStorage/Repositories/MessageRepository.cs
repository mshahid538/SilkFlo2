// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.MessageRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories
{
  public class MessageRepository : IMessageRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public MessageRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Message Get(string id) => this.GetAsync(id).Result;

    public async Task<Message> GetAsync(string id)
    {
      if (id == null)
        return (Message) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.Messages.SingleOrDefault<Message>((Func<Message, bool>) (x => x.Id == id));
    }

    public Message SingleOrDefault(Func<Message, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Message> SingleOrDefaultAsync(Func<Message, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.Messages.Where<Message>(predicate).FirstOrDefault<Message>();
    }

    public bool Add(Message entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Message entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Message> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Message> entities)
    {
      if (entities == null)
        return false;
      foreach (Message entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Message> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Message>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Message>) dataSetAsync.Messages;
    }

    public IEnumerable<Message> Find(Func<Message, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Message>> FindAsync(Func<Message, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.Messages.Where<Message>(predicate);
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<Message> lst;
      if (client == null)
        lst = (List<Message>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<Message>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.Messages.Where<Message>((Func<Message, bool>) (x => x.ClientId == client.Id)).ToList<Message>();
        dataSet = (DataSet) null;
        foreach (Message item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Messages = lst;
        lst = (List<Message>) null;
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

    public void GetForUser(User user) => this.GetForUserAsync(user).RunSynchronously();

    public async Task GetForUserAsync(User user)
    {
      List<Message> lst;
      if (user == null)
        lst = (List<Message>) null;
      else if (string.IsNullOrWhiteSpace(user.Id))
      {
        lst = (List<Message>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.Messages.Where<Message>((Func<Message, bool>) (x => x.UserId == user.Id)).ToList<Message>();
        dataSet = (DataSet) null;
        foreach (Message item in lst)
        {
          item.UserId = user.Id;
          item.User = user;
        }
        user.Messages = lst;
        lst = (List<Message>) null;
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
      Message entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Message entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Message entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Message> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Message> entities)
    {
      if (entities == null)
        throw new DuplicateException("The messages are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
