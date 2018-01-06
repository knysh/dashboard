using System;

namespace DashboardView.CI.Jenkins
{
    internal class JenkinsBuildRun
    {
        public string Result { get; set; }
        public int Id { get; set; }
        public double Timestamp { get; set; }
        public double Duration { get; set; }
    }
}