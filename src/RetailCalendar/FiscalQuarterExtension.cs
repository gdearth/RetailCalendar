using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RetailCalendar
{
    public static class FiscalQuarterExtension
    {
        public static FiscalQuarter FiscalQuarterDetails(int fiscalYear, short fiscalQuarter)
        {
            var quarters = GetFiscalQuartersByFiscalYear(fiscalYear);

            return quarters.Single(x => x.Quarter == fiscalQuarter);
        }

        public static FiscalQuarter FiscalQuarterDetails(this DateTime date)
        {
            var fiscalYear = date.FiscalYear();
            var quarters = GetFiscalQuartersByFiscalYear(fiscalYear);

            return quarters.Single(x => date >= x.StartDate && date <= x.EndDate);
        }

        public static DateTime StartOfFiscalQuarter(this DateTime date) => date.FiscalQuarterDetails().StartDate;

        public static DateTime StartOfFiscalQuarter(int fiscalYear, short fiscalQuarter) => FiscalQuarterDetails(fiscalYear, fiscalQuarter).StartDate;

        public static DateTime EndOfFiscalQuarter(this DateTime date) => date.FiscalQuarterDetails().EndDate;

        public static DateTime EndOfFiscalQuarter(int fiscalYear, short fiscalQuarter) => FiscalQuarterDetails(fiscalYear, fiscalQuarter).EndDate;

        public static int DaysInFiscalQuarter(this DateTime date) => date.FiscalQuarterDetails().NumberOfDays;

        public static int DaysInFiscalQuarter(int fiscalYear, short fiscalQuarter) => FiscalQuarterDetails(fiscalYear, fiscalQuarter).NumberOfDays;

        public static short FiscalQuarter(this DateTime date) => date.FiscalQuarterDetails().Quarter;
                      
        public static IList<FiscalQuarter> GetFiscalQuartersByFiscalSeason(this FiscalSeason season )
        {
            return GetFiscalQuartersByFiscalSeason(season.Year, season.Season);
        }

        public static IList<FiscalQuarter> GetFiscalQuartersByFiscalSeason(int fiscalYear, Season fiscalSeason)
        {
            var allQuarters = GetFiscalQuartersByFiscalYear(fiscalYear);
            return allQuarters.Where(x => x.Season == fiscalSeason).ToList();
        }

        public static IList<FiscalQuarter> GetFiscalQuartersByFiscalYear(int fiscalYear)
        {
            var fiscalYearDetails = FiscalYearExtension.FiscalYearDetails(fiscalYear);
            var list = new List<FiscalQuarter>();
            const int weeksInStandardQuarter = 13; // Retail months are 4-5-4 

            // Quarter 1 starts at the beginning of the fiscal year
            var quarterStart = fiscalYearDetails.StartDate;

            for (short quarter = 1; quarter <= 4; quarter++)
            {
                var nextQuarterStart = quarterStart.AddWeeks(weeksInStandardQuarter);
                var quarterEnd = quarter == 4 ? fiscalYearDetails.EndDate : nextQuarterStart.AddDays(-1);
                var daysInQuarter = DateTimeExtension.DaysBetweenDates(quarterStart, quarterEnd);

                list.Add(new FiscalQuarter(fiscalYear, GetSeasonByQuarter(quarter), quarter, quarterStart, quarterEnd,
                    daysInQuarter));

                quarterStart = nextQuarterStart;
            }

            return list;
        }

        private static Season GetSeasonByQuarter(short fiscalQuarter)
        {
            switch (fiscalQuarter)
            {
                case 1:
                case 2:
                    return Season.Spring;
                case 3:
                case 4:
                    return Season.Fall; 
                default:
                    throw new ArgumentException("May only contain 1 through 4", nameof(fiscalQuarter));
            }
        }
    }
}