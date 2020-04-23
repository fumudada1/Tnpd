using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using Tnpd.Filters;
using Tnpd.Helpers.DyLinq;
using TnpdModels;


namespace Tnpd.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class EseController : _BaseController
    {
        private BackendContext db = new BackendContext();
      

        private const int DefaultPageSize = 20;
        //


        public ActionResult Index(int? page, FormCollection fc)
        {

         
            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language==LanguageType.中文版);
            if (webSite==null)
            {
                webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }
            ViewBag.webSiteID = webSite.Id;
         

            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var newses = db.Eses.OrderByDescending(x=>x.InitDate).AsQueryable();

            var newsCatalogs = db.EseCatalogs.AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                newsCatalogs = newsCatalogs.Where(x => x.WebSiteId == webSite.Id);
                newses = newses.Where(w => w.Catalogs.Count(x=>x.WebSiteId == webSite.Id)>0);
                //newses = newses.Where(w => w.OwnWebSiteId==member.MyUnit.ParentId);
            }
          
            var newsCatalogs1 = newsCatalogs.ToList().Select(x => new
            {
                Id = x.Id,
                webid = x.WebSiteId,
                Subject = x.WebSite.Subject + "-" + x.Subject
            }).OrderBy(x => x.webid);

            ViewBag.CategoryId = new SelectList(newsCatalogs1, "Id", "Subject");


          
            //ViewBag.CategoryName = db.NewsCatalogs.FirstOrDefault(x => x.Id == CategoryId).Subject;
            if (hasViewData("SearchBySubject"))
            {
                string searchByTitle = getViewDateStr("SearchBySubject");
                newses = newses.Where(w => w.Subject.Contains(searchByTitle));
            }

            int siteID = 1;
            if (hasViewData("SearchBySite"))
            {
                if (getViewDateInt("SearchBySite") != 0)
                {
                    siteID = Convert.ToInt16(getViewDateStr("SearchBySite"));
                    newses = newses.Where(w => w.Catalogs.Count(x => x.WebSiteId == siteID) > 0);
                }

            }
          

            if (hasViewData("SearchByCategoryId"))
            {
                int categoryId = getViewDateInt("SearchByCategoryId");
                newses = newses.Where(x => x.Catalogs.Count(y => y.Id == categoryId)>0);
            }
           

            if (hasViewData("SearchByStartDate") && hasViewData("SearchByEndDate"))
            {
                DateTime startDate = Convert.ToDateTime(getViewDateStr("SearchByStartDate"));
                DateTime endDate = Convert.ToDateTime(getViewDateStr("SearchByEndDate")).AddDays(1);
                newses = newses.Where(w => w.InitDate >= startDate && w.InitDate <= endDate);
            }
            string SearchBySortField = "initDate";     //預設的排序欄位
            if (hasViewData("SearchBySortField"))
            {
                SearchBySortField = getViewDateStr("SearchBySortField");
            }
            string SearchBySortType = "Desc";
            if (hasViewData("SearchBySortType"))        //預設的排序方式
            {
                SearchBySortType = getViewDateStr("SearchBySortType");
            }

            var ls2 = DynamicLinqExpressions.OrderBy(newses.AsQueryable(), SearchBySortField, SearchBySortType);


            //ViewBag.SearchBySite = new SelectList(webSiteNames, "Id", "Subject", siteID);




            ViewBag.Title = "申辦服務";

            //ViewBag.Subject = getViewDateStr("SearchBySubject");
            return View(ls2.ToPagedList(currentPageIndex, DefaultPageSize));
        }

      
        // GET: /News/Details/5

        public ActionResult Details(int id = 0)
        {
            Ese news = db.Eses.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // GET: /News/Create

        public ActionResult Create()
        {



          
            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }

            var newsCatalogs = db.EseCatalogs.AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                newsCatalogs = newsCatalogs.Where(x => x.WebSiteId == webSite.Id);
            }
            var newsCatalogs1 = newsCatalogs.OrderBy(x=>x.WebSiteId).ToList();

            ViewBag.newsCatalogs = newsCatalogs1;

            ViewBag.Title = "申辦服務";
ViewBag.SiteCode = webSite.SiteCode;
            //ViewBag.WebSiteNameId = siteID;
            Ese news = new Ese();
         


            return View(news);
        }

        //
        // POST: /News/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Ese news, List<EseFile> newsFiles, int[] newsCatalogID)
        {
            ModelState.Remove("Catalogs");
            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }


            if (ModelState.IsValid)
            {
             
               
                foreach (var newsFile in newsFiles)
                {
                    if (!string.IsNullOrEmpty(newsFile.Subject) && !string.IsNullOrEmpty(newsFile.UpFile))
                        news.Fileses.Add(newsFile);
                }
                
              

                news.InitDate = DateTime.Now;
                news.Poster = member.Name;
                news.initOrg = member.MyUnit.ParentUnit.Subject + " " + member.MyUnit.Subject;
                news.OwnWebSiteId = member.MyUnit.ParentId.Value;
                List<EseCatalog> catalogs = db.EseCatalogs.Where(x => newsCatalogID.Contains(x.Id)).ToList();
                news.Catalogs=new List<EseCatalog>();
                foreach (var catalog in catalogs)
                {
                    news.Catalogs.Add(catalog);
                }


                db.Eses.Add(news);
                //news.Create(db,db.Newses); db.Newses.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            var newsCatalog = db.EseCatalogs.AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                newsCatalog = newsCatalog.Where(x => x.WebSiteId == webSite.Id);
            }
            var newsCatalogs1 = newsCatalog.OrderBy(x => x.WebSiteId).ToList();

            ViewBag.newsCatalogs = newsCatalogs1;

            ViewBag.Title = "申辦服務";

           ViewBag.SiteCode = webSite.SiteCode;

             
            return View(news);
        }

        //
        // GET: /News/Edit/5

        public ActionResult Edit(int id = 0, int type = 0)
        {
          
            Ese news = db.Eses.Find(id);
           
            if (news == null)
            {
                return HttpNotFound();
            }

            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }

            var newsCatalogs = db.EseCatalogs.AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                newsCatalogs = newsCatalogs.Where(x => x.WebSiteId == webSite.Id);
            }
            var newsCatalogs1 = newsCatalogs.OrderBy(x => x.WebSiteId).ToList();

            ViewBag.newsCatalogs = newsCatalogs1;
ViewBag.SiteCode = webSite.SiteCode;
            ViewBag.Title = "申辦服務";
            ViewBag.type = type;
            return View(news);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Ese news, int[] newsCatalogID)
        {
            ModelState.Remove("Catalog");
            if (ModelState.IsValid)
            {
                //取得資料庫裏面原來的值
                var newsItem = db.Eses.Find(news.Id);

                //套用新的值
                db.Entry(newsItem).CurrentValues.SetValues(news);


                //放入新的值
                newsItem.Catalogs.Clear();

                Member member =
                    db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
             

                //db.Entry(news).State = EntityState.Modified;
                news.UpdateDate = DateTime.Now;
                news.Updater = member.Name;
                news.UpdateOrg = member.MyUnit.ParentUnit.Subject + " " + member.MyUnit.Subject;
                List<EseCatalog> catalogs = db.EseCatalogs.Where(x => newsCatalogID.Contains(x.Id)).ToList();
                foreach (var catalog in catalogs)
                {
                    newsItem.Catalogs.Add(catalog);
                }

                db.Entry(newsItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { Page = -1 });
            }

            return View(news);
        }

        //
        // GET: /News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ViewBag.Title = "申辦服務";

            Ese news = db.Eses.Find(id);

            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ese news = db.Eses.Find(id);
          
            for (int i = news.Fileses.Count - 1; i == 0; i--)
            {
                var item = news.Fileses.ElementAt(i);
                db.EseFiles.Remove(item);
            }
           


            db.Eses.Remove(news);
            db.SaveChanges();

            return RedirectToAction("Index", new { Page = -1});
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

       

       
    }

}
