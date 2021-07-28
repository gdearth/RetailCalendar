using System;

namespace RetailCalendar
{
    public class FiscalWeek : IEquatable<FiscalWeek>
    {
        public FiscalWeek()
        {

        }

        public FiscalWeek(int fiscalYear, short fiscalWeek)
        {
            var fiscalWeekDetails = FiscalWeekExtension.FiscalWeekDetails(fiscalYear, fiscalWeek);
            Year = fiscalWeekDetails.Year;
            Month = fiscalWeekDetails.Month;
            Week = fiscalWeekDetails.Week;
            StartDate = fiscalWeekDetails.StartDate;
            EndDate = fiscalWeekDetails.EndDate;
        }

        public FiscalWeek(int fiscalYear, short fiscalMonth, short fiscalWeek, DateTime startDate, DateTime endDate)
        {
            Year = fiscalYear;
            Month = fiscalMonth;
            Week = fiscalWeek;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Year { get; set; }
        public short Month { get; set; }
        public short Week { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool Equals(FiscalWeek other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Year == other.Year 
                   && Month == other.Month 
                   && Week == other.Week 
                   && StartDate.Equals(other.StartDate) 
                   && EndDate.Equals(other.EndDate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FiscalWeek) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Year;
                hashCode = (hashCode * 397) ^ Month.GetHashCode();
                hashCode = (hashCode * 397) ^ Week.GetHashCode();
                hashCode = (hashCode * 397) ^ StartDate.GetHashCode();
                hashCode = (hashCode * 397) ^ EndDate.GetHashCode();
                return hashCode;
            }
        }
    }
}