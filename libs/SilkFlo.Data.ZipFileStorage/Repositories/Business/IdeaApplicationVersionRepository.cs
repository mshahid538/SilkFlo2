// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.IdeaApplicationVersionRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Repositories.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class IdeaApplicationVersionRepository : IIdeaApplicationVersionRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public IdeaApplicationVersionRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public IdeaApplicationVersion Get(string id) => this.GetAsync(id).Result;

    public async Task<IdeaApplicationVersion> GetAsync(string id)
    {
      if (id == null)
        return (IdeaApplicationVersion) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaApplicationVersions.SingleOrDefault<IdeaApplicationVersion>((Func<IdeaApplicationVersion, bool>) (x => x.Id == id));
    }

    public IdeaApplicationVersion SingleOrDefault(Func<IdeaApplicationVersion, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<IdeaApplicationVersion> SingleOrDefaultAsync(
      Func<IdeaApplicationVersion, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaApplicationVersions.Where<IdeaApplicationVersion>(predicate).FirstOrDefault<IdeaApplicationVersion>();
    }

    public bool Add(IdeaApplicationVersion entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(IdeaApplicationVersion entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<IdeaApplicationVersion> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<IdeaApplicationVersion> entities)
    {
      if (entities == null)
        return false;
      foreach (IdeaApplicationVersion entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<IdeaApplicationVersion> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<IdeaApplicationVersion>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<IdeaApplicationVersion>) dataSetAsync.BusinessIdeaApplicationVersions;
    }

    public IEnumerable<IdeaApplicationVersion> Find(Func<IdeaApplicationVersion, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<IdeaApplicationVersion>> FindAsync(
      Func<IdeaApplicationVersion, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeaApplicationVersions.Where<IdeaApplicationVersion>(predicate);
    }

    public void GetForIdea(Idea idea) => this.GetForIdeaAsync(idea).RunSynchronously();

    public async Task GetForIdeaAsync(Idea idea)
    {
      List<IdeaApplicationVersion> lst;
      if (idea == null)
        lst = (List<IdeaApplicationVersion>) null;
      else if (string.IsNullOrWhiteSpace(idea.Id))
      {
        lst = (List<IdeaApplicationVersion>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaApplicationVersions.Where<IdeaApplicationVersion>((Func<IdeaApplicationVersion, bool>) (x => x.IdeaId == idea.Id)).ToList<IdeaApplicationVersion>();
        //dataSet = (DataSet) null;
        foreach (IdeaApplicationVersion item in lst)
        {
          item.IdeaId = idea.Id;
          item.Idea = idea;
        }
        idea.IdeaApplicationVersions = lst;
        lst = (List<IdeaApplicationVersion>) null;
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

    public void GetForLanguage(Language language) => this.GetForLanguageAsync(language).RunSynchronously();

    public async Task GetForLanguageAsync(Language language)
    {
      List<IdeaApplicationVersion> lst;
      if (language == null)
        lst = (List<IdeaApplicationVersion>) null;
      else if (string.IsNullOrWhiteSpace(language.Id))
      {
        lst = (List<IdeaApplicationVersion>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaApplicationVersions.Where<IdeaApplicationVersion>((Func<IdeaApplicationVersion, bool>) (x => x.LanguageId == language.Id)).ToList<IdeaApplicationVersion>();
        //dataSet = (DataSet) null;
        foreach (IdeaApplicationVersion item in lst)
        {
          item.LanguageId = language.Id;
          item.Language = language;
        }
        language.IdeaApplicationVersions = lst;
        lst = (List<IdeaApplicationVersion>) null;
      }
    }

    public void GetForLanguage(IEnumerable<Language> languages) => this.GetForLanguageAsync(languages).RunSynchronously();

    public async Task GetForLanguageAsync(IEnumerable<Language> languages)
    {
      if (languages == null)
        return;
      foreach (Language language in languages)
        await this.GetForLanguageAsync(language);
    }

    public void GetForVersion(SilkFlo.Data.Core.Domain.Business.Version version) => this.GetForVersionAsync(version).RunSynchronously();

    public async Task GetForVersionAsync(SilkFlo.Data.Core.Domain.Business.Version version)
    {
      List<IdeaApplicationVersion> lst;
      if (version == null)
        lst = (List<IdeaApplicationVersion>) null;
      else if (string.IsNullOrWhiteSpace(version.Id))
      {
        lst = (List<IdeaApplicationVersion>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeaApplicationVersions.Where<IdeaApplicationVersion>((Func<IdeaApplicationVersion, bool>) (x => x.VersionId == version.Id)).ToList<IdeaApplicationVersion>();
        //dataSet = (DataSet) null;
        foreach (IdeaApplicationVersion item in lst)
        {
          item.VersionId = version.Id;
          item.Version = version;
        }
        version.IdeaApplicationVersions = lst;
        lst = (List<IdeaApplicationVersion>) null;
      }
    }

    public void GetForVersion(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions) => this.GetForVersionAsync(versions).RunSynchronously();

    public async Task GetForVersionAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions)
    {
      if (versions == null)
        return;
      foreach (SilkFlo.Data.Core.Domain.Business.Version version in versions)
        await this.GetForVersionAsync(version);
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      IdeaApplicationVersion entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(IdeaApplicationVersion entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(IdeaApplicationVersion entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<IdeaApplicationVersion> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaApplicationVersion> entities)
    {
      if (entities == null)
        throw new DuplicateException("The ideaApplicationVersions are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
