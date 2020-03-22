using System.Web.Optimization;

namespace GameStore.WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBoundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Scripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js",
                "~/Scripts/owl.carousel.min.js",
                "~/Scripts/cartAjax.js",
                "~/Scripts/jquery-ui-1.12.1.min.js",
                "~/Scripts/searchGames.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/owl.carousel.min.css",
                "~/Content/css/owl.theme.default.min.css",
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/main.css",
                "~/Content/themes/base/all.css"));
        }
    }
}