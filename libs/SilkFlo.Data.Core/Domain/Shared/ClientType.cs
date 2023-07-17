using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shop;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shared
{
  [XmlType(Namespace = "Shared")]
  public class ClientType : Abstract
  {
    private string _id = "";
    private string _description = "";
    private string _name = "";

    public ClientType() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Client> Clients { get; set; } = new List<Client>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Prospect> Prospects { get; set; } = new List<Prospect>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public void Update(ClientType clientType)
    {
      this.Description = clientType.Description;
      this.Name = clientType.Name;
    }

    public override string ToString() => this.Name;
  }
}
