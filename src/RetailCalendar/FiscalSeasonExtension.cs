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
        public static FiscalSeason FiscalSeasonDetails(this DateTime date)
        {
            var startOfSeason = date.StartOfFiscalSeason();
            var endOfSeason = date.EndOfFiscalSeason();
            var fiscalYear = date.FiscalYear();
            var fiscalSeason = date.FiscalSeason();
            var daysInSeason = DateTimeExtension.DaysBetweenDates(startOfSeason, endOfSeason);
            return new FiscalSeason(fiscalYear, fiscalSeason, startOfSeason, endOfSeason, daysInSeason);
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
        public static FiscalSeason FiscalSeasonDetails(int fiscalYear, Season fiscalSeason)
        {
            var startOfSeason = StartOfFiscalSeason(fiscalYear, fiscalSeason);
            var endOfSeason = EndOfFiscalSeason(fiscalYear, fiscalSeason);

            var daysInSeason = DateTimeExtension.DaysBetweenDates(startOfSeason, endOfSeason);
            return new FiscalSeason(fiscalYear, fiscalSeason, startOfSeason, endOfSeason, daysInSeason);
        }

        public static DateTime StartOfFiscalSeason(this DateTime date)
        {
            var startOfYear = date.StartOfFiscalYear();
            var startOfSecondSeason = startOfYear.AddDays(26 * 7); // 26 weeks in the first half of the year
            
            return date < startOfSecondSeason ? startOfYear.Date : startOfSecondSeason.Date;
        }

        public static DateTime StartOfFiscalSeason(int year, Season season)
        {
            var startOfYear = FiscalYearExtension.StartOfFiscalYear(year);

            switch (season)
            {
                case Season.Spring:
                    return startOfYear;
                case Season.Fall:
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

        public static DateTime EndOfFiscalSeason(int year, Season season)
        {
            switch (season)
            {
                case Season.Spring:
                    var startOfYear = FiscalYearExtension.StartOfFiscalYear(year);
                    return startOfYear.AddDays((26 * 7) - 1); // 26 weeks in the first half of the year, minus a day. 
                case Season.Fall:
                    return FiscalYearExtension.EndOfFiscalYear(year);
                default:
                    throw new ArgumentException("May only contain 1 or 2", nameof(season));
            }
        }

        public static Season FiscalSeason(this DateTime date)
        {
            var startOfYear = date.StartOfFiscalYear();
            var startOfFiscalSeason = date.StartOfFiscalSeason();

            return startOfFiscalSeason == startOfYear ? Season.Spring : Season.Fall;
        }

        public static List<FiscalSeason> FiscalSeasonsByYear(this FiscalYear year)
        {
            return FiscalSeasonsByYear(year.Year);
        }

        public static List<FiscalSeason> FiscalSeasonsByYear(int year)
        {
            var seasonStart = StartOfFiscalSeason(year, Season.Spring);
            var endSeason = EndOfFiscalSeason(year, Season.Spring);
            var daysInSeason = DaysInSeason(seasonStart, endSeason);
            var season1 = new FiscalSeason(year, Season.Spring, seasonStart, endSeason, daysInSeason);

            seasonStart = StartOfFiscalSeason(year, Season.Fall);
            endSeason = EndOfFiscalSeason(year, Season.Fall);
            daysInSeason = DaysInSeason(seasonStart, endSeason);
            var season2 = new FiscalSeason(year, Season.Fall, seasonStart, endSeason, daysInSeason);

            return new List<FiscalSeason> {season1, season2};
        }

        public static int DaysInSeason(this DateTime date)
        {
            var startOfSeason = date.StartOfFiscalSeason();
            var endOfSeason = date.EndOfFiscalSeason();
            return DaysInSeason(startOfSeason, endOfSeason);
        }

        public static int DaysInSeason(int year, Season season)
        {
            var startOfSeason = StartOfFiscalSeason(year, season);
            var endOfSeason = EndOfFiscalSeason(year, season);
            return DaysInSeason(startOfSeason, endOfSeason);
        }

        private static int DaysInSeason(DateTime startOfSeason, DateTime endOfSeason) =>
            DateTimeExtension.DaysBetweenDates(startOfSeason, endOfSeason);
    }
}