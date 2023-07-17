using System.Collections.Generic;

namespace SilkFlo.Web.Models.Business
{
    public partial class Role
    {
        public List<Shared.IdeaAuthorisation> IdeaAuthorisations { get; set; } = new();
        public RoleCost RoleCost { get; set; }

        public decimal Cost
        {
            get
            {
                if (RoleCost == null)
                    return 0;

                return RoleCost.Cost;
            }
        }
    }
}
