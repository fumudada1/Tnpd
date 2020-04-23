﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using tnpd.Controllers;
using TnpdModels;

namespace tnpd.Areas.baihe.Controllers
{
    public class AboutLinkController : _BaseController
    {

        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 10;
        //
        // GET: /About/

        public ActionResult Index(int? page, FormCollection fc)
        {
            string areaName = ControllerContext.RouteData.DataTokens["area"].ToString();

            int currentPageIndex = 0;

            //記住搜尋條件
            GetCatcheRoutes(page, fc);
            //取得正確的頁面
            currentPageIndex = getCurrentPage(page, fc);


            //ViewBag.SearchByCategoryId = sClass;




            var aboutLinks = _db.AboutLinks.Where(x => x.Catalog.WebSite.SiteCode==areaName).OrderByDescending(p => p.InitDate).AsQueryable();

            return View(aboutLinks.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }

    }
}