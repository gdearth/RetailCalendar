using System;

namespace RetailCalendar
{
    public class FiscalYear : IEquatable<FiscalYear>
    {
        public FiscalYear()
        {

        }

        public FiscalYear(int year)
        {
            var details = FiscalYearExtension.FiscalYearDetails(year);
            Year = details.Year;
            StartDate = details.StartDate;
            EndDate = details.EndDate;
            NumberOfDays = details.NumberOfDays;
        }

        public FiscalYear(int year, DateTime startDate, DateTime endDate, int daysInYear)
        {
            Year = year;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = daysInYear;
        }

        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }

        public bool Equals(FiscalYear other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Year == other.Year 
                   && StartDate.Equals(other.StartDate) 
                   && EndDate.Equals(other.EndDate) 
                   && NumberOfDays == other.NumberOfDays;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FiscalYear) obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Year;
                hashCode = (hashCode * 397) ^ StartDate.GetHashCode();
                hashCode = (hashCode * 397) ^ EndDate.GetHashCode();
                hashCode = (hashCode * 397) ^ NumberOfDays;
                return hashCode;
            }
        }
    }
}