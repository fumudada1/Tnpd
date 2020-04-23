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
    public class AboutLinkController : _BaseController
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

  var aboutLinkCatalogs = _db.AboutLinkCatalogs.OrderBy(x => x.ListNum).AsQueryable();
            var aboutlinks = _db.AboutLinks.Include(a => a.Catalog).OrderByDescending(p => p.ListNum).AsQueryable();

            if (hasViewData("SearchByCategoryId"))
            {
                int searchByCategoryId = getViewDateInt("SearchByCategoryId");
                aboutlinks = aboutlinks.Where(w => w.CategoryId == searchByCategoryId);
            }
          

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
                if (webSite == null)
                {
                    webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
                }
                aboutlinks = aboutlinks.Where(x => x.Catalog.WebSite.Id == webSite.Id);
                aboutLinkCatalogs = aboutLinkCatalogs.Where(x => x.WebSiteId == webSite.Id);
            }
            var aboutLinkCatalogs1 = aboutLinkCatalogs.Select(x => new
            {
                Id = x.Id,
                Subject = x.WebSite.Subject + x.Subject,
                ListNum = x.ListNum,
                WebSiteId = x.WebSiteId
            });
            ViewBag.CategoryId = new SelectList(aboutLinkCatalogs1, "Id", "Subject");


            //            ViewBag.Subject = Subject;
            return View(aboutlinks.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

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
                    AboutLink aboutlink = _db.AboutLinks.Find(Convert.ToInt16(itemDatas[0]));
                    aboutlink.ListNum = Convert.ToInt16(itemDatas[1]);

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }


        //
        // GET: /AboutLink/Details/5

        public ActionResult Details(int id = 0)
        {
            AboutLink aboutlink = _db.AboutLinks.Find(id);
            if (aboutlink == null)
            {
                //return HttpNotFound();
                return View();
            }
            return View(aboutlink);
        }

        //
        // GET: /AboutLink/Create

        public ActionResult Create()
        {
            var aboutLinkCatalogs = _db.AboutLinkCatalogs.OrderBy(x => x.ListNum).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
                if (webSite == null)
                {
                    webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
                }
              
                aboutLinkCatalogs = aboutLinkCatalogs.Where(x => x.WebSiteId == webSite.Id);
            }
            var aboutLinkCatalogs1 = aboutLinkCatalogs.Select(x => new
            {
                Id = x.Id,
                Subject = x.WebSite.Subject + x.Subject,
                ListNum = x.ListNum,
                WebSiteId = x.WebSiteId
            });
            ViewBag.CategoryId = new SelectList(aboutLinkCatalogs1, "Id", "Subject");
            return View();
        }

        //
        // POST: /AboutLink/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(AboutLink aboutlink, HttpPostedFileBase UpImageUrls)
        {
            if (ModelState.IsValid)
            {


                _db.AboutLinks.Add(aboutlink);
                int maxListNum = 0;
                if ((_db.AboutLinks.Any()))
                {
                    maxListNum = _db.AboutLinks.Max(x => x.ListNum);
                }
                aboutlink.ListNum = maxListNum + 1;
                aboutlink.Create(_db, _db.AboutLinks);
                return RedirectToAction("Index");
            }
            var aboutLinkCatalogs = _db.AboutLinkCatalogs.OrderBy(x => x.ListNum).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
                if (webSite == null)
                {
                    webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
                }
              
                aboutLinkCatalogs = aboutLinkCatalogs.Where(x => x.WebSiteId == webSite.Id);
            }
            var aboutLinkCatalogs1 = aboutLinkCatalogs.Select(x => new
            {
                Id = x.Id,
                Subject = x.WebSite.Subject + x.Subject,
                ListNum = x.ListNum,
                WebSiteId = x.WebSiteId
            });
            ViewBag.CategoryId = new SelectList(aboutLinkCatalogs1, "Id", "Subject");
            return View(aboutlink);
        }

        //
        // GET: /AboutLink/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AboutLink aboutlink = _db.AboutLinks.Find(id);
            if (aboutlink == null)
            {
                return HttpNotFound();
            }
            var aboutLinkCatalogs = _db.AboutLinkCatalogs.OrderBy(x => x.ListNum).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
                if (webSite == null)
                {
                    webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
                }
                
                aboutLinkCatalogs = aboutLinkCatalogs.Where(x => x.WebSiteId == webSite.Id);
            }
            var aboutLinkCatalogs1 = aboutLinkCatalogs.Select(x => new
            {
                Id = x.Id,
                Subject = x.WebSite.Subject + x.Subject,
                ListNum = x.ListNum,
                WebSiteId = x.WebSiteId
            });
            ViewBag.CategoryId = new SelectList(aboutLinkCatalogs1, "Id", "Subject", aboutlink.CategoryId);
           
            return View(aboutlink);
        }

        //
        // POST: /AboutLink/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(AboutLink aboutlink)
        {
            if (ModelState.IsValid)
            {


                //_db.Entry(aboutlink).State = EntityState.Modified;
                aboutlink.Update();
                return RedirectToAction("Index", new { Page = -1 });
            }
            var aboutLinkCatalogs = _db.AboutLinkCatalogs.OrderBy(x => x.ListNum).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                WebSiteName webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId);
                if (webSite == null)
                {
                    webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
                }

                aboutLinkCatalogs = aboutLinkCatalogs.Where(x => x.WebSiteId == webSite.Id);
            }
            var aboutLinkCatalogs1 = aboutLinkCatalogs.Select(x => new
            {
                Id = x.Id,
                Subject = x.WebSite.Subject + x.Subject,
                ListNum = x.ListNum,
                WebSiteId = x.WebSiteId
            });
            ViewBag.CategoryId = new SelectList(aboutLinkCatalogs1, "Id", "Subject", aboutlink.CategoryId);
            return View(aboutlink);
        }

        //
        // GET: /AboutLink/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AboutLink aboutlink = _db.AboutLinks.Find(id);
            if (aboutlink == null)
            {
                return HttpNotFound();
            }
            return View(aboutlink);
        }

        //
        // POST: /AboutLink/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutLink aboutlink = _db.AboutLinks.Find(id);
            _db.AboutLinks.Remove(aboutlink);
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
