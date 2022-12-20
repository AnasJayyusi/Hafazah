using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Hafazah.Common
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object cultureRequest = null;
            filterContext.ActionParameters.TryGetValue("culture", out cultureRequest);
            var currentCulture = Thread.CurrentThread.CurrentCulture.ToString();

            // If sessiong was empty so maybe it is First Login  
            if (Session["userCultureInfo"] == null || (cultureRequest != null ? cultureRequest.ToString() != currentCulture : false))
            {
                // Check The Link If some culture Exists 
                var culture = cultureRequest as string;
                if (!string.IsNullOrEmpty(culture))
                {
                    Session["userCultureInfo"] = CultureInfo.GetCultureInfo(culture);
                    var cultureInfo = CultureInfo.GetCultureInfo(culture);
                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                    base.OnActionExecuting(filterContext);
                }
            }
            else if (Session["userCultureInfo"] != null)
            {
                var cultureInfo = new CultureInfo(Session["userCultureInfo"].ToString());
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                base.OnActionExecuting(filterContext);
            }
        }

        public ActionResult RedirectToLocalized()
        {
            string lang = Thread.CurrentThread.CurrentCulture.ToString();
            RedirectPermanent("/" + lang + "/");
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Members");
            else
                return RedirectToAction("Login", "Account");
        }
    }
}