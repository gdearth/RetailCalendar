using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xunit;

namespace RetailCalendar.Tests
{
    public class FiscalYearTests
    {

        [Theory]
        [MemberData(nameof(FiscalYearDetailById))]
        public void FiscalYearDetailTheoryById(DateTime date, (int, DateTime, DateTime, int) expectedDetails)
        {
            var actualDetails = date.FiscalYearDetails();
            Assert.Equal(expectedDetails, actualDetails);
        }

        public static IEnumerable<object[]> FiscalYearDetailById =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 2, 2),  (2020, new DateTime(2020, 2, 2), new DateTime(2021, 1, 30), 364) },
                new object[] { new DateTime(2020, 2, 3), (2020, new DateTime(2020, 2, 2), new DateTime(2021, 1, 30), 364) },
                new object[] { new DateTime(2020, 12, 31), (2020, new DateTime(2020, 2, 2),  new DateTime(2021, 1, 30), 364) },
                new object[] { new DateTime(2021, 1, 1), (2020, new DateTime(2020, 2, 2), new DateTime(2021, 1, 30), 364) },
                new object[] { new DateTime(2021, 1, 30), (2020, new DateTime(2020, 2, 2), new DateTime(2021, 1, 30), 364) },

                new object[] { new DateTime(2023, 1, 28), (2022, new DateTime(2022, 1, 30),  new DateTime(2023, 1, 28), 364)},

                new object[] { new DateTime(2023, 1, 29), (2023, new DateTime(2023, 1, 29),  new DateTime(2024, 2, 3), 371) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), (2023, new DateTime(2023, 1, 29), new DateTime(2024, 2, 3), 371) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), (2023, new DateTime(2023, 1, 29), new DateTime(2024, 2, 3), 371) }, // 53 Week year
                new object[] { new DateTime(2023, 12, 31), (2023, new DateTime(2023, 1, 29), new DateTime(2024, 2, 3), 371) }, // 53 Week year
                new object[] { new DateTime(2024, 1, 31), (2023, new DateTime(2023, 1, 29), new DateTime(2024, 2, 3), 371) }, // 53 Week year
                new object[] { new DateTime(2024, 2, 3), (2023, new DateTime(2023, 1, 29), new DateTime(2024, 2, 3), 371) }, // 53 Week year

                new object[] { new DateTime(2024, 2, 4), (2024, new DateTime(2024, 2, 4), new DateTime(2025, 2, 1), 364) }
            };

        [Theory]
        [MemberData(nameof(FiscalYearIdStartDate))]
        public void FiscalYearStartTheoryById(int fiscalYear, DateTime expectedStartDate)
        {
            var actualStartOfYear = FiscalYearExtension.StartOfYear(fiscalYear);
            Assert.Equal(expectedStartDate, actualStartOfYear);
        }

        public static IEnumerable<object[]> FiscalYearIdStartDate =>
            new List<object[]>
            {
                new object[] { 2020, new DateTime(2020, 2, 2) },
                new object[] { 2021, new DateTime(2021, 1, 31)  },
                new object[] { 2022, new DateTime(2022, 1, 30) },
                new object[] { 2023, new DateTime(2023, 1, 29) }, // 53 Week year
                new object[] { 2024, new DateTime(2024, 2, 4) }
            };
        
        [Theory]
        [MemberData(nameof(FiscalYearDateStartDate))]
        public void FiscalYearStartTheoryByDate(DateTime date, DateTime expectedStartDate)
        {
            var actualStartOfYear = date.StartOfYear();
            Assert.Equal(expectedStartDate, actualStartOfYear);
        }

        public static IEnumerable<object[]> FiscalYearDateStartDate =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 2, 2), new DateTime(2020, 2, 2) },
                new object[] { new DateTime(2020, 2, 3), new DateTime(2020, 2, 2) },
                new object[] { new DateTime(2020, 12, 31), new DateTime(2020, 2, 2) },
                new object[] { new DateTime(2021, 1, 1), new DateTime(2020, 2, 2) },
                new object[] { new DateTime(2021, 1, 30), new DateTime(2020, 2, 2) },
                
                new object[] { new DateTime(2023, 1, 28), new DateTime(2022, 1, 30) },

                new object[] { new DateTime(2023, 1, 29), new DateTime(2023, 1, 29) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), new DateTime(2023, 1, 29) }, // 53 Week year
                new object[] { new DateTime(2023, 12, 31), new DateTime(2023, 1, 29) }, // 53 Week year
                new object[] { new DateTime(2024, 1, 31), new DateTime(2023, 1, 29) }, // 53 Week year
                new object[] { new DateTime(2024, 2, 3), new DateTime(2023, 1, 29) }, // 53 Week year

                new object[] { new DateTime(2024, 2, 4), new DateTime(2024, 2, 4) }
            };

        [Theory]
        [MemberData(nameof(FiscalYearIdEndDate))]
        public void FiscalYearEndTheoryById(int fiscalYear, DateTime expectedStartDate)
        {
            var actualStartOfYear = FiscalYearExtension.EndOfYear(fiscalYear);
            Assert.Equal(expectedStartDate, actualStartOfYear);
        }

        public static IEnumerable<object[]> FiscalYearIdEndDate =>
            new List<object[]>
            {
                new object[] { 2020, new DateTime(2021, 1, 30)  },
                new object[] { 2021, new DateTime(2022, 1, 29) },
                new object[] { 2022, new DateTime(2023, 1, 28) }, 
                new object[] { 2023, new DateTime(2024, 2, 3) }, // 53 Week year
                new object[] { 2024, new DateTime(2025, 2, 1) }
            };

        [Theory]
        [MemberData(nameof(FiscalYearDateEndDate))]
        public void FiscalYearEndTheoryByDate(DateTime date, DateTime expectedStartDate)
        {
            var actualStartOfYear = date.EndOfYear();
            Assert.Equal(expectedStartDate, actualStartOfYear);
        }

        public static IEnumerable<object[]> FiscalYearDateEndDate =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 2, 2), new DateTime(2021, 1, 30) },
                new object[] { new DateTime(2020, 2, 3), new DateTime(2021, 1, 30) },
                new object[] { new DateTime(2020, 12, 31), new DateTime(2021, 1, 30) },
                new object[] { new DateTime(2021, 1, 1), new DateTime(2021, 1, 30) },
                new object[] { new DateTime(2021, 1, 30), new DateTime(2021, 1, 30) },

                new object[] { new DateTime(2023, 1, 28), new DateTime(2023, 1, 28) },

                new object[] { new DateTime(2023, 1, 29), new DateTime(2024, 2, 3) }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), new DateTime(2024, 2, 3) }, // 53 Week year
                new object[] { new DateTime(2023, 12, 31), new DateTime(2024, 2, 3) }, // 53 Week year
                new object[] { new DateTime(2024, 1, 31), new DateTime(2024, 2, 3) }, // 53 Week year
                new object[] { new DateTime(2024, 2, 3), new DateTime(2024, 2, 3) }, // 53 Week year

                new object[] { new DateTime(2024, 2, 4), new DateTime(2025, 2, 1) }
            };

        [Theory]
        [MemberData(nameof(DaysInFiscalYearById))]
        public void DaysInFiscalYearByIdTheory(int fiscalYear, int expectedDaysInYear)
        {
            var actualDaysInYear = FiscalYearExtension.DaysInYear(fiscalYear);
            Assert.Equal(expectedDaysInYear, actualDaysInYear);
        }

        public static IEnumerable<object[]> DaysInFiscalYearById =>
            new List<object[]>
            {
                new object[] { 2020, 364 },
                new object[] { 2021, 364 },
                new object[] { 2022, 364 },
                new object[] { 2023, 371 }, // 53 Week year
                new object[] { 2024, 364 }
            };

        [Theory]
        [MemberData(nameof(DaysInFiscalYearByDate))]
        public void DaysInFiscalYearByDateTheory(DateTime date, int expectedNumberOfDays)
        {
            var actualNumberOfDays = date.DaysInYear();
            Assert.Equal(expectedNumberOfDays, actualNumberOfDays);
        }

        public static IEnumerable<object[]> DaysInFiscalYearByDate =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 2, 2), 364 },
                new object[] { new DateTime(2020, 2, 3), 364 },
                new object[] { new DateTime(2020, 12, 31), 364 },
                new object[] { new DateTime(2021, 1, 1), 364 },
                new object[] { new DateTime(2021, 1, 30),364 },

                new object[] { new DateTime(2023, 1, 28), 364},

                new object[] { new DateTime(2023, 1, 29), 371 }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), 371 }, // 53 Week year
                new object[] { new DateTime(2023, 12, 31), 371 }, // 53 Week year
                new object[] { new DateTime(2024, 1, 31), 371 }, // 53 Week year
                new object[] { new DateTime(2024, 2, 3), 371 }, // 53 Week year

                new object[] { new DateTime(2024, 2, 4), 364 }
            };

        [Theory]
        [MemberData(nameof(WeeksInFiscalYearById))]
        public void WeeksInFiscalYearByIdTheory(int fiscalYear, int expectedDaysInYear)
        {
            var actualDaysInYear = FiscalYearExtension.WeeksInYear(fiscalYear);
            Assert.Equal(expectedDaysInYear, actualDaysInYear);
        }

        public static IEnumerable<object[]> WeeksInFiscalYearById =>
            new List<object[]>
            {
                new object[] { 2020, 52 },
                new object[] { 2021, 52 },
                new object[] { 2022, 52 },
                new object[] { 2023, 53 }, // 53 Week year
                new object[] { 2024, 52 }
            };

        [Theory]
        [MemberData(nameof(WeeksInFiscalYearByDate))]
        public void WeeksInFiscalYearByDateTheory(DateTime date, int expectedNumberOfDays)
        {
            var actualNumberOfDays = date.WeeksInYear();
            Assert.Equal(expectedNumberOfDays, actualNumberOfDays);
        }

        public static IEnumerable<object[]> WeeksInFiscalYearByDate =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 2, 2), 52 },
                new object[] { new DateTime(2020, 2, 3), 52 },
                new object[] { new DateTime(2020, 12, 31), 52 },
                new object[] { new DateTime(2021, 1, 1), 52 },
                new object[] { new DateTime(2021, 1, 30),52 },

                new object[] { new DateTime(2023, 1, 28), 52},

                new object[] { new DateTime(2023, 1, 29), 53 }, // 53 Week year
                new object[] { new DateTime(2023, 2, 1), 53 }, // 53 Week year
                new object[] { new DateTime(2023, 12, 31), 53 }, // 53 Week year
                new object[] { new DateTime(2024, 1, 31), 53 }, // 53 Week year
                new object[] { new DateTime(2024, 2, 3), 53 }, // 53 Week year

                new object[] { new DateTime(2024, 2, 4), 52 }
            };
    }
}