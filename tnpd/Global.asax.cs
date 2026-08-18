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

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            if (ex != null)
            {
                System.Diagnostics.EventLog.WriteEntry(
                    "Application",
                    string.Format("URL: {0}\nIP: {1}\n\n{2}",
                        Request.Url,
                        Request.Headers["X-RDWR-IP"],
                        ex.ToString()),
                    System.Diagnostics.EventLogEntryType.Error
                );
            }
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
                    //System.IO.File.WriteAllText(@"C:\WebSite\tnpd\tnpd\upfiles\log.txt", "file write");
                    Response.Redirect("/Error/SizeError");
                }
                catch (Exception ex)
                {
                   
                }
            }
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            var app = sender as HttpApplication;
            // remove Server header
            if (app != null)
            {
                app.Context.Response.Headers.Remove("Server");
                app.Context.Response.Headers.Remove("X-AspNet-Version");
                app.Context.Response.Headers.Remove("X-AspNetMvc-Version");

                RemoveDuplicateSetCookie(app.Context.Response);
            }


        }

        /// <summary>
        /// 移除內容完全相同的重複 Set-Cookie 標頭。
        ///
        /// 成因：ASP.NET 在 Session 建立當下就將 Session Cookie 直接寫入回應標頭，
        /// 之後若有其他 Cookie（例如 View 產生的防偽 Token）加入 Response.Cookies 集合，
        /// ReleaseRequestState 階段會把整個集合再同步一次，且不會跳過已輸出過的 Session Cookie，
        /// 導致同一組 Session Cookie 出現兩筆完全相同的 Set-Cookie。
        ///
        /// 兩筆的 name/value/path 相同，瀏覽器解析後仍是同一個 Cookie，
        /// 不會造成 Session 覆蓋；此處僅為避免重複標頭造成的困擾。
        /// </summary>
        private void RemoveDuplicateSetCookie(HttpResponse response)
        {
            string[] setCookies = response.Headers.GetValues("Set-Cookie");
            if (setCookies == null || setCookies.Length < 2)
            {
                return;
            }

            string[] distinct = setCookies.Distinct(StringComparer.Ordinal).ToArray();
            if (distinct.Length == setCookies.Length)
            {
                return;   // 沒有完全相同的重複項，不動作
            }

            response.Headers.Remove("Set-Cookie");
            foreach (string cookie in distinct)
            {
                response.Headers.Add("Set-Cookie", cookie);
            }
        }
    }
}