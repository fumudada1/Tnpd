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
    public class HomeAdController : _BaseController
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
            var homeAD = _db.HomeAds.OrderBy(p => p.ListNum).AsQueryable();
            homeAD = homeAD.Where(w => w.WebSiteNameId == siteID);
            ViewBag.SearchBySite = new SelectList(webSiteNames, "Id", "Subject", siteID);
            return View(homeAD.ToPagedList(currentPageIndex, DefaultPageSize));
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
                    HomeAd homead = _db.HomeAds.Find(Convert.ToInt16(itemDatas[0]));
                    homead.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /HomeAd/Details/5

        public ActionResult Details(int id = 0)
        {
            HomeAd homead = _db.HomeAds.Find(id);
            if (homead == null)
            {
                return HttpNotFound();
            }
            return View(homead);
        }

        //
        // GET: /HomeAd/Create

        public ActionResult Create()
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

            //ViewBag.WebSiteNameId = siteID;
            ViewBag.WebSiteNameId = new SelectList(webSiteNames, "Id", "Subject", siteID);
            return View();
        }

        //
        // POST: /HomeAd/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(HomeAd homead ,HttpPostedFileBase UpImages)
        {
            if (ModelState.IsValid)
            {
                if (UpImages != null){ 
                    if (UpImages.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!"; 
                        return View(homead); 
                    } 
 
                    homead.UpImage = Utility.SaveUpImage(UpImages); 
                    Utility.GenerateThumbnailImage(homead.UpImage, UpImages.InputStream, Server.MapPath("~/upfiles/images"), "S", 127, 127); 
                } 
                System.Threading.Thread.Sleep(1000); 

                _db.HomeAds.Add(homead);
	  int maxListNum = 0;
      if (( _db.HomeAds.Any()))
      {
		 maxListNum = _db.HomeAds.Max(x => x.ListNum) ;
      }                
				homead.ListNum = maxListNum +1; 
                homead.Create(_db,_db.HomeAds);
                return RedirectToAction("Index");
            }

            return View(homead);
        }

        //
        // GET: /HomeAd/Edit/5

        public ActionResult Edit(int id = 0)
        {
            HomeAd homead = _db.HomeAds.Find(id);
            if (homead == null)
            {
                return HttpNotFound();
            }
            return View(homead);
        }

        //
        // POST: /HomeAd/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(HomeAd homead,HttpPostedFileBase UpImages)
        {
            if (ModelState.IsValid)
            {
                if (UpImages != null){ 
                    if (UpImages.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!"; 
                        return View(homead); 
                    } 
 
                    homead.UpImage = Utility.SaveUpImage(UpImages); 
                    Utility.GenerateThumbnailImage(homead.UpImage, UpImages.InputStream, Server.MapPath("~/upfiles/images"), "S", 127, 127); 
                } 
                System.Threading.Thread.Sleep(1000); 

               //_db.Entry(homead).State = EntityState.Modified;
                homead.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            return View(homead);
        }

        //
        // GET: /HomeAd/Delete/5

        public ActionResult Delete(int id = 0)
        {
            HomeAd homead = _db.HomeAds.Find(id);
            if (homead == null)
            {
                return HttpNotFound();
            }
            return View(homead);
        }

        //
        // POST: /HomeAd/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeAd homead = _db.HomeAds.Find(id);
            _db.HomeAds.Remove(homead);
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
