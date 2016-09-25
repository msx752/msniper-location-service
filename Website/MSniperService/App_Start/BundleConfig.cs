using System.Web;
using System.Web.Optimization;

namespace RMSniper1
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include(
                        "~/Scripts/modernizr-*"));



            bundles.Add(new ScriptBundle("~/Scripts/Global").Include(
                           "~/Scripts/jquery-{version}.js",
                           "~/Scripts/bootstrap.js",
                           "~/Scripts/respond.js"
                           ));


            bundles.Add(new StyleBundle("~/Styles/Global").Include(
                          "~/Content/font-awesome.css",
                           "~/Content/bootstrap.css"
                ));


            bundles.Add(new StyleBundle("~/Styles/snipe").Include(
                      "~/Content/site.css",
                      "~/Content/pkmn2.css"));

            bundles.Add(new ScriptBundle("~/Scripts/snipe").Include(
                "~/Scripts/jquery.signalR-2.2.1.js",
                "~/Scripts/pokemonImages.js",
                "~/Scripts/pkmn.js",
                "~/Scripts/client.js"
                ));

            BundleTable.EnableOptimizations = true;//for now
        }
    }
}
