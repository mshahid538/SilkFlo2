// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.ClientRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using SilkFlo.Data.Core.Repositories.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class ClientRepository : IClientRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public ClientRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Client Get(string id) => this.GetAsync(id).Result;

    public async Task<Client> GetAsync(string id)
    {
      if (id == null)
        return (Client) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == id));
    }

    public Client SingleOrDefault(Func<Client, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Client> SingleOrDefaultAsync(Func<Client, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessClients.Where<Client>(predicate).FirstOrDefault<Client>();
    }

    public bool Add(Client entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Client entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Client> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Client> entities)
    {
      if (entities == null)
        return false;
      foreach (Client entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Client> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Client>) dataSetAsync.BusinessClients.OrderBy<Client, string>((Func<Client, string>) (m => m.Name));
    }

    public IEnumerable<Client> Find(Func<Client, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Client>> FindAsync(Func<Client, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Client>) dataSetAsync.BusinessClients.Where<Client>(predicate).OrderBy<Client, string>((Func<Client, string>) (m => m.Name));
    }

    public Client GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Client> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Client) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Name == name));
    }

    public void GetForAccountOwner(User accountOwner) => this.GetForAccountOwnerAsync(accountOwner).RunSynchronously();

    public async Task GetForAccountOwnerAsync(User accountOwner)
    {
      List<Client> lst;
      if (accountOwner == null)
        lst = (List<Client>) null;
      else if (string.IsNullOrWhiteSpace(accountOwner.Id))
      {
        lst = (List<Client>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessClients.Where<Client>((Func<Client, bool>) (x => x.AccountOwnerId == accountOwner.Id)).OrderBy<Client, string>((Func<Client, string>) (x => x.Name)).ToList<Client>();
        dataSet = (DataSet) null;
        foreach (Client item in lst)
        {
          item.AccountOwnerId = accountOwner.Id;
          item.AccountOwner = accountOwner;
        }
        accountOwner.AccountOwners = lst;
        lst = (List<Client>) null;
      }
    }

    public void GetForAccountOwner(IEnumerable<User> accountOwners) => this.GetForAccountOwnerAsync(accountOwners).RunSynchronously();

    public async Task GetForAccountOwnerAsync(IEnumerable<User> accountOwners)
    {
      if (accountOwners == null)
        return;
      foreach (User accountOwner in accountOwners)
        await this.GetForAccountOwnerAsync(accountOwner);
    }

    public void GetForAgencyDiscount(Discount agencyDiscount) => this.GetForAgencyDiscountAsync(agencyDiscount).RunSynchronously();

    public async Task GetForAgencyDiscountAsync(Discount agencyDiscount)
    {
      List<Client> lst;
      if (agencyDiscount == null)
        lst = (List<Client>) null;
      else if (string.IsNullOrWhiteSpace(agencyDiscount.Id))
      {
        lst = (List<Client>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessClients.Where<Client>((Func<Client, bool>) (x => x.AgencyDiscountId == agencyDiscount.Id)).OrderBy<Client, string>((Func<Client, string>) (x => x.Name)).ToList<Client>();
        dataSet = (DataSet) null;
        foreach (Client item in lst)
        {
          item.AgencyDiscountId = agencyDiscount.Id;
          item.AgencyDiscount = agencyDiscount;
        }
        agencyDiscount.Clients = lst;
        lst = (List<Client>) null;
      }
    }

    public void GetForAgencyDiscount(IEnumerable<Discount> agencyDiscounts) => this.GetForAgencyDiscountAsync(agencyDiscounts).RunSynchronously();

    public async Task GetForAgencyDiscountAsync(IEnumerable<Discount> agencyDiscounts)
    {
      if (agencyDiscounts == null)
        return;
      foreach (Discount agencyDiscount in agencyDiscounts)
        await this.GetForAgencyDiscountAsync(agencyDiscount);
    }

    public void GetForAgency(Client agency) => this.GetForAgencyAsync(agency).RunSynchronously();

    public async Task GetForAgencyAsync(Client agency)
    {
      List<Client> lst;
      if (agency == null)
        lst = (List<Client>) null;
      else if (string.IsNullOrWhiteSpace(agency.Id))
      {
        lst = (List<Client>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessClients.Where<Client>((Func<Client, bool>) (x => x.AgencyId == agency.Id)).OrderBy<Client, string>((Func<Client, string>) (x => x.Name)).ToList<Client>();
        dataSet = (DataSet) null;
        foreach (Client item in lst)
        {
          item.AgencyId = agency.Id;
          item.Agency = agency;
        }
        agency.Customers = lst;
        lst = (List<Client>) null;
      }
    }

    public void GetForAgency(IEnumerable<Client> agencies) => this.GetForAgencyAsync(agencies).RunSynchronously();

    public async Task GetForAgencyAsync(IEnumerable<Client> agencies)
    {
      if (agencies == null)
        return;
      foreach (Client agency in agencies)
        await this.GetForAgencyAsync(agency);
    }

    public void GetForCountry(Country country) => this.GetForCountryAsync(country).RunSynchronously();

    public async Task GetForCountryAsync(Country country)
    {
      List<Client> lst;
      if (country == null)
        lst = (List<Client>) null;
      else if (string.IsNullOrWhiteSpace(country.Id))
      {
        lst = (List<Client>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessClients.Where<Client>((Func<Client, bool>) (x => x.CountryId == country.Id)).OrderBy<Client, string>((Func<Client, string>) (x => x.Name)).ToList<Client>();
        dataSet = (DataSet) null;
        foreach (Client item in lst)
        {
          item.CountryId = country.Id;
          item.Country = country;
        }
        country.Clients = lst;
        lst = (List<Client>) null;
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

    public void GetForCurrency(Currency currency) => this.GetForCurrencyAsync(currency).RunSynchronously();

    public async Task GetForCurrencyAsync(Currency currency)
    {
      List<Client> lst;
      if (currency == null)
        lst = (List<Client>) null;
      else if (string.IsNullOrWhiteSpace(currency.Id))
      {
        lst = (List<Client>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessClients.Where<Client>((Func<Client, bool>) (x => x.CurrencyId == currency.Id)).OrderBy<Client, string>((Func<Client, string>) (x => x.Name)).ToList<Client>();
        dataSet = (DataSet) null;
        foreach (Client item in lst)
        {
          item.CurrencyId = currency.Id;
          item.Currency = currency;
        }
        currency.Clients = lst;
        lst = (List<Client>) null;
      }
    }

    public void GetForCurrency(IEnumerable<Currency> currencies) => this.GetForCurrencyAsync(currencies).RunSynchronously();

    public async Task GetForCurrencyAsync(IEnumerable<Currency> currencies)
    {
      if (currencies == null)
        return;
      foreach (Currency currency in currencies)
        await this.GetForCurrencyAsync(currency);
    }

    public void GetForIndustry(Industry industry) => this.GetForIndustryAsync(industry).RunSynchronously();

    public async Task GetForIndustryAsync(Industry industry)
    {
      List<Client> lst;
      if (industry == null)
        lst = (List<Client>) null;
      else if (string.IsNullOrWhiteSpace(industry.Id))
      {
        lst = (List<Client>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessClients.Where<Client>((Func<Client, bool>) (x => x.IndustryId == industry.Id)).OrderBy<Client, string>((Func<Client, string>) (x => x.Name)).ToList<Client>();
        dataSet = (DataSet) null;
        foreach (Client item in lst)
        {
          item.IndustryId = industry.Id;
          item.Industry = industry;
        }
        industry.Clients = lst;
        lst = (List<Client>) null;
      }
    }

    public void GetForIndustry(IEnumerable<Industry> industries) => this.GetForIndustryAsync(industries).RunSynchronously();

    public async Task GetForIndustryAsync(IEnumerable<Industry> industries)
    {
      if (industries == null)
        return;
      foreach (Industry industry in industries)
        await this.GetForIndustryAsync(industry);
    }

    public void GetForLanguage(Language language) => this.GetForLanguageAsync(language).RunSynchronously();

    public async Task GetForLanguageAsync(Language language)
    {
      List<Client> lst;
      if (language == null)
        lst = (List<Client>) null;
      else if (string.IsNullOrWhiteSpace(language.Id))
      {
        lst = (List<Client>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessClients.Where<Client>((Func<Client, bool>) (x => x.LanguageId == language.Id)).OrderBy<Client, string>((Func<Client, string>) (x => x.Name)).ToList<Client>();
        dataSet = (DataSet) null;
        foreach (Client item in lst)
        {
          item.LanguageId = language.Id;
          item.Language = language;
        }
        language.Clients = lst;
        lst = (List<Client>) null;
      }
    }

    public void GetForLanguage(IEnumerable<Language> languages) => this.GetForLanguageAsync(languages).RunSynchronously();

    public async Task GetForLanguageAsync(IEnumerable<Language> languages)
    {
      if (languages == null)
        return;
      foreach (Language language in languages)
        await this.GetForLanguageAsync(language);
    }

    public void GetForPracticeAccount(Client practiceAccount) => this.GetForPracticeAccountAsync(practiceAccount).RunSynchronously();

    public async Task GetForPracticeAccountAsync(Client practiceAccount)
    {
      List<Client> lst;
      if (practiceAccount == null)
        lst = (List<Client>) null;
      else if (string.IsNullOrWhiteSpace(practiceAccount.Id))
      {
        lst = (List<Client>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessClients.Where<Client>((Func<Client, bool>) (x => x.PracticeId == practiceAccount.Id)).OrderBy<Client, string>((Func<Client, string>) (x => x.Name)).ToList<Client>();
        dataSet = (DataSet) null;
        foreach (Client item in lst)
        {
          item.PracticeId = practiceAccount.Id;
          item.PracticeAccount = practiceAccount;
        }
        practiceAccount.ProductionAccounts = lst;
        lst = (List<Client>) null;
      }
    }

    public void GetForPracticeAccount(IEnumerable<Client> practiceAccounts) => this.GetForPracticeAccountAsync(practiceAccounts).RunSynchronously();

    public async Task GetForPracticeAccountAsync(IEnumerable<Client> practiceAccounts)
    {
      if (practiceAccounts == null)
        return;
      foreach (Client practiceAccount in practiceAccounts)
        await this.GetForPracticeAccountAsync(practiceAccount);
    }

    public void GetForType(ClientType type) => this.GetForTypeAsync(type).RunSynchronously();

    public async Task GetForTypeAsync(ClientType type)
    {
      List<Client> lst;
      if (type == null)
        lst = (List<Client>) null;
      else if (string.IsNullOrWhiteSpace(type.Id))
      {
        lst = (List<Client>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessClients.Where<Client>((Func<Client, bool>) (x => x.TypeId == type.Id)).OrderBy<Client, string>((Func<Client, string>) (x => x.Name)).ToList<Client>();
        dataSet = (DataSet) null;
        foreach (Client item in lst)
        {
          item.TypeId = type.Id;
          item.Type = type;
        }
        type.Clients = lst;
        lst = (List<Client>) null;
      }
    }

    public void GetForType(IEnumerable<ClientType> types) => this.GetForTypeAsync(types).RunSynchronously();

    public async Task GetForTypeAsync(IEnumerable<ClientType> types)
    {
      if (types == null)
        return;
      foreach (ClientType type in types)
        await this.GetForTypeAsync(type);
    }

    public void GetClientFor(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> applications) => this.GetClientForAsync(applications).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> applications)
    {
      if (applications == null)
        return;
      foreach (SilkFlo.Data.Core.Domain.Business.Application application in applications)
        await this.GetClientForAsync(application);
    }

    public void GetClientFor(SilkFlo.Data.Core.Domain.Business.Application application) => this.GetClientForAsync(application).RunSynchronously();

    public async Task GetClientForAsync(SilkFlo.Data.Core.Domain.Business.Application application)
    {
      if (application == null)
        ;
      else
      {
                SilkFlo.Data.Core.Domain.Business.Application application1 = application;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        application1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == application.ClientId));
        application1 = (SilkFlo.Data.Core.Domain.Business.Application) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetAgencyFor(IEnumerable<Client> customers) => this.GetAgencyForAsync(customers).RunSynchronously();

    public async Task GetAgencyForAsync(IEnumerable<Client> customers)
    {
      if (customers == null)
        return;
      foreach (Client client in customers)
        await this.GetAgencyForAsync(client);
    }

    public void GetAgencyFor(Client client) => this.GetAgencyForAsync(client).RunSynchronously();

    public async Task GetAgencyForAsync(Client client)
    {
      if (client == null)
        ;
      else
      {
        Client client1 = client;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        client1.Agency = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == client.AgencyId));
        client1 = (Client) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetPracticeAccountFor(IEnumerable<Client> productionAccounts) => this.GetPracticeAccountForAsync(productionAccounts).RunSynchronously();

    public async Task GetPracticeAccountForAsync(IEnumerable<Client> productionAccounts)
    {
      if (productionAccounts == null)
        return;
      foreach (Client client in productionAccounts)
        await this.GetPracticeAccountForAsync(client);
    }

    public void GetPracticeAccountFor(Client client) => this.GetPracticeAccountForAsync(client).RunSynchronously();

    public async Task GetPracticeAccountForAsync(Client client)
    {
      if (client == null)
        ;
      else
      {
        Client client1 = client;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        client1.PracticeAccount = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == client.PracticeId));
        client1 = (Client) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<Comment> comments) => this.GetClientForAsync(comments).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<Comment> comments)
    {
      if (comments == null)
        return;
      foreach (Comment comment in comments)
        await this.GetClientForAsync(comment);
    }

    public void GetClientFor(Comment comment) => this.GetClientForAsync(comment).RunSynchronously();

    public async Task GetClientForAsync(Comment comment)
    {
      if (comment == null)
        ;
      else
      {
        Comment comment1 = comment;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        comment1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == comment.ClientId));
        comment1 = (Comment) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<Department> departments) => this.GetClientForAsync(departments).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<Department> departments)
    {
      if (departments == null)
        return;
      foreach (Department department in departments)
        await this.GetClientForAsync(department);
    }

    public void GetClientFor(Department department) => this.GetClientForAsync(department).RunSynchronously();

    public async Task GetClientForAsync(Department department)
    {
      if (department == null)
        ;
      else
      {
        Department department1 = department;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        department1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == department.ClientId));
        department1 = (Department) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<Document> documents) => this.GetClientForAsync(documents).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<Document> documents)
    {
      if (documents == null)
        return;
      foreach (Document document in documents)
        await this.GetClientForAsync(document);
    }

    public void GetClientFor(Document document) => this.GetClientForAsync(document).RunSynchronously();

    public async Task GetClientForAsync(Document document)
    {
      if (document == null)
        ;
      else
      {
        Document document1 = document;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        document1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == document.ClientId));
        document1 = (Document) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<Idea> ideas) => this.GetClientForAsync(ideas).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<Idea> ideas)
    {
      if (ideas == null)
        return;
      foreach (Idea idea in ideas)
        await this.GetClientForAsync(idea);
    }

    public void GetClientFor(Idea idea) => this.GetClientForAsync(idea).RunSynchronously();

    public async Task GetClientForAsync(Idea idea)
    {
      if (idea == null)
        ;
      else
      {
        Idea idea1 = idea;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        idea1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == idea.ClientId));
        idea1 = (Idea) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(
      IEnumerable<IdeaOtherRunningCost> ideaOtherRunningCosts)
    {
      this.GetClientForAsync(ideaOtherRunningCosts).RunSynchronously();
    }

    public async Task GetClientForAsync(
      IEnumerable<IdeaOtherRunningCost> ideaOtherRunningCosts)
    {
      if (ideaOtherRunningCosts == null)
        return;
      foreach (IdeaOtherRunningCost ideaOtherRunningCost in ideaOtherRunningCosts)
        await this.GetClientForAsync(ideaOtherRunningCost);
    }

    public void GetClientFor(IdeaOtherRunningCost ideaOtherRunningCost) => this.GetClientForAsync(ideaOtherRunningCost).RunSynchronously();

    public async Task GetClientForAsync(IdeaOtherRunningCost ideaOtherRunningCost)
    {
      if (ideaOtherRunningCost == null)
        ;
      else
      {
        IdeaOtherRunningCost otherRunningCost = ideaOtherRunningCost;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        otherRunningCost.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == ideaOtherRunningCost.ClientId));
        otherRunningCost = (IdeaOtherRunningCost) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<IdeaRunningCost> ideaRunningCosts) => this.GetClientForAsync(ideaRunningCosts).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<IdeaRunningCost> ideaRunningCosts)
    {
      if (ideaRunningCosts == null)
        return;
      foreach (IdeaRunningCost ideaRunningCost in ideaRunningCosts)
        await this.GetClientForAsync(ideaRunningCost);
    }

    public void GetClientFor(IdeaRunningCost ideaRunningCost) => this.GetClientForAsync(ideaRunningCost).RunSynchronously();

    public async Task GetClientForAsync(IdeaRunningCost ideaRunningCost)
    {
      if (ideaRunningCost == null)
        ;
      else
      {
        IdeaRunningCost ideaRunningCost1 = ideaRunningCost;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        ideaRunningCost1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == ideaRunningCost.ClientId));
        ideaRunningCost1 = (IdeaRunningCost) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(
      IEnumerable<ImplementationCost> implementationCosts)
    {
      this.GetClientForAsync(implementationCosts).RunSynchronously();
    }

    public async Task GetClientForAsync(
      IEnumerable<ImplementationCost> implementationCosts)
    {
      if (implementationCosts == null)
        return;
      foreach (ImplementationCost implementationCost in implementationCosts)
        await this.GetClientForAsync(implementationCost);
    }

    public void GetClientFor(ImplementationCost implementationCost) => this.GetClientForAsync(implementationCost).RunSynchronously();

    public async Task GetClientForAsync(ImplementationCost implementationCost)
    {
      if (implementationCost == null)
        ;
      else
      {
        ImplementationCost implementationCost1 = implementationCost;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        implementationCost1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == implementationCost.ClientId));
        implementationCost1 = (ImplementationCost) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<Location> locations) => this.GetClientForAsync(locations).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<Location> locations)
    {
      if (locations == null)
        return;
      foreach (Location location in locations)
        await this.GetClientForAsync(location);
    }

    public void GetClientFor(Location location) => this.GetClientForAsync(location).RunSynchronously();

    public async Task GetClientForAsync(Location location)
    {
      if (location == null)
        ;
      else
      {
        Location location1 = location;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        location1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == location.ClientId));
        location1 = (Location) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetTenantFor(IEnumerable<ManageTenant> manageTenants) => this.GetTenantForAsync(manageTenants).RunSynchronously();

    public async Task GetTenantForAsync(IEnumerable<ManageTenant> manageTenants)
    {
      if (manageTenants == null)
        return;
      foreach (ManageTenant manageTenant in manageTenants)
        await this.GetTenantForAsync(manageTenant);
    }

    public void GetTenantFor(ManageTenant manageTenant) => this.GetTenantForAsync(manageTenant).RunSynchronously();

    public async Task GetTenantForAsync(ManageTenant manageTenant)
    {
      if (manageTenant == null)
        ;
      else
      {
        ManageTenant manageTenant1 = manageTenant;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        manageTenant1.Tenant = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == manageTenant.TenantId));
        manageTenant1 = (ManageTenant) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<Message> messages) => this.GetClientForAsync(messages).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<Message> messages)
    {
      if (messages == null)
        return;
      foreach (Message message in messages)
        await this.GetClientForAsync(message);
    }

    public void GetClientFor(Message message) => this.GetClientForAsync(message).RunSynchronously();

    public async Task GetClientForAsync(Message message)
    {
      if (message == null)
        ;
      else
      {
        Message message1 = message;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        message1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == message.ClientId));
        message1 = (Message) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<OtherRunningCost> otherRunningCosts) => this.GetClientForAsync(otherRunningCosts).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<OtherRunningCost> otherRunningCosts)
    {
      if (otherRunningCosts == null)
        return;
      foreach (OtherRunningCost otherRunningCost in otherRunningCosts)
        await this.GetClientForAsync(otherRunningCost);
    }

    public void GetClientFor(OtherRunningCost otherRunningCost) => this.GetClientForAsync(otherRunningCost).RunSynchronously();

    public async Task GetClientForAsync(OtherRunningCost otherRunningCost)
    {
      if (otherRunningCost == null)
        ;
      else
      {
        OtherRunningCost otherRunningCost1 = otherRunningCost;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        otherRunningCost1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == otherRunningCost.ClientId));
        otherRunningCost1 = (OtherRunningCost) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<Process> processes) => this.GetClientForAsync(processes).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<Process> processes)
    {
      if (processes == null)
        return;
      foreach (Process process in processes)
        await this.GetClientForAsync(process);
    }

    public void GetClientFor(Process process) => this.GetClientForAsync(process).RunSynchronously();

    public async Task GetClientForAsync(Process process)
    {
      if (process == null)
        ;
      else
      {
        Process process1 = process;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        process1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == process.ClientId));
        process1 = (Process) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<SilkFlo.Data.Core.Domain.Business.Role> roles) => this.GetClientForAsync(roles).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Role> roles)
    {
      if (roles == null)
        return;
      foreach (SilkFlo.Data.Core.Domain.Business.Role role in roles)
        await this.GetClientForAsync(role);
    }

    public void GetClientFor(SilkFlo.Data.Core.Domain.Business.Role role) => this.GetClientForAsync(role).RunSynchronously();

    public async Task GetClientForAsync(SilkFlo.Data.Core.Domain.Business.Role role)
    {
      if (role == null)
        ;
      else
      {
        SilkFlo.Data.Core.Domain.Business.Role role1 = role;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        role1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == role.ClientId));
        role1 = (SilkFlo.Data.Core.Domain.Business.Role) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<RoleCost> roleCosts) => this.GetClientForAsync(roleCosts).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<RoleCost> roleCosts)
    {
      if (roleCosts == null)
        return;
      foreach (RoleCost roleCost in roleCosts)
        await this.GetClientForAsync(roleCost);
    }

    public void GetClientFor(RoleCost roleCost) => this.GetClientForAsync(roleCost).RunSynchronously();

    public async Task GetClientForAsync(RoleCost roleCost)
    {
      if (roleCost == null)
        ;
      else
      {
        RoleCost roleCost1 = roleCost;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        roleCost1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == roleCost.ClientId));
        roleCost1 = (RoleCost) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(
      IEnumerable<RoleIdeaAuthorisation> roleIdeaAuthorisations)
    {
      this.GetClientForAsync(roleIdeaAuthorisations).RunSynchronously();
    }

    public async Task GetClientForAsync(
      IEnumerable<RoleIdeaAuthorisation> roleIdeaAuthorisations)
    {
      if (roleIdeaAuthorisations == null)
        return;
      foreach (RoleIdeaAuthorisation roleIdeaAuthorisation in roleIdeaAuthorisations)
        await this.GetClientForAsync(roleIdeaAuthorisation);
    }

    public void GetClientFor(RoleIdeaAuthorisation roleIdeaAuthorisation) => this.GetClientForAsync(roleIdeaAuthorisation).RunSynchronously();

    public async Task GetClientForAsync(RoleIdeaAuthorisation roleIdeaAuthorisation)
    {
      if (roleIdeaAuthorisation == null)
        ;
      else
      {
        RoleIdeaAuthorisation ideaAuthorisation = roleIdeaAuthorisation;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        ideaAuthorisation.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == roleIdeaAuthorisation.ClientId));
        ideaAuthorisation = (RoleIdeaAuthorisation) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<RunningCost> runningCosts) => this.GetClientForAsync(runningCosts).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<RunningCost> runningCosts)
    {
      if (runningCosts == null)
        return;
      foreach (RunningCost runningCost in runningCosts)
        await this.GetClientForAsync(runningCost);
    }

    public void GetClientFor(RunningCost runningCost) => this.GetClientForAsync(runningCost).RunSynchronously();

    public async Task GetClientForAsync(RunningCost runningCost)
    {
      if (runningCost == null)
        ;
      else
      {
        RunningCost runningCost1 = runningCost;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        runningCost1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == runningCost.ClientId));
        runningCost1 = (RunningCost) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<SoftwareVender> softwareVenders) => this.GetClientForAsync(softwareVenders).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<SoftwareVender> softwareVenders)
    {
      if (softwareVenders == null)
        return;
      foreach (SoftwareVender softwareVender in softwareVenders)
        await this.GetClientForAsync(softwareVender);
    }

    public void GetClientFor(SoftwareVender softwareVender) => this.GetClientForAsync(softwareVender).RunSynchronously();

    public async Task GetClientForAsync(SoftwareVender softwareVender)
    {
      if (softwareVender == null)
        ;
      else
      {
        SoftwareVender softwareVender1 = softwareVender;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        softwareVender1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == softwareVender.ClientId));
        softwareVender1 = (SoftwareVender) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetTenantFor(IEnumerable<Subscription> tenantSubscriptions) => this.GetTenantForAsync(tenantSubscriptions).RunSynchronously();

    public async Task GetTenantForAsync(IEnumerable<Subscription> tenantSubscriptions)
    {
      if (tenantSubscriptions == null)
        return;
      foreach (Subscription subscription in tenantSubscriptions)
        await this.GetTenantForAsync(subscription);
    }

    public void GetTenantFor(Subscription subscription) => this.GetTenantForAsync(subscription).RunSynchronously();

    public async Task GetTenantForAsync(Subscription subscription)
    {
      if (subscription == null)
        ;
      else
      {
        Subscription subscription1 = subscription;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        subscription1.Tenant = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == subscription.TenantId));
        subscription1 = (Subscription) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetAgencyFor(IEnumerable<Subscription> agencySubscriptions) => this.GetAgencyForAsync(agencySubscriptions).RunSynchronously();

    public async Task GetAgencyForAsync(IEnumerable<Subscription> agencySubscriptions)
    {
      if (agencySubscriptions == null)
        return;
      foreach (Subscription subscription in agencySubscriptions)
        await this.GetAgencyForAsync(subscription);
    }

    public void GetAgencyFor(Subscription subscription) => this.GetAgencyForAsync(subscription).RunSynchronously();

    public async Task GetAgencyForAsync(Subscription subscription)
    {
      if (subscription == null)
        ;
      else
      {
        Subscription subscription1 = subscription;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        subscription1.Agency = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == subscription.AgencyId));
        subscription1 = (Subscription) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<Team> teams) => this.GetClientForAsync(teams).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<Team> teams)
    {
      if (teams == null)
        return;
      foreach (Team team in teams)
        await this.GetClientForAsync(team);
    }

    public void GetClientFor(Team team) => this.GetClientForAsync(team).RunSynchronously();

    public async Task GetClientForAsync(Team team)
    {
      if (team == null)
        ;
      else
      {
        Team team1 = team;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        team1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == team.ClientId));
        team1 = (Team) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<User> users) => this.GetClientForAsync(users).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<User> users)
    {
      if (users == null)
        return;
      foreach (User user in users)
        await this.GetClientForAsync(user);
    }

    public void GetClientFor(User user) => this.GetClientForAsync(user).RunSynchronously();

    public async Task GetClientForAsync(User user)
    {
      if (user == null)
        ;
      else
      {
        User user1 = user;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        user1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == user.ClientId));
        user1 = (User) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetClientFor(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions) => this.GetClientForAsync(versions).RunSynchronously();

    public async Task GetClientForAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions)
    {
      if (versions == null)
        return;
      foreach (SilkFlo.Data.Core.Domain.Business.Version version in versions)
        await this.GetClientForAsync(version);
    }

    public void GetClientFor(SilkFlo.Data.Core.Domain.Business.Version version) => this.GetClientForAsync(version).RunSynchronously();

    public async Task GetClientForAsync(SilkFlo.Data.Core.Domain.Business.Version version)
    {
      if (version == null)
        ;
      else
      {
        SilkFlo.Data.Core.Domain.Business.Version version1 = version;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        version1.Client = dataSet.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => x.Id == version.ClientId));
        version1 = (SilkFlo.Data.Core.Domain.Business.Version) null;
        dataSet = (DataSet) null;
      }
    }

    public Client GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Client> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Client) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessClients.SingleOrDefault<Client>((Func<Client, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Client entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Client entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Client entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Client> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Client> entities)
    {
      if (entities == null)
        throw new DuplicateException("The clients are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
