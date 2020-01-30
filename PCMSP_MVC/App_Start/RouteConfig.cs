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
                , "درباره-ما"
                , new { controller = "Media",action= "ContactUs", id = "" }
            );

            routes.MapRoute(
                "Niky"
                , "{controller}/{action}"
                
            );
        }
    }
}

