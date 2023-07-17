using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.Idea.ManageCollaborator
{
    public class CollaboratorList
    {
        public Models.Business.Idea Idea { get; set; }
        public int? CollaboratorLimit { get; set; }
        public string ParentFormId { get; set; }
        public bool CanScroll { get; set; }
        public bool CanEditCollaborators { get; set; }
        public List<Models.User> CollaboratingUsers { get; set; } = new();
    }
}