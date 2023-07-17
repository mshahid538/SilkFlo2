using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shop
{
  [XmlType(Namespace = "Shop")]
  public class Price : Abstract
  {
    private string _id = "";
    private Decimal? _amount;
    private Currency _currency;
    private string _currencyId;
    private bool _isActive;
    private bool _isLive;
    private Period _period;
    private string _periodId;
    private Product _product;
    private string _productId;

    public Price() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public Decimal? Amount
    {
      get => this._amount;
      set
      {
        Decimal? amount = this._amount;
        Decimal? nullable = value;
        if (amount.GetValueOrDefault() == nullable.GetValueOrDefault() & amount.HasValue == nullable.HasValue)
          return;
        this._amount = value;
        this.IsSaved = false;
      }
    }

    public string CurrencyString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Currency Currency
    {
      get => this._currency;
      set
      {
        if (this._currency == value)
          return;
        this._currency = value;
        this.CurrencyString = value == null ? "" : value.ToString();
      }
    }

    public string CurrencyId
    {
      get => this._currency != null ? this._currency.Id : this._currencyId;
      set
      {
        value = value?.Trim();
        this._currencyId = value;
        if (this._currency != null && this._currency.Id != this._currencyId)
          this._currency = (Currency) null;
        this.IsSaved = false;
      }
    }

    public bool IsActive
    {
      get => this._isActive;
      set
      {
        if (this._isActive == value)
          return;
        this._isActive = value;
        this.IsSaved = false;
      }
    }

    public bool IsLive
    {
      get => this._isLive;
      set
      {
        if (this._isLive == value)
          return;
        this._isLive = value;
        this.IsSaved = false;
      }
    }

    public string PeriodString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Period Period
    {
      get => this._period;
      set
      {
        if (this._period == value)
          return;
        this._period = value;
        this.PeriodString = value == null ? "" : value.ToString();
      }
    }

    public string PeriodId
    {
      get => this._period != null ? this._period.Id : this._periodId;
      set
      {
        value = value?.Trim();
        this._periodId = value;
        if (this._period != null && this._period.Id != this._periodId)
          this._period = (Period) null;
        this.IsSaved = false;
      }
    }

    public string ProductString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Product Product
    {
      get => this._product;
      set
      {
        if (this._product == value)
          return;
        this._product = value;
        this.ProductString = value == null ? "" : value.ToString();
      }
    }

    public string ProductId
    {
      get => this._product != null ? this._product.Id : this._productId;
      set
      {
        value = value?.Trim();
        this._productId = value;
        if (this._product != null && this._product.Id != this._productId)
          this._product = (Product) null;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public void Update(Price price)
    {
      if (price.CurrencyId == null)
        this.Currency = (Currency) null;
      this.Amount = price.Amount;
      this.CurrencyId = price.CurrencyId;
      this.IsActive = price.IsActive;
      this.IsLive = price.IsLive;
      this.PeriodId = price.PeriodId;
      this.ProductId = price.ProductId;
    }
  }
}
