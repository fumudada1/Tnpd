using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using TnpdModels;

namespace Tnpd.Controllers
{
    public class NewsFilesController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 15;
        //
        // GET: /NewsFiles/

        public ActionResult Index(int? page)
        {


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var newsfileses = db.NewsFileses.Include(n => n.News).OrderByDescending(p => p.ListNum);
            ViewBag.NewId = new SelectList(db.Newses.OrderBy(p => p.InitDate), "Id", "Subject");
            return View(newsfileses.OrderByDescending(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Index(string Subject, int NewId, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var newsfileses = db.NewsFileses.Include(n => n.News).OrderByDescending(p => p.ListNum).AsQueryable();
            ViewBag.NewId = new SelectList(db.Newses.OrderBy(p => p.InitDate), "Id", "Subject");
            if (!string.IsNullOrEmpty(Subject))
            {
                newsfileses = newsfileses.Where(w => w.Subject.Contains(Subject));
            }

            if (NewId != 0)
            {
                newsfileses = newsfileses.Where(w => w.NewId == NewId);
            }

            ViewBag.Subject = Subject;
            return View(newsfileses.OrderByDescending(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
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
                    NewsFiles newsfiles = db.NewsFileses.Find(Convert.ToInt16(itemDatas[0]));
                    newsfiles.ListNum = Convert.ToInt16(itemDatas[1]);

                    //db.Entry(publish).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            ViewBag.state = "sort";
            return RedirectToAction("Edit", "News", new { Id = Id, type = "1", state = "sort", pclass = Request["pclass"] });
        }


        //
        // GET: /NewsFiles/Details/5

        public ActionResult Details(int id = 0)
        {
            NewsFiles newsfiles = db.NewsFileses.Find(id);
            if (newsfiles == null)
            {
                return HttpNotFound();
            }
            return View(newsfiles);
        }

        //
        // GET: /NewsFiles/Create

        public ActionResult Create(string NewsId)
        {
            Member member =
                           db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }
            ViewBag.SiteCode = webSite.SiteCode;
            ViewBag.NewsId = NewsId;
            return View();
        }

        //
        // POST: /NewsFiles/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsFiles newsfiles)
        {
            if (ModelState.IsValid)
            {

                db.NewsFileses.Add(newsfiles);
                newsfiles.Create(db, db.NewsFileses);
                ViewBag.close = "true";
                ViewBag.NewsId = newsfiles.NewId;
                return View();
            }

            ViewBag.NewId = newsfiles.NewId;
            return View(newsfiles);
        }

        //
        // GET: /NewsFiles/Edit/5

        public ActionResult Edit(int id = 0)
        {
            NewsFiles newsfiles = db.NewsFileses.Find(id);
            if (newsfiles == null)
            {
                return HttpNotFound();
            }
            Member member =
                           db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }
            ViewBag.SiteCode = webSite.SiteCode;
            ViewBag.NewsId = newsfiles.NewId;
            return View(newsfiles);
        }

        //
        // POST: /NewsFiles/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(NewsFiles newsfiles)
        {
            if (ModelState.IsValid)
            {

                //db.Entry(newsfiles).State = EntityState.Modified;
                newsfiles.Update();
                ViewBag.close = "true";
                ViewBag.NewsId = newsfiles.NewId;
                return View(newsfiles);
            }
            ViewBag.NewsId = newsfiles.NewId;
            return View(newsfiles);
        }

        //
        // GET: /NewsFiles/Delete/5

        public ActionResult Delete(int fileid = 0)
        {
            NewsFiles newsfiles = db.NewsFileses.Find(fileid);
            if (newsfiles == null)
            {
                return HttpNotFound();
            }
            return View(newsfiles);
        }

        //
        // POST: /NewsFiles/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int fileid)
        {
            NewsFiles newsfiles = db.NewsFileses.Find(fileid);
            int newsId = (int)newsfiles.NewId;
            db.NewsFileses.Remove(newsfiles);
            db.SaveChanges();
            return RedirectToAction("Edit", "News", new { Id = newsId, type = "1", pclass = Request["pclass"] });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

}
