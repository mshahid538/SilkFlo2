// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.UserRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public UserRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public User Get(string id) => this.GetAsync(id).Result;

    public async Task<User> GetAsync(string id)
    {
      if (id == null)
        return (User) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == id));
    }

    public User SingleOrDefault(Func<User, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<User> SingleOrDefaultAsync(Func<User, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.Users.Where<User>(predicate).FirstOrDefault<User>();
    }

    public bool Add(User entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(User entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<User> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<User> entities)
    {
      if (entities == null)
        return false;
      foreach (User entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<User> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<User>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<User>) dataSetAsync.Users.OrderBy<User, string>((Func<User, string>) (m => m.FirstName)).ThenBy<User, string>((Func<User, string>) (m => m.LastName));
    }

    public IEnumerable<User> Find(Func<User, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<User>> FindAsync(Func<User, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<User>) dataSetAsync.Users.Where<User>(predicate).OrderBy<User, string>((Func<User, string>) (m => m.FirstName)).ThenBy<User, string>((Func<User, string>) (m => m.LastName));
    }

    public User GetUsingEmail(string email) => this.GetUsingEmailAsync(email).Result;

    public async Task<User> GetUsingEmailAsync(string email)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Email.ToLower() == email.ToLower()));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<User> lst;
      if (client == null)
        lst = (List<User>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<User>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.Users.Where<User>((Func<User, bool>) (x => x.ClientId == client.Id)).OrderBy<User, string>((Func<User, string>) (x => x.FirstName)).ThenBy<User, string>((Func<User, string>) (x => x.LastName)).ToList<User>();
        //dataSet = (DataSet) null;
        foreach (User item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Users = lst;
        lst = (List<User>) null;
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

    public void GetForDepartment(Department department) => this.GetForDepartmentAsync(department).RunSynchronously();

    public async Task GetForDepartmentAsync(Department department)
    {
      List<User> lst;
      if (department == null)
        lst = (List<User>) null;
      else if (string.IsNullOrWhiteSpace(department.Id))
      {
        lst = (List<User>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.Users.Where<User>((Func<User, bool>) (x => x.DepartmentId == department.Id)).OrderBy<User, string>((Func<User, string>) (x => x.FirstName)).ThenBy<User, string>((Func<User, string>) (x => x.LastName)).ToList<User>();
        //dataSet = (DataSet) null;
        foreach (User item in lst)
        {
          item.DepartmentId = department.Id;
          item.Department = department;
        }
        department.Users = lst;
        lst = (List<User>) null;
      }
    }

    public void GetForDepartment(IEnumerable<Department> departments) => this.GetForDepartmentAsync(departments).RunSynchronously();

    public async Task GetForDepartmentAsync(IEnumerable<Department> departments)
    {
      if (departments == null)
        return;
      foreach (Department department in departments)
        await this.GetForDepartmentAsync(department);
    }

    public void GetForLocation(Location location) => this.GetForLocationAsync(location).RunSynchronously();

    public async Task GetForLocationAsync(Location location)
    {
      List<User> lst;
      if (location == null)
        lst = (List<User>) null;
      else if (string.IsNullOrWhiteSpace(location.Id))
      {
        lst = (List<User>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.Users.Where<User>((Func<User, bool>) (x => x.LocationId == location.Id)).OrderBy<User, string>((Func<User, string>) (x => x.FirstName)).ThenBy<User, string>((Func<User, string>) (x => x.LastName)).ToList<User>();
        //dataSet = (DataSet) null;
        foreach (User item in lst)
        {
          item.LocationId = location.Id;
          item.Location = location;
        }
        location.Users = lst;
        lst = (List<User>) null;
      }
    }

    public void GetForLocation(IEnumerable<Location> locations) => this.GetForLocationAsync(locations).RunSynchronously();

    public async Task GetForLocationAsync(IEnumerable<Location> locations)
    {
      if (locations == null)
        return;
      foreach (Location location in locations)
        await this.GetForLocationAsync(location);
    }

    public void GetForManager(User manager) => this.GetForManagerAsync(manager).RunSynchronously();

    public async Task GetForManagerAsync(User manager)
    {
      List<User> lst;
      if (manager == null)
        lst = (List<User>) null;
      else if (string.IsNullOrWhiteSpace(manager.Id))
      {
        lst = (List<User>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.Users.Where<User>((Func<User, bool>) (x => x.ManagerId == manager.Id)).OrderBy<User, string>((Func<User, string>) (x => x.FirstName)).ThenBy<User, string>((Func<User, string>) (x => x.LastName)).ToList<User>();
        //dataSet = (DataSet) null;
        foreach (User item in lst)
        {
          item.ManagerId = manager.Id;
          item.Manager = manager;
        }
        manager.TeamMembers = lst;
        lst = (List<User>) null;
      }
    }

    public void GetForManager(IEnumerable<User> managers) => this.GetForManagerAsync(managers).RunSynchronously();

    public async Task GetForManagerAsync(IEnumerable<User> managers)
    {
      if (managers == null)
        return;
      foreach (User manager in managers)
        await this.GetForManagerAsync(manager);
    }

    public void GetUserFor(IEnumerable<Analytic> analytics) => this.GetUserForAsync(analytics).RunSynchronously();

    public async Task GetUserForAsync(IEnumerable<Analytic> analytics)
    {
      if (analytics == null)
        return;
      foreach (Analytic analytic in analytics)
        await this.GetUserForAsync(analytic);
    }

    public void GetUserFor(Analytic analytic) => this.GetUserForAsync(analytic).RunSynchronously();

    public async Task GetUserForAsync(Analytic analytic)
    {
      if (analytic == null)
        ;
      else
      {
        Analytic analytic1 = analytic;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        analytic1.User = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == analytic.UserId));
        analytic1 = (Analytic) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetAccountOwnerFor(IEnumerable<Client> accountOwners) => this.GetAccountOwnerForAsync(accountOwners).RunSynchronously();

    public async Task GetAccountOwnerForAsync(IEnumerable<Client> accountOwners)
    {
      if (accountOwners == null)
        return;
      foreach (Client client in accountOwners)
        await this.GetAccountOwnerForAsync(client);
    }

    public void GetAccountOwnerFor(Client client) => this.GetAccountOwnerForAsync(client).RunSynchronously();

    public async Task GetAccountOwnerForAsync(Client client)
    {
      if (client == null)
        ;
      else
      {
        Client client1 = client;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        client1.AccountOwner = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == client.AccountOwnerId));
        client1 = (Client) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetUserFor(IEnumerable<Collaborator> collaborators) => this.GetUserForAsync(collaborators).RunSynchronously();

    public async Task GetUserForAsync(IEnumerable<Collaborator> collaborators)
    {
      if (collaborators == null)
        return;
      foreach (Collaborator collaborator in collaborators)
        await this.GetUserForAsync(collaborator);
    }

    public void GetUserFor(Collaborator collaborator) => this.GetUserForAsync(collaborator).RunSynchronously();

    public async Task GetUserForAsync(Collaborator collaborator)
    {
      if (collaborator == null)
        ;
      else
      {
        Collaborator collaborator1 = collaborator;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        collaborator1.User = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == collaborator.UserId));
        collaborator1 = (Collaborator) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetInvitedByFor(IEnumerable<Collaborator> invitedCollaborators) => this.GetInvitedByForAsync(invitedCollaborators).RunSynchronously();

    public async Task GetInvitedByForAsync(IEnumerable<Collaborator> invitedCollaborators)
    {
      if (invitedCollaborators == null)
        return;
      foreach (Collaborator collaborator in invitedCollaborators)
        await this.GetInvitedByForAsync(collaborator);
    }

    public void GetInvitedByFor(Collaborator collaborator) => this.GetInvitedByForAsync(collaborator).RunSynchronously();

    public async Task GetInvitedByForAsync(Collaborator collaborator)
    {
      if (collaborator == null)
        ;
      else
      {
        Collaborator collaborator1 = collaborator;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        collaborator1.InvitedBy = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == collaborator.InvitedById));
        collaborator1 = (Collaborator) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetSenderFor(IEnumerable<Comment> commentsSend) => this.GetSenderForAsync(commentsSend).RunSynchronously();

    public async Task GetSenderForAsync(IEnumerable<Comment> commentsSend)
    {
      if (commentsSend == null)
        return;
      foreach (Comment comment in commentsSend)
        await this.GetSenderForAsync(comment);
    }

    public void GetSenderFor(Comment comment) => this.GetSenderForAsync(comment).RunSynchronously();

    public async Task GetSenderForAsync(Comment comment)
    {
      if (comment == null)
        ;
      else
      {
        Comment comment1 = comment;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        comment1.Sender = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == comment.SenderId));
        comment1 = (Comment) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetUserFor(IEnumerable<Follow> follows) => this.GetUserForAsync(follows).RunSynchronously();

    public async Task GetUserForAsync(IEnumerable<Follow> follows)
    {
      if (follows == null)
        return;
      foreach (Follow follow in follows)
        await this.GetUserForAsync(follow);
    }

    public void GetUserFor(Follow follow) => this.GetUserForAsync(follow).RunSynchronously();

    public async Task GetUserForAsync(Follow follow)
    {
      if (follow == null)
        ;
      else
      {
        Follow follow1 = follow;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        follow1.User = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == follow.UserId));
        follow1 = (Follow) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetProcessOwnerFor(IEnumerable<Idea> ideas) => this.GetProcessOwnerForAsync(ideas).RunSynchronously();

    public async Task GetProcessOwnerForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetProcessOwnerForAsync(idea);
    }

    public void GetProcessOwnerFor(Idea idea) => this.GetProcessOwnerForAsync(idea).RunSynchronously();

    public async Task GetProcessOwnerForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        idea1.ProcessOwner = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == idea.ProcessOwnerId));
        idea1 = (Idea) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetUserFor(IEnumerable<ManageTenant> manageTenants) => this.GetUserForAsync(manageTenants).RunSynchronously();

    public async Task GetUserForAsync(IEnumerable<ManageTenant> manageTenants)
    {
      if (manageTenants == null)
        return;
      foreach (ManageTenant manageTenant in manageTenants)
        await this.GetUserForAsync(manageTenant);
    }

    public void GetUserFor(ManageTenant manageTenant) => this.GetUserForAsync(manageTenant).RunSynchronously();

    public async Task GetUserForAsync(ManageTenant manageTenant)
    {
      if (manageTenant == null)
        ;
      else
      {
        ManageTenant manageTenant1 = manageTenant;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        manageTenant1.User = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == manageTenant.UserId));
        manageTenant1 = (ManageTenant) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetUserFor(IEnumerable<Message> messages) => this.GetUserForAsync(messages).RunSynchronously();

    public async Task GetUserForAsync(IEnumerable<Message> messages)
    {
      if (messages == null)
        return;
      foreach (Message message in messages)
        await this.GetUserForAsync(message);
    }

    public void GetUserFor(Message message) => this.GetUserForAsync(message).RunSynchronously();

    public async Task GetUserForAsync(Message message)
    {
      if (message == null)
        ;
      else
      {
        Message message1 = message;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        message1.User = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == message.UserId));
        message1 = (Message) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetUserFor(IEnumerable<Recipient> recipients) => this.GetUserForAsync(recipients).RunSynchronously();

    public async Task GetUserForAsync(IEnumerable<Recipient> recipients)
    {
      if (recipients == null)
        return;
      foreach (Recipient recipient in recipients)
        await this.GetUserForAsync(recipient);
    }

    public void GetUserFor(Recipient recipient) => this.GetUserForAsync(recipient).RunSynchronously();

    public async Task GetUserForAsync(Recipient recipient)
    {
      if (recipient == null)
        ;
      else
      {
        Recipient recipient1 = recipient;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        recipient1.User = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == recipient.UserId));
        recipient1 = (Recipient) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetManagerFor(IEnumerable<User> teamMembers) => this.GetManagerForAsync(teamMembers).RunSynchronously();

    public async Task GetManagerForAsync(IEnumerable<User> teamMembers)
    {
      if (teamMembers == null)
        return;
      foreach (User user in teamMembers)
        await this.GetManagerForAsync(user);
    }

    public void GetManagerFor(User user) => this.GetManagerForAsync(user).RunSynchronously();

    public async Task GetManagerForAsync(User user)
    {
      if (user == null)
        ;
      else
      {
        User user1 = user;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        user1.Manager = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == user.ManagerId));
        user1 = (User) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetUserFor(IEnumerable<UserAchievement> userAchievements) => this.GetUserForAsync(userAchievements).RunSynchronously();

    public async Task GetUserForAsync(IEnumerable<UserAchievement> userAchievements)
    {
      if (userAchievements == null)
        return;
      foreach (UserAchievement userAchievement in userAchievements)
        await this.GetUserForAsync(userAchievement);
    }

    public void GetUserFor(UserAchievement userAchievement) => this.GetUserForAsync(userAchievement).RunSynchronously();

    public async Task GetUserForAsync(UserAchievement userAchievement)
    {
      if (userAchievement == null)
        ;
      else
      {
        UserAchievement userAchievement1 = userAchievement;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        userAchievement1.User = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == userAchievement.UserId));
        userAchievement1 = (UserAchievement) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetUserFor(IEnumerable<UserAuthorisation> userAuthorisations) => this.GetUserForAsync(userAuthorisations).RunSynchronously();

    public async Task GetUserForAsync(IEnumerable<UserAuthorisation> userAuthorisations)
    {
      if (userAuthorisations == null)
        return;
      foreach (UserAuthorisation userAuthorisation in userAuthorisations)
        await this.GetUserForAsync(userAuthorisation);
    }

    public void GetUserFor(UserAuthorisation userAuthorisation) => this.GetUserForAsync(userAuthorisation).RunSynchronously();

    public async Task GetUserForAsync(UserAuthorisation userAuthorisation)
    {
      if (userAuthorisation == null)
        ;
      else
      {
        UserAuthorisation userAuthorisation1 = userAuthorisation;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        userAuthorisation1.User = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == userAuthorisation.UserId));
        userAuthorisation1 = (UserAuthorisation) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetUserFor(IEnumerable<UserBadge> userBadges) => this.GetUserForAsync(userBadges).RunSynchronously();

    public async Task GetUserForAsync(IEnumerable<UserBadge> userBadges)
    {
      if (userBadges == null)
        return;
      foreach (UserBadge userBadge in userBadges)
        await this.GetUserForAsync(userBadge);
    }

    public void GetUserFor(UserBadge userBadge) => this.GetUserForAsync(userBadge).RunSynchronously();

    public async Task GetUserForAsync(UserBadge userBadge)
    {
      if (userBadge == null)
        ;
      else
      {
        UserBadge userBadge1 = userBadge;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        userBadge1.User = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == userBadge.UserId));
        userBadge1 = (UserBadge) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetUserFor(IEnumerable<UserRole> userRoles) => this.GetUserForAsync(userRoles).RunSynchronously();

    public async Task GetUserForAsync(IEnumerable<UserRole> userRoles)
    {
      if (userRoles == null)
        return;
      foreach (UserRole userRole in userRoles)
        await this.GetUserForAsync(userRole);
    }

    public void GetUserFor(UserRole userRole) => this.GetUserForAsync(userRole).RunSynchronously();

    public async Task GetUserForAsync(UserRole userRole)
    {
      if (userRole == null)
        ;
      else
      {
        UserRole userRole1 = userRole;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        userRole1.User = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == userRole.UserId));
        userRole1 = (UserRole) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetUserFor(IEnumerable<Vote> votes) => this.GetUserForAsync(votes).RunSynchronously();

    public async Task GetUserForAsync(IEnumerable<Vote> votes)
    {
      if (votes == null)
        return;
      foreach (Vote vote in votes)
        await this.GetUserForAsync(vote);
    }

    public void GetUserFor(Vote vote) => this.GetUserForAsync(vote).RunSynchronously();

    public async Task GetUserForAsync(Vote vote)
    {
      if (vote == null)
        ;
      else
      {
        Vote vote1 = vote;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        vote1.User = dataSet.Users.SingleOrDefault<User>((Func<User, bool>) (x => x.Id == vote.UserId));
        vote1 = (Vote) null;
        //dataSet = (DataSet) null;
      }
    }

    public User GetByEmail(string email) => this.GetByEmailAsync(email).Result;

    public async Task<User> GetByEmailAsync(string email)
    {
      if (string.IsNullOrEmpty(email))
        return (User) null;
      var dataSet = await _unitOfWork.GetDataSetAsync();
      if (dataSet == null)
      {
        //dataSet = (DataSet) null;
        return (User) null;
      }
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.Users.SingleOrDefault<User>((Func<User, bool>) (x => string.Equals(x.Email, email, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      User entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(User entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(User entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<User> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<User> entities)
    {
      if (entities == null)
        throw new DuplicateException("The users are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
