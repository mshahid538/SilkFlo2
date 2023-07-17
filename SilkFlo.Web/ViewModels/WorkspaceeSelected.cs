using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels
{
    public class WorkspaceSelected
    {
        public WorkspaceSelected(string selectClientId, Dictionary<string, string> clients)
        {
            SelectClientId = selectClientId;
            Clients = clients;
        }

        public string SelectClientId { get; }

        public bool IsPracticeAccount { get; set; }

        public Dictionary<string, string> Clients { get; }

        public string TenantId { get; set; }

        public bool IsPracticeUser { get; set; }

        public bool ShowPracticeToggle { get; set; }
    }
}