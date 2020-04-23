using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnpd.Models;

namespace tnpd.Controllers
{
    public class CheckWebsiteController : Controller
    {
        //
        // GET: /CheckWebsite/

        public ActionResult Index(string id)
        {
            Utility.SendGmailMail("topidea.justin@gmail.com",id, "臺南市政府警察局-網站異常通知", "目前網站異常，無法正確顯示!!", "xuqoqvdvvsbwyrbl");
            return Content("Success");
        }

    }
}
