using System;

namespace SilkFlo.Web.Controllers2.FileUpload
{
    public static class StringExtensions
    {
        public static string TruncateLongString(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return str.Substring(0, Math.Min(str.Length, maxLength));
        }
    }
}
