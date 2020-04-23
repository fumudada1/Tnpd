using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TnpdAdmin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "News",                                           // Route name
                "News/{sClass}/{page}",                            // URL with parameters
                new { controller = "News", action = "Index",  sClass = UrlParameter.Optional, page = UrlParameter.Optional },
                constraints: new { controller = "News", action = "Index", id = UrlParameter.Optional, sClass = @"\d+" }  // Parameter defaults
                
            );
        }
    }
}