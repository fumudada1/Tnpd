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
    public class TrafficViolationItemController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 150;
        //




        public ActionResult Index(int? page, FormCollection fc,int id=0)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);


            var trafficviolationitems = _db.TrafficViolationItems.OrderByDescending(p => p.ListNum).AsQueryable();

            TrafficViolationType type;
            if (id == 0)
            {
                type = TrafficViolationType.平面道路;
            }
            else
            {
                type = TrafficViolationType.快速道路;
            }

            trafficviolationitems = trafficviolationitems.Where(w => w.TrafficViolationType ==type);
            ViewBag.typeid = id;
            //            ViewBag.Subject = Subject;
            return View(trafficviolationitems.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

        }



        [HttpPost]
        public ActionResult Sort(string sortData)
        {
            TrafficViolationType typeName = TrafficViolationType.平面道路;
            if (!string.IsNullOrEmpty(sortData))
            {
                string[] tempDatas = sortData.TrimEnd(',').Split(',');
                foreach (string tempData in tempDatas)
                {
                    string[] itemDatas = tempData.Split(':');
                    TrafficViolationItem trafficviolationitem = _db.TrafficViolationItems.Find(Convert.ToInt16(itemDatas[0]));
                    trafficviolationitem.ListNum = Convert.ToInt16(itemDatas[1]);
                    typeName = trafficviolationitem.TrafficViolationType;
                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }

            return RedirectToAction("Index", new { id = typeName == TrafficViolationType.平面道路 ? 0 : 1 });


        }


        //
        // GET: /TrafficViolationItem/Details/5

        public ActionResult Details(int id = 0)
        {
            TrafficViolationItem trafficviolationitem = _db.TrafficViolationItems.Find(id);
            if (trafficviolationitem == null)
            {
                //return HttpNotFound();
                return View();
            }
            return View(trafficviolationitem);
        }

        //
        // GET: /TrafficViolationItem/Create

        public ActionResult Create(int id=0)
        {
            ViewBag.typeid = id;
            return View();
        }

        //
        // POST: /TrafficViolationItem/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(TrafficViolationItem trafficviolationitem)
        {
            if (ModelState.IsValid)
            {

                _db.TrafficViolationItems.Add(trafficviolationitem);
                int maxListNum = 0;
                if ((_db.TrafficViolationItems.Any()))
                {
                    maxListNum = _db.TrafficViolationItems.Max(x => x.ListNum);
                }
                trafficviolationitem.ListNum = maxListNum + 1;
                trafficviolationitem.Create(_db, _db.TrafficViolationItems);
                return RedirectToAction("Index", new { id = trafficviolationitem.TrafficViolationType==TrafficViolationType.平面道路?0:1});
            }

            return View(trafficviolationitem);
        }

        //
        // GET: /TrafficViolationItem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TrafficViolationItem trafficviolationitem = _db.TrafficViolationItems.Find(id);
            if (trafficviolationitem == null)
            {
                return HttpNotFound();
            }
            return View(trafficviolationitem);
        }

        //
        // POST: /TrafficViolationItem/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(TrafficViolationItem trafficviolationitem)
        {
            if (ModelState.IsValid)
            {

                //_db.Entry(trafficviolationitem).State = EntityState.Modified;
                trafficviolationitem.Update();
                return RedirectToAction("Index", new { Page = -1, id = trafficviolationitem.TrafficViolationType == TrafficViolationType.平面道路 ? 0 : 1 });
            }
            return View(trafficviolationitem);
        }

        //
        // GET: /TrafficViolationItem/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TrafficViolationItem trafficviolationitem = _db.TrafficViolationItems.Find(id);
            if (trafficviolationitem == null)
            {
                return HttpNotFound();
            }
            return View(trafficviolationitem);
        }

        //
        // POST: /TrafficViolationItem/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrafficViolationItem trafficviolationitem = _db.TrafficViolationItems.Find(id);
            int typeId = trafficviolationitem.TrafficViolationType == TrafficViolationType.平面道路 ? 0 : 1;
            _db.TrafficViolationItems.Remove(trafficviolationitem);
            _db.SaveChanges();
            return RedirectToAction("Index", new { Page = -1, id = typeId });
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
