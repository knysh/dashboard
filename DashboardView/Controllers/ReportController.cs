using DashboardView.CI.CIFactory;
using DashboardView.CI.CIModels;
using System.Web.Mvc;

namespace DashboardView.Controllers
{
    public class ReportController : Controller
    { 
        public ActionResult Index()
        {
            var group = new Group
            {
                Builds = new CIFactory().GetCIApi().GetAllBuilds(),
                Title = "Group"
            };
            return View(group);
        }
    }
}