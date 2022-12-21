using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using tnpd.Controllers;
using TnpdModels;

namespace tnpd.Areas.wpb.Controllers
{
    public class WandaController : _BaseController
    {

        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 10;

        //
        // GET: /About/

        public ActionResult Index(Guid id, int? page, FormCollection fc)
        {
            int currentPageIndex = 0;
            ViewBag.UnId = id.ToString();
            //記住搜尋條件
            GetCatcheRoutes(page, fc);
            //取得正確的頁面
            currentPageIndex = getCurrentPage(page, fc);

            var wandas = _db.Wandas.Where(x => x.Status == BooleanType.是).OrderByDescending(p => p.InitDate).AsQueryable();

            ViewBag.Title = "各區近半年婦幼安全警示地點";
            return View(wandas.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }
        public ActionResult Details(string unid, int id = 0)
        {
            ViewBag.UnId = unid;

            Wanda wanda = _db.Wandas.Find(id);
            if (wanda == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "各區近半年婦幼安全警示地點";

            return View(wanda);
        }
    }
}
