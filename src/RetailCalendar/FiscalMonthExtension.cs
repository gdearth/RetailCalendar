using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RetailCalendar
{
    public static class FiscalMonthExtension
    {
        public static DateTime StartOfFiscalMonth(this DateTime date)
        {
            var fiscalMonthDetails = date.FiscalMonthDetails();
            return fiscalMonthDetails.Item5;
        }

        public static DateTime StartOfFiscalMonth(int fiscalYear, short fiscalMonth)
        {
            var fiscalMonthDetails = FiscalMonthDetails(fiscalYear, fiscalMonth);
            return fiscalMonthDetails.Item5;
        }

        public static DateTime EndOfFiscalMonth(this DateTime date)
        {
            var fiscalMonthDetails = date.FiscalMonthDetails();
            return fiscalMonthDetails.Item6;
        }

        public static DateTime EndOfFiscalMonth(int fiscalYear, short fiscalMonth)
        {
            var fiscalMonthDetails = FiscalMonthDetails(fiscalYear, fiscalMonth);
            return fiscalMonthDetails.Item5;
        }

        public static int DaysInFiscalMonth(this DateTime date)
        {
            var fiscalMonthDetails = date.FiscalMonthDetails();
            return fiscalMonthDetails.Item7;
        }

        public static int DaysInFiscalMonth(int fiscalYear, short fiscalMonth)
        {
            var fiscalMonthDetails = FiscalMonthDetails(fiscalYear, fiscalMonth);
            return fiscalMonthDetails.Item7;
        }

        public static (int, short, short, short, DateTime, DateTime, int) FiscalMonthDetails(this DateTime date)
        {
            var fiscalYear = date.FiscalYear();
            var fiscalMonths = GetFiscalMonthsByFiscalYear(fiscalYear);
            return fiscalMonths.Single(x => x.Item5 <= date && x.Item6 >= date);
        }

        public static (int, short, short, short, DateTime, DateTime, int) FiscalMonthDetails(int fiscalYear, short fiscalMonth)
        {
            var fiscalMonths = GetFiscalMonthsByFiscalYear(fiscalYear);
            return fiscalMonths.Single(x => x.Item4 == fiscalMonth);
        }

        public static IList<(int, short, short, short, DateTime, DateTime, int)> GetFiscalMonthsByFiscalYear(int fiscalYear)
        {
            var fiscalYearDetail = FiscalYearExtension.FiscalYearDetails(fiscalYear);

            var fiscalMonthStart = fiscalYearDetail.Item2;

            var list = new List<(int, short, short, short, DateTime, DateTime, int)>();

            for (short month = 1; month <= 12; month++)
            {
                var weeksInMonth = WeeksInFiscalMonth(month);

                var startOfNextFiscalMonth = fiscalMonthStart.AddWeeks(weeksInMonth);
                var endOfFiscalMonth = startOfNextFiscalMonth.AddDays(-1);

                list.Add((fiscalYear, 1, 1, month, fiscalMonthStart, endOfFiscalMonth, 0));

                fiscalMonthStart = startOfNextFiscalMonth;
            }

            return list;
        }

        private static short WeeksInFiscalMonth(short month)
        {
            short weeksInMonth;
            switch (month)
            {
                case 2:
                case 5:
                case 8:
                case 11:
                    weeksInMonth = 5;
                    break;
                case 1:
                case 3:
                case 4:
                case 6:
                case 7:
                case 9:
                case 10:
                case 12:
                default:
                    weeksInMonth = 4;
                    break;
            }

            return weeksInMonth;
        }
    }
}