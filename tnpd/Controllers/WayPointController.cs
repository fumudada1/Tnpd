using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using TnpdModels;

namespace tnpd.Controllers
{
    public class WayPointController : _BaseController
    {
        //
        // GET: /WayPoint/
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 10;

        public ActionResult Index(Guid id, int? page, FormCollection fc)
        {
            ViewBag.UnId = id.ToString();
            int currentPageIndex = 0;
            //記住搜尋條件
            GetCatcheRoutes(page, fc);
            //取得正確的頁面
            currentPageIndex = getCurrentPage(page, fc);


            //ViewBag.SearchByCategoryId = sClass;




            var wayPoints = _db.WayPoints.AsQueryable();
            

            //ViewBag.Subject = getViewDateStr("SearchBySubject");
            return View(wayPoints.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }

    }
}
