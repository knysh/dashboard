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
    public class ConfigReader
    {
        public static string GetProperty(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetUserName()
        {
            return GetProperty("userName");
        }

        public static string GetUserPassword()
        {
            return GetProperty("userPassword");
        }

        public static string GetCiUrl()
        {
            return GetProperty("ciUrl");
        }
    }
}