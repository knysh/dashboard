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
            using (var streamReader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DashboardViewConfig.json")))
            {
                var configAsJson = streamReader.ReadToEnd();
                var groups = JsonConvert.DeserializeObject<Groups>(configAsJson);
                return groups;
            }
        }
    }
}