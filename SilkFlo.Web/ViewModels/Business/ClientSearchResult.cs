using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business
{
    public class ClientSearchResult
    {
        public List<Models.Business.Client> Clients { get; set; } = new List<Models.Business.Client>();

        public int? FirstPage { get; set; }
        public int? Page { get; set; }
        public int? LastPage { get; set; }
        public string SearchText { get; set; }
    }
}
