using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using tnpd.Controllers;
using tnpd.Filters;
using TnpdModels;
using MvcPaging;
using Newtonsoft.Json;
using tnpd.Models;

namespace tnpd.Controllers
{
    public class NewsController : _BaseController
    {
        //
        // GET: /News/
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 10;

        [MyAuthorizeAttribute]
        public ActionResult Index(Guid id, int? sClass, int? page, FormCollection fc)
        {

            ViewBag.UnId = id.ToString();
            int currentPageIndex = 0;
            if (sClass.HasValue && sClass != 0)
            {
                fc["SearchByCategoryId"] = sClass.ToString();
                //記住搜尋條件
                GetCatcheRoutes(page, fc, null, false);
                //取得正確的頁面
                currentPageIndex = getCurrentPage(page, fc, null, false);
            }
            else
            {
                //記住搜尋條件
                GetCatcheRoutes(page, fc);
                //取得正確的頁面
                currentPageIndex = getCurrentPage(page, fc);
            }


            //ViewBag.SearchByCategoryId = sClass;




            var newses = _db.Newses.Where(x => x.NewsCatalogs.Count(y => y.WebCategoryId == sClass && y.WebSiteId == 1) > 0).OrderByDescending(p => p.StartDate).AsQueryable();
            DateTime yesterDay = DateTime.Now.AddDays(-1);
            newses = newses.Where(w => w.StartDate <= DateTime.Now && w.EndDate >= yesterDay && w.IsConfirm == BooleanType.是);


            //ViewBag.CategoryId = new SelectList(_db.NewsCatalogs.Where(x=>x.WebSiteId==1 && x.WebCategoryId==sClass).OrderBy(p => p.ListNum), "Id", "Subject");
            ViewBag.CategoryId = sClass;

            var CategoryCount = _db.NewsCatalogs.Count(x => x.WebCategoryId == sClass && x.WebSiteId == 1);
            ViewBag.CategoryCount = CategoryCount;

            //ViewBag.Subject = getViewDateStr("SearchBySubject");
            return View(newses.OrderByDescending(p => p.StartDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        public ActionResult Json()
        {



            var newses = _db.Newses.Where(x => x.NewsCatalogs.Count(y => y.WebCategoryId == 1 && y.WebSiteId == 1) > 0).OrderByDescending(p => p.StartDate).AsQueryable();
            DateTime yesterDay = DateTime.Now.AddDays(-1);
            newses = newses.Where(w => w.StartDate <= DateTime.Now && w.EndDate >= yesterDay && w.IsConfirm == BooleanType.是);
            var newsList = newses.ToList().Select(
                c => new
                {
                    Subject = c.Subject,
                    Article = c.Article,
                    InitDate = Convert.ToDateTime(c.UpdateDate).ToString("yyyy-MM-dd HH:mm"),
                    initOrg = c.initOrg,
                    URL = "https://www.tnpd.gov.tw/News/Details/a71f1b44-1f96-8a48-caa5-fe08e13047ee/1/" + c.Id
                }
            );




            var jsonContent = JsonConvert.SerializeObject(newsList, Formatting.Indented);

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };

        }


        public ActionResult Details(string unid, int sClass, int id = 0)
        {
            ViewBag.UnId = unid;
            ViewBag.sClass = sClass;
            News news = _db.Newses.Find(id);
            if (news == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (news.IsConfirm == BooleanType.否)
            {
                return RedirectToAction("Index", "Home");
            }
            DateTime yesterDay = DateTime.Now.AddDays(-1);

            if (news.StartDate <= DateTime.Now && news.EndDate >= yesterDay)
            {
                news.Views = news.Views + 1;
                news.Update(_db, _db.Newses);

                NewsCatalog catalog = news.NewsCatalogs.FirstOrDefault();
                ViewBag.Subject = catalog.Subject;
                ViewBag.Theme = catalog.MetaIndex.Theme;
                ViewBag.Cake = catalog.MetaIndex.Cake;
                ViewBag.Service = catalog.MetaIndex.Service;


                return View(news);
            }
            return RedirectToAction("Index", "Home");

        }



        /// <summary>
        /// 取得網站內容JSON
        /// </summary>
        /// <param name="id">網站編號</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetJson(int id)
        {
            if (id == null)
            {
                return new ContentResult { Content = "", ContentType = "application/text" };
            }
            var newses = _db.Newses.Where(x => x.NewsCatalogs.Count(y => y.WebSiteId == 1) > 0 && x.NewsCatalogs.Count(y => y.WebCategoryId == id) > 0).OrderByDescending(p => p.StartDate).AsQueryable();
            DateTime yesterDay = DateTime.Now.AddDays(-1);
            newses = newses.Where(w => w.StartDate <= DateTime.Now && w.EndDate >= yesterDay && w.IsConfirm == BooleanType.是);
            var item = newses.Select(
                           c => new
                           {
                               Id = c.Id,
                               Category = c.NewsCatalogs.FirstOrDefault().Subject,
                               Subject = c.Subject,
                               Article = c.Article,
                               CreateDate = c.StartDate
                           }
                   ).Take(10);

            string jsonContent = JsonConvert.SerializeObject(item.ToList(), Newtonsoft.Json.Formatting.Indented);
            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }

        public ActionResult check(int ip)
        {
            Session["PerView"] = ip;
            return Content("");
        }

        public ActionResult PerView(int id = 0)
        {

            News news = _db.Newses.Find(id);
            if (news == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //if (Session["PerView"] == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            news.Views = news.Views + 1;
            news.Update(_db, _db.Newses);

            NewsCatalog catalog = news.NewsCatalogs.FirstOrDefault();
            ViewBag.Subject = catalog.Subject;
            ViewBag.Title = catalog.Subject;
            ViewBag.Theme = catalog.MetaIndex.Theme;
            ViewBag.Cake = catalog.MetaIndex.Cake;
            ViewBag.Service = catalog.MetaIndex.Service;


            return View(news);
        }

    }
}
