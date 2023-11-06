// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Shared.IdeaAuthorisationRepository
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
  public class IdeaAuthorisationRepository : IIdeaAuthorisationRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public IdeaAuthorisationRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public IdeaAuthorisation Get(string id) => this.GetAsync(id).Result;

    public async Task<IdeaAuthorisation> GetAsync(string id)
    {
      if (id == null)
        return (IdeaAuthorisation) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedIdeaAuthorisations.SingleOrDefault<IdeaAuthorisation>((Func<IdeaAuthorisation, bool>) (x => x.Id == id));
    }

    public IdeaAuthorisation SingleOrDefault(Func<IdeaAuthorisation, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<IdeaAuthorisation> SingleOrDefaultAsync(
      Func<IdeaAuthorisation, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedIdeaAuthorisations.Where<IdeaAuthorisation>(predicate).FirstOrDefault<IdeaAuthorisation>();
    }

    public bool Add(IdeaAuthorisation entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(IdeaAuthorisation entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<IdeaAuthorisation> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<IdeaAuthorisation> entities)
    {
      if (entities == null)
        return false;
      foreach (IdeaAuthorisation entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<IdeaAuthorisation> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<IdeaAuthorisation>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<IdeaAuthorisation>) dataSetAsync.SharedIdeaAuthorisations.OrderBy<IdeaAuthorisation, int>((Func<IdeaAuthorisation, int>) (m => m.Sort)).ThenBy<IdeaAuthorisation, string>((Func<IdeaAuthorisation, string>) (m => m.Name));
    }

    public IEnumerable<IdeaAuthorisation> Find(Func<IdeaAuthorisation, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<IdeaAuthorisation>> FindAsync(
      Func<IdeaAuthorisation, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<IdeaAuthorisation>) dataSetAsync.SharedIdeaAuthorisations.Where<IdeaAuthorisation>(predicate).OrderBy<IdeaAuthorisation, int>((Func<IdeaAuthorisation, int>) (m => m.Sort)).ThenBy<IdeaAuthorisation, string>((Func<IdeaAuthorisation, string>) (m => m.Name));
    }

    public IdeaAuthorisation GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<IdeaAuthorisation> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (IdeaAuthorisation) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedIdeaAuthorisations.SingleOrDefault<IdeaAuthorisation>((Func<IdeaAuthorisation, bool>) (x => x.Name == name));
    }

    public void GetIdeaAuthorisationFor(
      IEnumerable<RoleIdeaAuthorisation> roleIdeaAuthorisations)
    {
      this.GetIdeaAuthorisationForAsync(roleIdeaAuthorisations).RunSynchronously();
    }

    public async Task GetIdeaAuthorisationForAsync(
      IEnumerable<RoleIdeaAuthorisation> roleIdeaAuthorisations)
    {
      if (roleIdeaAuthorisations == null)
        return;
      foreach (RoleIdeaAuthorisation roleIdeaAuthorisation in roleIdeaAuthorisations)
        await this.GetIdeaAuthorisationForAsync(roleIdeaAuthorisation);
    }

    public void GetIdeaAuthorisationFor(RoleIdeaAuthorisation roleIdeaAuthorisation) => this.GetIdeaAuthorisationForAsync(roleIdeaAuthorisation).RunSynchronously();

    public async Task GetIdeaAuthorisationForAsync(RoleIdeaAuthorisation roleIdeaAuthorisation)
    {
      if (roleIdeaAuthorisation == null)
        ;
      else
      {
        RoleIdeaAuthorisation ideaAuthorisation = roleIdeaAuthorisation;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        ideaAuthorisation.IdeaAuthorisation = dataSet.SharedIdeaAuthorisations.SingleOrDefault<IdeaAuthorisation>((Func<IdeaAuthorisation, bool>) (x => x.Id == roleIdeaAuthorisation.IdeaAuthorisationId));
        ideaAuthorisation = (RoleIdeaAuthorisation) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetIdeaAuthorisationFor(IEnumerable<UserAuthorisation> userAuthorisations) => this.GetIdeaAuthorisationForAsync(userAuthorisations).RunSynchronously();

    public async Task GetIdeaAuthorisationForAsync(IEnumerable<UserAuthorisation> userAuthorisations)
    {
      if (userAuthorisations == null)
        return;
      foreach (UserAuthorisation userAuthorisation in userAuthorisations)
        await this.GetIdeaAuthorisationForAsync(userAuthorisation);
    }

    public void GetIdeaAuthorisationFor(UserAuthorisation userAuthorisation) => this.GetIdeaAuthorisationForAsync(userAuthorisation).RunSynchronously();

    public async Task GetIdeaAuthorisationForAsync(UserAuthorisation userAuthorisation)
    {
      if (userAuthorisation == null)
        ;
      else
      {
        UserAuthorisation userAuthorisation1 = userAuthorisation;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        userAuthorisation1.IdeaAuthorisation = dataSet.SharedIdeaAuthorisations.SingleOrDefault<IdeaAuthorisation>((Func<IdeaAuthorisation, bool>) (x => x.Id == userAuthorisation.IdeaAuthorisationId));
        userAuthorisation1 = (UserAuthorisation) null;
        //dataSet = (DataSet) null;
      }
    }

    public IdeaAuthorisation GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<IdeaAuthorisation> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (IdeaAuthorisation) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.SharedIdeaAuthorisations.SingleOrDefault<IdeaAuthorisation>((Func<IdeaAuthorisation, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      IdeaAuthorisation entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(IdeaAuthorisation entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(IdeaAuthorisation entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<IdeaAuthorisation> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaAuthorisation> entities)
    {
      if (entities == null)
        throw new DuplicateException("The ideaAuthorisations are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
