using System.Collections.Generic;
using Newtonsoft.Json;
using DashboardView.CI.CIFactory;
using DashboardView.Utils;
using DashboardView.CI.CIModels;
using Utils;
using System;

namespace DashboardView.CI.Jenkins
{
    internal class JenkinsApi : CIApi
    {
        private static readonly string JenkinsUrl = ConfigReader.GetCiUrl();
        private static readonly string JenkinsUser = ConfigReader.GetUserName();
        private static readonly string JenkinsPassword = ConfigReader.GetUserPassword();

        public override List<Build> GetAllBuilds()
        {
            var builds = HttpUtil.GetRequest($"{JenkinsUrl}/api/json?tree=jobs[name, color]", JenkinsUser, JenkinsPassword);
            var jenkinsModelBuilds = JsonConvert.DeserializeObject<JenkinsListOfBuildsModel>(builds);
            var allJenkinsBuilds = new List<Build>();
            foreach (var job in jenkinsModelBuilds.Jobs)
            {
                allJenkinsBuilds.Add(new Build
                {
                    Name = job.Name,
                    BuildRuns = GetBuildRuns(job.Name)
                });
            }

            return allJenkinsBuilds;
        }

        public override Build GetBuildInfo(string buildName)
        {
            var response = HttpUtil.GetRequest($"{JenkinsUrl}/job/{buildName}/lastBuild/api/json", JenkinsUser, JenkinsPassword);
            var jenkinsBuildModel = JsonConvert.DeserializeObject<JenkinsBuildModel>(response);
            return new Build
            {
                Name = buildName,
                BuildRuns = GetBuildRuns(buildName),
                Duration = jenkinsBuildModel.Duration,
                Result = jenkinsBuildModel.Result,
                Url = jenkinsBuildModel.Url
            };
        }

        public override List<BuildRun> GetBuildRuns(string buildName)
        {
            var listOfBuildRuns = new List<BuildRun>();
            var response = HttpUtil.GetRequest(url: $"{JenkinsUrl}/job/{buildName}/api/json?tree=builds[number,status,timestamp,id,result]", user: JenkinsUser, password: JenkinsPassword);
            var listOfBuilds = JsonConvert.DeserializeObject<JenkinsListOfBuildRuns>(response);
            foreach (var build in listOfBuilds.Builds)
            {
                listOfBuildRuns.Add(
                    new BuildRun
                    {
                        Id = build.Id,
                        Result = build.Result,
                        StartDateTime = TimeUtil.GetDateTimeFromTimestamp(build.Timestamp),
                        Duration = TimeSpan.FromSeconds(build.Duration)
                    });
            }
            return listOfBuildRuns;
        }
    }
}