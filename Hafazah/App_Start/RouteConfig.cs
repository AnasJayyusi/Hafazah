using System.Web.Mvc;
using System.Web.Routing;

namespace Hafazah
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Root",
                url: "",
                defaults: new
                {
                    controller = "Base",
                    action = "RedirectToLocalized"
                }
            );
            routes.MapRoute(
                name: "Default",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new
                {
                    culture = "en",
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                constraints: new { culture = "en|ar" }
            );
        }
    }
}
