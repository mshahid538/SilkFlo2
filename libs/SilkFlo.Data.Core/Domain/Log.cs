using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain
{
  [XmlType(Namespace = "")]
  public class Log : Abstract
  {
    private string _id = "";
    private string _fullname = "";
    private string _functionName = "";
    private string _innerException = "";
    private string _message = "";
    private string _requestId = "";
    private int _severity;
    private string _source = "";
    private string _stackTrace = "";
    private string _targetSite = "";
    private string _text = "";
    private string _username = "";

    public Log() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Fullname
    {
      get => this._fullname;
      set
      {
        value = value?.Trim();
        if (this._fullname == value)
          return;
        this._fullname = value;
        this.IsSaved = false;
      }
    }

    public string FunctionName
    {
      get => this._functionName;
      set
      {
        value = value?.Trim();
        if (this._functionName == value)
          return;
        this._functionName = value;
        this.IsSaved = false;
      }
    }

    public string InnerException
    {
      get => this._innerException;
      set
      {
        value = value?.Trim();
        if (this._innerException == value)
          return;
        this._innerException = value;
        this.IsSaved = false;
      }
    }

    public string Message
    {
      get => this._message;
      set
      {
        value = value?.Trim();
        if (this._message == value)
          return;
        this._message = value;
        this.IsSaved = false;
      }
    }

    public string RequestId
    {
      get => this._requestId;
      set
      {
        value = value?.Trim();
        if (this._requestId == value)
          return;
        this._requestId = value;
        this.IsSaved = false;
      }
    }

    public int Severity
    {
      get => this._severity;
      set
      {
        if (this._severity == value)
          return;
        this._severity = value;
        this.IsSaved = false;
      }
    }

    public string Source
    {
      get => this._source;
      set
      {
        value = value?.Trim();
        if (this._source == value)
          return;
        this._source = value;
        this.IsSaved = false;
      }
    }

    public string StackTrace
    {
      get => this._stackTrace;
      set
      {
        value = value?.Trim();
        if (this._stackTrace == value)
          return;
        this._stackTrace = value;
        this.IsSaved = false;
      }
    }

    public string TargetSite
    {
      get => this._targetSite;
      set
      {
        value = value?.Trim();
        if (this._targetSite == value)
          return;
        this._targetSite = value;
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

    public string Username
    {
      get => this._username;
      set
      {
        value = value?.Trim();
        if (this._username == value)
          return;
        this._username = value;
        this.IsSaved = false;
      }
    }

    public void Update(Log log)
    {
      this.Fullname = log.Fullname;
      this.FunctionName = log.FunctionName;
      this.InnerException = log.InnerException;
      this.Message = log.Message;
      this.RequestId = log.RequestId;
      this.Severity = log.Severity;
      this.Source = log.Source;
      this.StackTrace = log.StackTrace;
      this.TargetSite = log.TargetSite;
      this.Text = log.Text;
      this.Username = log.Username;
    }

    public override string ToString() => this.Text;
  }
}
