using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class IdeaRunningCost : Abstract
  {
    private string _id = "";
    private Client _client;
    private string _clientId;
    private Idea _idea;
    private string _ideaId;
    private int _licenceCount;
    private RunningCost _runningCost;
    private string _runningCostId;

    public IdeaRunningCost() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public int LicenceCount
    {
      get => this._licenceCount;
      set
      {
        if (this._licenceCount == value)
          return;
        this._licenceCount = value;
        this.IsSaved = false;
      }
    }

    public string RunningCostString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public RunningCost RunningCost
    {
      get => this._runningCost;
      set
      {
        if (this._runningCost == value)
          return;
        this._runningCost = value;
        this.RunningCostString = value == null ? "" : value.ToString();
      }
    }

    public string RunningCostId
    {
      get => this._runningCost != null ? this._runningCost.Id : this._runningCostId;
      set
      {
        value = value?.Trim();
        this._runningCostId = value;
        if (this._runningCost != null && this._runningCost.Id != this._runningCostId)
          this._runningCost = (RunningCost) null;
        this.IsSaved = false;
      }
    }

    public void Update(IdeaRunningCost ideaRunningCost)
    {
      this.ClientId = ideaRunningCost.ClientId;
      this.IdeaId = ideaRunningCost.IdeaId;
      this.LicenceCount = ideaRunningCost.LicenceCount;
      this.RunningCostId = ideaRunningCost.RunningCostId;
    }
  }
}
