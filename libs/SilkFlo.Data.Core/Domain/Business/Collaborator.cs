using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class Collaborator : Abstract
  {
    private string _id = "";
    private Idea _idea;
    private string _ideaId;
    private User _invitedBy;
    private string _invitedById;
    private bool _isInvitationConfirmed;
    private User _user;
    private string _userId;

    public Collaborator() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string InvitedByString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public User InvitedBy
    {
      get => this._invitedBy;
      set
      {
        if (this._invitedBy == value)
          return;
        this._invitedBy = value;
        this.InvitedByString = value == null ? "" : value.ToString();
      }
    }

    public string InvitedById
    {
      get => this._invitedBy != null ? this._invitedBy.Id : this._invitedById;
      set
      {
        value = value?.Trim();
        this._invitedById = value;
        if (this._invitedBy != null && this._invitedBy.Id != this._invitedById)
          this._invitedBy = (User) null;
        this.IsSaved = false;
      }
    }

    public bool IsInvitationConfirmed
    {
      get => this._isInvitationConfirmed;
      set
      {
        if (this._isInvitationConfirmed == value)
          return;
        this._isInvitationConfirmed = value;
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

    [IgnoreDataMember]
    [XmlIgnore]
    public List<CollaboratorRole> CollaboratorRoles { get; set; } = new List<CollaboratorRole>();

    public void Update(Collaborator collaborator)
    {
      if (collaborator.InvitedById == null)
        this.InvitedBy = (User) null;
      this.IdeaId = collaborator.IdeaId;
      this.InvitedById = collaborator.InvitedById;
      this.IsInvitationConfirmed = collaborator.IsInvitationConfirmed;
      this.UserId = collaborator.UserId;
    }
  }
}
