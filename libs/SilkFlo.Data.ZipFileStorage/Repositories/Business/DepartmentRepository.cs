// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.DepartmentRepository
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
  public class DepartmentRepository : IDepartmentRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public DepartmentRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Department Get(string id) => this.GetAsync(id).Result;

    public async Task<Department> GetAsync(string id)
    {
      if (id == null)
        return (Department) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessDepartments.SingleOrDefault<Department>((Func<Department, bool>) (x => x.Id == id));
    }

    public Department SingleOrDefault(Func<Department, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Department> SingleOrDefaultAsync(Func<Department, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessDepartments.Where<Department>(predicate).FirstOrDefault<Department>();
    }

    public bool Add(Department entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Department entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Department> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Department> entities)
    {
      if (entities == null)
        return false;
      foreach (Department entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Department> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Department>) dataSetAsync.BusinessDepartments.OrderBy<Department, string>((Func<Department, string>) (m => m.Name));
    }

    public IEnumerable<Department> Find(Func<Department, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Department>> FindAsync(Func<Department, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Department>) dataSetAsync.BusinessDepartments.Where<Department>(predicate).OrderBy<Department, string>((Func<Department, string>) (m => m.Name));
    }

    public Department GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Department> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Department) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessDepartments.SingleOrDefault<Department>((Func<Department, bool>) (x => x.Name == name));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<Department> lst;
      if (client == null)
        lst = (List<Department>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<Department>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessDepartments.Where<Department>((Func<Department, bool>) (x => x.ClientId == client.Id)).OrderBy<Department, string>((Func<Department, string>) (x => x.Name)).ToList<Department>();
        dataSet = (DataSet) null;
        foreach (Department item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Departments = lst;
        lst = (List<Department>) null;
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

    public void GetDepartmentFor(IEnumerable<Idea> ideas) => this.GetDepartmentForAsync(ideas).RunSynchronously();

    public async Task GetDepartmentForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetDepartmentForAsync(idea);
    }

    public void GetDepartmentFor(Idea idea) => this.GetDepartmentForAsync(idea).RunSynchronously();

    public async Task GetDepartmentForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        idea1.Department = dataSet.BusinessDepartments.SingleOrDefault<Department>((Func<Department, bool>) (x => x.Id == idea.DepartmentId));
        idea1 = (Idea) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetDepartmentFor(IEnumerable<Team> teams) => this.GetDepartmentForAsync(teams).RunSynchronously();

    public async Task GetDepartmentForAsync(IEnumerable<Team> teams)
    {
      if (teams == null)
        return;
      foreach (Team team in teams)
        await this.GetDepartmentForAsync(team);
    }

    public void GetDepartmentFor(Team team) => this.GetDepartmentForAsync(team).RunSynchronously();

    public async Task GetDepartmentForAsync(Team team)
    {
      if (team == null)
        ;
      else
      {
        Team team1 = team;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        team1.Department = dataSet.BusinessDepartments.SingleOrDefault<Department>((Func<Department, bool>) (x => x.Id == team.DepartmentId));
        team1 = (Team) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetDepartmentFor(IEnumerable<User> users) => this.GetDepartmentForAsync(users).RunSynchronously();

    public async Task GetDepartmentForAsync(IEnumerable<User> users)
    {
      if (users == null)
        return;
      foreach (User user in users)
        await this.GetDepartmentForAsync(user);
    }

    public void GetDepartmentFor(User user) => this.GetDepartmentForAsync(user).RunSynchronously();

    public async Task GetDepartmentForAsync(User user)
    {
      if (user == null)
        ;
      else
      {
        User user1 = user;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        user1.Department = dataSet.BusinessDepartments.SingleOrDefault<Department>((Func<Department, bool>) (x => x.Id == user.DepartmentId));
        user1 = (User) null;
        dataSet = (DataSet) null;
      }
    }

    public Department GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Department> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Department) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessDepartments.SingleOrDefault<Department>((Func<Department, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Department entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Department entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Department entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Department> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Department> entities)
    {
      if (entities == null)
        throw new DuplicateException("The departments are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
