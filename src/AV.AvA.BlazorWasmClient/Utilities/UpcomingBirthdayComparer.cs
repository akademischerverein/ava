using NodaTime;
using NodaTime.Extensions;

namespace AV.AvA.BlazorWasmClient.Utilities
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0011:Add braces", Justification = "Readability")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1503:Braces should not be omitted", Justification = "Readability")]
    internal class UpcomingBirthdayComparer : IComparer<LocalDate?>, IComparer<LocalDate>
    {
        public LocalDate TodayReferenceDate { get; set; } = SystemClock.Instance.InBclSystemDefaultZone().GetCurrentDate();

        public int Compare(LocalDate? x, LocalDate? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;
            return Compare(x.Value, y.Value);
        }

        public int Compare(LocalDate x, LocalDate y)
        {
            if (x.Day == y.Day && x.Month == y.Month) return x.Year - y.Year;

            var today = TodayReferenceDate;
            if (IsUpcomingThisYear(x, today))
            {
                if (!IsUpcomingThisYear(y, today)) return -1;

                // so both are upcoming this year
                return JustCompareDate(x, y);
            }

            // so x is not upcoming this year
            if (IsUpcomingThisYear(y, today)) return 1;

            // so both are not upcoming this year
            return JustCompareDate(x, y);
        }

        // We can not use DayOfYear here due to leap years.
        private static int JustCompareDate(LocalDate x, LocalDate y)
        {
            if (x.Month < y.Month) return -1;
            if (x.Month > y.Month) return 1;

            // so month is same
            if (x.Day < y.Day) return -1;
            if (x.Day > y.Day) return 1;

            // so days are same, this should be unreachable when called from Compare()
            return 0;
        }

        // We can not use DayOfYear here due to leap years.
        private static bool IsUpcomingThisYear(LocalDate value, LocalDate today, bool valueIfToday = true)
        {
            if (value.Month > today.Month) return true;
            if (value.Month < today.Month) return false;

            // so month is equal
            if (value.Day > today.Day) return true;
            if (value.Day < today.Day) return false;

            // so it is today
            return valueIfToday;
        }
    }
}
