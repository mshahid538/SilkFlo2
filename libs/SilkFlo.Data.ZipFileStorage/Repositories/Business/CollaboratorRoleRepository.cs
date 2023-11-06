// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.CollaboratorRoleRepository
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
  public class CollaboratorRoleRepository : ICollaboratorRoleRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public CollaboratorRoleRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public CollaboratorRole Get(string id) => this.GetAsync(id).Result;

    public async Task<CollaboratorRole> GetAsync(string id)
    {
      if (id == null)
        return (CollaboratorRole) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessCollaboratorRoles.SingleOrDefault<CollaboratorRole>((Func<CollaboratorRole, bool>) (x => x.Id == id));
    }

    public CollaboratorRole SingleOrDefault(Func<CollaboratorRole, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<CollaboratorRole> SingleOrDefaultAsync(Func<CollaboratorRole, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessCollaboratorRoles.Where<CollaboratorRole>(predicate).FirstOrDefault<CollaboratorRole>();
    }

    public bool Add(CollaboratorRole entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(CollaboratorRole entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<CollaboratorRole> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<CollaboratorRole> entities)
    {
      if (entities == null)
        return false;
      foreach (CollaboratorRole entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<CollaboratorRole> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<CollaboratorRole>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<CollaboratorRole>) dataSetAsync.BusinessCollaboratorRoles;
    }

    public IEnumerable<CollaboratorRole> Find(Func<CollaboratorRole, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<CollaboratorRole>> FindAsync(
      Func<CollaboratorRole, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessCollaboratorRoles.Where<CollaboratorRole>(predicate);
    }

    public void GetForCollaborator(Collaborator collaborator) => this.GetForCollaboratorAsync(collaborator).RunSynchronously();

    public async Task GetForCollaboratorAsync(Collaborator collaborator)
    {
      List<CollaboratorRole> lst;
      if (collaborator == null)
        lst = (List<CollaboratorRole>) null;
      else if (string.IsNullOrWhiteSpace(collaborator.Id))
      {
        lst = (List<CollaboratorRole>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessCollaboratorRoles.Where<CollaboratorRole>((Func<CollaboratorRole, bool>) (x => x.CollaboratorId == collaborator.Id)).ToList<CollaboratorRole>();
        //dataSet = (DataSet) null;
        foreach (CollaboratorRole item in lst)
        {
          item.CollaboratorId = collaborator.Id;
          item.Collaborator = collaborator;
        }
        collaborator.CollaboratorRoles = lst;
        lst = (List<CollaboratorRole>) null;
      }
    }

    public void GetForCollaborator(IEnumerable<Collaborator> collaborators) => this.GetForCollaboratorAsync(collaborators).RunSynchronously();

    public async Task GetForCollaboratorAsync(IEnumerable<Collaborator> collaborators)
    {
      if (collaborators == null)
        return;
      foreach (Collaborator collaborator in collaborators)
        await this.GetForCollaboratorAsync(collaborator);
    }

    public void GetForRole(BusinessRole role) => this.GetForRoleAsync(role).RunSynchronously();

    public async Task GetForRoleAsync(BusinessRole role)
    {
      List<CollaboratorRole> lst;
      if (role == null)
        lst = (List<CollaboratorRole>) null;
      else if (string.IsNullOrWhiteSpace(role.Id))
      {
        lst = (List<CollaboratorRole>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessCollaboratorRoles.Where<CollaboratorRole>((Func<CollaboratorRole, bool>) (x => x.RoleId == role.Id)).ToList<CollaboratorRole>();
        //dataSet = (DataSet) null;
        foreach (CollaboratorRole item in lst)
        {
          item.RoleId = role.Id;
          item.Role = role;
        }
        role.CollaboratorRoles = lst;
        lst = (List<CollaboratorRole>) null;
      }
    }

    public void GetForRole(IEnumerable<BusinessRole> roles) => this.GetForRoleAsync(roles).RunSynchronously();

    public async Task GetForRoleAsync(IEnumerable<BusinessRole> roles)
    {
      if (roles == null)
        return;
      foreach (BusinessRole role in roles)
        await this.GetForRoleAsync(role);
    }

    public void GetCollaboratorRoleFor(IEnumerable<UserAuthorisation> userAuthorisations) => this.GetCollaboratorRoleForAsync(userAuthorisations).RunSynchronously();

    public async Task GetCollaboratorRoleForAsync(IEnumerable<UserAuthorisation> userAuthorisations)
    {
      if (userAuthorisations == null)
        return;
      foreach (UserAuthorisation userAuthorisation in userAuthorisations)
        await this.GetCollaboratorRoleForAsync(userAuthorisation);
    }

    public void GetCollaboratorRoleFor(UserAuthorisation userAuthorisation) => this.GetCollaboratorRoleForAsync(userAuthorisation).RunSynchronously();

    public async Task GetCollaboratorRoleForAsync(UserAuthorisation userAuthorisation)
    {
      if (userAuthorisation == null)
        ;
      else
      {
        UserAuthorisation userAuthorisation1 = userAuthorisation;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        userAuthorisation1.CollaboratorRole = dataSet.BusinessCollaboratorRoles.SingleOrDefault<CollaboratorRole>((Func<CollaboratorRole, bool>) (x => x.Id == userAuthorisation.CollaboratorRoleId));
        userAuthorisation1 = (UserAuthorisation) null;
        //dataSet = (DataSet) null;
      }
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      CollaboratorRole entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(CollaboratorRole entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(CollaboratorRole entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<CollaboratorRole> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<CollaboratorRole> entities)
    {
      if (entities == null)
        throw new DuplicateException("The collaboratorRoles are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
