using DashboardView.Utils;
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