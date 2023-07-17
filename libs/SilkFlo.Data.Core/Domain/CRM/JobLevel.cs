using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.CRM
{
  [XmlType(Namespace = "CRM")]
  public class JobLevel : Abstract
  {
    private string _id = "";
    private string _name = "";
    private int _sort;

    public JobLevel() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Prospect> TeamMembers { get; set; } = new List<Prospect>();

    public void Update(JobLevel jobLevel)
    {
      this.Name = jobLevel.Name;
      this.Sort = jobLevel.Sort;
    }

    public override string ToString() => this.Name;
  }
}
