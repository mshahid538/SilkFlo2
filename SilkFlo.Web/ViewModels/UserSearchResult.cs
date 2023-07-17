using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels
{
    public class UserSearchResult
    {
        public List<Models.User> Users { get; set; } = new List<Models.User>();
        public string SearchText { get; set; }

    }
}