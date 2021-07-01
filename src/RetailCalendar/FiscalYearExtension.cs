using System;

namespace RetailCalendar
{
    public static class FiscalYearExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Fiscal Year Number, Start of Fiscal Year, End of Fiscal Year, Number of Days in fiscal year</returns>
        public static (int, DateTime, DateTime, int) FiscalYearDetails(this DateTime date)
        {
            var startOfYear = date.StartOfYear();
            var endOfYear = date.EndOfYear();
            var fiscalYear = startOfYear.FiscalYearFromStartOfYear();
            var daysInYear = date.DaysInYear();
            return (fiscalYear, startOfYear, endOfYear, daysInYear);
        }

        public static int FiscalYear(this DateTime date)
        {
            var startOfYear = date.StartOfYear(); // TODO: Circular Reference
            return FiscalYearFromStartOfYear(startOfYear);
        }

        private static int FiscalYearFromStartOfYear(this DateTime startOfYear)
        {
            return startOfYear.Year;
        }

        public static DateTime StartOfYear(this DateTime date)
        {
            var year = date.Year;
            var fiscalYear = StartOfYear(year);
            var previousYear = StartOfYear(year - 1);
            return date >= fiscalYear ? fiscalYear : previousYear;
        }

        public static DateTime StartOfYear(int fiscalYear)
        {
            return new DateTime(fiscalYear, 1, 28).NextByDayOfWeek(DayOfWeek.Sunday);
        }

        public static DateTime EndOfYear(this DateTime date)
        {
            var year = date.FiscalYear();
            return EndOfYear(year);
        }

        public static DateTime EndOfYear(int fiscalYear)
        {
            return new DateTime(fiscalYear + 1, 1, 27).NextByDayOfWeek(DayOfWeek.Saturday);
        }

        public static int DaysInYear(this DateTime date)
        {
            var startOfYear = date.StartOfYear();
            var endOfYear = date.EndOfYear();
            return DaysInYear(startOfYear, endOfYear);
        }

        public static int DaysInYear(int fiscalYear)
        {
            var startOfYear = StartOfYear(fiscalYear);
            var endOfYear = EndOfYear(fiscalYear);
            return DaysInYear(startOfYear, endOfYear);
        }

        private static int DaysInYear(DateTime startOfYear, DateTime endOfYear)
        {
            return (endOfYear - startOfYear).Days + 1;
        }

        public static int WeeksInYear(this DateTime date)
        {
            var days = date.DaysInYear();
            return days / 7;
        }

        public static int WeeksInYear(int fiscalYear)
        {
            var days = DaysInYear(fiscalYear);
            return days / 7;
        }
    }
}
