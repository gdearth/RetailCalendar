using System;
using System.Collections.Generic;
using Xunit;

namespace RetailCalendar.Tests
{
    public class FiscalSeasonTests
    {
        [Theory]
        [MemberData(nameof(FiscalSeasonDetailByDate))]
        public void FiscalSeasonDetailTheoryByDate(DateTime date, (int,short, DateTime, DateTime, int) expectedDetails)
        {
            var actualDetails = date.FiscalSeasonDetails();
            Assert.Equal(expectedDetails, actualDetails);
        }

        public static IEnumerable<object[]> FiscalSeasonDetailByDate =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 2, 2),  (2020, (short)1, new DateTime(2020, 2, 2), new DateTime(2020, 8, 1), 182) },
                new object[] { new DateTime(2020, 2, 3), (2020, (short)1, new DateTime(2020, 2, 2), new DateTime(2020, 8, 1), 182) },
                new object[] { new DateTime(2020, 12, 31), (2020, (short)2, new DateTime(2020, 8, 2),  new DateTime(2021, 1, 30), 182) },
                new object[] { new DateTime(2021, 1, 1), (2020, (short)2, new DateTime(2020, 8, 2), new DateTime(2021, 1, 30), 182) },
                new object[] { new DateTime(2021, 1, 30), (2020, (short)2, new DateTime(2020, 8, 2), new DateTime(2021, 1, 30), 182) },

                new object[] { new DateTime(2023, 1, 28), (2022, (short)2, new DateTime(2022, 7, 31),  new DateTime(2023, 1, 28), 182) },

                new object[] { new DateTime(2023, 1, 29), (2023, (short)1, new DateTime(2023, 1, 29),  new DateTime(2023, 7, 29), 182) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), (2023, (short)1, new DateTime(2023, 1, 29), new DateTime(2023, 7, 29), 182) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), (2023, (short)1, new DateTime(2023, 1, 29), new DateTime(2023, 7, 29), 182) }, // 53 Week year
                new object[] { new DateTime(2023, 12, 31), (2023, (short)2, new DateTime(2023, 7, 30), new DateTime(2024, 2, 3), 189) }, // 53 Week year
                new object[] { new DateTime(2024, 1, 31), (2023, (short)2, new DateTime(2023, 7, 30), new DateTime(2024, 2, 3), 189) }, // 53 Week year
                new object[] { new DateTime(2024, 2, 3), (2023, (short)2, new DateTime(2023, 7, 30), new DateTime(2024, 2, 3), 189) }, // 53 Week year

                new object[] { new DateTime(2024, 2, 4), (2024, (short)1, new DateTime(2024, 2, 4), new DateTime(2024, 8, 3), 182) }
            };

        [Theory]
        [MemberData(nameof(FiscalSeasonDetailById))]
        public void FiscalSeasonDetailTheoryById(int fiscalYear, short fiscalSeason, (int, short, DateTime, DateTime, int) expectedDetails)
        {
            var actualDetails = FiscalSeasonExtension.FiscalSeasonDetails(fiscalYear, fiscalSeason);
            Assert.Equal(expectedDetails, actualDetails);
        }

        public static IEnumerable<object[]> FiscalSeasonDetailById =>
            new List<object[]>
            {
                new object[] { 2020, (short)1,  (2020, (short)1, new DateTime(2020, 2, 2), new DateTime(2020, 8, 1), 182) },
                new object[] { 2020, (short)2, (2020, (short)2, new DateTime(2020, 8, 2),  new DateTime(2021, 1, 30), 182) },

                new object[] { 2022, (short)2, (2022, (short)2, new DateTime(2022, 7, 31),  new DateTime(2023, 1, 28), 182) },

                new object[] { 2023, (short)1, (2023, (short)1, new DateTime(2023, 1, 29),  new DateTime(2023, 7, 29), 182) }, // 53 Week year
                new object[] { 2023, (short)2, (2023, (short)2, new DateTime(2023, 7, 30), new DateTime(2024, 2, 3), 189) }, // 53 Week year

                new object[] { 2024, (short)1, (2024, (short)1, new DateTime(2024, 2, 4), new DateTime(2024, 8, 3), 182) }
            };
    }
}
