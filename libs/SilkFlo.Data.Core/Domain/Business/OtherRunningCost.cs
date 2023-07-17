using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class OtherRunningCost : Abstract
  {
    private string _id = "";
    private Client _client;
    private string _clientId;
    private Decimal _cost;
    private CostType _costType;
    private string _costTypeId;
    private string _description = "";
    private Period _frequency;
    private string _frequencyId;
    private bool _isLive;
    private string _name = "";

    public OtherRunningCost() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string CostTypeString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public CostType CostType
    {
      get => this._costType;
      set
      {
        if (this._costType == value)
          return;
        this._costType = value;
        this.CostTypeString = value == null ? "" : value.ToString();
      }
    }

    public string CostTypeId
    {
      get => this._costType != null ? this._costType.Id : this._costTypeId;
      set
      {
        value = value?.Trim();
        this._costTypeId = value;
        if (this._costType != null && this._costType.Id != this._costTypeId)
          this._costType = (CostType) null;
        this.IsSaved = false;
      }
    }

    public string Description
    {
      get => this._description;
      set
      {
        value = value?.Trim();
        if (this._description == value)
          return;
        this._description = value;
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

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaOtherRunningCost> IdeaOtherRunningCosts { get; set; } = new List<IdeaOtherRunningCost>();

    public void Update(OtherRunningCost otherRunningCost)
    {
      this.ClientId = otherRunningCost.ClientId;
      this.Cost = otherRunningCost.Cost;
      this.CostTypeId = otherRunningCost.CostTypeId;
      this.Description = otherRunningCost.Description;
      this.FrequencyId = otherRunningCost.FrequencyId;
      this.IsLive = otherRunningCost.IsLive;
      this.Name = otherRunningCost.Name;
    }

    public override string ToString() => this.Name;
  }
}
