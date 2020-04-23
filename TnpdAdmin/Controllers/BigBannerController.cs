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
    public class BigBannerController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 150;
        //

        


        public ActionResult Index(int? page, FormCollection fc )
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);


            var bigbanners = _db.BigBanners.Include(b => b.WebSite).AsQueryable();
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
                bigbanners = bigbanners.Where(w => w.WebSiteNameId == webSite.Id);
            }

            ViewBag.WebSiteNameId = new SelectList(websites, "Id", "Subject", webSite.Id);


            if (hasViewData("SearchByWebSiteNameId"))
            {
                int searchByWebSiteNameId = getViewDateInt("SearchByWebSiteNameId");
                bigbanners = bigbanners.Where(w => w.WebSiteNameId == searchByWebSiteNameId);
                Session["BannersWebSiteNameId"] = searchByWebSiteNameId;
            }
            else
            {
                bigbanners = bigbanners.Where(w => w.WebSiteNameId == webSite.Id);

            }
//            ViewBag.Subject = Subject;
            return View(bigbanners.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));

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
                    BigBanner bigbanner = _db.BigBanners.Find(Convert.ToInt16(itemDatas[0]));
                    bigbanner.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //_db.Entry(publish).State = EntityState.Modified;
                    _db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /BigBanner/Details/5

        public ActionResult Details(int id = 0)
        {
            BigBanner bigbanner = _db.BigBanners.Find(id);
            if (bigbanner == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(bigbanner);
        }

        //
        // GET: /BigBanner/Create

        public ActionResult Create()
        {
            if (Session["BannersWebSiteNameId"]!=null)
            {
                int WebSiteNameId =Convert.ToInt32(Session["BannersWebSiteNameId"]) ;
               
                ViewBag.WebSiteNameId =
                    new SelectList(_db.WebSiteNames.Where(x => x.Language == LanguageType.中文版).OrderBy(p => p.ListNum),
                        "Id", "Subject", WebSiteNameId);
            }
            else
            {
                ViewBag.WebSiteNameId =
                    new SelectList(_db.WebSiteNames.Where(x => x.Language == LanguageType.中文版).OrderBy(p => p.ListNum),
                        "Id", "Subject");
            }
            
            return View();
        }

        //
        // POST: /BigBanner/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(BigBanner bigbanner ,HttpPostedFileBase UpImages)
        {
            if (ModelState.IsValid)
            {
                if (UpImages != null){ 
                    if (UpImages.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.WebSiteNameId = new SelectList(_db.WebSiteNames.Where(x => x.Language == LanguageType.中文版).OrderBy(p => p.ListNum), "Id", "Subject", bigbanner.WebSiteNameId);
                        return View(bigbanner); 
                    } 
 
                    bigbanner.UpImage = Utility.SaveUpImage(UpImages);
                    
                    //Utility.GenerateThumbnailImageGinny(bigbanner.UpImage, UpImages.InputStream, Server.MapPath("~/upfiles/images"), "M", 1100, 500);
                    Utility.GenerateThumbnailImageGinny(bigbanner.UpImage, UpImages.InputStream, Server.MapPath("~/upfiles/images"), "S", 200, 127); 
                } 
                System.Threading.Thread.Sleep(1000); 

                _db.BigBanners.Add(bigbanner);
	  int maxListNum = 0;
      if (( _db.BigBanners.Any()))
      {
		 maxListNum = _db.BigBanners.Max(x => x.ListNum) ;
      }                
				bigbanner.ListNum = maxListNum +1; 
                bigbanner.Create(_db,_db.BigBanners);
                return RedirectToAction("Index");
            }

            ViewBag.WebSiteNameId = new SelectList(_db.WebSiteNames.Where(x => x.Language == LanguageType.中文版).OrderBy(p => p.ListNum), "Id", "Subject", bigbanner.WebSiteNameId);
            return View(bigbanner);
        }

        //
        // GET: /BigBanner/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BigBanner bigbanner = _db.BigBanners.Find(id);
            if (bigbanner == null)
            {
                return HttpNotFound();
            }
            ViewBag.WebSiteNameId = new SelectList(_db.WebSiteNames.Where(x => x.Language == LanguageType.中文版).OrderBy(p => p.ListNum), "Id", "Subject", bigbanner.WebSiteNameId);
            return View(bigbanner);
        }

        //
        // POST: /BigBanner/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(BigBanner bigbanner,HttpPostedFileBase UpImages)
        {
            if (ModelState.IsValid)
            {
                if (UpImages != null){ 
                    if (UpImages.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.WebSiteNameId = new SelectList(_db.WebSiteNames.Where(x => x.Language == LanguageType.中文版).OrderBy(p => p.ListNum), "Id", "Subject", bigbanner.WebSiteNameId);
                        return View(bigbanner); 
                    } 
 
                    bigbanner.UpImage = Utility.SaveUpImage(UpImages);
                    //Utility.GenerateThumbnailImage(bigbanner.UpImage, UpImages.InputStream, Server.MapPath("~/upfiles/images"), "M", 1000, 500);
                    Utility.GenerateThumbnailImage(bigbanner.UpImage, UpImages.InputStream, Server.MapPath("~/upfiles/images"), "S", 200, 127); 
                } 
                System.Threading.Thread.Sleep(1000); 

               //_db.Entry(bigbanner).State = EntityState.Modified;
                bigbanner.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            ViewBag.WebSiteNameId = new SelectList(_db.WebSiteNames.Where(x => x.Language == LanguageType.中文版).OrderBy(p => p.ListNum), "Id", "Subject", bigbanner.WebSiteNameId);
            return View(bigbanner);
        }

        //
        // GET: /BigBanner/Delete/5

        public ActionResult Delete(int id = 0)
        {
            BigBanner bigbanner = _db.BigBanners.Find(id);
            if (bigbanner == null)
            {
                return HttpNotFound();
            }
            return View(bigbanner);
        }

        //
        // POST: /BigBanner/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BigBanner bigbanner = _db.BigBanners.Find(id);
            _db.BigBanners.Remove(bigbanner);
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
