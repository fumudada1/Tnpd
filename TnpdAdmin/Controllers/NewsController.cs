using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common.CommandTrees;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using Tnpd.Filters;
using Tnpd.Helpers.DyLinq;
using Tnpd.Models;
using TnpdModels;


namespace Tnpd.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class NewsController : _BaseController
    {
        private BackendContext db = new BackendContext();


        private const int DefaultPageSize = 20;
        //


        public ActionResult Index(int? page, int pclass, FormCollection fc)
        {


            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }
            ViewBag.webSiteID = webSite.Id;


            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var newses = db.Newses.OrderByDescending(x => x.InitDate).AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")) )
            {
                newses = newses.Where(x => x.WebCategoryId ==pclass);
            }

            
            var newsCatalogs = db.NewsCatalogs.Where(x => x.WebCategoryId == pclass).AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")) && webSite.Id!=1)
            {
                newsCatalogs = newsCatalogs.Where(x => x.WebSiteId == webSite.Id);
                newses = newses.Where(w => w.OwnWebSiteId==webSite.Id||w.initOrg.Contains(webSite.Subject));
                //newses = newses.Where(w => w.OwnWebSiteId==member.MyUnit.ParentId);
            }

            var newsCataloglist = newsCatalogs.ToList();
            var newsCatalogs1 = newsCataloglist.Select(x => new
            {
                Id = x.Id,
                webid = x.WebSiteId,
                Subject = x.WebSite.Subject + "-" + x.Subject
            }).OrderBy(x => x.webid);

            ViewBag.CategoryId = new SelectList(newsCatalogs1, "Id", "Subject");



            //ViewBag.CategoryName = db.NewsCatalogs.FirstOrDefault(x => x.Id == CategoryId).Subject;
            if (hasViewData("SearchBySubject"))
            {
                string searchByTitle = getViewDateStr("SearchBySubject");
                newses = newses.Where(w => w.Subject.Contains(searchByTitle) ||  w.Article.Contains(searchByTitle) );
            }

            int siteID = 1;
            if (hasViewData("SearchBySite"))
            {
                if (getViewDateInt("SearchBySite") != 0)
                {
                    siteID = Convert.ToInt16(getViewDateStr("SearchBySite"));
                    newses = newses.Where(w => w.NewsCatalogs.Count(x => x.WebSiteId == siteID) > 0);
                }

            }


            if (hasViewData("SearchByCategoryId"))
            {
                int categoryId = getViewDateInt("SearchByCategoryId");
                newses = newses.Where(x => x.NewsCatalogs.Count(y => y.Id == categoryId) > 0);
            }


            if (hasViewData("SearchByStartDate") && hasViewData("SearchByEndDate"))
            {
                DateTime startDate = Convert.ToDateTime(getViewDateStr("SearchByStartDate"));
                DateTime endDate = Convert.ToDateTime(getViewDateStr("SearchByEndDate")).AddDays(1);
                newses = newses.Where(w => w.InitDate >= startDate && w.InitDate <= endDate);
            }
            string SearchBySortField = "initDate";     //預設的排序欄位
            if (hasViewData("SearchBySortField"))
            {
                SearchBySortField = getViewDateStr("SearchBySortField");
            }
            string SearchBySortType = "Desc";
            if (hasViewData("SearchBySortType"))        //預設的排序方式
            {
                SearchBySortType = getViewDateStr("SearchBySortType");
            }

            var ls2 = DynamicLinqExpressions.OrderBy(newses.AsQueryable(), SearchBySortField, SearchBySortType);


            //ViewBag.SearchBySite = new SelectList(webSiteNames, "Id", "Subject", siteID);


            WebNewsCatalog catalog = db.WebNewsCatalogs.Find(pclass);

            ViewBag.Title = catalog.Subject;

            //ViewBag.Subject = getViewDateStr("SearchBySubject");
            return View(ls2.ToPagedList(currentPageIndex, DefaultPageSize));
        }


        // GET: /News/Details/5

        public ActionResult Details(int id = 0)
        {
            News news = db.Newses.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // GET: /News/Create

        public ActionResult Create(int pclass)
        {




            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }

            var newsCatalogs = db.NewsCatalogs.Where(x => x.WebCategoryId == pclass).AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")) && webSite.Id != 1)
            {
                newsCatalogs = newsCatalogs.Where(x => x.WebSiteId == webSite.Id || x.WebSiteId == 1);
            }
            //if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")) && webSite.Id != 1)
            //{
            //    newsCatalogs = newsCatalogs.Where(x => x.WebSiteId == webSite.Id);
            //}
            var newsCatalogs1 = newsCatalogs.OrderBy(x => x.WebSiteId).ToList();

            ViewBag.newsCatalogs = newsCatalogs1;

            WebNewsCatalog catalog = db.WebNewsCatalogs.Find(pclass);

            ViewBag.Title = catalog.Subject;
            ViewBag.SiteCode = webSite.SiteCode;

            //ViewBag.WebSiteNameId = siteID;
            News news = new News()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6)
            };


            return View(news);
        }

        //
        // POST: /News/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(News news, List<NewsFiles> newsFiles, List<NewsImage> newsImages, List<NewsLink> links, int pclass, int[] newsCatalogID)
        {
            ModelState.Remove("newsCatalog");
            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }

            bool vaild = true;
            if (string.IsNullOrEmpty(news.Article))
            {
                ViewBag.Message = "內容不可為空";
                vaild = false;
            }

            if (Utility.IsFontSizePxpt(news.Article))
            {
                ViewBag.Message = "請切到原始碼模式，檢查是否包含font-size 標籤，若有不可採用PX或PT單位";
                vaild = false;
            }


            if (ModelState.IsValid && vaild)
            {


                foreach (var newsFile in newsFiles)
                {
                    if (!string.IsNullOrEmpty(newsFile.Subject) && !string.IsNullOrEmpty(newsFile.UpFile))
                        news.NewsFileses.Add(newsFile);
                }
                foreach (var link in links)
                {
                    if (!string.IsNullOrEmpty(link.Subject) && !string.IsNullOrEmpty(link.LinkUrl))
                        news.NewsLinks.Add(link);
                }

                foreach (var newsImage in newsImages)
                {
                    if (!string.IsNullOrEmpty(newsImage.Subject) && !string.IsNullOrEmpty(newsImage.UpFile))
                        news.NewsImagses.Add(newsImage);
                }

                news.IsConfirm=BooleanType.否;
                news.InitDate = DateTime.Now;
                news.Poster = member.Name;
                news.initOrg = member.MyUnit.ParentUnit.Subject + " " + member.MyUnit.Subject;
                news.WebCategoryId = pclass;
                news.OwnWebSiteId = member.MyUnit.ParentId.Value;
                news.MemberId = member.Id;
                List<NewsCatalog> catalogs = db.NewsCatalogs.Where(x => newsCatalogID.Contains(x.Id)).ToList();
                foreach (var catalog in catalogs)
                {
                    news.NewsCatalogs.Add(catalog);
                }


                db.Newses.Add(news);
                //news.Create(db,db.Newses); db.Newses.Add(news);
                SystemLog log = new SystemLog();
                log.InitDate = DateTime.Now;
                log.Poster = User.Identity.Name;
                log.Subject = "新增" + news.Subject;
                db.SystemLogs.Add(log);
                db.SaveChanges();
                return RedirectToAction("Edit", new {id=news.Id, pclass = pclass });
            }
           


            var newsCatalog = db.NewsCatalogs.Where(x => x.WebCategoryId == pclass).AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")) && webSite.Id != 1)
            {
                newsCatalog = newsCatalog.Where(x => x.WebSiteId == webSite.Id || x.WebSiteId == 1);
            }
            var newsCatalogs1 = newsCatalog.OrderBy(x => x.WebSiteId).ToList();

            ViewBag.newsCatalogs = newsCatalogs1;

            WebNewsCatalog webcatalog = db.WebNewsCatalogs.Find(pclass);

            ViewBag.Title = webcatalog.Subject;




            return View(news);
        }

        //
        // GET: /News/Edit/5

        public ActionResult Edit(int pclass, int type = 0, int id = 0)
        {

            News news = db.Newses.Find(id);

            if (news == null)
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

            var newsCatalogs = db.NewsCatalogs.Where(x => x.WebCategoryId == pclass).AsQueryable();
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                newsCatalogs = newsCatalogs.Where(x => x.WebSiteId == webSite.Id || x.WebSiteId == 1);
            }
            var newsCatalogs1 = newsCatalogs.OrderBy(x => x.WebSiteId).ToList();

            ViewBag.newsCatalogs = newsCatalogs1;

            WebNewsCatalog catalog = db.WebNewsCatalogs.Find(pclass);

            ViewBag.Title = catalog.Subject;
            ViewBag.type = type;
            ViewBag.SiteCode = webSite.SiteCode;
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            return View(news);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(News news, int pclass, int[] newsCatalogID)
        {
            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ModelState.Remove("newsCatalog");
            bool vaild = true;
            if (string.IsNullOrEmpty(news.Article))
            {
               TempData["Message"]= "內容不可為空";
                vaild = false;
            }

            if (Utility.IsFontSizePxpt(news.Article))
            {
                TempData["Message"] = "請切到原始碼模式，檢查是否包含font-size 標籤，若有不可採用PX或PT單位";
                vaild = false;
            }
            if (ModelState.IsValid && vaild)
            {
                //取得資料庫裏面原來的值
                var newsItem = db.Newses.Find(news.Id);

                //套用新的值
                db.Entry(newsItem).CurrentValues.SetValues(news);


                //放入新的值
                newsItem.NewsCatalogs.Clear();

               


                //db.Entry(news).State = EntityState.Modified;
                news.UpdateDate = DateTime.Now;
                news.Updater = member.Name;
                news.UpdateOrg = member.MyUnit.ParentUnit.Subject + " " + member.MyUnit.Subject;
                List<NewsCatalog> catalogs = db.NewsCatalogs.Where(x => newsCatalogID.Contains(x.Id)).ToList();
                foreach (var catalog in catalogs)
                {
                    newsItem.NewsCatalogs.Add(catalog);
                }

                db.Entry(newsItem).State = EntityState.Modified;
                SystemLog log = new SystemLog();
                log.InitDate = DateTime.Now;
                log.Poster = User.Identity.Name;
                log.Subject = "修改" + news.Subject;
                db.SystemLogs.Add(log);
                
                db.SaveChanges();
                return RedirectToAction("Index", new { Page = -1, pclass = pclass });
            }



            return RedirectToAction("Edit", new { id = news.Id, pclass = pclass });
        }

        //
        // GET: /News/Delete/5

        public ActionResult Delete(int pclass, int id = 0)
        {
            WebNewsCatalog catalog = db.WebNewsCatalogs.Find(pclass);

            ViewBag.Title = catalog.Subject;

            News news = db.Newses.Find(id);

            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.Newses.Find(id);

            for (int i = news.NewsFileses.Count - 1; i == 0; i--)
            {
                var item = news.NewsFileses.ElementAt(i);
                db.NewsFileses.Remove(item);
            }
            for (int i = news.NewsImagses.Count - 1; i == 0; i--)
            {
                var item = news.NewsImagses.ElementAt(i);
                db.NewsImages.Remove(item);
            }
            for (int i = news.NewsLinks.Count - 1; i == 0; i--)
            {
                var item = news.NewsLinks.ElementAt(i);
                db.NewsLinks.Remove(item);
            }


            db.Newses.Remove(news);
            SystemLog log = new SystemLog();
            log.InitDate = DateTime.Now;
            log.Poster = User.Identity.Name;
            log.Subject = "刪除" + news.Subject;
            db.SystemLogs.Add(log);
            db.SaveChanges();
            

            return RedirectToAction("Index", new { Page = -1, pclass = Request["pclass"] });
        }

        public ActionResult Review(int? page, FormCollection fc)
        {


            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
          

            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var newses = db.Newses.Where(x => x.MemberId==member.Id && x.IsConfirm==BooleanType.否).OrderByDescending(x => x.InitDate).AsQueryable();




            ViewBag.Title = "訊息審核";

            //ViewBag.Subject = getViewDateStr("SearchBySubject");
            return View(newses.ToPagedList(currentPageIndex, DefaultPageSize));
        }


        public ActionResult ReviewEdit( int id = 0)
        {

            News news = db.Newses.Find(id);

            if (news == null)
            {
                return HttpNotFound();
            }

            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            if (news.NewsCatalogs.Count > 0)
            {
                var pclass = news.NewsCatalogs.FirstOrDefault().WebCategoryId;
                var newsCatalogs = db.NewsCatalogs.Where(x => x.WebCategoryId == pclass).AsQueryable();

                var newsCatalogs1 = newsCatalogs.OrderBy(x => x.WebSiteId).ToList();

                ViewBag.newsCatalogs = newsCatalogs1;

            }


         
            

           
         
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ReviewEdit(int memberId,int Id=0)
        {
            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            News newsItem = db.Newses.Find(Id);
            
            newsItem.IsConfirm = BooleanType.是;
            newsItem.MemberId = member.Id;
            newsItem.AssignDateTime = DateTime.Now;
            newsItem.Update(db, db.Newses);

            return RedirectToAction("Review", new { Page = -1 });
        }

        public ActionResult Report()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Report(string SearchByStartDate, string SearchByEndDate)
        {


            string tempSql = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/NewsReport.sql"), System.Text.Encoding.UTF8);


            if (!string.IsNullOrEmpty(SearchByStartDate) && !string.IsNullOrEmpty(SearchByEndDate))
            {
                DateTime startDate = Convert.ToDateTime(SearchByStartDate);
                DateTime endDate = Convert.ToDateTime(SearchByEndDate).AddDays(1);
                tempSql = tempSql.Replace("2019/1/1", startDate.ToString("yyyy/MM/dd"));
                tempSql = tempSql.Replace("2019/3/1", endDate.ToString("yyyy/MM/dd"));
            }
            else
            {
                return View();
            }
            string tempBody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/NewsReport.xls"), System.Text.Encoding.UTF8);
            string temptr = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/NewsReport.txt"), System.Text.Encoding.UTF8);
            tempBody = tempBody.Replace("2019/1/1", SearchByStartDate);
            tempBody = tempBody.Replace("2019/3/1", SearchByEndDate);


            SqlConnection conn = new SqlConnection(GetConnectionStringByName("TnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = tempSql;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);





            tempBody = tempBody.Replace("{initDate}", SearchByStartDate + "~" + SearchByEndDate);


            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                string strTr = temptr;
                strTr = strTr.Replace("{Name}", row["Name"].ToString());
                string myUnit = row["initOrg"].ToString();
                string[] units = myUnit.Split(' ');


                strTr = strTr.Replace("{Unit}", units[0]);
                if (units.Length > 1)
                {
                    strTr = strTr.Replace("{Unit1}", units[1]);
                }
                else
                {
                    strTr = strTr.Replace("{Unit1}", "");
                }


                strTr = strTr.Replace("{Total}", row["Total"].ToString());

                sb.AppendLine(strTr);
            }
            tempBody = tempBody.Replace("{bodytr}", sb.ToString());

            string fileName = "NewsReportUse.xls";
            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + fileName), tempBody, System.Text.Encoding.UTF8);

            return File(Server.MapPath("/MailTemp/" + fileName), "application/msexcel", "動態發布統計.xls");

        }

        public ActionResult ReportByUnits()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReportByUnits(string SearchByStartDate, string SearchByEndDate)
        {


            string tempSql = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/NewsReportByUnits.sql"), System.Text.Encoding.UTF8);


            if (!string.IsNullOrEmpty(SearchByStartDate) && !string.IsNullOrEmpty(SearchByEndDate))
            {
                DateTime startDate = Convert.ToDateTime(SearchByStartDate);
                DateTime endDate = Convert.ToDateTime(SearchByEndDate).AddDays(1);
                tempSql = tempSql.Replace("2019/1/1", startDate.ToString("yyyy/MM/dd"));
                tempSql = tempSql.Replace("2019/3/1", endDate.ToString("yyyy/MM/dd"));
            }
            else
            {
                return View();
            }

            var units = db.Units.Where(x => x.ParentId == null).ToList();
            StringBuilder sqlBuilder = new StringBuilder(tempSql);
            StringBuilder sqlUnionBuilder = new StringBuilder("select * from #union1 ");

            for (int i = 1; i <= units.Count - 1;i++) {
                string ttsql = tempSql;
                ttsql = ttsql.Replace("OwnWebSiteId = 1", "OwnWebSiteId = " + units[i].Id.ToString());
                ttsql = ttsql.Replace("局本部", units[i].Subject.ToString());
                ttsql = ttsql.Replace("#union1", "#union" + (i+1));
                sqlBuilder.Append(ttsql);
                sqlUnionBuilder.Append(" union all \n select * from #union" +(i+1) +"\n");
            }

            sqlBuilder.Append(sqlUnionBuilder);

            string runSql = sqlBuilder.ToString();


            string tempBody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/NewsReportUnits.xls"), System.Text.Encoding.UTF8);
            string temptr = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/NewsReportUnits.txt"), System.Text.Encoding.UTF8);
            tempBody = tempBody.Replace("2019/1/1", SearchByStartDate);
            tempBody = tempBody.Replace("2019/3/1", SearchByEndDate);


            SqlConnection conn = new SqlConnection(GetConnectionStringByName("TnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = runSql;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);





            tempBody = tempBody.Replace("{initDate}", SearchByStartDate + "~" + SearchByEndDate);


            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                string strTr = temptr;
                strTr = strTr.Replace("{Unit}", row["Subject"].ToString());
                strTr = strTr.Replace("{CatalogName}", row["CatalogName"].ToString());
                

                strTr = strTr.Replace("{Total}", row["Total"].ToString());

                sb.AppendLine(strTr);
            }
            tempBody = tempBody.Replace("{bodytr}", sb.ToString());

            string fileName = "NewsReportUnitsTemp.xls";
            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + fileName), tempBody, System.Text.Encoding.UTF8);

            return File(Server.MapPath("/MailTemp/" + fileName), "application/msexcel", "單位動態發布統計.xls");

        }

        static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }




    }

}
