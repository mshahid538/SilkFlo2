using SilkFlo.Data.Core.Domain.Business;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shared
{
  [XmlType(Namespace = "Shared")]
  public class SubmissionPath : Abstract
  {
    private string _id = "";
    private string _name = "";

    public SubmissionPath() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Idea> Ideas { get; set; } = new List<Idea>();

    public void Update(SubmissionPath submissionPath) => this.Name = submissionPath.Name;

    public override string ToString() => this.Name;
  }
}
