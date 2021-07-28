using System;

namespace RetailCalendar
{
    public class FiscalSeason : IEquatable<FiscalSeason>
    {
        public FiscalSeason()
        {

        }

        public FiscalSeason(int fiscalYear, Season season)
        {
            var details = FiscalSeasonExtension.FiscalSeasonDetails(fiscalYear, season);
            Year = details.Year;
            Season = details.Season;
            StartDate = details.StartDate;
            EndDate = details.EndDate;
            NumberOfDays = details.NumberOfDays;
        }

        public FiscalSeason(int fiscalYear, Season season, DateTime startDate, DateTime endDate, int daysInSeason)
        {
            Year = fiscalYear;
            Season = season;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = daysInSeason;
        }
        public int Year { get; set; }
        public Season Season { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }

        public bool Equals(FiscalSeason other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Year == other.Year 
                   && Season == other.Season 
                   && StartDate.Equals(other.StartDate) 
                   && EndDate.Equals(other.EndDate) 
                   && NumberOfDays == other.NumberOfDays;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FiscalSeason) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Year;
                hashCode = (hashCode * 397) ^ (int) Season;
                hashCode = (hashCode * 397) ^ StartDate.GetHashCode();
                hashCode = (hashCode * 397) ^ EndDate.GetHashCode();
                hashCode = (hashCode * 397) ^ NumberOfDays;
                return hashCode;
            }
        }
    }
}