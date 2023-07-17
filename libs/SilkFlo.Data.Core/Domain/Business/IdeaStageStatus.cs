using SilkFlo.Data.Core.Domain.Shared;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class IdeaStageStatus : Abstract
  {
    private string _id = "";
    private System.DateTime _date;
    private IdeaStage _ideaStage;
    private string _ideaStageId;
    private IdeaStatus _status;
    private string _statusId;

    public IdeaStageStatus() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string IdeaStageString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public IdeaStage IdeaStage
    {
      get => this._ideaStage;
      set
      {
        if (this._ideaStage == value)
          return;
        this._ideaStage = value;
        this.IdeaStageString = value == null ? "" : value.ToString();
      }
    }

    public string IdeaStageId
    {
      get => this._ideaStage != null ? this._ideaStage.Id : this._ideaStageId;
      set
      {
        value = value?.Trim();
        this._ideaStageId = value;
        if (this._ideaStage != null && this._ideaStage.Id != this._ideaStageId)
          this._ideaStage = (IdeaStage) null;
        this.IsSaved = false;
      }
    }

    public string StatusString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public IdeaStatus Status
    {
      get => this._status;
      set
      {
        if (this._status == value)
          return;
        this._status = value;
        this.StatusString = value == null ? "" : value.ToString();
      }
    }

    public string StatusId
    {
      get => this._status != null ? this._status.Id : this._statusId;
      set
      {
        value = value?.Trim();
        this._statusId = value;
        if (this._status != null && this._status.Id != this._statusId)
          this._status = (IdeaStatus) null;
        this.IsSaved = false;
      }
    }

    public void Update(IdeaStageStatus ideaStageStatus)
    {
      this.Date = ideaStageStatus.Date;
      this.IdeaStageId = ideaStageStatus.IdeaStageId;
      this.StatusId = ideaStageStatus.StatusId;
    }
  }
}
