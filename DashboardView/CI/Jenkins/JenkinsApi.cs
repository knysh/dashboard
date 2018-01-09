using System.Collections.Generic;
using Newtonsoft.Json;
using DashboardView.CI.CIFactory;
using DashboardView.Utils;
using DashboardView.CI.CIModels;
using Utils;
using System;
using System.Linq;

namespace DashboardView.CI.Jenkins
{
    internal class JenkinsApi : CIApi
    {
        private static readonly string JenkinsUrl = ConfigReader.GetCiUrl();
        private static readonly string JenkinsUser = ConfigReader.GetUserName();
        private static readonly string JenkinsPassword = ConfigReader.GetUserPassword();

        public override List<Build> GetAllBuilds()
        {
            var builds = HttpUtil.GetRequest($"{JenkinsUrl}/api/json?tree=jobs[name, color]", JenkinsUser,
                JenkinsPassword);
            var jenkinsModelBuilds = JsonConvert.DeserializeObject<JenkinsListOfBuildsModel>(builds);
            var allJenkinsBuilds = new List<Build>();
            foreach (var job in jenkinsModelBuilds.Jobs)
            {
                var build = GetBuildInfo(job.Name);
                allJenkinsBuilds.Add(build);
            }
            return allJenkinsBuilds;
        }

        public override Build GetBuildInfo(string buildName)
        {
            try
            {
                var response = HttpUtil.GetRequest($"{JenkinsUrl}/job/{buildName}/lastBuild/api/json", JenkinsUser,
                JenkinsPassword);
                var jenkinsBuildModel = JsonConvert.DeserializeObject<JenkinsBuildModel>(response);
                var build = new Build
                {
                    Name = buildName,
                    BuildRuns = GetBuildRuns(buildName),
                    Duration = jenkinsBuildModel.Duration,
                    Result = jenkinsBuildModel.Result ?? "Unknown",
                    Url = jenkinsBuildModel.Url
                };
                return build;
            }
            catch (Exception e)
            {
                return new Build()
                {
                    Name = buildName,
                    BuildRuns = new List<BuildRun>(),
                    Duration = 0,
                    Result = e.Message,
                    Url = e.Message
                };
            }
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
    }
}