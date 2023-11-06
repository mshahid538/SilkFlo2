// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.LocationRepository
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
  public class LocationRepository : ILocationRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public LocationRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Location Get(string id) => this.GetAsync(id).Result;

    public async Task<Location> GetAsync(string id)
    {
      if (id == null)
        return (Location) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessLocations.SingleOrDefault<Location>((Func<Location, bool>) (x => x.Id == id));
    }

    public Location SingleOrDefault(Func<Location, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Location> SingleOrDefaultAsync(Func<Location, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessLocations.Where<Location>(predicate).FirstOrDefault<Location>();
    }

    public bool Add(Location entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Location entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Location> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Location> entities)
    {
      if (entities == null)
        return false;
      foreach (Location entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Location> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Location>) dataSetAsync.BusinessLocations.OrderBy<Location, string>((Func<Location, string>) (m => m.Name));
    }

    public IEnumerable<Location> Find(Func<Location, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Location>> FindAsync(Func<Location, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Location>) dataSetAsync.BusinessLocations.Where<Location>(predicate).OrderBy<Location, string>((Func<Location, string>) (m => m.Name));
    }

    public Location GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Location> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Location) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessLocations.SingleOrDefault<Location>((Func<Location, bool>) (x => x.Name == name));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<Location> lst;
      if (client == null)
        lst = (List<Location>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<Location>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessLocations.Where<Location>((Func<Location, bool>) (x => x.ClientId == client.Id)).OrderBy<Location, string>((Func<Location, string>) (x => x.Name)).ToList<Location>();
        //dataSet = (DataSet) null;
        foreach (Location item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Locations = lst;
        lst = (List<Location>) null;
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

    public void GetLocationFor(IEnumerable<User> users) => this.GetLocationForAsync(users).RunSynchronously();

    public async Task GetLocationForAsync(IEnumerable<User> users)
    {
      if (users == null)
        return;
      foreach (User user in users)
        await this.GetLocationForAsync(user);
    }

    public void GetLocationFor(User user) => this.GetLocationForAsync(user).RunSynchronously();

    public async Task GetLocationForAsync(User user)
    {
      if (user == null)
        ;
      else
      {
        User user1 = user;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        user1.Location = dataSet.BusinessLocations.SingleOrDefault<Location>((Func<Location, bool>) (x => x.Id == user.LocationId));
        user1 = (User) null;
        //dataSet = (DataSet) null;
      }
    }

    public Location GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Location> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Location) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessLocations.SingleOrDefault<Location>((Func<Location, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Location entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Location entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Location entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Location> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Location> entities)
    {
      if (entities == null)
        throw new DuplicateException("The locations are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
