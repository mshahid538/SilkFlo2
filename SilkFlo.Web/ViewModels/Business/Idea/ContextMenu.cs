namespace SilkFlo.Web.ViewModels.Business.Idea
{
    public class ContextMenu
    {
        public string MenuName { get; set; }
        public bool ShowManageStagesMenuItem { get; set; }
        public bool ShowViewDetailsMenuItem { get; set; }
        public bool ShowEditMenuItem { get; set; }
        public bool ShowDeleteMenuItem { get; set; }

        public bool ShowContextMenu =>
            ShowManageStagesMenuItem 
            && ShowViewDetailsMenuItem 
            && ShowEditMenuItem 
            && ShowDeleteMenuItem;
    }
}