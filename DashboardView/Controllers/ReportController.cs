using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using DashboardView.CI.CIFactory;
using DashboardView.CI.CIModels;

namespace DashboardView.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index(string jobNamePattern, DateTime? fromDateTime, DateTime? toDateTime,
            string slavesPattern)
        {
            if (fromDateTime == null)
            {
                fromDateTime = DateTime.UtcNow;
            }
            if (toDateTime == null)
            {
                toDateTime = DateTime.UtcNow;
            }
            if (jobNamePattern == null)
            {
                return View(new List<Build>());
            }

            var builds = CIFactory.GetCIApi().GetAllBuilds();
            builds = (from build in builds
                where Regex.IsMatch(build.Name, jobNamePattern)
                select build).ToList();

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
                    if (buildRun.StartDateTime >= fromDateTime && buildRun.StartDateTime <= toDateTime)
                    {
                        newBuild.BuildRuns.Add(buildRun);
                    }
                }

                if (newBuild.BuildRuns.Count > 0)
                {
                    filteredListOfBuilds.Add(newBuild);
                }
            }
            return View(filteredListOfBuilds);
        }
    }
}