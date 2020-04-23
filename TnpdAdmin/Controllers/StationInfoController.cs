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
    public class StationInfoController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 150;
        //




        public ActionResult Index(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var stationinfos = _db.StationInfos.OrderByDescending(p => p.ListNum).ToList();
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var selectOrg = stationinfos.Where(x => member.MyUnit.ParentUnit.Subject.Contains(x.Subject) ).OrderBy(p => p.ListNum).ToList();
            if (member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                selectOrg = stationinfos.Where(x => x.ParentId == null|| x.ParentId==2).OrderBy(p => p.ListNum).ToList();
            }


            ViewBag.ParentId = new SelectList(selectOrg, "Id", "Subject");

            int ParentId = selectOrg.FirstOrDefault().Id;
            var station = stationinfos.Where(w => w.ParentId == ParentId || w.Id==ParentId);

            if (hasViewData("SearchByParentId"))
            {
                int searchByParentId = getViewDateInt("SearchByParentId");
                station = stationinfos.Where(w => w.ParentId == searchByParentId || w.Id == searchByParentId);
            }
           



            //            ViewBag.Subject = Subject;
            return View(station.ToPagedList(currentPageIndex, DefaultPageSize));

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
                    StationInfo stationinfo = _db.StationInfos.Find(Convert.ToInt16(itemDatas[0]));
                    stationinfo.ListNum = Convert.ToInt16(itemDatas[1]);

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }


        //
        // GET: /StationInfo/Details/5

        public ActionResult Details(int id = 0)
        {
            StationInfo stationinfo = _db.StationInfos.Find(id);
            if (stationinfo == null)
            {
                //return HttpNotFound();
                return View();
            }
            return View(stationinfo);
        }

        //
        // GET: /StationInfo/Create

        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(_db.StationInfos.Where(x => (x.ParentId == null || x.ParentId == 2) && x.Id != 2).OrderBy(p => p.ListNum), "Id", "Subject");
            ViewBag.TownId = new SelectList(_db.Towns.OrderBy(p => p.ListNum), "Id", "Subject");
            return View();
        }

        //
        // POST: /StationInfo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(StationInfo stationinfo)
        {
            if (ModelState.IsValid)
            {

                _db.StationInfos.Add(stationinfo);
                int maxListNum = 0;
                if ((_db.StationInfos.Any()))
                {
                    maxListNum = _db.StationInfos.Max(x => x.ListNum);
                }
                stationinfo.ListNum = maxListNum + 1;
                Member member =
                    _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
                stationinfo.OwnWebSiteId = member.MyUnit.ParentId.Value;
                stationinfo.Create(_db, _db.StationInfos);
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(_db.StationInfos.OrderBy(p => p.ListNum), "Id", "Subject", stationinfo.ParentId);
            ViewBag.TownId = new SelectList(_db.Towns.OrderBy(p => p.ListNum), "Id", "Subject", stationinfo.TownId);
            return View(stationinfo);
        }

        //
        // GET: /StationInfo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            StationInfo stationinfo = _db.StationInfos.Find(id);
            if (stationinfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(_db.StationInfos.Where(x => (x.ParentId == null || x.ParentId == 2) && x.Id!=2).OrderBy(p => p.ListNum), "Id", "Subject", stationinfo.ParentId);
            ViewBag.TownId = new SelectList(_db.Towns.OrderBy(p => p.ListNum), "Id", "Subject", stationinfo.TownId);
            return View(stationinfo);
        }

        //
        // POST: /StationInfo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(StationInfo stationinfo)
        {
            if (ModelState.IsValid)
            {
                StationInfo stationinfo1 = _db.StationInfos.Find(stationinfo.Id);
                if (stationinfo1.ParentId == null)
                {
                    stationinfo.ParentId = null;
                }

                //_db.Entry(stationinfo).State = EntityState.Modified;
                stationinfo.Update();
                return RedirectToAction("Index", new { Page = -1 });
            }
            ViewBag.ParentId = new SelectList(_db.StationInfos.OrderBy(p => p.ListNum), "Id", "Subject", stationinfo.ParentId);
            ViewBag.TownId = new SelectList(_db.Towns.OrderBy(p => p.ListNum), "Id", "Subject", stationinfo.TownId);
            return View(stationinfo);
        }

        //
        // GET: /StationInfo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            StationInfo stationinfo = _db.StationInfos.Find(id);
            if (stationinfo == null)
            {
                return HttpNotFound();
            }
            return View(stationinfo);
        }

        //
        // POST: /StationInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StationInfo stationinfo = _db.StationInfos.Find(id);
            for (int i = stationinfo.StationInfoFIleses.Count - 1; i == 0; i--)
            {
                var item = stationinfo.StationInfoFIleses.ElementAt(i);
                _db.StationInfoFIleses.Remove(item);
            }


            _db.StationInfos.Remove(stationinfo);
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
