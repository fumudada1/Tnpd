using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace tnpd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "NewsDetails",                                           // Route name
                "News/Details/{unid}/{sClass}/{id}",                            // URL with parameters
                new { controller = "News", action = "Details" },
                constraints: new { controller = "News", Action = "Details", unid = new GuidConstraint(), id = @"\d+", sClass = @"\d+" },  // Parameter defaults
                namespaces: new[] { "tnpd.Controllers" }
            );


            routes.MapRoute(
                "News",                                           // Route name
                "News/{id}/{sClass}/{page}",                            // URL with parameters
                new { controller = "News", action = "Index", id = UrlParameter.Optional, sClass = UrlParameter.Optional, page = UrlParameter.Optional },
                constraints: new { controller = "News", action = "Index", id = new GuidConstraint(), sClass = @"\d+" },  // Parameter defaults
                namespaces: new[] { "tnpd.Controllers" }
            );




            routes.MapRoute(
                "Article",                                           // Route name
                "Article/{id}",                            // URL with parameters
                new { controller = "Content", action = "Article", id = UrlParameter.Optional },
                constraints: new { id = new GuidConstraint() },
                namespaces: new[] { "tnpd.Controllers" }
            );
            routes.MapRoute(
                "Directory",                                           // Route name
                "Directory/{id}",                            // URL with parameters
                new { controller = "Content", action = "Directory", id = UrlParameter.Optional },
                constraints: new { id = new GuidConstraint() },
                namespaces: new[] { "tnpd.Controllers" }
            );

            routes.MapRoute(
                "SystemDetail",                                           // Route name
                "{controller}/Details/{unid}/{id}/{page}",                            // URL with parameters
                new { action = "Details", id = UrlParameter.Optional, page = UrlParameter.Optional }, // Parameter defaults
                constraints: new { unid = new GuidConstraint(), id = @"\d+" },
                namespaces: new[] { "tnpd.Controllers" }
            );

            routes.MapRoute(
                "SystemWrite",                                           // Route name
                "{controller}/Write/{unid}/{id}/{page}",                            // URL with parameters
                new { action = "Write", id = UrlParameter.Optional, page = UrlParameter.Optional }, // Parameter defaults
                constraints: new { unid = new GuidConstraint(), id = @"\d+" },
                namespaces: new[] { "tnpd.Controllers" }
            );

         

            routes.MapRoute(
                "RSS",                                              // Route name
                "RSS/Index/{id}/{siteCode}",                           // URL with parameters
                new { controller = "Rss", action = "Index", id = UrlParameter.Optional, siteCode = UrlParameter.Optional },  // Parameter defaults
                namespaces: new[] { "tnpd.Controllers" }
            );


            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },  // Parameter defaults
                namespaces: new[] { "tnpd.Controllers" }
            );

        }
        public class GuidConstraint : IRouteConstraint
        {
            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
                RouteDirection routeDirection)
            {
                if (values.ContainsKey(parameterName))
                {
                    var guid = values[parameterName] as Guid?;
                    if (guid.HasValue == false)
                    {
                        var stringValue = values[parameterName] as string;
                        if (string.IsNullOrWhiteSpace(stringValue) == false)
                        {
                            Guid parsedGuid;
                            // .NET 4 新增的 Guid.TryParse
                            Guid.TryParse(stringValue, out parsedGuid);
                            guid = parsedGuid;
                        }
                    }
                    return (guid.HasValue && guid.Value != Guid.Empty);
                }
                return false;
            }
        }
    }
    
}