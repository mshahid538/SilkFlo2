using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain
{
  public abstract class Abstract
  {
    protected User _createdBy;
    protected string _createdById;
    protected System.DateTime? _createdDate;
    protected User _updatedBy;
    protected string _updatedById;
    protected System.DateTime? _updatedDate;
    protected bool _isSaved = true;

    public abstract bool IsNew { get; }

    public bool IsDeleted { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public User Owner => this.UpdatedBy == null ? this.CreatedBy : this.UpdatedBy;

    [IgnoreDataMember]
    [XmlIgnore]
    public User CreatedBy
    {
      get => this._createdBy;
      set
      {
        if (this._createdBy == value)
          return;
        this._createdBy = value;
        this._createdById = this._createdBy != null ? this._createdBy.Id : (string) null;
        this.IsSaved = false;
      }
    }

    public string CreatedById
    {
      get => this._createdById;
      set
      {
        if (!(this._createdById != value))
          return;
        this._createdById = value;
        this.IsSaved = false;
      }
    }

    public System.DateTime? CreatedDate
    {
      get => this._createdDate;
      set
      {
        System.DateTime? createdDate = this._createdDate;
        System.DateTime? nullable = value;
        if (createdDate.HasValue == nullable.HasValue && (!createdDate.HasValue || !(createdDate.GetValueOrDefault() != nullable.GetValueOrDefault())))
          return;
        this._createdDate = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public string CreatedDateString
    {
      get
      {
        System.DateTime? createdDate = this.CreatedDate;
        ref System.DateTime? local = ref createdDate;
        return local.HasValue ? local.GetValueOrDefault().ToString(Settings.DateFormatShort) : (string) null;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public User UpdatedBy
    {
      get => this._updatedBy;
      set
      {
        if (this._updatedBy == value)
          return;
        this._updatedBy = value;
        this._updatedById = this._updatedBy != null ? this._updatedBy.Id : (string) null;
        this.IsSaved = false;
      }
    }

    public string UpdatedById
    {
      get => this._updatedById;
      set
      {
        if (!(this._updatedById != value))
          return;
        this._updatedById = value;
        this.IsSaved = false;
      }
    }

    public System.DateTime? UpdatedDate
    {
      get => this._updatedDate;
      set
      {
        System.DateTime? updatedDate = this._updatedDate;
        System.DateTime? nullable = value;
        if (updatedDate.HasValue == nullable.HasValue && (!updatedDate.HasValue || !(updatedDate.GetValueOrDefault() != nullable.GetValueOrDefault())))
          return;
        this._updatedDate = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public string UpdatedDateString
    {
      get
      {
        System.DateTime? updatedDate = this.UpdatedDate;
        ref System.DateTime? local = ref updatedDate;
        return local.HasValue ? local.GetValueOrDefault().ToString(Settings.DateFormatShort) : (string) null;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public System.DateTime? Date
    {
      get
      {
        if (this.UpdatedDate.HasValue)
        {
          System.DateTime? updatedDate = this.UpdatedDate;
          ref System.DateTime? local = ref updatedDate;
          return local.HasValue ? new System.DateTime?(local.GetValueOrDefault().Date) : new System.DateTime?();
        }
        System.DateTime? date = this.CreatedDate;
        if (date.HasValue)
        {
          date = this.CreatedDate;
          ref System.DateTime? local = ref date;
          return local.HasValue ? new System.DateTime?(local.GetValueOrDefault().Date) : new System.DateTime?();
        }
        date = new System.DateTime?();
        return date;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public System.DateTime? DateTime
    {
      get
      {
        if (this.UpdatedDate.HasValue)
          return this.UpdatedDate;
        System.DateTime? dateTime = this.CreatedDate;
        if (dateTime.HasValue)
          return this.CreatedDate;
        dateTime = new System.DateTime?();
        return dateTime;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public string DateString
    {
      get
      {
        if (!this.Date.HasValue)
          return "";
        System.DateTime? date = this.Date;
        ref System.DateTime? local = ref date;
        return local.HasValue ? local.GetValueOrDefault().ToString(Settings.DateFormatShort) : (string) null;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public bool IsSaved
    {
      get => this._isSaved;
      set
      {
        if (this._isSaved == value || Settings.IsOpening)
          return;
        this._isSaved = value;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public bool CanDelete { get; set; }
  }
}
