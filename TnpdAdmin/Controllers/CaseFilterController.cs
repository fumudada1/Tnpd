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
    public class CaseFilterController : _BaseController
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
            

            var casefilterses = _db.CaseFilterses.Include(c => c.PoprocsSubType).OrderByDescending(p => p.InitDate).AsQueryable();
            ViewBag.TypeId = new SelectList(_db.PoprocsSubTypes.OrderBy(p=>p.InitDate), "Id", "Subject");
            if (hasViewData("SearchByFilterType")) 
            { 
            string FilterType = getViewDateStr("SearchByFilterType");             
             TnpdModels.CaseFilterType searchByFilterType= (TnpdModels.CaseFilterType)Enum.Parse(typeof(TnpdModels.CaseFilterType), FilterType, false); 
             
                casefilterses = casefilterses.Where(w => w.FilterType == searchByFilterType); 
            } 
 
            if (hasViewData("SearchBySubject")) 
            { 
            string searchBySubject = getViewDateStr("SearchBySubject");             
                casefilterses = casefilterses.Where(w => w.Subject.Contains(searchBySubject)); 
            } 
 
            if (hasViewData("SearchByTypeId")) 
            { 
            int searchByTypeId = getViewDateInt("SearchByTypeId");             
                casefilterses = casefilterses.Where(w => w.TypeId == searchByTypeId); 
            } 

//            ViewBag.Subject = Subject;
            return View(casefilterses.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }



        

        //
        // GET: /CaseFilter/Details/5

        public ActionResult Details(int id = 0)
        {
            CaseFilters casefilters = _db.CaseFilterses.Find(id);
            if (casefilters == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(casefilters);
        }

        //
        // GET: /CaseFilter/Create

        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(_db.PoprocsSubTypes.OrderBy(p=>p.InitDate), "Id", "Subject");
            return View();
        }

        //
        // POST: /CaseFilter/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(CaseFilters casefilters )
        {
            if (ModelState.IsValid)
            {

                _db.CaseFilterses.Add(casefilters);
                casefilters.Create(_db,_db.CaseFilterses);
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(_db.PoprocsSubTypes.OrderBy(p => p.InitDate), "Id", "Subject", casefilters.TypeId);
            return View(casefilters);
        }

        //
        // GET: /CaseFilter/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CaseFilters casefilters = _db.CaseFilterses.Find(id);
            if (casefilters == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeId = new SelectList(_db.PoprocsSubTypes.OrderBy(p => p.InitDate), "Id", "Subject", casefilters.TypeId);
            return View(casefilters);
        }

        //
        // POST: /CaseFilter/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(CaseFilters casefilters)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(casefilters).State = EntityState.Modified;
                casefilters.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            ViewBag.TypeId = new SelectList(_db.PoprocsSubTypes.OrderBy(p => p.InitDate), "Id", "Subject", casefilters.TypeId);
            return View(casefilters);
        }

        //
        // GET: /CaseFilter/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CaseFilters casefilters = _db.CaseFilterses.Find(id);
            if (casefilters == null)
            {
                return HttpNotFound();
            }
            return View(casefilters);
        }

        //
        // POST: /CaseFilter/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CaseFilters casefilters = _db.CaseFilterses.Find(id);
            _db.CaseFilterses.Remove(casefilters);
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
