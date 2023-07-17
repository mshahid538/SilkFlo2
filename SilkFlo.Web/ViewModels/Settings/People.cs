namespace SilkFlo.Web.ViewModels.Settings
{
    public class People
    {
        public Models.Business.Client Client { get; set; }
        public string TableUrl { get; set; } = "Settings/People/Table";
        public bool GuestOnly { get; set; }
    }
}
