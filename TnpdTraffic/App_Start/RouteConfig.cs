using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TnpdTraffic
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },  // Parameter defaults
                namespaces: new[] { "TnpdTraffic.Controllers" }
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