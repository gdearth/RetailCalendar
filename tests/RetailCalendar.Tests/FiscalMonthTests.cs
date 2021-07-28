using RetailCalendar;
using System;
using System.Collections.Generic;
using Xunit;

namespace RetailCalendar.Tests
{
    public class FiscalMonthTests
    {
        [Theory]
        [MemberData(nameof(FiscalMonthDetailByDate))]
        public void FiscalMonthDetailTheoryByDate(DateTime date, FiscalMonth expectedDetails)
        {
            var actualDetails = date.FiscalMonthDetails();
            Assert.Equal(expectedDetails, actualDetails);
        }

        public static IEnumerable<object[]> FiscalMonthDetailByDate =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 2, 2),  new FiscalMonth(2020, 1, 1, new DateTime(2020, 2, 2), new DateTime(2020, 2, 29), 28) },
                new object[] { new DateTime(2020, 2, 3), new FiscalMonth(2020, 1, 1, new DateTime(2020, 2, 2), new DateTime(2020, 2, 29), 28) },
                new object[] { new DateTime(2020, 12, 31), new FiscalMonth(2020, 4, 11, new DateTime(2020, 11, 29),  new DateTime(2021, 1, 2), 35) },
                new object[] { new DateTime(2021, 1, 1), new FiscalMonth(2020, 4, 11, new DateTime(2020, 11, 29), new DateTime(2021, 1, 2), 35) },
                new object[] { new DateTime(2021, 1, 30), new FiscalMonth(2020, 4, 12, new DateTime(2021, 1, 3), new DateTime(2021, 1, 30), 28) },

                new object[] { new DateTime(2023, 1, 28), new FiscalMonth(2022, 4, 12, new DateTime(2023, 1, 1),  new DateTime(2023, 1, 28), 28) },

                new object[] { new DateTime(2023, 1, 29), new FiscalMonth(2023, 1, 1, new DateTime(2023, 1, 29),  new DateTime(2023, 2, 25), 28) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), new FiscalMonth(2023, 1, 1, new DateTime(2023, 1, 29), new DateTime(2023, 2, 25), 28) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), new FiscalMonth(2023, 1, 1, new DateTime(2023, 1, 29), new DateTime(2023, 2, 25), 28) }, // 53 Week year
                new object[] { new DateTime(2023, 12, 31), new FiscalMonth(2023, 4, 12, new DateTime(2023, 12, 31), new DateTime(2024, 2, 3), 35) }, // 53 Week year
                new object[] { new DateTime(2024, 1, 31), new FiscalMonth(2023, 4, 12, new DateTime(2023, 12, 31), new DateTime(2024, 2, 3), 35) }, // 53 Week year
                new object[] { new DateTime(2024, 2, 3), new FiscalMonth(2023, 4, 12, new DateTime(2023, 12, 31), new DateTime(2024, 2, 3), 35) }, // 53 Week year

                new object[] { new DateTime(2024, 2, 4), new FiscalMonth(2024, 1, 1, new DateTime(2024, 2, 4), new DateTime(2024, 3, 2), 28) }
            };

        [Theory]
        [MemberData(nameof(FiscalMonthDetail))]
        public void FiscalMonthDetailTheoryByYearQuarter(FiscalMonth expectedDetails)
        {
            var actualDetails = new FiscalMonth(expectedDetails.Year, expectedDetails.Month);
            Assert.Equal(expectedDetails, actualDetails);
        }

        [Theory]
        [MemberData(nameof(FiscalMonthDetail))]
        public void FiscalMonthDetailTheoryByConstructor(FiscalMonth expectedDetails)
        {
            var actualDetails = new FiscalMonth(expectedDetails.Year, expectedDetails.Quarter, expectedDetails.Month, expectedDetails.StartDate, expectedDetails.EndDate, expectedDetails.NumberOfDays);
            Assert.Equal(expectedDetails, actualDetails);
        }

        public static IEnumerable<object[]> FiscalMonthDetail =>
            new List<object[]>
            {
                new object[] { new FiscalMonth(2020, 1, 1, new DateTime(2020, 2, 2), new DateTime(2020, 2, 29), 28) },
                new object[] { new FiscalMonth(2020, 1, 1, new DateTime(2020, 2, 2), new DateTime(2020, 2, 29), 28) },
                new object[] { new FiscalMonth(2020, 4, 11, new DateTime(2020, 11, 29), new DateTime(2021, 1, 2), 35) },
                new object[] { new FiscalMonth(2020, 4, 11, new DateTime(2020, 11, 29), new DateTime(2021, 1, 2), 35)},
                new object[] { new FiscalMonth(2020, 4, 11, new DateTime(2020, 11, 29), new DateTime(2021, 1, 2), 35)},

                new object[] { new FiscalMonth(2022, 4, 12, new DateTime(2023, 1, 1),  new DateTime(2023, 1, 28), 28) },

                new object[] { new FiscalMonth(2023, 1, 1, new DateTime(2023, 1, 29),  new DateTime(2023, 2, 25), 28) }, // 53 Week year
                new object[] { new FiscalMonth(2023, 1, 1, new DateTime(2023, 1, 29),  new DateTime(2023, 2, 25), 28) }, // 53 Week year
                new object[] { new FiscalMonth(2023, 1, 1, new DateTime(2023, 1, 29),  new DateTime(2023, 2, 25), 28) }, // 53 Week year
                new object[] { new FiscalMonth(2023, 4, 12, new DateTime(2023, 12, 31), new DateTime(2024, 2, 3), 35) }, // 53 Week year
                new object[] { new FiscalMonth(2023, 4, 12, new DateTime(2023, 12, 31), new DateTime(2024, 2, 3), 35) }, // 53 Week year
                new object[] { new FiscalMonth(2023, 4, 12, new DateTime(2023, 12, 31), new DateTime(2024, 2, 3), 35) }, // 53 Week year
                                                         
                new object[] { new FiscalMonth(2024, 1, 1, new DateTime(2024, 2, 4), new DateTime(2024, 3, 2), 28) }
            };

        [Theory]
        [MemberData(nameof(FiscalMonthDetail))]
        public void StartOfFiscalMonthTheoryByDate(FiscalMonth expectedDetails)
        {
            var actualDetails = expectedDetails.EndDate.StartOfFiscalMonth();
            Assert.Equal(expectedDetails.StartDate, actualDetails);
        }

        [Theory]
        [MemberData(nameof(FiscalMonthDetail))]
        public void StartOfFiscalMonthTheoryByYearMonth(FiscalMonth expectedDetails)
        {
            var actualDetails = FiscalMonthExtension.StartOfFiscalMonth(expectedDetails.Year, expectedDetails.Month);
            Assert.Equal(expectedDetails.StartDate, actualDetails);
        }

        [Theory]
        [MemberData(nameof(FiscalMonthDetail))]
        public void EndOfFiscalMonthTheoryByDate(FiscalMonth expectedDetails)
        {
            var actualDetails = expectedDetails.StartDate.EndOfFiscalMonth();
            Assert.Equal(expectedDetails.EndDate, actualDetails);
        }

        [Theory]
        [MemberData(nameof(FiscalMonthDetail))]
        public void EndOfFiscalMonthTheoryByYearMonth(FiscalMonth expectedDetails)
        {
            var actualDetails = FiscalMonthExtension.EndOfFiscalMonth(expectedDetails.Year, expectedDetails.Month);
            Assert.Equal(expectedDetails.EndDate, actualDetails);
        }

        [Theory]
        [MemberData(nameof(FiscalMonthDetail))]
        public void DaysInFiscalMonthTheoryByDate(FiscalMonth expectedDetails)
        {
            var actualDetails = expectedDetails.EndDate.DaysInFiscalMonth();
            Assert.Equal(expectedDetails.NumberOfDays, actualDetails);
        }

        [Theory]
        [MemberData(nameof(FiscalMonthDetail))]
        public void DaysInFiscalMonthTheoryByYearMonth(FiscalMonth expectedDetails)
        {
            var actualDetails = FiscalMonthExtension.DaysInFiscalMonth(expectedDetails.Year, expectedDetails.Month);
            Assert.Equal(expectedDetails.NumberOfDays, actualDetails);
        }
    }
}