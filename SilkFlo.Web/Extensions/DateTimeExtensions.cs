using System;

namespace SilkFlo.Web.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool InRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentOutOfRangeException("Start date time canot be greater than end date time.");

            var dateToCheckUtc = dateToCheck.ToUniversalTime();
            var startDateUtc = startDate.ToUniversalTime();
            var endDateUtc = endDate.ToUniversalTime();

            return dateToCheckUtc >= startDateUtc && dateToCheckUtc < endDateUtc;
        }
    }
}