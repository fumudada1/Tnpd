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
    public class AboutLinkController : _BaseController
    {

        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 10;
        //
        // GET: /About/

        public ActionResult Index(Guid id, int? page, FormCollection fc)
        {
            ViewBag.UnId = id.ToString();
            string areaName = ControllerContext.RouteData.DataTokens["area"].ToString();

            int currentPageIndex = 0;

            //記住搜尋條件
            GetCatcheRoutes(page, fc);
            //取得正確的頁面
            currentPageIndex = getCurrentPage(page, fc);


            //ViewBag.SearchByCategoryId = sClass;


            var aboutLinks = _db.AboutLinks.Where(x => x.Catalog.WebSite.SiteCode == areaName && x.Status == BooleanType.是).AsQueryable();

            return View(aboutLinks.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }

    }
}
