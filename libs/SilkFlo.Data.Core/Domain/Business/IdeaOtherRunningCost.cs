using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class IdeaOtherRunningCost : Abstract
  {
    private string _id = "";
    private Client _client;
    private string _clientId;
    private Idea _idea;
    private string _ideaId;
    private Decimal _number;
    private OtherRunningCost _otherRunningCost;
    private string _otherRunningCostId;

    public IdeaOtherRunningCost() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string IdeaString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Idea Idea
    {
      get => this._idea;
      set
      {
        if (this._idea == value)
          return;
        this._idea = value;
        this.IdeaString = value == null ? "" : value.ToString();
      }
    }

    public string IdeaId
    {
      get => this._idea != null ? this._idea.Id : this._ideaId;
      set
      {
        value = value?.Trim();
        this._ideaId = value;
        if (this._idea != null && this._idea.Id != this._ideaId)
          this._idea = (Idea) null;
        this.IsSaved = false;
      }
    }

    public Decimal Number
    {
      get => this._number;
      set
      {
        if (this._number == value)
          return;
        this._number = value;
        this.IsSaved = false;
      }
    }

    public string OtherRunningCostString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public OtherRunningCost OtherRunningCost
    {
      get => this._otherRunningCost;
      set
      {
        if (this._otherRunningCost == value)
          return;
        this._otherRunningCost = value;
        this.OtherRunningCostString = value == null ? "" : value.ToString();
      }
    }

    public string OtherRunningCostId
    {
      get => this._otherRunningCost != null ? this._otherRunningCost.Id : this._otherRunningCostId;
      set
      {
        value = value?.Trim();
        this._otherRunningCostId = value;
        if (this._otherRunningCost != null && this._otherRunningCost.Id != this._otherRunningCostId)
          this._otherRunningCost = (OtherRunningCost) null;
        this.IsSaved = false;
      }
    }

    public void Update(IdeaOtherRunningCost ideaOtherRunningCost)
    {
      this.ClientId = ideaOtherRunningCost.ClientId;
      this.IdeaId = ideaOtherRunningCost.IdeaId;
      this.Number = ideaOtherRunningCost.Number;
      this.OtherRunningCostId = ideaOtherRunningCost.OtherRunningCostId;
    }
  }
}
