using System.Collections.Generic;
using Newtonsoft.Json;
using DashboardView.CI.CIFactory;
using DashboardView.Utils;
using DashboardView.CI.CIModels;
using Utils;
using System.Linq;
using System.Text.RegularExpressions;

namespace DashboardView.CI.Jenkins
{
    internal class JenkinsApi : CIApi
    {
        private const string NodeNamePattern = @"Running on\s+(.+)\s+in";
        private static readonly string JenkinsUrl = ConfigReader.GetCiUrl();
        private static readonly string JenkinsUser = ConfigReader.GetUserName();
        private static readonly string JenkinsPassword = ConfigReader.GetUserPassword();

        public override List<Build> GetAllBuilds()
        {
            var builds = HttpUtil.GetRequest($"{JenkinsUrl}/api/json?tree=jobs[name]", JenkinsUser, JenkinsPassword);
            var jenkinsModelBuilds = JsonConvert.DeserializeObject<JenkinsListOfBuildsModel>(builds);
            return jenkinsModelBuilds.Jobs.Select(job => new Build {Name = job.Name}).ToList();
        }

        public override List<BuildRun> GetBuildRuns(string buildName)
        {
            var response = HttpUtil.GetRequest(url: $"{JenkinsUrl}/job/{buildName}/api/json?tree=builds[number,timestamp,id,result,url,duration]",user: JenkinsUser, password: JenkinsPassword);
            var listOfBuilds = JsonConvert.DeserializeObject<JenkinsListOfBuildRuns>(response);
            var builds = listOfBuilds.Builds.Select(build => new BuildRun
            {
                Id = build.Id,
                Result = build.Result ?? "Unknown",
                StartDateTime = TimeUtil.GetDateTimeFromTimestamp(build.Timestamp),
                Duration = build.Duration,
                Url = build.Url
            }).ToList();
            return builds;
        }

        public override string GetBuildRunLog(string buildName, int buildNumber)
        {
            return HttpUtil.GetRequest($"{JenkinsUrl}/job/{buildName}/{buildNumber}/consoleText", JenkinsUser, JenkinsPassword);
        }

        public override string GetBuildRunNode(string buildName, int buildNumber)
        {
            return Regex.Match(GetBuildRunLog(buildName, buildNumber), NodeNamePattern).Groups[1].ToString();
        }
    }    
}