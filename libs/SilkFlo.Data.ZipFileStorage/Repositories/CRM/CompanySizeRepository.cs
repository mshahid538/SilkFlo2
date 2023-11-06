// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.CRM.CompanySizeRepository
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
  public class CompanySizeRepository : ICompanySizeRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public CompanySizeRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public CompanySize Get(string id) => this.GetAsync(id).Result;

    public async Task<CompanySize> GetAsync(string id)
    {
      if (id == null)
        return (CompanySize) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.CRMCompanySizes.SingleOrDefault<CompanySize>((Func<CompanySize, bool>) (x => x.Id == id));
    }

    public CompanySize SingleOrDefault(Func<CompanySize, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<CompanySize> SingleOrDefaultAsync(Func<CompanySize, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.CRMCompanySizes.Where<CompanySize>(predicate).FirstOrDefault<CompanySize>();
    }

    public bool Add(CompanySize entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(CompanySize entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<CompanySize> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<CompanySize> entities)
    {
      if (entities == null)
        return false;
      foreach (CompanySize entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<CompanySize> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<CompanySize>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<CompanySize>) dataSetAsync.CRMCompanySizes.OrderBy<CompanySize, int>((Func<CompanySize, int>) (m => m.Sort)).ThenBy<CompanySize, string>((Func<CompanySize, string>) (m => m.Name));
    }

    public IEnumerable<CompanySize> Find(Func<CompanySize, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<CompanySize>> FindAsync(Func<CompanySize, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<CompanySize>) dataSetAsync.CRMCompanySizes.Where<CompanySize>(predicate).OrderBy<CompanySize, int>((Func<CompanySize, int>) (m => m.Sort)).ThenBy<CompanySize, string>((Func<CompanySize, string>) (m => m.Name));
    }

    public CompanySize GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<CompanySize> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (CompanySize) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.CRMCompanySizes.SingleOrDefault<CompanySize>((Func<CompanySize, bool>) (x => x.Name == name));
    }

    public void GetCompanySizeFor(IEnumerable<Prospect> prospects) => this.GetCompanySizeForAsync(prospects).RunSynchronously();

    public async Task GetCompanySizeForAsync(IEnumerable<Prospect> prospects)
    {
      if (prospects == null)
        return;
      foreach (Prospect prospect in prospects)
        await this.GetCompanySizeForAsync(prospect);
    }

    public void GetCompanySizeFor(Prospect prospect) => this.GetCompanySizeForAsync(prospect).RunSynchronously();

    public async Task GetCompanySizeForAsync(Prospect prospect)
    {
      if (prospect == null)
        ;
      else
      {
        Prospect prospect1 = prospect;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        prospect1.CompanySize = dataSet.CRMCompanySizes.SingleOrDefault<CompanySize>((Func<CompanySize, bool>) (x => x.Id == prospect.CompanySizeId));
        prospect1 = (Prospect) null;
        //dataSet = (DataSet) null;
      }
    }

    public CompanySize GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<CompanySize> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (CompanySize) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.CRMCompanySizes.SingleOrDefault<CompanySize>((Func<CompanySize, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      CompanySize entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(CompanySize entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(CompanySize entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<CompanySize> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<CompanySize> entities)
    {
      if (entities == null)
        throw new DuplicateException("The companySizes are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
