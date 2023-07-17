using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Application
{
  [XmlType(Namespace = "Application")]
  public class Setting : Abstract
  {
    private string _id = "";
    private string _value = "";

    public Setting() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Value
    {
      get => this._value;
      set
      {
        value = value?.Trim();
        if (this._value == value)
          return;
        this._value = value;
        this.IsSaved = false;
      }
    }

    public void Update(Setting setting) => this.Value = setting.Value;
  }
}
