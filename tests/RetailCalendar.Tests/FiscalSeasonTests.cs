using RetailCalendar;
using System;
using System.Collections.Generic;
using Xunit;

namespace RetailCalendar.Tests
{
    public class FiscalSeasonTests
    {
        [Theory]
        [MemberData(nameof(FiscalSeasonDetailByDate))]
        public void FiscalSeasonDetailTheoryByDate(DateTime date, FiscalSeason expectedDetails)
        {
            var actualDetails = date.FiscalSeasonDetails();
            Assert.Equal(expectedDetails, actualDetails);
        }

        public static IEnumerable<object[]> FiscalSeasonDetailByDate =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 2, 2),  new FiscalSeason(2020, Season.Spring, new DateTime(2020, 2, 2), new DateTime(2020, 8, 1), 182) },
                new object[] { new DateTime(2020, 2, 3), new FiscalSeason(2020, Season.Spring, new DateTime(2020, 2, 2), new DateTime(2020, 8, 1), 182) },
                new object[] { new DateTime(2020, 12, 31), new FiscalSeason(2020, Season.Fall, new DateTime(2020, 8, 2),  new DateTime(2021, 1, 30), 182) },
                new object[] { new DateTime(2021, 1, 1), new FiscalSeason(2020, Season.Fall, new DateTime(2020, 8, 2), new DateTime(2021, 1, 30), 182) },
                new object[] { new DateTime(2021, 1, 30), new FiscalSeason(2020, Season.Fall, new DateTime(2020, 8, 2), new DateTime(2021, 1, 30), 182) },

                new object[] { new DateTime(2023, 1, 28), new FiscalSeason(2022, Season.Fall, new DateTime(2022, 7, 31),  new DateTime(2023, 1, 28), 182) },

                new object[] { new DateTime(2023, 1, 29), new FiscalSeason(2023, Season.Spring, new DateTime(2023, 1, 29),  new DateTime(2023, 7, 29), 182) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), new FiscalSeason(2023, Season.Spring, new DateTime(2023, 1, 29), new DateTime(2023, 7, 29), 182) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), new FiscalSeason(2023, Season.Spring, new DateTime(2023, 1, 29), new DateTime(2023, 7, 29), 182) }, // 53 Week year
                new object[] { new DateTime(2023, 12, 31), new FiscalSeason(2023, Season.Fall, new DateTime(2023, 7, 30), new DateTime(2024, 2, 3), 189) }, // 53 Week year
                new object[] { new DateTime(2024, 1, 31), new FiscalSeason(2023, Season.Fall, new DateTime(2023, 7, 30), new DateTime(2024, 2, 3), 189) }, // 53 Week year
                new object[] { new DateTime(2024, 2, 3), new FiscalSeason(2023, Season.Fall, new DateTime(2023, 7, 30), new DateTime(2024, 2, 3), 189) }, // 53 Week year

                new object[] { new DateTime(2024, 2, 4), new FiscalSeason(2024, Season.Spring, new DateTime(2024, 2, 4), new DateTime(2024, 8, 3), 182) }
            };

        [Theory]
        [MemberData(nameof(FiscalSeasonDetailById))]
        public void FiscalSeasonDetailTheoryById(int fiscalYear, Season fiscalSeason, FiscalSeason expectedDetails)
        {
            var actualDetails = FiscalSeasonExtension.FiscalSeasonDetails(fiscalYear, fiscalSeason);
            Assert.Equal(expectedDetails, actualDetails);
        }

        public static IEnumerable<object[]> FiscalSeasonDetailById =>
            new List<object[]>
            {
                new object[] { 2020, Season.Spring, new FiscalSeason(2020, Season.Spring, new DateTime(2020, 2, 2), new DateTime(2020, 8, 1), 182) },
                new object[] { 2020, Season.Fall, new FiscalSeason(2020, Season.Fall, new DateTime(2020, 8, 2),  new DateTime(2021, 1, 30), 182) },

                new object[] { 2022, Season.Fall, new FiscalSeason(2022, Season.Fall, new DateTime(2022, 7, 31),  new DateTime(2023, 1, 28), 182) },

                new object[] { 2023, Season.Spring, new FiscalSeason(2023, Season.Spring, new DateTime(2023, 1, 29),  new DateTime(2023, 7, 29), 182) }, // 53 Week year
                new object[] { 2023, Season.Fall, new FiscalSeason(2023, Season.Fall, new DateTime(2023, 7, 30), new DateTime(2024, 2, 3), 189) }, // 53 Week year

                new object[] { 2024, Season.Spring, new FiscalSeason(2024, Season.Spring, new DateTime(2024, 2, 4), new DateTime(2024, 8, 3), 182) }
            };

        [Theory]
        [MemberData(nameof(FiscalSeasonDetailById))]
        public void DaysInSeasonTheoryByYearSeason(int fiscalYear, Season fiscalSeason, FiscalSeason expectedDetails)
        {
            var actual = FiscalSeasonExtension.DaysInSeason(fiscalYear, fiscalSeason);
            Assert.Equal(expectedDetails.NumberOfDays, actual);
        }

        [Theory]
        [MemberData(nameof(FiscalSeasonDetailByDate))]
        public void DaysInSeasonTheoryByDate(DateTime date, FiscalSeason expectedDetails)
        {
            var actual = date.DaysInSeason();
            Assert.Equal(expectedDetails.NumberOfDays, actual);
        }
    }
}
