using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain
{
  [XmlType(Namespace = "")]
  public class UserRole : Abstract
  {
    private string _id = "";
    private Role _role;
    private string _roleId;
    private User _user;
    private string _userId;

    public UserRole() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string RoleString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Role Role
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
          this._role = (Role) null;
        this.IsSaved = false;
      }
    }

    public string UserString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public User User
    {
      get => this._user;
      set
      {
        if (this._user == value)
          return;
        this._user = value;
        this.UserString = value == null ? "" : value.ToString();
      }
    }

    public string UserId
    {
      get => this._user != null ? this._user.Id : this._userId;
      set
      {
        value = value?.Trim();
        this._userId = value;
        if (this._user != null && this._user.Id != this._userId)
          this._user = (User) null;
        this.IsSaved = false;
      }
    }

    public void Update(UserRole userRole)
    {
      this.RoleId = userRole.RoleId;
      this.UserId = userRole.UserId;
    }
  }
}
