using SilkFlo.Data.Core.Domain.Business;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shop
{
  [XmlType(Namespace = "Shop")]
  public class Discount : Abstract
  {
    private string _id = "";
    private string _descriptionReferrer = "";
    private string _descriptionReseller = "";
    private int? _max;
    private int? _min;
    private string _name = "";
    private int? _percentReferrer;
    private int? _percentReseller;

    public Discount() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string DescriptionReferrer
    {
      get => this._descriptionReferrer;
      set
      {
        value = value?.Trim();
        if (this._descriptionReferrer == value)
          return;
        this._descriptionReferrer = value;
        this.IsSaved = false;
      }
    }

    public string DescriptionReseller
    {
      get => this._descriptionReseller;
      set
      {
        value = value?.Trim();
        if (this._descriptionReseller == value)
          return;
        this._descriptionReseller = value;
        this.IsSaved = false;
      }
    }

    public int? Max
    {
      get => this._max;
      set
      {
        int? max = this._max;
        int? nullable = value;
        if (max.GetValueOrDefault() == nullable.GetValueOrDefault() & max.HasValue == nullable.HasValue)
          return;
        this._max = value;
        this.IsSaved = false;
      }
    }

    public int? Min
    {
      get => this._min;
      set
      {
        int? min = this._min;
        int? nullable = value;
        if (min.GetValueOrDefault() == nullable.GetValueOrDefault() & min.HasValue == nullable.HasValue)
          return;
        this._min = value;
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

    public int? PercentReferrer
    {
      get => this._percentReferrer;
      set
      {
        int? percentReferrer = this._percentReferrer;
        int? nullable = value;
        if (percentReferrer.GetValueOrDefault() == nullable.GetValueOrDefault() & percentReferrer.HasValue == nullable.HasValue)
          return;
        this._percentReferrer = value;
        this.IsSaved = false;
      }
    }

    public int? PercentReseller
    {
      get => this._percentReseller;
      set
      {
        int? percentReseller = this._percentReseller;
        int? nullable = value;
        if (percentReseller.GetValueOrDefault() == nullable.GetValueOrDefault() & percentReseller.HasValue == nullable.HasValue)
          return;
        this._percentReseller = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Client> Clients { get; set; } = new List<Client>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public void Update(Discount discount)
    {
      this.DescriptionReferrer = discount.DescriptionReferrer;
      this.DescriptionReseller = discount.DescriptionReseller;
      this.Max = discount.Max;
      this.Min = discount.Min;
      this.Name = discount.Name;
      this.PercentReferrer = discount.PercentReferrer;
      this.PercentReseller = discount.PercentReseller;
    }

    public override string ToString() => this.Name;
  }
}
