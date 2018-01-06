using DashboardView.CI.CIFactory;
using System;
using System.Web.Mvc;

namespace DashboardView.Controllers
{
    public class ReportController : Controller
    { 
        public ActionResult Index(DateTime? startedFromDate, DateTime? startedToDate)
        {
            return View(new CIFactory().GetCIApi().GetAllBuilds());
        }
    }
}