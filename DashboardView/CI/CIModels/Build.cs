using System.Collections.Generic;

namespace DashboardView.CI.CIModels
{
    public class Build
    {
        private double duration;
        public string Name { get; set; }
        public string Result { get; set; }
        public int Duration { get { return (int)(duration / 1000); } set { duration = value; } }
        public string Url { get; set; }
        public List<BuildRun> BuildRuns { get; set; }
    }
}