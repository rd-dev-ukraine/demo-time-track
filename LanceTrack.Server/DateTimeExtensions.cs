using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;

namespace LanceTrack.Server
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime date)
        {
            var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            if (date.DayOfWeek == firstDayOfWeek)
                return date;

            return date.Past().First(d => d.DayOfWeek == firstDayOfWeek);
        }

        public static DateTime EndOfWeek(this DateTime date)
        {
            return date.StartOfWeek().AddDays(6);
        }

        public static IEnumerable<DateTime> Future(this DateTime date)
        {
            while (date < SqlDateTime.MaxValue.Value)
            {
                date = date.AddDays(1);
                yield return date;
            }
        }

        public static IEnumerable<DateTime> Past(this DateTime date)
        {
            while (date > SqlDateTime.MinValue.Value)
            {
                date = date.AddDays(-1);
                yield return date;
            }
        }
    }
}