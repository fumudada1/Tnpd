using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using Tnpd.Filters;
using TnpdModels;
using Newtonsoft.Json;

namespace Tnpd.Controllers
{
    [PermissionFilters]
    [Authorize]

    public class EseCatalogController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 100;
        //




        public ActionResult Index(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);
            var escCatalogs = _db.EseCatalogs.AsQueryable();
            Member member =
                             _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
                if (webSite == null)
                {
                    webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
                }
                escCatalogs = escCatalogs.Where(x => x.WebSiteId == webSite.Id);
            }



            ViewBag.Title = "申辦服務";

            return View(escCatalogs.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        [AllowAnonymous]
        public ActionResult GetJson()
        {
            var item = _db.WebNewsCatalogs.OrderBy(p => p.ListNum);
            string jsonContent = JsonConvert.SerializeObject(item.ToList(), Newtonsoft.Json.Formatting.Indented);
            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }



        [HttpPost]
        public ActionResult Sort(string sortData)
        {
            if (!string.IsNullOrEmpty(sortData))
            {
                string[] tempDatas = sortData.TrimEnd(',').Split(',');
                foreach (string tempData in tempDatas)
                {
                    string[] itemDatas = tempData.Split(':');
                    EseCatalog newscatalog = _db.EseCatalogs.Find(Convert.ToInt16(itemDatas[0]));
                    newscatalog.ListNum = Convert.ToInt16(itemDatas[1]);

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }

            return RedirectToAction("Index");
        }


        //
        // GET: /NewsCatalog/Details/5

        public ActionResult Details(int id = 0)
        {
            EseCatalog esecatalog = _db.EseCatalogs.Find(id);
            if (esecatalog == null)
            {
                return HttpNotFound();
            }
            return View(esecatalog);
        }

        //
        // GET: /NewsCatalog/Create

        public ActionResult Create()
        {


            ViewBag.Title = "申辦服務";

            ViewBag.MetaId = new SelectList(_db.MetaIndices.OrderBy(p => p.ListNum), "Id", "Subject");
            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p => p.ListNum), "Id", "Subject");
            Member member =
                             _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
            }
            ViewBag.webSite = webSite;
            ViewBag.admin = false;
            if (member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.admin = true;
            }

            return View();
        }

        //
        // POST: /NewsCatalog/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(EseCatalog newscatalog)
        {
            if (ModelState.IsValid)
            {

                int maxListNum = 0;
                if ((_db.NewsCatalogs.Any()))
                {
                    maxListNum = _db.NewsCatalogs.Max(x => x.ListNum);
                }

                newscatalog.ListNum = maxListNum + 1;
                _db.EseCatalogs.Add(newscatalog);
                newscatalog.Create(_db, _db.EseCatalogs);

                return RedirectToAction("Index");
            }

            return View(newscatalog);
        }

        //
        // GET: /NewsCatalog/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EseCatalog newscatalog = _db.EseCatalogs.Find(id);
            if (newscatalog == null)
            {
                return HttpNotFound();
            }



            ViewBag.Title = "申辦服務";

            ViewBag.MetaId = new SelectList(_db.MetaIndices.OrderBy(p => p.ListNum), "Id", "Subject", newscatalog.MetaId);
            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p => p.ListNum), "Id", "Subject", newscatalog.WebSiteId);
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
            }
            ViewBag.webSite = webSite;
            ViewBag.admin = false;
            if (member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.admin = true;
            }
            return View(newscatalog);
        }

        //
        // POST: /NewsCatalog/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(EseCatalog newscatalog)
        {
            if (ModelState.IsValid)
            {

                //_db.Entry(newscatalog).State = EntityState.Modified;
                newscatalog.Update();

                return RedirectToAction("Index", new { Page = -1});
            }
            return View(newscatalog);
        }

        //
        // GET: /NewsCatalog/Delete/5

        public ActionResult Delete(int id = 0)
        {


            ViewBag.Title = "申辦服務";
            EseCatalog newscatalog = _db.EseCatalogs.Find(id);
            if (newscatalog == null)
            {
                return HttpNotFound();
            }
            return View(newscatalog);
        }

        //
        // POST: /NewsCatalog/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            EseCatalog newscatalog = _db.EseCatalogs.Find(id);

            _db.EseCatalogs.Remove(newscatalog);
            _db.SaveChanges();

            return RedirectToAction("Index", new { Page = -1 });
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
