using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.SoftwareVender 
{
    public class Summary : SummaryAbstract
    {
        public Summary(
            List<Models.Business.SoftwareVender> softwareVenders)
        {
            SoftwareVenders = softwareVenders;
        }


        public List<Models.Business.SoftwareVender> SoftwareVenders { get; }
    }
}