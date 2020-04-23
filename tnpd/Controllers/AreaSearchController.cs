using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tnpd.Controllers
{
    public class AreaSearchController : Controller
    {
        //
        // GET: /AreaSearch/

        public ActionResult Index(Guid id)
        {
            ViewBag.UnId = id.ToString();
            return View();
        }

    }
}
