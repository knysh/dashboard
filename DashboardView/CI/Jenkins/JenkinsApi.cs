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
        private const string JenkinsUrl = "http://localhost:8080/";
        private const string JenkinsUser = "admin";
        private const string JenkinsPassword = "admin";

        public static string GetAllBuilds()
        {
            var builds = HttpUtil.GetRequest($"{JenkinsUrl}/api/json?tree=jobs[name, color]", JenkinsUser, JenkinsPassword);
            return builds;
        }

        public static string GetBuildInfo(string buildName)
        {
            return HttpUtil.GetRequest($"{JenkinsUrl}/job/{buildName}/lastBuild/api/json", JenkinsUser, JenkinsPassword);
        }

        public static string GetLatestBuildResult(string buildName)
        {
            var response = HttpUtil.GetRequest($"{JenkinsUrl}/job/{buildName}/lastBuild/api/json", JenkinsUser, JenkinsPassword);
            return JsonConvert.DeserializeObject<BuildModel>(response).Result;
        }
    }
}