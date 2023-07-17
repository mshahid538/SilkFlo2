using System;

namespace SilkFlo.Web.ViewModels.Explore
{
    public class People
    {
        public bool IsPractice { get; set; }
        public bool NoLocations { get; set; }
        public bool NoBusinessUnits { get; set; }
        public Models.User[] Users { get; set; } = Array.Empty<Models.User>();
        public string SearchText { get; set; }
    }
}