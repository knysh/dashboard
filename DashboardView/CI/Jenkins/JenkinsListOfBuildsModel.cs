﻿using System.Collections.Generic;

namespace DashboardView.CI.Jenkins
{
    internal class JenkinsListOfBuildsModel
    {
        public string _Class { get; set; }
        public List<JenkinsBuildNameModel> Jobs { get; set; }
    }
}