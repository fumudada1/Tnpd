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
    public class PoprocsSubTypeController : _BaseController
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
            

            var poprocssubtypes = _db.PoprocsSubTypes.OrderByDescending(p => p.InitDate).AsQueryable();
            if (hasViewData("SearchBySubject")) 
            { 
            string searchBySubject = getViewDateStr("SearchBySubject");             
                poprocssubtypes = poprocssubtypes.Where(w => w.Subject.Contains(searchBySubject)); 
            } 
 

//            ViewBag.Subject = Subject;
            return View(poprocssubtypes.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }



        

        //
        // GET: /PoprocsSubType/Details/5

        public ActionResult Details(int id = 0)
        {
            PoprocsSubType poprocssubtype = _db.PoprocsSubTypes.Find(id);
            if (poprocssubtype == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(poprocssubtype);
        }

        //
        // GET: /PoprocsSubType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PoprocsSubType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(PoprocsSubType poprocssubtype )
        {
            if (ModelState.IsValid)
            {

                _db.PoprocsSubTypes.Add(poprocssubtype);
                poprocssubtype.Create(_db,_db.PoprocsSubTypes);
                return RedirectToAction("Index");
            }

            return View(poprocssubtype);
        }

        //
        // GET: /PoprocsSubType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PoprocsSubType poprocssubtype = _db.PoprocsSubTypes.Find(id);
            if (poprocssubtype == null)
            {
                return HttpNotFound();
            }
            return View(poprocssubtype);
        }

        //
        // POST: /PoprocsSubType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         [ValidateInput(false)]
        public ActionResult Edit(PoprocsSubType poprocssubtype)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(poprocssubtype).State = EntityState.Modified;
                poprocssubtype.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            return View(poprocssubtype);
        }

        //
        // GET: /PoprocsSubType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PoprocsSubType poprocssubtype = _db.PoprocsSubTypes.Find(id);
            if (poprocssubtype == null)
            {
                return HttpNotFound();
            }
            return View(poprocssubtype);
        }

        //
        // POST: /PoprocsSubType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PoprocsSubType poprocssubtype = _db.PoprocsSubTypes.Find(id);
            _db.PoprocsSubTypes.Remove(poprocssubtype);
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
