using System;

namespace SilkFlo.Web.Exceptions
{
    public class DateBeforeException : Exception
    {
        public DateBeforeException(string message) : base(message) { }
    }
}