using System.Collections.Generic;
using DashboardView.CI.CIModels;

namespace DashboardView.CI
{
    public class CIManager
    {
        private static List<Build> _currentBuilds;

        public static List<Build> GetCurrentBuilds()
        {
            if (_currentBuilds != null) return _currentBuilds;
            _currentBuilds = new CIFactory.CIFactory().GetCIApi().GetAllBuilds();
            return _currentBuilds;
        }

        public static void CleanCurrentBuilds()
        {
            _currentBuilds = null;
        }
    }
}