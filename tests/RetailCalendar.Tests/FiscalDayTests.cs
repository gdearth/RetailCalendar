using RetailCalendar;
using System;
using System.Collections.Generic;
using Xunit;

namespace RetailCalendar.Tests
{
    public class FiscalDayTests
    {
        [Theory]
        [MemberData(nameof(FiscalDayDetail))]
        public void FiscalDayDetailTheoryByDate(FiscalDay expectedDetails)
        {
            var actualDetails = expectedDetails.Date.FiscalDayDetails();
            Assert.Equal(expectedDetails, actualDetails);
        }

        [Theory]
        [MemberData(nameof(FiscalDayDetail))]
        public void FiscalDayDetailTheoryByYearDay(FiscalDay expectedDetails)
        {
            var actualDetails = new FiscalDay(expectedDetails.Year, expectedDetails.Day);
            Assert.Equal(expectedDetails, actualDetails);
        }

        [Theory]
        [MemberData(nameof(FiscalDayDetail))]
        public void FiscalWeekDetailTheoryByConstructor(FiscalDay expectedDetails)
        {
            var actualDetails = new FiscalDay(expectedDetails.Year, expectedDetails.Week, expectedDetails.Day, expectedDetails.Date);
            Assert.Equal(expectedDetails, actualDetails);
        }

        public static IEnumerable<object[]> FiscalDayDetail =>
            new List<object[]>
            {
                new object[] { new FiscalDay(2020, 1, 1, new DateTime(2020, 2, 2)) },
                new object[] { new FiscalDay(2020, 1, 2, new DateTime(2020, 2, 3)) },
                new object[] { new FiscalDay(2020, 48, 330, new DateTime(2020, 12, 27)) },
                new object[] { new FiscalDay(2020, 48, 336, new DateTime(2021, 1, 2)) },
                new object[] { new FiscalDay(2020, 52, 364, new DateTime(2021, 1, 30)) },

                new object[] { new FiscalDay(2022, 52, 364, new DateTime(2023, 1, 28)) },

                new object[] { new FiscalDay(2023, 1, 1, new DateTime(2023, 1, 29)) }, // 53 Week year
                new object[] { new FiscalDay(2023, 1, 7, new DateTime(2023, 2, 4)) }, // 53 Week year
                new object[] { new FiscalDay(2023, 49, 337, new DateTime(2023, 12, 31)) }, // 53 Week year
                new object[] { new FiscalDay(2023, 53, 365, new DateTime(2024, 1, 28)) }, // 53 Week year
                new object[] { new FiscalDay(2023, 53, 371, new DateTime(2024, 2, 3)) }, // 53 Week year
                                         
                new object[] { new FiscalDay(2024, 1, 1, new DateTime(2024, 2, 4)) }
            };

        [Theory]
        [MemberData(nameof(FiscalDayDetail))]
        public void FiscalDayTest(FiscalDay expectedDetails)
        {
            var actualDay = expectedDetails.Date.FiscalDay();
            Assert.Equal(expectedDetails.Day, actualDay);
        }

        [Theory]
        [MemberData(nameof(FiscalDayDetail))]
        public void FiscalDateTest(FiscalDay expectedDetails)
        {
            var actualDay = FiscalDayExtension.FiscalDate(expectedDetails.Year, expectedDetails.Day);
            Assert.Equal(expectedDetails.Date, actualDay);
        }
    }
}