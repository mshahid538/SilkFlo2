// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.ApplicationRepository
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
  public class ApplicationRepository : IApplicationRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public ApplicationRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public SilkFlo.Data.Core.Domain.Business.Application Get(string id) => this.GetAsync(id).Result;

    public async Task<SilkFlo.Data.Core.Domain.Business.Application> GetAsync(string id)
    {
      if (id == null)
        return (SilkFlo.Data.Core.Domain.Business.Application) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessApplications.SingleOrDefault<SilkFlo.Data.Core.Domain.Business.Application>((Func<SilkFlo.Data.Core.Domain.Business.Application, bool>) (x => x.Id == id));
    }

    public SilkFlo.Data.Core.Domain.Business.Application SingleOrDefault(Func<SilkFlo.Data.Core.Domain.Business.Application, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<SilkFlo.Data.Core.Domain.Business.Application> SingleOrDefaultAsync(Func<SilkFlo.Data.Core.Domain.Business.Application, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessApplications.Where<SilkFlo.Data.Core.Domain.Business.Application>(predicate).FirstOrDefault<SilkFlo.Data.Core.Domain.Business.Application>();
    }

    public bool Add(SilkFlo.Data.Core.Domain.Business.Application entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(SilkFlo.Data.Core.Domain.Business.Application entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> entities)
    {
      if (entities == null)
        return false;
      foreach (SilkFlo.Data.Core.Domain.Business.Application entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<SilkFlo.Data.Core.Domain.Business.Application>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<SilkFlo.Data.Core.Domain.Business.Application>) dataSetAsync.BusinessApplications.OrderBy<SilkFlo.Data.Core.Domain.Business.Application, string>((Func<SilkFlo.Data.Core.Domain.Business.Application, string>) (m => m.Name));
    }

    public IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> Find(Func<SilkFlo.Data.Core.Domain.Business.Application, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<SilkFlo.Data.Core.Domain.Business.Application>> FindAsync(Func<SilkFlo.Data.Core.Domain.Business.Application, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<SilkFlo.Data.Core.Domain.Business.Application>) dataSetAsync.BusinessApplications.Where<SilkFlo.Data.Core.Domain.Business.Application>(predicate).OrderBy<SilkFlo.Data.Core.Domain.Business.Application, string>((Func<SilkFlo.Data.Core.Domain.Business.Application, string>) (m => m.Name));
    }

    public SilkFlo.Data.Core.Domain.Business.Application GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<SilkFlo.Data.Core.Domain.Business.Application> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (SilkFlo.Data.Core.Domain.Business.Application) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessApplications.SingleOrDefault<SilkFlo.Data.Core.Domain.Business.Application>((Func<SilkFlo.Data.Core.Domain.Business.Application, bool>) (x => x.Name == name));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<SilkFlo.Data.Core.Domain.Business.Application> lst;
      if (client == null)
        lst = (List<SilkFlo.Data.Core.Domain.Business.Application>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<SilkFlo.Data.Core.Domain.Business.Application>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessApplications.Where<SilkFlo.Data.Core.Domain.Business.Application>((Func<SilkFlo.Data.Core.Domain.Business.Application, bool>) (x => x.ClientId == client.Id)).OrderBy<SilkFlo.Data.Core.Domain.Business.Application, string>((Func<SilkFlo.Data.Core.Domain.Business.Application, string>) (x => x.Name)).ToList<SilkFlo.Data.Core.Domain.Business.Application>();
        //dataSet = (DataSet) null;
        foreach (SilkFlo.Data.Core.Domain.Business.Application item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Applications = lst;
        lst = (List<SilkFlo.Data.Core.Domain.Business.Application>) null;
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

    public void GetApplicationFor(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions) => this.GetApplicationForAsync(versions).RunSynchronously();

    public async Task GetApplicationForAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions)
    {
      if (versions == null)
        return;
      foreach (SilkFlo.Data.Core.Domain.Business.Version version in versions)
        await this.GetApplicationForAsync(version);
    }

    public void GetApplicationFor(SilkFlo.Data.Core.Domain.Business.Version version) => this.GetApplicationForAsync(version).RunSynchronously();

    public async Task GetApplicationForAsync(SilkFlo.Data.Core.Domain.Business.Version version)
    {
      if (version == null)
        ;
      else
      {
        SilkFlo.Data.Core.Domain.Business.Version version1 = version;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        version1.Application = dataSet.BusinessApplications.SingleOrDefault<SilkFlo.Data.Core.Domain.Business.Application>((Func<SilkFlo.Data.Core.Domain.Business.Application, bool>) (x => x.Id == version.ApplicationId));
        version1 = (SilkFlo.Data.Core.Domain.Business.Version) null;
        //dataSet = (DataSet) null;
      }
    }

    public SilkFlo.Data.Core.Domain.Business.Application GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<SilkFlo.Data.Core.Domain.Business.Application> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (SilkFlo.Data.Core.Domain.Business.Application) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessApplications.SingleOrDefault<SilkFlo.Data.Core.Domain.Business.Application>((Func<SilkFlo.Data.Core.Domain.Business.Application, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
       SilkFlo.Data.Core.Domain.Business.Application entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(SilkFlo.Data.Core.Domain.Business.Application entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(SilkFlo.Data.Core.Domain.Business.Application entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> entities)
    {
      if (entities == null)
        throw new DuplicateException("The applications are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
