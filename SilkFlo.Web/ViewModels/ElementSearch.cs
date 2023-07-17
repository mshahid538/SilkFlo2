using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.ViewModels
{
    public class ElementSearch
    {
        private string _id = "";
        public string Id
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_id))
                    return "Search.Element";

                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Name { get; set; }

        public string PlaceHolder { get; set; } = "Search Here ...";

        public string JavaScriptOnKeyUp { get; set; }

        public string MaxWidth { get; set; } //= "max-width: 400px;";

        public string MarginBottom { get; set; } = "1rem;";

        public string Attributes
        {
            get
            {
                var str = "";

                
                if (!string.IsNullOrWhiteSpace(Id))
                    str += $" id=\"{Id}\"";

                if (!string.IsNullOrWhiteSpace(Name))
                    str += $" name=\"{Name}\"";

                if (!string.IsNullOrWhiteSpace(PlaceHolder))
                    str += $" placeholder=\"{PlaceHolder}\"";


                if (!string.IsNullOrWhiteSpace(JavaScriptOnKeyUp))
                    str += $" onkeyup=\"{JavaScriptOnKeyUp}\"";

                return str;
            }
        }

        public string AttributesContainer
        {
            get
            {
                string str = "";

                string style = "";
                if (!string.IsNullOrWhiteSpace(MaxWidth))
                    style += $"max-width: {MaxWidth};";

                if (!string.IsNullOrWhiteSpace(MarginBottom))
                    style += $"margin-bottom: {MarginBottom};";

                if (!string.IsNullOrWhiteSpace(style))
                {
                    style = " style=\"" + style + "\"";
                    str += style;
                }

                return str;
            }
        }
    }
}
