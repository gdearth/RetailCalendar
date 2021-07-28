using RetailCalendar;
using System;
using System.Collections.Generic;
using Xunit;

namespace RetailCalendar.Tests
{
    public class FiscalWeekTests
    {
        [Theory]
        [MemberData(nameof(FiscalWeekDetailByDate))]
        public void FiscalWeekDetailTheoryByDate(DateTime date, FiscalWeek expectedDetails)
        {
            var actualDetails = date.FiscalWeekDetails();
            Assert.Equal(expectedDetails, actualDetails);
        }

        public static IEnumerable<object[]> FiscalWeekDetailByDate =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 2, 2),   new FiscalWeek(2020, 1, 1, new DateTime(2020, 2, 2), new DateTime(2020, 2, 8)) },
                new object[] { new DateTime(2020, 2, 3),   new FiscalWeek(2020, 1, 1, new DateTime(2020, 2, 2), new DateTime(2020, 2, 8)) },
                new object[] { new DateTime(2020, 12, 31), new FiscalWeek(2020, 11, 48, new DateTime(2020, 12, 27),  new DateTime(2021, 1, 2)) },
                new object[] { new DateTime(2021, 1, 1),   new FiscalWeek(2020, 11, 48, new DateTime(2020, 12, 27), new DateTime(2021, 1, 2)) },
                new object[] { new DateTime(2021, 1, 30),  new FiscalWeek(2020, 12, 52, new DateTime(2021, 1, 24), new DateTime(2021, 1, 30)) },

                new object[] { new DateTime(2023, 1, 28),  new FiscalWeek(2022, 12, 52, new DateTime(2023, 1, 22),  new DateTime(2023, 1, 28)) },

                new object[] { new DateTime(2023, 1, 29),  new FiscalWeek(2023, 1, 1, new DateTime(2023, 1, 29),  new DateTime(2023, 2, 4)) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1),   new FiscalWeek(2023, 1, 1, new DateTime(2023, 1, 29), new DateTime(2023, 2, 4)) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1),   new FiscalWeek(2023, 1, 1, new DateTime(2023, 1, 29), new DateTime(2023, 2, 4)) }, // 53 Week year
                new object[] { new DateTime(2023, 12, 31), new FiscalWeek(2023, 12, 49, new DateTime(2023, 12, 31), new DateTime(2024, 1, 6)) }, // 53 Week year
                new object[] { new DateTime(2024, 1, 31),  new FiscalWeek(2023, 12, 53, new DateTime(2024, 1, 28), new DateTime(2024, 2, 3)) }, // 53 Week year
                new object[] { new DateTime(2024, 2, 3),   new FiscalWeek(2023, 12, 53, new DateTime(2024, 1, 28), new DateTime(2024, 2, 3)) }, // 53 Week year

                new object[] { new DateTime(2024, 2, 4),   new FiscalWeek(2024, 1, 1, new DateTime(2024, 2, 4), new DateTime(2024, 2, 10)) }
            };

        [Theory]
        [MemberData(nameof(FiscalWeekDetail))]
        public void FiscalWeekDetailTheoryByYearQuarter(FiscalWeek expectedDetails)
        {
            var actualDetails = new FiscalWeek(expectedDetails.Year, expectedDetails.Week);
            Assert.Equal(expectedDetails, actualDetails);
        }

        [Theory]
        [MemberData(nameof(FiscalWeekDetail))]
        public void FiscalWeekDetailTheoryByConstructor(FiscalWeek expectedDetails)
        {
            var actualDetails = new FiscalWeek(expectedDetails.Year, expectedDetails.Month, expectedDetails.Week, expectedDetails.StartDate, expectedDetails.EndDate);
            Assert.Equal(expectedDetails, actualDetails);
        }

        public static IEnumerable<object[]> FiscalWeekDetail =>
            new List<object[]>
            {
                new object[] { new FiscalWeek(2020, 1, 1, new DateTime(2020, 2, 2), new DateTime(2020, 2, 8)) },
                new object[] { new FiscalWeek(2020, 1, 1, new DateTime(2020, 2, 2), new DateTime(2020, 2, 8)) },
                new object[] { new FiscalWeek(2020, 11, 48, new DateTime(2020, 12, 27),  new DateTime(2021, 1, 2)) },
                new object[] { new FiscalWeek(2020, 11, 48, new DateTime(2020, 12, 27), new DateTime(2021, 1, 2)) },
                new object[] { new FiscalWeek(2020, 12, 52, new DateTime(2021, 1, 24), new DateTime(2021, 1, 30)) },

                new object[] { new FiscalWeek(2022, 12, 52, new DateTime(2023, 1, 22),  new DateTime(2023, 1, 28)) },

                new object[] { new FiscalWeek(2023, 1, 1, new DateTime(2023, 1, 29),  new DateTime(2023, 2, 4)) }, // 53 Week year
                new object[] { new FiscalWeek(2023, 1, 1, new DateTime(2023, 1, 29), new DateTime(2023, 2, 4)) }, // 53 Week year
                new object[] { new FiscalWeek(2023, 1, 1, new DateTime(2023, 1, 29), new DateTime(2023, 2, 4)) }, // 53 Week year
                new object[] { new FiscalWeek(2023, 12, 49, new DateTime(2023, 12, 31), new DateTime(2024, 1, 6)) }, // 53 Week year
                new object[] { new FiscalWeek(2023, 12, 53, new DateTime(2024, 1, 28), new DateTime(2024, 2, 3)) }, // 53 Week year
                new object[] { new FiscalWeek(2023, 12, 53, new DateTime(2024, 1, 28), new DateTime(2024, 2, 3)) }, // 53 Week year
                               
                new object[] { new FiscalWeek(2024, 1, 1, new DateTime(2024, 2, 4), new DateTime(2024, 2, 10)) }
            };

        [Theory]
        [MemberData(nameof(FiscalWeekDetail))]
        public void StartOfFiscalWeekTest(FiscalWeek expectedDetails)
        {
            var actual = FiscalWeekExtension.StartOfFiscalWeek(expectedDetails.Year, expectedDetails.Week);
            Assert.Equal(expectedDetails.StartDate, actual);
        }

        [Theory]
        [MemberData(nameof(FiscalWeekDetail))]
        public void EndOfFiscalWeekTest(FiscalWeek expectedDetails)
        {
            var actual = FiscalWeekExtension.EndOfFiscalWeek(expectedDetails.Year, expectedDetails.Week);
            Assert.Equal(expectedDetails.EndDate, actual);
        }
    }
}