using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class RunningCost : Abstract
  {
    private string _id = "";
    private AutomationType _automationType;
    private string _automationTypeId;
    private Client _client;
    private string _clientId;
    private Decimal _cost;
    private Period _frequency;
    private string _frequencyId;
    private bool _isLive;
    private string _licenceType = "";
    private SoftwareVender _vender;
    private string _venderId;

    public RunningCost() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string AutomationTypeString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public AutomationType AutomationType
    {
      get => this._automationType;
      set
      {
        if (this._automationType == value)
          return;
        this._automationType = value;
        this.AutomationTypeString = value == null ? "" : value.ToString();
      }
    }

    public string AutomationTypeId
    {
      get => this._automationType != null ? this._automationType.Id : this._automationTypeId;
      set
      {
        value = value?.Trim();
        this._automationTypeId = value;
        if (this._automationType != null && this._automationType.Id != this._automationTypeId)
          this._automationType = (AutomationType) null;
        this.IsSaved = false;
      }
    }

    public string ClientString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Client Client
    {
      get => this._client;
      set
      {
        if (this._client == value)
          return;
        this._client = value;
        this.ClientString = value == null ? "" : value.ToString();
      }
    }

    public string ClientId
    {
      get => this._client != null ? this._client.Id : this._clientId;
      set
      {
        value = value?.Trim();
        this._clientId = value;
        if (this._client != null && this._client.Id != this._clientId)
          this._client = (Client) null;
        this.IsSaved = false;
      }
    }

    public Decimal Cost
    {
      get => this._cost;
      set
      {
        if (this._cost == value)
          return;
        this._cost = value;
        this.IsSaved = false;
      }
    }

    public string FrequencyString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Period Frequency
    {
      get => this._frequency;
      set
      {
        if (this._frequency == value)
          return;
        this._frequency = value;
        this.FrequencyString = value == null ? "" : value.ToString();
      }
    }

    public string FrequencyId
    {
      get => this._frequency != null ? this._frequency.Id : this._frequencyId;
      set
      {
        value = value?.Trim();
        this._frequencyId = value;
        if (this._frequency != null && this._frequency.Id != this._frequencyId)
          this._frequency = (Period) null;
        this.IsSaved = false;
      }
    }

    public bool IsLive
    {
      get => this._isLive;
      set
      {
        if (this._isLive == value)
          return;
        this._isLive = value;
        this.IsSaved = false;
      }
    }

    public string LicenceType
    {
      get => this._licenceType;
      set
      {
        value = value?.Trim();
        if (this._licenceType == value)
          return;
        this._licenceType = value;
        this.IsSaved = false;
      }
    }

    public string VenderString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public SoftwareVender Vender
    {
      get => this._vender;
      set
      {
        if (this._vender == value)
          return;
        this._vender = value;
        this.VenderString = value == null ? "" : value.ToString();
      }
    }

    public string VenderId
    {
      get => this._vender != null ? this._vender.Id : this._venderId;
      set
      {
        value = value?.Trim();
        this._venderId = value;
        if (this._vender != null && this._vender.Id != this._venderId)
          this._vender = (SoftwareVender) null;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Idea> Ideas { get; set; } = new List<Idea>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaRunningCost> IdeaRunningCosts { get; set; } = new List<IdeaRunningCost>();

    public void Update(RunningCost runningCost)
    {
      this.AutomationTypeId = runningCost.AutomationTypeId;
      this.ClientId = runningCost.ClientId;
      this.Cost = runningCost.Cost;
      this.FrequencyId = runningCost.FrequencyId;
      this.IsLive = runningCost.IsLive;
      this.LicenceType = runningCost.LicenceType;
      this.VenderId = runningCost.VenderId;
    }
  }
}
