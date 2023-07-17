using SilkFlo.Data.Core.Domain.Business;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shared
{
  [XmlType(Namespace = "Shared")]
  public class Stage : Abstract
  {
    private string _id = "";
    private bool _canAssignCost;
    private bool _isMileStone;
    private string _name = "";
    private bool _setDateAutomatically;
    private int _sort;
    private StageGroup _stageGroup;
    private string _stageGroupId;

    public Stage() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public bool CanAssignCost
    {
      get => this._canAssignCost;
      set
      {
        if (this._canAssignCost == value)
          return;
        this._canAssignCost = value;
        this.IsSaved = false;
      }
    }

    public bool IsMileStone
    {
      get => this._isMileStone;
      set
      {
        if (this._isMileStone == value)
          return;
        this._isMileStone = value;
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

    public bool SetDateAutomatically
    {
      get => this._setDateAutomatically;
      set
      {
        if (this._setDateAutomatically == value)
          return;
        this._setDateAutomatically = value;
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

    public string StageGroupString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public StageGroup StageGroup
    {
      get => this._stageGroup;
      set
      {
        if (this._stageGroup == value)
          return;
        this._stageGroup = value;
        this.StageGroupString = value == null ? "" : value.ToString();
      }
    }

    public string StageGroupId
    {
      get => this._stageGroup != null ? this._stageGroup.Id : this._stageGroupId;
      set
      {
        value = value?.Trim();
        this._stageGroupId = value;
        if (this._stageGroup != null && this._stageGroup.Id != this._stageGroupId)
          this._stageGroup = (StageGroup) null;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaStage> IdeaStages { get; set; } = new List<IdeaStage>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaStatus> IdeaStatuses { get; set; } = new List<IdeaStatus>();

    public void Update(Stage stage)
    {
      this.CanAssignCost = stage.CanAssignCost;
      this.IsMileStone = stage.IsMileStone;
      this.Name = stage.Name;
      this.SetDateAutomatically = stage.SetDateAutomatically;
      this.Sort = stage.Sort;
      this.StageGroupId = stage.StageGroupId;
    }

    public override string ToString() => this.Name;
  }
}
