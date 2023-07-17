using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shop
{
  [XmlType(Namespace = "Shop")]
  public class Product : Abstract
  {
    private string _id = "";
    private int? _adminUserLimit;
    private string _adminUserText = "";
    private int? _collaboratorLimit;
    private string _collaboratorText = "";
    private int? _ideaLimit;
    private string _ideaText = "";
    private bool _isLive;
    private bool _isVisible;
    private string _name = "";
    private bool _noPrice;
    private string _note = "";
    private int _sort;
    private int? _standardUserLimit;
    private string _standardUserText = "";
    private int? _storageLimit;
    private string _storageText = "";

    public Product() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public int? AdminUserLimit
    {
      get => this._adminUserLimit;
      set
      {
        int? adminUserLimit = this._adminUserLimit;
        int? nullable = value;
        if (adminUserLimit.GetValueOrDefault() == nullable.GetValueOrDefault() & adminUserLimit.HasValue == nullable.HasValue)
          return;
        this._adminUserLimit = value;
        this.IsSaved = false;
      }
    }

    public string AdminUserText
    {
      get => this._adminUserText;
      set
      {
        value = value?.Trim();
        if (this._adminUserText == value)
          return;
        this._adminUserText = value;
        this.IsSaved = false;
      }
    }

    public int? CollaboratorLimit
    {
      get => this._collaboratorLimit;
      set
      {
        int? collaboratorLimit = this._collaboratorLimit;
        int? nullable = value;
        if (collaboratorLimit.GetValueOrDefault() == nullable.GetValueOrDefault() & collaboratorLimit.HasValue == nullable.HasValue)
          return;
        this._collaboratorLimit = value;
        this.IsSaved = false;
      }
    }

    public string CollaboratorText
    {
      get => this._collaboratorText;
      set
      {
        value = value?.Trim();
        if (this._collaboratorText == value)
          return;
        this._collaboratorText = value;
        this.IsSaved = false;
      }
    }

    public int? IdeaLimit
    {
      get => this._ideaLimit;
      set
      {
        int? ideaLimit = this._ideaLimit;
        int? nullable = value;
        if (ideaLimit.GetValueOrDefault() == nullable.GetValueOrDefault() & ideaLimit.HasValue == nullable.HasValue)
          return;
        this._ideaLimit = value;
        this.IsSaved = false;
      }
    }

    public string IdeaText
    {
      get => this._ideaText;
      set
      {
        value = value?.Trim();
        if (this._ideaText == value)
          return;
        this._ideaText = value;
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

    public bool IsVisible
    {
      get => this._isVisible;
      set
      {
        if (this._isVisible == value)
          return;
        this._isVisible = value;
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

    public bool NoPrice
    {
      get => this._noPrice;
      set
      {
        if (this._noPrice == value)
          return;
        this._noPrice = value;
        this.IsSaved = false;
      }
    }

    public string Note
    {
      get => this._note;
      set
      {
        value = value?.Trim();
        if (this._note == value)
          return;
        this._note = value;
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

    public int? StandardUserLimit
    {
      get => this._standardUserLimit;
      set
      {
        int? standardUserLimit = this._standardUserLimit;
        int? nullable = value;
        if (standardUserLimit.GetValueOrDefault() == nullable.GetValueOrDefault() & standardUserLimit.HasValue == nullable.HasValue)
          return;
        this._standardUserLimit = value;
        this.IsSaved = false;
      }
    }

    public string StandardUserText
    {
      get => this._standardUserText;
      set
      {
        value = value?.Trim();
        if (this._standardUserText == value)
          return;
        this._standardUserText = value;
        this.IsSaved = false;
      }
    }

    public int? StorageLimit
    {
      get => this._storageLimit;
      set
      {
        int? storageLimit = this._storageLimit;
        int? nullable = value;
        if (storageLimit.GetValueOrDefault() == nullable.GetValueOrDefault() & storageLimit.HasValue == nullable.HasValue)
          return;
        this._storageLimit = value;
        this.IsSaved = false;
      }
    }

    public string StorageText
    {
      get => this._storageText;
      set
      {
        value = value?.Trim();
        if (this._storageText == value)
          return;
        this._storageText = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Price> Prices { get; set; } = new List<Price>();

    public void Update(Product product)
    {
      this.AdminUserLimit = product.AdminUserLimit;
      this.AdminUserText = product.AdminUserText;
      this.CollaboratorLimit = product.CollaboratorLimit;
      this.CollaboratorText = product.CollaboratorText;
      this.IdeaLimit = product.IdeaLimit;
      this.IdeaText = product.IdeaText;
      this.IsLive = product.IsLive;
      this.IsVisible = product.IsVisible;
      this.Name = product.Name;
      this.NoPrice = product.NoPrice;
      this.Note = product.Note;
      this.Sort = product.Sort;
      this.StandardUserLimit = product.StandardUserLimit;
      this.StandardUserText = product.StandardUserText;
      this.StorageLimit = product.StorageLimit;
      this.StorageText = product.StorageText;
    }

    public override string ToString() => this.Name;
  }
}
