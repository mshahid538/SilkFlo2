using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shop
{
  [XmlType(Namespace = "Shop")]
  public class Subscription : Abstract
  {
    private string _id = "";
    private SilkFlo.Data.Core.Domain.Shop.Discount _agencyDiscount;
    private string _agencyDiscountId;
    private Client _agency;
    private string _agencyId;
    private ClientType _agencyType;
    private string _agencyTypeId;
    private Decimal? _amount;
    private string _cancelToken = "";
    private Coupon _coupon;
    private string _couponId;
    private System.DateTime? _dateCancelled;
    private System.DateTime? _dateEnd;
    private System.DateTime _dateStart;
    private Decimal? _discount;
    private string _invoiceId = "";
    private string _invoiceNumber = "";
    private string _invoiceUrl = "";
    private Price _price;
    private string _priceId;
    private Client _tenant;
    private string _tenantId;

    public Subscription() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string AgencyDiscountString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public SilkFlo.Data.Core.Domain.Shop.Discount AgencyDiscount
    {
      get => this._agencyDiscount;
      set
      {
        if (this._agencyDiscount == value)
          return;
        this._agencyDiscount = value;
        this.AgencyDiscountString = value == null ? "" : value.ToString();
      }
    }

    public string AgencyDiscountId
    {
      get => this._agencyDiscount != null ? this._agencyDiscount.Id : this._agencyDiscountId;
      set
      {
        value = value?.Trim();
        this._agencyDiscountId = value;
        if (this._agencyDiscount != null && this._agencyDiscount.Id != this._agencyDiscountId)
          this._agencyDiscount = (SilkFlo.Data.Core.Domain.Shop.Discount) null;
        this.IsSaved = false;
      }
    }

    public string AgencyString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Client Agency
    {
      get => this._agency;
      set
      {
        if (this._agency == value)
          return;
        this._agency = value;
        this.AgencyString = value == null ? "" : value.ToString();
      }
    }

    public string AgencyId
    {
      get => this._agency != null ? this._agency.Id : this._agencyId;
      set
      {
        value = value?.Trim();
        this._agencyId = value;
        if (this._agency != null && this._agency.Id != this._agencyId)
          this._agency = (Client) null;
        this.IsSaved = false;
      }
    }

    public string AgencyTypeString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public ClientType AgencyType
    {
      get => this._agencyType;
      set
      {
        if (this._agencyType == value)
          return;
        this._agencyType = value;
        this.AgencyTypeString = value == null ? "" : value.ToString();
      }
    }

    public string AgencyTypeId
    {
      get => this._agencyType != null ? this._agencyType.Id : this._agencyTypeId;
      set
      {
        value = value?.Trim();
        this._agencyTypeId = value;
        if (this._agencyType != null && this._agencyType.Id != this._agencyTypeId)
          this._agencyType = (ClientType) null;
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

    public string CancelToken
    {
      get => this._cancelToken;
      set
      {
        value = value?.Trim();
        if (this._cancelToken == value)
          return;
        this._cancelToken = value;
        this.IsSaved = false;
      }
    }

    public string CouponString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Coupon Coupon
    {
      get => this._coupon;
      set
      {
        if (this._coupon == value)
          return;
        this._coupon = value;
        this.CouponString = value == null ? "" : value.ToString();
      }
    }

    public string CouponId
    {
      get => this._coupon != null ? this._coupon.Id : this._couponId;
      set
      {
        value = value?.Trim();
        this._couponId = value;
        if (this._coupon != null && this._coupon.Id != this._couponId)
          this._coupon = (Coupon) null;
        this.IsSaved = false;
      }
    }

    public System.DateTime? DateCancelled
    {
      get => this._dateCancelled;
      set
      {
        System.DateTime? dateCancelled = this._dateCancelled;
        System.DateTime? nullable = value;
        if (dateCancelled.HasValue == nullable.HasValue && (!dateCancelled.HasValue || dateCancelled.GetValueOrDefault() == nullable.GetValueOrDefault()))
          return;
        this._dateCancelled = value;
        this.IsSaved = false;
      }
    }

    public System.DateTime? DateEnd
    {
      get => this._dateEnd;
      set
      {
        System.DateTime? dateEnd = this._dateEnd;
        System.DateTime? nullable = value;
        if (dateEnd.HasValue == nullable.HasValue && (!dateEnd.HasValue || dateEnd.GetValueOrDefault() == nullable.GetValueOrDefault()))
          return;
        this._dateEnd = value;
        this.IsSaved = false;
      }
    }

    public System.DateTime DateStart
    {
      get => this._dateStart;
      set
      {
        if (this._dateStart == value)
          return;
        this._dateStart = value;
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

    public string InvoiceId
    {
      get => this._invoiceId;
      set
      {
        value = value?.Trim();
        if (this._invoiceId == value)
          return;
        this._invoiceId = value;
        this.IsSaved = false;
      }
    }

    public string InvoiceNumber
    {
      get => this._invoiceNumber;
      set
      {
        value = value?.Trim();
        if (this._invoiceNumber == value)
          return;
        this._invoiceNumber = value;
        this.IsSaved = false;
      }
    }

    public string InvoiceUrl
    {
      get => this._invoiceUrl;
      set
      {
        value = value?.Trim();
        if (this._invoiceUrl == value)
          return;
        this._invoiceUrl = value;
        this.IsSaved = false;
      }
    }

    public string PriceString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Price Price
    {
      get => this._price;
      set
      {
        if (this._price == value)
          return;
        this._price = value;
        this.PriceString = value == null ? "" : value.ToString();
      }
    }

    public string PriceId
    {
      get => this._price != null ? this._price.Id : this._priceId;
      set
      {
        value = value?.Trim();
        this._priceId = value;
        if (this._price != null && this._price.Id != this._priceId)
          this._price = (Price) null;
        this.IsSaved = false;
      }
    }

    public string TenantString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Client Tenant
    {
      get => this._tenant;
      set
      {
        if (this._tenant == value)
          return;
        this._tenant = value;
        this.TenantString = value == null ? "" : value.ToString();
      }
    }

    public string TenantId
    {
      get => this._tenant != null ? this._tenant.Id : this._tenantId;
      set
      {
        value = value?.Trim();
        this._tenantId = value;
        if (this._tenant != null && this._tenant.Id != this._tenantId)
          this._tenant = (Client) null;
        this.IsSaved = false;
      }
    }

    public void Update(Subscription subscription)
    {
      if (subscription.AgencyDiscountId == null)
        this.AgencyDiscount = (SilkFlo.Data.Core.Domain.Shop.Discount) null;
      if (subscription.AgencyId == null)
        this.Agency = (Client) null;
      if (subscription.AgencyTypeId == null)
        this.AgencyType = (ClientType) null;
      if (subscription.CouponId == null)
        this.Coupon = (Coupon) null;
      if (subscription.PriceId == null)
        this.Price = (Price) null;
      this.AgencyDiscountId = subscription.AgencyDiscountId;
      this.AgencyId = subscription.AgencyId;
      this.AgencyTypeId = subscription.AgencyTypeId;
      this.Amount = subscription.Amount;
      this.CancelToken = subscription.CancelToken;
      this.CouponId = subscription.CouponId;
      this.DateCancelled = subscription.DateCancelled;
      this.DateEnd = subscription.DateEnd;
      this.DateStart = subscription.DateStart;
      this.Discount = subscription.Discount;
      this.InvoiceId = subscription.InvoiceId;
      this.InvoiceNumber = subscription.InvoiceNumber;
      this.InvoiceUrl = subscription.InvoiceUrl;
      this.PriceId = subscription.PriceId;
      this.TenantId = subscription.TenantId;
    }
  }
}
