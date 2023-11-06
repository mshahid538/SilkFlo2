// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.DocumentRepository
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
  public class DocumentRepository : IDocumentRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public DocumentRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Document Get(string id) => this.GetAsync(id).Result;

    public async Task<Document> GetAsync(string id)
    {
      if (id == null)
        return (Document) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessDocuments.SingleOrDefault<Document>((Func<Document, bool>) (x => x.Id == id));
    }

    public Document SingleOrDefault(Func<Document, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Document> SingleOrDefaultAsync(Func<Document, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessDocuments.Where<Document>(predicate).FirstOrDefault<Document>();
    }

    public bool Add(Document entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Document entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Document> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Document> entities)
    {
      if (entities == null)
        return false;
      foreach (Document entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Document> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Document>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Document>) dataSetAsync.BusinessDocuments.OrderBy<Document, string>((Func<Document, string>) (m => m.ClientId)).ThenBy<Document, string>((Func<Document, string>) (m => m.Filename)).ThenBy<Document, string>((Func<Document, string>) (m => m.Text));
    }

    public IEnumerable<Document> Find(Func<Document, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Document>> FindAsync(Func<Document, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Document>) dataSetAsync.BusinessDocuments.Where<Document>(predicate).OrderBy<Document, string>((Func<Document, string>) (m => m.ClientId)).ThenBy<Document, string>((Func<Document, string>) (m => m.Filename)).ThenBy<Document, string>((Func<Document, string>) (m => m.Text));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<Document> lst;
      if (client == null)
        lst = (List<Document>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<Document>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessDocuments.Where<Document>((Func<Document, bool>) (x => x.ClientId == client.Id)).OrderBy<Document, string>((Func<Document, string>) (x => x.ClientId)).ThenBy<Document, string>((Func<Document, string>) (x => x.Filename)).ThenBy<Document, string>((Func<Document, string>) (x => x.Text)).ToList<Document>();
        //dataSet = (DataSet) null;
        foreach (Document item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Documents = lst;
        lst = (List<Document>) null;
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

    public void GetForIdea(Idea idea) => this.GetForIdeaAsync(idea).RunSynchronously();

    public async Task GetForIdeaAsync(Idea idea)
    {
      List<Document> lst;
      if (idea == null)
        lst = (List<Document>) null;
      else if (string.IsNullOrWhiteSpace(idea.Id))
      {
        lst = (List<Document>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessDocuments.Where<Document>((Func<Document, bool>) (x => x.IdeaId == idea.Id)).OrderBy<Document, string>((Func<Document, string>) (x => x.ClientId)).ThenBy<Document, string>((Func<Document, string>) (x => x.Filename)).ThenBy<Document, string>((Func<Document, string>) (x => x.Text)).ToList<Document>();
        //dataSet = (DataSet) null;
        foreach (Document item in lst)
        {
          item.IdeaId = idea.Id;
          item.Idea = idea;
        }
        idea.Documents = lst;
        lst = (List<Document>) null;
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

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Document entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Document entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Document entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Document> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Document> entities)
    {
      if (entities == null)
        throw new DuplicateException("The documents are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
