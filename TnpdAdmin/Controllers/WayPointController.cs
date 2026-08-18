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
    public class WayPointController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 15;
        //

        


        public ActionResult Index(int? page, FormCollection fc )
        {
			//記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);
            

            var waypoints = _db.WayPoints.OrderByDescending(p => p.InitDate).AsQueryable();
            if (hasViewData("SearchBySubject"))
            {
                string searchBySubject = getViewDateStr("SearchBySubject");
                waypoints = waypoints.Where(w => w.Address.Contains(searchBySubject));
            } 

            return View(waypoints.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }



        

        //
        // GET: /WayPoint/Details/5

        public ActionResult Details(int id = 0)
        {
            WayPoint waypoint = _db.WayPoints.Find(id);
            if (waypoint == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(waypoint);
        }

        //
        // GET: /WayPoint/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WayPoint/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(WayPoint waypoint )
        {
            if (ModelState.IsValid)
            {

                _db.WayPoints.Add(waypoint);
                waypoint.Create(_db,_db.WayPoints);
                return RedirectToAction("Index");
            }

            return View(waypoint);
        }

        //
        // GET: /WayPoint/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WayPoint waypoint = _db.WayPoints.Find(id);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            return View(waypoint);
        }

        //
        // POST: /WayPoint/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(WayPoint waypoint)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(waypoint).State = EntityState.Modified;
                waypoint.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            return View(waypoint);
        }

        //
        // GET: /WayPoint/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WayPoint waypoint = _db.WayPoints.Find(id);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            return View(waypoint);
        }

        //
        // POST: /WayPoint/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WayPoint waypoint = _db.WayPoints.Find(id);
            _db.WayPoints.Remove(waypoint);
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
