using System.Collections.Generic;
using System.Linq;

namespace SilkFlo.Web.Models.Business
{
    public class ManageStatusHelper
    {
        readonly IEnumerable<Data.Core.Domain.Shared.IdeaStatus> _statuses;
        public ManageStatusHelper(IEnumerable<Data.Core.Domain.Shared.IdeaStatus> statuses)
        {
            _statuses = statuses;
            Statuses = new List<Data.Core.Domain.Shared.IdeaStatus>();
        }


        public List<Data.Core.Domain.Shared.IdeaStatus> Statuses { get; }

        public void Add(Data.Core.Enumerators.IdeaStatus id)
        {
            var child = _statuses
                .FirstOrDefault(x => x.Id == id.ToString());
            Statuses.Add(child);
        }
    }
}
