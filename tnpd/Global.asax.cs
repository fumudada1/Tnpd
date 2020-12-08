using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TnpdModels;

namespace tnpd
{
    // 注意: 如需啟用 IIS6 或 IIS7 傳統模式的說明，
    // 請造訪 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            
            
        }

        protected void Session_Start(Object sender, EventArgs e)
        {

            if (Application["checkDay"] == null || Application["holiday"]==null)
            {
                string holiday = getHoliday();
                Application["checkDay"] = DateTime.Today;
                Application["holiday"] = holiday;
                Session["holiday"] = holiday;
            }
            else
            {
                DateTime checkDay = (DateTime) Application["checkDay"];
                if (checkDay != DateTime.Today)
                {
                    string holiday = getHoliday();
                    Application["checkDay"] = DateTime.Today;
                    Application["holiday"] = holiday;
                    Session["holiday"] = holiday;
                }
                else
                {
                    Session["holiday"] = Application["holiday"];
                }
            }

        }

        private string getHoliday()
        {
            BackendContext db = new BackendContext();
            //三大節

            DateTime sday = DateTime.Today.AddDays(-7);
            DateTime eday = DateTime.Today.AddDays(7);
            var holidys = db.Holidays.Where(x => x.InitDate >= sday && x.InitDate <= eday);
            if (holidys.Any(x => x.Description.Contains("除夕")))
            {
                return "top-banner-1.png";
            }
            if (holidys.Any(x => x.Description.Contains("端午")))
            {
                return "top-banner-2.png";
            }
            if (holidys.Any(x => x.Description.Contains("中秋")))
            {
                return "top-banner-3.png";
            }
            //聖誕節
            DateTime xmas = new DateTime(DateTime.Today.Year, 12, 25);
            if (xmas >= sday && xmas <= eday)
            {
                return "top-banner-4.png";
            }

            return null;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpRuntimeSection section = (HttpRuntimeSection)ConfigurationManager.GetSection("system.web/httpRuntime");
            int maxFileSize = section.MaxRequestLength * 1024;
            if (Request.ContentLength > maxFileSize)
            {
                try
                {
                    Response.Redirect("/Error/SizeError");
                }
                catch (Exception ex)
                {
                   
                }
            }
        }
    }
}