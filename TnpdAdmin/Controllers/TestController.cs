using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tnpd.Models;

namespace TnpdAdmin.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Index(string myVal)
        {
            bool status = Utility.IsFontSizePxpt(myVal);
            if (status)
            {
                ViewBag.message = "have font";
            }
            else
            {
                ViewBag.message = "no font";
            }

            return View();
        }

    }
}
