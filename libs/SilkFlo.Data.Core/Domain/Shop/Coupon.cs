using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shop
{
  [XmlType(Namespace = "Shop")]
  public class Coupon : Abstract
  {
    private string _id = "";
    private System.DateTime? _dateExpiry;
    private Decimal? _discount;
    private int? _discountPercent;
    private bool _isRecurring;
    private string _name = "";
    private int? _trialDay;
    private int? _useCount;
    private int? _useTotal;

    public Coupon() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public System.DateTime? DateExpiry
    {
      get => this._dateExpiry;
      set
      {
        System.DateTime? dateExpiry = this._dateExpiry;
        System.DateTime? nullable = value;
        if (dateExpiry.HasValue == nullable.HasValue && (!dateExpiry.HasValue || dateExpiry.GetValueOrDefault() == nullable.GetValueOrDefault()))
          return;
        this._dateExpiry = value;
        this.IsSaved = false;
      }
    }

    public Decimal? Discount
    {
      get => this._discount;
      set
      {
        Decimal? discount = this._discount;
        Decimal? nullable = value;
        if (discount.GetValueOrDefault() == nullable.GetValueOrDefault() & discount.HasValue == nullable.HasValue)
          return;
        this._discount = value;
        this.IsSaved = false;
      }
    }

    public int? DiscountPercent
    {
      get => this._discountPercent;
      set
      {
        int? discountPercent = this._discountPercent;
        int? nullable = value;
        if (discountPercent.GetValueOrDefault() == nullable.GetValueOrDefault() & discountPercent.HasValue == nullable.HasValue)
          return;
        this._discountPercent = value;
        this.IsSaved = false;
      }
    }

    public bool IsRecurring
    {
      get => this._isRecurring;
      set
      {
        if (this._isRecurring == value)
          return;
        this._isRecurring = value;
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

    public int? TrialDay
    {
      get => this._trialDay;
      set
      {
        int? trialDay = this._trialDay;
        int? nullable = value;
        if (trialDay.GetValueOrDefault() == nullable.GetValueOrDefault() & trialDay.HasValue == nullable.HasValue)
          return;
        this._trialDay = value;
        this.IsSaved = false;
      }
    }

    public int? UseCount
    {
      get => this._useCount;
      set
      {
        int? useCount = this._useCount;
        int? nullable = value;
        if (useCount.GetValueOrDefault() == nullable.GetValueOrDefault() & useCount.HasValue == nullable.HasValue)
          return;
        this._useCount = value;
        this.IsSaved = false;
      }
    }

    public int? UseTotal
    {
      get => this._useTotal;
      set
      {
        int? useTotal = this._useTotal;
        int? nullable = value;
        if (useTotal.GetValueOrDefault() == nullable.GetValueOrDefault() & useTotal.HasValue == nullable.HasValue)
          return;
        this._useTotal = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public void Update(Coupon coupon)
    {
      this.DateExpiry = coupon.DateExpiry;
      this.Discount = coupon.Discount;
      this.DiscountPercent = coupon.DiscountPercent;
      this.IsRecurring = coupon.IsRecurring;
      this.Name = coupon.Name;
      this.TrialDay = coupon.TrialDay;
      this.UseCount = coupon.UseCount;
      this.UseTotal = coupon.UseTotal;
    }

    public override string ToString() => this.Name;
  }
}
