// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.InputRepository
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
  public class InputRepository : IInputRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public InputRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Input Get(string id) => this.GetAsync(id).Result;

    public async Task<Input> GetAsync(string id)
    {
      if (id == null)
        return (Input) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedInputs.SingleOrDefault<Input>((Func<Input, bool>) (x => x.Id == id));
    }

    public Input SingleOrDefault(Func<Input, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Input> SingleOrDefaultAsync(Func<Input, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedInputs.Where<Input>(predicate).FirstOrDefault<Input>();
    }

    public bool Add(Input entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Input entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Input> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Input> entities)
    {
      if (entities == null)
        return false;
      foreach (Input entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Input> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Input>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Input>) dataSetAsync.SharedInputs.OrderByDescending<Input, Decimal>((Func<Input, Decimal>) (m => m.Weighting)).ThenBy<Input, string>((Func<Input, string>) (m => m.Name));
    }

    public IEnumerable<Input> Find(Func<Input, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Input>> FindAsync(Func<Input, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Input>) dataSetAsync.SharedInputs.Where<Input>(predicate).OrderByDescending<Input, Decimal>((Func<Input, Decimal>) (m => m.Weighting)).ThenBy<Input, string>((Func<Input, string>) (m => m.Name));
    }

    public Input GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Input> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Input) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedInputs.SingleOrDefault<Input>((Func<Input, bool>) (x => x.Name == name));
    }

    public void GetInputFor(IEnumerable<Idea> ideas) => this.GetInputForAsync(ideas).RunSynchronously();

    public async Task GetInputForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetInputForAsync(idea);
    }

    public void GetInputFor(Idea idea) => this.GetInputForAsync(idea).RunSynchronously();

    public async Task GetInputForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        idea1.Input = dataSet.SharedInputs.SingleOrDefault<Input>((Func<Input, bool>) (x => x.Id == idea.InputId));
        idea1 = (Idea) null;
        //dataSet = (DataSet) null;
      }
    }

    public Input GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Input> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Input) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedInputs.SingleOrDefault<Input>((Func<Input, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Input entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Input entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Input entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Input> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Input> entities)
    {
      if (entities == null)
        throw new DuplicateException("The inputs are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
