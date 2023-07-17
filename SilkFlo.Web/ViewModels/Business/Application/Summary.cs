using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.Application
{
    public class Summary : SummaryAbstract
    {
        public Summary(
            List<Models.Business.Application> applications,
            List<Models.Shared.Language> languages)
        {
            Applications = applications;
            Languages = languages;
        }


        public List<Models.Business.Application> Applications { get; }
        public List<Models.Shared.Language> Languages { get; }
    }
}
