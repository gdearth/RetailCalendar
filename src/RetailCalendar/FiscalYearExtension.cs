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
            var startOfYear = date.StartOfFiscalYear();
            var endOfYear = date.EndOfFiscalYear();
            var fiscalYear = startOfYear.FiscalYearFromStartOfYear();
            var daysInYear = date.DaysInFiscalYear();
            return (fiscalYear, startOfYear, endOfYear, daysInYear);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <returns>Fiscal Year Number, Start of Fiscal Year, End of Fiscal Year, Number of Days in fiscal year</returns>
        public static (int, DateTime, DateTime, int) FiscalYearDetails(int fiscalYear)
        {
            var startOfYear = StartOfFiscalYear(fiscalYear);
            var endOfYear = EndOfFiscalYear(fiscalYear);
            var daysInYear = DaysInFiscalYear(fiscalYear);
            return (fiscalYear, startOfYear, endOfYear, daysInYear);
        }

        public static int FiscalYear(this DateTime date)
        {
            var startOfYear = date.StartOfFiscalYear(); // TODO: Circular Reference
            return FiscalYearFromStartOfYear(startOfYear);
        }

        private static int FiscalYearFromStartOfYear(this DateTime startOfYear)
        {
            return startOfYear.Year;
        }

        public static DateTime StartOfFiscalYear(this DateTime date)
        {
            var year = date.Year;
            var fiscalYear = StartOfFiscalYear(year);
            var previousYear = StartOfFiscalYear(year - 1);
            return date >= fiscalYear ? fiscalYear : previousYear;
        }

        public static DateTime StartOfFiscalYear(int fiscalYear)
        {
            return new DateTime(fiscalYear, 1, 28).NextByDayOfWeek(DayOfWeek.Sunday);
        }

        public static DateTime EndOfFiscalYear(this DateTime date)
        {
            var year = date.FiscalYear();
            return EndOfFiscalYear(year);
        }

        public static DateTime EndOfFiscalYear(int fiscalYear)
        {
            return new DateTime(fiscalYear + 1, 1, 27).NextByDayOfWeek(DayOfWeek.Saturday);
        }

        public static int DaysInFiscalYear(this DateTime date)
        {
            var startOfYear = date.StartOfFiscalYear();
            var endOfYear = date.EndOfFiscalYear();
            return DaysInFiscalYear(startOfYear, endOfYear);
        }

        public static int DaysInFiscalYear(int fiscalYear)
        {
            var startOfYear = StartOfFiscalYear(fiscalYear);
            var endOfYear = EndOfFiscalYear(fiscalYear);
            return DateTimeExtension.DaysBetweenDates(startOfYear, endOfYear);
        }

        private static int DaysInFiscalYear(DateTime startOfYear, DateTime endOfYear)
        {
            return (endOfYear - startOfYear).Days + 1;
        }

        public static int WeeksInFiscalYear(this DateTime date)
        {
            var days = date.DaysInFiscalYear();
            return days / 7;
        }

        public static int WeeksInFiscalYear(int fiscalYear)
        {
            var days = DaysInFiscalYear(fiscalYear);
            return days / 7;
        }
    }
}
