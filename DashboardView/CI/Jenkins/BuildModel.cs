using DashboardView.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardView.CI.Jenkins
{
    public class BuildModel
    {
        private double duration;
        public string Result { get; set; }
        public int Duration { get { return (int) (duration / 1000); } set { duration = value; } }
        public string Url { get; set; }
    }
}