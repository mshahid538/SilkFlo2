namespace SilkFlo.Web.ViewModels
{
    public class SubmitIdeaButton
    {
        private SubmitIdeaButton() { }

        public SubmitIdeaButton(string id, string title)
        {
            Id = id;
            Title = title;
        }

        public SubmitIdeaButton(string id, string title, string onClick)
        {
            Id = id;
            Title = title;
            OnClick = onClick;
            ShowMenu = true;
        }

        public SubmitIdeaButton(string id, string title, string onClick, bool showMenu)
        {
            Id = id;
            Title = title;
            OnClick = onClick;
            ShowMenu = showMenu;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string OnClick { get; set; } = "SilkFlo.SideBar.SubmitIdea('submitIdeaMenu', 'btnSubmitContainer', 'right')";

        public bool ShowMenu { get; set; }
    }
}
