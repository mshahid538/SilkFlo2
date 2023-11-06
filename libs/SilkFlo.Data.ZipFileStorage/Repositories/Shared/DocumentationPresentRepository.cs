// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.DocumentationPresentRepository
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
  public class DocumentationPresentRepository : IDocumentationPresentRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public DocumentationPresentRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public DocumentationPresent Get(string id) => this.GetAsync(id).Result;

    public async Task<DocumentationPresent> GetAsync(string id)
    {
      if (id == null)
        return (DocumentationPresent) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDocumentationPresents.SingleOrDefault<DocumentationPresent>((Func<DocumentationPresent, bool>) (x => x.Id == id));
    }

    public DocumentationPresent SingleOrDefault(Func<DocumentationPresent, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<DocumentationPresent> SingleOrDefaultAsync(
      Func<DocumentationPresent, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDocumentationPresents.Where<DocumentationPresent>(predicate).FirstOrDefault<DocumentationPresent>();
    }

    public bool Add(DocumentationPresent entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(DocumentationPresent entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<DocumentationPresent> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<DocumentationPresent> entities)
    {
      if (entities == null)
        return false;
      foreach (DocumentationPresent entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<DocumentationPresent> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<DocumentationPresent>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<DocumentationPresent>) dataSetAsync.SharedDocumentationPresents.OrderByDescending<DocumentationPresent, Decimal>((Func<DocumentationPresent, Decimal>) (m => m.Weighting)).ThenBy<DocumentationPresent, string>((Func<DocumentationPresent, string>) (m => m.Name));
    }

    public IEnumerable<DocumentationPresent> Find(Func<DocumentationPresent, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<DocumentationPresent>> FindAsync(
      Func<DocumentationPresent, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<DocumentationPresent>) dataSetAsync.SharedDocumentationPresents.Where<DocumentationPresent>(predicate).OrderByDescending<DocumentationPresent, Decimal>((Func<DocumentationPresent, Decimal>) (m => m.Weighting)).ThenBy<DocumentationPresent, string>((Func<DocumentationPresent, string>) (m => m.Name));
    }

    public DocumentationPresent GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<DocumentationPresent> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (DocumentationPresent) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDocumentationPresents.SingleOrDefault<DocumentationPresent>((Func<DocumentationPresent, bool>) (x => x.Name == name));
    }

    public void GetDocumentationPresentFor(IEnumerable<Idea> ideas) => this.GetDocumentationPresentForAsync(ideas).RunSynchronously();

    public async Task GetDocumentationPresentForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetDocumentationPresentForAsync(idea);
    }

    public void GetDocumentationPresentFor(Idea idea) => this.GetDocumentationPresentForAsync(idea).RunSynchronously();

    public async Task GetDocumentationPresentForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        idea1.DocumentationPresent = dataSet.SharedDocumentationPresents.SingleOrDefault<DocumentationPresent>((Func<DocumentationPresent, bool>) (x => x.Id == idea.DocumentationPresentId));
        idea1 = (Idea) null;
        //dataSet = (DataSet) null;
      }
    }

    public DocumentationPresent GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<DocumentationPresent> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (DocumentationPresent) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDocumentationPresents.SingleOrDefault<DocumentationPresent>((Func<DocumentationPresent, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      DocumentationPresent entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(DocumentationPresent entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(DocumentationPresent entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<DocumentationPresent> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<DocumentationPresent> entities)
    {
      if (entities == null)
        throw new DuplicateException("The documentationPresents are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
