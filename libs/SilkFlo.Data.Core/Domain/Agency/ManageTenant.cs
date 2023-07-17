using SilkFlo.Data.Core.Domain.Business;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Agency
{
  [XmlType(Namespace = "Agency")]
  public class ManageTenant : Abstract
  {
    private string _id = "";
    private Client _tenant;
    private string _tenantId;
    private User _user;
    private string _userId;

    public ManageTenant() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string UserString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public User User
    {
      get => this._user;
      set
      {
        if (this._user == value)
          return;
        this._user = value;
        this.UserString = value == null ? "" : value.ToString();
      }
    }

    public string UserId
    {
      get => this._user != null ? this._user.Id : this._userId;
      set
      {
        value = value?.Trim();
        this._userId = value;
        if (this._user != null && this._user.Id != this._userId)
          this._user = (User) null;
        this.IsSaved = false;
      }
    }

    public void Update(ManageTenant manageTenant)
    {
      this.TenantId = manageTenant.TenantId;
      this.UserId = manageTenant.UserId;
    }
  }
}
