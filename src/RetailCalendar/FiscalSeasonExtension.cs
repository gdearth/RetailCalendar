using System;
using System.Collections.Generic;

namespace RetailCalendar
{
    public static class FiscalSeasonExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns>
        ///     Item 1: Fiscal Year
        ///     Item 2: Fiscal Season
        ///     Item 3: Start Of Season,
        ///     Item 4: End Of Season,
        ///     Item 5: Days in Season
        /// </returns>
        public static (int, short, DateTime, DateTime, int) FiscalSeasonDetails(this DateTime date)
        {
            var startOfSeason = date.StartOfFiscalSeason();
            var endOfSeason = date.EndOfFiscalSeason();
            var fiscalYear = date.FiscalYear();
            var fiscalSeason = date.FiscalSeason();
            var daysInSeason = DateTimeExtension.DaysBetweenDates(startOfSeason, endOfSeason);
            return (fiscalYear, fiscalSeason, startOfSeason, endOfSeason, daysInSeason);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <param name="fiscalSeason"></param>
        /// <returns>
        ///     Item 1: Fiscal Year
        ///     Item 2: Fiscal Season
        ///     Item 3: Start Of Season,
        ///     Item 4: End Of Season,
        ///     Item 5: Days in Season
        /// </returns>
        public static (int, short, DateTime, DateTime, int) FiscalSeasonDetails(int fiscalYear, short fiscalSeason)
        {
            var startOfSeason = StartOfFiscalSeason(fiscalYear, fiscalSeason);
            var endOfSeason = EndOfFiscalSeason(fiscalYear, fiscalSeason);

            var daysInSeason = DateTimeExtension.DaysBetweenDates(startOfSeason, endOfSeason);
            return (fiscalYear, fiscalSeason, startOfSeason, endOfSeason, daysInSeason);
        }

        public static DateTime StartOfFiscalSeason(this DateTime date)
        {
            var startOfYear = date.StartOfFiscalYear();
            var startOfSecondSeason = startOfYear.AddDays(26 * 7); // 26 weeks in the first half of the year
            
            return date < startOfSecondSeason ? startOfYear.Date : startOfSecondSeason.Date;
        }

        public static DateTime StartOfFiscalSeason(int year, short season)
        {
            if (season != 1 && season != 2)
                throw new ArgumentException("May only contain 1 or 2", nameof(season));
            
            var startOfYear = FiscalYearExtension.StartOfFiscalYear(year);

            switch (season)
            {
                case 1:
                    return startOfYear;
                case 2:
                    return startOfYear.AddDays(26 * 7); // 26 weeks in the first half of the year
                default:
                    throw new ArgumentException("May only contain 1 or 2", nameof(season));
            }
        }

        public static DateTime EndOfFiscalSeason(this DateTime date)
        {
            var startOfYear = date.StartOfFiscalYear();
            var endOfYear = date.EndOfFiscalYear();
            var endOfFirstSeason = startOfYear.AddDays((26 * 7) - 1); // 26 weeks in the first half of the year, minus a day. 
            if (date > endOfFirstSeason)
                return endOfYear.Date;
            return endOfFirstSeason.Date;
        }

        public static DateTime EndOfFiscalSeason(int year, short season)
        {
            if (season != 1 && season != 2)
                throw new ArgumentException("May only contain 1 or 2", nameof(season));

            switch (season)
            {
                case 1:
                    var startOfYear = FiscalYearExtension.StartOfFiscalYear(year);
                    return startOfYear.AddDays((26 * 7) - 1); // 26 weeks in the first half of the year, minus a day. 
                case 2:
                    return FiscalYearExtension.EndOfFiscalYear(year);
                default:
                    throw new ArgumentException("May only contain 1 or 2", nameof(season));
            }
        }

        public static short FiscalSeason(this DateTime date)
        {
            var startOfYear = date.StartOfFiscalYear();
            var startOfFiscalSeason = date.StartOfFiscalSeason();

            return (short) (startOfFiscalSeason == startOfYear ? 1 : 2);
        }

        public static List<(int, DateTime, DateTime)> SeasonsByYear(int year)
        {
            var seasonStart = StartOfFiscalSeason(year, 1);
            var endSeason = EndOfFiscalSeason(year, 1);
            var season1 = (1, seasonStart, endStart: endSeason);

            seasonStart = StartOfFiscalSeason(year, 2);
            endSeason = EndOfFiscalSeason(year, 2);
            var season2 = (2, seasonStart, endStart: endSeason);

            return new List<(int, DateTime, DateTime)> {season1, season2};
        }

        public static int DaysInSeason(this DateTime date)
        {
            var startOfSeason = date.StartOfFiscalSeason();
            var endOfSeason = date.EndOfFiscalSeason();
            return DateTimeExtension.DaysBetweenDates(startOfSeason, endOfSeason);
        }

        public static int DaysInSeason(int year, short season)
        {
            var startOfSeason = StartOfFiscalSeason(year, season);
            var endOfSeason = EndOfFiscalSeason(year, season);
            return DateTimeExtension.DaysBetweenDates(startOfSeason, endOfSeason);
        }
    }
}