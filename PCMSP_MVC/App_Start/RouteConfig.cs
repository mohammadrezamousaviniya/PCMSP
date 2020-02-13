using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PCMSP_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Media", action = "Index" }
            );
            routes.MapRoute(
                "ContactUS"
                , "ارتباط-با-ما"
                , new { controller = "Media",action= "ContactUs", id = "" }
            );
            routes.MapRoute(
                "AboutUS"
                , "درباره-ما"
                , new { controller = "Media", action = "AboutUs", id = "" }
            );
            
            routes.MapRoute(
                name: "News",
                url: "اخبار/{category}/{tags}/{pages}",
                defaults: new { Controller = "Media", Action = "News" }
            );

            routes.MapRoute(
                name: "News_Details",
                url: "جزئیات خبر/{category}/{title}/{pre}/{id_news}/{next}",
                defaults: new { Controller = "Media", Action = "News_Details" }
            );

            routes.MapRoute(
                name: "userCustomer",
                url: "{controller}/{action}",
                defaults: new { controller = "userCustomer" }
            );

            routes.MapRoute(
                "Niky"
                , "{controller}/{action}");

        }
    }
}

