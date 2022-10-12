using Hafazah.Common;
using System.Web.Mvc;

namespace Hafazah.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Members");
            else
                return RedirectToAction("Login", "Account");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Anas Al-Jayyusi";

            return View();
        }
    }
}