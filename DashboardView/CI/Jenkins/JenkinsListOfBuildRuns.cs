using System.Collections.Generic;

namespace DashboardView.CI.Jenkins
{
    internal class JenkinsListOfBuildRuns
    {
        public string _Class { get; set; }
        public List<JenkinsBuildRun> Builds { get; set; }
    }
}