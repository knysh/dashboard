using DashboardView.CI.CIModels;
using DashboardView.Utils;
using System.Collections.Generic;
using System.Web.Mvc;
using DashboardView.CI;

namespace DashboardView.Controllers
{
    public class SettingsController : Controller
    { 
        public ActionResult Index()
        {
            var allBuilds = CIManager.GetCurrentBuilds();
            var existingGroups = DashboardConfigurationManager.GetGroups();
            var newGroups = new Groups { GroupList = new List<Group>() };

            foreach (var group in existingGroups.GroupList)
            {
                var allBuildsForGroup = allBuilds;
                newGroups.GroupList.Add(new Group { Title = group.Title, Builds = allBuildsForGroup });
            }

            return View(newGroups);
        }
    }
}