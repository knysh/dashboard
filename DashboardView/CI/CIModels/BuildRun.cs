using System;

namespace DashboardView.CI.CIModels
{
    public class BuildRun
    {
        public string Result { get; set; }
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}