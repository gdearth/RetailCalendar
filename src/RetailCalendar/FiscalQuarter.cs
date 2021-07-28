using System;

namespace RetailCalendar
{
    public class FiscalQuarter : IEquatable<FiscalQuarter>
    {
        public FiscalQuarter()
        {

        }

        public FiscalQuarter(int fiscalYear, short fiscalQuarter)
        {
            var fiscalQuarterDetails = FiscalQuarterExtension.FiscalQuarterDetails(fiscalYear, fiscalQuarter);
            Year = fiscalQuarterDetails.Year;
            Season = fiscalQuarterDetails.Season;
            Quarter = fiscalQuarterDetails.Quarter;
            StartDate = fiscalQuarterDetails.StartDate;
            EndDate = fiscalQuarterDetails.EndDate;
            NumberOfDays = fiscalQuarterDetails.NumberOfDays;
        }

        public FiscalQuarter(int year, Season season, short quarter, DateTime startDate, DateTime endDate, int days)
        {
            Year = year;
            Season = season;
            Quarter = quarter;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = days;
        }

        public int Year { get; set; }
        public Season Season { get; set; }
        public short Quarter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }

        public bool Equals(FiscalQuarter other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Year == other.Year 
                   && Season == other.Season 
                   && Quarter == other.Quarter 
                   && StartDate.Equals(other.StartDate) 
                   && EndDate.Equals(other.EndDate) 
                   && NumberOfDays == other.NumberOfDays;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FiscalQuarter) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Year;
                hashCode = (hashCode * 397) ^ (int) Season;
                hashCode = (hashCode * 397) ^ Quarter.GetHashCode();
                hashCode = (hashCode * 397) ^ StartDate.GetHashCode();
                hashCode = (hashCode * 397) ^ EndDate.GetHashCode();
                hashCode = (hashCode * 397) ^ NumberOfDays;
                return hashCode;
            }
        }
    }
}