using System;

namespace DashboardView.CI.CIModels
{
    public class BuildRun
    {
        public string Result { get; set; }
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public int Duration { get; set; }
        public string Url { get; set; }
        public string NodeName { get; set; } = null;
    }
}