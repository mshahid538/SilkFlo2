// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.UserAuthorisationRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Repositories.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class UserAuthorisationRepository : IUserAuthorisationRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public UserAuthorisationRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public UserAuthorisation Get(string id) => this.GetAsync(id).Result;

    public async Task<UserAuthorisation> GetAsync(string id)
    {
      if (id == null)
        return (UserAuthorisation) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessUserAuthorisations.SingleOrDefault<UserAuthorisation>((Func<UserAuthorisation, bool>) (x => x.Id == id));
    }

    public UserAuthorisation SingleOrDefault(Func<UserAuthorisation, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<UserAuthorisation> SingleOrDefaultAsync(
      Func<UserAuthorisation, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessUserAuthorisations.Where<UserAuthorisation>(predicate).FirstOrDefault<UserAuthorisation>();
    }

    public bool Add(UserAuthorisation entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(UserAuthorisation entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<UserAuthorisation> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<UserAuthorisation> entities)
    {
      if (entities == null)
        return false;
      foreach (UserAuthorisation entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<UserAuthorisation> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<UserAuthorisation>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<UserAuthorisation>) dataSetAsync.BusinessUserAuthorisations.OrderBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (m => m.UserId)).ThenBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (m => m.IdeaId)).ThenBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (m => m.IdeaAuthorisationId));
    }

    public IEnumerable<UserAuthorisation> Find(Func<UserAuthorisation, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<UserAuthorisation>> FindAsync(
      Func<UserAuthorisation, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<UserAuthorisation>) dataSetAsync.BusinessUserAuthorisations.Where<UserAuthorisation>(predicate).OrderBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (m => m.UserId)).ThenBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (m => m.IdeaId)).ThenBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (m => m.IdeaAuthorisationId));
    }

    public void GetForCollaboratorRole(CollaboratorRole collaboratorRole) => this.GetForCollaboratorRoleAsync(collaboratorRole).RunSynchronously();

    public async Task GetForCollaboratorRoleAsync(CollaboratorRole collaboratorRole)
    {
      List<UserAuthorisation> lst;
      if (collaboratorRole == null)
        lst = (List<UserAuthorisation>) null;
      else if (string.IsNullOrWhiteSpace(collaboratorRole.Id))
      {
        lst = (List<UserAuthorisation>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessUserAuthorisations.Where<UserAuthorisation>((Func<UserAuthorisation, bool>) (x => x.CollaboratorRoleId == collaboratorRole.Id)).OrderBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (x => x.UserId)).ThenBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (x => x.IdeaId)).ThenBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (x => x.IdeaAuthorisationId)).ToList<UserAuthorisation>();
        //dataSet = (DataSet) null;
        foreach (UserAuthorisation item in lst)
        {
          item.CollaboratorRoleId = collaboratorRole.Id;
          item.CollaboratorRole = collaboratorRole;
        }
        collaboratorRole.UserAuthorisations = lst;
        lst = (List<UserAuthorisation>) null;
      }
    }

    public void GetForCollaboratorRole(IEnumerable<CollaboratorRole> collaboratorRoles) => this.GetForCollaboratorRoleAsync(collaboratorRoles).RunSynchronously();

    public async Task GetForCollaboratorRoleAsync(IEnumerable<CollaboratorRole> collaboratorRoles)
    {
      if (collaboratorRoles == null)
        return;
      foreach (CollaboratorRole collaboratorRole in collaboratorRoles)
        await this.GetForCollaboratorRoleAsync(collaboratorRole);
    }

    public void GetForIdeaAuthorisation(IdeaAuthorisation ideaAuthorisation) => this.GetForIdeaAuthorisationAsync(ideaAuthorisation).RunSynchronously();

    public async Task GetForIdeaAuthorisationAsync(IdeaAuthorisation ideaAuthorisation)
    {
      List<UserAuthorisation> lst;
      if (ideaAuthorisation == null)
        lst = (List<UserAuthorisation>) null;
      else if (string.IsNullOrWhiteSpace(ideaAuthorisation.Id))
      {
        lst = (List<UserAuthorisation>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessUserAuthorisations.Where<UserAuthorisation>((Func<UserAuthorisation, bool>) (x => x.IdeaAuthorisationId == ideaAuthorisation.Id)).OrderBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (x => x.UserId)).ThenBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (x => x.IdeaId)).ThenBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (x => x.IdeaAuthorisationId)).ToList<UserAuthorisation>();
        //dataSet = (DataSet) null;
        foreach (UserAuthorisation item in lst)
        {
          item.IdeaAuthorisationId = ideaAuthorisation.Id;
          item.IdeaAuthorisation = ideaAuthorisation;
        }
        ideaAuthorisation.UserAuthorisations = lst;
        lst = (List<UserAuthorisation>) null;
      }
    }

    public void GetForIdeaAuthorisation(IEnumerable<IdeaAuthorisation> ideaAuthorisations) => this.GetForIdeaAuthorisationAsync(ideaAuthorisations).RunSynchronously();

    public async Task GetForIdeaAuthorisationAsync(IEnumerable<IdeaAuthorisation> ideaAuthorisations)
    {
      if (ideaAuthorisations == null)
        return;
      foreach (IdeaAuthorisation ideaAuthorisation in ideaAuthorisations)
        await this.GetForIdeaAuthorisationAsync(ideaAuthorisation);
    }

    public void GetForIdea(Idea idea) => this.GetForIdeaAsync(idea).RunSynchronously();

    public async Task GetForIdeaAsync(Idea idea)
    {
      List<UserAuthorisation> lst;
      if (idea == null)
        lst = (List<UserAuthorisation>) null;
      else if (string.IsNullOrWhiteSpace(idea.Id))
      {
        lst = (List<UserAuthorisation>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessUserAuthorisations.Where<UserAuthorisation>((Func<UserAuthorisation, bool>) (x => x.IdeaId == idea.Id)).OrderBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (x => x.UserId)).ThenBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (x => x.IdeaId)).ThenBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (x => x.IdeaAuthorisationId)).ToList<UserAuthorisation>();
        //dataSet = (DataSet) null;
        foreach (UserAuthorisation item in lst)
        {
          item.IdeaId = idea.Id;
          item.Idea = idea;
        }
        idea.UserAuthorisations = lst;
        lst = (List<UserAuthorisation>) null;
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

    public void GetForUser(User user) => this.GetForUserAsync(user).RunSynchronously();

    public async Task GetForUserAsync(User user)
    {
      List<UserAuthorisation> lst;
      if (user == null)
        lst = (List<UserAuthorisation>) null;
      else if (string.IsNullOrWhiteSpace(user.Id))
      {
        lst = (List<UserAuthorisation>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessUserAuthorisations.Where<UserAuthorisation>((Func<UserAuthorisation, bool>) (x => x.UserId == user.Id)).OrderBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (x => x.UserId)).ThenBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (x => x.IdeaId)).ThenBy<UserAuthorisation, string>((Func<UserAuthorisation, string>) (x => x.IdeaAuthorisationId)).ToList<UserAuthorisation>();
        //dataSet = (DataSet) null;
        foreach (UserAuthorisation item in lst)
        {
          item.UserId = user.Id;
          item.User = user;
        }
        user.UserAuthorisations = lst;
        lst = (List<UserAuthorisation>) null;
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

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      UserAuthorisation entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(UserAuthorisation entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(UserAuthorisation entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<UserAuthorisation> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<UserAuthorisation> entities)
    {
      if (entities == null)
        throw new DuplicateException("The userAuthorisations are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
