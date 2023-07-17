using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class Recipient : Abstract
  {
    private string _id = "";
    private Comment _comment;
    private string _commentId;
    private User _user;
    private string _userId;

    public Recipient() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string CommentString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Comment Comment
    {
      get => this._comment;
      set
      {
        if (this._comment == value)
          return;
        this._comment = value;
        this.CommentString = value == null ? "" : value.ToString();
      }
    }

    public string CommentId
    {
      get => this._comment != null ? this._comment.Id : this._commentId;
      set
      {
        value = value?.Trim();
        this._commentId = value;
        if (this._comment != null && this._comment.Id != this._commentId)
          this._comment = (Comment) null;
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

    public void Update(Recipient recipient)
    {
      this.CommentId = recipient.CommentId;
      this.UserId = recipient.UserId;
    }
  }
}
