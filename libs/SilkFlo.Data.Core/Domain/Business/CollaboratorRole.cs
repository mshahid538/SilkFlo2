using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class CollaboratorRole : Abstract
  {
    private string _id = "";
    private Collaborator _collaborator;
    private string _collaboratorId;
    private BusinessRole _role;
    private string _roleId;

    public CollaboratorRole() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string CollaboratorString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Collaborator Collaborator
    {
      get => this._collaborator;
      set
      {
        if (this._collaborator == value)
          return;
        this._collaborator = value;
        this.CollaboratorString = value == null ? "" : value.ToString();
      }
    }

    public string CollaboratorId
    {
      get => this._collaborator != null ? this._collaborator.Id : this._collaboratorId;
      set
      {
        value = value?.Trim();
        this._collaboratorId = value;
        if (this._collaborator != null && this._collaborator.Id != this._collaboratorId)
          this._collaborator = (Collaborator) null;
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

    [IgnoreDataMember]
    [XmlIgnore]
    public List<UserAuthorisation> UserAuthorisations { get; set; } = new List<UserAuthorisation>();

    public void Update(CollaboratorRole collaboratorRole)
    {
      this.CollaboratorId = collaboratorRole.CollaboratorId;
      this.RoleId = collaboratorRole.RoleId;
    }
  }
}
