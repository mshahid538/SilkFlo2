using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IClientRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Client> GetAsync(string id);

    Task<Client> SingleOrDefaultAsync(Func<Client, bool> predicate);

    Task<bool> AddAsync(Client entity);

    Task<bool> AddRangeAsync(IEnumerable<Client> entities);

    Task<IEnumerable<Client>> GetAllAsync();

    Task<IEnumerable<Client>> FindAsync(Func<Client, bool> predicate);

    Task<Client> GetUsingNameAsync(string name);

    Task GetForAccountOwnerAsync(User accountOwner);

    Task GetForAccountOwnerAsync(IEnumerable<User> accountOwners);

    Task GetForAgencyDiscountAsync(Discount agencyDiscount);

    Task GetForAgencyDiscountAsync(IEnumerable<Discount> agencyDiscounts);

    Task GetForAgencyAsync(Client agency);

    Task GetForAgencyAsync(IEnumerable<Client> agencies);

    Task GetForCountryAsync(Country country);

    Task GetForCountryAsync(IEnumerable<Country> countries);

    Task GetForCurrencyAsync(Currency currency);

    Task GetForCurrencyAsync(IEnumerable<Currency> currencies);

    Task GetForIndustryAsync(Industry industry);

    Task GetForIndustryAsync(IEnumerable<Industry> industries);

    Task GetForLanguageAsync(Language language);

    Task GetForLanguageAsync(IEnumerable<Language> languages);

    Task GetForPracticeAccountAsync(Client practiceAccount);

    Task GetForPracticeAccountAsync(IEnumerable<Client> practiceAccounts);

    Task GetForTypeAsync(ClientType type);

    Task GetForTypeAsync(IEnumerable<ClientType> types);

    Task GetClientForAsync(SilkFlo.Data.Core.Domain.Business.Application application);

    Task GetClientForAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Application> applications);

    Task GetAgencyForAsync(Client client);

    Task GetAgencyForAsync(IEnumerable<Client> clients);

    Task GetPracticeAccountForAsync(Client client);

    Task GetPracticeAccountForAsync(IEnumerable<Client> clients);

    Task GetClientForAsync(Comment comment);

    Task GetClientForAsync(IEnumerable<Comment> comments);

    Task GetClientForAsync(Department department);

    Task GetClientForAsync(IEnumerable<Department> departments);

    Task GetClientForAsync(Document document);

    Task GetClientForAsync(IEnumerable<Document> documents);

    Task GetClientForAsync(Idea idea);

    Task GetClientForAsync(IEnumerable<Idea> ideas);

    Task GetClientForAsync(IdeaOtherRunningCost ideaOtherRunningCost);

    Task GetClientForAsync(
      IEnumerable<IdeaOtherRunningCost> ideaOtherRunningCosts);

    Task GetClientForAsync(IdeaRunningCost ideaRunningCost);

    Task GetClientForAsync(IEnumerable<IdeaRunningCost> ideaRunningCosts);

    Task GetClientForAsync(ImplementationCost implementationCost);

    Task GetClientForAsync(
      IEnumerable<ImplementationCost> implementationCosts);

    Task GetClientForAsync(Location location);

    Task GetClientForAsync(IEnumerable<Location> locations);

    Task GetTenantForAsync(ManageTenant manageTenant);

    Task GetTenantForAsync(IEnumerable<ManageTenant> manageTenants);

    Task GetClientForAsync(Message message);

    Task GetClientForAsync(IEnumerable<Message> messages);

    Task GetClientForAsync(OtherRunningCost otherRunningCost);

    Task GetClientForAsync(IEnumerable<OtherRunningCost> otherRunningCosts);

    Task GetClientForAsync(Process process);

    Task GetClientForAsync(IEnumerable<Process> processes);

    Task GetClientForAsync(SilkFlo.Data.Core.Domain.Business.BusinessRole role);

    Task GetClientForAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.BusinessRole> roles);

    Task GetClientForAsync(RoleCost roleCost);

    Task GetClientForAsync(IEnumerable<RoleCost> roleCosts);

    Task GetClientForAsync(RoleIdeaAuthorisation roleIdeaAuthorisation);

    Task GetClientForAsync(
      IEnumerable<RoleIdeaAuthorisation> roleIdeaAuthorisations);

    Task GetClientForAsync(RunningCost runningCost);

    Task GetClientForAsync(IEnumerable<RunningCost> runningCosts);

    Task GetClientForAsync(SoftwareVender softwareVender);

    Task GetClientForAsync(IEnumerable<SoftwareVender> softwareVenders);

    Task GetTenantForAsync(Subscription subscription);

    Task GetTenantForAsync(IEnumerable<Subscription> subscriptions);

    Task GetAgencyForAsync(Subscription subscription);

    Task GetAgencyForAsync(IEnumerable<Subscription> subscriptions);

    Task GetClientForAsync(Team team);

    Task GetClientForAsync(IEnumerable<Team> teams);

    Task GetClientForAsync(User user);

    Task GetClientForAsync(IEnumerable<User> users);

    Task GetClientForAsync(SilkFlo.Data.Core.Domain.Business.Version version);

    Task GetClientForAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions);

    Task<Client> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Client entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Client> entities);
  }
}
