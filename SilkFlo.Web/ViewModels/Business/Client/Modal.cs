using System;

namespace SilkFlo.Web.ViewModels.Business.Client
{
    public class Modal
    {
        public Models.Business.Client Client { get; set; }
        public bool ShowLicence { get; set; }

        public bool ShowActivateCheckBox { get; set; }

        public bool ShowSubscription { get; set; }


        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}