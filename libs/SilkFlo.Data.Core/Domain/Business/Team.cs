using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class Team : Abstract
  {
    private string _id = "";
    private Client _client;
    private string _clientId;
    private Department _department;
    private string _departmentId;
    private string _name = "";

    public Team() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string DepartmentString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Department Department
    {
      get => this._department;
      set
      {
        if (this._department == value)
          return;
        this._department = value;
        this.DepartmentString = value == null ? "" : value.ToString();
      }
    }

    public string DepartmentId
    {
      get => this._department != null ? this._department.Id : this._departmentId;
      set
      {
        value = value?.Trim();
        this._departmentId = value;
        if (this._department != null && this._department.Id != this._departmentId)
          this._department = (Department) null;
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
    public List<Idea> Ideas { get; set; } = new List<Idea>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Process> Processes { get; set; } = new List<Process>();

    public void Update(Team team)
    {
      this.ClientId = team.ClientId;
      this.DepartmentId = team.DepartmentId;
      this.Name = team.Name;
    }

    public override string ToString() => this.Name;
  }
}
