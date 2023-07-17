using SilkFlo.Data.Core.Domain.Shared;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain
{
  [XmlType(Namespace = "")]
  public class UserBadge : Abstract
  {
    private string _id = "";
    private Badge _badge;
    private string _badgeId;
    private User _user;
    private string _userId;

    public UserBadge() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string BadgeString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Badge Badge
    {
      get => this._badge;
      set
      {
        if (this._badge == value)
          return;
        this._badge = value;
        this.BadgeString = value == null ? "" : value.ToString();
      }
    }

    public string BadgeId
    {
      get => this._badge != null ? this._badge.Id : this._badgeId;
      set
      {
        value = value?.Trim();
        this._badgeId = value;
        if (this._badge != null && this._badge.Id != this._badgeId)
          this._badge = (Badge) null;
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

    public void Update(UserBadge userBadge)
    {
      this.BadgeId = userBadge.BadgeId;
      this.UserId = userBadge.UserId;
    }
  }
}
