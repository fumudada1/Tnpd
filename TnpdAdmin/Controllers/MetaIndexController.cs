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
using Newtonsoft.Json;

namespace Tnpd.Controllers
{

    [Authorize]
    public class MetaIndexController : Controller
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 15;
        //
        // GET: /MetaIndex/

        [PermissionFilters]
        public ActionResult Index(int? page)
        {


            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.MetaIndices.OrderBy(p=>p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        [PermissionFilters]
        public ActionResult Index(string Subject, int? page )
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.MetaIndices.OrderBy(p => p.ListNum).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        [HttpPost]
        [PermissionFilters]
        public ActionResult Sort(string sortData)
        {
            if (!string.IsNullOrEmpty(sortData))
            {
                string[] tempDatas = sortData.TrimEnd(',').Split(',');
                foreach (string tempData in tempDatas)
                {
                    string[] itemDatas = tempData.Split(':');
                    MetaIndex metaindex = db.MetaIndices.Find(Convert.ToInt16(itemDatas[0]));
                    metaindex.ListNum = Convert.ToInt16(itemDatas[1]) ;

                    //db.Entry(publish).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /MetaIndex/Details/5
        [PermissionFilters]
        public ActionResult Details(int id = 0)
        {
            MetaIndex metaindex = db.MetaIndices.Find(id);
            if (metaindex == null)
            {
                return HttpNotFound();
            }
            return View(metaindex);
        }

        //
        // GET: /MetaIndex/Create
        [PermissionFilters]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MetaIndex/Create
        [PermissionFilters]
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(MetaIndex metaindex )
        {
            if (ModelState.IsValid)
            {

                db.MetaIndices.Add(metaindex);
                metaindex.Create(db,db.MetaIndices);
                return RedirectToAction("Index");
            }

            return View(metaindex);
        }

        //
        // GET: /MetaIndex/Edit/5
        [PermissionFilters]
        public ActionResult Edit(int id = 0)
        {
            MetaIndex metaindex = db.MetaIndices.Find(id);
            if (metaindex == null)
            {
                return HttpNotFound();
            }
            return View(metaindex);
        }

        //
        // POST: /MetaIndex/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionFilters]
        public ActionResult Edit(MetaIndex metaindex)
        {
            if (ModelState.IsValid)
            {

               //db.Entry(metaindex).State = EntityState.Modified;
                metaindex.Update();
                return RedirectToAction("Index");
            }
            return View(metaindex);
        }

        //
        // GET: /MetaIndex/Delete/5
        [PermissionFilters]
        public ActionResult Delete(int id = 0)
        {
            MetaIndex metaindex = db.MetaIndices.Find(id);
            if (metaindex == null)
            {
                return HttpNotFound();
            }
            return View(metaindex);
        }

        //
        // POST: /MetaIndex/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionFilters]
        public ActionResult DeleteConfirmed(int id)
        {
            MetaIndex metaindex = db.MetaIndices.Find(id);
            db.MetaIndices.Remove(metaindex);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //輸出treeView javascript Code 

        public JavaScriptResult TreeScript(int id = 0)
        {
            MetaIndex metaindex = db.MetaIndices.Find(id);
            string Cake = "";
            string Service = "";
            string Theme = "";
            if (metaindex != null)
            {
                Cake = metaindex.Cake;
                Theme = metaindex.Theme;
                Service = metaindex.Service;
            }

            string strCakeMenu = string.Format("var treeCakeData =[{0}]", Utility.GetMenu(Cake, "~/Config/cake.xml"));
            string strServiceMenu = string.Format("var treeServiceData =[{0}]", Utility.GetMenu(Service, "~/Config/service.xml"));
            string strThemeMenu = string.Format("var treeThemeData =[{0}]", Utility.GetMenu(Theme, "~/Config/theme.xml"));
            string treeScript = System.IO.File.ReadAllText(Server.MapPath("~/Config/PermissionTree.js"));
            string treeService = treeScript.Replace("tree3", "treeService").Replace("treeData", "treeServiceData").Replace("#Permission", "#Service");
            string treeCake = treeScript.Replace("tree3", "treeCake").Replace("treeData", "treeCakeData").Replace("#Permission", "#Cake");
            string treeTheme = treeScript.Replace("tree3", "treeTheme").Replace("treeData", "treeThemeData").Replace("#Permission", "#Theme");



            return JavaScript(strCakeMenu + treeService + strServiceMenu + treeTheme + strThemeMenu + treeCake);

        }

        /// <summary>
        /// 取得分類檢索
        /// </summary>
        /// <param name="id">網站編號</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetJson(string id)
        {
            if (id == null)
            {
                return new ContentResult { Content = "Parameters Error", ContentType = "application/plain" };
            }
            var item = db.MetaIndices.Select(
                            c => new
                            {
                                Id = c.Id,
                                Subject = c.Subject,
                                ListNum = c.ListNum
                            }
                    ).OrderBy(o => o.ListNum);

            string jsonContent = JsonConvert.SerializeObject(item.ToList(), Formatting.Indented);
            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

}
