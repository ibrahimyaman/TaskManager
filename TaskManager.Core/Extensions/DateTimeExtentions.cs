using System;
using System.Globalization;

namespace TaskManager.Core.Extensions
{
    public static class DateTimeExtentions
    {
        public static DateTime FirstDateOfWeek(this DateTime dateTime, int year, int weekNumberOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);

            int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;

            DateTime firstDayOfFirstWeek = jan1.AddDays(daysOffset);

            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1)
            {
                weekNumberOfYear -= 1;
            }

            return firstDayOfFirstWeek.AddDays(weekNumberOfYear * 7);
        }
        public static int Week(this DateTime time)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }
        public static DateTime AddWeeks(this DateTime date, double value)
        {
            return date.AddDays(7 * value);
        }
    }
}
