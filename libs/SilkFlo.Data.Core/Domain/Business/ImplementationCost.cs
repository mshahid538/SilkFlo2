using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class ImplementationCost : Abstract
  {
    private string _id = "";
    private Decimal _allocation;
    private Client _client;
    private string _clientId;
    private Decimal _day;
    private IdeaStage _ideaStage;
    private string _ideaStageId;
    private BusinessRole _role;
    private string _roleId;

    public ImplementationCost() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public Decimal Allocation
    {
      get => this._allocation;
      set
      {
        if (this._allocation == value)
          return;
        this._allocation = value;
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

    public Decimal Day
    {
      get => this._day;
      set
      {
        if (this._day == value)
          return;
        this._day = value;
        this.IsSaved = false;
      }
    }

    public string IdeaStageString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public IdeaStage IdeaStage
    {
      get => this._ideaStage;
      set
      {
        if (this._ideaStage == value)
          return;
        this._ideaStage = value;
        this.IdeaStageString = value == null ? "" : value.ToString();
      }
    }

    public string IdeaStageId
    {
      get => this._ideaStage != null ? this._ideaStage.Id : this._ideaStageId;
      set
      {
        value = value?.Trim();
        this._ideaStageId = value;
        if (this._ideaStage != null && this._ideaStage.Id != this._ideaStageId)
          this._ideaStage = (IdeaStage) null;
        this.IsSaved = false;
      }
    }

    public string RoleString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public BusinessRole Role
    {
      get => this._role;
      set
      {
        if (this._role == value)
          return;
        this._role = value;
        this.RoleString = value == null ? "" : value.ToString();
      }
    }

    public string RoleId
    {
      get => this._role != null ? this._role.Id : this._roleId;
      set
      {
        value = value?.Trim();
        this._roleId = value;
        if (this._role != null && this._role.Id != this._roleId)
          this._role = (BusinessRole) null;
        this.IsSaved = false;
      }
    }

    public void Update(ImplementationCost implementationCost)
    {
      this.Allocation = implementationCost.Allocation;
      this.ClientId = implementationCost.ClientId;
      this.Day = implementationCost.Day;
      this.IdeaStageId = implementationCost.IdeaStageId;
      this.RoleId = implementationCost.RoleId;
    }
  }
}
