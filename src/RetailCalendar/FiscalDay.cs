using System;

namespace RetailCalendar
{
    public class FiscalDay : IEquatable<FiscalDay>
    {
        public FiscalDay()
        {

        }

        public FiscalDay(int fiscalYear, short fiscalDay)
        {
            var details = FiscalDayExtension.FiscalDayDetails(fiscalYear, fiscalDay);
            Year = details.Year;
            Week = details.Week;
            Day = details.Day;
            Date = details.Date;
        }

        public FiscalDay(int fiscalYear, short fiscalWeek, short fiscalDay, DateTime fiscalDate)
        {
            Year = fiscalYear;
            Week = fiscalWeek;
            Day = fiscalDay;
            Date = fiscalDate;
        }

        public int Year { get; set; }
        public short Week { get; set; }
        public short Day { get; set; }
        public DateTime Date { get; set; }

        public bool Equals(FiscalDay other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Year == other.Year 
                   && Week == other.Week 
                   && Day == other.Day 
                   && Date.Equals(other.Date);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FiscalDay) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Year;
                hashCode = (hashCode * 397) ^ Week.GetHashCode();
                hashCode = (hashCode * 397) ^ Day.GetHashCode();
                hashCode = (hashCode * 397) ^ Date.GetHashCode();
                return hashCode;
            }
        }
    }
}