using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class Document : Abstract
  {
    private string _id = "";
    private Client _client;
    private string _clientId;
    private string _filename = "";
    private string _filenameBackend = "";
    private Idea _idea;
    private string _ideaId;
    private string _text = "";

    public Document() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Filename
    {
      get => this._filename;
      set
      {
        value = value?.Trim();
        if (this._filename == value)
          return;
        this._filename = value;
        this.IsSaved = false;
      }
    }

    public string FilenameBackend
    {
      get => this._filenameBackend;
      set
      {
        value = value?.Trim();
        if (this._filenameBackend == value)
          return;
        this._filenameBackend = value;
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

    public void Update(Document document)
    {
      this.ClientId = document.ClientId;
      this.Filename = document.Filename;
      this.FilenameBackend = document.FilenameBackend;
      this.IdeaId = document.IdeaId;
      this.Text = document.Text;
    }

    public override string ToString() => this.Filename;
  }
}
