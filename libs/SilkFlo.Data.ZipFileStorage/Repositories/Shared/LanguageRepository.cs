// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.LanguageRepository
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
  public class LanguageRepository : ILanguageRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public LanguageRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Language Get(string id) => this.GetAsync(id).Result;

    public async Task<Language> GetAsync(string id)
    {
      if (id == null)
        return (Language) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedLanguages.SingleOrDefault<Language>((Func<Language, bool>) (x => x.Id == id));
    }

    public Language SingleOrDefault(Func<Language, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Language> SingleOrDefaultAsync(Func<Language, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedLanguages.Where<Language>(predicate).FirstOrDefault<Language>();
    }

    public bool Add(Language entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Language entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Language> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Language> entities)
    {
      if (entities == null)
        return false;
      foreach (Language entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Language> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Language>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Language>) dataSetAsync.SharedLanguages;
    }

    public IEnumerable<Language> Find(Func<Language, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Language>> FindAsync(Func<Language, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedLanguages.Where<Language>(predicate);
    }

    public Language GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Language> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Language) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedLanguages.SingleOrDefault<Language>((Func<Language, bool>) (x => x.Name == name));
    }

    public void GetLanguageFor(IEnumerable<Client> clients) => this.GetLanguageForAsync(clients).RunSynchronously();

    public async Task GetLanguageForAsync(IEnumerable<Client> clients)
    {
      if (clients == null)
        return;
      foreach (Client client in clients)
        await this.GetLanguageForAsync(client);
    }

    public void GetLanguageFor(Client client) => this.GetLanguageForAsync(client).RunSynchronously();

    public async Task GetLanguageForAsync(Client client)
    {
      if (client == null)
        ;
      else
      {
        Client client1 = client;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        client1.Language = dataSet.SharedLanguages.SingleOrDefault<Language>((Func<Language, bool>) (x => x.Id == client.LanguageId));
        client1 = (Client) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetLanguageFor(
      IEnumerable<IdeaApplicationVersion> ideaApplicationVersions)
    {
      this.GetLanguageForAsync(ideaApplicationVersions).RunSynchronously();
    }

    public async Task GetLanguageForAsync(
      IEnumerable<IdeaApplicationVersion> ideaApplicationVersions)
    {
      if (ideaApplicationVersions == null)
        return;
      foreach (IdeaApplicationVersion ideaApplicationVersion in ideaApplicationVersions)
        await this.GetLanguageForAsync(ideaApplicationVersion);
    }

    public void GetLanguageFor(IdeaApplicationVersion ideaApplicationVersion) => this.GetLanguageForAsync(ideaApplicationVersion).RunSynchronously();

    public async Task GetLanguageForAsync(IdeaApplicationVersion ideaApplicationVersion)
    {
      if (ideaApplicationVersion == null)
        ;
      else
      {
        IdeaApplicationVersion applicationVersion = ideaApplicationVersion;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        applicationVersion.Language = dataSet.SharedLanguages.SingleOrDefault<Language>((Func<Language, bool>) (x => x.Id == ideaApplicationVersion.LanguageId));
        applicationVersion = (IdeaApplicationVersion) null;
        //dataSet = (DataSet) null;
      }
    }

    public Language GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Language> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Language) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedLanguages.SingleOrDefault<Language>((Func<Language, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Language entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Language entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Language entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Language> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Language> entities)
    {
      if (entities == null)
        throw new DuplicateException("The languages are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
