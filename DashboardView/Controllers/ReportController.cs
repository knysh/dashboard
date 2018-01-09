using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using DashboardView.CI;
using DashboardView.CI.CIModels;

namespace DashboardView.Controllers
{
    public class ReportController : Controller
    { 
        public ActionResult Index(string jobNamePattern, DateTime? fromDateTime, DateTime? toDateTime)
        {
            if (fromDateTime == null)
            {
                fromDateTime = DateTime.MinValue;
            }
            if (toDateTime == null)
            {
                toDateTime = DateTime.MaxValue;
            }

            var filteredListOfBuilds = new List<Build>();
            var builds = CIManager.GetCurrentBuilds();
            foreach (var build in builds)
            {
                if (jobNamePattern != null && !Regex.IsMatch(build.Name, jobNamePattern))
                {
                    continue;
                }
                var newBuild = new Build()
                {
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
                    newBuild.Duration = build.Duration;
                    newBuild.Name = build.Name;
                    newBuild.Result = build.Result;
                    newBuild.Url = build.Url;
                    filteredListOfBuilds.Add(newBuild);
                }
            }
            return View(filteredListOfBuilds);
        }

        public ActionResult UpdateData(string jobNamePattern, DateTime? fromDateTime, DateTime? toDateTime)
        {
            CIManager.CleanCurrentBuilds();
            CIManager.GetCurrentBuilds();
            return RedirectToAction("Index");
        }
    }
}