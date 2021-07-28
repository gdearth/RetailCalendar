using System;

namespace RetailCalendar
{
    public class FiscalMonth : IEquatable<FiscalMonth>
    {

        public FiscalMonth()
        {
        }


        public FiscalMonth(int fiscalYear, short fiscalMonth)
        {
            var fiscalMonthDetails = FiscalMonthExtension.FiscalMonthDetails(fiscalYear, fiscalMonth);
            Year = fiscalMonthDetails.Year;
            Quarter = fiscalMonthDetails.Quarter;
            Month = fiscalMonthDetails.Month;
            StartDate = fiscalMonthDetails.StartDate;
            EndDate = fiscalMonthDetails.EndDate;
            NumberOfDays = fiscalMonthDetails.NumberOfDays;
        }

        public FiscalMonth(int year, short quarter, short month, DateTime startDate, DateTime endDate, int numberOfDays)
        {
            Year = year;
            Quarter = quarter;
            Month = month;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = numberOfDays;
        }

        public int Year { get; set; }
        public short Quarter { get; set; }
        public short Month { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }

        public bool Equals(FiscalMonth other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Year == other.Year
                   && Quarter == other.Quarter 
                   && Month == other.Month 
                   && StartDate.Equals(other.StartDate) 
                   && EndDate.Equals(other.EndDate) 
                   && NumberOfDays == other.NumberOfDays;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FiscalMonth) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Year;
                hashCode = (hashCode * 397) ^ Quarter.GetHashCode();
                hashCode = (hashCode * 397) ^ Month.GetHashCode();
                hashCode = (hashCode * 397) ^ StartDate.GetHashCode();
                hashCode = (hashCode * 397) ^ EndDate.GetHashCode();
                hashCode = (hashCode * 397) ^ NumberOfDays;
                return hashCode;
            }
        }
    }
}