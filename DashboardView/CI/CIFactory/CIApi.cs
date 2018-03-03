using DashboardView.CI.CIModels;
using System.Collections.Generic;

namespace DashboardView.CI.CIFactory
{
    public abstract class CIApi
    {
        public abstract List<Build> GetAllBuilds();

        public abstract List<BuildRun> GetBuildRuns(string buildName);

        public abstract string GetBuildRunLog(string buildName, int buildNumber);

        public abstract string GetBuildRunNode(string buildName, int buildNumber);
    }
}
