using System.Web;
using System.Web.Optimization;


namespace GMSTYS.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                   "~/Scripts/bootstrap.js"));

             bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                              "~/Scripts/knockout-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
           "~/Content/bootstrap.css",
           "~/Content/site.css"));
        }


    }
}