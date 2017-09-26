using DashboardView.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

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