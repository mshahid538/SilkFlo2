namespace SilkFlo.Data.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ManageTenants",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TenantString = c.String(),
                        TenantId = c.String(maxLength: 128),
                        UserString = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.TenantId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.TenantId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        About = c.String(),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        DepartmentString = c.String(),
                        DepartmentId = c.String(maxLength: 128),
                        Email = c.String(),
                        EmailConfirmationToken = c.String(),
                        EmailNew = c.String(),
                        FirstName = c.String(),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        IsMuted = c.Boolean(nullable: false),
                        JobTitle = c.String(),
                        LastName = c.String(),
                        LocationString = c.String(),
                        LocationId = c.String(maxLength: 128),
                        ManagerString = c.String(),
                        ManagerId = c.String(maxLength: 128),
                        Note = c.String(),
                        PasswordHash = c.String(),
                        PasswordResetToken = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        Department_Id = c.String(maxLength: 128),
                        Location_Id = c.String(maxLength: 128),
                        Client_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .ForeignKey("dbo.Users", t => t.ManagerId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.DepartmentId)
                .Index(t => t.LocationId)
                .Index(t => t.ManagerId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.Department_Id)
                .Index(t => t.Location_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AccountOwnerString = c.String(),
                        AccountOwnerId = c.String(maxLength: 128),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        AgencyDiscountString = c.String(),
                        AgencyDiscountId = c.String(maxLength: 128),
                        AgencyString = c.String(),
                        AgencyId = c.String(maxLength: 128),
                        AllowGuestSignIn = c.Boolean(nullable: false),
                        AverageWorkingDay = c.Int(nullable: false),
                        AverageWorkingHour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        City = c.String(),
                        CountryString = c.String(),
                        CountryId = c.String(maxLength: 128),
                        CurrencyString = c.String(),
                        CurrencyId = c.String(maxLength: 128),
                        FreeTrialDay = c.Int(),
                        IndustryString = c.String(),
                        IndustryId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        IsDemo = c.Boolean(nullable: false),
                        IsPractice = c.Boolean(nullable: false),
                        LanguageString = c.String(),
                        LanguageId = c.String(maxLength: 128),
                        Name = c.String(),
                        PostCode = c.String(),
                        PracticeAccountString = c.String(),
                        PracticeId = c.String(),
                        ReceiveMarketing = c.Boolean(nullable: false),
                        State = c.String(),
                        StripeId = c.String(),
                        TermsOfUse = c.Boolean(nullable: false),
                        TypeString = c.String(),
                        TypeId = c.String(maxLength: 128),
                        Website = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        Client_Id = c.String(maxLength: 128),
                        PracticeAccount_Id = c.String(maxLength: 128),
                        Client_Id1 = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AccountOwnerId)
                .ForeignKey("dbo.Clients", t => t.AgencyId)
                .ForeignKey("dbo.Discounts", t => t.AgencyDiscountId)
                .ForeignKey("dbo.ClientTypes", t => t.TypeId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .ForeignKey("dbo.Languages", t => t.LanguageId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Industries", t => t.IndustryId)
                .ForeignKey("dbo.Clients", t => t.PracticeAccount_Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id1)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.AccountOwnerId)
                .Index(t => t.AgencyDiscountId)
                .Index(t => t.AgencyId)
                .Index(t => t.CountryId)
                .Index(t => t.CurrencyId)
                .Index(t => t.IndustryId)
                .Index(t => t.LanguageId)
                .Index(t => t.TypeId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.Client_Id)
                .Index(t => t.PracticeAccount_Id)
                .Index(t => t.Client_Id1)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DescriptionReferrer = c.String(),
                        DescriptionReseller = c.String(),
                        Max = c.Int(),
                        Min = c.Int(),
                        Name = c.String(),
                        PercentReferrer = c.Int(),
                        PercentReseller = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AgencyDiscountString = c.String(),
                        AgencyDiscountId = c.String(maxLength: 128),
                        AgencyString = c.String(),
                        AgencyId = c.String(maxLength: 128),
                        AgencyTypeString = c.String(),
                        AgencyTypeId = c.String(maxLength: 128),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        CancelToken = c.String(),
                        CouponString = c.String(),
                        CouponId = c.String(maxLength: 128),
                        DateCancelled = c.DateTime(),
                        DateEnd = c.DateTime(),
                        DateStart = c.DateTime(nullable: false),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        InvoiceId = c.String(),
                        InvoiceNumber = c.String(),
                        InvoiceUrl = c.String(),
                        PriceString = c.String(),
                        PriceId = c.String(maxLength: 128),
                        TenantString = c.String(),
                        TenantId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        Client_Id = c.String(maxLength: 128),
                        Client_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.AgencyId)
                .ForeignKey("dbo.Discounts", t => t.AgencyDiscountId)
                .ForeignKey("dbo.ClientTypes", t => t.AgencyTypeId)
                .ForeignKey("dbo.Coupons", t => t.CouponId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Prices", t => t.PriceId)
                .ForeignKey("dbo.Clients", t => t.TenantId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id1)
                .Index(t => t.AgencyDiscountId)
                .Index(t => t.AgencyId)
                .Index(t => t.AgencyTypeId)
                .Index(t => t.CouponId)
                .Index(t => t.PriceId)
                .Index(t => t.TenantId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.Client_Id)
                .Index(t => t.Client_Id1);
            
            CreateTable(
                "dbo.ClientTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Prospects",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientTypeString = c.String(),
                        ClientTypeId = c.String(maxLength: 128),
                        CompanyName = c.String(),
                        CompanySizeString = c.String(),
                        CompanySizeId = c.String(maxLength: 128),
                        CountryString = c.String(),
                        CountryId = c.String(maxLength: 128),
                        Email = c.String(),
                        FirstName = c.String(),
                        JobLevelString = c.String(),
                        JobLevelId = c.String(maxLength: 128),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Pipeline = c.String(),
                        TermsAgreed = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientTypes", t => t.ClientTypeId)
                .ForeignKey("dbo.CompanySizes", t => t.CompanySizeId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.JobLevels", t => t.JobLevelId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientTypeId)
                .Index(t => t.CompanySizeId)
                .Index(t => t.CountryId)
                .Index(t => t.JobLevelId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.CompanySizes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.JobLevels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Coupons",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateExpiry = c.DateTime(),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        DiscountPercent = c.Int(),
                        IsRecurring = c.Boolean(nullable: false),
                        Name = c.String(),
                        TrialDay = c.Int(),
                        UseCount = c.Int(),
                        UseTotal = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        CurrencyString = c.String(),
                        CurrencyId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        IsLive = c.Boolean(nullable: false),
                        PeriodString = c.String(),
                        PeriodId = c.String(maxLength: 128),
                        ProductString = c.String(),
                        ProductId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .ForeignKey("dbo.Periods", t => t.PeriodId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CurrencyId)
                .Index(t => t.PeriodId)
                .Index(t => t.ProductId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Symbol = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Periods",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CancelPeriodInDay = c.Int(nullable: false),
                        CancelPeriodInDayNoRenew = c.Int(nullable: false),
                        MonthCount = c.Int(nullable: false),
                        Name = c.String(),
                        NamePlural = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.OtherRunningCosts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostTypeString = c.String(),
                        CostTypeId = c.String(maxLength: 128),
                        Description = c.String(),
                        FrequencyString = c.String(),
                        FrequencyId = c.String(maxLength: 128),
                        IsLive = c.Boolean(nullable: false),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.CostTypes", t => t.CostTypeId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Periods", t => t.FrequencyId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.CostTypeId)
                .Index(t => t.FrequencyId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.CostTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.IdeaOtherRunningCosts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        IdeaString = c.String(),
                        IdeaId = c.String(maxLength: 128),
                        Number = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherRunningCostString = c.String(),
                        OtherRunningCostId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Ideas", t => t.IdeaId)
                .ForeignKey("dbo.OtherRunningCosts", t => t.OtherRunningCostId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.IdeaId)
                .Index(t => t.OtherRunningCostId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ActivityVolumeAverage = c.Int(),
                        ActivityVolumeAverageComment = c.String(),
                        AHTRobot = c.Decimal(precision: 18, scale: 2),
                        ApplicationStabilityComment = c.String(),
                        ApplicationStabilityString = c.String(),
                        ApplicationStabilityId = c.String(maxLength: 128),
                        AutomationGoalString = c.String(),
                        AutomationGoalId = c.String(maxLength: 128),
                        AverageEmployeeFullCost = c.Int(),
                        AverageEmployeeFullCostComment = c.String(),
                        AverageErrorRate = c.Int(),
                        AverageErrorRateComment = c.String(),
                        AverageNumberOfStepComment = c.String(),
                        AverageNumberOfStepString = c.String(),
                        AverageNumberOfStepId = c.String(maxLength: 128),
                        AverageProcessingTime = c.Decimal(precision: 18, scale: 2),
                        AverageProcessingTimeComment = c.String(),
                        AverageReviewTime = c.Decimal(precision: 18, scale: 2),
                        AverageReviewTimeComment = c.String(),
                        AverageReworkTime = c.Decimal(precision: 18, scale: 2),
                        AverageReworkTimeComment = c.String(),
                        AverageWorkingDay = c.Int(),
                        AverageWorkingDayComment = c.String(),
                        AverageWorkToBeReviewed = c.Decimal(precision: 18, scale: 2),
                        AverageWorkToBeReviewedComment = c.String(),
                        BenefitActual = c.String(),
                        BenefitExpected = c.String(),
                        ChallengeActual = c.String(),
                        ChallengeExpected = c.String(),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        DataInputPercentOfStructuredComment = c.String(),
                        DataInputPercentOfStructuredString = c.String(),
                        DataInputPercentOfStructuredId = c.String(maxLength: 128),
                        DataInputScannedComment = c.String(),
                        DecisionCountComment = c.String(),
                        DecisionCountString = c.String(),
                        DecisionCountId = c.String(maxLength: 128),
                        DecisionDifficultyComment = c.String(),
                        DecisionDifficultyString = c.String(),
                        DecisionDifficultyId = c.String(maxLength: 128),
                        DepartmentString = c.String(),
                        DepartmentId = c.String(maxLength: 128),
                        DocumentationPresentComment = c.String(),
                        DocumentationPresentString = c.String(),
                        DocumentationPresentId = c.String(maxLength: 128),
                        EaseOfImplementationFinal = c.String(),
                        EmployeeCount = c.Int(),
                        EmployeeCountComment = c.String(),
                        InputComment = c.String(),
                        InputDataStructureString = c.String(),
                        InputDataStructureId = c.String(maxLength: 128),
                        InputString = c.String(),
                        InputId = c.String(maxLength: 128),
                        IsAlternative = c.Boolean(nullable: false),
                        IsDataInputScanned = c.Boolean(nullable: false),
                        IsDataSensitive = c.Boolean(nullable: false),
                        IsDraft = c.Boolean(nullable: false),
                        IsHighRisk = c.Boolean(nullable: false),
                        IsHostUpgrade = c.Boolean(nullable: false),
                        LessenLearnt = c.String(),
                        Name = c.String(),
                        NegativeImpactComment = c.String(),
                        NumberOfWaysToCompleteProcessComment = c.String(),
                        NumberOfWaysToCompleteProcessString = c.String(),
                        NumberOfWaysToCompleteProcessId = c.String(maxLength: 128),
                        PainPointComment = c.String(),
                        PotentialFineAmount = c.Decimal(precision: 18, scale: 2),
                        PotentialFineProbability = c.Decimal(precision: 18, scale: 2),
                        ProcessString = c.String(),
                        ProcessId = c.String(maxLength: 128),
                        ProcessOwnerString = c.String(),
                        ProcessOwnerId = c.String(maxLength: 128),
                        ProcessPeakComment = c.String(),
                        ProcessPeakString = c.String(),
                        ProcessPeakId = c.String(maxLength: 128),
                        ProcessStabilityComment = c.String(),
                        ProcessStabilityString = c.String(),
                        ProcessStabilityId = c.String(maxLength: 128),
                        ProcessVolumetryPerMonth = c.Decimal(precision: 18, scale: 2),
                        ProcessVolumetryPerYear = c.Decimal(precision: 18, scale: 2),
                        Rating = c.Int(),
                        RatingComment = c.String(),
                        RobotSpeedMultiplier = c.Decimal(precision: 18, scale: 2),
                        RobotWorkDayYear = c.Decimal(precision: 18, scale: 2),
                        RobotWorkHourDay = c.Decimal(precision: 18, scale: 2),
                        RuleComment = c.String(),
                        RuleString = c.String(),
                        RuleId = c.String(maxLength: 128),
                        RunningCostString = c.String(),
                        RunningCostId = c.String(maxLength: 128),
                        StructureComment = c.String(),
                        SubmissionPathString = c.String(),
                        SubmissionPathId = c.String(maxLength: 128),
                        SubTitle = c.String(),
                        Summary = c.String(),
                        TaskFrequencyComment = c.String(),
                        TaskFrequencyString = c.String(),
                        TaskFrequencyId = c.String(maxLength: 128),
                        TeamString = c.String(),
                        TeamId = c.String(maxLength: 128),
                        WorkingHour = c.Decimal(precision: 18, scale: 2),
                        WorkingHourComment = c.String(),
                        WorkloadSplit = c.Decimal(precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationStabilities", t => t.ApplicationStabilityId)
                .ForeignKey("dbo.AutomationGoals", t => t.AutomationGoalId)
                .ForeignKey("dbo.AverageNumberOfSteps", t => t.AverageNumberOfStepId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.DataInputPercentOfStructureds", t => t.DataInputPercentOfStructuredId)
                .ForeignKey("dbo.DecisionCounts", t => t.DecisionCountId)
                .ForeignKey("dbo.DecisionDifficulties", t => t.DecisionDifficultyId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .ForeignKey("dbo.Processes", t => t.ProcessId)
                .ForeignKey("dbo.DocumentationPresents", t => t.DocumentationPresentId)
                .ForeignKey("dbo.RunningCosts", t => t.RunningCostId)
                .ForeignKey("dbo.Inputs", t => t.InputId)
                .ForeignKey("dbo.InputDataStructures", t => t.InputDataStructureId)
                .ForeignKey("dbo.NumberOfWaysToCompleteProcesses", t => t.NumberOfWaysToCompleteProcessId)
                .ForeignKey("dbo.Users", t => t.ProcessOwnerId)
                .ForeignKey("dbo.ProcessPeaks", t => t.ProcessPeakId)
                .ForeignKey("dbo.ProcessStabilities", t => t.ProcessStabilityId)
                .ForeignKey("dbo.Rules", t => t.RuleId)
                .ForeignKey("dbo.SubmissionPaths", t => t.SubmissionPathId)
                .ForeignKey("dbo.TaskFrequencies", t => t.TaskFrequencyId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.ApplicationStabilityId)
                .Index(t => t.AutomationGoalId)
                .Index(t => t.AverageNumberOfStepId)
                .Index(t => t.ClientId)
                .Index(t => t.DataInputPercentOfStructuredId)
                .Index(t => t.DecisionCountId)
                .Index(t => t.DecisionDifficultyId)
                .Index(t => t.DepartmentId)
                .Index(t => t.DocumentationPresentId)
                .Index(t => t.InputDataStructureId)
                .Index(t => t.InputId)
                .Index(t => t.NumberOfWaysToCompleteProcessId)
                .Index(t => t.ProcessId)
                .Index(t => t.ProcessOwnerId)
                .Index(t => t.ProcessPeakId)
                .Index(t => t.ProcessStabilityId)
                .Index(t => t.RuleId)
                .Index(t => t.RunningCostId)
                .Index(t => t.SubmissionPathId)
                .Index(t => t.TaskFrequencyId)
                .Index(t => t.TeamId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ApplicationStabilities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Colour = c.String(),
                        Name = c.String(),
                        Rating = c.Int(nullable: false),
                        Weighting = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.AutomationGoals",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.AverageNumberOfSteps",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Colour = c.String(),
                        Name = c.String(),
                        ShortName = c.String(),
                        Weighting = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Collaborators",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IdeaString = c.String(),
                        IdeaId = c.String(maxLength: 128),
                        InvitedByString = c.String(),
                        InvitedById = c.String(maxLength: 128),
                        IsInvitationConfirmed = c.Boolean(nullable: false),
                        UserString = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        User_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Ideas", t => t.IdeaId)
                .ForeignKey("dbo.Users", t => t.InvitedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .Index(t => t.IdeaId)
                .Index(t => t.InvitedById)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.CollaboratorRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CollaboratorString = c.String(),
                        CollaboratorId = c.String(maxLength: 128),
                        RoleString = c.String(),
                        RoleId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Collaborators", t => t.CollaboratorId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.BusinessRoles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CollaboratorId)
                .Index(t => t.RoleId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.BusinessRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        Description = c.String(),
                        IsBuiltIn = c.Boolean(nullable: false),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.ImplementationCosts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Allocation = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        Day = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdeaStageString = c.String(),
                        IdeaStageId = c.String(maxLength: 128),
                        RoleString = c.String(),
                        RoleId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.IdeaStages", t => t.IdeaStageId)
                .ForeignKey("dbo.BusinessRoles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.IdeaStageId)
                .Index(t => t.RoleId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.IdeaStages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateEnd = c.DateTime(),
                        DateEndEstimate = c.DateTime(),
                        DateStart = c.DateTime(),
                        DateStartEstimate = c.DateTime(nullable: false),
                        IdeaString = c.String(),
                        IdeaId = c.String(maxLength: 128),
                        IsInWorkFlow = c.Boolean(nullable: false),
                        StageString = c.String(),
                        StageId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Ideas", t => t.IdeaId)
                .ForeignKey("dbo.Stages", t => t.StageId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.IdeaId)
                .Index(t => t.StageId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.IdeaStageStatus",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        IdeaStageString = c.String(),
                        IdeaStageId = c.String(maxLength: 128),
                        StatusString = c.String(),
                        StatusId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.IdeaStages", t => t.IdeaStageId)
                .ForeignKey("dbo.IdeaStatus", t => t.StatusId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.IdeaStageId)
                .Index(t => t.StatusId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.IdeaStatus",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ButtonClass = c.String(),
                        IsPathToSuccess = c.Boolean(nullable: false),
                        Name = c.String(),
                        RequireIdeaStageField = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        StageString = c.String(),
                        StageId = c.String(maxLength: 128),
                        TextClass = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Stages", t => t.StageId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.StageId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Stages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CanAssignCost = c.Boolean(nullable: false),
                        IsMileStone = c.Boolean(nullable: false),
                        Name = c.String(),
                        SetDateAutomatically = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        StageGroupString = c.String(),
                        StageGroupId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.StageGroups", t => t.StageGroupId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.StageGroupId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.StageGroups",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.RoleCosts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RoleString = c.String(),
                        RoleId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.BusinessRoles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.RoleId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.RoleIdeaAuthorisations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        IdeaAuthorisationString = c.String(),
                        IdeaAuthorisationId = c.String(maxLength: 128),
                        RoleString = c.String(),
                        RoleId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.IdeaAuthorisations", t => t.IdeaAuthorisationId)
                .ForeignKey("dbo.BusinessRoles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.IdeaAuthorisationId)
                .Index(t => t.RoleId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.IdeaAuthorisations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.UserAuthorisations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CollaboratorRoleString = c.String(),
                        CollaboratorRoleId = c.String(maxLength: 128),
                        IdeaAuthorisationString = c.String(),
                        IdeaAuthorisationId = c.String(maxLength: 128),
                        IdeaString = c.String(),
                        IdeaId = c.String(maxLength: 128),
                        UserString = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CollaboratorRoles", t => t.CollaboratorRoleId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Ideas", t => t.IdeaId)
                .ForeignKey("dbo.IdeaAuthorisations", t => t.IdeaAuthorisationId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.CollaboratorRoleId)
                .Index(t => t.IdeaAuthorisationId)
                .Index(t => t.IdeaId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        ComponentId = c.String(),
                        IdeaString = c.String(),
                        IdeaId = c.String(maxLength: 128),
                        SenderString = c.String(),
                        SenderId = c.String(maxLength: 128),
                        Text = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Ideas", t => t.IdeaId)
                .ForeignKey("dbo.Users", t => t.SenderId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.ClientId)
                .Index(t => t.IdeaId)
                .Index(t => t.SenderId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Recipients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CommentString = c.String(),
                        CommentId = c.String(maxLength: 128),
                        UserString = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.CommentId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.DataInputPercentOfStructureds",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Colour = c.String(),
                        Name = c.String(),
                        Weighting = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.DecisionCounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Colour = c.String(),
                        Name = c.String(),
                        Weighting = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.DecisionDifficulties",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Colour = c.String(),
                        Name = c.String(),
                        ShortName = c.String(),
                        Weighting = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        DepartmentString = c.String(),
                        DepartmentId = c.String(maxLength: 128),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.DepartmentId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Processes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        Name = c.String(),
                        TeamString = c.String(),
                        TeamId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.TeamId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.DocumentationPresents",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Colour = c.String(),
                        Name = c.String(),
                        Rating = c.Int(nullable: false),
                        Weighting = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        Filename = c.String(),
                        FilenameBackend = c.String(),
                        IdeaString = c.String(),
                        IdeaId = c.String(maxLength: 128),
                        Text = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Ideas", t => t.IdeaId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.IdeaId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IdeaString = c.String(),
                        IdeaId = c.String(maxLength: 128),
                        UserString = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Ideas", t => t.IdeaId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.IdeaId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.IdeaApplicationVersions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IdeaString = c.String(),
                        IdeaId = c.String(maxLength: 128),
                        IsThinClient = c.Boolean(nullable: false),
                        LanguageString = c.String(),
                        LanguageId = c.String(maxLength: 128),
                        VersionString = c.String(),
                        VersionId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Ideas", t => t.IdeaId)
                .ForeignKey("dbo.Languages", t => t.LanguageId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Versions", t => t.VersionId)
                .Index(t => t.IdeaId)
                .Index(t => t.LanguageId)
                .Index(t => t.VersionId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Locale = c.String(),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Versions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ApplicationString = c.String(),
                        ApplicationId = c.String(maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        IsLive = c.Boolean(nullable: false),
                        Name = c.String(),
                        PlannedUpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.ApplicationId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ApplicationId)
                .Index(t => t.ClientId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.IdeaRunningCosts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        IdeaString = c.String(),
                        IdeaId = c.String(maxLength: 128),
                        LicenceCount = c.Int(nullable: false),
                        RunningCostString = c.String(),
                        RunningCostId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Ideas", t => t.IdeaId)
                .ForeignKey("dbo.RunningCosts", t => t.RunningCostId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.IdeaId)
                .Index(t => t.RunningCostId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.RunningCosts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AutomationTypeString = c.String(),
                        AutomationTypeId = c.String(maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FrequencyString = c.String(),
                        FrequencyId = c.String(maxLength: 128),
                        IsLive = c.Boolean(nullable: false),
                        LicenceType = c.String(),
                        VenderString = c.String(),
                        VenderId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutomationTypes", t => t.AutomationTypeId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Periods", t => t.FrequencyId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.SoftwareVenders", t => t.VenderId)
                .Index(t => t.AutomationTypeId)
                .Index(t => t.ClientId)
                .Index(t => t.FrequencyId)
                .Index(t => t.VenderId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.AutomationTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.SoftwareVenders",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        IsLive = c.Boolean(nullable: false),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Inputs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Colour = c.String(),
                        Name = c.String(),
                        Rating = c.Int(nullable: false),
                        Weighting = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.InputDataStructures",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Colour = c.String(),
                        Name = c.String(),
                        Rating = c.Int(nullable: false),
                        Weighting = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.NumberOfWaysToCompleteProcesses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Colour = c.String(),
                        Name = c.String(),
                        ShortName = c.String(),
                        Weighting = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.ProcessPeaks",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Colour = c.String(),
                        Name = c.String(),
                        Weighting = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.ProcessStabilities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Colour = c.String(),
                        Name = c.String(),
                        Rating = c.Int(nullable: false),
                        Weighting = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Colour = c.String(),
                        Name = c.String(),
                        Rating = c.Int(nullable: false),
                        Weighting = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.SubmissionPaths",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.TaskFrequencies",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Hour = c.Int(nullable: false),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IdeaString = c.String(),
                        IdeaId = c.String(maxLength: 128),
                        UserString = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Ideas", t => t.IdeaId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.IdeaId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AdminUserLimit = c.Int(),
                        AdminUserText = c.String(),
                        CollaboratorLimit = c.Int(),
                        CollaboratorText = c.String(),
                        IdeaLimit = c.Int(),
                        IdeaText = c.String(),
                        IsLive = c.Boolean(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                        Name = c.String(),
                        NoPrice = c.Boolean(nullable: false),
                        Note = c.String(),
                        Sort = c.Int(nullable: false),
                        StandardUserLimit = c.Int(),
                        StandardUserText = c.String(),
                        StorageLimit = c.Int(),
                        StorageText = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Industries",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.ClientId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientString = c.String(),
                        ClientId = c.String(maxLength: 128),
                        Subject = c.String(),
                        Text = c.String(),
                        UserString = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.ClientId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Analytics",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Action = c.String(),
                        Date = c.DateTime(nullable: false),
                        Language = c.String(),
                        Platform = c.String(),
                        SessionTracker = c.String(),
                        TimeZone = c.String(),
                        URL = c.String(),
                        UserAgent = c.String(),
                        UserColour = c.String(),
                        UserString = c.String(),
                        UserId = c.String(maxLength: 128),
                        UserTracker = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserAchievements",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AchievementString = c.String(),
                        AchievementId = c.String(maxLength: 128),
                        UserString = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Achievements", t => t.AchievementId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.AchievementId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.UserBadges",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BadgeString = c.String(),
                        BadgeId = c.String(maxLength: 128),
                        UserString = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Badges", t => t.BadgeId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.BadgeId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Badges",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RoleString = c.String(),
                        RoleId = c.String(maxLength: 128),
                        UserString = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        Name = c.String(),
                        PolicyCount = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.HotSpots",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Text = c.String(),
                        Width = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CanDelete = c.Boolean(nullable: false),
                        IsMenuItem = c.Boolean(nullable: false),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        Text = c.String(),
                        TextHeight = c.Decimal(precision: 18, scale: 2),
                        URL = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Fullname = c.String(),
                        FunctionName = c.String(),
                        InnerException = c.String(),
                        Message = c.String(),
                        RequestId = c.String(),
                        Severity = c.Int(nullable: false),
                        Source = c.String(),
                        StackTrace = c.String(),
                        TargetSite = c.String(),
                        Text = c.String(),
                        Username = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.WebHookLogs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        KeyId = c.String(),
                        SourceId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        IsSaved = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebHookLogs", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.WebHookLogs", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Logs", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Logs", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Settings", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Settings", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Pages", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Pages", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.HotSpots", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.HotSpots", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ManageTenants", "UserId", "dbo.Users");
            DropForeignKey("dbo.ManageTenants", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.ManageTenants", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Votes", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Roles", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Roles", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.UserBadges", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserBadges", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserBadges", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.UserBadges", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.UserBadges", "BadgeId", "dbo.Badges");
            DropForeignKey("dbo.Badges", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Badges", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.UserAuthorisations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserAchievements", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserAchievements", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAchievements", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.UserAchievements", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.UserAchievements", "AchievementId", "dbo.Achievements");
            DropForeignKey("dbo.Achievements", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Achievements", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Users", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Users", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Recipients", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ManageTenants", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "ManagerId", "dbo.Users");
            DropForeignKey("dbo.Users", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Collaborators", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.Ideas", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Follows", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Users", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Collaborators", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Analytics", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Analytics", "UserId", "dbo.Users");
            DropForeignKey("dbo.Analytics", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Analytics", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Clients", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Clients", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Subscriptions", "Client_Id1", "dbo.Clients");
            DropForeignKey("dbo.Clients", "Client_Id1", "dbo.Clients");
            DropForeignKey("dbo.Clients", "PracticeAccount_Id", "dbo.Clients");
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Messages", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Messages", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ManageTenants", "TenantId", "dbo.Clients");
            DropForeignKey("dbo.Users", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Locations", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Locations", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Locations", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Industries", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Industries", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Clients", "IndustryId", "dbo.Industries");
            DropForeignKey("dbo.Clients", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Clients", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Subscriptions", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Discounts", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Subscriptions", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Subscriptions", "TenantId", "dbo.Clients");
            DropForeignKey("dbo.Prices", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Subscriptions", "PriceId", "dbo.Prices");
            DropForeignKey("dbo.Products", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Prices", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Periods", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Prices", "PeriodId", "dbo.Periods");
            DropForeignKey("dbo.OtherRunningCosts", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaOtherRunningCosts", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaOtherRunningCosts", "OtherRunningCostId", "dbo.OtherRunningCosts");
            DropForeignKey("dbo.Votes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Votes", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Votes", "IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.Votes", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.TaskFrequencies", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "TaskFrequencyId", "dbo.TaskFrequencies");
            DropForeignKey("dbo.TaskFrequencies", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.SubmissionPaths", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "SubmissionPathId", "dbo.SubmissionPaths");
            DropForeignKey("dbo.SubmissionPaths", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Rules", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "RuleId", "dbo.Rules");
            DropForeignKey("dbo.Rules", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ProcessStabilities", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "ProcessStabilityId", "dbo.ProcessStabilities");
            DropForeignKey("dbo.ProcessStabilities", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ProcessPeaks", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "ProcessPeakId", "dbo.ProcessPeaks");
            DropForeignKey("dbo.ProcessPeaks", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "ProcessOwnerId", "dbo.Users");
            DropForeignKey("dbo.NumberOfWaysToCompleteProcesses", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "NumberOfWaysToCompleteProcessId", "dbo.NumberOfWaysToCompleteProcesses");
            DropForeignKey("dbo.NumberOfWaysToCompleteProcesses", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.InputDataStructures", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "InputDataStructureId", "dbo.InputDataStructures");
            DropForeignKey("dbo.InputDataStructures", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Inputs", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "InputId", "dbo.Inputs");
            DropForeignKey("dbo.Inputs", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaRunningCosts", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.SoftwareVenders", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.RunningCosts", "VenderId", "dbo.SoftwareVenders");
            DropForeignKey("dbo.SoftwareVenders", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.SoftwareVenders", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.RunningCosts", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "RunningCostId", "dbo.RunningCosts");
            DropForeignKey("dbo.IdeaRunningCosts", "RunningCostId", "dbo.RunningCosts");
            DropForeignKey("dbo.RunningCosts", "FrequencyId", "dbo.Periods");
            DropForeignKey("dbo.RunningCosts", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.RunningCosts", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.AutomationTypes", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.RunningCosts", "AutomationTypeId", "dbo.AutomationTypes");
            DropForeignKey("dbo.AutomationTypes", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaRunningCosts", "IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.IdeaRunningCosts", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaRunningCosts", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.IdeaOtherRunningCosts", "IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.Versions", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaApplicationVersions", "VersionId", "dbo.Versions");
            DropForeignKey("dbo.Versions", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Versions", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Versions", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Applications", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Applications", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Applications", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.IdeaApplicationVersions", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Languages", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaApplicationVersions", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.Languages", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Clients", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.IdeaApplicationVersions", "IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.IdeaApplicationVersions", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Follows", "UserId", "dbo.Users");
            DropForeignKey("dbo.Follows", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Follows", "IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.Follows", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Documents", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Documents", "IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.Documents", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Documents", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.DocumentationPresents", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "DocumentationPresentId", "dbo.DocumentationPresents");
            DropForeignKey("dbo.DocumentationPresents", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Users", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Departments", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Teams", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Processes", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Processes", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Ideas", "ProcessId", "dbo.Processes");
            DropForeignKey("dbo.Processes", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Processes", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Ideas", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Teams", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Teams", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Ideas", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Departments", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.DecisionDifficulties", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "DecisionDifficultyId", "dbo.DecisionDifficulties");
            DropForeignKey("dbo.DecisionDifficulties", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.DecisionCounts", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "DecisionCountId", "dbo.DecisionCounts");
            DropForeignKey("dbo.DecisionCounts", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.DataInputPercentOfStructureds", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "DataInputPercentOfStructuredId", "dbo.DataInputPercentOfStructureds");
            DropForeignKey("dbo.DataInputPercentOfStructureds", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Comments", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Comments", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Recipients", "UserId", "dbo.Users");
            DropForeignKey("dbo.Recipients", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Recipients", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Recipients", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.Comments", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Comments", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Collaborators", "UserId", "dbo.Users");
            DropForeignKey("dbo.Collaborators", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Collaborators", "InvitedById", "dbo.Users");
            DropForeignKey("dbo.Collaborators", "IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.Collaborators", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.CollaboratorRoles", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.BusinessRoles", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.RoleIdeaAuthorisations", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.RoleIdeaAuthorisations", "RoleId", "dbo.BusinessRoles");
            DropForeignKey("dbo.UserAuthorisations", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAuthorisations", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.UserAuthorisations", "IdeaAuthorisationId", "dbo.IdeaAuthorisations");
            DropForeignKey("dbo.UserAuthorisations", "IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.UserAuthorisations", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.UserAuthorisations", "CollaboratorRoleId", "dbo.CollaboratorRoles");
            DropForeignKey("dbo.IdeaAuthorisations", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.RoleIdeaAuthorisations", "IdeaAuthorisationId", "dbo.IdeaAuthorisations");
            DropForeignKey("dbo.IdeaAuthorisations", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.RoleIdeaAuthorisations", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.RoleIdeaAuthorisations", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.RoleCosts", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.RoleCosts", "RoleId", "dbo.BusinessRoles");
            DropForeignKey("dbo.RoleCosts", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.RoleCosts", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ImplementationCosts", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.ImplementationCosts", "RoleId", "dbo.BusinessRoles");
            DropForeignKey("dbo.IdeaStages", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.ImplementationCosts", "IdeaStageId", "dbo.IdeaStages");
            DropForeignKey("dbo.IdeaStageStatus", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaStatus", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Stages", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.StageGroups", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Stages", "StageGroupId", "dbo.StageGroups");
            DropForeignKey("dbo.StageGroups", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaStatus", "StageId", "dbo.Stages");
            DropForeignKey("dbo.IdeaStages", "StageId", "dbo.Stages");
            DropForeignKey("dbo.Stages", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaStageStatus", "StatusId", "dbo.IdeaStatus");
            DropForeignKey("dbo.IdeaStatus", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaStageStatus", "IdeaStageId", "dbo.IdeaStages");
            DropForeignKey("dbo.IdeaStageStatus", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaStages", "IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.IdeaStages", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ImplementationCosts", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ImplementationCosts", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.BusinessRoles", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.CollaboratorRoles", "RoleId", "dbo.BusinessRoles");
            DropForeignKey("dbo.BusinessRoles", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.CollaboratorRoles", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.CollaboratorRoles", "CollaboratorId", "dbo.Collaborators");
            DropForeignKey("dbo.Ideas", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.AverageNumberOfSteps", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "AverageNumberOfStepId", "dbo.AverageNumberOfSteps");
            DropForeignKey("dbo.AverageNumberOfSteps", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.AutomationGoals", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "AutomationGoalId", "dbo.AutomationGoals");
            DropForeignKey("dbo.AutomationGoals", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ApplicationStabilities", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Ideas", "ApplicationStabilityId", "dbo.ApplicationStabilities");
            DropForeignKey("dbo.ApplicationStabilities", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaOtherRunningCosts", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.IdeaOtherRunningCosts", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.OtherRunningCosts", "FrequencyId", "dbo.Periods");
            DropForeignKey("dbo.OtherRunningCosts", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.CostTypes", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.OtherRunningCosts", "CostTypeId", "dbo.CostTypes");
            DropForeignKey("dbo.CostTypes", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.OtherRunningCosts", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Periods", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Currencies", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Prices", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Currencies", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Clients", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Prices", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Subscriptions", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Coupons", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Subscriptions", "CouponId", "dbo.Coupons");
            DropForeignKey("dbo.Coupons", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ClientTypes", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Subscriptions", "AgencyTypeId", "dbo.ClientTypes");
            DropForeignKey("dbo.Prospects", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.JobLevels", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Prospects", "JobLevelId", "dbo.JobLevels");
            DropForeignKey("dbo.JobLevels", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Prospects", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Countries", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Prospects", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Countries", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Clients", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.CompanySizes", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Prospects", "CompanySizeId", "dbo.CompanySizes");
            DropForeignKey("dbo.CompanySizes", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Prospects", "ClientTypeId", "dbo.ClientTypes");
            DropForeignKey("dbo.ClientTypes", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Clients", "TypeId", "dbo.ClientTypes");
            DropForeignKey("dbo.Subscriptions", "AgencyDiscountId", "dbo.Discounts");
            DropForeignKey("dbo.Subscriptions", "AgencyId", "dbo.Clients");
            DropForeignKey("dbo.Discounts", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Clients", "AgencyDiscountId", "dbo.Discounts");
            DropForeignKey("dbo.Clients", "AgencyId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "AccountOwnerId", "dbo.Users");
            DropIndex("dbo.WebHookLogs", new[] { "UpdatedById" });
            DropIndex("dbo.WebHookLogs", new[] { "CreatedById" });
            DropIndex("dbo.Logs", new[] { "UpdatedById" });
            DropIndex("dbo.Logs", new[] { "CreatedById" });
            DropIndex("dbo.Settings", new[] { "UpdatedById" });
            DropIndex("dbo.Settings", new[] { "CreatedById" });
            DropIndex("dbo.Pages", new[] { "UpdatedById" });
            DropIndex("dbo.Pages", new[] { "CreatedById" });
            DropIndex("dbo.HotSpots", new[] { "UpdatedById" });
            DropIndex("dbo.HotSpots", new[] { "CreatedById" });
            DropIndex("dbo.Roles", new[] { "UpdatedById" });
            DropIndex("dbo.Roles", new[] { "CreatedById" });
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            DropIndex("dbo.UserRoles", new[] { "UpdatedById" });
            DropIndex("dbo.UserRoles", new[] { "CreatedById" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Badges", new[] { "UpdatedById" });
            DropIndex("dbo.Badges", new[] { "CreatedById" });
            DropIndex("dbo.UserBadges", new[] { "User_Id" });
            DropIndex("dbo.UserBadges", new[] { "UpdatedById" });
            DropIndex("dbo.UserBadges", new[] { "CreatedById" });
            DropIndex("dbo.UserBadges", new[] { "UserId" });
            DropIndex("dbo.UserBadges", new[] { "BadgeId" });
            DropIndex("dbo.Achievements", new[] { "UpdatedById" });
            DropIndex("dbo.Achievements", new[] { "CreatedById" });
            DropIndex("dbo.UserAchievements", new[] { "User_Id" });
            DropIndex("dbo.UserAchievements", new[] { "UpdatedById" });
            DropIndex("dbo.UserAchievements", new[] { "CreatedById" });
            DropIndex("dbo.UserAchievements", new[] { "UserId" });
            DropIndex("dbo.UserAchievements", new[] { "AchievementId" });
            DropIndex("dbo.Analytics", new[] { "User_Id" });
            DropIndex("dbo.Analytics", new[] { "UpdatedById" });
            DropIndex("dbo.Analytics", new[] { "CreatedById" });
            DropIndex("dbo.Analytics", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "User_Id" });
            DropIndex("dbo.Messages", new[] { "UpdatedById" });
            DropIndex("dbo.Messages", new[] { "CreatedById" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "ClientId" });
            DropIndex("dbo.Locations", new[] { "UpdatedById" });
            DropIndex("dbo.Locations", new[] { "CreatedById" });
            DropIndex("dbo.Locations", new[] { "ClientId" });
            DropIndex("dbo.Industries", new[] { "UpdatedById" });
            DropIndex("dbo.Industries", new[] { "CreatedById" });
            DropIndex("dbo.Products", new[] { "UpdatedById" });
            DropIndex("dbo.Products", new[] { "CreatedById" });
            DropIndex("dbo.Votes", new[] { "User_Id" });
            DropIndex("dbo.Votes", new[] { "UpdatedById" });
            DropIndex("dbo.Votes", new[] { "CreatedById" });
            DropIndex("dbo.Votes", new[] { "UserId" });
            DropIndex("dbo.Votes", new[] { "IdeaId" });
            DropIndex("dbo.TaskFrequencies", new[] { "UpdatedById" });
            DropIndex("dbo.TaskFrequencies", new[] { "CreatedById" });
            DropIndex("dbo.SubmissionPaths", new[] { "UpdatedById" });
            DropIndex("dbo.SubmissionPaths", new[] { "CreatedById" });
            DropIndex("dbo.Rules", new[] { "UpdatedById" });
            DropIndex("dbo.Rules", new[] { "CreatedById" });
            DropIndex("dbo.ProcessStabilities", new[] { "UpdatedById" });
            DropIndex("dbo.ProcessStabilities", new[] { "CreatedById" });
            DropIndex("dbo.ProcessPeaks", new[] { "UpdatedById" });
            DropIndex("dbo.ProcessPeaks", new[] { "CreatedById" });
            DropIndex("dbo.NumberOfWaysToCompleteProcesses", new[] { "UpdatedById" });
            DropIndex("dbo.NumberOfWaysToCompleteProcesses", new[] { "CreatedById" });
            DropIndex("dbo.InputDataStructures", new[] { "UpdatedById" });
            DropIndex("dbo.InputDataStructures", new[] { "CreatedById" });
            DropIndex("dbo.Inputs", new[] { "UpdatedById" });
            DropIndex("dbo.Inputs", new[] { "CreatedById" });
            DropIndex("dbo.SoftwareVenders", new[] { "UpdatedById" });
            DropIndex("dbo.SoftwareVenders", new[] { "CreatedById" });
            DropIndex("dbo.SoftwareVenders", new[] { "ClientId" });
            DropIndex("dbo.AutomationTypes", new[] { "UpdatedById" });
            DropIndex("dbo.AutomationTypes", new[] { "CreatedById" });
            DropIndex("dbo.RunningCosts", new[] { "UpdatedById" });
            DropIndex("dbo.RunningCosts", new[] { "CreatedById" });
            DropIndex("dbo.RunningCosts", new[] { "VenderId" });
            DropIndex("dbo.RunningCosts", new[] { "FrequencyId" });
            DropIndex("dbo.RunningCosts", new[] { "ClientId" });
            DropIndex("dbo.RunningCosts", new[] { "AutomationTypeId" });
            DropIndex("dbo.IdeaRunningCosts", new[] { "UpdatedById" });
            DropIndex("dbo.IdeaRunningCosts", new[] { "CreatedById" });
            DropIndex("dbo.IdeaRunningCosts", new[] { "RunningCostId" });
            DropIndex("dbo.IdeaRunningCosts", new[] { "IdeaId" });
            DropIndex("dbo.IdeaRunningCosts", new[] { "ClientId" });
            DropIndex("dbo.Applications", new[] { "UpdatedById" });
            DropIndex("dbo.Applications", new[] { "CreatedById" });
            DropIndex("dbo.Applications", new[] { "ClientId" });
            DropIndex("dbo.Versions", new[] { "UpdatedById" });
            DropIndex("dbo.Versions", new[] { "CreatedById" });
            DropIndex("dbo.Versions", new[] { "ClientId" });
            DropIndex("dbo.Versions", new[] { "ApplicationId" });
            DropIndex("dbo.Languages", new[] { "UpdatedById" });
            DropIndex("dbo.Languages", new[] { "CreatedById" });
            DropIndex("dbo.IdeaApplicationVersions", new[] { "UpdatedById" });
            DropIndex("dbo.IdeaApplicationVersions", new[] { "CreatedById" });
            DropIndex("dbo.IdeaApplicationVersions", new[] { "VersionId" });
            DropIndex("dbo.IdeaApplicationVersions", new[] { "LanguageId" });
            DropIndex("dbo.IdeaApplicationVersions", new[] { "IdeaId" });
            DropIndex("dbo.Follows", new[] { "User_Id" });
            DropIndex("dbo.Follows", new[] { "UpdatedById" });
            DropIndex("dbo.Follows", new[] { "CreatedById" });
            DropIndex("dbo.Follows", new[] { "UserId" });
            DropIndex("dbo.Follows", new[] { "IdeaId" });
            DropIndex("dbo.Documents", new[] { "UpdatedById" });
            DropIndex("dbo.Documents", new[] { "CreatedById" });
            DropIndex("dbo.Documents", new[] { "IdeaId" });
            DropIndex("dbo.Documents", new[] { "ClientId" });
            DropIndex("dbo.DocumentationPresents", new[] { "UpdatedById" });
            DropIndex("dbo.DocumentationPresents", new[] { "CreatedById" });
            DropIndex("dbo.Processes", new[] { "UpdatedById" });
            DropIndex("dbo.Processes", new[] { "CreatedById" });
            DropIndex("dbo.Processes", new[] { "TeamId" });
            DropIndex("dbo.Processes", new[] { "ClientId" });
            DropIndex("dbo.Teams", new[] { "UpdatedById" });
            DropIndex("dbo.Teams", new[] { "CreatedById" });
            DropIndex("dbo.Teams", new[] { "DepartmentId" });
            DropIndex("dbo.Teams", new[] { "ClientId" });
            DropIndex("dbo.Departments", new[] { "UpdatedById" });
            DropIndex("dbo.Departments", new[] { "CreatedById" });
            DropIndex("dbo.Departments", new[] { "ClientId" });
            DropIndex("dbo.DecisionDifficulties", new[] { "UpdatedById" });
            DropIndex("dbo.DecisionDifficulties", new[] { "CreatedById" });
            DropIndex("dbo.DecisionCounts", new[] { "UpdatedById" });
            DropIndex("dbo.DecisionCounts", new[] { "CreatedById" });
            DropIndex("dbo.DataInputPercentOfStructureds", new[] { "UpdatedById" });
            DropIndex("dbo.DataInputPercentOfStructureds", new[] { "CreatedById" });
            DropIndex("dbo.Recipients", new[] { "User_Id" });
            DropIndex("dbo.Recipients", new[] { "UpdatedById" });
            DropIndex("dbo.Recipients", new[] { "CreatedById" });
            DropIndex("dbo.Recipients", new[] { "UserId" });
            DropIndex("dbo.Recipients", new[] { "CommentId" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "UpdatedById" });
            DropIndex("dbo.Comments", new[] { "CreatedById" });
            DropIndex("dbo.Comments", new[] { "SenderId" });
            DropIndex("dbo.Comments", new[] { "IdeaId" });
            DropIndex("dbo.Comments", new[] { "ClientId" });
            DropIndex("dbo.UserAuthorisations", new[] { "User_Id" });
            DropIndex("dbo.UserAuthorisations", new[] { "UpdatedById" });
            DropIndex("dbo.UserAuthorisations", new[] { "CreatedById" });
            DropIndex("dbo.UserAuthorisations", new[] { "UserId" });
            DropIndex("dbo.UserAuthorisations", new[] { "IdeaId" });
            DropIndex("dbo.UserAuthorisations", new[] { "IdeaAuthorisationId" });
            DropIndex("dbo.UserAuthorisations", new[] { "CollaboratorRoleId" });
            DropIndex("dbo.IdeaAuthorisations", new[] { "UpdatedById" });
            DropIndex("dbo.IdeaAuthorisations", new[] { "CreatedById" });
            DropIndex("dbo.RoleIdeaAuthorisations", new[] { "UpdatedById" });
            DropIndex("dbo.RoleIdeaAuthorisations", new[] { "CreatedById" });
            DropIndex("dbo.RoleIdeaAuthorisations", new[] { "RoleId" });
            DropIndex("dbo.RoleIdeaAuthorisations", new[] { "IdeaAuthorisationId" });
            DropIndex("dbo.RoleIdeaAuthorisations", new[] { "ClientId" });
            DropIndex("dbo.RoleCosts", new[] { "UpdatedById" });
            DropIndex("dbo.RoleCosts", new[] { "CreatedById" });
            DropIndex("dbo.RoleCosts", new[] { "RoleId" });
            DropIndex("dbo.RoleCosts", new[] { "ClientId" });
            DropIndex("dbo.StageGroups", new[] { "UpdatedById" });
            DropIndex("dbo.StageGroups", new[] { "CreatedById" });
            DropIndex("dbo.Stages", new[] { "UpdatedById" });
            DropIndex("dbo.Stages", new[] { "CreatedById" });
            DropIndex("dbo.Stages", new[] { "StageGroupId" });
            DropIndex("dbo.IdeaStatus", new[] { "UpdatedById" });
            DropIndex("dbo.IdeaStatus", new[] { "CreatedById" });
            DropIndex("dbo.IdeaStatus", new[] { "StageId" });
            DropIndex("dbo.IdeaStageStatus", new[] { "UpdatedById" });
            DropIndex("dbo.IdeaStageStatus", new[] { "CreatedById" });
            DropIndex("dbo.IdeaStageStatus", new[] { "StatusId" });
            DropIndex("dbo.IdeaStageStatus", new[] { "IdeaStageId" });
            DropIndex("dbo.IdeaStages", new[] { "UpdatedById" });
            DropIndex("dbo.IdeaStages", new[] { "CreatedById" });
            DropIndex("dbo.IdeaStages", new[] { "StageId" });
            DropIndex("dbo.IdeaStages", new[] { "IdeaId" });
            DropIndex("dbo.ImplementationCosts", new[] { "UpdatedById" });
            DropIndex("dbo.ImplementationCosts", new[] { "CreatedById" });
            DropIndex("dbo.ImplementationCosts", new[] { "RoleId" });
            DropIndex("dbo.ImplementationCosts", new[] { "IdeaStageId" });
            DropIndex("dbo.ImplementationCosts", new[] { "ClientId" });
            DropIndex("dbo.BusinessRoles", new[] { "UpdatedById" });
            DropIndex("dbo.BusinessRoles", new[] { "CreatedById" });
            DropIndex("dbo.BusinessRoles", new[] { "ClientId" });
            DropIndex("dbo.CollaboratorRoles", new[] { "UpdatedById" });
            DropIndex("dbo.CollaboratorRoles", new[] { "CreatedById" });
            DropIndex("dbo.CollaboratorRoles", new[] { "RoleId" });
            DropIndex("dbo.CollaboratorRoles", new[] { "CollaboratorId" });
            DropIndex("dbo.Collaborators", new[] { "User_Id1" });
            DropIndex("dbo.Collaborators", new[] { "User_Id" });
            DropIndex("dbo.Collaborators", new[] { "UpdatedById" });
            DropIndex("dbo.Collaborators", new[] { "CreatedById" });
            DropIndex("dbo.Collaborators", new[] { "UserId" });
            DropIndex("dbo.Collaborators", new[] { "InvitedById" });
            DropIndex("dbo.Collaborators", new[] { "IdeaId" });
            DropIndex("dbo.AverageNumberOfSteps", new[] { "UpdatedById" });
            DropIndex("dbo.AverageNumberOfSteps", new[] { "CreatedById" });
            DropIndex("dbo.AutomationGoals", new[] { "UpdatedById" });
            DropIndex("dbo.AutomationGoals", new[] { "CreatedById" });
            DropIndex("dbo.ApplicationStabilities", new[] { "UpdatedById" });
            DropIndex("dbo.ApplicationStabilities", new[] { "CreatedById" });
            DropIndex("dbo.Ideas", new[] { "User_Id" });
            DropIndex("dbo.Ideas", new[] { "UpdatedById" });
            DropIndex("dbo.Ideas", new[] { "CreatedById" });
            DropIndex("dbo.Ideas", new[] { "TeamId" });
            DropIndex("dbo.Ideas", new[] { "TaskFrequencyId" });
            DropIndex("dbo.Ideas", new[] { "SubmissionPathId" });
            DropIndex("dbo.Ideas", new[] { "RunningCostId" });
            DropIndex("dbo.Ideas", new[] { "RuleId" });
            DropIndex("dbo.Ideas", new[] { "ProcessStabilityId" });
            DropIndex("dbo.Ideas", new[] { "ProcessPeakId" });
            DropIndex("dbo.Ideas", new[] { "ProcessOwnerId" });
            DropIndex("dbo.Ideas", new[] { "ProcessId" });
            DropIndex("dbo.Ideas", new[] { "NumberOfWaysToCompleteProcessId" });
            DropIndex("dbo.Ideas", new[] { "InputId" });
            DropIndex("dbo.Ideas", new[] { "InputDataStructureId" });
            DropIndex("dbo.Ideas", new[] { "DocumentationPresentId" });
            DropIndex("dbo.Ideas", new[] { "DepartmentId" });
            DropIndex("dbo.Ideas", new[] { "DecisionDifficultyId" });
            DropIndex("dbo.Ideas", new[] { "DecisionCountId" });
            DropIndex("dbo.Ideas", new[] { "DataInputPercentOfStructuredId" });
            DropIndex("dbo.Ideas", new[] { "ClientId" });
            DropIndex("dbo.Ideas", new[] { "AverageNumberOfStepId" });
            DropIndex("dbo.Ideas", new[] { "AutomationGoalId" });
            DropIndex("dbo.Ideas", new[] { "ApplicationStabilityId" });
            DropIndex("dbo.IdeaOtherRunningCosts", new[] { "UpdatedById" });
            DropIndex("dbo.IdeaOtherRunningCosts", new[] { "CreatedById" });
            DropIndex("dbo.IdeaOtherRunningCosts", new[] { "OtherRunningCostId" });
            DropIndex("dbo.IdeaOtherRunningCosts", new[] { "IdeaId" });
            DropIndex("dbo.IdeaOtherRunningCosts", new[] { "ClientId" });
            DropIndex("dbo.CostTypes", new[] { "UpdatedById" });
            DropIndex("dbo.CostTypes", new[] { "CreatedById" });
            DropIndex("dbo.OtherRunningCosts", new[] { "UpdatedById" });
            DropIndex("dbo.OtherRunningCosts", new[] { "CreatedById" });
            DropIndex("dbo.OtherRunningCosts", new[] { "FrequencyId" });
            DropIndex("dbo.OtherRunningCosts", new[] { "CostTypeId" });
            DropIndex("dbo.OtherRunningCosts", new[] { "ClientId" });
            DropIndex("dbo.Periods", new[] { "UpdatedById" });
            DropIndex("dbo.Periods", new[] { "CreatedById" });
            DropIndex("dbo.Currencies", new[] { "UpdatedById" });
            DropIndex("dbo.Currencies", new[] { "CreatedById" });
            DropIndex("dbo.Prices", new[] { "UpdatedById" });
            DropIndex("dbo.Prices", new[] { "CreatedById" });
            DropIndex("dbo.Prices", new[] { "ProductId" });
            DropIndex("dbo.Prices", new[] { "PeriodId" });
            DropIndex("dbo.Prices", new[] { "CurrencyId" });
            DropIndex("dbo.Coupons", new[] { "UpdatedById" });
            DropIndex("dbo.Coupons", new[] { "CreatedById" });
            DropIndex("dbo.JobLevels", new[] { "UpdatedById" });
            DropIndex("dbo.JobLevels", new[] { "CreatedById" });
            DropIndex("dbo.Countries", new[] { "UpdatedById" });
            DropIndex("dbo.Countries", new[] { "CreatedById" });
            DropIndex("dbo.CompanySizes", new[] { "UpdatedById" });
            DropIndex("dbo.CompanySizes", new[] { "CreatedById" });
            DropIndex("dbo.Prospects", new[] { "UpdatedById" });
            DropIndex("dbo.Prospects", new[] { "CreatedById" });
            DropIndex("dbo.Prospects", new[] { "JobLevelId" });
            DropIndex("dbo.Prospects", new[] { "CountryId" });
            DropIndex("dbo.Prospects", new[] { "CompanySizeId" });
            DropIndex("dbo.Prospects", new[] { "ClientTypeId" });
            DropIndex("dbo.ClientTypes", new[] { "UpdatedById" });
            DropIndex("dbo.ClientTypes", new[] { "CreatedById" });
            DropIndex("dbo.Subscriptions", new[] { "Client_Id1" });
            DropIndex("dbo.Subscriptions", new[] { "Client_Id" });
            DropIndex("dbo.Subscriptions", new[] { "UpdatedById" });
            DropIndex("dbo.Subscriptions", new[] { "CreatedById" });
            DropIndex("dbo.Subscriptions", new[] { "TenantId" });
            DropIndex("dbo.Subscriptions", new[] { "PriceId" });
            DropIndex("dbo.Subscriptions", new[] { "CouponId" });
            DropIndex("dbo.Subscriptions", new[] { "AgencyTypeId" });
            DropIndex("dbo.Subscriptions", new[] { "AgencyId" });
            DropIndex("dbo.Subscriptions", new[] { "AgencyDiscountId" });
            DropIndex("dbo.Discounts", new[] { "UpdatedById" });
            DropIndex("dbo.Discounts", new[] { "CreatedById" });
            DropIndex("dbo.Clients", new[] { "User_Id" });
            DropIndex("dbo.Clients", new[] { "Client_Id1" });
            DropIndex("dbo.Clients", new[] { "PracticeAccount_Id" });
            DropIndex("dbo.Clients", new[] { "Client_Id" });
            DropIndex("dbo.Clients", new[] { "UpdatedById" });
            DropIndex("dbo.Clients", new[] { "CreatedById" });
            DropIndex("dbo.Clients", new[] { "TypeId" });
            DropIndex("dbo.Clients", new[] { "LanguageId" });
            DropIndex("dbo.Clients", new[] { "IndustryId" });
            DropIndex("dbo.Clients", new[] { "CurrencyId" });
            DropIndex("dbo.Clients", new[] { "CountryId" });
            DropIndex("dbo.Clients", new[] { "AgencyId" });
            DropIndex("dbo.Clients", new[] { "AgencyDiscountId" });
            DropIndex("dbo.Clients", new[] { "AccountOwnerId" });
            DropIndex("dbo.Users", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Client_Id" });
            DropIndex("dbo.Users", new[] { "Location_Id" });
            DropIndex("dbo.Users", new[] { "Department_Id" });
            DropIndex("dbo.Users", new[] { "UpdatedById" });
            DropIndex("dbo.Users", new[] { "CreatedById" });
            DropIndex("dbo.Users", new[] { "ManagerId" });
            DropIndex("dbo.Users", new[] { "LocationId" });
            DropIndex("dbo.Users", new[] { "DepartmentId" });
            DropIndex("dbo.Users", new[] { "ClientId" });
            DropIndex("dbo.ManageTenants", new[] { "User_Id" });
            DropIndex("dbo.ManageTenants", new[] { "UpdatedById" });
            DropIndex("dbo.ManageTenants", new[] { "CreatedById" });
            DropIndex("dbo.ManageTenants", new[] { "UserId" });
            DropIndex("dbo.ManageTenants", new[] { "TenantId" });
            DropTable("dbo.WebHookLogs");
            DropTable("dbo.Logs");
            DropTable("dbo.Settings");
            DropTable("dbo.Pages");
            DropTable("dbo.HotSpots");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Badges");
            DropTable("dbo.UserBadges");
            DropTable("dbo.Achievements");
            DropTable("dbo.UserAchievements");
            DropTable("dbo.Analytics");
            DropTable("dbo.Messages");
            DropTable("dbo.Locations");
            DropTable("dbo.Industries");
            DropTable("dbo.Products");
            DropTable("dbo.Votes");
            DropTable("dbo.TaskFrequencies");
            DropTable("dbo.SubmissionPaths");
            DropTable("dbo.Rules");
            DropTable("dbo.ProcessStabilities");
            DropTable("dbo.ProcessPeaks");
            DropTable("dbo.NumberOfWaysToCompleteProcesses");
            DropTable("dbo.InputDataStructures");
            DropTable("dbo.Inputs");
            DropTable("dbo.SoftwareVenders");
            DropTable("dbo.AutomationTypes");
            DropTable("dbo.RunningCosts");
            DropTable("dbo.IdeaRunningCosts");
            DropTable("dbo.Applications");
            DropTable("dbo.Versions");
            DropTable("dbo.Languages");
            DropTable("dbo.IdeaApplicationVersions");
            DropTable("dbo.Follows");
            DropTable("dbo.Documents");
            DropTable("dbo.DocumentationPresents");
            DropTable("dbo.Processes");
            DropTable("dbo.Teams");
            DropTable("dbo.Departments");
            DropTable("dbo.DecisionDifficulties");
            DropTable("dbo.DecisionCounts");
            DropTable("dbo.DataInputPercentOfStructureds");
            DropTable("dbo.Recipients");
            DropTable("dbo.Comments");
            DropTable("dbo.UserAuthorisations");
            DropTable("dbo.IdeaAuthorisations");
            DropTable("dbo.RoleIdeaAuthorisations");
            DropTable("dbo.RoleCosts");
            DropTable("dbo.StageGroups");
            DropTable("dbo.Stages");
            DropTable("dbo.IdeaStatus");
            DropTable("dbo.IdeaStageStatus");
            DropTable("dbo.IdeaStages");
            DropTable("dbo.ImplementationCosts");
            DropTable("dbo.BusinessRoles");
            DropTable("dbo.CollaboratorRoles");
            DropTable("dbo.Collaborators");
            DropTable("dbo.AverageNumberOfSteps");
            DropTable("dbo.AutomationGoals");
            DropTable("dbo.ApplicationStabilities");
            DropTable("dbo.Ideas");
            DropTable("dbo.IdeaOtherRunningCosts");
            DropTable("dbo.CostTypes");
            DropTable("dbo.OtherRunningCosts");
            DropTable("dbo.Periods");
            DropTable("dbo.Currencies");
            DropTable("dbo.Prices");
            DropTable("dbo.Coupons");
            DropTable("dbo.JobLevels");
            DropTable("dbo.Countries");
            DropTable("dbo.CompanySizes");
            DropTable("dbo.Prospects");
            DropTable("dbo.ClientTypes");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Discounts");
            DropTable("dbo.Clients");
            DropTable("dbo.Users");
            DropTable("dbo.ManageTenants");
        }
    }
}
