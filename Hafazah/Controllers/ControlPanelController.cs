using Hafazah.Common;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hafazah.Controllers
{
    public class ControlPanelController : BaseController
    {
        // GET: ControlPanel

        [Route("ControlPanel")]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}