using DashboardView.CI.CIModels;
using Newtonsoft.Json;
using System;
using System.IO;

namespace DashboardView.Utils
{
    public class DashboardConfigurationManager
    {
        public static Groups GetGroups()
        {
            var configAsJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigReader.GetConfigFileName()));
            var groups = JsonConvert.DeserializeObject<Groups>(configAsJson);
            return groups;
        }

        public static void UpdateGroups(Groups groups)
        {
            var modifiedJson = JsonConvert.SerializeObject(groups);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigReader.GetConfigFileName()), modifiedJson);
        }
    }
}