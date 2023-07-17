namespace SilkFlo.Web.ViewModels.Elements.ToolBar
{
    public class Button : Element
    {
        public string Label { get; set; }
        public string OnClick { get; set; }
        public string ModalId { get; set; }
        public string Class { get; set; } = "btn btn-primary hide";
        public string Style { get; set; }
        public int Sort { get; set; }

        public override string ToString()
        {
            return Label;
        }
    }
}