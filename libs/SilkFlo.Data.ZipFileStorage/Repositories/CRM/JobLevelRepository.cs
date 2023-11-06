// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.CRM.JobLevelRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Repositories.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.CRM
{
  public class JobLevelRepository : IJobLevelRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public JobLevelRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public JobLevel Get(string id) => this.GetAsync(id).Result;

    public async Task<JobLevel> GetAsync(string id)
    {
      if (id == null)
        return (JobLevel) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.CRMJobLevels.SingleOrDefault<JobLevel>((Func<JobLevel, bool>) (x => x.Id == id));
    }

    public JobLevel SingleOrDefault(Func<JobLevel, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<JobLevel> SingleOrDefaultAsync(Func<JobLevel, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.CRMJobLevels.Where<JobLevel>(predicate).FirstOrDefault<JobLevel>();
    }

    public bool Add(JobLevel entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(JobLevel entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<JobLevel> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<JobLevel> entities)
    {
      if (entities == null)
        return false;
      foreach (JobLevel entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<JobLevel> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<JobLevel>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<JobLevel>) dataSetAsync.CRMJobLevels.OrderBy<JobLevel, int>((Func<JobLevel, int>) (m => m.Sort)).ThenBy<JobLevel, string>((Func<JobLevel, string>) (m => m.Name));
    }

    public IEnumerable<JobLevel> Find(Func<JobLevel, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<JobLevel>> FindAsync(Func<JobLevel, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<JobLevel>) dataSetAsync.CRMJobLevels.Where<JobLevel>(predicate).OrderBy<JobLevel, int>((Func<JobLevel, int>) (m => m.Sort)).ThenBy<JobLevel, string>((Func<JobLevel, string>) (m => m.Name));
    }

    public JobLevel GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<JobLevel> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (JobLevel) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.CRMJobLevels.SingleOrDefault<JobLevel>((Func<JobLevel, bool>) (x => x.Name == name));
    }

    public void GetJobLevelFor(IEnumerable<Prospect> teamMembers) => this.GetJobLevelForAsync(teamMembers).RunSynchronously();

    public async Task GetJobLevelForAsync(IEnumerable<Prospect> teamMembers)
    {
      if (teamMembers == null)
        return;
      foreach (Prospect prospect in teamMembers)
        await this.GetJobLevelForAsync(prospect);
    }

    public void GetJobLevelFor(Prospect prospect) => this.GetJobLevelForAsync(prospect).RunSynchronously();

    public async Task GetJobLevelForAsync(Prospect prospect)
    {
      if (prospect == null)
        ;
      else
      {
        Prospect prospect1 = prospect;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        prospect1.JobLevel = dataSet.CRMJobLevels.SingleOrDefault<JobLevel>((Func<JobLevel, bool>) (x => x.Id == prospect.JobLevelId));
        prospect1 = (Prospect) null;
        //dataSet = (DataSet) null;
      }
    }

    public JobLevel GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<JobLevel> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (JobLevel) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.CRMJobLevels.SingleOrDefault<JobLevel>((Func<JobLevel, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      JobLevel entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(JobLevel entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(JobLevel entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<JobLevel> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<JobLevel> entities)
    {
      if (entities == null)
        throw new DuplicateException("The jobLevels are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
