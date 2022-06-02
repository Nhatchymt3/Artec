using System.Web;
using System.Web.Optimization;

namespace Labixa
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //CSS for  Ace admin
            bundles.Add(new StyleBundle("~/Style/Common").Include(
     "~/Content/admin/css/bootstrap.min.css",
      "~/Content/admin/css/font-awesome.min.css",
       "~/Content/admin/css/ace.min.css",
       "~/Content/admin/css/ace-rtl.min.css",
         "~/Content/admin/css/ace-skins.min.css",
         "~/Content/admin/css/ace-fonts.css",
          "~/Content/admin/css/datepicker.css",
          "~/Content/admin/css/font-awesome.min.css",
         "~/Content/admin/admincustom.css"
            ));

            //Scipts for  Ace admin
            bundles.Add(new ScriptBundle("~/Scripts/Common").Include(
    "~/Content/admin/js/jquery-2.0.3.min.js",
    "~/Content/admin/js/bootstrap.min.js",
    "~/Content/admin/js/jquery-ui-1.10.3.full.min.js",
    "~/Content/admin/js/date-time/bootstrap-datepicker.min.js",
                    "~/Content/assets/js/ace-elements.min.js",
    "~/Content/admin/js/ace.min.js",
    "~/Content/admin/js/ace-extra.min.js",
    "~/Content/admin/vpn/adminvpn.js"
            ));
            //css tai lieu
            bundles.Add(new StyleBundle("~/Styles/tai-lieu").Include(
                "~/Content/tailieu/css/bootstrap.min.css",
                "~/Content/tailieu/css/jquery-ui.css",
                "~/Content/tailieu/css/jquery.fancybox.min.css",
                "~/Content/tailieu/css/font-awosome.css",
                "~/Content/tailieu/flat-font/flaticon.css",
                "~/Content/tailieu/css/ticker.min.css",
                "~/Content/tailieu/css/owl.carousel.min.css",
                "~/Content/tailieu/css/owl.theme.default.min.css",
                "~/Content/tailieu/css/sm-core-css.css",
                "~/Content/tailieu/css/sm-mint.css",
                "~/Content/tailieu/css/sm-style.css",
                "~/Content/tailieu/css/aos.css",
                "~/Content/tailieu/css/animate.min.css",
                "~/Content/tailieu/css/magnific-popup.css",
                "~/Content/tailieu/css/style.css"));
            //js tai lieu
            bundles.Add(new ScriptBundle("~/Scripts/tai-lieu").Include(
                "~/Content/tailieu/js/jquery-3.4.1.min.js",
                "~/Content/tailieu/js/bootstrap.min.js",
                "~/Content/tailieu/js/aos.js",
                "~/Content/tailieu/js/jquery-ui.js",
                "~/Content/tailieu/js/jquery.smartmenus.js",
                "~/Content/tailieu/js/owl.carousel.min.js",
                "~/Content/tailieu/js/jquery.fancybox.min.js",
                "~/Content/tailieu/js/jquery.magnific-popup.min.js",
                "~/Content/tailieu/js/waypoints.min.js",
                "~/Content/tailieu/js/theme.js"
                ));
        }
    }
}
