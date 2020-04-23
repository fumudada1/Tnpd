using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using Tnpd.Controllers;
using Tnpd.Filters;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
	[PermissionFilters]
    [Authorize]
    public class AboutLinkCatalogController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 100;
        //

        


        public ActionResult Index(int? page, FormCollection fc )
        {
			//記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var aboutlinkcatalogs = _db.AboutLinkCatalogs.Include(a => a.WebSite).OrderByDescending(p => p.ListNum).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
                if (webSite == null)
                {
                    webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
                }
                aboutlinkcatalogs = aboutlinkcatalogs.Where(x => x.WebSiteId == webSite.Id);
            }



           
            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p=>p.ListNum), "Id", "Subject");
           

//            ViewBag.Subject = Subject;
            return View(aboutlinkcatalogs.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

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
                    AboutLinkCatalog aboutlinkcatalog = _db.AboutLinkCatalogs.Find(Convert.ToInt16(itemDatas[0]));
                    aboutlinkcatalog.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /AboutLinkCatalog/Details/5

        public ActionResult Details(int id = 0)
        {
            AboutLinkCatalog aboutlinkcatalog = _db.AboutLinkCatalogs.Find(id);
            if (aboutlinkcatalog == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(aboutlinkcatalog);
        }

        //
        // GET: /AboutLinkCatalog/Create

        public ActionResult Create()
        {
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.WebSiteId =
                    new SelectList(
                        _db.WebSiteNames.Where((x => x.UnitId == member.MyUnit.ParentId)).OrderBy(p => p.ListNum), "Id",
                        "Subject");
            }
            else
            {

                ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p => p.ListNum), "Id", "Subject");
            }
            return View();
        }

        //
        // POST: /AboutLinkCatalog/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(AboutLinkCatalog aboutlinkcatalog )
        {
            if (ModelState.IsValid)
            {

                _db.AboutLinkCatalogs.Add(aboutlinkcatalog);
	  int maxListNum = 0;
      if (( _db.AboutLinkCatalogs.Any()))
      {
		 maxListNum = _db.AboutLinkCatalogs.Max(x => x.ListNum) ;
      }                
				aboutlinkcatalog.ListNum = maxListNum +1; 
                aboutlinkcatalog.Create(_db,_db.AboutLinkCatalogs);
                return RedirectToAction("Index");
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.WebSiteId =
                    new SelectList(
                        _db.WebSiteNames.Where((x => x.UnitId == member.MyUnit.ParentId)).OrderBy(p => p.ListNum), "Id",
                        "Subject");
            }
            else
            {

                ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p => p.ListNum), "Id", "Subject");
            }


            return View(aboutlinkcatalog);
        }

        //
        // GET: /AboutLinkCatalog/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AboutLinkCatalog aboutlinkcatalog = _db.AboutLinkCatalogs.Find(id);
            if (aboutlinkcatalog == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.WebSiteId =
                    new SelectList(
                        _db.WebSiteNames.Where((x => x.UnitId == member.MyUnit.ParentId)).OrderBy(p => p.ListNum), "Id",
                        "Subject", aboutlinkcatalog.WebSiteId);
            }
            else
            {

                ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p => p.ListNum), "Id", "Subject", aboutlinkcatalog.WebSiteId);
            }

            
            return View(aboutlinkcatalog);
        }

        //
        // POST: /AboutLinkCatalog/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(AboutLinkCatalog aboutlinkcatalog)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(aboutlinkcatalog).State = EntityState.Modified;
                aboutlinkcatalog.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.WebSiteId =
                    new SelectList(
                        _db.WebSiteNames.Where((x => x.UnitId == member.MyUnit.ParentId)).OrderBy(p => p.ListNum), "Id",
                        "Subject", aboutlinkcatalog.WebSiteId);
            }
            else
            {

                ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p => p.ListNum), "Id", "Subject", aboutlinkcatalog.WebSiteId);
            }
            return View(aboutlinkcatalog);
        }

        //
        // GET: /AboutLinkCatalog/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AboutLinkCatalog aboutlinkcatalog = _db.AboutLinkCatalogs.Find(id);
            if (aboutlinkcatalog == null)
            {
                return HttpNotFound();
            }
            return View(aboutlinkcatalog);
        }

        //
        // POST: /AboutLinkCatalog/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutLinkCatalog aboutlinkcatalog = _db.AboutLinkCatalogs.Find(id);
            _db.AboutLinkCatalogs.Remove(aboutlinkcatalog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
