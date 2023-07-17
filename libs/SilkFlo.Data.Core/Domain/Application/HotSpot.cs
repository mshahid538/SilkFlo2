using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Application
{
  [XmlType(Namespace = "Application")]
  public class HotSpot : Abstract
  {
    private string _id = "";
    private string _name = "";
    private string _text = "";
    private string _width = "";

    public HotSpot() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Width
    {
      get => this._width;
      set
      {
        value = value?.Trim();
        if (this._width == value)
          return;
        this._width = value;
        this.IsSaved = false;
      }
    }

    public void Update(HotSpot hotSpot)
    {
      this.Name = hotSpot.Name;
      this.Text = hotSpot.Text;
      this.Width = hotSpot.Width;
    }

    public override string ToString() => this.Name;
  }
}
