using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Elements.TabBar
{
    public enum Justify
    {
        Start,
        Center,
        End
    }

    public class Bar
    {
        public Justify Justify { get; set; } = Justify.Start; 
        public List<Tab> Tabs { get; set; } = new();
    }
}