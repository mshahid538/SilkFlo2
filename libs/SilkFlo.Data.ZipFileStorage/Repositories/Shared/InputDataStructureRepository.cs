// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.InputDataStructureRepository
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
  public class InputDataStructureRepository : IInputDataStructureRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public InputDataStructureRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public InputDataStructure Get(string id) => this.GetAsync(id).Result;

    public async Task<InputDataStructure> GetAsync(string id)
    {
      if (id == null)
        return (InputDataStructure) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedInputDataStructures.SingleOrDefault<InputDataStructure>((Func<InputDataStructure, bool>) (x => x.Id == id));
    }

    public InputDataStructure SingleOrDefault(Func<InputDataStructure, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<InputDataStructure> SingleOrDefaultAsync(
      Func<InputDataStructure, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedInputDataStructures.Where<InputDataStructure>(predicate).FirstOrDefault<InputDataStructure>();
    }

    public bool Add(InputDataStructure entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(InputDataStructure entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<InputDataStructure> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<InputDataStructure> entities)
    {
      if (entities == null)
        return false;
      foreach (InputDataStructure entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<InputDataStructure> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<InputDataStructure>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<InputDataStructure>) dataSetAsync.SharedInputDataStructures.OrderByDescending<InputDataStructure, Decimal>((Func<InputDataStructure, Decimal>) (m => m.Weighting)).ThenBy<InputDataStructure, string>((Func<InputDataStructure, string>) (m => m.Name));
    }

    public IEnumerable<InputDataStructure> Find(Func<InputDataStructure, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<InputDataStructure>> FindAsync(
      Func<InputDataStructure, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<InputDataStructure>) dataSetAsync.SharedInputDataStructures.Where<InputDataStructure>(predicate).OrderByDescending<InputDataStructure, Decimal>((Func<InputDataStructure, Decimal>) (m => m.Weighting)).ThenBy<InputDataStructure, string>((Func<InputDataStructure, string>) (m => m.Name));
    }

    public InputDataStructure GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<InputDataStructure> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (InputDataStructure) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedInputDataStructures.SingleOrDefault<InputDataStructure>((Func<InputDataStructure, bool>) (x => x.Name == name));
    }

    public void GetInputDataStructureFor(IEnumerable<Idea> ideas) => this.GetInputDataStructureForAsync(ideas).RunSynchronously();

    public async Task GetInputDataStructureForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetInputDataStructureForAsync(idea);
    }

    public void GetInputDataStructureFor(Idea idea) => this.GetInputDataStructureForAsync(idea).RunSynchronously();

    public async Task GetInputDataStructureForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        idea1.InputDataStructure = dataSet.SharedInputDataStructures.SingleOrDefault<InputDataStructure>((Func<InputDataStructure, bool>) (x => x.Id == idea.InputDataStructureId));
        idea1 = (Idea) null;
        dataSet = (DataSet) null;
      }
    }

    public InputDataStructure GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<InputDataStructure> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (InputDataStructure) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedInputDataStructures.SingleOrDefault<InputDataStructure>((Func<InputDataStructure, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      InputDataStructure entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(InputDataStructure entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(InputDataStructure entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<InputDataStructure> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<InputDataStructure> entities)
    {
      if (entities == null)
        throw new DuplicateException("The inputDataStructures are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
