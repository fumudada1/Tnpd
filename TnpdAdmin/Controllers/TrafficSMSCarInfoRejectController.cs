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
    public class TrafficSMSCarInfoRejectController : _BaseController
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
            

            var trafficsmscarinforejects = _db.TrafficSmsCarInfoRejects.OrderByDescending(p => p.ListNum).AsQueryable();
            if (hasViewData("SearchBySubject")) 
            { 
            string searchBySubject = getViewDateStr("SearchBySubject");             
                trafficsmscarinforejects = trafficsmscarinforejects.Where(w => w.Subject.Contains(searchBySubject)); 
            } 
 

//            ViewBag.Subject = Subject;
            return View(trafficsmscarinforejects.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

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
                    TrafficSMSCarInfoReject trafficsmscarinforeject = _db.TrafficSmsCarInfoRejects.Find(Convert.ToInt16(itemDatas[0]));
                    trafficsmscarinforeject.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /TrafficSMSCarInfoReject/Details/5

        public ActionResult Details(int id = 0)
        {
            TrafficSMSCarInfoReject trafficsmscarinforeject = _db.TrafficSmsCarInfoRejects.Find(id);
            if (trafficsmscarinforeject == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(trafficsmscarinforeject);
        }

        //
        // GET: /TrafficSMSCarInfoReject/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TrafficSMSCarInfoReject/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(TrafficSMSCarInfoReject trafficsmscarinforeject )
        {
            if (ModelState.IsValid)
            {

                _db.TrafficSmsCarInfoRejects.Add(trafficsmscarinforeject);
	  int maxListNum = 0;
      if (( _db.TrafficSmsCarInfoRejects.Any()))
      {
		 maxListNum = _db.TrafficSmsCarInfoRejects.Max(x => x.ListNum) ;
      }                
				trafficsmscarinforeject.ListNum = maxListNum +1; 
                trafficsmscarinforeject.Create(_db,_db.TrafficSmsCarInfoRejects);
                return RedirectToAction("Index");
            }

            return View(trafficsmscarinforeject);
        }

        //
        // GET: /TrafficSMSCarInfoReject/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TrafficSMSCarInfoReject trafficsmscarinforeject = _db.TrafficSmsCarInfoRejects.Find(id);
            if (trafficsmscarinforeject == null)
            {
                return HttpNotFound();
            }
            return View(trafficsmscarinforeject);
        }

        //
        // POST: /TrafficSMSCarInfoReject/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(TrafficSMSCarInfoReject trafficsmscarinforeject)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(trafficsmscarinforeject).State = EntityState.Modified;
                trafficsmscarinforeject.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            return View(trafficsmscarinforeject);
        }

        //
        // GET: /TrafficSMSCarInfoReject/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TrafficSMSCarInfoReject trafficsmscarinforeject = _db.TrafficSmsCarInfoRejects.Find(id);
            if (trafficsmscarinforeject == null)
            {
                return HttpNotFound();
            }
            return View(trafficsmscarinforeject);
        }

        //
        // POST: /TrafficSMSCarInfoReject/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrafficSMSCarInfoReject trafficsmscarinforeject = _db.TrafficSmsCarInfoRejects.Find(id);
            _db.TrafficSmsCarInfoRejects.Remove(trafficsmscarinforeject);
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
