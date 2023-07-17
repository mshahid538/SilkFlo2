using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain
{
  [XmlType(Namespace = "")]
  public class WebHookLog : Abstract
  {
    private string _id = "";
    private string _keyId = "";
    private string _sourceId = "";

    public WebHookLog() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string KeyId
    {
      get => this._keyId;
      set
      {
        value = value?.Trim();
        if (this._keyId == value)
          return;
        this._keyId = value;
        this.IsSaved = false;
      }
    }

    public string SourceId
    {
      get => this._sourceId;
      set
      {
        value = value?.Trim();
        if (this._sourceId == value)
          return;
        this._sourceId = value;
        this.IsSaved = false;
      }
    }

    public void Update(WebHookLog webHookLog)
    {
      this.KeyId = webHookLog.KeyId;
      this.SourceId = webHookLog.SourceId;
    }
  }
}
