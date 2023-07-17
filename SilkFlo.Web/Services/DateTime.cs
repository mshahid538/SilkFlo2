namespace SilkFlo.Web.Services
{
    public class DateTimeTools
    {
        public static bool IsOverLap(
            System.DateTime startA,
            System.DateTime endA,
            System.DateTime startB,
            System.DateTime endB)
        {
            // https://en.wikipedia.org/wiki/De_Morgan%27s_laws
            // Not (A Or B) <=> Not A And Not B
            // Which translates to: (StartA <= EndB)  and(EndA >= StartB)
            return System.DateTime.Compare(startA, endB) <= 0  && System.DateTime.Compare(endA, startB) >= 0;
        }
    }
}