using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain
{
  [XmlType(Namespace = "")]
  public class Analytic : Abstract
  {
    private string _id = "";
    private string _action = "";
    private System.DateTime _date;
    private string _language = "";
    private string _platform = "";
    private string _sessionTracker = "";
    private string _timeZone = "";
    private string _uRL = "";
    private string _userAgent = "";
    private string _userColour = "";
    private User _user;
    private string _userId;
    private string _userTracker = "";

    public Analytic() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Action
    {
      get => this._action;
      set
      {
        value = value?.Trim();
        if (this._action == value)
          return;
        this._action = value;
        this.IsSaved = false;
      }
    }

    public System.DateTime Date
    {
      get => this._date;
      set
      {
        if (this._date == value)
          return;
        this._date = value;
        this.IsSaved = false;
      }
    }

    public string Language
    {
      get => this._language;
      set
      {
        value = value?.Trim();
        if (this._language == value)
          return;
        this._language = value;
        this.IsSaved = false;
      }
    }

    public string Platform
    {
      get => this._platform;
      set
      {
        value = value?.Trim();
        if (this._platform == value)
          return;
        this._platform = value;
        this.IsSaved = false;
      }
    }

    public string SessionTracker
    {
      get => this._sessionTracker;
      set
      {
        value = value?.Trim();
        if (this._sessionTracker == value)
          return;
        this._sessionTracker = value;
        this.IsSaved = false;
      }
    }

    public string TimeZone
    {
      get => this._timeZone;
      set
      {
        value = value?.Trim();
        if (this._timeZone == value)
          return;
        this._timeZone = value;
        this.IsSaved = false;
      }
    }

    public string URL
    {
      get => this._uRL;
      set
      {
        value = value?.Trim();
        if (this._uRL == value)
          return;
        this._uRL = value;
        this.IsSaved = false;
      }
    }

    public string UserAgent
    {
      get => this._userAgent;
      set
      {
        value = value?.Trim();
        if (this._userAgent == value)
          return;
        this._userAgent = value;
        this.IsSaved = false;
      }
    }

    public string UserColour
    {
      get => this._userColour;
      set
      {
        value = value?.Trim();
        if (this._userColour == value)
          return;
        this._userColour = value;
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

    public string UserTracker
    {
      get => this._userTracker;
      set
      {
        value = value?.Trim();
        if (this._userTracker == value)
          return;
        this._userTracker = value;
        this.IsSaved = false;
      }
    }

    public void Update(Analytic analytic)
    {
      if (analytic.UserId == null)
        this.User = (User) null;
      this.Action = analytic.Action;
      this.Date = analytic.Date;
      this.Language = analytic.Language;
      this.Platform = analytic.Platform;
      this.SessionTracker = analytic.SessionTracker;
      this.TimeZone = analytic.TimeZone;
      this.URL = analytic.URL;
      this.UserAgent = analytic.UserAgent;
      this.UserColour = analytic.UserColour;
      this.UserId = analytic.UserId;
      this.UserTracker = analytic.UserTracker;
    }
  }
}
