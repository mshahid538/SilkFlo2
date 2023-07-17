using SilkFlo.Data.Core.Domain.Business;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shared
{
  [XmlType(Namespace = "Shared")]
  public class IdeaStatus : Abstract
  {
    private string _id = "";
    private string _buttonClass = "";
    private bool _isPathToSuccess;
    private string _name = "";
    private bool _requireIdeaStageField;
    private int _sort;
    private Stage _stage;
    private string _stageId;
    private string _textClass = "";

    public IdeaStatus() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string ButtonClass
    {
      get => this._buttonClass;
      set
      {
        value = value?.Trim();
        if (this._buttonClass == value)
          return;
        this._buttonClass = value;
        this.IsSaved = false;
      }
    }

    public bool IsPathToSuccess
    {
      get => this._isPathToSuccess;
      set
      {
        if (this._isPathToSuccess == value)
          return;
        this._isPathToSuccess = value;
        this.IsSaved = false;
      }
    }

    public string Name
    {
      get => this._name;
      set
      {
        value = value?.Trim();
        if (this._name == value)
          return;
        this._name = value;
        this.IsSaved = false;
      }
    }

    public bool RequireIdeaStageField
    {
      get => this._requireIdeaStageField;
      set
      {
        if (this._requireIdeaStageField == value)
          return;
        this._requireIdeaStageField = value;
        this.IsSaved = false;
      }
    }

    public int Sort
    {
      get => this._sort;
      set
      {
        if (this._sort == value)
          return;
        this._sort = value;
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

    public string TextClass
    {
      get => this._textClass;
      set
      {
        value = value?.Trim();
        if (this._textClass == value)
          return;
        this._textClass = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaStageStatus> IdeaStageStatuses { get; set; } = new List<IdeaStageStatus>();

    public void Update(IdeaStatus ideaStatus)
    {
      this.ButtonClass = ideaStatus.ButtonClass;
      this.IsPathToSuccess = ideaStatus.IsPathToSuccess;
      this.Name = ideaStatus.Name;
      this.RequireIdeaStageField = ideaStatus.RequireIdeaStageField;
      this.Sort = ideaStatus.Sort;
      this.StageId = ideaStatus.StageId;
      this.TextClass = ideaStatus.TextClass;
    }

    public override string ToString() => this.Name;
  }
}
