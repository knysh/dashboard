using System;

namespace DashboardView.Utils
{
    public class TimeUtil
    {
        public static string GetStringFormatFromSeconds(int seconds)
        {
            var time = TimeSpan.FromSeconds(seconds);
            return $"{time.Hours}h : {time.Minutes}m : {time.Seconds}s";
        }

        public static DateTime GetDateTimeFromTimestamp(double unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}