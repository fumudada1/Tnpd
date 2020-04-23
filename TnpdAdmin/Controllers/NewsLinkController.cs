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
    public class NewsLinkController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 15;
        //
        // GET: /NewsLink/

        public ActionResult Index(int? page)
        {


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var newslinks = db.NewsLinks.Include(n => n.News).OrderByDescending(p=>p.ListNum);
            ViewBag.NewId = new SelectList(db.Newses.OrderBy(p=>p.InitDate), "Id", "Subject");
            return View(newslinks.OrderByDescending(p=>p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        public ActionResult Index(string Subject, System.Int32? NewId, TnpdModels.TargetType? Target, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var newslinks = db.NewsLinks.Include(n => n.News).OrderByDescending(p => p.ListNum).AsQueryable();
            ViewBag.NewId = new SelectList(db.Newses.OrderBy(p=>p.InitDate), "Id", "Subject");
            if (!string.IsNullOrEmpty(Subject)) 
            { 
                newslinks = newslinks.Where(w => w.Subject.Contains(Subject)); 
            } 
 
            if (NewId.HasValue) 
            { 
                newslinks = newslinks.Where(w => w.NewId == NewId); 
            } 
            if (Target.HasValue) 
            { 
                newslinks = newslinks.Where(w => w.Target == Target); 
            } 

            ViewBag.Subject = Subject;
            return View(newslinks.OrderByDescending(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
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
                    NewsLink newslink = db.NewsLinks.Find(Convert.ToInt16(itemDatas[0]));
                    newslink.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //db.Entry(publish).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            ViewBag.state = "sort";
            return RedirectToAction("Edit", "News", new { Id = Id, type = "2", state = "sort", pclass = Request["pclass"] });
        }
        

        //
        // GET: /NewsLink/Details/5

        public ActionResult Details(int id = 0)
        {
            NewsLink newslink = db.NewsLinks.Find(id);
            if (newslink == null)
            {
                return HttpNotFound();
            }
            return View(newslink);
        }

        //
        // GET: /NewsLink/Create

        public ActionResult Create(string NewsId)
        {
            ViewBag.NewsId = NewsId;
            return View();
        }

        //
        // POST: /NewsLink/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(NewsLink newslink )
        {
            if (ModelState.IsValid)
            {

                db.NewsLinks.Add(newslink);
                newslink.Create(db,db.NewsLinks);
                ViewBag.close = "true";
                ViewBag.NewsId = newslink.NewId;
            }

            ViewBag.NewsId = newslink.NewId;
            return View(newslink);
        }

        //
        // GET: /NewsLink/Edit/5

        public ActionResult Edit(int id = 0)
        {
            NewsLink newslink = db.NewsLinks.Find(id);
            if (newslink == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewId = newslink.NewId;
            return View(newslink);
        }

        //
        // POST: /NewsLink/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(NewsLink newslink)
        {
            if (ModelState.IsValid)
            {

               //db.Entry(newslink).State = EntityState.Modified;
                newslink.Update();
                ViewBag.close = "true";
                ViewBag.NewsId = newslink.NewId;
                return View(newslink);
            }
            ViewBag.NewsId = newslink.NewId;
            return View(newslink);
        }

        //
        // GET: /NewsLink/Delete/5

        public ActionResult Delete(int linkid = 0)
        {
            NewsLink newslink = db.NewsLinks.Find(linkid);
            if (newslink == null)
            {
                return HttpNotFound();
            }
            return View(newslink);
        }

        //
        // POST: /NewsLink/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int linkid)
        {
            NewsLink newslink = db.NewsLinks.Find(linkid);
            int newsId = (int)newslink.NewId;
            db.NewsLinks.Remove(newslink);
            db.SaveChanges();
            return RedirectToAction("Edit", "News", new { Id = newsId, type = "2", pclass = Request["pclass"] });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

}
