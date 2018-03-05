using System.Collections.Generic;

namespace DashboardView.CI.Jenkins
{
    internal class JenkinsBuildNameModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<JenkinsBuildRun> Builds { get; set; }
    }
}