using DashboardView.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardView.CI.Jenkins
{
    public class JenkinsApi
    {
        private static readonly string JenkinsUrl = ConfigReader.GetCiUrl();
        private static readonly string JenkinsUser = ConfigReader.GetUserName();
        private static readonly string JenkinsPassword = ConfigReader.GetUserPassword();

        public static string GetAllBuilds()
        {
            var builds = HttpUtil.GetRequest($"{JenkinsUrl}/api/json?tree=jobs[name, color]", JenkinsUser, JenkinsPassword);
            return builds;
        }

        public static BuildModel GetBuildInfo(string buildName)
        {
            var response = HttpUtil.GetRequest($"{JenkinsUrl}/job/{buildName}/lastBuild/api/json", JenkinsUser, JenkinsPassword);
            return JsonConvert.DeserializeObject<BuildModel>(response);
        }
    }
}