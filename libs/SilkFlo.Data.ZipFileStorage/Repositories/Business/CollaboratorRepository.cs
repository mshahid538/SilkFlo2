// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.CollaboratorRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Repositories.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class CollaboratorRepository : ICollaboratorRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public CollaboratorRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Collaborator Get(string id) => this.GetAsync(id).Result;

    public async Task<Collaborator> GetAsync(string id)
    {
      if (id == null)
        return (Collaborator) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessCollaborators.SingleOrDefault<Collaborator>((Func<Collaborator, bool>) (x => x.Id == id));
    }

    public Collaborator SingleOrDefault(Func<Collaborator, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Collaborator> SingleOrDefaultAsync(Func<Collaborator, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessCollaborators.Where<Collaborator>(predicate).FirstOrDefault<Collaborator>();
    }

    public bool Add(Collaborator entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Collaborator entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Collaborator> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Collaborator> entities)
    {
      if (entities == null)
        return false;
      foreach (Collaborator entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Collaborator> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Collaborator>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Collaborator>) dataSetAsync.BusinessCollaborators;
    }

    public IEnumerable<Collaborator> Find(Func<Collaborator, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Collaborator>> FindAsync(Func<Collaborator, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessCollaborators.Where<Collaborator>(predicate);
    }

    public void GetForIdea(Idea idea) => this.GetForIdeaAsync(idea).RunSynchronously();

    public async Task GetForIdeaAsync(Idea idea)
    {
      List<Collaborator> lst;
      if (idea == null)
        lst = (List<Collaborator>) null;
      else if (string.IsNullOrWhiteSpace(idea.Id))
      {
        lst = (List<Collaborator>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessCollaborators.Where<Collaborator>((Func<Collaborator, bool>) (x => x.IdeaId == idea.Id)).ToList<Collaborator>();
        //dataSet = (DataSet) null;
        foreach (Collaborator item in lst)
        {
          item.IdeaId = idea.Id;
          item.Idea = idea;
        }
        idea.Collaborators = lst;
        lst = (List<Collaborator>) null;
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

    public void GetForInvitedBy(User invitedBy) => this.GetForInvitedByAsync(invitedBy).RunSynchronously();

    public async Task GetForInvitedByAsync(User invitedBy)
    {
      List<Collaborator> lst;
      if (invitedBy == null)
        lst = (List<Collaborator>) null;
      else if (string.IsNullOrWhiteSpace(invitedBy.Id))
      {
        lst = (List<Collaborator>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessCollaborators.Where<Collaborator>((Func<Collaborator, bool>) (x => x.InvitedById == invitedBy.Id)).ToList<Collaborator>();
        //dataSet = (DataSet) null;
        foreach (Collaborator item in lst)
        {
          item.InvitedById = invitedBy.Id;
          item.InvitedBy = invitedBy;
        }
        invitedBy.InvitedCollaborators = lst;
        lst = (List<Collaborator>) null;
      }
    }

    public void GetForInvitedBy(IEnumerable<User> invitedBies) => this.GetForInvitedByAsync(invitedBies).RunSynchronously();

    public async Task GetForInvitedByAsync(IEnumerable<User> invitedBies)
    {
      if (invitedBies == null)
        return;
      foreach (User invitedBy in invitedBies)
        await this.GetForInvitedByAsync(invitedBy);
    }

    public void GetForUser(User user) => this.GetForUserAsync(user).RunSynchronously();

    public async Task GetForUserAsync(User user)
    {
      List<Collaborator> lst;
      if (user == null)
        lst = (List<Collaborator>) null;
      else if (string.IsNullOrWhiteSpace(user.Id))
      {
        lst = (List<Collaborator>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessCollaborators.Where<Collaborator>((Func<Collaborator, bool>) (x => x.UserId == user.Id)).ToList<Collaborator>();
        //dataSet = (DataSet) null;
        foreach (Collaborator item in lst)
        {
          item.UserId = user.Id;
          item.User = user;
        }
        user.Collaborators = lst;
        lst = (List<Collaborator>) null;
      }
    }

    public void GetForUser(IEnumerable<User> users) => this.GetForUserAsync(users).RunSynchronously();

    public async Task GetForUserAsync(IEnumerable<User> users)
    {
      if (users == null)
        return;
      foreach (User user in users)
        await this.GetForUserAsync(user);
    }

    public void GetCollaboratorFor(IEnumerable<CollaboratorRole> collaboratorRoles) => this.GetCollaboratorForAsync(collaboratorRoles).RunSynchronously();

    public async Task GetCollaboratorForAsync(IEnumerable<CollaboratorRole> collaboratorRoles)
    {
      if (collaboratorRoles == null)
        return;
      foreach (CollaboratorRole collaboratorRole in collaboratorRoles)
        await this.GetCollaboratorForAsync(collaboratorRole);
    }

    public void GetCollaboratorFor(CollaboratorRole collaboratorRole) => this.GetCollaboratorForAsync(collaboratorRole).RunSynchronously();

    public async Task GetCollaboratorForAsync(CollaboratorRole collaboratorRole)
    {
      if (collaboratorRole == null)
        ;
      else
      {
        CollaboratorRole collaboratorRole1 = collaboratorRole;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        collaboratorRole1.Collaborator = dataSet.BusinessCollaborators.SingleOrDefault<Collaborator>((Func<Collaborator, bool>) (x => x.Id == collaboratorRole.CollaboratorId));
        collaboratorRole1 = (CollaboratorRole) null;
        //dataSet = (DataSet) null;
      }
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Collaborator entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Collaborator entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Collaborator entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Collaborator> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Collaborator> entities)
    {
      if (entities == null)
        throw new DuplicateException("The collaborators are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
