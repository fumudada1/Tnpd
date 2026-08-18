using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TnpdModels;

namespace tnpd.Areas.cic.Controllers
{
    public class LeaderController : Controller
    {
        //
        // GET: /cic/Leader/
        private BackendContext _db = new BackendContext();
        //
        // GET: /Leader/

        public ActionResult Index(Guid id, int lclass)
        {
            ViewBag.UnId = id.ToString();
            LeaderCatalog catalog = _db.LeaderCatalogs.Where(x => x.Id == lclass).FirstOrDefault();
            if (catalog == null)
            {
                return HttpNotFound();
            }

            return View(catalog);
        }
    }
}
