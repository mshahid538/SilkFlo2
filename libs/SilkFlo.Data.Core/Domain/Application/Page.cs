using System;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Application
{
  [XmlType(Namespace = "Application")]
  public class Page : Abstract
  {
    private string _id = "";
    private bool _canDelete;
    private bool _isMenuItem;
    private string _name = "";
    private int _sort;
    private string _text = "";
    private Decimal? _textHeight;
    private string _uRL = "";

    public Page() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public new bool CanDelete
    {
      get => this._canDelete;
      set
      {
        if (this._canDelete == value)
          return;
        this._canDelete = value;
        this.IsSaved = false;
      }
    }

    public bool IsMenuItem
    {
      get => this._isMenuItem;
      set
      {
        if (this._isMenuItem == value)
          return;
        this._isMenuItem = value;
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

    public string Text
    {
      get => this._text;
      set
      {
        value = value?.Trim();
        if (this._text == value)
          return;
        this._text = value;
        this.IsSaved = false;
      }
    }

    public Decimal? TextHeight
    {
      get => this._textHeight;
      set
      {
        Decimal? textHeight = this._textHeight;
        Decimal? nullable = value;
        if (textHeight.GetValueOrDefault() == nullable.GetValueOrDefault() & textHeight.HasValue == nullable.HasValue)
          return;
        this._textHeight = value;
        this.IsSaved = false;
      }
    }

    public string URL
    {
      get => this._uRL;
      set
      {
        value = value?.Trim();
        if (this._uRL == value)
          return;
        this._uRL = value;
        this.IsSaved = false;
      }
    }

    public void Update(Page page)
    {
      this.CanDelete = page.CanDelete;
      this.IsMenuItem = page.IsMenuItem;
      this.Name = page.Name;
      this.Sort = page.Sort;
      this.Text = page.Text;
      this.TextHeight = page.TextHeight;
      this.URL = page.URL;
    }

    public override string ToString() => this.Name;
  }
}
