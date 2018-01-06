using DashboardView.CI.CIFactory;
using DashboardView.CI.CIModels;
using DashboardView.Utils;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DashboardView.Controllers
{
    public class SettingsController : Controller
    { 
        public ActionResult Index()
        {
            var allBuilds = new CIFactory().GetCIApi().GetAllBuilds();
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