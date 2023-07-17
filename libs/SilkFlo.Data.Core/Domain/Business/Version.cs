using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class Version : Abstract
  {
    private string _id = "";
    private Application _application;
    private string _applicationId;
    private Client _client;
    private string _clientId;
    private bool _isLive;
    private string _name = "";
    private System.DateTime? _plannedUpdateDate;

    public Version() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string ApplicationString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Application Application
    {
      get => this._application;
      set
      {
        if (this._application == value)
          return;
        this._application = value;
        this.ApplicationString = value == null ? "" : value.ToString();
      }
    }

    public string ApplicationId
    {
      get => this._application != null ? this._application.Id : this._applicationId;
      set
      {
        value = value?.Trim();
        this._applicationId = value;
        if (this._application != null && this._application.Id != this._applicationId)
          this._application = (Application) null;
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

    public System.DateTime? PlannedUpdateDate
    {
      get => this._plannedUpdateDate;
      set
      {
        System.DateTime? plannedUpdateDate = this._plannedUpdateDate;
        System.DateTime? nullable = value;
        if (plannedUpdateDate.HasValue == nullable.HasValue && (!plannedUpdateDate.HasValue || plannedUpdateDate.GetValueOrDefault() == nullable.GetValueOrDefault()))
          return;
        this._plannedUpdateDate = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaApplicationVersion> IdeaApplicationVersions { get; set; } = new List<IdeaApplicationVersion>();

    public void Update(Version version)
    {
      this.ApplicationId = version.ApplicationId;
      this.ClientId = version.ClientId;
      this.IsLive = version.IsLive;
      this.Name = version.Name;
      this.PlannedUpdateDate = version.PlannedUpdateDate;
    }

    public override string ToString() => this.Name;
  }
}
