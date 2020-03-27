using System.Web;
using System.Web.Optimization;

namespace Enpratik.Web.PageDemo
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Content/js/jquery-3.3.1.min.js",
                        "~/Content/js/bootstrap/bootstrap.js",
                        "~/Content/js/jquery-ui.min.js",
                        "~/Content/js/jquery.countdown.min.js",
                        "~/Content/js/jquery.nice-select.min.js",
                        "~/Content/js/jquery.zoom.min.js",
                        "~/Content/js/jquery.dd.min.js",
                        "~/Content/js/jquery.slicknav.js",
                        "~/Content/js/owl.carousel.min.js",
                        "~/Content/js/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/css/bootstrap/bootstrap.css",
                    "~/Content/css/font-awesome.min.css",
                    "~/Content/css/themify-icons.css",
                    "~/Content/css/elegant-icons.css",
                    "~/Content/css/owl.carousel.min.css",
                    "~/Content/css/nice-select.css",
                    "~/Content/css/jquery-ui.min.css",
                    "~/Content/css/slicknav.min.css",
                    "~/Content/css/style.css"));
        }
    }
}
