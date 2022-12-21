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
    public class TrafficFrontEndViolationItemController : _BaseController
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
            

            var trafficfrontendviolationitems = _db.TrafficFrontEndViolationItems.OrderByDescending(p => p.ListNum).AsQueryable();
            if (hasViewData("SearchBySubject")) 
            { 
            string searchBySubject = getViewDateStr("SearchBySubject");             
                trafficfrontendviolationitems = trafficfrontendviolationitems.Where(w => w.Subject.Contains(searchBySubject)); 
            } 
 
            if (hasViewData("SearchByIsEnable")) 
            { 
            string IsEnable = getViewDateStr("SearchByIsEnable");             
             TnpdModels.BooleanType searchByIsEnable= (TnpdModels.BooleanType)Enum.Parse(typeof(TnpdModels.BooleanType), IsEnable, false); 
             
                trafficfrontendviolationitems = trafficfrontendviolationitems.Where(w => w.IsEnable == searchByIsEnable); 
            } 
 

//            ViewBag.Subject = Subject;
            return View(trafficfrontendviolationitems.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

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
                    TrafficFrontEndViolationItem trafficfrontendviolationitem = _db.TrafficFrontEndViolationItems.Find(Convert.ToInt16(itemDatas[0]));
                    trafficfrontendviolationitem.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /TrafficFrontEndViolationItem/Details/5

        public ActionResult Details(int id = 0)
        {
            TrafficFrontEndViolationItem trafficfrontendviolationitem = _db.TrafficFrontEndViolationItems.Find(id);
            if (trafficfrontendviolationitem == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(trafficfrontendviolationitem);
        }

        //
        // GET: /TrafficFrontEndViolationItem/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TrafficFrontEndViolationItem/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(TrafficFrontEndViolationItem trafficfrontendviolationitem )
        {
            if (ModelState.IsValid)
            {

                _db.TrafficFrontEndViolationItems.Add(trafficfrontendviolationitem);
	  int maxListNum = 0;
      if (( _db.TrafficFrontEndViolationItems.Any()))
      {
		 maxListNum = _db.TrafficFrontEndViolationItems.Max(x => x.ListNum) ;
      }                
				trafficfrontendviolationitem.ListNum = maxListNum +1; 
                trafficfrontendviolationitem.Create(_db,_db.TrafficFrontEndViolationItems);
                return RedirectToAction("Index");
            }

            return View(trafficfrontendviolationitem);
        }

        //
        // GET: /TrafficFrontEndViolationItem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TrafficFrontEndViolationItem trafficfrontendviolationitem = _db.TrafficFrontEndViolationItems.Find(id);
            if (trafficfrontendviolationitem == null)
            {
                return HttpNotFound();
            }
            return View(trafficfrontendviolationitem);
        }

        //
        // POST: /TrafficFrontEndViolationItem/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(TrafficFrontEndViolationItem trafficfrontendviolationitem)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(trafficfrontendviolationitem).State = EntityState.Modified;
                trafficfrontendviolationitem.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            return View(trafficfrontendviolationitem);
        }

        //
        // GET: /TrafficFrontEndViolationItem/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TrafficFrontEndViolationItem trafficfrontendviolationitem = _db.TrafficFrontEndViolationItems.Find(id);
            if (trafficfrontendviolationitem == null)
            {
                return HttpNotFound();
            }
            return View(trafficfrontendviolationitem);
        }

        //
        // POST: /TrafficFrontEndViolationItem/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrafficFrontEndViolationItem trafficfrontendviolationitem = _db.TrafficFrontEndViolationItems.Find(id);
            _db.TrafficFrontEndViolationItems.Remove(trafficfrontendviolationitem);
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
