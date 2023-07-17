using SilkFlo.Data.Core.Domain.Business;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shared
{
  [XmlType(Namespace = "Shared")]
  public class IdeaAuthorisation : Abstract
  {
    private string _id = "";
    private string _description = "";
    private string _name = "";
    private int _sort;

    public IdeaAuthorisation() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Description
    {
      get => this._description;
      set
      {
        value = value?.Trim();
        if (this._description == value)
          return;
        this._description = value;
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
    public List<RoleIdeaAuthorisation> RoleIdeaAuthorisations { get; set; } = new List<RoleIdeaAuthorisation>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<UserAuthorisation> UserAuthorisations { get; set; } = new List<UserAuthorisation>();

    public void Update(IdeaAuthorisation ideaAuthorisation)
    {
      this.Description = ideaAuthorisation.Description;
      this.Name = ideaAuthorisation.Name;
      this.Sort = ideaAuthorisation.Sort;
    }

    public override string ToString() => this.Name;
  }
}
