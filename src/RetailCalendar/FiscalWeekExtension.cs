using System;
using System.Collections;
using System.Collections.Generic;

namespace RetailCalendar
{
    public static class FiscalWeekExtension
    {
        public static FiscalWeek FiscalWeekDetails(this DateTime date)
        {
            var startOfWeek = date.StartOfFiscalWeek();
            var endOfWeek = date.EndOfFiscalWeek();
            var fiscalWeek = date.FiscalWeek();
            var fiscalYear = date.FiscalYear();
            var fiscalMonth = date.FiscalMonth();
            return new FiscalWeek(fiscalYear, fiscalMonth, fiscalWeek, startOfWeek, endOfWeek);
        }

        public static FiscalWeek FiscalWeekDetails(int fiscalYear, short fiscalWeek)
        {
            var startOfYear = FiscalYearExtension.StartOfFiscalYear(fiscalYear);
            var startOfWeek = StartOfFiscalWeek(startOfYear, fiscalWeek);
            var endOfWeek = EndOfFiscalWeek(startOfWeek);
            var fiscalMonth = startOfWeek.FiscalMonth();

            return new FiscalWeek(fiscalYear, fiscalMonth, fiscalWeek, startOfWeek, endOfWeek);
        }

        public static DateTime EndOfFiscalWeek(int fiscalYear, short fiscalWeek)
        {
            var startOfWeek = StartOfFiscalWeek(fiscalYear, fiscalWeek);
            return EndOfFiscalWeek(startOfWeek);
        }

        public static DateTime StartOfFiscalWeek(int fiscalYear, short fiscalWeek)
        {
            var startOfYear = FiscalYearExtension.StartOfFiscalYear(fiscalYear);
            return StartOfFiscalWeek(startOfYear, fiscalWeek);
        }

        private static DateTime StartOfFiscalWeek(DateTime startOfFiscalYear, short fiscalWeek) => 
            startOfFiscalYear.AddWeeks(fiscalWeek - 1);

        public static DateTime StartOfFiscalWeek(this DateTime date) =>
            date.PreviousOrEqualByDayOfWeek(DayOfWeek.Sunday);

        public static DateTime EndOfFiscalWeek(this DateTime date) => 
            date.NextOrEqualByDayOfWeek(DayOfWeek.Saturday);

        public static short FiscalWeek(this DateTime date) => 
            (short) (DateTimeExtension.DaysBetweenDates(date.StartOfFiscalYear(), date.StartOfFiscalWeek()).DaysToWeeks() + 1);

        public static IList<FiscalWeek> FiscalWeeksByMonth(this FiscalMonth fiscalMonth)
        {
            var weeksInMonth = fiscalMonth.NumberOfDays.DaysToWeeks();
            var weekStartDate = fiscalMonth.StartDate;
            var fiscalWeeks = new List<FiscalWeek>();

            while (weekStartDate < fiscalMonth.EndDate)
            {
                fiscalWeeks.Add(weekStartDate.FiscalWeekDetails());
                weekStartDate.AddWeeks(1);
            }

            return fiscalWeeks;
        }

        public static IList<FiscalWeek> FiscalWeeksByMonth(int fiscalYear, short fiscalMonth)
        {
            var month = FiscalMonthExtension.FiscalMonthDetails(fiscalYear, fiscalMonth);
            return month.FiscalWeeksByMonth();
        }
    }
}