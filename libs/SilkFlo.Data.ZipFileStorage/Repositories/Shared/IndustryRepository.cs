// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.IndustryRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Shared
{
  public class IndustryRepository : IIndustryRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public IndustryRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Industry Get(string id) => this.GetAsync(id).Result;

    public async Task<Industry> GetAsync(string id)
    {
      if (id == null)
        return (Industry) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedIndustries.SingleOrDefault<Industry>((Func<Industry, bool>) (x => x.Id == id));
    }

    public Industry SingleOrDefault(Func<Industry, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Industry> SingleOrDefaultAsync(Func<Industry, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedIndustries.Where<Industry>(predicate).FirstOrDefault<Industry>();
    }

    public bool Add(Industry entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Industry entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Industry> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Industry> entities)
    {
      if (entities == null)
        return false;
      foreach (Industry entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Industry> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Industry>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Industry>) dataSetAsync.SharedIndustries.OrderBy<Industry, int>((Func<Industry, int>) (m => m.Sort)).ThenBy<Industry, string>((Func<Industry, string>) (m => m.Name));
    }

    public IEnumerable<Industry> Find(Func<Industry, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Industry>> FindAsync(Func<Industry, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Industry>) dataSetAsync.SharedIndustries.Where<Industry>(predicate).OrderBy<Industry, int>((Func<Industry, int>) (m => m.Sort)).ThenBy<Industry, string>((Func<Industry, string>) (m => m.Name));
    }

    public Industry GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Industry> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Industry) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedIndustries.SingleOrDefault<Industry>((Func<Industry, bool>) (x => x.Name == name));
    }

    public void GetIndustryFor(IEnumerable<Client> clients) => this.GetIndustryForAsync(clients).RunSynchronously();

    public async Task GetIndustryForAsync(IEnumerable<Client> clients)
    {
      if (clients == null)
        return;
      foreach (Client client in clients)
        await this.GetIndustryForAsync(client);
    }

    public void GetIndustryFor(Client client) => this.GetIndustryForAsync(client).RunSynchronously();

    public async Task GetIndustryForAsync(Client client)
    {
      if (client == null)
        ;
      else
      {
        Client client1 = client;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        client1.Industry = dataSet.SharedIndustries.SingleOrDefault<Industry>((Func<Industry, bool>) (x => x.Id == client.IndustryId));
        client1 = (Client) null;
        //dataSet = (DataSet) null;
      }
    }

    public Industry GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Industry> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Industry) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedIndustries.SingleOrDefault<Industry>((Func<Industry, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Industry entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Industry entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Industry entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Industry> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Industry> entities)
    {
      if (entities == null)
        throw new DuplicateException("The industries are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
