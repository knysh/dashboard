using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardView.CI.Jenkins
{
    public class JenkinsListOfBuildsModel
    {
        public string _Class { get; set; }
        public List<JenkinsBuildNameModel> Jobs { get; set; }
    }
}