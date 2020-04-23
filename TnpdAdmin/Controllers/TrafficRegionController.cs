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
    public class TrafficRegionController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 550;
        //

        


        public ActionResult Index(int? page, FormCollection fc )
        {
			//記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);
            

            var trafficregions = _db.TrafficRegions.Include(t => t.assignUnit).OrderByDescending(p => p.ListNum).AsQueryable();
            ViewBag.UnitId = new SelectList(_db.Units.OrderBy(p=>p.ListNum), "Id", "Subject");
            if (hasViewData("SearchBySubject")) 
            { 
            string searchBySubject = getViewDateStr("SearchBySubject");             
                trafficregions = trafficregions.Where(w => w.Subject.Contains(searchBySubject)); 
            } 
 
            if (hasViewData("SearchByUnitId")) 
            { 
            int searchByUnitId = getViewDateInt("SearchByUnitId");             
                trafficregions = trafficregions.Where(w => w.UnitId == searchByUnitId); 
            } 

//            ViewBag.Subject = Subject;
            return View(trafficregions.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

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
                    TrafficRegion trafficregion = _db.TrafficRegions.Find(Convert.ToInt16(itemDatas[0]));
                    trafficregion.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /TrafficRegion/Details/5

        public ActionResult Details(int id = 0)
        {
            TrafficRegion trafficregion = _db.TrafficRegions.Find(id);
            if (trafficregion == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(trafficregion);
        }

        //
        // GET: /TrafficRegion/Create

        public ActionResult Create()
        {
            ViewBag.UnitId = new SelectList(_db.Units.Where(x=>x.ParentId==null && x.Id!=1).OrderBy(p=>p.ListNum), "Id", "Subject");
            return View();
        }

        //
        // POST: /TrafficRegion/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(TrafficRegion trafficregion )
        {
            if (ModelState.IsValid)
            {

                _db.TrafficRegions.Add(trafficregion);
	  int maxListNum = 0;
      if (( _db.TrafficRegions.Any()))
      {
		 maxListNum = _db.TrafficRegions.Max(x => x.ListNum) ;
      }                
				trafficregion.ListNum = maxListNum +1; 
                trafficregion.Create(_db,_db.TrafficRegions);
                return RedirectToAction("Index");
            }

            ViewBag.UnitId = new SelectList(_db.Units.Where(x => x.ParentId == null && x.Id != 1).OrderBy(p => p.ListNum), "Id", "Subject");
            return View(trafficregion);
        }

        //
        // GET: /TrafficRegion/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TrafficRegion trafficregion = _db.TrafficRegions.Find(id);
            if (trafficregion == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnitId = new SelectList(_db.Units.Where(x => x.ParentId == null && x.Id != 1).OrderBy(p => p.ListNum), "Id", "Subject");
            return View(trafficregion);
        }

        //
        // POST: /TrafficRegion/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(TrafficRegion trafficregion)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(trafficregion).State = EntityState.Modified;
                trafficregion.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            ViewBag.UnitId = new SelectList(_db.Units.Where(x => x.ParentId == null && x.Id != 1).OrderBy(p => p.ListNum), "Id", "Subject");
            return View(trafficregion);
        }

        //
        // GET: /TrafficRegion/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TrafficRegion trafficregion = _db.TrafficRegions.Find(id);
            if (trafficregion == null)
            {
                return HttpNotFound();
            }
            return View(trafficregion);
        }

        //
        // POST: /TrafficRegion/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrafficRegion trafficregion = _db.TrafficRegions.Find(id);
            _db.TrafficRegions.Remove(trafficregion);
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
