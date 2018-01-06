namespace DashboardView.CI.Jenkins
{
    internal class JenkinsBuildModel
    {
        private double duration;
        public string Result { get; set; }
        public int Duration { get { return (int) (duration / 1000); } set { duration = value; } }
        public string Url { get; set; }
    }
}