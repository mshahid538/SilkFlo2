using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class Comment : Abstract
  {
    private string _id = "";
    private Client _client;
    private string _clientId;
    private string _componentId = "";
    private Idea _idea;
    private string _ideaId;
    private User _sender;
    private string _senderId;
    private string _text = "";

    public Comment() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string ComponentId
    {
      get => this._componentId;
      set
      {
        value = value?.Trim();
        if (this._componentId == value)
          return;
        this._componentId = value;
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

    public string SenderString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public User Sender
    {
      get => this._sender;
      set
      {
        if (this._sender == value)
          return;
        this._sender = value;
        this.SenderString = value == null ? "" : value.ToString();
      }
    }

    public string SenderId
    {
      get => this._sender != null ? this._sender.Id : this._senderId;
      set
      {
        value = value?.Trim();
        this._senderId = value;
        if (this._sender != null && this._sender.Id != this._senderId)
          this._sender = (User) null;
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

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Recipient> Recipients { get; set; } = new List<Recipient>();

    public void Update(Comment comment)
    {
      this.ClientId = comment.ClientId;
      this.ComponentId = comment.ComponentId;
      this.IdeaId = comment.IdeaId;
      this.SenderId = comment.SenderId;
      this.Text = comment.Text;
    }

    public override string ToString() => this.Text;
  }
}
