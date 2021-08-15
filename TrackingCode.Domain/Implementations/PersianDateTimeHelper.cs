using System;
using System.Globalization;

namespace TrackingCode.Domain.Implementations
{
    public static class PersianDateTimeHelper
    {
        private static readonly PersianCalendar PersianCalendar = new PersianCalendar();
        private static readonly TimeZoneInfo IranStandardTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");

        public static string GetDateString(DateTime dt)
        {
            var year = PersianCalendar.GetYear(dt);
            var month = PersianCalendar.GetMonth(dt);
            var day = PersianCalendar.GetDayOfMonth(dt);

            return $"{year}/{month:00}/{day:00}";
        }
        
        public static string GetDateStringShamsi(this DateTime dt)
        {
            var year = PersianCalendar.GetYear(dt);
            var month = PersianCalendar.GetMonth(dt);
            var day = PersianCalendar.GetDayOfMonth(dt);

            return $"{year}/{month:00}/{day:00}";
        }

        public static int GetIntegerValue(DateTime dt)
        {
            var year = PersianCalendar.GetYear(dt);
            var month = PersianCalendar.GetMonth(dt);
            var day = PersianCalendar.GetDayOfMonth(dt);

            return int.Parse($"{year}{month:00}{day:00}");
        }

        public static DateTime? Parse(string dt)
        {
            if (string.IsNullOrEmpty(dt))
            {
                return null;
            }

            try
            {
                var parts = dt.Split('/');
                var year = int.Parse(parts[0]);
                var month = int.Parse(parts[1]);
                var day = int.Parse(parts[2]);

                return new DateTime(year, month, day, PersianCalendar);
            }

            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime ConvertIranStandardTimeToUtc(DateTime dt)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dt, IranStandardTimeZone);
        }
    }
}