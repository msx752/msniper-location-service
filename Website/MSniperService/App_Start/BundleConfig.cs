using System.Web;
using System.Web.Optimization;

namespace MSniperService
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            //bundles.Add(new ScriptBundle("~/Scripts/jqueryval").Include(
            //            "~/assets/msniper/js/jquery.validate*"
            //            ));

            //bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include(
            //            "~/assets/msniper/js/modernizr-*"
            //            ));


            bundles.Add(new StyleBundle("~/Styles/global").Include(
                          "~/assets/css/bootstrap.css",
                           "~/assets/css/ionicons.css",
                           "~/assets/css/main.css",
                           "~/assets/css/css.css",
                           "~/assets/msniper/css/msniper.css"
                ));
            
            bundles.Add(new ScriptBundle("~/Scripts/global").Include(
                           "~/assets/js/jquery/jquery-2.1.0.min.js",
                           "~/assets/js/bootstrap/bootstrap.js",
                           "~/assets/js/plugins/bootstrap-multiselect/bootstrap-multiselect.js",
                           "~/assets/js/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                           "~/assets/js/custom-common.js",
                           "~/assets/js/plugins/stat/flot/jquery.flot.min.js",
                           "~/assets/js/plugins/stat/flot/jquery.flot.resize.min.js",
                           "~/assets/js/plugins/stat/flot/jquery.flot.time.min.js",
                           "~/assets/js/plugins/stat/flot/jquery.flot.orderBars.js",
                           "~/assets/js/plugins/stat/flot/jquery.flot.tooltip.min.js",
                           "~/assets/js/plugins/mapael/raphael/raphael-min.js",
                           "~/assets/js/plugins/mapael/jquery.mapael.js",
                           "~/assets/js/plugins/mapael/maps/world_countries.js",
                           "~/assets/js/plugins/bootstrap-progressbar/bootstrap-progressbar.min.js",
                           "~/assets/js/plugins/moment/moment.min.js",
                           "~/assets/js/plugins/bootstrap-editable/bootstrap-editable.min.js",
                           "~/assets/js/plugins/jquery-maskedinput/jquery.masked-input.min.js",
                           "~/assets/js/custom-charts.js",
                           "~/assets/js/custom-maps.js",
                           "~/assets/js/custom-elements.js"
               ));

            bundles.Add(new ScriptBundle("~/Scripts/table").Include(
                        "~/assets/js/plugins/datatable/jquery.dataTables.min.js",
                         "~/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js",
                          "~/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js",
                          "~/assets/js/plugins/datatable/exts/dataTables.tableTools.min.js",
                          "~/assets/js/plugins/datatable/dataTables.bootstrap.js",
                          "~/assets/js/custom-table.js"
              ));

            bundles.Add(new ScriptBundle("~/Scripts/snipe").Include(
                "~/assets/msniper/js/jquery.signalR-2.2.1.js",
                "~/assets/msniper/js/pokemonImages.js",
                "~/assets/msniper/js/pkmn.js",
                "~/assets/msniper/js/client.js",
                "~/assets/msniper/js/AutoMSniper.js"
                ));

            BundleTable.EnableOptimizations = false;//for now
        }
    }
}
