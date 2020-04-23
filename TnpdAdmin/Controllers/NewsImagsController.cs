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
    public class NewsImagsController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 15;
        //




        public ActionResult Index(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);


            var newsimages = _db.NewsImages.Include(n => n.News).OrderByDescending(p => p.ListNum).AsQueryable();
            ViewBag.NewId = new SelectList(_db.Newses.OrderBy(p => p.InitDate), "Id", "Subject");
            if (hasViewData("SearchBySubject"))
            {
                string searchBySubject = getViewDateStr("SearchBySubject");
                newsimages = newsimages.Where(w => w.Subject.Contains(searchBySubject));
            }

            if (hasViewData("SearchByNewId"))
            {
                int searchByNewId = getViewDateInt("SearchByNewId");
                newsimages = newsimages.Where(w => w.NewId == searchByNewId);
            }

            //            ViewBag.Subject = Subject;
            return View(newsimages.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

        }





        //
        // GET: /NewsImags/Details/5

        public ActionResult Details(int id = 0)
        {
            NewsImage newsimage = _db.NewsImages.Find(id);
            if (newsimage == null)
            {
                //return HttpNotFound();
                return View();
            }
            return View(newsimage);
        }

        [HttpPost]
        public ActionResult Sort(string sortData, int Id)
        {
            if (!string.IsNullOrEmpty(sortData))
            {
                string[] tempDatas = sortData.TrimEnd(',').Split(',');
                foreach (string tempData in tempDatas)
                {
                    string[] itemDatas = tempData.Split(':');
                    NewsImage newsImages = _db.NewsImages.Find(Convert.ToInt16(itemDatas[0]));
                    newsImages.ListNum = Convert.ToInt16(itemDatas[1]);

                    //db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            ViewBag.state = "sort";
            return RedirectToAction("Edit", "News", new { Id = Id, type = "3", state = "sort", pclass = Request["pclass"] });
        }
        //
        // GET: /NewsImags/Create

        public ActionResult Create(string NewsId)
        {
            Member member =
                           _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }
            ViewBag.SiteCode = webSite.SiteCode;
            ViewBag.NewsId = NewsId;
            return View();
        }

        //
        // POST: /NewsImags/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(NewsImage newsimage)
        {
            if (ModelState.IsValid)
            {

                _db.NewsImages.Add(newsimage);
                newsimage.Create(_db, _db.NewsImages);
                ViewBag.close = "true";
                ViewBag.NewsId = newsimage.NewId;
                return View();
            }

            ViewBag.NewId = newsimage.NewId;
            return View(newsimage);
        }

        //
        // GET: /NewsImags/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }
            ViewBag.SiteCode = webSite.SiteCode;
            NewsImage newsImage = _db.NewsImages.Find(id);
            if (newsImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewsId = newsImage.NewId;
            return View(newsImage);
        }

        //
        // POST: /NewsImags/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(NewsImage newsimage)
        {
            if (ModelState.IsValid)
            {

                //db.Entry(newsfiles).State = EntityState.Modified;
                newsimage.Update();
                ViewBag.close = "true";
                ViewBag.NewsId = newsimage.NewId;
                return View(newsimage);
            }
            ViewBag.NewsId = newsimage.NewId;
            return View(newsimage);
        }

        //
        // GET: /NewsImags/Delete/5

        public ActionResult Delete(int fileid = 0)
        {
            NewsImage newsImage = _db.NewsImages.Find(fileid);
            if (newsImage == null)
            {
                return HttpNotFound();
            }
            return View(newsImage);
        }

        //
        // POST: /NewsFiles/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int fileid)
        {
            NewsImage newsImage = _db.NewsImages.Find(fileid);

            int newsId = (int)newsImage.NewId;
            _db.NewsImages.Remove(newsImage);
            _db.SaveChanges();
            return RedirectToAction("Edit", "News", new { Id = newsId, type = "3", pclass = Request["pclass"] });
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
