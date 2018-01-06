using System.Collections.Generic;

namespace DashboardView.CI.CIModels
{
    public class Group
    {
        public string Title { get; set; }
        public List<Build> Builds { get; set; }
    }
}