using SilkFlo.Data.Core.Domain.Shared;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class IdeaApplicationVersion : Abstract
  {
    private string _id = "";
    private Idea _idea;
    private string _ideaId;
    private bool _isThinClient;
    private Language _language;
    private string _languageId;
    private Version _version;
    private string _versionId;

    public IdeaApplicationVersion() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public bool IsThinClient
    {
      get => this._isThinClient;
      set
      {
        if (this._isThinClient == value)
          return;
        this._isThinClient = value;
        this.IsSaved = false;
      }
    }

    public string LanguageString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Language Language
    {
      get => this._language;
      set
      {
        if (this._language == value)
          return;
        this._language = value;
        this.LanguageString = value == null ? "" : value.ToString();
      }
    }

    public string LanguageId
    {
      get => this._language != null ? this._language.Id : this._languageId;
      set
      {
        value = value?.Trim();
        this._languageId = value;
        if (this._language != null && this._language.Id != this._languageId)
          this._language = (Language) null;
        this.IsSaved = false;
      }
    }

    public string VersionString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Version Version
    {
      get => this._version;
      set
      {
        if (this._version == value)
          return;
        this._version = value;
        this.VersionString = value == null ? "" : value.ToString();
      }
    }

    public string VersionId
    {
      get => this._version != null ? this._version.Id : this._versionId;
      set
      {
        value = value?.Trim();
        this._versionId = value;
        if (this._version != null && this._version.Id != this._versionId)
          this._version = (Version) null;
        this.IsSaved = false;
      }
    }

    public void Update(IdeaApplicationVersion ideaApplicationVersion)
    {
      this.IdeaId = ideaApplicationVersion.IdeaId;
      this.IsThinClient = ideaApplicationVersion.IsThinClient;
      this.LanguageId = ideaApplicationVersion.LanguageId;
      this.VersionId = ideaApplicationVersion.VersionId;
    }
  }
}
