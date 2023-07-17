// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.ClientTypeRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using SilkFlo.Data.Core.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Shared
{
  public class ClientTypeRepository : IClientTypeRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public ClientTypeRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public ClientType Get(string id) => this.GetAsync(id).Result;

    public async Task<ClientType> GetAsync(string id)
    {
      if (id == null)
        return (ClientType) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedClientTypes.SingleOrDefault<ClientType>((Func<ClientType, bool>) (x => x.Id == id));
    }

    public ClientType SingleOrDefault(Func<ClientType, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<ClientType> SingleOrDefaultAsync(Func<ClientType, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedClientTypes.Where<ClientType>(predicate).FirstOrDefault<ClientType>();
    }

    public bool Add(ClientType entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(ClientType entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<ClientType> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<ClientType> entities)
    {
      if (entities == null)
        return false;
      foreach (ClientType entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<ClientType> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<ClientType>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<ClientType>) dataSetAsync.SharedClientTypes.OrderBy<ClientType, string>((Func<ClientType, string>) (m => m.Name));
    }

    public IEnumerable<ClientType> Find(Func<ClientType, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<ClientType>> FindAsync(Func<ClientType, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<ClientType>) dataSetAsync.SharedClientTypes.Where<ClientType>(predicate).OrderBy<ClientType, string>((Func<ClientType, string>) (m => m.Name));
    }

    public ClientType GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<ClientType> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (ClientType) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedClientTypes.SingleOrDefault<ClientType>((Func<ClientType, bool>) (x => x.Name == name));
    }

    public void GetTypeFor(IEnumerable<Client> clients) => this.GetTypeForAsync(clients).RunSynchronously();

    public async Task GetTypeForAsync(IEnumerable<Client> clients)
    {
      if (clients == null)
        return;
      foreach (Client client in clients)
        await this.GetTypeForAsync(client);
    }

    public void GetTypeFor(Client client) => this.GetTypeForAsync(client).RunSynchronously();

    public async Task GetTypeForAsync(Client client)
    {
      if (client == null)
        ;
      else
      {
        Client client1 = client;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        client1.Type = dataSet.SharedClientTypes.SingleOrDefault<ClientType>((Func<ClientType, bool>) (x => x.Id == client.TypeId));
        client1 = (Client) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientTypeFor(IEnumerable<Prospect> prospects) => this.GetClientTypeForAsync(prospects).RunSynchronously();

    public async Task GetClientTypeForAsync(IEnumerable<Prospect> prospects)
    {
      if (prospects == null)
        return;
      foreach (Prospect prospect in prospects)
        await this.GetClientTypeForAsync(prospect);
    }

    public void GetClientTypeFor(Prospect prospect) => this.GetClientTypeForAsync(prospect).RunSynchronously();

    public async Task GetClientTypeForAsync(Prospect prospect)
    {
      if (prospect == null)
        ;
      else
      {
        Prospect prospect1 = prospect;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        prospect1.ClientType = dataSet.SharedClientTypes.SingleOrDefault<ClientType>((Func<ClientType, bool>) (x => x.Id == prospect.ClientTypeId));
        prospect1 = (Prospect) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetAgencyTypeFor(IEnumerable<Subscription> subscriptions) => this.GetAgencyTypeForAsync(subscriptions).RunSynchronously();

    public async Task GetAgencyTypeForAsync(IEnumerable<Subscription> subscriptions)
    {
      if (subscriptions == null)
        return;
      foreach (Subscription subscription in subscriptions)
        await this.GetAgencyTypeForAsync(subscription);
    }

    public void GetAgencyTypeFor(Subscription subscription) => this.GetAgencyTypeForAsync(subscription).RunSynchronously();

    public async Task GetAgencyTypeForAsync(Subscription subscription)
    {
      if (subscription == null)
        ;
      else
      {
        Subscription subscription1 = subscription;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        subscription1.AgencyType = dataSet.SharedClientTypes.SingleOrDefault<ClientType>((Func<ClientType, bool>) (x => x.Id == subscription.AgencyTypeId));
        subscription1 = (Subscription) null;
        dataSet = (DataSet) null;
      }
    }

    public ClientType GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<ClientType> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (ClientType) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedClientTypes.SingleOrDefault<ClientType>((Func<ClientType, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      ClientType entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(ClientType entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(ClientType entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<ClientType> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ClientType> entities)
    {
      if (entities == null)
        throw new DuplicateException("The clientTypes are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
