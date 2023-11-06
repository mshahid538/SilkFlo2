using SilkFlo.Data.Core.Domain.Shared;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class RoleIdeaAuthorisation : Abstract
  {
    private string _id = "";
    private Client _client;
    private string _clientId;
    private IdeaAuthorisation _ideaAuthorisation;
    private string _ideaAuthorisationId;
    private BusinessRole _role;
    private string _roleId;

    public RoleIdeaAuthorisation() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string IdeaAuthorisationString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public IdeaAuthorisation IdeaAuthorisation
    {
      get => this._ideaAuthorisation;
      set
      {
        if (this._ideaAuthorisation == value)
          return;
        this._ideaAuthorisation = value;
        this.IdeaAuthorisationString = value == null ? "" : value.ToString();
      }
    }

    public string IdeaAuthorisationId
    {
      get => this._ideaAuthorisation != null ? this._ideaAuthorisation.Id : this._ideaAuthorisationId;
      set
      {
        value = value?.Trim();
        this._ideaAuthorisationId = value;
        if (this._ideaAuthorisation != null && this._ideaAuthorisation.Id != this._ideaAuthorisationId)
          this._ideaAuthorisation = (IdeaAuthorisation) null;
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

    public void Update(RoleIdeaAuthorisation roleIdeaAuthorisation)
    {
      this.ClientId = roleIdeaAuthorisation.ClientId;
      this.IdeaAuthorisationId = roleIdeaAuthorisation.IdeaAuthorisationId;
      this.RoleId = roleIdeaAuthorisation.RoleId;
    }
  }
}
