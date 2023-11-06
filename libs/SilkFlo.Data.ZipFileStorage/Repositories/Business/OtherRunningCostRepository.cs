// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.OtherRunningCostRepository
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
  public class OtherRunningCostRepository : IOtherRunningCostRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public OtherRunningCostRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public OtherRunningCost Get(string id) => this.GetAsync(id).Result;

    public async Task<OtherRunningCost> GetAsync(string id)
    {
      if (id == null)
        return (OtherRunningCost) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessOtherRunningCosts.SingleOrDefault<OtherRunningCost>((Func<OtherRunningCost, bool>) (x => x.Id == id));
    }

    public OtherRunningCost SingleOrDefault(Func<OtherRunningCost, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<OtherRunningCost> SingleOrDefaultAsync(Func<OtherRunningCost, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessOtherRunningCosts.Where<OtherRunningCost>(predicate).FirstOrDefault<OtherRunningCost>();
    }

    public bool Add(OtherRunningCost entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(OtherRunningCost entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<OtherRunningCost> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<OtherRunningCost> entities)
    {
      if (entities == null)
        return false;
      foreach (OtherRunningCost entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<OtherRunningCost> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<OtherRunningCost>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<OtherRunningCost>) dataSetAsync.BusinessOtherRunningCosts;
    }

    public IEnumerable<OtherRunningCost> Find(Func<OtherRunningCost, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<OtherRunningCost>> FindAsync(
      Func<OtherRunningCost, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessOtherRunningCosts.Where<OtherRunningCost>(predicate);
    }

    public OtherRunningCost GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<OtherRunningCost> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (OtherRunningCost) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessOtherRunningCosts.SingleOrDefault<OtherRunningCost>((Func<OtherRunningCost, bool>) (x => x.Name == name));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<OtherRunningCost> lst;
      if (client == null)
        lst = (List<OtherRunningCost>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<OtherRunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessOtherRunningCosts.Where<OtherRunningCost>((Func<OtherRunningCost, bool>) (x => x.ClientId == client.Id)).ToList<OtherRunningCost>();
        //dataSet = (DataSet) null;
        foreach (OtherRunningCost item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.OtherRunningCosts = lst;
        lst = (List<OtherRunningCost>) null;
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

    public void GetForCostType(CostType costType) => this.GetForCostTypeAsync(costType).RunSynchronously();

    public async Task GetForCostTypeAsync(CostType costType)
    {
      List<OtherRunningCost> lst;
      if (costType == null)
        lst = (List<OtherRunningCost>) null;
      else if (string.IsNullOrWhiteSpace(costType.Id))
      {
        lst = (List<OtherRunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessOtherRunningCosts.Where<OtherRunningCost>((Func<OtherRunningCost, bool>) (x => x.CostTypeId == costType.Id)).ToList<OtherRunningCost>();
        //dataSet = (DataSet) null;
        foreach (OtherRunningCost item in lst)
        {
          item.CostTypeId = costType.Id;
          item.CostType = costType;
        }
        costType.OtherRunningCosts = lst;
        lst = (List<OtherRunningCost>) null;
      }
    }

    public void GetForCostType(IEnumerable<CostType> costTypes) => this.GetForCostTypeAsync(costTypes).RunSynchronously();

    public async Task GetForCostTypeAsync(IEnumerable<CostType> costTypes)
    {
      if (costTypes == null)
        return;
      foreach (CostType costType in costTypes)
        await this.GetForCostTypeAsync(costType);
    }

    public void GetForFrequency(Period frequency) => this.GetForFrequencyAsync(frequency).RunSynchronously();

    public async Task GetForFrequencyAsync(Period frequency)
    {
      List<OtherRunningCost> lst;
      if (frequency == null)
        lst = (List<OtherRunningCost>) null;
      else if (string.IsNullOrWhiteSpace(frequency.Id))
      {
        lst = (List<OtherRunningCost>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessOtherRunningCosts.Where<OtherRunningCost>((Func<OtherRunningCost, bool>) (x => x.FrequencyId == frequency.Id)).ToList<OtherRunningCost>();
        //dataSet = (DataSet) null;
        foreach (OtherRunningCost item in lst)
        {
          item.FrequencyId = frequency.Id;
          item.Frequency = frequency;
        }
        frequency.OtherRunningCosts = lst;
        lst = (List<OtherRunningCost>) null;
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

    public void GetOtherRunningCostFor(
      IEnumerable<IdeaOtherRunningCost> ideaOtherRunningCosts)
    {
      this.GetOtherRunningCostForAsync(ideaOtherRunningCosts).RunSynchronously();
    }

    public async Task GetOtherRunningCostForAsync(
      IEnumerable<IdeaOtherRunningCost> ideaOtherRunningCosts)
    {
      if (ideaOtherRunningCosts == null)
        return;
      foreach (IdeaOtherRunningCost ideaOtherRunningCost in ideaOtherRunningCosts)
        await this.GetOtherRunningCostForAsync(ideaOtherRunningCost);
    }

    public void GetOtherRunningCostFor(IdeaOtherRunningCost ideaOtherRunningCost) => this.GetOtherRunningCostForAsync(ideaOtherRunningCost).RunSynchronously();

    public async Task GetOtherRunningCostForAsync(IdeaOtherRunningCost ideaOtherRunningCost)
    {
      if (ideaOtherRunningCost == null)
        ;
      else
      {
        IdeaOtherRunningCost otherRunningCost = ideaOtherRunningCost;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        otherRunningCost.OtherRunningCost = dataSet.BusinessOtherRunningCosts.SingleOrDefault<OtherRunningCost>((Func<OtherRunningCost, bool>) (x => x.Id == ideaOtherRunningCost.OtherRunningCostId));
        otherRunningCost = (IdeaOtherRunningCost) null;
        //dataSet = (DataSet) null;
      }
    }

    public OtherRunningCost GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<OtherRunningCost> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (OtherRunningCost) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessOtherRunningCosts.SingleOrDefault<OtherRunningCost>((Func<OtherRunningCost, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      OtherRunningCost entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(OtherRunningCost entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(OtherRunningCost entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<OtherRunningCost> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<OtherRunningCost> entities)
    {
      if (entities == null)
        throw new DuplicateException("The otherRunningCosts are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
