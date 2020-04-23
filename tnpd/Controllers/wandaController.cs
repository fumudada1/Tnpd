using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tnpd.Controllers
{
    public class wandaController : Controller
    {
        //
        // GET: /wanda/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

    }
}
