using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using static System.Int32;
using Role = SilkFlo.Data.Core.Domain.Role;

namespace SilkFlo.Web.Insert
{
    public class PracticeData
    {
        public static async Task CreatePracticeAccountAsync(Client productionAccount, 
                                                            Data.Core.IUnitOfWork unitOfWork,
                                                            bool useTestUsers,
                                                            User accountOwner)
        {
            // Guard Clause
            if (productionAccount == null)
                throw new ArgumentNullException(nameof(productionAccount));


            // Guard Clause
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            // Guard Clause - Check practice already present
            if (!string.IsNullOrWhiteSpace(productionAccount.PracticeId)) 
                return;

            try
            {
                var random = new Random();
                var industries = (await unitOfWork.SharedIndustries.GetAllAsync()).ToList();
                var industryIndex = random.Next(0, industries.Count());
                var industryId = industries[industryIndex].Id;


                var practice = new Client
                {
                    Name = productionAccount.Name + " - " + Settings.PracticeAccountName,
                    IsPractice = true,
                    TypeId = Data.Core.Enumerators.ClientType.Client39.ToString(),
                    IndustryId = industryId,
                    CountryId = productionAccount.CountryId,
                    AverageWorkingDay = productionAccount.AverageWorkingDay,
                    AverageWorkingHour = productionAccount.AverageWorkingHour,
                    CurrencyId = productionAccount.CurrencyId,
                    Address1 = productionAccount.Address1,
                    Address2 = productionAccount.Address2,
                    City = productionAccount.City,
                    State = productionAccount.State,
                    PostCode = productionAccount.PostCode,
                    LanguageId = productionAccount.LanguageId,
                    AccountOwnerId = productionAccount.AccountOwnerId,
                    Website = Settings.PracticeAccountWebSite
                };


                var model = new Models.Business.Client(practice);

                await unitOfWork .AddAsync(practice);
                await model.SetupTenantAsync(unitOfWork);

                productionAccount.PracticeAccount = practice;
                practice.ProductionAccounts.Add(productionAccount);
                productionAccount.PracticeId = practice.Id;

                await unitOfWork.CompleteAsync();


                var applications = await ApplicationsAsync(practice, unitOfWork);
                var departments = await DepartmentsAndTeamsAsync(practice, unitOfWork);
                var users = await AddUsersAsync(practice, unitOfWork, 5, Settings.PracticeAccountEmailSuffix, useTestUsers);
                await SoftwareVendersAsync(practice, unitOfWork);
                await OtherRunningCostsAsync(practice, unitOfWork);

                await unitOfWork.CompleteAsync();

                var languages = (await unitOfWork.SharedLanguages.GetAllAsync()).ToList();

                var ideas = await AddIdeasAsync(
                    unitOfWork, 
                    practice, 
                    applications, 
                    departments, 
                    languages, 
                    users);

                await AddCollaboratorsAsync(
                    unitOfWork,
                    ideas,
                    users,
                    productionAccount,
                    accountOwner);
            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
                throw;
            }
        }

        private static async Task AddCollaboratorsAsync(Data.Core.IUnitOfWork unitOfWork,
                                                        List<Idea> ideas,
                                                        IReadOnlyList<User> users,
                                                        Client tenant,
                                                        User accountOwner)
        {
            if (ideas == null)
                throw new ArgumentNullException(nameof(ideas));

            var businessRoles = 
                (await unitOfWork.BusinessRoles
                    .FindAsync(x => x.IsBuiltIn || x.ClientId == tenant.Id))
                    .ToArray();


            var random = new Random();
            foreach (var idea in ideas)
            {
                var usersTemp = new List<User>();
                usersTemp.AddRange(users);

                var length = random.Next(2); 
                var collaborators = new List<Collaborator>();
                for (var i = 0; i < length; i++)
                {
                    var collaborator = new Collaborator();
                    var userIndex = random.Next(usersTemp.Count());
                    var user = usersTemp[userIndex];
                    usersTemp.Remove(user);

                    var invitedByIndex = random.Next(users.Count());
                    var invitedBy = users[invitedByIndex];

                    collaborator.User = user;
                    collaborator.InvitedBy = invitedBy;
                    collaborator.Idea = idea;
                    collaborators.Add(collaborator);

                    var roleCount = random.Next(1, businessRoles.Count());
                    var rolesTemp = new List<Data.Core.Domain.Business.BusinessRole>();
                    rolesTemp.AddRange(businessRoles);
                    for (var j = 0; j < roleCount; j++)
                    {
                        var rowIndex = random.Next(rolesTemp.Count());
                        var role = rolesTemp[rowIndex];
                        rolesTemp.Remove(role);

                        var collaboratorRole = new CollaboratorRole
                        {
                            RoleId = role.Id,
                            CollaboratorId = collaborator.Id,
                        };


                        collaborator.CollaboratorRoles.Add(collaboratorRole);
                    }
                }

                await Models.Business.Collaborator.UpdateAsync(
                    unitOfWork,
                    Models.Business.Collaborator.Create(collaborators),
                    idea.Id,
                    accountOwner?.Id);
            }
        }




        private static async Task<List<Application>> ApplicationsAsync(Client practiceAccount,
                                                                       Data.Core.IUnitOfWork unitOfWork)
        {
            try
            {
                if(!practiceAccount.IsPractice)
                    throw new ArgumentException("practiceAccount is a production account.");

                var applications = new List<Application>();

                // 1
                var application = await AddApplicationAsync("MS Word", "2019", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS Word", "2016", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS Word", "2010", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS Word", "2007", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS Word", "2003", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                // 2
                application = await AddApplicationAsync("MS Excel", "2019", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS Excel", "2016", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS Excel", "2010", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS Excel", "2007", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS Excel", "2003", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);


                // 3
                application = await AddApplicationAsync("MS Outlook", "2019", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS Outlook", "2016", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS Outlook", "2010", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS Outlook", "2007", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS Outlook", "2003", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);


                // 4
                application = await AddApplicationAsync("MS PowerPoint", "2019", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);

                application = await AddApplicationAsync("MS PowerPoint", "2016", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);


                // 5
                application = await AddApplicationAsync("Acrobat Reader", "2020", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);


                // 6
                application = await AddApplicationAsync("MS Access Finance Database", "2019", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);


                // 7
                application = await AddApplicationAsync("SalesForce", "2020", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);


                // 8
                application = await AddApplicationAsync("SAP", "2020", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);


                // 9
                application = await AddApplicationAsync("OneNote", "2019", practiceAccount, unitOfWork);
                if (application != null) applications.Add(application);


                return applications;
            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
                throw;
            }
        }

        //private static async Task InitialCostsAsync(
        //    Client practiceAccount,
        //    Data.Core.IUnitOfWork unitOfWork)
        //{
        //    try
        //    {
        //        if (!practiceAccount.IsPractice)
        //            throw new ArgumentException("practiceAccount is a production account.");


        //        var builtInRoles = (await unitOfWork.BusinessRoles.FindAsync(x => x.IsBuiltIn)).ToArray();

        //        var roles = (await unitOfWork.BusinessRoles.FindAsync(x => !x.IsBuiltIn && x.ClientId == practiceAccount.Id)).ToArray();

        //    }
        //    catch (Exception ex)
        //    {
        //        unitOfWork.Log(ex);
        //        throw;
        //    }
        //}

        private static async Task SoftwareVendersAsync(
            Client practiceAccount,
            Data.Core.IUnitOfWork unitOfWork)
        {
            try
            {
                if (!practiceAccount.IsPractice)
                    throw new ArgumentException("practiceAccount is a production account.");

                var name = "Microsoft Power Automate";
                var softwareVender = await unitOfWork.BusinessSoftwareVenders
                    .SingleOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.ClientId == practiceAccount.Id);

                if (softwareVender == null)
                {
                    softwareVender = new SoftwareVender
                    {
                        Name = name,
                        IsLive = true,
                        Client = practiceAccount,
                    };

                    await unitOfWork.AddAsync(softwareVender);


                    var runningCost = new RunningCost
                    {
                        Vender = softwareVender,
                        AutomationTypeId = Data.Core.Enumerators.AutomationType.Attended.ToString(),
                        LicenceType = "Test",
                        Cost = 100,
                        FrequencyId = "Monthly",
                        IsLive = true,
                        Client = practiceAccount
                    };
                    await unitOfWork.AddAsync(runningCost);

                    runningCost = new RunningCost
                    {
                        Vender = softwareVender,
                        AutomationTypeId = Data.Core.Enumerators.AutomationType.Unattended.ToString(),
                        Cost = 100,
                        LicenceType = "Test",
                        FrequencyId = "Monthly",
                        IsLive = true,
                        Client = practiceAccount
                    };
                    await unitOfWork.AddAsync(runningCost);
                }




                name = "UiPath";
                softwareVender = await unitOfWork.BusinessSoftwareVenders
                    .SingleOrDefaultAsync(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase) 
                                               && x.ClientId == practiceAccount.Id);

                if (softwareVender == null)
                {
                    softwareVender = new SoftwareVender
                    {
                        Name = name,
                        IsLive = true,
                        Client = practiceAccount,
                    };

                    await unitOfWork.AddAsync(softwareVender);


                    var runningCost = new RunningCost
                    {
                        Vender = softwareVender,
                        AutomationTypeId = Data.Core.Enumerators.AutomationType.Attended.ToString(),
                        LicenceType = "Test",
                        Cost = 1380,
                        FrequencyId = "Monthly",
                        IsLive = true,
                        Client = practiceAccount
                    };
                    await unitOfWork.AddAsync(runningCost);

                    runningCost = new RunningCost
                    {
                        Vender = softwareVender,
                        AutomationTypeId = Data.Core.Enumerators.AutomationType.Unattended.ToString(),
                        Cost = 1380,
                        LicenceType = "Test",
                        FrequencyId = "Monthly",
                        IsLive = true,
                        Client = practiceAccount
                    };
                    await unitOfWork.AddAsync(runningCost);
                }


            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
                throw;
            }
        }


        private static async Task OtherRunningCostsAsync(
            Client practiceAccount,
            Data.Core.IUnitOfWork unitOfWork)
        {
            try
            {
                if (!practiceAccount.IsPractice)
                    throw new ArgumentException("practiceAccount is a production account.");

                var name = "Office 365";

                var core = await unitOfWork.BusinessOtherRunningCosts.SingleOrDefaultAsync(x =>
                    x.Name.ToLower() == name.ToLower() && x.ClientId == practiceAccount.Id);

                if (core == null)
                {
                    core = new OtherRunningCost
                    {
                        Name = name,
                        CostTypeId = Data.Core.Enumerators.CostType.SoftwareLicence.ToString(),
                        Description = "Test",
                        FrequencyId = Data.Core.Enumerators.Period.Monthly.ToString(),
                        Cost = 28.10m,
                        IsLive = true,
                        Client = practiceAccount
                    };

                    await unitOfWork.AddAsync(core);
                }


                name = "Acrobat Standard DC";

                core = await unitOfWork.BusinessOtherRunningCosts.SingleOrDefaultAsync(x =>
                    x.Name.ToLower() == name.ToLower() && x.ClientId == practiceAccount.Id);

                if (core == null)
                {
                    core = new OtherRunningCost
                    {
                        Name = name,
                        CostTypeId = Data.Core.Enumerators.CostType.SoftwareLicence.ToString(),
                        Description = "Test",
                        FrequencyId = Data.Core.Enumerators.Period.Annual.ToString(),
                        Cost = 156.89m,
                        IsLive = true,
                        Client = practiceAccount
                    };

                    await unitOfWork.AddAsync(core);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        private static async Task<List<Department>> DepartmentsAndTeamsAsync(Client tenant,
                                                                             Data.Core.IUnitOfWork unitOfWork)
        {
            try
            {
                var tenantId = tenant.Id;

                var departments = new List<Department>();

                var name = "Finance";
                var department = await unitOfWork.BusinessDepartments
                                                 .SingleOrDefaultAsync(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)
                                                                         && x.ClientId == tenantId);
                if (department == null)
                {
                    department = new Department
                    {
                        Name = name,
                        ClientId = tenantId
                    };
                    await unitOfWork.AddAsync(department);
                    departments.Add(department);
                }


                name = "Accounts Payable - Invoice Payment";
                var team = await unitOfWork.BusinessTeams
                                           .SingleOrDefaultAsync(x => String.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)
                                                                   && x.ClientId == tenantId);
                if (team == null)
                {
                    team = new Team
                    {
                        Name = name,
                        ClientId = tenantId,
                        Department = department
                    };
                    await unitOfWork.AddAsync(team);
                    department.Teams.Add(team);
                }


                name = "Invoice Processing";
                var process = await unitOfWork.BusinessProcesses
                                              .SingleOrDefaultAsync(x => String.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)
                                                                      && x.ClientId == tenantId);
                if (process == null)
                {
                    process
                        = new Process
                        {
                            Name = name,
                            ClientId = tenantId,
                            Team = team
                        };
                    await unitOfWork.AddAsync(process);
                    team.Processes.Add(process);
                }



                name = "Information Technology";
                department = await unitOfWork.BusinessDepartments
                    .SingleOrDefaultAsync(x => String.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)
                                               && x.ClientId == tenantId);
                if (department == null)
                {
                    department = new Department
                    {
                        Name = name,
                        ClientId = tenantId
                    };
                    await unitOfWork.AddAsync(department);
                    departments.Add(department);
                }


                name = "Human Resources";
                department = await unitOfWork.BusinessDepartments
                                             .SingleOrDefaultAsync(x => String.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)
                                                                     && x.ClientId == tenantId);
                if (department != null)
                    return departments;


                department = new Department
                {
                    Name = name,
                    ClientId = tenantId
                };
                await unitOfWork.AddAsync(department);
                departments.Add(department);

                return departments;
            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
                throw;
            }   
        }

        public static async Task AssignClientAndRoleAsync(string email,
                                                           string clientId,
                                                           Data.Core.Enumerators.Role role,
                                                           Data.Core.IUnitOfWork unitOfWork)
        {
            try
            {
                var user = await unitOfWork.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == email);
                if (user != null)
                {
                    user.ClientId = clientId;

                    // Add Roles
                    string roleId = ((int)role).ToString();
                    var userRole = await unitOfWork.UserRoles
                                                   .SingleOrDefaultAsync(x => x.RoleId == roleId
                                                                           && x.UserId == user.Id);
                    if (userRole == null)
                        await unitOfWork.UserRoles
                                  .AddAsync(new UserRole
                                  {
                                      RoleId = roleId,
                                      UserId = user.Id
                                  });
                }
            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
                throw;
            }    
        }

        private static async Task<Application> AddApplicationAsync(string name,
                                                                     string versionName,
                                                                     Client practiceAccount,
                                                                     Data.Core.IUnitOfWork unitOfWork)
        {
            try
            {
                var application = await unitOfWork.BusinessApplications
                    .SingleOrDefaultAsync(x => x.Name == name
                                       && x.ClientId == practiceAccount.Id);

                if (application == null)
                {
                    application = new Application
                    {
                        Name = name,
                        ClientId = practiceAccount.Id,
                    };
                    await unitOfWork.AddAsync(application);
                }



                var version = await unitOfWork.BusinessVersions
                                              .SingleOrDefaultAsync(x => x.Name == versionName
                                                                      && x.ApplicationId == application.Id
                                                                      && x.ClientId == practiceAccount.Id);

                if (version != null)
                    return application;


                version = new Data.Core.Domain.Business.Version
                {
                    Name = versionName,
                    ClientId = practiceAccount.Id,
                    Application = application,
                    IsLive = true
                };
                await unitOfWork.AddAsync(version);


                return application;
            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
                throw;
            }
        }


        /// <summary>
        /// Add NPC Users
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="count"></param>
        /// <param name="domainName"></param>
        /// <param name="useTestUsers"></param>
        internal static async Task<List<User>> AddUsersAsync(Client tenant,
                                                             Data.Core.IUnitOfWork unitOfWork,
                                                             int count,
                                                             string domainName,
                                                             bool useTestUsers)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(domainName))
                    throw new ArgumentNullException(nameof(domainName));

                var tenantId = tenant.Id;

                await unitOfWork.Users.GetForClientAsync(tenant);

                var users = tenant.Users;

                if (users.Count > 0)
                    return users;

                var roles = (await unitOfWork.Roles.FindAsync(x => x.Name.IndexOf("agency", StringComparison.InvariantCultureIgnoreCase) == -1
                                                                   && x.Name.IndexOf("can backup dataset", StringComparison.InvariantCultureIgnoreCase) == -1
                                                                   && x.Name.IndexOf("administrator", StringComparison.InvariantCultureIgnoreCase) == -1)).ToArray();


                if (useTestUsers)
                {
                    var user = await AddUserAndRole(
                        domainName,
                        tenantId,
                        "Account",
                        "Owner",
                        roles.Where(x => x.Id == ((int)Data.Core.Enumerators.Role.AccountOwner).ToString()),
                        1,
                        unitOfWork);

                    if (user != null)
                        users.Add(user);


                    user = await AddUserAndRole(
                        domainName,
                        tenantId,
                        "Program",
                        "Manager",
                        roles.Where(x => x.Id == ((int)Data.Core.Enumerators.Role.ProgramManager).ToString()),
                        1,
                        unitOfWork);

                    if (user != null)
                        users.Add(user);


                    user = await AddUserAndRole(
                        domainName,
                        tenantId,
                        "Idea",
                        "Approver",
                        roles.Where(x => x.Id == ((int)Data.Core.Enumerators.Role.IdeaApprover).ToString()),
                        1,
                        unitOfWork);

                    if (user != null)
                        users.Add(user);


                    user = await AddUserAndRole(
                        domainName,
                        tenantId,
                        "Authorised",
                        "User",
                        roles.Where(x => x.Id == ((int)Data.Core.Enumerators.Role.AuthorisedUser).ToString()),
                        1,
                        unitOfWork);

                    if (user != null)
                        users.Add(user);



                    user = await AddUserAndRole(
                        domainName,
                        tenantId,
                        "Automation",
                        "Sponsor",
                        roles.Where(x => x.Id == ((int)Data.Core.Enumerators.Role.RPASponsor).ToString()),
                        1,
                        unitOfWork);

                    if (user != null)
                        users.Add(user);



                    user = await AddUserAndRole(
                        domainName,
                        tenantId,
                        "Standard",
                        "User",
                        roles.Where(x => x.Id == ((int)Data.Core.Enumerators.Role.StandardUser).ToString()),
                        1,
                        unitOfWork);

                    if (user != null)
                        users.Add(user);



                    return users;
                }



                string[] firstNames = { "James", "Robert", "John", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua", "Kenneth", "Kevin", "Brian", "George", "Edward", "Ronald", "Timothy", "Jason", "Jeffrey", "Ryan", "Jacob", "Gary", "Nicholas", "Eric", "Jonathan", "Stephen", "Larry", "Justin", "Scott", "Brandon", "Benjamin", "Samuel", "Gregory", "Frank", "Alexander", "Raymond", "Patrick", "Jack", "Dennis", "Jerry", "Tyler", "Aaron", "Jose", "Adam", "Henry", "Nathan", "Douglas", "Zachary", "Peter", "Kyle", "Walter", "Ethan", "Jeremy", "Harold", "Keith", "Christian", "Roger", "Noah", "Gerald", "Carl", "Terry", "Sean", "Austin", "Arthur", "Lawrence", "Jesse", "Dylan", "Bryan", "Joe", "Jordan", "Billy", "Bruce", "Albert", "Willie", "Gabriel", "Logan", "Alan", "Juan", "Wayne", "Roy", "Ralph", "Randy", "Eugene", "Vincent", "Russell", "Elijah", "Louis", "Bobby", "Philip", "Johnny", "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth", "Barbara", "Susan", "Jessica", "Sarah", "Karen", "Nancy", "Lisa", "Betty", "Margaret", "Sandra", "Ashley", "Kimberly", "Emily", "Donna", "Michelle", "Dorothy", "Carol", "Amanda", "Melissa", "Deborah", "Stephanie", "Rebecca", "Sharon", "Laura", "Cynthia", "Kathleen", "Amy", "Shirley", "Angela", "Helen", "Anna", "Brenda", "Pamela", "Nicole", "Emma", "Samantha", "Katherine", "Christine", "Debra", "Rachel", "Catherine", "Carolyn", "Janet", "Ruth", "Maria", "Heather", "Diane", "Virginia", "Julie", "Joyce", "Victoria", "Olivia", "Kelly", "Christina", "Lauren", "Joan", "Evelyn", "Judith", "Megan", "Cheryl", "Andrea", "Hannah", "Martha", "Jacqueline", "Frances", "Gloria", "Ann", "Teresa", "Kathryn", "Sara", "Janice", "Jean", "Alice", "Madison", "Doris", "Abigail", "Julia", "Judy", "Grace", "Denise", "Amber", "Marilyn", "Beverly", "Danielle", "Theresa", "Sophia", "Marie", "Diana", "Brittany", "Natalie", "Isabella", "Charlotte", "Rose", "Alexis", "Kayla" };
                string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts" };

                var firstNameCount = firstNames.Length - 1;
                var lastNameCount = lastNames.Length - 1;




                var random = new Random();



                var i = 0;
                while(i < count)
                {
                    var firstName = firstNames[random.Next(0, firstNameCount)];
                    var lastName = lastNames[random.Next(0, lastNameCount)];


                    var user = await AddUserAndRole(
                        domainName,
                        tenantId,
                        firstName,
                        lastName,
                        roles,
                        4,
                        unitOfWork);


                    i++;

                    if (user == null)
                        continue;

                    users.Add(user);
                }


                return users;
            }
            catch (Exception ex)
            {
                unitOfWork.Log(ex);
                throw;
            }
        }

        public static async Task<User> AddUserAndRole(
            string domainName,
            string tenantId,
            string firstName,
            string lastName,
            IEnumerable<Role> roles,
            int roleCount,
            Data.Core.IUnitOfWork unitOfWork)
        {
            var email = firstName.ToLower() + "." + lastName.ToLower() + "@" + domainName.ToLower();

            var clone = await unitOfWork.Users
                .SingleOrDefaultAsync(x => x.Email == email);

            if (clone != null)
                return null;

            var random = new Random();

            var user = new User
            {
                ClientId = tenantId,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                IsEmailConfirmed = true,
                PasswordHash = unitOfWork.GeneratePasswordHash("password1"),
            };



            await unitOfWork.AddAsync(user);

            var userRoleCount = roleCount == 1 ? 1 : random.Next(0, roleCount);
            if (userRoleCount <= 0)
                return null;

            await UserRolesSelectedAsync(
                roles.ToList(),
                roleCount,
                userRoleCount,
                user,
                unitOfWork);

            return user;
        }

        public static async Task UserRolesSelectedAsync(
            List<Role> all,
            int count,
            int countRequired,
            User user,
            Data.Core.IUnitOfWork unitOfWork)
        {
            var selected = new List<UserRole>();

            var random = new Random();

            var addRange = false;
            if (all.Count == 1)
            {
                var role = all[0];

                if ((await unitOfWork.UserRoles.FindAsync(x => x.RoleId == role.Id && x.UserId == user.Id))
                    .Any())
                    return;

                var userRole = new UserRole
                {
                    RoleId = role.Id,
                    User = user
                };

                await unitOfWork.UserRoles.AddAsync(userRole);
                return;
            }

            for (var i = 0; i < countRequired; i++)
            {
                var userRoleIndex = random.Next(0, count);
                var role = all[userRoleIndex];

                var isPresent = selected.Any(x => x.RoleId == role.Id);

                if (isPresent)
                    continue;


                if (!(await unitOfWork.UserRoles.FindAsync(x => x.RoleId == role.Id && x.UserId == user.Id))
                    .Any()) 
                    continue;

                var userRole = new UserRole
                {
                    RoleId = role.Id,
                    User = user
                };

                selected.Add(userRole);
                addRange = true;
            }

            if(addRange)
                await unitOfWork.UserRoles.AddRangeAsync(selected);
        }


        public static async Task<List<Idea>> AddIdeasAsync (
            Data.Core.IUnitOfWork unitOfWork,
            Client tenant,
            List<Application> applications,
            List<Department> departments,
            List<Data.Core.Domain.Shared.Language> languages,
            List<User> users)
        {
            try
            {
                if (unitOfWork == null)
                    throw new ArgumentNullException(nameof(unitOfWork));

                if (tenant == null)
                    throw new ArgumentNullException(nameof(tenant));

                if (applications == null)
                    throw new ArgumentNullException(nameof(applications));


                if (applications.Count == 0)
                    throw new Exception("Applications list is empty.");


                if (departments == null)
                    throw new ArgumentNullException(nameof(departments));


                if (departments.Count == 0)
                    throw new Exception("Departments list is empty.");


                if (users == null)
                    throw new ArgumentNullException(nameof(users));

                if (users.Count == 0)
                    throw new Exception("Users list is empty.");


                await unitOfWork.BusinessVersions.GetForApplicationAsync(applications);


                var hr = departments.SingleOrDefault(x => x.Name == "Human Resources");
                if (hr == null)
                    throw new Exception("Human Resources is null");

                var humanResourcesId = hr.Id;

                var it = departments.SingleOrDefault(x => x.Name == "Information Technology");
                if (it == null)
                    throw new Exception("Information Technology is null");


                var informationTechnologyId = it.Id;

                var ideas = new List<Idea>();

                var name = "Source Job Candidates";
                var idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);

                var random = new Random();
                await unitOfWork.Users.GetForClientAsync(tenant);


                var userCount = tenant.Users.Count;

                if (idea == null)
                {
                    idea = await CreateIdea(unitOfWork,
                        tenant.Users[random.Next(0, userCount)],
                        name,
                        new DateTime(2022, 1, 1),
                        "Source job candidates",
                        humanResourcesId,
                        "VeryRuleBased",
                        "VeryMuchDigital",
                        "VeryMuchStructured",
                        "SmallChange",
                        "Yes",
                        "Productivity",
                        "SmallChange",
                        260,
                        8m,
                        35000,
                        3,
                        "Weekly",
                        90,
                        15,
                        3,
                        2,
                        3,
                        2,
                        "ThereAreNoPeaks",
                        "Between100_150Steps",
                        "Between2_5Ways",
                        "Between60And80",
                        "Between2And3 ",
                        "SimpleDecisions",
                        0,
                        false,
                        false,
                        false,
                        false,
                        false,
                        Data.Core.Enumerators.Stage.n01_Assess.ToString(),
                        Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
                        tenant,
                        applications,
                        languages);
                    ideas.Add(idea);
                }


                //name = "Screen candidates";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        "Screen Candidates",
                //        new DateTime(2022, 1, 1),
                //        name,
                //        humanResourcesId,
                //        "PrettyRuleBased",
                //        "VeryMuchDigital",
                //        "PrettyMuchStructured",
                //        "SmallChange",
                //        "Yes",
                //        "Productivity",
                //        "SmallChange",
                //        260,
                //        8m,
                //        35000,
                //        4,
                //        "Daily",
                //        50,
                //        10,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3 ",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n01_Assess.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Manage Pre-Employment Verification";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 1, 1),
                //        "Manage pre-employment verification",
                //        humanResourcesId,
                //        "VeryRuleBased",
                //        "PrettyMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        35000,
                //        2,
                //        "Weekly",
                //        80,
                //        21,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "RareButPredictableEvent",
                //        "Between 50-100 steps ",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3 ",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        true,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n11_Qualify_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);

                //    ideas.Add(idea);
                //}

                //name = "Create and Manage Reports";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 1, 1),
                //        "Create and manage reports",
                //        humanResourcesId,
                //        "PrettyRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Quality",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        45000,
                //        3,
                //        "Daily",
                //        60,
                //        5,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "SameWay",
                //        "GreaterThen80",
                //        "Between2And3 ",
                //        "SimpleDecisions",
                //        0,
                //        true,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n13_Qualify_Approved.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Manage Employee Performance";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 1, 1),
                //        "Manage employee performance",
                //        humanResourcesId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "PrettyMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        40000,
                //        2,
                //        "Daily",
                //        50,
                //        12,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between 50-100 steps ",
                //        "Between2_5Ways",
                //        "Between60And80",
                //        "Between2And3 ",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n03_Analysis.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n17_Analysis_InProgress.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Service Employee Inquiries";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 1, 1),
                //        "Service employee inquiries",
                //        humanResourcesId,
                //        "PrettyRuleBased",
                //        "PrettyMuchDigital",
                //        "PrettyMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        35000,
                //        3,
                //        "Weekly",
                //        70,
                //        25,
                //        3,
                //        4,
                //        3,
                //        4,
                //        "ThereAreNoPeaks",
                //        "Between 50-100 steps ",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3 ",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        true,
                //        Data.Core.Enumerators.Stage.n05_Development.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n33_Development_InProgress.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Manage Employee Off-Boarding";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 1, 1),
                //        "Manage employee off-boarding",
                //        humanResourcesId,
                //        "VeryRuleBased",
                //        "PrettyMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Cost",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        35000,
                //        4,
                //        "Daily",
                //        90,
                //        24,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "SameWay",
                //        "GreaterThen80",
                //        "Between4And5",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n07_Deployed.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n51_Deployed_InProduction.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Manage Time & Attendance";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 1, 1),
                //        "Manage time & attendance",
                //        humanResourcesId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        35000,
                //        5,
                //        "Daily",
                //        80,
                //        15,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between 50-100 steps ",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3 ",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n07_Deployed.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n51_Deployed_InProduction.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Manage IT Portfolio";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 1, 1),
                //        "Manage IT Portfolio",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        3,
                //        "Weekly",
                //        150,
                //        45,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "RareAndUnpredictable",
                //        "Between20_50Steps",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3 ",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n01_Assess.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Analyze and Manage Demand for IT Services";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 1, 1),
                //        "Analyze and manage demand for IT Services",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        2,
                //        "Daily",
                //        95,
                //        9,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3 ",
                //        "ComplexDecisions",
                //        0,
                //        false,
                //        true,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n01_Assess.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Manage IT Customer Satisfaction";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 2, 1),
                //        "Manage IT customer satisfaction",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Cost",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        3,
                //        "Weekly",
                //        69,
                //        45,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "Between5_8Ways",
                //        "GreaterThen80",
                //        "Between4And5",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n11_Qualify_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Define the Enterprise Information Architecture";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 2, 1),
                //        "Define the enterprise information architecture",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        4,
                //        "Daily",
                //        153,
                //        36,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "RareButPredictableEvent",
                //        "Between20_50Steps",
                //        "SameWay",
                //        "GreaterThen80",
                //        "Between2And3 ",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        true,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n11_Qualify_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Perform Enterprise Data and Content Management";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 2, 1),
                //        "Perform enterprise data and content management",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        5,
                //        "Daily",
                //        25,
                //        6,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between100_150Steps ",
                //        "Between5_8Ways",
                //        "GreaterThen80",
                //        "Between4And5",
                //        "SimpleDecisions",
                //        0,
                //        true,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n03_Analysis.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n17_Analysis_InProgress.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Perform IT Services Life Cycle Planning";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 2, 1),
                //        "Perform IT services life cycle planning",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        3,
                //        "Weekly",
                //        99,
                //        52,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between100_150Steps",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between4And5",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n05_Development.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n33_Development_InProgress.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Maintain IT Services and Solutions";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 2, 1),
                //        "Maintain IT services and solutions",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Quality",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        2,
                //        "Daily",
                //        145,
                //        39,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "GreaterThen5",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        true,
                //        Data.Core.Enumerators.Stage.n01_Assess.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Create IT Services and Solutions";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 2, 1),
                //        "Create IT services and solutions",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Quality",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        3,
                //        "Daily",
                //        14,
                //        10,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between 50-100 steps ",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        true,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n01_Assess.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Maintain IT Services and Solutions 2";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 2, 1),
                //        "Maintain IT services and solutions 2",
                //        informationTechnologyId,
                //        "PrettyRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Quality",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        4,
                //        "Weekly",
                //        69,
                //        11,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "SameWay",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n11_Qualify_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Plan and Implement Changes";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 2, 1),
                //        "Plan and implement changes",
                //        informationTechnologyId,
                //        "PrettyRuleBased",
                //        "PrettyMuchDigital",
                //        "PrettyMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Cost",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        5,
                //        "Daily",
                //        160,
                //        46,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "RareAndUnpredictable",
                //        "Between 50-100 steps ",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        true,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n11_Qualify_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Manage IT Infrastructure Resources";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 3, 1),
                //        "Manage IT infrastructure resources",
                //        informationTechnologyId,
                //        "PrettyRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "SmallChange",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        3,
                //        "Weekly",
                //        36,
                //        33,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between 50-100 steps ",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n03_Analysis.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n16_Analysis_NotStarted.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Package Software Configuration";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 3, 1),
                //        "Package Software Configuration",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "PrettyMuchDigital",
                //        "PrettyMuchStructured",
                //        "SmallChange",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        2,
                //        "Daily",
                //        99,
                //        36,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "SameWay",
                //        "Between60And80",
                //        "Between2And3",
                //        "ComplexDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n01_Assess.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Perform IT Application Unit Testing";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 3, 1),
                //        "Perform IT Application Unit Testing",
                //        informationTechnologyId,
                //        "PrettyRuleBased",
                //        "VeryMuchDigital",
                //        "PrettyMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Quality",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        3,
                //        "Daily",
                //        42,
                //        23,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "RareButPredictableEvent",
                //        "Between 50-100 steps ",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        true,
                //        Data.Core.Enumerators.Stage.n01_Assess.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Perform User Acceptance Testing";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 3, 1),
                //        "Perform User Acceptance Testing",
                //        informationTechnologyId,
                //        "PrettyRuleBased",
                //        "VeryMuchDigital",
                //        "PrettyMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Quality",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        4,
                //        "Weekly",
                //        92,
                //        10,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between4And5",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n11_Qualify_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Perform Regression Testing";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 3, 1),
                //        "Perform Regression Testing",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "PrettyMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Quality",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        5,
                //        "Daily",
                //        59,
                //        50,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        true,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n11_Qualify_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Perform Software QA Testing";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 3, 1),
                //        "Perform Software QA Testing",
                //        informationTechnologyId,
                //        "PrettyRuleBased",
                //        "VeryMuchDigital",
                //        "PrettyMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        3,
                //        "Daily",
                //        45,
                //        26,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "Between5_8Ways",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n03_Analysis.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n16_Analysis_NotStarted.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Schedule Jobs";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 3, 1),
                //        "Schedule Jobs",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Cost",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        2,
                //        "Weekly",
                //        153,
                //        21,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "SameWay",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        true,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n07_Deployed.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n51_Deployed_InProduction.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Manage Network Security";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 3, 1),
                //        "Manage Network Security",
                //        informationTechnologyId,
                //        "PrettyRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "SmallChange",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        3,
                //        "Daily",
                //        132,
                //        24,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between100_150Steps ",
                //        "Between5_8Ways",
                //        "GreaterThen80",
                //        "Between4And5",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n03_Analysis.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n16_Analysis_NotStarted.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Distribute Electronic Software";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 3, 1),
                //        "Distribute Electronic Software",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Quality",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        4,
                //        "Weekly",
                //        141,
                //        27,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "RareAndUnpredictable",
                //        "Between100_150Steps",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        true,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n07_Deployed.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n51_Deployed_InProduction.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Deploy Content Filtering, Firewalls and Virus Protection";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 4, 1),
                //        "Deploy Content Filtering, Firewalls and Virus Protection",
                //        informationTechnologyId,
                //        "PrettyRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Quality",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        5,
                //        "Weekly",
                //        49,
                //        14,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between4And5",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n11_Qualify_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Monitor Remote Management (RIMS)";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{

                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 4, 1),
                //        "Monitor Remote Management (RIMS)",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "PrettyMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Quality",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        3,
                //        "Daily",
                //        147,
                //        38,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between 50-100 steps ",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between4And5",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n13_Qualify_Approved.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Perform User Administration";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{

                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 4, 1),
                //        "Perform User Administration",
                //        informationTechnologyId,
                //        "PrettyRuleBased",
                //        "VeryMuchDigital",
                //        "PrettyMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Quality",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        2,
                //        "Weekly",
                //        5,
                //        40,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "RareButPredictableEvent",
                //        "Between20_50Steps",
                //        "SameWay",
                //        "Between60And80",
                //        "GreaterThen5",
                //        "ComplexDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n01_Assess.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Provide Help-desk Services";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{

                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 4, 1),
                //        "Provide Help-desk Services",
                //        informationTechnologyId,
                //        "PrettyRuleBased",
                //        "VeryMuchDigital",
                //        "PrettyMuchStructured",
                //        "SmallChange",
                //        "Yes",
                //        "Cost",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        3,
                //        "Daily",
                //        70,
                //        14,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between 50-100 steps ",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        true,
                //        true,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n01_Assess.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Perform QA and Services Audit";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{
                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 4, 1),
                //        "Perform QA and Services Audit",
                //        informationTechnologyId,
                //        "VeryRuleBased",
                //        "VeryMuchDigital",
                //        "PrettyMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        4,
                //        "Daily",
                //        26,
                //        31,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between 50-100 steps ",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n11_Qualify_AwaitingReview.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Perform Backups and Restorations";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{

                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 4, 1),
                //        "Perform Backups and Restorations",
                //        informationTechnologyId,
                //        "PrettyRuleBased",
                //        "PrettyMuchDigital",
                //        "VeryMuchStructured",
                //        "NoChangeExpected",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        5,
                //        "Weekly",
                //        135,
                //        21,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between20_50Steps",
                //        "SameWay",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        false,
                //        false,
                //        false,
                //        false,
                //        false,
                //        Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n13_Qualify_Approved.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}

                //name = "Perform Corrective and Adaptive Maintenance";
                //idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x => x.Name == name && x.ClientId == tenant.Id);
                //if (idea == null)
                //{

                //    idea = await CreateIdea(unitOfWork,
                //        tenant.Users[random.Next(0, userCount)],
                //        name,
                //        new DateTime(2022, 4, 1),
                //        "Perform Corrective and Adaptive Maintenance",
                //        informationTechnologyId,
                //        "PrettyRuleBased",
                //        "VeryMuchDigital",
                //        "VeryMuchStructured",
                //        "SmallChange",
                //        "Yes",
                //        "Productivity",
                //        "NoChangeExpected",
                //        260,
                //        8m,
                //        65000,
                //        3,
                //        "Daily",
                //        68,
                //        14,
                //        3,
                //        3,
                //        3,
                //        3,
                //        "ThereAreNoPeaks",
                //        "Between 50-100 steps ",
                //        "Between2_5Ways",
                //        "GreaterThen80",
                //        "Between2And3",
                //        "SimpleDecisions",
                //        0,
                //        true,
                //        false,
                //        false,
                //        false,
                //        true,
                //        Data.Core.Enumerators.Stage.n01_Assess.ToString(),
                //        Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress.ToString(),
                //        tenant,
                //        applications,
                //        languages);
                //    ideas.Add(idea);
                //}


                await unitOfWork.CompleteAsync();

                return ideas;
            }
            catch (Exception ex)
            {
                if (unitOfWork != null) 
                    unitOfWork.Log(ex);
                throw;
            }
        }

        private static async Task<Idea> CreateIdea(
            Data.Core.IUnitOfWork unitOfWork,
            User accountOwner,
            string name,
            DateTime dateStartForIdea,
            string summary,
            string departmentId,
            string ruleId,
            string inputId,
            string inputDataStructureId,
            string processStabilityId,
            string documentationPresentId,
            string automationGoalId,
            string applicationStabilityId,
            int averageWorkingDay,
            decimal workingHour,
            int averageEmployeeFullCost,
            int employeeCount,
            string taskFrequencyId,
            int activityVolumeAverage,
            int averageProcessingTime,
            int averageErrorRate,
            int averageReworkTime,
            int averageWorkToBeReviewed,
            int averageReviewTime,
            string processPeakId,
            string averageNumberOfStepId,
            string numberOfWaysToCompleteProcessId,
            string dataInputPercentOfStructuredId,
            string decisionCountId,
            string decisionDifficultyId,
            int potentialFineProbability,
            bool isHighRisk,
            bool isDataSensitive,
            bool isAlternative,
            bool isHostUpgrade,
            bool isDataInputScanned,
            string stageId,
            string statusId,
            Client tenant,
            IReadOnlyCollection<Application> applications,
            List<Data.Core.Domain.Shared.Language> languages)
        {
            if (languages == null) throw new ArgumentNullException(nameof(languages));

            var idea = await unitOfWork.BusinessIdeas.SingleOrDefaultAsync(x =>
                string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase) 
                && x.ClientId == tenant.Id);

            if (idea != null)
                return idea;

            idea = new Idea
            {
                ProcessOwner = accountOwner,
                Name = name,
                Summary = summary,
                DepartmentId = departmentId,
                RuleId = ruleId,
                InputId = inputId,
                InputDataStructureId = inputDataStructureId,
                ProcessStabilityId = processStabilityId,
                DocumentationPresentId = documentationPresentId,
                AutomationGoalId = automationGoalId,
                ApplicationStabilityId = applicationStabilityId,
                AverageWorkingDay = averageWorkingDay,
                WorkingHour = workingHour,
                AverageEmployeeFullCost = averageEmployeeFullCost,
                EmployeeCount = employeeCount,
                TaskFrequencyId = taskFrequencyId,
                ActivityVolumeAverage = activityVolumeAverage,
                AverageProcessingTime = averageProcessingTime,
                AverageErrorRate = averageErrorRate,
                AverageReworkTime = averageReworkTime,
                AverageWorkToBeReviewed = averageWorkToBeReviewed,
                AverageReviewTime = averageReviewTime,
                ProcessPeakId = processPeakId,
                AverageNumberOfStepId = averageNumberOfStepId,
                NumberOfWaysToCompleteProcessId = numberOfWaysToCompleteProcessId,
                DataInputPercentOfStructuredId = dataInputPercentOfStructuredId,
                DecisionCountId = decisionCountId,
                DecisionDifficultyId = decisionDifficultyId,
                PotentialFineProbability = potentialFineProbability,
                IsHighRisk = isHighRisk,
                IsDataSensitive = isDataSensitive,
                IsAlternative = isAlternative,
                IsHostUpgrade = isHostUpgrade,
                IsDataInputScanned = isDataInputScanned,
                ClientId = tenant.Id,
                SubmissionPathId = Data.Core.Enumerators.SubmissionPath.StandardUser.ToString(),
                PainPointComment = "This is a test.",
                NegativeImpactComment = "This is a test."
            };

            await unitOfWork.AddAsync(idea);
            await unitOfWork.CompleteAsync();
            idea.CreatedDate = dateStartForIdea;

            var random = new Random();

            // Idea Stage
            var dateStartEstimated = dateStartForIdea;
            var ideaStage = new IdeaStage
            {
                StageId = Data.Core.Enumerators.Stage.n00_Idea.ToString(),
                IdeaId = idea.Id,
                DateStartEstimate = dateStartEstimated,
                DateStart = dateStartEstimated,
                IsInWorkFlow = true,
            };

            await unitOfWork.AddAsync(ideaStage);

            var now = DateTime.UtcNow;
            var ideaStatus = new IdeaStageStatus
            {
                IdeaStage = ideaStage,
                StatusId = Data.Core.Enumerators.IdeaStatus.n00_Idea_AwaitingReview.ToString(),
                Date = now
            };

            await unitOfWork.AddAsync(ideaStatus);


            // Assess Stage
            dateStartEstimated = dateStartEstimated.AddDays(1);
            ideaStage = new IdeaStage
            {
                StageId = Data.Core.Enumerators.Stage.n01_Assess.ToString(),
                IdeaId = idea.Id,
                DateStartEstimate = dateStartEstimated,
                DateStart = dateStartEstimated,
                IsInWorkFlow = true,
            };
            await unitOfWork.AddAsync(ideaStage);

            now = now.AddSeconds(1);
            ideaStatus = new IdeaStageStatus
            {
                IdeaStage = ideaStage,
                StatusId = Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
                Date = now
            };
            await unitOfWork.AddAsync(ideaStatus);


            now = now.AddSeconds(1);
            ideaStatus = new IdeaStageStatus
            {
                IdeaStage = ideaStage,
                StatusId = Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress.ToString(),
                Date = now
            };
            await unitOfWork.AddAsync(ideaStatus);
            


            // Qualify Stage
            dateStartEstimated = dateStartEstimated.AddDays(1);
            ideaStage = new IdeaStage
            {
                StageId = Data.Core.Enumerators.Stage.n02_Qualify.ToString(),
                IdeaId = idea.Id,
                DateStartEstimate = dateStartEstimated,
                DateStart = dateStartEstimated,
                IsInWorkFlow = false,
            };

            await unitOfWork.AddAsync(ideaStage);


            dateStartEstimated = dateStartEstimated.AddDays(1);
            var dateStart = dateStartEstimated;
            var dateEndEstimated = dateStartEstimated.AddDays(random.Next(5, 10));
            var dateEnd = dateStart.AddDays(random.Next(5, 10));

            var stages = (await unitOfWork.SharedStages.FindAsync(x => !x.SetDateAutomatically)).ToArray();
            foreach (var stage in stages)
            {
                ideaStage = new IdeaStage
                {
                    IdeaId = idea.Id,
                    StageId = stage.Id,
                    DateStartEstimate = dateStartEstimated,
                    DateStart = dateStart,
                    DateEndEstimate = dateEndEstimated,
                    DateEnd = dateEnd,
                };


                await unitOfWork.AddAsync(ideaStage);

                dateStartEstimated = dateEndEstimated.AddDays(1);
                dateStart = dateEnd.AddDays(1);

                dateEndEstimated = dateStartEstimated.AddDays(random.Next(5, 10));
                dateEnd = dateStart.AddDays(random.Next(5, 10));
            }

            await unitOfWork.AddAsync(ideaStage);


            await AssignStage(
                unitOfWork,
                idea,
                stageId,
                statusId);


            if (!applications.Any())
                return idea;


            var maxCount = 3;
            if (applications.Count < maxCount)
                maxCount = applications.Count;

            var applicationClone = new List<Application>(applications);

            for (var j = 0; j < maxCount; j++)
            {
                var index = random.Next(0, applicationClone.Count);
                var application = applicationClone[index];
                applicationClone.Remove(application);

                if (!application.Versions.Any())
                    continue;

                index = random.Next(0, application.Versions.Count);
                var version = application.Versions[index];


                index = random.Next(0, languages.Count);
                var language = languages[index];




                var ideaApplicationVersion = new IdeaApplicationVersion
                {
                    VersionId = version.Id,
                    Idea = idea,
                    LanguageId = language.Id,
                    IsThinClient = random.Next(0, 1) == 1
                };


                await unitOfWork.AddAsync(ideaApplicationVersion);
            }


            return idea;
        }


        private static async Task AssignStage(
            Data.Core.IUnitOfWork unitOfWork,
            Idea idea,
            string stageId,
            string statusId)
        {
            // Get stage number
            var parts = stageId.Split('_');
            var sNumber = parts[0].Replace("n", "");
            TryParse(sNumber, out var stageNumber);

            parts = statusId.Split('_');
            sNumber = parts[0].Replace("n", "");
            TryParse(sNumber, out var statusNumber);



            // Get the previous stages for the idea
            var previousStages = new List<Models.Shared.Stage>();
            for (var i = 1; i <= stageNumber; i++)
            {
                var stage =
                    await unitOfWork
                        .SharedStages
                        .SingleOrDefaultAsync(x => x.Id[..3] == "n0" + i);

                var model = new Models.Shared.Stage(stage);
                previousStages.Add(model);

                // Get path to success statuses for the stage
                var ideaStatuses =
                    (await unitOfWork.SharedIdeaStatuses.FindAsync(x => x.IsPathToSuccess
                                                                       && Parse(x.Id.Substring(1, 2)) <= statusNumber
                                                                       && x.StageId == stage.Id)).ToList();

                model.Statuses = Models.Shared.IdeaStatus.Create(ideaStatuses);
            }



            var random = new Random();


            foreach (var previousStage in previousStages)
            {
                // Get the previous stage for the idea (IdeaStage table)
                var ideaStage =
                    await unitOfWork
                        .BusinessIdeaStages
                        .SingleOrDefaultAsync(
                            x => x.IdeaId == idea.Id
                                 && x.StageId == previousStage.Id);

                if (ideaStage == null)
                    continue;


                // Assign start dates and add to workflow.
                ideaStage.IsInWorkFlow = true;

                var dateStartEstimated = ideaStage.DateStartEstimate;
                var dateStart = ideaStage.DateStart?? dateStartEstimated;

                // Add to unit of work.
                // Unit of work with decide whether to add new or update existing record.
                await unitOfWork.AddAsync(ideaStage);


                // Add path to success statuses to the workflow (IdeaStageStatus table)
                foreach (var status in previousStage.Statuses)
                {
                    var ideaStageStatus = 
                        await unitOfWork.BusinessIdeaStageStatuses
                            .SingleOrDefaultAsync(x => x.StatusId == status.Id 
                                            && x.IdeaStageId == ideaStage.Id);

                    if (ideaStageStatus == null)
                    {
                        ideaStageStatus = new IdeaStageStatus
                        {
                            StatusId = status.Id,
                            IdeaStageId = ideaStage.Id,
                            Date = dateStart
                        };

                        await unitOfWork.AddAsync(ideaStageStatus);
                    }

                    dateStart = dateStart.AddDays(1);
                }


                var dateEnd = dateStart.AddDays(random.Next(5, 10));
                var dateEndEstimated = dateStart.AddDays(random.Next(5, 10));

                if (ideaStage.StageId == Data.Core.Enumerators.Stage.n00_Idea.ToString() ||
                    ideaStage.StageId == Data.Core.Enumerators.Stage.n01_Assess.ToString() ||
                    ideaStage.StageId == Data.Core.Enumerators.Stage.n02_Qualify.ToString())
                    continue;


                ideaStage.DateEnd = dateEnd;
                ideaStage.DateEndEstimate = dateEndEstimated;
            }
        }
    }
}