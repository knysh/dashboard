using System.Collections.Generic;
using Newtonsoft.Json;
using DashboardView.CI.CIFactory;
using DashboardView.Utils;
using DashboardView.CI.CIModels;
using Utils;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Ajax.Utilities;

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
            var jenkinsBuilds =
                HttpUtil.GetRequest(
                    $"{JenkinsUrl}/api/json?tree=jobs[name,url,builds[number,timestamp,id,result,url,duration,actions[parameters[name,value]]]]",
                    JenkinsUser, JenkinsPassword);
            var jenkinsModelBuilds = JsonConvert.DeserializeObject<JenkinsListOfBuildsModel>(jenkinsBuilds);

            var builds = new List<Build>();
            foreach (var jenkinsJob in jenkinsModelBuilds.Jobs)
            {
                var build = new Build
                {
                    Name = jenkinsJob.Name,
                    Url = jenkinsJob.Url,
                    BuildRuns = new List<BuildRun>()
                };
                foreach (var jenkinsBuild in jenkinsJob.Builds)
                {
                    build.BuildRuns.Add(new BuildRun
                    {
                        Duration = jenkinsBuild.Duration,
                        Id = jenkinsBuild.Id,
                        Url = jenkinsBuild.Url,
                        Result = jenkinsBuild.Result ?? "In progress",
                        StartDateTime = TimeUtil.GetDateTimeFromTimestamp(jenkinsBuild.Timestamp),
                        NodeName= GetParamValueByName(jenkinsBuild.Actions, "currentNodeName")
                    });
                }
                
                builds.Add(build);
            }

            return builds;
        }

        public override List<BuildRun> GetBuildRuns(string buildName)
        {
            var response =
                HttpUtil.GetRequest(
                    url: $"{JenkinsUrl}/job/{buildName}/api/json?tree=builds[number,timestamp,id,result,url,duration]",
                    user: JenkinsUser, password: JenkinsPassword);
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
            return HttpUtil.GetRequest($"{JenkinsUrl}/job/{buildName}/{buildNumber}/consoleText", JenkinsUser,
                JenkinsPassword);
        }

        public override string GetBuildRunNode(string buildName, int buildNumber)
        {
            return Regex.Match(GetBuildRunLog(buildName, buildNumber), NodeNamePattern).Groups[1].ToString();
        }

        private static string GetParamValueByName(IEnumerable<JenkinsBuildAction> actions, string paramName)
        {
            foreach (var action in actions)
            {
                if (action.Parameters == null)
                {
                    continue;
                }
                foreach (var parameter in action.Parameters)
                {
                    if (parameter.Name.Equals(paramName))
                    {
                        return parameter.Value;
                    }
                }
            }

            return "";
        }
    }
}