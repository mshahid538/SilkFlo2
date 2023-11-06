// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.SoftwareVenderRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Repositories.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class SoftwareVenderRepository : ISoftwareVenderRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public SoftwareVenderRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public SoftwareVender Get(string id) => this.GetAsync(id).Result;

    public async Task<SoftwareVender> GetAsync(string id)
    {
      if (id == null)
        return (SoftwareVender) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessSoftwareVenders.SingleOrDefault<SoftwareVender>((Func<SoftwareVender, bool>) (x => x.Id == id));
    }

    public SoftwareVender SingleOrDefault(Func<SoftwareVender, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<SoftwareVender> SingleOrDefaultAsync(Func<SoftwareVender, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessSoftwareVenders.Where<SoftwareVender>(predicate).FirstOrDefault<SoftwareVender>();
    }

    public bool Add(SoftwareVender entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(SoftwareVender entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<SoftwareVender> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<SoftwareVender> entities)
    {
      if (entities == null)
        return false;
      foreach (SoftwareVender entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<SoftwareVender> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<SoftwareVender>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<SoftwareVender>) dataSetAsync.BusinessSoftwareVenders;
    }

    public IEnumerable<SoftwareVender> Find(Func<SoftwareVender, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<SoftwareVender>> FindAsync(Func<SoftwareVender, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessSoftwareVenders.Where<SoftwareVender>(predicate);
    }

    public SoftwareVender GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<SoftwareVender> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (SoftwareVender) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessSoftwareVenders.SingleOrDefault<SoftwareVender>((Func<SoftwareVender, bool>) (x => x.Name == name));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<SoftwareVender> lst;
      if (client == null)
        lst = (List<SoftwareVender>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<SoftwareVender>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessSoftwareVenders.Where<SoftwareVender>((Func<SoftwareVender, bool>) (x => x.ClientId == client.Id)).ToList<SoftwareVender>();
        //dataSet = (DataSet) null;
        foreach (SoftwareVender item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.SoftwareVenders = lst;
        lst = (List<SoftwareVender>) null;
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

    public void GetVenderFor(IEnumerable<RunningCost> runningCosts) => this.GetVenderForAsync(runningCosts).RunSynchronously();

    public async Task GetVenderForAsync(IEnumerable<RunningCost> runningCosts)
    {
      if (runningCosts == null)
        return;
      foreach (RunningCost runningCost in runningCosts)
        await this.GetVenderForAsync(runningCost);
    }

    public void GetVenderFor(RunningCost runningCost) => this.GetVenderForAsync(runningCost).RunSynchronously();

    public async Task GetVenderForAsync(RunningCost runningCost)
    {
      if (runningCost == null)
        ;
      else
      {
        RunningCost runningCost1 = runningCost;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        runningCost1.Vender = dataSet.BusinessSoftwareVenders.SingleOrDefault<SoftwareVender>((Func<SoftwareVender, bool>) (x => x.Id == runningCost.VenderId));
        runningCost1 = (RunningCost) null;
        //dataSet = (DataSet) null;
      }
    }

    public SoftwareVender GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<SoftwareVender> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (SoftwareVender) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessSoftwareVenders.SingleOrDefault<SoftwareVender>((Func<SoftwareVender, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      SoftwareVender entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(SoftwareVender entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(SoftwareVender entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<SoftwareVender> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<SoftwareVender> entities)
    {
      if (entities == null)
        throw new DuplicateException("The softwareVenders are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
