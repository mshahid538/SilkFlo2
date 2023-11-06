// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.RunningCostRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Repositories.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class RunningCostRepository : IRunningCostRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public RunningCostRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public RunningCost Get(string id) => this.GetAsync(id).Result;

    public async Task<RunningCost> GetAsync(string id)
    {
      if (id == null)
        return (RunningCost) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRunningCosts.SingleOrDefault<RunningCost>((Func<RunningCost, bool>) (x => x.Id == id));
    }

    public RunningCost SingleOrDefault(Func<RunningCost, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<RunningCost> SingleOrDefaultAsync(Func<RunningCost, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRunningCosts.Where<RunningCost>(predicate).FirstOrDefault<RunningCost>();
    }

    public bool Add(RunningCost entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(RunningCost entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<RunningCost> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<RunningCost> entities)
    {
      if (entities == null)
        return false;
      foreach (RunningCost entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<RunningCost> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<RunningCost>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<RunningCost>) dataSetAsync.BusinessRunningCosts;
    }

    public IEnumerable<RunningCost> Find(Func<RunningCost, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<RunningCost>> FindAsync(Func<RunningCost, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRunningCosts.Where<RunningCost>(predicate);
    }

    public void GetForAutomationType(AutomationType automationType) => this.GetForAutomationTypeAsync(automationType).RunSynchronously();

    public async Task GetForAutomationTypeAsync(AutomationType automationType)
    {
      List<RunningCost> lst;
      if (automationType == null)
        lst = (List<RunningCost>) null;
      else if (string.IsNullOrWhiteSpace(automationType.Id))
      {
        lst = (List<RunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessRunningCosts.Where<RunningCost>((Func<RunningCost, bool>) (x => x.AutomationTypeId == automationType.Id)).ToList<RunningCost>();
        //dataSet = (DataSet) null;
        foreach (RunningCost item in lst)
        {
          item.AutomationTypeId = automationType.Id;
          item.AutomationType = automationType;
        }
        automationType.RunningCosts = lst;
        lst = (List<RunningCost>) null;
      }
    }

    public void GetForAutomationType(IEnumerable<AutomationType> automationTypes) => this.GetForAutomationTypeAsync(automationTypes).RunSynchronously();

    public async Task GetForAutomationTypeAsync(IEnumerable<AutomationType> automationTypes)
    {
      if (automationTypes == null)
        return;
      foreach (AutomationType automationType in automationTypes)
        await this.GetForAutomationTypeAsync(automationType);
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<RunningCost> lst;
      if (client == null)
        lst = (List<RunningCost>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<RunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessRunningCosts.Where<RunningCost>((Func<RunningCost, bool>) (x => x.ClientId == client.Id)).ToList<RunningCost>();
        //dataSet = (DataSet) null;
        foreach (RunningCost item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.RunningCosts = lst;
        lst = (List<RunningCost>) null;
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

    public void GetForFrequency(Period frequency) => this.GetForFrequencyAsync(frequency).RunSynchronously();

    public async Task GetForFrequencyAsync(Period frequency)
    {
      List<RunningCost> lst;
      if (frequency == null)
        lst = (List<RunningCost>) null;
      else if (string.IsNullOrWhiteSpace(frequency.Id))
      {
        lst = (List<RunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessRunningCosts.Where<RunningCost>((Func<RunningCost, bool>) (x => x.FrequencyId == frequency.Id)).ToList<RunningCost>();
        //dataSet = (DataSet) null;
        foreach (RunningCost item in lst)
        {
          item.FrequencyId = frequency.Id;
          item.Frequency = frequency;
        }
        frequency.RunningCosts = lst;
        lst = (List<RunningCost>) null;
      }
    }

    public void GetForFrequency(IEnumerable<Period> frequencies) => this.GetForFrequencyAsync(frequencies).RunSynchronously();

    public async Task GetForFrequencyAsync(IEnumerable<Period> frequencies)
    {
      if (frequencies == null)
        return;
      foreach (Period frequency in frequencies)
        await this.GetForFrequencyAsync(frequency);
    }

    public void GetForVender(SoftwareVender vender) => this.GetForVenderAsync(vender).RunSynchronously();

    public async Task GetForVenderAsync(SoftwareVender vender)
    {
      List<RunningCost> lst;
      if (vender == null)
        lst = (List<RunningCost>) null;
      else if (string.IsNullOrWhiteSpace(vender.Id))
      {
        lst = (List<RunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessRunningCosts.Where<RunningCost>((Func<RunningCost, bool>) (x => x.VenderId == vender.Id)).ToList<RunningCost>();
        //dataSet = (DataSet) null;
        foreach (RunningCost item in lst)
        {
          item.VenderId = vender.Id;
          item.Vender = vender;
        }
        vender.RunningCosts = lst;
        lst = (List<RunningCost>) null;
      }
    }

    public void GetForVender(IEnumerable<SoftwareVender> venders) => this.GetForVenderAsync(venders).RunSynchronously();

    public async Task GetForVenderAsync(IEnumerable<SoftwareVender> venders)
    {
      if (venders == null)
        return;
      foreach (SoftwareVender vender in venders)
        await this.GetForVenderAsync(vender);
    }

    public void GetRunningCostFor(IEnumerable<Idea> ideas) => this.GetRunningCostForAsync(ideas).RunSynchronously();

    public async Task GetRunningCostForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetRunningCostForAsync(idea);
    }

    public void GetRunningCostFor(Idea idea) => this.GetRunningCostForAsync(idea).RunSynchronously();

    public async Task GetRunningCostForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        idea1.RunningCost = dataSet.BusinessRunningCosts.SingleOrDefault<RunningCost>((Func<RunningCost, bool>) (x => x.Id == idea.RunningCostId));
        idea1 = (Idea) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetRunningCostFor(IEnumerable<IdeaRunningCost> ideaRunningCosts) => this.GetRunningCostForAsync(ideaRunningCosts).RunSynchronously();

    public async Task GetRunningCostForAsync(IEnumerable<IdeaRunningCost> ideaRunningCosts)
    {
      if (ideaRunningCosts == null)
        return;
      foreach (IdeaRunningCost ideaRunningCost in ideaRunningCosts)
        await this.GetRunningCostForAsync(ideaRunningCost);
    }

    public void GetRunningCostFor(IdeaRunningCost ideaRunningCost) => this.GetRunningCostForAsync(ideaRunningCost).RunSynchronously();

    public async Task GetRunningCostForAsync(IdeaRunningCost ideaRunningCost)
    {
      if (ideaRunningCost == null)
        ;
      else
      {
        IdeaRunningCost ideaRunningCost1 = ideaRunningCost;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        ideaRunningCost1.RunningCost = dataSet.BusinessRunningCosts.SingleOrDefault<RunningCost>((Func<RunningCost, bool>) (x => x.Id == ideaRunningCost.RunningCostId));
        ideaRunningCost1 = (IdeaRunningCost) null;
        //dataSet = (DataSet) null;
      }
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      RunningCost entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(RunningCost entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(RunningCost entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<RunningCost> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<RunningCost> entities)
    {
      if (entities == null)
        throw new DuplicateException("The runningCosts are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
