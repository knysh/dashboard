using System.Collections.Generic;

namespace DashboardView.CI.CIModels
{
    public class Build
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<BuildRun> BuildRuns { get; set; } = null;
    }
}