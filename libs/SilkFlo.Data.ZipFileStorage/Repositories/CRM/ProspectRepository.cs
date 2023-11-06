// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.CRM.ProspectRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Repositories.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.CRM
{
  public class ProspectRepository : IProspectRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public ProspectRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Prospect Get(string id) => this.GetAsync(id).Result;

    public async Task<Prospect> GetAsync(string id)
    {
      if (id == null)
        return (Prospect) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.CRMProspects.SingleOrDefault<Prospect>((Func<Prospect, bool>) (x => x.Id == id));
    }

    public Prospect SingleOrDefault(Func<Prospect, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Prospect> SingleOrDefaultAsync(Func<Prospect, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.CRMProspects.Where<Prospect>(predicate).FirstOrDefault<Prospect>();
    }

    public bool Add(Prospect entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Prospect entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Prospect> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Prospect> entities)
    {
      if (entities == null)
        return false;
      foreach (Prospect entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Prospect> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Prospect>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Prospect>) dataSetAsync.CRMProspects.OrderBy<Prospect, string>((Func<Prospect, string>) (m => m.CompanyName)).ThenBy<Prospect, string>((Func<Prospect, string>) (m => m.FirstName)).ThenBy<Prospect, string>((Func<Prospect, string>) (m => m.LastName));
    }

    public IEnumerable<Prospect> Find(Func<Prospect, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Prospect>> FindAsync(Func<Prospect, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<Prospect>) dataSetAsync.CRMProspects.Where<Prospect>(predicate).OrderBy<Prospect, string>((Func<Prospect, string>) (m => m.CompanyName)).ThenBy<Prospect, string>((Func<Prospect, string>) (m => m.FirstName)).ThenBy<Prospect, string>((Func<Prospect, string>) (m => m.LastName));
    }

    public Prospect GetUsingEmail(string email) => this.GetUsingEmailAsync(email).Result;

    public async Task<Prospect> GetUsingEmailAsync(string email)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.CRMProspects.SingleOrDefault<Prospect>((Func<Prospect, bool>) (x => x.Email.ToLower() == email.ToLower()));
    }

    public void GetForClientType(ClientType clientType) => this.GetForClientTypeAsync(clientType).RunSynchronously();

    public async Task GetForClientTypeAsync(ClientType clientType)
    {
      List<Prospect> lst;
      if (clientType == null)
        lst = (List<Prospect>) null;
      else if (string.IsNullOrWhiteSpace(clientType.Id))
      {
        lst = (List<Prospect>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.CRMProspects.Where<Prospect>((Func<Prospect, bool>) (x => x.ClientTypeId == clientType.Id)).OrderBy<Prospect, string>((Func<Prospect, string>) (x => x.CompanyName)).ThenBy<Prospect, string>((Func<Prospect, string>) (x => x.FirstName)).ThenBy<Prospect, string>((Func<Prospect, string>) (x => x.LastName)).ToList<Prospect>();
        //dataSet = (DataSet) null;
        foreach (Prospect item in lst)
        {
          item.ClientTypeId = clientType.Id;
          item.ClientType = clientType;
        }
        clientType.Prospects = lst;
        lst = (List<Prospect>) null;
      }
    }

    public void GetForClientType(IEnumerable<ClientType> clientTypes) => this.GetForClientTypeAsync(clientTypes).RunSynchronously();

    public async Task GetForClientTypeAsync(IEnumerable<ClientType> clientTypes)
    {
      if (clientTypes == null)
        return;
      foreach (ClientType clientType in clientTypes)
        await this.GetForClientTypeAsync(clientType);
    }

    public void GetForCompanySize(CompanySize companySize) => this.GetForCompanySizeAsync(companySize).RunSynchronously();

    public async Task GetForCompanySizeAsync(CompanySize companySize)
    {
      List<Prospect> lst;
      if (companySize == null)
        lst = (List<Prospect>) null;
      else if (string.IsNullOrWhiteSpace(companySize.Id))
      {
        lst = (List<Prospect>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.CRMProspects.Where<Prospect>((Func<Prospect, bool>) (x => x.CompanySizeId == companySize.Id)).OrderBy<Prospect, string>((Func<Prospect, string>) (x => x.CompanyName)).ThenBy<Prospect, string>((Func<Prospect, string>) (x => x.FirstName)).ThenBy<Prospect, string>((Func<Prospect, string>) (x => x.LastName)).ToList<Prospect>();
        //dataSet = (DataSet) null;
        foreach (Prospect item in lst)
        {
          item.CompanySizeId = companySize.Id;
          item.CompanySize = companySize;
        }
        companySize.Prospects = lst;
        lst = (List<Prospect>) null;
      }
    }

    public void GetForCompanySize(IEnumerable<CompanySize> companySizes) => this.GetForCompanySizeAsync(companySizes).RunSynchronously();

    public async Task GetForCompanySizeAsync(IEnumerable<CompanySize> companySizes)
    {
      if (companySizes == null)
        return;
      foreach (CompanySize companySize in companySizes)
        await this.GetForCompanySizeAsync(companySize);
    }

    public void GetForCountry(Country country) => this.GetForCountryAsync(country).RunSynchronously();

    public async Task GetForCountryAsync(Country country)
    {
      List<Prospect> lst;
      if (country == null)
        lst = (List<Prospect>) null;
      else if (string.IsNullOrWhiteSpace(country.Id))
      {
        lst = (List<Prospect>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.CRMProspects.Where<Prospect>((Func<Prospect, bool>) (x => x.CountryId == country.Id)).OrderBy<Prospect, string>((Func<Prospect, string>) (x => x.CompanyName)).ThenBy<Prospect, string>((Func<Prospect, string>) (x => x.FirstName)).ThenBy<Prospect, string>((Func<Prospect, string>) (x => x.LastName)).ToList<Prospect>();
        //dataSet = (DataSet) null;
        foreach (Prospect item in lst)
        {
          item.CountryId = country.Id;
          item.Country = country;
        }
        country.Prospects = lst;
        lst = (List<Prospect>) null;
      }
    }

    public void GetForCountry(IEnumerable<Country> countries) => this.GetForCountryAsync(countries).RunSynchronously();

    public async Task GetForCountryAsync(IEnumerable<Country> countries)
    {
      if (countries == null)
        return;
      foreach (Country country in countries)
        await this.GetForCountryAsync(country);
    }

    public void GetForJobLevel(JobLevel jobLevel) => this.GetForJobLevelAsync(jobLevel).RunSynchronously();

    public async Task GetForJobLevelAsync(JobLevel jobLevel)
    {
      List<Prospect> lst;
      if (jobLevel == null)
        lst = (List<Prospect>) null;
      else if (string.IsNullOrWhiteSpace(jobLevel.Id))
      {
        lst = (List<Prospect>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.CRMProspects.Where<Prospect>((Func<Prospect, bool>) (x => x.JobLevelId == jobLevel.Id)).OrderBy<Prospect, string>((Func<Prospect, string>) (x => x.CompanyName)).ThenBy<Prospect, string>((Func<Prospect, string>) (x => x.FirstName)).ThenBy<Prospect, string>((Func<Prospect, string>) (x => x.LastName)).ToList<Prospect>();
        //dataSet = (DataSet) null;
        foreach (Prospect item in lst)
        {
          item.JobLevelId = jobLevel.Id;
          item.JobLevel = jobLevel;
        }
        jobLevel.TeamMembers = lst;
        lst = (List<Prospect>) null;
      }
    }

    public void GetForJobLevel(IEnumerable<JobLevel> jobLevels) => this.GetForJobLevelAsync(jobLevels).RunSynchronously();

    public async Task GetForJobLevelAsync(IEnumerable<JobLevel> jobLevels)
    {
      if (jobLevels == null)
        return;
      foreach (JobLevel jobLevel in jobLevels)
        await this.GetForJobLevelAsync(jobLevel);
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Prospect entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Prospect entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Prospect entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Prospect> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Prospect> entities)
    {
      if (entities == null)
        throw new DuplicateException("The prospects are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
