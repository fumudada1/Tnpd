using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tnpd.Models
{
    public class MyHttpModule:IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestContent += new EventHandler(context_PreSendRequestContent);
        }

        private void context_PreSendRequestContent(object sender, EventArgs e)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Headers.Remove("Server");
        }

        public void Dispose()
        {
            
        }
    }
}