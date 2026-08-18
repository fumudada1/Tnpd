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
using Tnpd.Models;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class LeaderController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 300;
        //




        public ActionResult Index(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);


            var leaders = _db.Leaders.Include(l => l.Catalog).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
                if (webSite == null)
                {
                    webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
                }
                leaders = leaders.Where(w => w.Catalog.WebSiteId == webSite.Id);

            }


            //            ViewBag.Title = Title;
            return View(leaders.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

        }





        //
        // GET: /Leader/Details/5

        public ActionResult Details(int id = 0)
        {
            Leader leader = _db.Leaders.Find(id);
            if (leader == null)
            {
                //return HttpNotFound();
                return View();
            }
            return View(leader);
        }

        //
        // GET: /Leader/Create

        public ActionResult Create()
        {

            ViewBag.LeaderCatalogId = GetleaderCatalogs();
            Leader leader = new Leader()
            {
                ListNum = 0
            };
            return View(leader);
        }

        private SelectList GetleaderCatalogs(int selectValue=0)
        {
            var leaderCatalogs = _db.LeaderCatalogs.AsQueryable();
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
                if (webSite == null)
                {
                    webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
                }
                leaderCatalogs = leaderCatalogs.Where(w => w.WebSiteId == webSite.Id);

            }

            var data = leaderCatalogs.OrderBy(x => x.ListNum).Select(x => new
            {
                Id = x.Id,
                subject = x.WebSite.Subject + "-" + x.Subject
            });
            return new SelectList(data, "Id", "Subject", selectValue);
        }

        //
        // POST: /Leader/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Leader leader, HttpPostedFileBase UpImage1)
        {
            if (ModelState.IsValid)
            {
                if (UpImage1 != null)
                {
                    if (UpImage1.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.LeaderCatalogId = GetleaderCatalogs();
                        return View(leader);
                    }

                    leader.UpImage1 = Utility.SaveUpImage(UpImage1);

                    //Utility.GenerateThumbnailImageGinny(bigbanner.UpImage, UpImages.InputStream, Server.MapPath("~/upfiles/images"), "M", 1100, 500);
                    Utility.GenerateThumbnailImageGinny(leader.UpImage1, UpImage1.InputStream, Server.MapPath("~/upfiles/images"), "S", 200, 127);
                } 
                _db.Leaders.Add(leader);
                leader.Create(_db, _db.Leaders);
                return RedirectToAction("Index");
            }

            ViewBag.LeaderCatalogId = GetleaderCatalogs();
            return View(leader);
        }

        //
        // GET: /Leader/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Leader leader = _db.Leaders.Find(id);
            if (leader == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeaderCatalogId = GetleaderCatalogs(leader.LeaderCatalogId.Value);
            return View(leader);
        }

        //
        // POST: /Leader/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Leader leader, HttpPostedFileBase UpImage1)
        {
            if (ModelState.IsValid)
            {
                if (UpImage1 != null)
                {
                    if (UpImage1.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.LeaderCatalogId = GetleaderCatalogs(leader.LeaderCatalogId.Value);
                        return View(leader);
                    }

                    leader.UpImage1 = Utility.SaveUpImage(UpImage1);

                    //Utility.GenerateThumbnailImageGinny(bigbanner.UpImage, UpImages.InputStream, Server.MapPath("~/upfiles/images"), "M", 1100, 500);
                    Utility.GenerateThumbnailImageGinny(leader.UpImage1, UpImage1.InputStream, Server.MapPath("~/upfiles/images"), "S", 200, 127);
                } 
                //_db.Entry(leader).State = EntityState.Modified;
                leader.Update();
                return RedirectToAction("Index", new { Page = -1 });
            }
            ViewBag.LeaderCatalogId = GetleaderCatalogs(leader.LeaderCatalogId.Value);
            return View(leader);
        }

        //
        // GET: /Leader/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Leader leader = _db.Leaders.Find(id);
            if (leader == null)
            {
                return HttpNotFound();
            }
            return View(leader);
        }

        //
        // POST: /Leader/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leader leader = _db.Leaders.Find(id);
            _db.Leaders.Remove(leader);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
                    Leader leader = _db.Leaders.Find(Convert.ToInt16(itemDatas[0]));
                    leader.ListNum = Convert.ToInt16(itemDatas[1]);

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
