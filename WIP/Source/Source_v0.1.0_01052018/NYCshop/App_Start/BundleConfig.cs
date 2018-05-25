using System.Web;
using System.Web.Optimization;

namespace NYCshop
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/validate").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/extra-css").Include(
                      "~/Content/animate.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/custom.css",
                      "~/Content/font-awesome.css",
                      "~/Content/owl.carousel.css",
                      "~/Content/owl.theme.css",
                      "~/Content/owl.transitions.css",
                      "~/Content/style.blue.css",
                      "~/Content/style.default.css",
                      "~/Content/style.green.css",
                      "~/Content/style.mono.css",
                      "~/Content/style.pink.css",
                      "~/Content/style.violet.css",
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/extra-js").Include(
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/bootstrap-hover-dropdown.js",
                        "~/Scripts/front.js",
                        "~/Scripts/jquery.cookie.js",
                        "~/Scripts/jquery.flexslider.js",
                        "~/Scripts/jquery.flexslider.min.js",
                        "~/Scripts/jquery-1.11.0.min.js",
                        "~/Scripts/main.js",
                        "~/Scripts/modernizr.js",
                        "~/Scripts/owl.carousel.min.js",
                        "~/Scripts/respond.min.js",
                        "~/Scripts/waypoints.min.js"));
        }
    }
}