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
    public class TrafficViolationsRejectclassController : _BaseController
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
            

            var trafficviolationsrejectclasses = _db.TrafficViolationsRejectclasses.OrderByDescending(p => p.ListNum).AsQueryable();
            if (hasViewData("SearchBySubject")) 
            { 
            string searchBySubject = getViewDateStr("SearchBySubject");             
                trafficviolationsrejectclasses = trafficviolationsrejectclasses.Where(w => w.Subject.Contains(searchBySubject)); 
            } 
 

//            ViewBag.Subject = Subject;
            return View(trafficviolationsrejectclasses.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

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
                    TrafficViolationsRejectclass trafficviolationsrejectclass = _db.TrafficViolationsRejectclasses.Find(Convert.ToInt16(itemDatas[0]));
                    trafficviolationsrejectclass.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /TrafficViolationsRejectclass/Details/5

        public ActionResult Details(int id = 0)
        {
            TrafficViolationsRejectclass trafficviolationsrejectclass = _db.TrafficViolationsRejectclasses.Find(id);
            if (trafficviolationsrejectclass == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(trafficviolationsrejectclass);
        }

        //
        // GET: /TrafficViolationsRejectclass/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TrafficViolationsRejectclass/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(TrafficViolationsRejectclass trafficviolationsrejectclass )
        {
            if (ModelState.IsValid)
            {

                _db.TrafficViolationsRejectclasses.Add(trafficviolationsrejectclass);
	  int maxListNum = 0;
      if (( _db.TrafficViolationsRejectclasses.Any()))
      {
		 maxListNum = _db.TrafficViolationsRejectclasses.Max(x => x.ListNum) ;
      }                
				trafficviolationsrejectclass.ListNum = maxListNum +1; 
                trafficviolationsrejectclass.Create(_db,_db.TrafficViolationsRejectclasses);
                return RedirectToAction("Index");
            }

            return View(trafficviolationsrejectclass);
        }

        //
        // GET: /TrafficViolationsRejectclass/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TrafficViolationsRejectclass trafficviolationsrejectclass = _db.TrafficViolationsRejectclasses.Find(id);
            if (trafficviolationsrejectclass == null)
            {
                return HttpNotFound();
            }
            return View(trafficviolationsrejectclass);
        }

        //
        // POST: /TrafficViolationsRejectclass/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(TrafficViolationsRejectclass trafficviolationsrejectclass)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(trafficviolationsrejectclass).State = EntityState.Modified;
                trafficviolationsrejectclass.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            return View(trafficviolationsrejectclass);
        }

        //
        // GET: /TrafficViolationsRejectclass/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TrafficViolationsRejectclass trafficviolationsrejectclass = _db.TrafficViolationsRejectclasses.Find(id);
            if (trafficviolationsrejectclass == null)
            {
                return HttpNotFound();
            }
            return View(trafficviolationsrejectclass);
        }

        //
        // POST: /TrafficViolationsRejectclass/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrafficViolationsRejectclass trafficviolationsrejectclass = _db.TrafficViolationsRejectclasses.Find(id);
            _db.TrafficViolationsRejectclasses.Remove(trafficviolationsrejectclass);
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
