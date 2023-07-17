// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.DataInputPercentOfStructuredRepository
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
  public class DataInputPercentOfStructuredRepository : IDataInputPercentOfStructuredRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public DataInputPercentOfStructuredRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public DataInputPercentOfStructured Get(string id) => this.GetAsync(id).Result;

    public async Task<DataInputPercentOfStructured> GetAsync(string id)
    {
      if (id == null)
        return (DataInputPercentOfStructured) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDataInputPercentOfStructureds.SingleOrDefault<DataInputPercentOfStructured>((Func<DataInputPercentOfStructured, bool>) (x => x.Id == id));
    }

    public DataInputPercentOfStructured SingleOrDefault(
      Func<DataInputPercentOfStructured, bool> predicate)
    {
      return this.SingleOrDefaultAsync(predicate).Result;
    }

    public async Task<DataInputPercentOfStructured> SingleOrDefaultAsync(
      Func<DataInputPercentOfStructured, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDataInputPercentOfStructureds.Where<DataInputPercentOfStructured>(predicate).FirstOrDefault<DataInputPercentOfStructured>();
    }

    public bool Add(DataInputPercentOfStructured entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(DataInputPercentOfStructured entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<DataInputPercentOfStructured> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<DataInputPercentOfStructured> entities)
    {
      if (entities == null)
        return false;
      foreach (DataInputPercentOfStructured entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<DataInputPercentOfStructured> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<DataInputPercentOfStructured>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<DataInputPercentOfStructured>) dataSetAsync.SharedDataInputPercentOfStructureds.OrderByDescending<DataInputPercentOfStructured, Decimal>((Func<DataInputPercentOfStructured, Decimal>) (m => m.Weighting)).ThenBy<DataInputPercentOfStructured, string>((Func<DataInputPercentOfStructured, string>) (m => m.Name));
    }

    public IEnumerable<DataInputPercentOfStructured> Find(
      Func<DataInputPercentOfStructured, bool> predicate)
    {
      return this.FindAsync(predicate).Result;
    }

    public async Task<IEnumerable<DataInputPercentOfStructured>> FindAsync(
      Func<DataInputPercentOfStructured, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<DataInputPercentOfStructured>) dataSetAsync.SharedDataInputPercentOfStructureds.Where<DataInputPercentOfStructured>(predicate).OrderByDescending<DataInputPercentOfStructured, Decimal>((Func<DataInputPercentOfStructured, Decimal>) (m => m.Weighting)).ThenBy<DataInputPercentOfStructured, string>((Func<DataInputPercentOfStructured, string>) (m => m.Name));
    }

    public DataInputPercentOfStructured GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<DataInputPercentOfStructured> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (DataInputPercentOfStructured) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDataInputPercentOfStructureds.SingleOrDefault<DataInputPercentOfStructured>((Func<DataInputPercentOfStructured, bool>) (x => x.Name == name));
    }

    public void GetDataInputPercentOfStructuredFor(IEnumerable<Idea> ideas) => this.GetDataInputPercentOfStructuredForAsync(ideas).RunSynchronously();

    public async Task GetDataInputPercentOfStructuredForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetDataInputPercentOfStructuredForAsync(idea);
    }

    public void GetDataInputPercentOfStructuredFor(Idea idea) => this.GetDataInputPercentOfStructuredForAsync(idea).RunSynchronously();

    public async Task GetDataInputPercentOfStructuredForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        idea1.DataInputPercentOfStructured = dataSet.SharedDataInputPercentOfStructureds.SingleOrDefault<DataInputPercentOfStructured>((Func<DataInputPercentOfStructured, bool>) (x => x.Id == idea.DataInputPercentOfStructuredId));
        idea1 = (Idea) null;
        dataSet = (DataSet) null;
      }
    }

    public DataInputPercentOfStructured GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<DataInputPercentOfStructured> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (DataInputPercentOfStructured) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedDataInputPercentOfStructureds.SingleOrDefault<DataInputPercentOfStructured>((Func<DataInputPercentOfStructured, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      DataInputPercentOfStructured entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(DataInputPercentOfStructured entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(DataInputPercentOfStructured entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<DataInputPercentOfStructured> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(
      IEnumerable<DataInputPercentOfStructured> entities)
    {
      if (entities == null)
        throw new DuplicateException("The dataInputPercentOfStructureds are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
