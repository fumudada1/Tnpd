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
    public class TrafficRoaddataController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 500;
        //

        


        public ActionResult Index(int? page, FormCollection fc )
        {
			//記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);
            

            var trafficroaddatas = _db.TrafficRoaddatas.Include(t => t.Region).OrderByDescending(p => p.ListNum).AsQueryable();
            var trafficRegions = _db.TrafficRegions.OrderBy(p => p.ListNum);
            ViewBag.RegionId = new SelectList(_db.TrafficRegions.OrderBy(p=>p.ListNum), "Id", "Subject");
            if (hasViewData("SearchByRegionId"))
            {
                int searchByRegionId = getViewDateInt("SearchByRegionId");
                trafficroaddatas = trafficroaddatas.Where(w => w.RegionId == searchByRegionId);
            }
            else
            {
                trafficroaddatas = trafficroaddatas.Where(w => w.RegionId == trafficRegions.FirstOrDefault().Id);
            }


//            ViewBag.Subject = Subject;
            return View(trafficroaddatas.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

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
                    TrafficRoaddata trafficroaddata = _db.TrafficRoaddatas.Find(Convert.ToInt16(itemDatas[0]));
                    trafficroaddata.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /TrafficRoaddata/Details/5

        public ActionResult Details(int id = 0)
        {
            TrafficRoaddata trafficroaddata = _db.TrafficRoaddatas.Find(id);
            if (trafficroaddata == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(trafficroaddata);
        }

        //
        // GET: /TrafficRoaddata/Create

        public ActionResult Create()
        {
            ViewBag.RegionId = new SelectList(_db.TrafficRegions.OrderBy(p=>p.ListNum), "Id", "Subject");
            return View();
        }

        //
        // POST: /TrafficRoaddata/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(TrafficRoaddata trafficroaddata )
        {
            if (ModelState.IsValid)
            {

                _db.TrafficRoaddatas.Add(trafficroaddata);
	  int maxListNum = 0;
      if (( _db.TrafficRoaddatas.Any()))
      {
		 maxListNum = _db.TrafficRoaddatas.Max(x => x.ListNum) ;
      }                
				trafficroaddata.ListNum = maxListNum +1; 
                trafficroaddata.Create(_db,_db.TrafficRoaddatas);
                return RedirectToAction("Index");
            }

            ViewBag.RegionId = new SelectList(_db.TrafficRegions.OrderBy(p=>p.ListNum), "Id", "Subject", trafficroaddata.RegionId);
            return View(trafficroaddata);
        }

        //
        // GET: /TrafficRoaddata/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TrafficRoaddata trafficroaddata = _db.TrafficRoaddatas.Find(id);
            if (trafficroaddata == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionId = new SelectList(_db.TrafficRegions.OrderBy(p=>p.ListNum), "Id", "Subject", trafficroaddata.RegionId);
            return View(trafficroaddata);
        }

        //
        // POST: /TrafficRoaddata/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(TrafficRoaddata trafficroaddata)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(trafficroaddata).State = EntityState.Modified;
                trafficroaddata.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            ViewBag.RegionId = new SelectList(_db.TrafficRegions.OrderBy(p=>p.ListNum), "Id", "Subject", trafficroaddata.RegionId);
            return View(trafficroaddata);
        }

        //
        // GET: /TrafficRoaddata/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TrafficRoaddata trafficroaddata = _db.TrafficRoaddatas.Find(id);
            if (trafficroaddata == null)
            {
                return HttpNotFound();
            }
            return View(trafficroaddata);
        }

        //
        // POST: /TrafficRoaddata/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrafficRoaddata trafficroaddata = _db.TrafficRoaddatas.Find(id);
            _db.TrafficRoaddatas.Remove(trafficroaddata);
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
