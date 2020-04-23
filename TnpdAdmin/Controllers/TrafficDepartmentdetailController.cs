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
    public class TrafficDepartmentdetailController : _BaseController
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

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            var trafficdepartmentdetails = _db.TrafficDepartmentdetails.Include(t => t.assignMember).OrderByDescending(p => p.InitDate).AsQueryable();

            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")) && !member.Roles.Any(x => x.Subject.Contains("交通檢舉信箱管理者")))
            {

                trafficdepartmentdetails = trafficdepartmentdetails.Where(w => w.assignMember.MyUnit.ParentId == member.MyUnit.ParentId); 
            }
           
            ViewBag.UnitId = new SelectList(_db.Units.Where(x=>x.ParentId==null).OrderBy(p => p.InitDate), "Id", "Subject");
            if (hasViewData("SearchByUnitId")) 
            {
                int searchByUnitId = getViewDateInt("SearchByUnitId");
                trafficdepartmentdetails = trafficdepartmentdetails.Where(w => w.assignMember.MyUnit.ParentId == searchByUnitId); 
            } 


            return View(trafficdepartmentdetails.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }



        

        //
        // GET: /TrafficDepartmentdetail/Details/5

        public ActionResult Details(int id = 0)
        {
            TrafficDepartmentdetail trafficdepartmentdetail = _db.TrafficDepartmentdetails.Find(id);
            if (trafficdepartmentdetail == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(trafficdepartmentdetail);
        }

        //
        // GET: /TrafficDepartmentdetail/Create

        public ActionResult Create()
        {
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            return View();
        }

        //
        // POST: /TrafficDepartmentdetail/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(TrafficDepartmentdetail trafficdepartmentdetail )
        {
            if (ModelState.IsValid)
            {

                _db.TrafficDepartmentdetails.Add(trafficdepartmentdetail);
                trafficdepartmentdetail.Create(_db,_db.TrafficDepartmentdetails);
                return RedirectToAction("Index");
            }

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            return View(trafficdepartmentdetail);
        }

        //
        // GET: /TrafficDepartmentdetail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TrafficDepartmentdetail trafficdepartmentdetail = _db.TrafficDepartmentdetails.Find(id);
            if (trafficdepartmentdetail == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            return View(trafficdepartmentdetail);
        }

        //
        // POST: /TrafficDepartmentdetail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(TrafficDepartmentdetail trafficdepartmentdetail)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(trafficdepartmentdetail).State = EntityState.Modified;
                trafficdepartmentdetail.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            return View(trafficdepartmentdetail);
        }

        //
        // GET: /TrafficDepartmentdetail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TrafficDepartmentdetail trafficdepartmentdetail = _db.TrafficDepartmentdetails.Find(id);
            if (trafficdepartmentdetail == null)
            {
                return HttpNotFound();
            }
            return View(trafficdepartmentdetail);
        }

        //
        // POST: /TrafficDepartmentdetail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrafficDepartmentdetail trafficdepartmentdetail = _db.TrafficDepartmentdetails.Find(id);
            _db.TrafficDepartmentdetails.Remove(trafficdepartmentdetail);
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
