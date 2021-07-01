using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace RetailCalendar
{
    public static class FiscalSeasonExtension
    {
        public static DateTime StartOfSeason(this DateTime date)
        {
            var startOfYear = date.StartOfYear();
            var startOfSecondSeason = startOfYear.AddDays(26 * 7); // 26 weeks in the first half of the year
            if (date < startOfSecondSeason)
                return startOfYear.Date;
            return startOfSecondSeason.Date;
        }

        public static DateTime StartOfSeason(int year, short season)
        {
            if (season != 1 && season != 2)
                throw new ArgumentException("May only contain 1 or 2", nameof(season));
            throw new NotImplementedException();
        }

        public static DateTime EndOfSeason(this DateTime date)
        {
            var startOfYear = date.StartOfYear();
            var endOfYear = date.EndOfYear();
            var endOfFirstSeason = startOfYear.AddDays((26 * 7) - 1); // 26 weeks in the first half of the year, minus a day. 
            if (date > endOfFirstSeason)
                return endOfYear.Date;
            return endOfFirstSeason.Date;
        }

        public static DateTime EndOfSeason(int year, short season)
        {
            if (season != 1 && season != 2)
                throw new ArgumentException("May only contain 1 or 2", nameof(season));
            throw new NotImplementedException();
        }

        public static int FiscalSeason(this DateTime date)
        {
            var startOfYear = date.StartOfYear();
            var startOfFiscalSeason = date.StartOfSeason();

            return startOfFiscalSeason == startOfYear ? 1 : 2;
        }

        public static List<(int, DateTime, DateTime)> SeasonsByYear(int year)
        {
            throw new NotImplementedException();
        }
    }
}