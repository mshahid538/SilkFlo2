using SilkFlo.Data.Core.Domain.Business;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shop
{
  [XmlType(Namespace = "Shop")]
  public class Currency : Abstract
  {
    private string _id = "";
    private string _symbol = "";

    public Currency() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Symbol
    {
      get => this._symbol;
      set
      {
        value = value?.Trim();
        if (this._symbol == value)
          return;
        this._symbol = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Client> Clients { get; set; } = new List<Client>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Price> Prices { get; set; } = new List<Price>();

    public void Update(Currency currency) => this.Symbol = currency.Symbol;

    public override string ToString() => this.Id + " (" + this.Symbol + ")";
  }
}
