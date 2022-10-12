using Hafazah.Common;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hafazah.Controllers
{
    public class ControlPanelController : BaseController
    {
        // GET: ControlPanel
        [Route("ControlPanel")]
        public ActionResult Index()
        {
            return View();
        }
    }
}