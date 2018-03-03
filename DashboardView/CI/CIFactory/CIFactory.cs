using DashboardView.CI.Jenkins;
using DashboardView.Utils;
using System;

namespace DashboardView.CI.CIFactory
{
    public class CIFactory
    {
        public static CIApi GetCIApi()
        {
            var currentCIType = ConfigReader.GetCiType();
            switch (currentCIType)
            {
                case CITypes.Jenkins: return new JenkinsApi();
                case CITypes.Unknown:
                default: throw new IndexOutOfRangeException($"Unknow ci type {currentCIType}");
            }
        }
    }
}
