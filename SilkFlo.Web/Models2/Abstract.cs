namespace SilkFlo.Web.Models
{
    public partial class Abstract
    {
        public string MakeAttributeFriendly(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            else
            {
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
                str = str.Replace("\"", "&quot;");
                str = str.Replace("'", "&#39;");
                str = str.Replace("&", "&amp;");
                str = str.Replace("\r\n", "");
                str = str.Replace("#", "%23");

                return str;
            }
        }
    }
}
