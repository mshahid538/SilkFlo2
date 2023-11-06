// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.VersionRepository
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
  public class VersionRepository : IVersionRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public VersionRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public SilkFlo.Data.Core.Domain.Business.Version Get(string id) => this.GetAsync(id).Result;

    public async Task<SilkFlo.Data.Core.Domain.Business.Version> GetAsync(string id)
    {
      if (id == null)
        return (SilkFlo.Data.Core.Domain.Business.Version) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessVersions.SingleOrDefault<SilkFlo.Data.Core.Domain.Business.Version>((Func<SilkFlo.Data.Core.Domain.Business.Version, bool>) (x => x.Id == id));
    }

    public SilkFlo.Data.Core.Domain.Business.Version SingleOrDefault(Func<SilkFlo.Data.Core.Domain.Business.Version, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<SilkFlo.Data.Core.Domain.Business.Version> SingleOrDefaultAsync(
      Func<SilkFlo.Data.Core.Domain.Business.Version, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessVersions.Where<SilkFlo.Data.Core.Domain.Business.Version>(predicate).FirstOrDefault<SilkFlo.Data.Core.Domain.Business.Version>();
    }

    public bool Add(SilkFlo.Data.Core.Domain.Business.Version entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(SilkFlo.Data.Core.Domain.Business.Version entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> entities)
    {
      if (entities == null)
        return false;
      foreach (SilkFlo.Data.Core.Domain.Business.Version entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<SilkFlo.Data.Core.Domain.Business.Version>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<SilkFlo.Data.Core.Domain.Business.Version>) dataSetAsync.BusinessVersions.OrderByDescending<SilkFlo.Data.Core.Domain.Business.Version, string>((Func<SilkFlo.Data.Core.Domain.Business.Version, string>) (m => m.Name)).ThenByDescending<SilkFlo.Data.Core.Domain.Business.Version, string>((Func<SilkFlo.Data.Core.Domain.Business.Version, string>) (m => m.ApplicationId));
    }

    public IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> Find(Func<SilkFlo.Data.Core.Domain.Business.Version, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<SilkFlo.Data.Core.Domain.Business.Version>> FindAsync(
      Func<SilkFlo.Data.Core.Domain.Business.Version, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<SilkFlo.Data.Core.Domain.Business.Version>) dataSetAsync.BusinessVersions.Where<SilkFlo.Data.Core.Domain.Business.Version>(predicate).OrderByDescending<SilkFlo.Data.Core.Domain.Business.Version, string>((Func<SilkFlo.Data.Core.Domain.Business.Version, string>) (m => m.Name)).ThenByDescending<SilkFlo.Data.Core.Domain.Business.Version, string>((Func<SilkFlo.Data.Core.Domain.Business.Version, string>) (m => m.ApplicationId));
    }

    public SilkFlo.Data.Core.Domain.Business.Version GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<SilkFlo.Data.Core.Domain.Business.Version> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (SilkFlo.Data.Core.Domain.Business.Version) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessVersions.SingleOrDefault<SilkFlo.Data.Core.Domain.Business.Version>((Func<SilkFlo.Data.Core.Domain.Business.Version, bool>) (x => x.Name == name));
    }

    public void GetForApplication(SilkFlo.Data.Core.Domain.Business.Application application) => this.GetForApplicationAsync(application).RunSynchronously();

    public async Task GetForApplicationAsync(SilkFlo.Data.Core.Domain.Business.Application application)
    {
      List<SilkFlo.Data.Core.Domain.Business.Version> lst;
      if (application == null)
        lst = (List<SilkFlo.Data.Core.Domain.Business.Version>) null;
      else if (string.IsNullOrWhiteSpace(application.Id))
      {
        lst = (List<SilkFlo.Data.Core.Domain.Business.Version>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessVersions.Where<SilkFlo.Data.Core.Domain.Business.Version>((Func<SilkFlo.Data.Core.Domain.Business.Version, bool>) (x => x.ApplicationId == application.Id)).OrderByDescending<SilkFlo.Data.Core.Domain.Business.Version, string>((Func<SilkFlo.Data.Core.Domain.Business.Version, string>) (x => x.Name)).ThenByDescending<SilkFlo.Data.Core.Domain.Business.Version, string>((Func<SilkFlo.Data.Core.Domain.Business.Version, string>) (x => x.ApplicationId)).ToList<SilkFlo.Data.Core.Domain.Business.Version>();
        //dataSet = (DataSet) null;
        foreach (SilkFlo.Data.Core.Domain.Business.Version item in lst)
        {
          item.ApplicationId = application.Id;
          item.Application = application;
        }
        application.Versions = lst;
        lst = (List<SilkFlo.Data.Core.Domain.Business.Version>) null;
      }
    }

    public void GetForApplication(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> applications) => this.GetForApplicationAsync(applications).RunSynchronously();

    public async Task GetForApplicationAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> applications)
    {
      if (applications == null)
        return;
      foreach (SilkFlo.Data.Core.Domain.Business.Application application in applications)
        await this.GetForApplicationAsync(application);
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<SilkFlo.Data.Core.Domain.Business.Version> lst;
      if (client == null)
        lst = (List<SilkFlo.Data.Core.Domain.Business.Version>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<SilkFlo.Data.Core.Domain.Business.Version>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessVersions.Where<SilkFlo.Data.Core.Domain.Business.Version>((Func<SilkFlo.Data.Core.Domain.Business.Version, bool>) (x => x.ClientId == client.Id)).OrderByDescending<SilkFlo.Data.Core.Domain.Business.Version, string>((Func<SilkFlo.Data.Core.Domain.Business.Version, string>) (x => x.Name)).ThenByDescending<SilkFlo.Data.Core.Domain.Business.Version, string>((Func<SilkFlo.Data.Core.Domain.Business.Version, string>) (x => x.ApplicationId)).ToList<SilkFlo.Data.Core.Domain.Business.Version>();
        //dataSet = (DataSet) null;
        foreach (SilkFlo.Data.Core.Domain.Business.Version item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Versions = lst;
        lst = (List<SilkFlo.Data.Core.Domain.Business.Version>) null;
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

    public void GetVersionFor(
      IEnumerable<IdeaApplicationVersion> ideaApplicationVersions)
    {
      this.GetVersionForAsync(ideaApplicationVersions).RunSynchronously();
    }

    public async Task GetVersionForAsync(
      IEnumerable<IdeaApplicationVersion> ideaApplicationVersions)
    {
      if (ideaApplicationVersions == null)
        return;
      foreach (IdeaApplicationVersion ideaApplicationVersion in ideaApplicationVersions)
        await this.GetVersionForAsync(ideaApplicationVersion);
    }

    public void GetVersionFor(IdeaApplicationVersion ideaApplicationVersion) => this.GetVersionForAsync(ideaApplicationVersion).RunSynchronously();

    public async Task GetVersionForAsync(IdeaApplicationVersion ideaApplicationVersion)
    {
      if (ideaApplicationVersion == null)
        ;
      else
      {
        IdeaApplicationVersion applicationVersion = ideaApplicationVersion;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        applicationVersion.Version = dataSet.BusinessVersions.SingleOrDefault<SilkFlo.Data.Core.Domain.Business.Version>((Func<SilkFlo.Data.Core.Domain.Business.Version, bool>) (x => x.Id == ideaApplicationVersion.VersionId));
        applicationVersion = (IdeaApplicationVersion) null;
        //dataSet = (DataSet) null;
      }
    }

    public SilkFlo.Data.Core.Domain.Business.Version GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<SilkFlo.Data.Core.Domain.Business.Version> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (SilkFlo.Data.Core.Domain.Business.Version) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessVersions.SingleOrDefault<SilkFlo.Data.Core.Domain.Business.Version>((Func<SilkFlo.Data.Core.Domain.Business.Version, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      SilkFlo.Data.Core.Domain.Business.Version entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(SilkFlo.Data.Core.Domain.Business.Version entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(SilkFlo.Data.Core.Domain.Business.Version entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> entities)
    {
      if (entities == null)
        throw new DuplicateException("The versions are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
