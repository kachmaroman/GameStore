using System.Web.Mvc;

namespace GameStore.WebUI.HtmlHelpers
{
    public static class LinkHelpers
    {
        public static string IsActive(this HtmlHelper html, string control, string action)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            var isActive = routeControl == control && routeAction == action;

            return isActive ? "active" : "";
        }
    }
}