namespace SilkFlo.Web.Services
{
    internal class JSON_Tools
    {
        internal static string Replace(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.Replace("_a_m_p_;_", "&");
                s = s.Replace("_p_l_u_s_", "+");
            }

            return s;
        }
    }
}