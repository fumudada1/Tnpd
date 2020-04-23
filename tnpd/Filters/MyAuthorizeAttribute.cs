using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using Newtonsoft.Json;
using System.Web.Routing;

namespace tnpd.Filters
{
    public class MyAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext authorizationContext)
        {
            if (authorizationContext.RequestContext.HttpContext.Request.HttpMethod != "POST")
                return;

            new ValidateAntiForgeryTokenAttribute().OnAuthorization(authorizationContext);
        }
    }
}
