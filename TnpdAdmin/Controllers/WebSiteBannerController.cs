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
    public class WebSiteBannerController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 55;
        //

        


        public ActionResult Index(int? page, FormCollection fc )
        {
			//記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);
            
            return View(_db.WebSiteNames.Where(x=>x.Enable==BooleanType.是 && x.SiteCode !="tnpd").OrderByDescending(p=>p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        

        //
        // GET: /WebSiteBanner/Details/5

        public ActionResult Details(int id = 0)
        {
            WebSiteName websitename = _db.WebSiteNames.Find(id);
            if (websitename == null)
            {
                return HttpNotFound();
            }
            return View(websitename);
        }



        //
        // GET: /WebSiteBanner/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WebSiteName websitename = _db.WebSiteNames.Find(id);
            if (websitename == null)
            {
                return HttpNotFound();
            }
            return View(websitename);
        }

        //
        // POST: /WebSiteBanner/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(WebSiteName websitename,HttpPostedFileBase UpImageUrls)
        {
            if (ModelState.IsValid)
            {
                if (UpImageUrls != null){ 
                    if (UpImageUrls.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1) 
                   { 
                        ViewBag.Message = "檔案型態錯誤!"; 
                        return View(websitename); 
                    } 
 
                    websitename.UpImageUrl = Utility.SaveUpImage(UpImageUrls); 
                    Utility.GenerateThumbnailImage(websitename.UpImageUrl, UpImageUrls.InputStream, Server.MapPath("~/upfiles/images"), "S", 127, 127); 
                } 
                System.Threading.Thread.Sleep(1000); 

               //_db.Entry(websitename).State = EntityState.Modified;
                websitename.Update();
                return RedirectToAction("Index",new{Page=-1});
            }
            return View(websitename);
        }

        //
        // GET: /WebSiteBanner/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WebSiteName websitename = _db.WebSiteNames.Find(id);
            if (websitename == null)
            {
                return HttpNotFound();
            }
            return View(websitename);
        }

        //
        // POST: /WebSiteBanner/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WebSiteName websitename = _db.WebSiteNames.Find(id);
            _db.WebSiteNames.Remove(websitename);
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
