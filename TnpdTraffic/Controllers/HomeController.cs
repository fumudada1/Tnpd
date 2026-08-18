using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TnpdTraffic.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return Redirect("https://tr.tnpd.gov.tw/TrafficMailbox/Index/92929b01-cf5e-99d6-2539-bee668350a6d");
            //return View();
        }

    }
}
