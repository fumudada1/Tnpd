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
    public class LeaderCatalogController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 300;
        //

        


        public ActionResult Index(int? page, FormCollection fc )
        {
			//記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);
            

            var leadercatalogs = _db.LeaderCatalogs.Include(l => l.WebSite).OrderByDescending(p => p.ListNum).AsQueryable();
            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p=>p.ListNum), "Id", "Subject");
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
                if (webSite == null)
                {
                    webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
                }
                leadercatalogs = leadercatalogs.Where(w => w.WebSiteId == webSite.Id); 
            }
            

//            ViewBag.Subject = Subject;
            return View(leadercatalogs.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

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
                    LeaderCatalog leadercatalog = _db.LeaderCatalogs.Find(Convert.ToInt16(itemDatas[0]));
                    leadercatalog.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /LeaderCatalog/Details/5

        public ActionResult Details(int id = 0)
        {
            LeaderCatalog leadercatalog = _db.LeaderCatalogs.Find(id);
            if (leadercatalog == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(leadercatalog);
        }

        //
        // GET: /LeaderCatalog/Create

        public ActionResult Create()
        {
            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p=>p.ListNum), "Id", "Subject");
            return View();
        }

        //
        // POST: /LeaderCatalog/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(LeaderCatalog leadercatalog )
        {
            if (ModelState.IsValid)
            {

                _db.LeaderCatalogs.Add(leadercatalog);
	  int maxListNum = 0;
      if (( _db.LeaderCatalogs.Any()))
      {
		 maxListNum = _db.LeaderCatalogs.Max(x => x.ListNum) ;
      }                
				leadercatalog.ListNum = maxListNum +1; 
                leadercatalog.Create(_db,_db.LeaderCatalogs);
                return RedirectToAction("Index");
            }

            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p=>p.ListNum), "Id", "Subject", leadercatalog.WebSiteId);
            return View(leadercatalog);
        }

        //
        // GET: /LeaderCatalog/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LeaderCatalog leadercatalog = _db.LeaderCatalogs.Find(id);
            if (leadercatalog == null)
            {
                return HttpNotFound();
            }
            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p=>p.ListNum), "Id", "Subject", leadercatalog.WebSiteId);
            return View(leadercatalog);
        }

        //
        // POST: /LeaderCatalog/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(LeaderCatalog leadercatalog)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(leadercatalog).State = EntityState.Modified;
                leadercatalog.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p=>p.ListNum), "Id", "Subject", leadercatalog.WebSiteId);
            return View(leadercatalog);
        }

        //
        // GET: /LeaderCatalog/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LeaderCatalog leadercatalog = _db.LeaderCatalogs.Find(id);
            if (leadercatalog == null)
            {
                return HttpNotFound();
            }
            return View(leadercatalog);
        }

        //
        // POST: /LeaderCatalog/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaderCatalog leadercatalog = _db.LeaderCatalogs.Find(id);
            _db.LeaderCatalogs.Remove(leadercatalog);
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
