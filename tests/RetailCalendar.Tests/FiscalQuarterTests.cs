using RetailCalendar;
using System;
using System.Collections.Generic;
using Xunit;

namespace RetailCalendar.Tests
{
    public class FiscalQuarterTests
    {
        [Theory]
        [MemberData(nameof(FiscalQuarterDetailByDate))]
        public void FiscalQuarterDetailTheoryByDate(DateTime date, FiscalQuarter expectedDetails)
        {
            var actualDetails = date.FiscalQuarterDetails();
            Assert.Equal(expectedDetails, actualDetails);
        }

        public static IEnumerable<object[]> FiscalQuarterDetailByDate =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 2, 2),  new FiscalQuarter(2020, Season.Spring, 1, new DateTime(2020, 2, 2), new DateTime(2020, 5, 2), 91) },
                new object[] { new DateTime(2020, 2, 3), new FiscalQuarter(2020, Season.Spring, 1, new DateTime(2020, 2, 2), new DateTime(2020, 5, 2), 91) },
                new object[] { new DateTime(2020, 12, 31), new FiscalQuarter(2020, Season.Fall, 4, new DateTime(2020, 11, 1),  new DateTime(2021, 1, 30), 91) },
                new object[] { new DateTime(2021, 1, 1), new FiscalQuarter(2020, Season.Fall, 4, new DateTime(2020, 11, 1), new DateTime(2021, 1, 30), 91) },
                new object[] { new DateTime(2021, 1, 30), new FiscalQuarter(2020, Season.Fall, 4, new DateTime(2020, 11, 1), new DateTime(2021, 1, 30), 91) },

                new object[] { new DateTime(2023, 1, 28), new FiscalQuarter(2022, Season.Fall, 4, new DateTime(2022, 10, 30),  new DateTime(2023, 1, 28), 91) },

                new object[] { new DateTime(2023, 1, 29), new FiscalQuarter(2023, Season.Spring, 1, new DateTime(2023, 1, 29),  new DateTime(2023, 4, 29), 91) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), new FiscalQuarter(2023, Season.Spring, 1, new DateTime(2023, 1, 29), new DateTime(2023, 4, 29), 91) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), new FiscalQuarter(2023, Season.Spring, 1, new DateTime(2023, 1, 29), new DateTime(2023, 4, 29), 91) }, // 53 Week year
                new object[] { new DateTime(2023, 12, 31), new FiscalQuarter(2023, Season.Fall, 4, new DateTime(2023, 10, 29), new DateTime(2024, 2, 3), 98) }, // 53 Week year
                new object[] { new DateTime(2024, 1, 31), new FiscalQuarter(2023, Season.Fall, 4, new DateTime(2023, 10, 29), new DateTime(2024, 2, 3), 98) }, // 53 Week year
                new object[] { new DateTime(2024, 2, 3), new FiscalQuarter(2023, Season.Fall, 4, new DateTime(2023, 10, 29), new DateTime(2024, 2, 3), 98) }, // 53 Week year

                new object[] { new DateTime(2024, 2, 4), new FiscalQuarter(2024, Season.Spring, 1, new DateTime(2024, 2, 4), new DateTime(2024, 5, 4), 91) }
            };

        [Theory]
        [MemberData(nameof(FiscalQuarterDetail))]
        public void FiscalQuarterDetailTheoryByYearQuarter(FiscalQuarter expectedDetails)
        {
            var actualDetails = new FiscalQuarter(expectedDetails.Year, expectedDetails.Quarter);
            Assert.Equal(expectedDetails, actualDetails);
        }

        [Theory]
        [MemberData(nameof(FiscalQuarterDetail))]
        public void FiscalQuarterDetailTheoryByConstructor(FiscalQuarter expectedDetails)
        {
            var actualDetails = new FiscalQuarter(expectedDetails.Year, expectedDetails.Season, expectedDetails.Quarter, expectedDetails.StartDate, expectedDetails.EndDate, expectedDetails.NumberOfDays);
            Assert.Equal(expectedDetails, actualDetails);
        }

        public static IEnumerable<object[]> FiscalQuarterDetail =>
            new List<object[]>
            {
                new object[] { new FiscalQuarter(2020, Season.Spring, 1, new DateTime(2020, 2, 2), new DateTime(2020, 5, 2), 91) },
                new object[] { new FiscalQuarter(2020, Season.Spring, 1, new DateTime(2020, 2, 2), new DateTime(2020, 5, 2), 91) },
                new object[] { new FiscalQuarter(2020, Season.Fall, 4, new DateTime(2020, 11, 1),  new DateTime(2021, 1, 30), 91) },
                new object[] { new FiscalQuarter(2020, Season.Fall, 4, new DateTime(2020, 11, 1), new DateTime(2021, 1, 30), 91) },
                new object[] { new FiscalQuarter(2020, Season.Fall, 4, new DateTime(2020, 11, 1), new DateTime(2021, 1, 30), 91) },

                new object[] { new FiscalQuarter(2022, Season.Fall, 4, new DateTime(2022, 10, 30),  new DateTime(2023, 1, 28), 91) },

                new object[] { new FiscalQuarter(2023, Season.Spring, 1, new DateTime(2023, 1, 29),  new DateTime(2023, 4, 29), 91) }, // 53 Week year
                new object[] { new FiscalQuarter(2023, Season.Spring, 1, new DateTime(2023, 1, 29), new DateTime(2023, 4, 29), 91) }, // 53 Week year
                new object[] { new FiscalQuarter(2023, Season.Spring, 1, new DateTime(2023, 1, 29), new DateTime(2023, 4, 29), 91) }, // 53 Week year
                new object[] { new FiscalQuarter(2023, Season.Fall, 4, new DateTime(2023, 10, 29), new DateTime(2024, 2, 3), 98) }, // 53 Week year
                new object[] { new FiscalQuarter(2023, Season.Fall, 4, new DateTime(2023, 10, 29), new DateTime(2024, 2, 3), 98) }, // 53 Week year
                new object[] { new FiscalQuarter(2023, Season.Fall, 4, new DateTime(2023, 10, 29), new DateTime(2024, 2, 3), 98) }, // 53 Week year

                new object[] { new FiscalQuarter(2024, Season.Spring, 1, new DateTime(2024, 2, 4), new DateTime(2024, 5, 4), 91) }
            };

        [Theory]
        [MemberData(nameof(FiscalQuarterDetail))]
        public void StartOfFiscalQuarterTheoryByYearQuarter(FiscalQuarter expectedDetails)
        {
            var actual = FiscalQuarterExtension.StartOfFiscalQuarter(expectedDetails.Year, expectedDetails.Quarter);
            Assert.Equal(expectedDetails.StartDate, actual);
        }

        [Theory]
        [MemberData(nameof(FiscalQuarterDetail))]
        public void StartOfFiscalQuarterTheoryByDate(FiscalQuarter expectedDetails)
        {
            var actual = expectedDetails.EndDate.StartOfFiscalQuarter();
            Assert.Equal(expectedDetails.StartDate, actual);
        }

        [Theory]
        [MemberData(nameof(FiscalQuarterDetail))]
        public void EndOfFiscalQuarterTheoryByYearQuarter(FiscalQuarter expectedDetails)
        {
            var actual = FiscalQuarterExtension.EndOfFiscalQuarter(expectedDetails.Year, expectedDetails.Quarter);
            Assert.Equal(expectedDetails.EndDate, actual);
        }

        [Theory]
        [MemberData(nameof(FiscalQuarterDetail))]
        public void EndOfFiscalQuarterTheoryByDate(FiscalQuarter expectedDetails)
        {
            var actual = expectedDetails.StartDate.EndOfFiscalQuarter();
            Assert.Equal(expectedDetails.EndDate, actual);
        }

        [Theory]
        [MemberData(nameof(FiscalQuarterDetail))]
        public void DaysInFiscalQuarterTheoryByYearQuarter(FiscalQuarter expectedDetails)
        {
            var actual = FiscalQuarterExtension.DaysInFiscalQuarter(expectedDetails.Year, expectedDetails.Quarter);
            Assert.Equal(expectedDetails.NumberOfDays, actual);
        }

        [Theory]
        [MemberData(nameof(FiscalQuarterDetail))]
        public void DaysInFiscalQuarterTheoryByDate(FiscalQuarter expectedDetails)
        {
            var actual = expectedDetails.StartDate.DaysInFiscalQuarter();
            Assert.Equal(expectedDetails.NumberOfDays, actual);
        }
    }
}