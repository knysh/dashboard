using System.Collections.Generic;

namespace DashboardView.CI.Jenkins
{
    internal class JenkinsBuildAction
    {
        public string _class { get; set; }
        public List<JenkinsBuildParameter> Parameters { get; set; }
    }
}