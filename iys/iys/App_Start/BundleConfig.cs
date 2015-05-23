using System.Web;
using System.Web.Optimization;

namespace iys
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js", "~/Scripts/jquery.min.js", "~/Scripts/jquery.cycle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(

                      "~/Content/bootstrap.css", "~/Content/stil.css", "~/Content/layout.css", "~/Content/featured_slide.css", "~/Content/forms.css", "~/Content/navi.css", "~/Content/tables.css",//));
                      "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Bundles/css").Include(
                     "~/Content/bootstrap.css", "~/Content/style.css", "~/Content/layout.css"));
            //"~/Content/site.css", ));
            
            
            // ******************** orange menu için ********************************
            bundles.Add(new ScriptBundle("~/bundles/orangeMenuScript").Include(
                             "~/Scripts/customScript.js"));
        }
    }
}
