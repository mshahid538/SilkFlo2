using SilkFlo.Data.Core.Domain.Shared;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class UserAuthorisation : Abstract
  {
    private string _id = "";
    private CollaboratorRole _collaboratorRole;
    private string _collaboratorRoleId;
    private IdeaAuthorisation _ideaAuthorisation;
    private string _ideaAuthorisationId;
    private Idea _idea;
    private string _ideaId;
    private User _user;
    private string _userId;

    public UserAuthorisation() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string CollaboratorRoleString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public CollaboratorRole CollaboratorRole
    {
      get => this._collaboratorRole;
      set
      {
        if (this._collaboratorRole == value)
          return;
        this._collaboratorRole = value;
        this.CollaboratorRoleString = value == null ? "" : value.ToString();
      }
    }

    public string CollaboratorRoleId
    {
      get => this._collaboratorRole != null ? this._collaboratorRole.Id : this._collaboratorRoleId;
      set
      {
        value = value?.Trim();
        this._collaboratorRoleId = value;
        if (this._collaboratorRole != null && this._collaboratorRole.Id != this._collaboratorRoleId)
          this._collaboratorRole = (CollaboratorRole) null;
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

    public string IdeaString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Idea Idea
    {
      get => this._idea;
      set
      {
        if (this._idea == value)
          return;
        this._idea = value;
        this.IdeaString = value == null ? "" : value.ToString();
      }
    }

    public string IdeaId
    {
      get => this._idea != null ? this._idea.Id : this._ideaId;
      set
      {
        value = value?.Trim();
        this._ideaId = value;
        if (this._idea != null && this._idea.Id != this._ideaId)
          this._idea = (Idea) null;
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

    public void Update(UserAuthorisation userAuthorisation)
    {
      this.CollaboratorRoleId = userAuthorisation.CollaboratorRoleId;
      this.IdeaAuthorisationId = userAuthorisation.IdeaAuthorisationId;
      this.IdeaId = userAuthorisation.IdeaId;
      this.UserId = userAuthorisation.UserId;
    }
  }
}
