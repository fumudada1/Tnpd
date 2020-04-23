using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using Tnpd.Filters;
using Tnpd.Models;
using TnpdModels;

namespace Tnpd.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class HomeThemeController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        
        private const int DefaultPageSize = 15;
        //

        


        public ActionResult Index(int? page, FormCollection fc )
        {
           Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            int siteID = 1;
            var webSite = _db.WebSiteNames.Where(x => x.Enable == BooleanType.是);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                //string SiteCode = member.Roles.FirstOrDefault().SiteCode;
                //var listSiteCode = member.Roles.Select(d => d.SiteCode).ToList();
                //webSite = webSite.Where(x => listSiteCode.Contains(x.SiteCode));
                //siteID = _db.WebSiteNames.Where(x => x.SiteCode == SiteCode).OrderBy(x => x.Id).FirstOrDefault().Id;
            }


            var webSiteNames = webSite.Select(
                              c => new
                              {
                                  Id = c.Id,
                                  Subject = c.Subject,
                                  code = c.SiteCode
                              }
                      ).OrderBy(o => o.Id);

			//記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);
            if (hasViewData("SearchBySite"))
            {
                if (getViewDateInt("SearchBySite") != 0)
                {
                    siteID = Convert.ToInt16(getViewDateStr("SearchBySite"));
                }

            }
            var HomeThemes = _db.HomeThemes.OrderBy(p => p.ListNum).AsQueryable();
            HomeThemes = HomeThemes.Where(w => w.WebSiteNameId == siteID);

            ViewBag.SearchBySite = new SelectList(webSiteNames, "Id", "Subject", siteID);
            return View(HomeThemes.ToPagedList(currentPageIndex, DefaultPageSize));
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
                    HomeTheme hometheme = _db.HomeThemes.Find(Convert.ToInt16(itemDatas[0]));
                    hometheme.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /HomeTheme/Details/5

        public ActionResult Details(int id = 0)
        {
            HomeTheme hometheme = _db.HomeThemes.Find(id);
            if (hometheme == null)
            {
                return HttpNotFound();
            }
            return View(hometheme);
        }

        //
        // GET: /HomeTheme/Create

        public ActionResult Create()
        {
           Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            int siteID = 1;
            var webSite = _db.WebSiteNames.Where(x => x.Enable == BooleanType.是);
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
            //    string SiteCode = member.Roles.FirstOrDefault().SiteCode;
            //    var listSiteCode = member.Roles.Select(d => d.SiteCode).ToList();
            //    webSite = webSite.Where(x => listSiteCode.Contains(x.SiteCode));
            //    siteID = _db.WebSiteNames.Where(x => x.SiteCode == SiteCode).OrderBy(x => x.Id).FirstOrDefault().Id;
            }


            var webSiteNames = webSite.Select(
                              c => new
                              {
                                  Id = c.Id,
                                  Subject = c.Subject,
                                  code = c.SiteCode
                              }
                      ).OrderBy(o => o.Id);

            //ViewBag.WebSiteNameId = siteID;
            ViewBag.WebSiteNameId = new SelectList(webSiteNames, "Id", "Subject", siteID);
            return View();
        }

        //
        // POST: /HomeTheme/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(HomeTheme hometheme ,HttpPostedFileBase UpImages)
        {
            if (ModelState.IsValid)
            {
                if (UpImages != null){ 
                    if (UpImages.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!"; 
                        return View(hometheme); 
                    } 
 
                    hometheme.UpImage = Utility.SaveUpImage(UpImages); 
                    Utility.GenerateThumbnailImage(hometheme.UpImage, UpImages.InputStream, Server.MapPath("~/upfiles/images"), "S", 127, 127); 
                } 
                System.Threading.Thread.Sleep(1000); 

                _db.HomeThemes.Add(hometheme);
	  int maxListNum = 0;
      if (( _db.HomeThemes.Any()))
      {
		 maxListNum = _db.HomeThemes.Max(x => x.ListNum) ;
      }                
				hometheme.ListNum = maxListNum +1; 
                hometheme.Create(_db,_db.HomeThemes);
                return RedirectToAction("Index");
            }

            return View(hometheme);
        }

        //
        // GET: /HomeTheme/Edit/5

        public ActionResult Edit(int id = 0)
        {
            HomeTheme hometheme = _db.HomeThemes.Find(id);
            if (hometheme == null)
            {
                return HttpNotFound();
            }
            return View(hometheme);
        }

        //
        // POST: /HomeTheme/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(HomeTheme hometheme,HttpPostedFileBase UpImages)
        {
            if (ModelState.IsValid)
            {
                if (UpImages != null){ 
                    if (UpImages.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!"; 
                        return View(hometheme); 
                    } 
 
                    hometheme.UpImage = Utility.SaveUpImage(UpImages); 
                    Utility.GenerateThumbnailImage(hometheme.UpImage, UpImages.InputStream, Server.MapPath("~/upfiles/images"), "S", 127, 127); 
                } 
                System.Threading.Thread.Sleep(1000); 

               //_db.Entry(hometheme).State = EntityState.Modified;
                hometheme.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            return View(hometheme);
        }

        //
        // GET: /HomeTheme/Delete/5

        public ActionResult Delete(int id = 0)
        {
            HomeTheme hometheme = _db.HomeThemes.Find(id);
            if (hometheme == null)
            {
                return HttpNotFound();
            }
            return View(hometheme);
        }

        //
        // POST: /HomeTheme/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeTheme hometheme = _db.HomeThemes.Find(id);
            _db.HomeThemes.Remove(hometheme);
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
