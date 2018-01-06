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
    }
}