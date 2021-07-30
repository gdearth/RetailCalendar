using System;
using System.Collections;
using System.Collections.Generic;

namespace RetailCalendar
{
    public static class FiscalDayExtension
    {
        public static FiscalDay FiscalDayDetails(this DateTime date)
        {
            var fiscalYearDetails = date.FiscalYearDetails();
            var fiscalYear = fiscalYearDetails.Year;
            var fiscalWeek = date.FiscalWeek();
            var fiscalDay = (short)DateTimeExtension.DaysBetweenDates(fiscalYearDetails.StartDate.Date, date.Date);

            return new FiscalDay(fiscalYear, fiscalWeek, fiscalDay, date.Date);
        }

        public static FiscalDay FiscalDayDetails(int fiscalYear, short fiscalDay)
        {
            var fiscalYearDetails = FiscalYearExtension.FiscalYearDetails(fiscalYear);
            var fiscalDate = fiscalYearDetails.StartDate.Date.AddDays(fiscalDay - 1);
            var fiscalWeek = fiscalDate.FiscalWeek();

            return new FiscalDay(fiscalYear, fiscalWeek, fiscalDay, fiscalDate.Date);
        }

        public static int FiscalDay(this DateTime date) => 
            date.FiscalDayDetails().Day;

        public static DateTime FiscalDate(int fiscalYear, short fiscalDay) =>
            FiscalDayDetails(fiscalYear, fiscalDay).Date;

        public static IList<FiscalDay> FiscalDaysByWeek(this FiscalWeek fiscalWeek)
        {
            var fiscalDate = fiscalWeek.StartDate;
            var fiscalDays = new List<FiscalDay>();
            
            while (fiscalDate < fiscalWeek.EndDate)
            {
                fiscalDays.Add(fiscalDate.FiscalDayDetails());
                fiscalDate = fiscalDate.AddDays(1);
            }

            return fiscalDays;
        }

        public static IList<FiscalDay> FiscalDaysByWeek(int fiscalYear, short fiscalWeek)
        {
            var fiscalWeekDetail = FiscalWeekExtension.FiscalWeekDetails(fiscalYear, fiscalWeek);
            return fiscalWeekDetail.FiscalDaysByWeek();
        }
    }
}