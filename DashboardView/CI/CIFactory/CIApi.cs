using DashboardView.CI.CIModels;
using System.Collections.Generic;

namespace DashboardView.CI.CIFactory
{
    public abstract class CIApi
    {
        public abstract List<Build> GetAllBuilds();

        public abstract Build GetBuildInfo(string buildName);

        public abstract List<BuildRun> GetBuildRuns(string buildName);
    }
}
