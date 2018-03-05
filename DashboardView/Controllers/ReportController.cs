using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using DashboardView.CI.CIFactory;
using DashboardView.CI.CIModels;
using DashboardView.Utils;

namespace DashboardView.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index(string jobNamePattern, DateTime? fromDateTime, DateTime? toDateTime,
            string slavePattern)
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
            var buildsByName = FilterUtil.FilterByName(builds, jobNamePattern);
            var filteredBuilds = FilterUtil.FilterByDateTimeAndNode(buildsByName, fromDateTime, toDateTime, slavePattern);
            return View(filteredBuilds);
        }
    }
}