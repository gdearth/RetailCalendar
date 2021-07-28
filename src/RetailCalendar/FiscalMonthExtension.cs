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
            return fiscalMonthDetails.StartDate;
        }

        public static DateTime StartOfFiscalMonth(int fiscalYear, short fiscalMonth)
        {
            var fiscalMonthDetails = FiscalMonthDetails(fiscalYear, fiscalMonth);
            return fiscalMonthDetails.StartDate;
        }

        public static DateTime EndOfFiscalMonth(this DateTime date)
        {
            var fiscalMonthDetails = date.FiscalMonthDetails();
            return fiscalMonthDetails.EndDate;
        }

        public static DateTime EndOfFiscalMonth(int fiscalYear, short fiscalMonth)
        {
            var fiscalMonthDetails = FiscalMonthDetails(fiscalYear, fiscalMonth);
            return fiscalMonthDetails.EndDate;
        }

        public static int DaysInFiscalMonth(this DateTime date)
        {
            var fiscalMonthDetails = date.FiscalMonthDetails();
            return fiscalMonthDetails.NumberOfDays;
        }

        public static int DaysInFiscalMonth(int fiscalYear, short fiscalMonth)
        {
            var fiscalMonthDetails = FiscalMonthDetails(fiscalYear, fiscalMonth);
            return fiscalMonthDetails.NumberOfDays;
        }

        public static short FiscalMonth(this DateTime date)
        {
            var fiscalMonthDetails = date.FiscalMonthDetails();
            return fiscalMonthDetails.Month;
        }

        public static FiscalMonth FiscalMonthDetails(this DateTime date)
        {
            var fiscalYear = date.FiscalYear();
            var fiscalMonths = GetFiscalMonthsByFiscalYear(fiscalYear);
            return fiscalMonths.Single(x => x.StartDate <= date && x.EndDate >= date);
        }

        public static FiscalMonth FiscalMonthDetails(int fiscalYear, short fiscalMonth)
        {
            var fiscalMonths = GetFiscalMonthsByFiscalYear(fiscalYear);
            return fiscalMonths.Single(x => x.Month == fiscalMonth);
        }

        public static IList<FiscalMonth> GetFiscalMonthsByFiscalYear(int fiscalYear)
        {
            var fiscalYearDetail = FiscalYearExtension.FiscalYearDetails(fiscalYear);

            var fiscalMonthStart = fiscalYearDetail.StartDate;

            var list = new List<FiscalMonth>();

            for (short month = 1; month <= 12; month++)
            {
                var weeksInMonth = WeeksInFiscalMonth(month);

                var startOfNextFiscalMonth = fiscalMonthStart.AddWeeks(weeksInMonth);
                var endOfFiscalMonth = month == 12 ? fiscalYearDetail.EndDate : startOfNextFiscalMonth.AddDays(-1);
                var numberOfDays = DateTimeExtension.DaysBetweenDates(fiscalMonthStart, endOfFiscalMonth);
                var fiscalQuarter = fiscalMonthStart.FiscalQuarter();

                list.Add(new FiscalMonth(fiscalYear, fiscalQuarter, month, fiscalMonthStart, endOfFiscalMonth, numberOfDays));

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