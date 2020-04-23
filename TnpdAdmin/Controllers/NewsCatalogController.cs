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

    public class NewsCatalogController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 100;
        //




        public ActionResult Index(int? page, int pclass, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);
            var newsCatalogs = _db.NewsCatalogs.Where(x => x.WebCategoryId == pclass);
            Member member =
                             _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
                if (webSite == null)
                {
                    webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
                }
                newsCatalogs = newsCatalogs.Where(x => x.WebSiteId == webSite.Id);
            }
            
            WebNewsCatalog catalog = _db.WebNewsCatalogs.Find(pclass);

            ViewBag.Title = catalog.Subject;

            return View(newsCatalogs.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        [AllowAnonymous]
        public ActionResult GetJson()
        {
            var item = _db.WebNewsCatalogs.OrderBy(p => p.ListNum);
            string jsonContent = JsonConvert.SerializeObject(item.ToList(), Newtonsoft.Json.Formatting.Indented);
            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }



        [HttpPost]
        public ActionResult Sort(string sortData, int pclass)
        {
            if (!string.IsNullOrEmpty(sortData))
            {
                string[] tempDatas = sortData.TrimEnd(',').Split(',');
                foreach (string tempData in tempDatas)
                {
                    string[] itemDatas = tempData.Split(':');
                    NewsCatalog newscatalog = _db.NewsCatalogs.Find(Convert.ToInt16(itemDatas[0]));
                    newscatalog.ListNum = Convert.ToInt16(itemDatas[1]);

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }

            return RedirectToAction("Index", new { pclass = pclass });
        }


        //
        // GET: /NewsCatalog/Details/5

        public ActionResult Details(int id = 0)
        {
            NewsCatalog newscatalog = _db.NewsCatalogs.Find(id);
            if (newscatalog == null)
            {
                return HttpNotFound();
            }
            return View(newscatalog);
        }

        //
        // GET: /NewsCatalog/Create

        public ActionResult Create(int pclass)
        {
            ViewBag.pclass = pclass;
            WebNewsCatalog catalog = _db.WebNewsCatalogs.Find(pclass);

            ViewBag.Title = catalog.Subject;

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

        public ActionResult Create(NewsCatalog newscatalog)
        {
            if (ModelState.IsValid)
            {

                int maxListNum = 0;
                if ((_db.NewsCatalogs.Any()))
                {
                    maxListNum = _db.NewsCatalogs.Max(x => x.ListNum);
                }

                newscatalog.ListNum = maxListNum + 1;
                _db.NewsCatalogs.Add(newscatalog);
                newscatalog.Create(_db, _db.NewsCatalogs);

                return RedirectToAction("Index", new { pclass = newscatalog.WebCategoryId });
            }

            return View(newscatalog);
        }

        //
        // GET: /NewsCatalog/Edit/5

        public ActionResult Edit(int pclass,int id = 0)
        {
            NewsCatalog newscatalog = _db.NewsCatalogs.Find(id);
            if (newscatalog == null)
            {
                return HttpNotFound();
            }

            WebNewsCatalog catalog = _db.WebNewsCatalogs.Find(pclass);

            ViewBag.Title = catalog.Subject;

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

        public ActionResult Edit(NewsCatalog newscatalog)
        {
            if (ModelState.IsValid)
            {

                //_db.Entry(newscatalog).State = EntityState.Modified;
                newscatalog.Update();

                return RedirectToAction("Index", new { Page = -1, pclass = newscatalog.WebCategoryId });
            }
            return View(newscatalog);
        }

        //
        // GET: /NewsCatalog/Delete/5

        public ActionResult Delete(int pclass,int id = 0)
        {
            WebNewsCatalog catalog = _db.WebNewsCatalogs.Find(pclass);

            ViewBag.Title = catalog.Subject;
            NewsCatalog newscatalog = _db.NewsCatalogs.Find(id);
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

            NewsCatalog newscatalog = _db.NewsCatalogs.Find(id);
            int pclass = newscatalog.WebCategoryId.Value;
            _db.NewsCatalogs.Remove(newscatalog);
            _db.SaveChanges();

            return RedirectToAction("Index", new { Page = -1, pclass = pclass });
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
