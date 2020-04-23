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
    public class WandaController : _BaseController
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
            

            var wandas = _db.Wandas.Include(w => w.StationInfo).OrderByDescending(p => p.InitDate).AsQueryable();
            ViewBag.StationInfoId = new SelectList(_db.StationInfos.Where(x=>x.Subject.Contains("分局")).OrderBy(p=>p.InitDate), "Id", "Subject");
            if (hasViewData("SearchBySubject")) 
            { 
            string searchBySubject = getViewDateStr("SearchBySubject");             
                wandas = wandas.Where(w => w.Subject.Contains(searchBySubject)); 
            } 
 
            if (hasViewData("SearchByStationInfoId")) 
            { 
            int searchByStationInfoId = getViewDateInt("SearchByStationInfoId");             
                wandas = wandas.Where(w => w.StationInfoId == searchByStationInfoId); 
            } 
            if (hasViewData("SearchByStatus")) 
            { 
            string Status = getViewDateStr("SearchByStatus");             
             TnpdModels.BooleanType searchByStatus= (TnpdModels.BooleanType)Enum.Parse(typeof(TnpdModels.BooleanType), Status, false); 
             
                wandas = wandas.Where(w => w.Status == searchByStatus); 
            } 
 

//            ViewBag.Subject = Subject;
            return View(wandas.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }



        

        //
        // GET: /Wanda/Details/5

        public ActionResult Details(int id = 0)
        {
            Wanda wanda = _db.Wandas.Find(id);
            if (wanda == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(wanda);
        }

        //
        // GET: /Wanda/Create

        public ActionResult Create()
        {
            ViewBag.StationInfoId = new SelectList(_db.StationInfos.OrderBy(p=>p.ListNum), "Id", "Subject");
            return View();
        }

        //
        // POST: /Wanda/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Wanda wanda )
        {
            if (ModelState.IsValid)
            {
                Member member =
                    _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
                wanda.OwnWebSiteId = member.MyUnit.ParentId.Value;
                _db.Wandas.Add(wanda);
                wanda.Create(_db,_db.Wandas);
                return RedirectToAction("Index");
            }

            ViewBag.StationInfoId = new SelectList(_db.StationInfos.OrderBy(p=>p.ListNum), "Id", "Subject", wanda.StationInfoId);
            return View(wanda);
        }

        //
        // GET: /Wanda/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Wanda wanda = _db.Wandas.Find(id);
            if (wanda == null)
            {
                return HttpNotFound();
            }
            ViewBag.StationInfoId = new SelectList(_db.StationInfos.OrderBy(p=>p.ListNum), "Id", "Subject", wanda.StationInfoId);
            return View(wanda);
        }

        //
        // POST: /Wanda/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         [ValidateInput(false)]
        public ActionResult Edit(Wanda wanda)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(wanda).State = EntityState.Modified;
                wanda.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            ViewBag.StationInfoId = new SelectList(_db.StationInfos.OrderBy(p=>p.ListNum), "Id", "Subject", wanda.StationInfoId);
            return View(wanda);
        }

        //
        // GET: /Wanda/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Wanda wanda = _db.Wandas.Find(id);
            if (wanda == null)
            {
                return HttpNotFound();
            }
            return View(wanda);
        }

        //
        // POST: /Wanda/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wanda wanda = _db.Wandas.Find(id);
            _db.Wandas.Remove(wanda);
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
