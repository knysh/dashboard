using DashboardView.CI.CIFactory;
using System.Configuration;

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
            return GetProperty("ciUserName");
        }

        public static string GetUserPassword()
        {
            return GetProperty("ciUserPassword");
        }

        public static string GetCiUrl()
        {
            return GetProperty("ciUrl");
        }

        public static CITypes GetCiType()
        {
            switch (GetProperty("ciType"))
            {
                case "Jenkins": return CITypes.Jenkins;
                default: return CITypes.Unknown;
            }
        }

        public static string GetConfigFileName()
        {
            return GetProperty("configFileName");
        }
    }
}