using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain
{
  [XmlType(Namespace = "")]
  public class User : Abstract
  {
    private string _id = "";
    private string _about = "";
    private Client _client;
    private string _clientId;
    private Department _department;
    private string _departmentId;
    private string _email = "";
    private string _emailConfirmationToken = "";
    private string _emailNew = "";
    private string _firstName = "";
    private bool _isEmailConfirmed;
    private bool _isLockedOut;
    private bool _isMuted;
    private string _jobTitle = "";
    private string _lastName = "";
    private Location _location;
    private string _locationId;
    private User _manager;
    private string _managerId;
    private string _note = "";
    private string _passwordHash = "";
    private string _passwordResetToken = "";

    public User() => this._createdDate = new System.DateTime?(System.DateTime.Now);

    public User(string email, string passwordHash, string firstName, string lastName)
    {
      this.EmailConfirmationToken = Guid.NewGuid().ToString();
      this.Email = email;
      this.PasswordHash = passwordHash;
      this.FirstName = firstName;
      this.LastName = lastName;
    }

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

    public string About
    {
      get => this._about;
      set
      {
        value = value?.Trim();
        if (this._about == value)
          return;
        this._about = value;
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

    public string Email
    {
      get => this._email;
      set
      {
        value = value?.Trim();
        if (this._email == value)
          return;
        this._email = value;
        this.IsSaved = false;
      }
    }

    public string EmailConfirmationToken
    {
      get => this._emailConfirmationToken;
      set
      {
        value = value?.Trim();
        if (this._emailConfirmationToken == value)
          return;
        this._emailConfirmationToken = value;
        this.IsSaved = false;
      }
    }

    public string EmailNew
    {
      get => this._emailNew;
      set
      {
        value = value?.Trim();
        if (this._emailNew == value)
          return;
        this._emailNew = value;
        this.IsSaved = false;
      }
    }

    public string FirstName
    {
      get => this._firstName;
      set
      {
        value = value?.Trim();
        if (this._firstName == value)
          return;
        this._firstName = value;
        this.IsSaved = false;
      }
    }

    public bool IsEmailConfirmed
    {
      get => this._isEmailConfirmed;
      set
      {
        if (this._isEmailConfirmed == value)
          return;
        this._isEmailConfirmed = value;
        this.IsSaved = false;
      }
    }

    public bool IsLockedOut
    {
      get => this._isLockedOut;
      set
      {
        if (this._isLockedOut == value)
          return;
        this._isLockedOut = value;
        this.IsSaved = false;
      }
    }

    public bool IsMuted
    {
      get => this._isMuted;
      set
      {
        if (this._isMuted == value)
          return;
        this._isMuted = value;
        this.IsSaved = false;
      }
    }

    public string JobTitle
    {
      get => this._jobTitle;
      set
      {
        value = value?.Trim();
        if (this._jobTitle == value)
          return;
        this._jobTitle = value;
        this.IsSaved = false;
      }
    }

    public string LastName
    {
      get => this._lastName;
      set
      {
        value = value?.Trim();
        if (this._lastName == value)
          return;
        this._lastName = value;
        this.IsSaved = false;
      }
    }

    public string LocationString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Location Location
    {
      get => this._location;
      set
      {
        if (this._location == value)
          return;
        this._location = value;
        this.LocationString = value == null ? "" : value.ToString();
      }
    }

    public string LocationId
    {
      get => this._location != null ? this._location.Id : this._locationId;
      set
      {
        value = value?.Trim();
        this._locationId = value;
        if (this._location != null && this._location.Id != this._locationId)
          this._location = (Location) null;
        this.IsSaved = false;
      }
    }

    public string ManagerString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public User Manager
    {
      get => this._manager;
      set
      {
        if (this._manager == value)
          return;
        this._manager = value;
        this.ManagerString = value == null ? "" : value.ToString();
      }
    }

    public string ManagerId
    {
      get => this._manager != null ? this._manager.Id : this._managerId;
      set
      {
        value = value?.Trim();
        this._managerId = value;
        if (this._manager != null && this._manager.Id != this._managerId)
          this._manager = (User) null;
        this.IsSaved = false;
      }
    }

    public string Note
    {
      get => this._note;
      set
      {
        value = value?.Trim();
        if (this._note == value)
          return;
        this._note = value;
        this.IsSaved = false;
      }
    }

    public string PasswordHash
    {
      get => this._passwordHash;
      set
      {
        value = value?.Trim();
        if (this._passwordHash == value)
          return;
        this._passwordHash = value;
        this.IsSaved = false;
      }
    }

    public string PasswordResetToken
    {
      get => this._passwordResetToken;
      set
      {
        value = value?.Trim();
        if (this._passwordResetToken == value)
          return;
        this._passwordResetToken = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Analytic> Analytics { get; set; } = new List<Analytic>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Client> AccountOwners { get; set; } = new List<Client>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Collaborator> Collaborators { get; set; } = new List<Collaborator>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Collaborator> InvitedCollaborators { get; set; } = new List<Collaborator>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Comment> CommentsSend { get; set; } = new List<Comment>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Follow> Follows { get; set; } = new List<Follow>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Idea> Ideas { get; set; } = new List<Idea>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<ManageTenant> ManageTenants { get; set; } = new List<ManageTenant>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Message> Messages { get; set; } = new List<Message>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Recipient> Recipients { get; set; } = new List<Recipient>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<User> TeamMembers { get; set; } = new List<User>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<UserAuthorisation> UserAuthorisations { get; set; } = new List<UserAuthorisation>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<UserBadge> UserBadges { get; set; } = new List<UserBadge>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Vote> Votes { get; set; } = new List<Vote>();

    public string Fullname
    {
      get
      {
        string firstName = this.FirstName;
        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(this.LastName))
          firstName += " ";
        return firstName + this.LastName;
      }
    }

    public void Update(User user)
    {
      if (user.ClientId == null)
        this.Client = (Client) null;
      if (user.DepartmentId == null)
        this.Department = (Department) null;
      if (user.LocationId == null)
        this.Location = (Location) null;
      if (user.ManagerId == null)
        this.Manager = (User) null;
      this.About = user.About;
      this.ClientId = user.ClientId;
      this.DepartmentId = user.DepartmentId;
      this.Email = user.Email;
      this.EmailConfirmationToken = user.EmailConfirmationToken;
      this.EmailNew = user.EmailNew;
      this.FirstName = user.FirstName;
      this.IsEmailConfirmed = user.IsEmailConfirmed;
      this.IsLockedOut = user.IsLockedOut;
      this.IsMuted = user.IsMuted;
      this.JobTitle = user.JobTitle;
      this.LastName = user.LastName;
      this.LocationId = user.LocationId;
      this.ManagerId = user.ManagerId;
      this.Note = user.Note;
      this.PasswordHash = user.PasswordHash;
      this.PasswordResetToken = user.PasswordResetToken;
    }

    public override string ToString() => this.Fullname;
  }
}
