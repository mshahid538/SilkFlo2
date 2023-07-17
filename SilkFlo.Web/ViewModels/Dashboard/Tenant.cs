using System.Collections.Generic;
using System.Linq;

namespace SilkFlo.Web.ViewModels.Dashboard
{
    public class Tenant
    {
        public Tenant()
        {         
        }

        public string ClientName { get; set; }

        public bool NoIdeas { get; set; }

        public bool IsPractice { get; set; }

        public List<int> Years { get; set; }

		public List<Models.Business.Department> Departments { get; set; } = new();
		public List<KeyValuePair<string, string>> POList { get; set; }
        public List<KeyValuePair<string, string>> ISList { get; set; }

    }
}
