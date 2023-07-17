using System;

namespace SilkFlo.Web.ViewModels
{
    public class Element_InputWithComment : Element
    {
        public string SubTitle { get; set; }

        public bool IsMandatory { get; set; }

        public string Line1
        {
            get
            {
                string line1 = IsMandatory ? "<label class=\"mandatory\">" : "<label>";
                line1 += Title;
                line1 += string.IsNullOrWhiteSpace(SubTitle) ? "" : $" <span class=\"subLabel\">{SubTitle}</span>";
                line1 += "</label>";
                return line1;
            }
        }

        public string ContainerAttributes
        { 
            get
            {
                string str = "";

                if(IsReadOnly)
                {
                    str += " class=\"form-group\" style=\"margin-bottom: 0.5rem;\"";
                }
                else
                {
                    string style = "margin-bottom: 0.5rem; display: grid; grid-template-columns: ";

                    if(!string.IsNullOrWhiteSpace(Prefix))
                        style += "auto ";

                    style += "1fr ";


                    if(!string.IsNullOrWhiteSpace(Suffix))
                        style += "auto ";

                    style += "auto; ";


                    str += $" class=\"inputAndButton\" style=\"{style}\"";
                }

                return str;
            }
        }

        public string InputElementAttributes
        {
            get
            {
                string str = "";

                // id
                if (!String.IsNullOrWhiteSpace(Id))
                    str += $" id=\"{Id}\"";


                // name
                if (!String.IsNullOrWhiteSpace(Name))
                    str += $" name=\"{Name}\"";


                // value
                if (!String.IsNullOrWhiteSpace(Value))
                    str += $" value=\"{Value}\"";


                // maxlength
                if (MaximumLength > 0)
                    str += $" maxlength=\"{MaximumLength}\"";

                string cls = "form-control";
                if (IsInvalid)
                    cls += " is-invalid";

                str += $" class=\"{cls}\"";

                if (IsReadOnly)
                {
                    // style
                    str += " style=\"border-bottom-left-radius: 0; border-bottom-right-radius: 0;\"";
                    str += " type=\"text\"";
                    str += " readonly";
                    str += " disabled";
                }
                else
                {
                    // style
                    str += " style=\"margin-bottom: 0; margin-bottom: 0; border-top-left-radius: var(--border-radius) !important; border-bottom-left-radius: var(--border-radius) !important;\"";

                    if (!String.IsNullOrWhiteSpace(ElementType))
                    {
                        str += $" type=\"{ElementType}\"";
                        if (ElementType.ToLower() == "number")
                        {
                            if (!string.IsNullOrWhiteSpace(Step))
                            {
                                str += $" step=\"{Step}\"";
                            }

                            // min number
                            if (!String.IsNullOrWhiteSpace(MinimumValue))
                                str += " min=\"" + MinimumValue + "\"\r\n";

                            // max number
                            if (!String.IsNullOrWhiteSpace(MaximumValue))
                                str += " max=\"" + MaximumValue + "\"\r\n";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(MinimumValue))
                        MinimumValue = ", " + MinimumValue;

                    if (!string.IsNullOrWhiteSpace(MaximumValue))
                        MaximumValue = ", " + MaximumValue;

                    if (!string.IsNullOrWhiteSpace(JavaScriptOnKeyDown))
                        str += $" onkeydown=\"{JavaScriptOnKeyDown}\"\r\n";

                    string onInput = "";
                    if (IsMandatory)
                    {
                        str += "onfocusout=\"SilkFlo.IsRequired(this" + MinimumValue + MaximumValue + ", 'Required');\"\r\n";

                        onInput += "SilkFlo.IsRequired(this" + MinimumValue + MaximumValue + ", 'Required');";
                    }
                    else if (MinimumValue.Length > 0 || MaximumValue.Length > 0)
                    {
                        str += "onfocusout=\"SilkFlo.IsInRange(this" + MinimumValue + MaximumValue + ");\"\r\n";

                        onInput += "SilkFlo.IsInRange(this" + MinimumValue + MaximumValue + ");";
                    }


                    if (!string.IsNullOrWhiteSpace(JavaScriptOnInput))
                        onInput += " " + JavaScriptOnInput;


                    if (!String.IsNullOrWhiteSpace(onInput))
                        str += " oninput = \"" + onInput + "\"\r\n";

                }


                return str;
            }
        }
    }
}
