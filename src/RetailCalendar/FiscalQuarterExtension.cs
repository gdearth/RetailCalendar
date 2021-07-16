using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RetailCalendar
{
    public static class FiscalQuarterExtension
    {
        public static (int, short, short, DateTime, DateTime, int) FiscalQuarterDetails(int fiscalYear, short fiscalQuarter)
        {
            var quarters = GetFiscalQuartersByFiscalYear(fiscalYear);

            return quarters.Single(x => x.Item3 == fiscalQuarter);
        }

        public static (int, short, short, DateTime, DateTime, int) FiscalQuarterDetails(this DateTime date)
        {
            var fiscalYear = date.FiscalYear();
            var quarters = GetFiscalQuartersByFiscalYear(fiscalYear);

            return quarters.Single(x => date >= x.Item4 && date <= x.Item5);
        }

        public static DateTime StartOfFiscalQuarter(this DateTime date) => date.FiscalQuarterDetails().Item4;

        public static DateTime StartOfFiscalQuarter(int fiscalYear, short fiscalQuarter) => FiscalQuarterDetails(fiscalYear, fiscalQuarter).Item4;

        public static DateTime EndOfFiscalQuarter(this DateTime date) => date.FiscalQuarterDetails().Item5;

        public static DateTime EndOfFiscalQuarter(int fiscalYear, short fiscalQuarter) => FiscalQuarterDetails(fiscalYear, fiscalQuarter).Item5;

        public static int DaysInFiscalQuarter(this DateTime date) => date.FiscalQuarterDetails().Item6;

        public static int DaysInFiscalQuarter(int fiscalYear, short fiscalQuarter) => FiscalQuarterDetails(fiscalYear, fiscalQuarter).Item6;

        public static IList<(int, short, short, DateTime, DateTime, int)> GetFiscalQuartersByFiscalSeason(int fiscalYear, short fiscalSeason)
        {
            var allQuarters = GetFiscalQuartersByFiscalYear(fiscalYear);
            return allQuarters.Where(x => x.Item2 == fiscalSeason).ToList();
        }

        public static IList<(int, short, short, DateTime, DateTime, int)> GetFiscalQuartersByFiscalYear(int fiscalYear)
        {
            var fiscalYearDetails = FiscalYearExtension.FiscalYearDetails(fiscalYear);
            var list = new List<(int, short, short, DateTime, DateTime, int)>();
            const int weeksInStandardQuarter = 13; // Retail months are 4-5-4 

            // Quarter 1 starts at the beginning of the fiscal year
            var quarterStart = fiscalYearDetails.Item2;

            for (short quarter = 1; quarter <= 4; quarter++)
            {
                var nextQuarterStart = quarterStart.AddWeeks(weeksInStandardQuarter);
                var quarterEnd = quarter == 4 ? fiscalYearDetails.Item3 : nextQuarterStart.AddDays(-1);
                var daysInQuarter = DateTimeExtension.DaysBetweenDates(quarterStart, quarterEnd);

                list.Add((fiscalYear, GetSeasonByQuarter(quarter), quarter, quarterStart, nextQuarterStart.AddDays(-1),
                    daysInQuarter));

                quarterStart = nextQuarterStart;
            }

            return list;
        }

        private static short GetSeasonByQuarter(short fiscalQuarter)
        {
            switch (fiscalQuarter)
            {
                case 1:
                case 2:
                    return 1;
                case 3:
                case 4:
                    return 2; 
                default:
                    throw new ArgumentException("May only contain 1 through 4", nameof(fiscalQuarter));
            }
        }
    }
}