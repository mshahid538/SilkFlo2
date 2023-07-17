using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.Version
{
    public class Summary : SummaryAbstract
    {
        public Summary(
            List<Models.Business.Version> versions)
        {
            Versions = versions;
        }


        public List<Models.Business.Version> Versions { get; }
    }
}
