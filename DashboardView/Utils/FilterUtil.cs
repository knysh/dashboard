using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DashboardView.CI.CIModels;

namespace DashboardView.Utils
{
    public class FilterUtil
    {
        public static List<Build> FilterByName(List<Build> builds, string namePattern)
        {
            return (from build in builds
                where Regex.IsMatch(build.Name, namePattern)
                select build).ToList();
        }

        public static List<Build> FilterByDateTimeAndNode(List<Build> builds, DateTime? fromDateTime,
            DateTime? toDateTime, string nodeNamePattern)
        {
            var filteredListOfBuilds = new List<Build>();
            foreach (var build in builds)
            {
                var newBuild = new Build()
                {
                    Name = build.Name,
                    Url = build.Url,
                    BuildRuns = new List<BuildRun>()
                };
                foreach (var buildRun in build.BuildRuns)
                {
                    if (buildRun.StartDateTime >= fromDateTime && buildRun.StartDateTime <= toDateTime &&
                        Regex.IsMatch(buildRun.NodeName, nodeNamePattern))
                    {
                        newBuild.BuildRuns.Add(buildRun);
                    }
                }

                if (newBuild.BuildRuns.Count > 0)
                {
                    filteredListOfBuilds.Add(newBuild);
                }
            }
            return filteredListOfBuilds;
        }
    }
}