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
    public class TrafficViolationsClassController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 150;
        //

        


        public ActionResult Index(int? page, FormCollection fc )
        {
			//記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);
            

            var trafficviolationsclasses = _db.TrafficViolationsClasses.OrderByDescending(p => p.ListNum).AsQueryable();
            if (hasViewData("SearchBySubject")) 
            { 
            string searchBySubject = getViewDateStr("SearchBySubject");             
                trafficviolationsclasses = trafficviolationsclasses.Where(w => w.Subject.Contains(searchBySubject)); 
            } 
 

//            ViewBag.Subject = Subject;
            return View(trafficviolationsclasses.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

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
                    TrafficViolationsClass trafficviolationsclass = _db.TrafficViolationsClasses.Find(Convert.ToInt16(itemDatas[0]));
                    trafficviolationsclass.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /TrafficViolationsClass/Details/5

        public ActionResult Details(int id = 0)
        {
            TrafficViolationsClass trafficviolationsclass = _db.TrafficViolationsClasses.Find(id);
            if (trafficviolationsclass == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(trafficviolationsclass);
        }

        //
        // GET: /TrafficViolationsClass/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TrafficViolationsClass/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(TrafficViolationsClass trafficviolationsclass )
        {
            if (ModelState.IsValid)
            {

                _db.TrafficViolationsClasses.Add(trafficviolationsclass);
	  int maxListNum = 0;
      if (( _db.TrafficViolationsClasses.Any()))
      {
		 maxListNum = _db.TrafficViolationsClasses.Max(x => x.ListNum) ;
      }                
				trafficviolationsclass.ListNum = maxListNum +1; 
                trafficviolationsclass.Create(_db,_db.TrafficViolationsClasses);
                return RedirectToAction("Index");
            }

            return View(trafficviolationsclass);
        }

        //
        // GET: /TrafficViolationsClass/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TrafficViolationsClass trafficviolationsclass = _db.TrafficViolationsClasses.Find(id);
            if (trafficviolationsclass == null)
            {
                return HttpNotFound();
            }
            return View(trafficviolationsclass);
        }

        //
        // POST: /TrafficViolationsClass/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(TrafficViolationsClass trafficviolationsclass)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(trafficviolationsclass).State = EntityState.Modified;
                trafficviolationsclass.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            return View(trafficviolationsclass);
        }

        //
        // GET: /TrafficViolationsClass/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TrafficViolationsClass trafficviolationsclass = _db.TrafficViolationsClasses.Find(id);
            if (trafficviolationsclass == null)
            {
                return HttpNotFound();
            }
            return View(trafficviolationsclass);
        }

        //
        // POST: /TrafficViolationsClass/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrafficViolationsClass trafficviolationsclass = _db.TrafficViolationsClasses.Find(id);
            _db.TrafficViolationsClasses.Remove(trafficviolationsclass);
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
