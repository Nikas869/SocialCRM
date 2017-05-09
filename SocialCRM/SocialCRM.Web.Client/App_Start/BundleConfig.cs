using System.Web.Optimization;

namespace SocialCRM.Web.Client
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));
            
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                      "~/Scripts/Vendors/Bootstrap/bootstrap.js",
                      "~/Scripts/Vendors/AdminLTE/app.js"));

            bundles.Add(new StyleBundle("~/Content/main").Include(
                      "~/Content/Vendors/Bootstrap/bootstrap.css",
                      "~/Content/Vendors/AdminLTE/AdminLTE.css",
                      "~/Content/Vendors/AdminLTE/skins/*.css"));
        }
    }
}