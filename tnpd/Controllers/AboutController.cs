using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using TnpdModels;

namespace tnpd.Controllers
{
    public class AboutController : _BaseController
    {
    
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 10;
        //
        // GET: /About/

        public ActionResult Index(int? page, FormCollection fc)
        {
            int currentPageIndex = 0;

            //記住搜尋條件
            GetCatcheRoutes(page, fc);
            //取得正確的頁面
            currentPageIndex = getCurrentPage(page, fc);


            //ViewBag.SearchByCategoryId = sClass;




            var aboutLinks = _db.AboutLinks.Where(x => x.Catalog.WebSiteId == 1 && x.Status==BooleanType.是).AsQueryable();

            return View(aboutLinks.OrderBy(p => p.Catalog.ListNum).ThenBy(x=>x.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }

    }
}
