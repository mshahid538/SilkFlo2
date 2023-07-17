namespace SilkFlo.Web.ViewModels.Elements.TabBar
{
    /// <summary>
    /// User with the _TableBar Element
    /// </summary>
    public class Tab
    {
        /// <summary>
        /// Display Name
        /// For Example:
        /// Dashboard/Personal
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Element Name without the suffix .Tab or .Content
        /// For Example:
        /// Dashboard.Personal.Tab becomes Dashboard.Personal
        /// Dashboard.Personal.Content becomes Dashboard.Personal
        /// </summary>
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public string ParentId { get; set; }
        public bool GetOnSelect { get; set; }
        public string DisplayPath { get; set; }
        public int Sort { get; set; }
        public bool LoadOnce { get; set; }
        public string Html
        {
            get
            {
                var active = IsActive ? "active " : "";

                var html = $"<h3 class=\"{active}silkflo-tab-label\"";

                if (!string.IsNullOrWhiteSpace(Name))
                    html += $" name=\"{Name}.Tab\"";

                if (!string.IsNullOrWhiteSpace(ParentId))
                    html += $" parentid=\"{ParentId}\"";

                if (!string.IsNullOrWhiteSpace(DisplayPath))
                    html += $" displaypath=\"{DisplayPath}\"";

                if (GetOnSelect)
                    html += "getOnSelect=\"\"";


                html += $">{Title}</h3>";

                return html;
            }
        }
    }
}