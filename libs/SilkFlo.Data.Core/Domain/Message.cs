using SilkFlo.Data.Core.Domain.Business;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain
{
  [XmlType(Namespace = "")]
  public class Message : Abstract
  {
    private string _id = "";
    private Client _client;
    private string _clientId;
    private string _subject = "";
    private string _text = "";
    private User _user;
    private string _userId;

    public Message() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Subject
    {
      get => this._subject;
      set
      {
        value = value?.Trim();
        if (this._subject == value)
          return;
        this._subject = value;
        this.IsSaved = false;
      }
    }

    public string Text
    {
      get => this._text;
      set
      {
        value = value?.Trim();
        if (this._text == value)
          return;
        this._text = value;
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

    public void Update(Message message)
    {
      this.ClientId = message.ClientId;
      this.Subject = message.Subject;
      this.Text = message.Text;
      this.UserId = message.UserId;
    }

    public override string ToString() => this.Subject;
  }
}
