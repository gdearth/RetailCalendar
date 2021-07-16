using System;
using System.Net.NetworkInformation;

namespace RetailCalendar
{
    public static class DateTimeExtension
    {
        public static DateTime NextByDayOfWeek(this DateTime from, DayOfWeek dayOfWeek)
        {
            var start = (int)from.DayOfWeek;
            var target = (int)dayOfWeek;
            if (target <= start)
                target += 7;
            return from.AddDays(target - start).Date;
        }

        public static DateTime AddWeeks(this DateTime date, int numberOfWeeks) => date.AddDays(numberOfWeeks * 7);

        public static int DaysBetweenDates(DateTime startDate, DateTime endDate) => (endDate - startDate).Days + 1;
    }
}
