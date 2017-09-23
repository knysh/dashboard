using DashboardView.Models;
using DashboardView.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashboardView.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var groups = DashboardConfigurationManager.GetGroups();
            return View(groups);
        }
    }
}