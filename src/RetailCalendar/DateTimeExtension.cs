using System;
using System.Net.NetworkInformation;

namespace RetailCalendar
{
    public static class DateTimeExtension
    {
        public static DateTime NextOrEqualByDayOfWeek(this DateTime from, DayOfWeek dayOfWeek)
        {
            var start = (int)from.DayOfWeek;
            var target = (int)dayOfWeek;
            if (target < start)
                target += 7;
            return from.AddDays(target - start).Date;
        }

        public static DateTime PreviousOrEqualByDayOfWeek(this DateTime from, DayOfWeek dayOfWeek)
        {
            var start = (int)from.DayOfWeek;
            var target = (int)dayOfWeek;
            if (target > start)
                target -= 7;
            return from.AddDays(target - start).Date;
        }

        public static DateTime AddWeeks(this DateTime date, int numberOfWeeks) => date.AddDays(numberOfWeeks * 7);

        public static int DaysBetweenDates(DateTime startDate, DateTime endDate) => (endDate.Date - startDate.Date).Days + 1;

        public static int DaysToWeeks(this int days) => days / 7;
    }
}
