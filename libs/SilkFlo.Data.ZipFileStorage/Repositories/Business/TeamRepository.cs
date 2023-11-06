// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.TeamRepository
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
  public class TeamRepository : ITeamRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public TeamRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Team Get(string id) => this.GetAsync(id).Result;

    public async Task<Team> GetAsync(string id)
    {
      if (id == null)
        return (Team) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessTeams.SingleOrDefault<Team>((Func<Team, bool>) (x => x.Id == id));
    }

    public Team SingleOrDefault(Func<Team, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Team> SingleOrDefaultAsync(Func<Team, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessTeams.Where<Team>(predicate).FirstOrDefault<Team>();
    }

    public bool Add(Team entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Team entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Team> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Team> entities)
    {
      if (entities == null)
        return false;
      foreach (Team entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Team> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Team>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Team>) dataSetAsync.BusinessTeams.OrderBy<Team, string>((Func<Team, string>) (m => m.Name));
    }

    public IEnumerable<Team> Find(Func<Team, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Team>> FindAsync(Func<Team, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Team>) dataSetAsync.BusinessTeams.Where<Team>(predicate).OrderBy<Team, string>((Func<Team, string>) (m => m.Name));
    }

    public Team GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Team> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Team) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessTeams.SingleOrDefault<Team>((Func<Team, bool>) (x => x.Name == name));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<Team> lst;
      if (client == null)
        lst = (List<Team>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<Team>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessTeams.Where<Team>((Func<Team, bool>) (x => x.ClientId == client.Id)).OrderBy<Team, string>((Func<Team, string>) (x => x.Name)).ToList<Team>();
        //dataSet = (DataSet) null;
        foreach (Team item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Teams = lst;
        lst = (List<Team>) null;
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
      List<Team> lst;
      if (department == null)
        lst = (List<Team>) null;
      else if (string.IsNullOrWhiteSpace(department.Id))
      {
        lst = (List<Team>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessTeams.Where<Team>((Func<Team, bool>) (x => x.DepartmentId == department.Id)).OrderBy<Team, string>((Func<Team, string>) (x => x.Name)).ToList<Team>();
        //dataSet = (DataSet) null;
        foreach (Team item in lst)
        {
          item.DepartmentId = department.Id;
          item.Department = department;
        }
        department.Teams = lst;
        lst = (List<Team>) null;
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

    public void GetTeamFor(IEnumerable<Idea> ideas) => this.GetTeamForAsync(ideas).RunSynchronously();

    public async Task GetTeamForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetTeamForAsync(idea);
    }

    public void GetTeamFor(Idea idea) => this.GetTeamForAsync(idea).RunSynchronously();

    public async Task GetTeamForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        idea1.Team = dataSet.BusinessTeams.SingleOrDefault<Team>((Func<Team, bool>) (x => x.Id == idea.TeamId));
        idea1 = (Idea) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetTeamFor(IEnumerable<Process> processes) => this.GetTeamForAsync(processes).RunSynchronously();

    public async Task GetTeamForAsync(IEnumerable<Process> processes)
    {
      if (processes == null)
        return;
      foreach (Process process in processes)
        await this.GetTeamForAsync(process);
    }

    public void GetTeamFor(Process process) => this.GetTeamForAsync(process).RunSynchronously();

    public async Task GetTeamForAsync(Process process)
    {
      if (process == null)
        ;
      else
      {
        Process process1 = process;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        process1.Team = dataSet.BusinessTeams.SingleOrDefault<Team>((Func<Team, bool>) (x => x.Id == process.TeamId));
        process1 = (Process) null;
        //dataSet = (DataSet) null;
      }
    }

    public Team GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Team> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Team) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessTeams.SingleOrDefault<Team>((Func<Team, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Team entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Team entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Team entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Team> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Team> entities)
    {
      if (entities == null)
        throw new DuplicateException("The teams are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
