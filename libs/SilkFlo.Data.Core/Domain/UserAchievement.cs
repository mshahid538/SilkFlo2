using SilkFlo.Data.Core.Domain.Shared;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain
{
  [XmlType(Namespace = "")]
  public class UserAchievement : Abstract
  {
    private string _id = "";
    private Achievement _achievement;
    private string _achievementId;
    private User _user;
    private string _userId;

    public UserAchievement() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string AchievementString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Achievement Achievement
    {
      get => this._achievement;
      set
      {
        if (this._achievement == value)
          return;
        this._achievement = value;
        this.AchievementString = value == null ? "" : value.ToString();
      }
    }

    public string AchievementId
    {
      get => this._achievement != null ? this._achievement.Id : this._achievementId;
      set
      {
        value = value?.Trim();
        this._achievementId = value;
        if (this._achievement != null && this._achievement.Id != this._achievementId)
          this._achievement = (Achievement) null;
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

    public void Update(UserAchievement userAchievement)
    {
      this.AchievementId = userAchievement.AchievementId;
      this.UserId = userAchievement.UserId;
    }
  }
}
