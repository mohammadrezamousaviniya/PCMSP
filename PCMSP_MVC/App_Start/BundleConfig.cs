using System.Web;
using System.Web.Optimization;

namespace PCMSP_MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //JS Starts

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Resources/JavaScript/web/js/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxpagination").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/ajax-pagination.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerycountdown").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/jquery.countdown.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/timer").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/timer.js"));

            bundles.Add(new ScriptBundle("~/bundles/crummegamenu").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/crum-mega-menu.js"));

            bundles.Add(new ScriptBundle("~/bundles/swiperjquery").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/swiper.jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerytypeahead").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/jquery.typeahead.js"));

            bundles.Add(new ScriptBundle("~/bundles/velocity").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/velocity.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/formactions").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/form-actions.js"));

            bundles.Add(new ScriptBundle("~/bundles/waypoints").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/waypoints.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerycountTo").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/jquery-countTo.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryniceselect").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/jquery.nice-select.js"));

            bundles.Add(new ScriptBundle("~/bundles/imagesLoaded").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/imagesLoaded.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymagnificpopup").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/jquery.magnific-popup.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymatchHeight").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/jquery.matchHeight.js"));

            bundles.Add(new ScriptBundle("~/bundles/Headroom").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/Headroom.js"));

            bundles.Add(new ScriptBundle("~/bundles/smoothscroll").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/smooth-scroll.js"));

            bundles.Add(new ScriptBundle("~/bundles/segment").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/segment.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/momenttimezone").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/moment-timezone.js"));

            bundles.Add(new ScriptBundle("~/bundles/isotopepkgd").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/isotope.pkgd.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ionrangeSlider").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/ion.rangeSlider.js"));

            bundles.Add(new ScriptBundle("~/bundles/parsley").Include(
                       "~/Resources/JavaScript/web/js/js-plugins/parsley.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                       "~/Resources/JavaScript/web/js/main.js"));
            bundles.Add(new ScriptBundle("~/bundles/custome").Include(
                "~/Resources/JavaScript/Custome.js"));

            // CSS Starts 
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Resources/CSS/web/css/PandaMainSiteCSS.css",
                      "~/Resources/CSS/web/fonts/fIcon/flaticon.css"));

        }
    }
}
