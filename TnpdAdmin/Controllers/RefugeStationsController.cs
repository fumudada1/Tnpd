using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using Newtonsoft.Json;
using Tnpd.Controllers;
using Tnpd.Filters;
using Tnpd.Models;
using TnpdAdmin.Models;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
	[PermissionFilters]
    [Authorize]
    public class RefugeStationsController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 15;
        //

        


        public ActionResult Index(int? page, FormCollection fc )
        {

           
            

			//記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            string searchByUnits = "";
            var refugestations = _db.refugeStations.AsQueryable();
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }

            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者") || x.Subject.Contains("防空疏散避難設施地圖管理者")))
            {
                refugestations = refugestations.Where(x => x.Precinct==webSite.Subject);
            }
            else
            {
                ViewBag.admin = true;
                if (hasViewData("SearchByUnits"))
                { 
                    searchByUnits = getViewDateStr("SearchByUnits");
                    refugestations = refugestations.Where(w => w.Precinct == searchByUnits);
                }
            }

            if (hasViewData("SearchBySubject")) 
            { 
                string searchBySubject = getViewDateStr("SearchBySubject");
                refugestations = refugestations.Where(w => w.Subject.Contains(searchBySubject) || w.Address.Contains(searchBySubject) || w.DIstrict.Contains(searchBySubject) || w.Village.Contains(searchBySubject)); 
            }
            
            var units = _db.Units.Where(x => x.Subject.Contains("分局") && x.ParentId == null).OrderBy(x => x.ListNum).Select(x => new
            {
                id = x.Subject.Substring(3, x.Subject.Length - 3),
                subject = x.Subject.Substring(3, x.Subject.Length - 3),
            }).ToList();

            var SearchByUnits = new SelectList(units, "id", "Subject",searchByUnits);
            ViewBag.SearchByUnits = SearchByUnits;

//            ViewBag.Subject = Subject;
            return View(refugestations.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        
        public ActionResult GetPointByAddress(string address)
        {

            string targetURI = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyC631kTAWg6pT3b3w3sFaD8xhxxASuM7xI&sensor=false&language=zh-tw";
            var request = WebRequest.Create(targetURI);
            request.ContentType = "application/json; charset=utf-8";
            string text;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            var gjsons = JsonConvert.DeserializeObject<GJson>(text);

            
            if (gjsons.results.Count() > 0)
            {
                var geometry = gjsons.results[0].geometry;
                var gResult = new
                {
                    twd97X = geometry.location.lat,
                    twd97Y = geometry.location.lng
                };

                return Json(gResult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var gResult = new
                {
                    twd97X = "",
                    twd97Y = ""
                };
                return Json(gResult, JsonRequestBehavior.AllowGet);
            }

            

        }

        

        //
        // GET: /RefugeStations/Details/5

        public ActionResult Details(int id = 0)
        {
            RefugeStation refugestation = _db.refugeStations.Find(id);
            if (refugestation == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(refugestation);
        }

        //
        // GET: /RefugeStations/Create

        public ActionResult Create()
        {
            

            var units = _db.Units.Where(x => x.Subject.Contains("分局") && x.ParentId == null).OrderBy(x => x.ListNum).Select(x => new
            {
                id = x.Subject.Substring(3, x.Subject.Length - 3),
                subject = x.Subject.Substring(3, x.Subject.Length - 3),
            }).ToList();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }

            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者") || x.Subject.Contains("防空疏散避難設施地圖管理者")))
            {
                units = units.Where(x => x.subject == webSite.Subject).ToList();
            }
           

            

            var SearchByUnits = new SelectList(units, "id", "Subject");
            ViewBag.Precinct = SearchByUnits;

            return View();
        }

        //
        // POST: /RefugeStations/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(RefugeStation refugestation )
        {
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (ModelState.IsValid)
            {

                _db.refugeStations.Add(refugestation);
                refugestation.Create(_db,_db.refugeStations);
                string mailBody = @"區別：" + refugestation.DIstrict + @"<br/>
里別：" + refugestation.Village + @"<br/>
名稱：" + refugestation.Subject + @"<br/>
防空疏散避難設施地址：" + refugestation.Address + @"<br/>
可容納人數：" + refugestation.Number + @"<br/>
管轄分局：" + refugestation.Precinct + @"<br/>
x座標：" + refugestation.Twd97X + @"<br/>
y座標：" + refugestation.Twd97Y + @"<br/>
建立時間:" + DateTime.Now + @"<br/>
建立者：" + member.MyUnit.ParentUnit.Subject + @"-" + member.Name + @"<br/>";
                Utility.SystemSendMail(ConfigurationManager.AppSettings["RefugeStationsMailFrom"], "防空疏散避難設施地圖-資料建立通知", mailBody);
                return RedirectToAction("Index");
            }
            var units = _db.Units.Where(x => x.Subject.Contains("分局") && x.ParentId == null).OrderBy(x => x.ListNum).Select(x => new
            {
                id = x.Subject.Substring(3, x.Subject.Length - 3),
                subject = x.Subject.Substring(3, x.Subject.Length - 3),
            }).ToList();
           
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }

            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者") || x.Subject.Contains("防空疏散避難設施地圖管理者")))
            {
                units = units.Where(x => x.subject == webSite.Subject).ToList();
            }
            var SearchByUnits = new SelectList(units, "id", "Subject");
            ViewBag.Precinct = SearchByUnits;
            
            return View(refugestation);
        }

        //
        // GET: /RefugeStations/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RefugeStation refugestation = _db.refugeStations.Find(id);
            if (refugestation == null)
            {
                return HttpNotFound();
            }
            var units = _db.Units.Where(x => x.Subject.Contains("分局") && x.ParentId == null).OrderBy(x => x.ListNum).Select(x => new
            {
                id = x.Subject.Substring(3, x.Subject.Length - 3),
                subject = x.Subject.Substring(3, x.Subject.Length - 3),
            }).ToList();
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }

            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者") || x.Subject.Contains("防空疏散避難設施地圖管理者")))
            {
                units = units.Where(x => x.subject == webSite.Subject).ToList();
            }
            var SearchByUnits = new SelectList(units, "id", "Subject",refugestation.Precinct);
            ViewBag.Precinct = SearchByUnits;
            return View(refugestation);
        }

        //
        // POST: /RefugeStations/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(RefugeStation refugestation)
        {
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (ModelState.IsValid)
            {
               

               //_db.Entry(refugestation).State = EntityState.Modified;
                refugestation.Update();

                string mailBody = @"區別：" + refugestation.DIstrict + @"<br/>
里別：" + refugestation.Village + @"<br/>
名稱：" + refugestation.Subject + @"<br/>
防空疏散避難設施地址：" + refugestation.Address + @"<br/>
可容納人數：" + refugestation.Number + @"<br/>
管轄分局：" + refugestation.Precinct + @"<br/>
x座標：" + refugestation.Twd97X + @"<br/>
y座標：" + refugestation.Twd97Y + @"<br/>
更新時間:" + DateTime.Now + @"<br/>
更新者：" + member.MyUnit.ParentUnit.Subject + @"-" + member.Name + @"<br/>";

                Utility.SystemSendMail(ConfigurationManager.AppSettings["RefugeStationsMailFrom"], "防空疏散避難設施地圖-資料更新通知",mailBody);
                return RedirectToAction("Index",new{Page=-1});
            }
            var units = _db.Units.Where(x => x.Subject.Contains("分局") && x.ParentId == null).OrderBy(x => x.ListNum).Select(x => new
            {
                id = x.Subject.Substring(3, x.Subject.Length - 3),
                subject = x.Subject.Substring(3, x.Subject.Length - 3),
            }).ToList();

            var SearchByUnits = new SelectList(units, "id", "Subject", refugestation.Precinct);
           
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }

            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者") || x.Subject.Contains("防空疏散避難設施地圖管理者")))
            {
                units = units.Where(x => x.subject == webSite.Subject).ToList();
            }
            ViewBag.Precinct = SearchByUnits;
            return View(refugestation);
        }

        //
        // GET: /RefugeStations/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RefugeStation refugestation = _db.refugeStations.Find(id);
            if (refugestation == null)
            {
                return HttpNotFound();
            }
            return View(refugestation);
        }

        //
        // POST: /RefugeStations/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            RefugeStation refugestation = _db.refugeStations.Find(id);
            _db.refugeStations.Remove(refugestation);
            string mailBody = @"區別：" + refugestation.DIstrict + @"<br/>
里別：" + refugestation.Village + @"<br/>
名稱：" + refugestation.Subject + @"<br/>
防空疏散避難設施地址：" + refugestation.Address + @"<br/>
可容納人數：" + refugestation.Number + @"<br/>
管轄分局：" + refugestation.Precinct + @"<br/>
x座標：" + refugestation.Twd97X + @"<br/>
y座標：" + refugestation.Twd97Y + @"<br/>
刪除時間:" + DateTime.Now + @"<br/>
刪除者：" + member.MyUnit.ParentUnit.Subject + @"-" + member.Name + @"<br/>";
            Utility.SystemSendMail(ConfigurationManager.AppSettings["RefugeStationsMailFrom"], "防空疏散避難設施地圖-資料刪除通知", mailBody);
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
