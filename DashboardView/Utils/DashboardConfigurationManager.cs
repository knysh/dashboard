using DashboardView.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DashboardView.Utils
{
    public class DashboardConfigurationManager
    {
        public static Groups GetGroups()
        {
            var configAsJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DashboardViewConfig.json"));
            var groups = JsonConvert.DeserializeObject<Groups>(configAsJson);
            return groups;
            
        }

        public static void UpdateGroups(Groups groups)
        {
            var modifiedJson = JsonConvert.SerializeObject(groups);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DashboardViewConfig.json"), modifiedJson);
        }
    }
}