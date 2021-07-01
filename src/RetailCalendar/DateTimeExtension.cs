using System;

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
    }
}
