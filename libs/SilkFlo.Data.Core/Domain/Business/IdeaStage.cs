using SilkFlo.Data.Core.Domain.Shared;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class IdeaStage : Abstract
  {
    private string _id = "";
    private System.DateTime? _dateEnd;
    private System.DateTime? _dateEndEstimate;
    private System.DateTime? _dateStart;
    private System.DateTime _dateStartEstimate;
    private Idea _idea;
    private string _ideaId;
    private bool _isInWorkFlow;
    private Stage _stage;
    private string _stageId;

    public IdeaStage() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public System.DateTime? DateEnd
    {
      get => this._dateEnd;
      set
      {
        System.DateTime? dateEnd = this._dateEnd;
        System.DateTime? nullable = value;
        if (dateEnd.HasValue == nullable.HasValue && (!dateEnd.HasValue || dateEnd.GetValueOrDefault() == nullable.GetValueOrDefault()))
          return;
        this._dateEnd = value;
        this.IsSaved = false;
      }
    }

    public System.DateTime? DateEndEstimate
    {
      get => this._dateEndEstimate;
      set
      {
        System.DateTime? dateEndEstimate = this._dateEndEstimate;
        System.DateTime? nullable = value;
        if (dateEndEstimate.HasValue == nullable.HasValue && (!dateEndEstimate.HasValue || dateEndEstimate.GetValueOrDefault() == nullable.GetValueOrDefault()))
          return;
        this._dateEndEstimate = value;
        this.IsSaved = false;
      }
    }

    public System.DateTime? DateStart
    {
      get => this._dateStart;
      set
      {
        System.DateTime? dateStart = this._dateStart;
        System.DateTime? nullable = value;
        if (dateStart.HasValue == nullable.HasValue && (!dateStart.HasValue || dateStart.GetValueOrDefault() == nullable.GetValueOrDefault()))
          return;
        this._dateStart = value;
        this.IsSaved = false;
      }
    }

    public System.DateTime DateStartEstimate
    {
      get => this._dateStartEstimate;
      set
      {
        if (this._dateStartEstimate == value)
          return;
        this._dateStartEstimate = value;
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

    public bool IsInWorkFlow
    {
      get => this._isInWorkFlow;
      set
      {
        if (this._isInWorkFlow == value)
          return;
        this._isInWorkFlow = value;
        this.IsSaved = false;
      }
    }

    public string StageString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Stage Stage
    {
      get => this._stage;
      set
      {
        if (this._stage == value)
          return;
        this._stage = value;
        this.StageString = value == null ? "" : value.ToString();
      }
    }

    public string StageId
    {
      get => this._stage != null ? this._stage.Id : this._stageId;
      set
      {
        value = value?.Trim();
        this._stageId = value;
        if (this._stage != null && this._stage.Id != this._stageId)
          this._stage = (Stage) null;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaStageStatus> IdeaStageStatuses { get; set; } = new List<IdeaStageStatus>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<ImplementationCost> ImplementationCosts { get; set; } = new List<ImplementationCost>();

    public void Update(IdeaStage ideaStage)
    {
      this.DateEnd = ideaStage.DateEnd;
      this.DateEndEstimate = ideaStage.DateEndEstimate;
      this.DateStart = ideaStage.DateStart;
      this.DateStartEstimate = ideaStage.DateStartEstimate;
      this.IdeaId = ideaStage.IdeaId;
      this.IsInWorkFlow = ideaStage.IsInWorkFlow;
      this.StageId = ideaStage.StageId;
    }
  }
}
