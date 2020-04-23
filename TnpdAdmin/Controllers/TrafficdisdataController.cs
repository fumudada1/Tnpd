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
    public class TrafficdisdataController : _BaseController
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
            

            var trafficdisdatas = _db.Trafficdisdatas.Include(t => t.assignMember).OrderByDescending(p => p.InitDate).AsQueryable();
            ViewBag.MemberId = new SelectList(_db.Members.OrderBy(p=>p.InitDate), "Id", "Account");
            if (hasViewData("SearchByMemberId")) 
            { 
            int searchByMemberId = getViewDateInt("SearchByMemberId");             
                trafficdisdatas = trafficdisdatas.Where(w => w.MemberId == searchByMemberId); 
            } 
            if (hasViewData("SearchByisAutoAssign")) 
            { 
            string isAutoAssign = getViewDateStr("SearchByisAutoAssign");             
             TnpdModels.BooleanType searchByisAutoAssign= (TnpdModels.BooleanType)Enum.Parse(typeof(TnpdModels.BooleanType), isAutoAssign, false); 
             
                trafficdisdatas = trafficdisdatas.Where(w => w.isAutoAssign == searchByisAutoAssign); 
            } 
 


            return View(trafficdisdatas.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }



        

        //
        // GET: /Trafficdisdata/Details/5

        public ActionResult Details(int id = 0)
        {
            Trafficdisdata trafficdisdata = _db.Trafficdisdatas.Find(id);
            if (trafficdisdata == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(trafficdisdata);
        }

        //
        // GET: /Trafficdisdata/Create

        public ActionResult Create()
        {
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            return View();
        }

        //
        // POST: /Trafficdisdata/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(Trafficdisdata trafficdisdata )
        {
            if (ModelState.IsValid)
            {

                _db.Trafficdisdatas.Add(trafficdisdata);
                trafficdisdata.Create(_db,_db.Trafficdisdatas);
                return RedirectToAction("Index");
            }

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            return View(trafficdisdata);
        }

        //
        // GET: /Trafficdisdata/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Trafficdisdata trafficdisdata = _db.Trafficdisdatas.Find(id);
            if (trafficdisdata == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            return View(trafficdisdata);
        }

        //
        // POST: /Trafficdisdata/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(Trafficdisdata trafficdisdata)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(trafficdisdata).State = EntityState.Modified;
                trafficdisdata.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            return View(trafficdisdata);
        }

        //
        // GET: /Trafficdisdata/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Trafficdisdata trafficdisdata = _db.Trafficdisdatas.Find(id);
            if (trafficdisdata == null)
            {
                return HttpNotFound();
            }
            return View(trafficdisdata);
        }

        //
        // POST: /Trafficdisdata/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trafficdisdata trafficdisdata = _db.Trafficdisdatas.Find(id);
            _db.Trafficdisdatas.Remove(trafficdisdata);
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
