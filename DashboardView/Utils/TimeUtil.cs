using System;

namespace DashboardView.Utils
{
    public class TimeUtil
    {
        public static string GetStringFormatFromSeconds(int sec)
        {
            var time = TimeSpan.FromSeconds(sec);
            return GetStringFormatFromSeconds(time);
        }

        public static string GetStringFormatFromMillis(int millis)
        {
            var time = TimeSpan.FromMilliseconds(millis);
            return GetStringFormatFromSeconds(time);
        }

        public static string GetStringFormatFromSeconds(TimeSpan timespan)
        {
            return $"{timespan.Hours}h : {timespan.Minutes}m : {timespan.Seconds}s";
        }

        public static DateTime GetDateTimeFromTimestamp(double unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dtDateTime.AddMilliseconds(unixTimeStamp).ToUniversalTime();
        }
    }
}