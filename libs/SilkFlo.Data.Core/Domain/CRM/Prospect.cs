using SilkFlo.Data.Core.Domain.Shared;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.CRM
{
  [XmlType(Namespace = "CRM")]
  public class Prospect : Abstract
  {
    private string _id = "";
    private ClientType _clientType;
    private string _clientTypeId;
    private string _companyName = "";
    private CompanySize _companySize;
    private string _companySizeId;
    private Country _country;
    private string _countryId;
    private string _email = "";
    private string _firstName = "";
    private JobLevel _jobLevel;
    private string _jobLevelId;
    private string _lastName = "";
    private string _phoneNumber = "";
    private string _pipeline = "";
    private bool _termsAgreed;

    public Prospect() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string ClientTypeString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public ClientType ClientType
    {
      get => this._clientType;
      set
      {
        if (this._clientType == value)
          return;
        this._clientType = value;
        this.ClientTypeString = value == null ? "" : value.ToString();
      }
    }

    public string ClientTypeId
    {
      get => this._clientType != null ? this._clientType.Id : this._clientTypeId;
      set
      {
        value = value?.Trim();
        this._clientTypeId = value;
        if (this._clientType != null && this._clientType.Id != this._clientTypeId)
          this._clientType = (ClientType) null;
        this.IsSaved = false;
      }
    }

    public string CompanyName
    {
      get => this._companyName;
      set
      {
        value = value?.Trim();
        if (this._companyName == value)
          return;
        this._companyName = value;
        this.IsSaved = false;
      }
    }

    public string CompanySizeString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public CompanySize CompanySize
    {
      get => this._companySize;
      set
      {
        if (this._companySize == value)
          return;
        this._companySize = value;
        this.CompanySizeString = value == null ? "" : value.ToString();
      }
    }

    public string CompanySizeId
    {
      get => this._companySize != null ? this._companySize.Id : this._companySizeId;
      set
      {
        value = value?.Trim();
        this._companySizeId = value;
        if (this._companySize != null && this._companySize.Id != this._companySizeId)
          this._companySize = (CompanySize) null;
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

    public string Email
    {
      get => this._email;
      set
      {
        value = value?.Trim();
        if (this._email == value)
          return;
        this._email = value;
        this.IsSaved = false;
      }
    }

    public string FirstName
    {
      get => this._firstName;
      set
      {
        value = value?.Trim();
        if (this._firstName == value)
          return;
        this._firstName = value;
        this.IsSaved = false;
      }
    }

    public string JobLevelString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public JobLevel JobLevel
    {
      get => this._jobLevel;
      set
      {
        if (this._jobLevel == value)
          return;
        this._jobLevel = value;
        this.JobLevelString = value == null ? "" : value.ToString();
      }
    }

    public string JobLevelId
    {
      get => this._jobLevel != null ? this._jobLevel.Id : this._jobLevelId;
      set
      {
        value = value?.Trim();
        this._jobLevelId = value;
        if (this._jobLevel != null && this._jobLevel.Id != this._jobLevelId)
          this._jobLevel = (JobLevel) null;
        this.IsSaved = false;
      }
    }

    public string LastName
    {
      get => this._lastName;
      set
      {
        value = value?.Trim();
        if (this._lastName == value)
          return;
        this._lastName = value;
        this.IsSaved = false;
      }
    }

    public string PhoneNumber
    {
      get => this._phoneNumber;
      set
      {
        value = value?.Trim();
        if (this._phoneNumber == value)
          return;
        this._phoneNumber = value;
        this.IsSaved = false;
      }
    }

    public string Pipeline
    {
      get => this._pipeline;
      set
      {
        value = value?.Trim();
        if (this._pipeline == value)
          return;
        this._pipeline = value;
        this.IsSaved = false;
      }
    }

    public bool TermsAgreed
    {
      get => this._termsAgreed;
      set
      {
        if (this._termsAgreed == value)
          return;
        this._termsAgreed = value;
        this.IsSaved = false;
      }
    }

    public string Fullname
    {
      get
      {
        string firstName = this.FirstName;
        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(this.LastName))
          firstName += " ";
        return firstName + this.LastName;
      }
    }

    public void Update(Prospect prospect)
    {
      this.ClientTypeId = prospect.ClientTypeId;
      this.CompanyName = prospect.CompanyName;
      this.CompanySizeId = prospect.CompanySizeId;
      this.CountryId = prospect.CountryId;
      this.Email = prospect.Email;
      this.FirstName = prospect.FirstName;
      this.JobLevelId = prospect.JobLevelId;
      this.LastName = prospect.LastName;
      this.PhoneNumber = prospect.PhoneNumber;
      this.Pipeline = prospect.Pipeline;
      this.TermsAgreed = prospect.TermsAgreed;
    }

    public override string ToString() => this.Fullname;
  }
}
