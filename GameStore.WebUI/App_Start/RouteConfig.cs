using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(null,
                "{controller}/{action}/{shortId}",
                 new
                 {
                     controller = "Game",
                     action = "Info"
                 });

            routes.MapRoute(null, "{controller}/{action}",
                new
                {
                    controller = "Home",
                    action = "Index"
                });
        }
    }
}
