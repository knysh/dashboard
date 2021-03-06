﻿using System.Collections.Generic;

namespace DashboardView.CI.Jenkins
{
    internal class JenkinsBuildRun
    {
        public string Result { get; set; }
        public int Id { get; set; }
        public double Timestamp { get; set; }
        public int Duration { get; set; }
        public string Url { get; set; }
        public List<JenkinsBuildAction> Actions { get; set; }
    }
}