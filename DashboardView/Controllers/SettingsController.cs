using DashboardView.CI.Jenkins;
using DashboardView.Models;
using DashboardView.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashboardView.Controllers
{
    public class SettingsController : Controller
    { 
        public ActionResult Index()
        {
            var allBuilds = JenkinsApi.GetAllBuilds();
            var existingGroups = DashboardConfigurationManager.GetGroups();
            var newGroups = new Groups { GroupList = new List<Group>() };

            //todo: refactor this
            foreach (var group in existingGroups.GroupList)
            {
                var allBuildsForGroup = allBuilds;
                foreach(var build in allBuildsForGroup)
                {
                    foreach(var existingbuild in group.Builds)
                    {
                        if (existingbuild.Name.Equals(build.Name))
                        {
                            build.IsVisible = true;
                            break;
                        }
                    }
                }

                newGroups.GroupList.Add(new Group { Title = group.Title, Builds = allBuildsForGroup });
            }

            return View(newGroups);
        }
        
        public ActionResult Save(Groups groups)
        {
            DashboardConfigurationManager.UpdateGroups(groups);
            return RedirectToAction("Index", "Home");
        }
    }
}