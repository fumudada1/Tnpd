using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tnpd.Controllers
{
    public class AboutLinkController : Controller
    {
        //
        // GET: /AboutLink/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

    }
}
