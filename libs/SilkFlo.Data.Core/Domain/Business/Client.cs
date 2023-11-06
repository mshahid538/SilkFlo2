using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class Client : Abstract
  {
    private string _id = "";
    private User _accountOwner;
    private string _accountOwnerId;
    private string _address1 = "";
    private string _address2 = "";
    private Discount _agencyDiscount;
    private string _agencyDiscountId;
    private Client _agency;
    private string _agencyId;
    private bool _allowGuestSignIn;
    private int _averageWorkingDay = 260;
    private Decimal _averageWorkingHour = 8M;
    private string _city = "";
    private Country _country;
    private string _countryId;
    private Currency _currency;
    private string _currencyId;
    private int? _freeTrialDay;
    private Industry _industry;
    private string _industryId;
    private bool _isActive = true;
    private bool _isDemo;
    private bool _isPractice;
    private Language _language;
    private string _languageId;
    private string _name = "";
    private string _postCode = "";
    private Client _practiceAccount;
    private string _practiceId;
    private bool _receiveMarketing;
    private string _state = "";
    private string _stripeId = "";
    private bool _termsOfUse;
    private ClientType _type;
    private string _typeId;
    private string _website = "";

    public Client() => this._createdDate = new System.DateTime?(System.DateTime.Now);

    public override bool IsNew => string.IsNullOrWhiteSpace(this.Id);

    public string Id
    {
      get => this._id;
      set
      {
        value = value?.Trim();
        if (this._id == value)
          return;
        this._id = value;
        this.IsSaved = false;
      }
    }

    public string AccountOwnerString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public User AccountOwner
    {
      get => this._accountOwner;
      set
      {
        if (this._accountOwner == value)
          return;
        this._accountOwner = value;
        this.AccountOwnerString = value == null ? "" : value.ToString();
      }
    }

    public string AccountOwnerId
    {
      get => this._accountOwner != null ? this._accountOwner.Id : this._accountOwnerId;
      set
      {
        value = value?.Trim();
        this._accountOwnerId = value;
        if (this._accountOwner != null && this._accountOwner.Id != this._accountOwnerId)
          this._accountOwner = (User) null;
        this.IsSaved = false;
      }
    }

    public string Address1
    {
      get => this._address1;
      set
      {
        value = value?.Trim();
        if (this._address1 == value)
          return;
        this._address1 = value;
        this.IsSaved = false;
      }
    }

    public string Address2
    {
      get => this._address2;
      set
      {
        value = value?.Trim();
        if (this._address2 == value)
          return;
        this._address2 = value;
        this.IsSaved = false;
      }
    }

    public string AgencyDiscountString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Discount AgencyDiscount
    {
      get => this._agencyDiscount;
      set
      {
        if (this._agencyDiscount == value)
          return;
        this._agencyDiscount = value;
        this.AgencyDiscountString = value == null ? "" : value.ToString();
      }
    }

    public string AgencyDiscountId
    {
      get => this._agencyDiscount != null ? this._agencyDiscount.Id : this._agencyDiscountId;
      set
      {
        value = value?.Trim();
        this._agencyDiscountId = value;
        if (this._agencyDiscount != null && this._agencyDiscount.Id != this._agencyDiscountId)
          this._agencyDiscount = (Discount) null;
        this.IsSaved = false;
      }
    }

    public string AgencyString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Client Agency
    {
      get => this._agency;
      set
      {
        if (this._agency == value)
          return;
        this._agency = value;
        this.AgencyString = value == null ? "" : value.ToString();
      }
    }

    public string AgencyId
    {
      get => this._agency != null ? this._agency.Id : this._agencyId;
      set
      {
        value = value?.Trim();
        this._agencyId = value;
        if (this._agency != null && this._agency.Id != this._agencyId)
          this._agency = (Client) null;
        this.IsSaved = false;
      }
    }

    public bool AllowGuestSignIn
    {
      get => this._allowGuestSignIn;
      set
      {
        if (this._allowGuestSignIn == value)
          return;
        this._allowGuestSignIn = value;
        this.IsSaved = false;
      }
    }

    public int AverageWorkingDay
    {
      get => this._averageWorkingDay;
      set
      {
        if (this._averageWorkingDay == value)
          return;
        this._averageWorkingDay = value;
        this.IsSaved = false;
      }
    }

    public Decimal AverageWorkingHour
    {
      get => this._averageWorkingHour;
      set
      {
        if (this._averageWorkingHour == value)
          return;
        this._averageWorkingHour = value;
        this.IsSaved = false;
      }
    }

    public string City
    {
      get => this._city;
      set
      {
        value = value?.Trim();
        if (this._city == value)
          return;
        this._city = value;
        this.IsSaved = false;
      }
    }

    public string CountryString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Country Country
    {
      get => this._country;
      set
      {
        if (this._country == value)
          return;
        this._country = value;
        this.CountryString = value == null ? "" : value.ToString();
      }
    }

    public string CountryId
    {
      get => this._country != null ? this._country.Id : this._countryId;
      set
      {
        value = value?.Trim();
        this._countryId = value;
        if (this._country != null && this._country.Id != this._countryId)
          this._country = (Country) null;
        this.IsSaved = false;
      }
    }

    public string CurrencyString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Currency Currency
    {
      get => this._currency;
      set
      {
        if (this._currency == value)
          return;
        this._currency = value;
        this.CurrencyString = value == null ? "" : value.ToString();
      }
    }

    public string CurrencyId
    {
      get => this._currency != null ? this._currency.Id : this._currencyId;
      set
      {
        value = value?.Trim();
        this._currencyId = value;
        if (this._currency != null && this._currency.Id != this._currencyId)
          this._currency = (Currency) null;
        this.IsSaved = false;
      }
    }

    public int? FreeTrialDay
    {
      get => this._freeTrialDay;
      set
      {
        int? freeTrialDay = this._freeTrialDay;
        int? nullable = value;
        if (freeTrialDay.GetValueOrDefault() == nullable.GetValueOrDefault() & freeTrialDay.HasValue == nullable.HasValue)
          return;
        this._freeTrialDay = value;
        this.IsSaved = false;
      }
    }

    public string IndustryString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Industry Industry
    {
      get => this._industry;
      set
      {
        if (this._industry == value)
          return;
        this._industry = value;
        this.IndustryString = value == null ? "" : value.ToString();
      }
    }

    public string IndustryId
    {
      get => this._industry != null ? this._industry.Id : this._industryId;
      set
      {
        value = value?.Trim();
        this._industryId = value;
        if (this._industry != null && this._industry.Id != this._industryId)
          this._industry = (Industry) null;
        this.IsSaved = false;
      }
    }

    public bool IsActive
    {
      get => this._isActive;
      set
      {
        if (this._isActive == value)
          return;
        this._isActive = value;
        this.IsSaved = false;
      }
    }

    public bool IsDemo
    {
      get => this._isDemo;
      set
      {
        if (this._isDemo == value)
          return;
        this._isDemo = value;
        this.IsSaved = false;
      }
    }

    public bool IsPractice
    {
      get => this._isPractice;
      set
      {
        if (this._isPractice == value)
          return;
        this._isPractice = value;
        this.IsSaved = false;
      }
    }

    public string LanguageString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Language Language
    {
      get => this._language;
      set
      {
        if (this._language == value)
          return;
        this._language = value;
        this.LanguageString = value == null ? "" : value.ToString();
      }
    }

    public string LanguageId
    {
      get => this._language != null ? this._language.Id : this._languageId;
      set
      {
        value = value?.Trim();
        this._languageId = value;
        if (this._language != null && this._language.Id != this._languageId)
          this._language = (Language) null;
        this.IsSaved = false;
      }
    }

    public string Name
    {
      get => this._name;
      set
      {
        value = value?.Trim();
        if (this._name == value)
          return;
        this._name = value;
        this.IsSaved = false;
      }
    }

    public string PostCode
    {
      get => this._postCode;
      set
      {
        value = value?.Trim();
        if (this._postCode == value)
          return;
        this._postCode = value;
        this.IsSaved = false;
      }
    }

    public string PracticeAccountString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Client PracticeAccount
    {
      get => this._practiceAccount;
      set
      {
        if (this._practiceAccount == value)
          return;
        this._practiceAccount = value;
        this.PracticeAccountString = value == null ? "" : value.ToString();
      }
    }

    public string PracticeId
    {
      get => this._practiceAccount != null ? this._practiceAccount.Id : this._practiceId;
      set
      {
        value = value?.Trim();
        this._practiceId = value;
        if (this._practiceAccount != null && this._practiceAccount.Id != this._practiceId)
          this._practiceAccount = (Client) null;
        this.IsSaved = false;
      }
    }

    public bool ReceiveMarketing
    {
      get => this._receiveMarketing;
      set
      {
        if (this._receiveMarketing == value)
          return;
        this._receiveMarketing = value;
        this.IsSaved = false;
      }
    }

    public string State
    {
      get => this._state;
      set
      {
        value = value?.Trim();
        if (this._state == value)
          return;
        this._state = value;
        this.IsSaved = false;
      }
    }

    public string StripeId
    {
      get => this._stripeId;
      set
      {
        value = value?.Trim();
        if (this._stripeId == value)
          return;
        this._stripeId = value;
        this.IsSaved = false;
      }
    }

    public bool TermsOfUse
    {
      get => this._termsOfUse;
      set
      {
        if (this._termsOfUse == value)
          return;
        this._termsOfUse = value;
        this.IsSaved = false;
      }
    }

    public string TypeString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public ClientType Type
    {
      get => this._type;
      set
      {
        if (this._type == value)
          return;
        this._type = value;
        this.TypeString = value == null ? "" : value.ToString();
      }
    }

    public string TypeId
    {
      get => this._type != null ? this._type.Id : this._typeId;
      set
      {
        value = value?.Trim();
        this._typeId = value;
        if (this._type != null && this._type.Id != this._typeId)
          this._type = (ClientType) null;
        this.IsSaved = false;
      }
    }

    public string Website
    {
      get => this._website;
      set
      {
        value = value?.Trim();
        if (this._website == value)
          return;
        this._website = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Application> Applications { get; set; } = new List<Application>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Client> Customers { get; set; } = new List<Client>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Client> ProductionAccounts { get; set; } = new List<Client>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Comment> Comments { get; set; } = new List<Comment>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Department> Departments { get; set; } = new List<Department>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Document> Documents { get; set; } = new List<Document>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Idea> Ideas { get; set; } = new List<Idea>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaOtherRunningCost> IdeaOtherRunningCosts { get; set; } = new List<IdeaOtherRunningCost>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaRunningCost> IdeaRunningCosts { get; set; } = new List<IdeaRunningCost>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<ImplementationCost> ImplementationCosts { get; set; } = new List<ImplementationCost>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Location> Locations { get; set; } = new List<Location>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<ManageTenant> ManageTenants { get; set; } = new List<ManageTenant>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Message> Messages { get; set; } = new List<Message>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<OtherRunningCost> OtherRunningCosts { get; set; } = new List<OtherRunningCost>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Process> Processes { get; set; } = new List<Process>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<BusinessRole> Roles { get; set; } = new List<BusinessRole>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<RoleCost> RoleCosts { get; set; } = new List<RoleCost>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<RoleIdeaAuthorisation> RoleIdeaAuthorisations { get; set; } = new List<RoleIdeaAuthorisation>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<RunningCost> RunningCosts { get; set; } = new List<RunningCost>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<SoftwareVender> SoftwareVenders { get; set; } = new List<SoftwareVender>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Subscription> TenantSubscriptions { get; set; } = new List<Subscription>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Subscription> AgencySubscriptions { get; set; } = new List<Subscription>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Team> Teams { get; set; } = new List<Team>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<User> Users { get; set; } = new List<User>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Version> Versions { get; set; } = new List<Version>();

    public void Update(Client client)
    {
      if (client.AccountOwnerId == null)
        this.AccountOwner = (User) null;
      if (client.AgencyDiscountId == null)
        this.AgencyDiscount = (Discount) null;
      if (client.AgencyId == null)
        this.Agency = (Client) null;
      if (client.CountryId == null)
        this.Country = (Country) null;
      if (client.IndustryId == null)
        this.Industry = (Industry) null;
      if (client.PracticeId == null)
        this.PracticeAccount = (Client) null;
      this.AccountOwnerId = client.AccountOwnerId;
      this.Address1 = client.Address1;
      this.Address2 = client.Address2;
      this.AgencyDiscountId = client.AgencyDiscountId;
      this.AgencyId = client.AgencyId;
      this.AllowGuestSignIn = client.AllowGuestSignIn;
      this.AverageWorkingDay = client.AverageWorkingDay;
      this.AverageWorkingHour = client.AverageWorkingHour;
      this.City = client.City;
      this.CountryId = client.CountryId;
      this.CurrencyId = client.CurrencyId;
      this.FreeTrialDay = client.FreeTrialDay;
      this.IndustryId = client.IndustryId;
      this.IsActive = client.IsActive;
      this.IsDemo = client.IsDemo;
      this.IsPractice = client.IsPractice;
      this.LanguageId = client.LanguageId;
      this.Name = client.Name;
      this.PostCode = client.PostCode;
      this.PracticeId = client.PracticeId;
      this.ReceiveMarketing = client.ReceiveMarketing;
      this.State = client.State;
      this.StripeId = client.StripeId;
      this.TermsOfUse = client.TermsOfUse;
      this.TypeId = client.TypeId;
      this.Website = client.Website;
    }

    public override string ToString() => this.Name;
  }
}
