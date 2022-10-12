using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Hafazah.Common
{
    public class BaseController : Controller
    {
        // GET: Base

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string culture = filterContext.RouteData.Values["culture"]?.ToString() ?? "en";

            // Set the action parameter just in case we didn't get one
            // from the route.
            filterContext.ActionParameters["culture"] = culture;
            var cultureInfo = CultureInfo.GetCultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            // Because we've overwritten the ActionParameters, we
            // make sure we provide the override to the
            // base implementation.
            base.OnActionExecuting(filterContext);
        }

        public ActionResult RedirectToLocalized()
        {
            RedirectPermanent("/en/");
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Members");
            else
                return RedirectToAction("Login", "Account");
        }
    }
}