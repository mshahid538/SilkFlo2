// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.CountryRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Shared
{
  public class CountryRepository : ICountryRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public CountryRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Country Get(string id) => this.GetAsync(id).Result;

    public async Task<Country> GetAsync(string id)
    {
      if (id == null)
        return (Country) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedCountries.SingleOrDefault<Country>((Func<Country, bool>) (x => x.Id == id));
    }

    public Country SingleOrDefault(Func<Country, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Country> SingleOrDefaultAsync(Func<Country, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedCountries.Where<Country>(predicate).FirstOrDefault<Country>();
    }

    public bool Add(Country entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Country entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Country> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Country> entities)
    {
      if (entities == null)
        return false;
      foreach (Country entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Country> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Country>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Country>) dataSetAsync.SharedCountries.OrderBy<Country, int>((Func<Country, int>) (m => m.Sort)).ThenBy<Country, string>((Func<Country, string>) (m => m.Name));
    }

    public IEnumerable<Country> Find(Func<Country, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Country>> FindAsync(Func<Country, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Country>) dataSetAsync.SharedCountries.Where<Country>(predicate).OrderBy<Country, int>((Func<Country, int>) (m => m.Sort)).ThenBy<Country, string>((Func<Country, string>) (m => m.Name));
    }

    public Country GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Country> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Country) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedCountries.SingleOrDefault<Country>((Func<Country, bool>) (x => x.Name == name));
    }

    public void GetCountryFor(IEnumerable<Client> clients) => this.GetCountryForAsync(clients).RunSynchronously();

    public async Task GetCountryForAsync(IEnumerable<Client> clients)
    {
      if (clients == null)
        return;
      foreach (Client client in clients)
        await this.GetCountryForAsync(client);
    }

    public void GetCountryFor(Client client) => this.GetCountryForAsync(client).RunSynchronously();

    public async Task GetCountryForAsync(Client client)
    {
      if (client == null)
        ;
      else
      {
        Client client1 = client;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        client1.Country = dataSet.SharedCountries.SingleOrDefault<Country>((Func<Country, bool>) (x => x.Id == client.CountryId));
        client1 = (Client) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetCountryFor(IEnumerable<Prospect> prospects) => this.GetCountryForAsync(prospects).RunSynchronously();

    public async Task GetCountryForAsync(IEnumerable<Prospect> prospects)
    {
      if (prospects == null)
        return;
      foreach (Prospect prospect in prospects)
        await this.GetCountryForAsync(prospect);
    }

    public void GetCountryFor(Prospect prospect) => this.GetCountryForAsync(prospect).RunSynchronously();

    public async Task GetCountryForAsync(Prospect prospect)
    {
      if (prospect == null)
        ;
      else
      {
        Prospect prospect1 = prospect;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        prospect1.Country = dataSet.SharedCountries.SingleOrDefault<Country>((Func<Country, bool>) (x => x.Id == prospect.CountryId));
        prospect1 = (Prospect) null;
        //dataSet = (DataSet) null;
      }
    }

    public Country GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Country> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Country) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedCountries.SingleOrDefault<Country>((Func<Country, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Country entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Country entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Country entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Country> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Country> entities)
    {
      if (entities == null)
        throw new DuplicateException("The countries are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
