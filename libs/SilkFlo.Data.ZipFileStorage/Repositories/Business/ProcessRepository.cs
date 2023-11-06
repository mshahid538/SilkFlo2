// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.ProcessRepository
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
  public class ProcessRepository : IProcessRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public ProcessRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Process Get(string id) => this.GetAsync(id).Result;

    public async Task<Process> GetAsync(string id)
    {
      if (id == null)
        return (Process) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessProcesses.SingleOrDefault<Process>((Func<Process, bool>) (x => x.Id == id));
    }

    public Process SingleOrDefault(Func<Process, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Process> SingleOrDefaultAsync(Func<Process, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessProcesses.Where<Process>(predicate).FirstOrDefault<Process>();
    }

    public bool Add(Process entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Process entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Process> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Process> entities)
    {
      if (entities == null)
        return false;
      foreach (Process entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Process> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Process>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Process>) dataSetAsync.BusinessProcesses.OrderBy<Process, string>((Func<Process, string>) (m => m.Name));
    }

    public IEnumerable<Process> Find(Func<Process, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Process>> FindAsync(Func<Process, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Process>) dataSetAsync.BusinessProcesses.Where<Process>(predicate).OrderBy<Process, string>((Func<Process, string>) (m => m.Name));
    }

    public Process GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Process> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Process) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessProcesses.SingleOrDefault<Process>((Func<Process, bool>) (x => x.Name == name));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<Process> lst;
      if (client == null)
        lst = (List<Process>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<Process>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessProcesses.Where<Process>((Func<Process, bool>) (x => x.ClientId == client.Id)).OrderBy<Process, string>((Func<Process, string>) (x => x.Name)).ToList<Process>();
        //dataSet = (DataSet) null;
        foreach (Process item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Processes = lst;
        lst = (List<Process>) null;
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

    public void GetForTeam(Team team) => this.GetForTeamAsync(team).RunSynchronously();

    public async Task GetForTeamAsync(Team team)
    {
      List<Process> lst;
      if (team == null)
        lst = (List<Process>) null;
      else if (string.IsNullOrWhiteSpace(team.Id))
      {
        lst = (List<Process>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessProcesses.Where<Process>((Func<Process, bool>) (x => x.TeamId == team.Id)).OrderBy<Process, string>((Func<Process, string>) (x => x.Name)).ToList<Process>();
        //dataSet = (DataSet) null;
        foreach (Process item in lst)
        {
          item.TeamId = team.Id;
          item.Team = team;
        }
        team.Processes = lst;
        lst = (List<Process>) null;
      }
    }

    public void GetForTeam(IEnumerable<Team> teams) => this.GetForTeamAsync(teams).RunSynchronously();

    public async Task GetForTeamAsync(IEnumerable<Team> teams)
    {
      if (teams == null)
        return;
      foreach (Team team in teams)
        await this.GetForTeamAsync(team);
    }

    public void GetProcessFor(IEnumerable<Idea> ideas) => this.GetProcessForAsync(ideas).RunSynchronously();

    public async Task GetProcessForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetProcessForAsync(idea);
    }

    public void GetProcessFor(Idea idea) => this.GetProcessForAsync(idea).RunSynchronously();

    public async Task GetProcessForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        idea1.Process = dataSet.BusinessProcesses.SingleOrDefault<Process>((Func<Process, bool>) (x => x.Id == idea.ProcessId));
        idea1 = (Idea) null;
        //dataSet = (DataSet) null;
      }
    }

    public Process GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Process> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Process) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessProcesses.SingleOrDefault<Process>((Func<Process, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Process entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Process entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Process entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Process> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Process> entities)
    {
      if (entities == null)
        throw new DuplicateException("The processes are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
