using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shop;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shared
{
  [XmlType(Namespace = "Shared")]
  public class Period : Abstract
  {
    private string _id = "";
    private int _cancelPeriodInDay;
    private int _cancelPeriodInDayNoRenew;
    private int _monthCount;
    private string _name = "";
    private string _namePlural = "";
    private int _sort;

    public Period() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public int CancelPeriodInDay
    {
      get => this._cancelPeriodInDay;
      set
      {
        if (this._cancelPeriodInDay == value)
          return;
        this._cancelPeriodInDay = value;
        this.IsSaved = false;
      }
    }

    public int CancelPeriodInDayNoRenew
    {
      get => this._cancelPeriodInDayNoRenew;
      set
      {
        if (this._cancelPeriodInDayNoRenew == value)
          return;
        this._cancelPeriodInDayNoRenew = value;
        this.IsSaved = false;
      }
    }

    public int MonthCount
    {
      get => this._monthCount;
      set
      {
        if (this._monthCount == value)
          return;
        this._monthCount = value;
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

    public string NamePlural
    {
      get => this._namePlural;
      set
      {
        value = value?.Trim();
        if (this._namePlural == value)
          return;
        this._namePlural = value;
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
    public List<OtherRunningCost> OtherRunningCosts { get; set; } = new List<OtherRunningCost>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Price> Prices { get; set; } = new List<Price>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<RunningCost> RunningCosts { get; set; } = new List<RunningCost>();

    public void Update(Period period)
    {
      this.CancelPeriodInDay = period.CancelPeriodInDay;
      this.CancelPeriodInDayNoRenew = period.CancelPeriodInDayNoRenew;
      this.MonthCount = period.MonthCount;
      this.Name = period.Name;
      this.NamePlural = period.NamePlural;
      this.Sort = period.Sort;
    }

    public override string ToString() => this.Name;
  }
}
