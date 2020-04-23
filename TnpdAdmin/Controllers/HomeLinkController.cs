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
    public class HomeLinkController : _BaseController
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



            var homelinks = _db.HomeLinks.Where(x=>x.DataType==2).Include(h => h.WebSite).OrderByDescending(p => p.ListNum).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
            }
            var websites = _db.WebSiteNames.Where(x => x.Language == LanguageType.中文版).OrderBy(p => p.ListNum).AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {

                websites = websites.Where(x => x.Id == webSite.Id);
                homelinks = homelinks.Where(w => w.WebSiteNameId == webSite.Id);
            }

            ViewBag.WebSiteNameId = new SelectList(websites, "Id", "Subject", webSite.Id);

           
            if (hasViewData("SearchByWebSiteNameId"))
            {
                int searchByWebSiteNameId = getViewDateInt("SearchByWebSiteNameId");
                homelinks = homelinks.Where(w => w.WebSiteNameId == searchByWebSiteNameId);
                Session["HomeLinkWebSiteNameId"] = searchByWebSiteNameId;
            }
            else
            {
                homelinks = homelinks.Where(w => w.WebSiteNameId == webSite.Id);

            }

            //            ViewBag.Subject = Subject;
            return View(homelinks.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

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
                    HomeLink homelink = _db.HomeLinks.Find(Convert.ToInt16(itemDatas[0]));
                    homelink.ListNum = Convert.ToInt16(itemDatas[1]);

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }


        //
        // GET: /HomeLink/Details/5

        public ActionResult Details(int id = 0)
        {
            HomeLink homelink = _db.HomeLinks.Find(id);
            if (homelink == null)
            {
                //return HttpNotFound();
                return View();
            }
            return View(homelink);
        }

        //
        // GET: /HomeLink/Create

        public ActionResult Create()
        {
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
            }
            var websites = _db.WebSiteNames.Where(x => x.Language == LanguageType.中文版).OrderBy(p => p.ListNum).AsQueryable();
            if (member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                if (Session["HomeLinkWebSiteNameId"] != null)
                {
                    int WebSiteNameId = Convert.ToInt32(Session["HomeLinkWebSiteNameId"]);

                    ViewBag.WebSiteNameId =
                        new SelectList(websites,
                            "Id", "Subject", WebSiteNameId);
                }
                else
                {
                    ViewBag.WebSiteNameId =
                        new SelectList(
                            websites,
                            "Id", "Subject");
                }

            }
            else
            {
                websites = websites.Where(x => x.Id == webSite.Id);
                ViewBag.WebSiteNameId = new SelectList(websites, "Id", "Subject", webSite.Id);
            }

           
            return View();
        }

        //
        // POST: /HomeLink/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(HomeLink homelink, HttpPostedFileBase UpImages)
        {

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (ModelState.IsValid)
            {
                if (UpImages != null)
                {
                    if (UpImages.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.WebSiteNameId = new SelectList(_db.WebSiteNames.OrderBy(p => p.ListNum), "Id", "Subject", homelink.WebSiteNameId);
                        return View(homelink);
                    }

                    homelink.UpImage = "/upfiles/images/" + Utility.SaveUpImage(UpImages);
                    //Utility.GenerateThumbnailImage(homelink.UpImage, UpImages.InputStream, Server.MapPath("~/upfiles/images"), "S", 127, 127);
                }
                System.Threading.Thread.Sleep(1000);
 
                homelink.OwnWebSiteId = member.MyUnit.ParentId.Value;
                _db.HomeLinks.Add(homelink);
                int maxListNum = 0;
                if ((_db.HomeLinks.Any()))
                {
                    maxListNum = _db.HomeLinks.Max(x => x.ListNum);
                }
                homelink.ListNum = maxListNum + 1;
                homelink.DataType = 2;
               
                homelink.Create(_db, _db.HomeLinks);
                return RedirectToAction("Index");
            }

            WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
            }
            var websites = _db.WebSiteNames.Where(x => x.Language == LanguageType.中文版).OrderBy(p => p.ListNum).AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {

                websites = websites.Where(x => x.Id == webSite.Id);
            }

            ViewBag.WebSiteNameId = new SelectList(websites, "Id", "Subject", webSite.Id);
            return View(homelink);
        }

        //
        // GET: /HomeLink/Edit/5

        public ActionResult Edit(int id = 0)
        {
            HomeLink homelink = _db.HomeLinks.Find(id);
            if (homelink == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
            }
            var websites = _db.WebSiteNames.Where(x => x.Language == LanguageType.中文版).OrderBy(p => p.ListNum).AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {

                websites = websites.Where(x => x.Id == webSite.Id);
            }

            ViewBag.WebSiteNameId = new SelectList(websites, "Id", "Subject", homelink.WebSiteNameId);
            return View(homelink);
        }

        //
        // POST: /HomeLink/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(HomeLink homelink, HttpPostedFileBase UpImages)
        {
            if (ModelState.IsValid)
            {
                if (UpImages != null)
                {
                    if (UpImages.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.WebSiteNameId = new SelectList(_db.WebSiteNames.OrderBy(p => p.ListNum), "Id", "Subject", homelink.WebSiteNameId);
                        return View(homelink);
                    }

                    homelink.UpImage = "/upfiles/images/" + Utility.SaveUpImage(UpImages);
                    //Utility.GenerateThumbnailImage(homelink.UpImage, UpImages.InputStream, Server.MapPath("~/upfiles/images"), "S", 127, 127);
                }
                System.Threading.Thread.Sleep(1000);

                //_db.Entry(homelink).State = EntityState.Modified;
                homelink.Update();
                return RedirectToAction("Index", new { Page = -1 });
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
            }
            var websites = _db.WebSiteNames.Where(x => x.Language == LanguageType.中文版).OrderBy(p => p.ListNum).AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {

                websites = websites.Where(x => x.Id == webSite.Id);
            }

            ViewBag.WebSiteNameId = new SelectList(websites, "Id", "Subject", webSite.Id);
            return View(homelink);
        }

        //
        // GET: /HomeLink/Delete/5

        public ActionResult Delete(int id = 0)
        {
            HomeLink homelink = _db.HomeLinks.Find(id);
            if (homelink == null)
            {
                return HttpNotFound();
            }
            return View(homelink);
        }

        //
        // POST: /HomeLink/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeLink homelink = _db.HomeLinks.Find(id);
            _db.HomeLinks.Remove(homelink);
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
