namespace SilkFlo.Web.ViewModels
{
    public class Home
    {
        public ViewModels.Dashboard.Tenant TenantDashboard { get; set; }
        public ViewModels.Dashboard.Agency AgencyDashboard { get; set; }
        public ViewModels.Dashboard.Administrator AdministratorDashboard { get; set; }

        public string ClientId { get; set; }
    }
}
