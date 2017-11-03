using System.Web;
using System.Web.Optimization;

namespace IMS
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

            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                        "~/Scripts/raphael.js",
                        "~/Content/morris/morris.min.js",
                        "~/Content/sparkline/jquery.sparkline.min.js",
                        "~/Content/jvectormap/jquery-jvectormap-1.2.2.min.js",
                        "~/Content/jvectormap/jquery-jvectormap-world-mill-en.js",
                        "~/Content/knob/jquery.knob.js",
                        "~/Scripts/moment.min.js",
                        "~/Content/daterangepicker/daterangepicker.js",
                        "~/Content/datepicker/bootstrap-datepicker.js",
                        "~/Content/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                        "~/Content/slimScroll/jquery.slimscroll.min.js",
                        //"~/Content/fastclick/fastclick.js",
                        "~/Scripts/Custom/dist/js/app.min.js",
                        "~/Content/select2/select2.full.min.js",
                        "~/Content/input-mask/jquery.inputmask.js",
                        "~/Content/input-mask/jquery.inputmask.date.extensions.js",
                        "~/Content/input-mask/jquery.inputmask.extensions.js",
                        "~/Content/colorpicker/bootstrap-colorpicker.min.js",
                        "~/Content/timepicker/bootstrap-timepicker.min.js",
                        "~/Content/iCheck/icheck.min.js",
                        "~/Content/datatables/jquery.dataTables.min.js",
                        "~/Content/datatables/dataTables.bootstrap.min.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                    "~/Content/AdminLTE.css",
                   "~/Scripts/Custom/dist/css/skins/_all-skins.css",
                   "~/Content/iCheck/flat/blue.css",
                   "~/Content/morris/morris.css",
                   "~/Content/jvectormap/jquery-jvectormap-1.2.2.css",
                   "~/Content/datepicker/datepicker3.css",
                   "~/Content/daterangepicker/daterangepicker.css",
                   "~/Content/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                   "~/Content/iCheck/all.css",
                   "~/Content/colorpicker/bootstrap-colorpicker.min.css",
                   "~/Content/timepicker/bootstrap-timepicker.min.css",
                   "~/Content/select2/select2.min.css",
                   "~/Content/Notification.css",
                   "~/Content/datatables/dataTables.bootstrap.css"
              ));
        }
    }
}
