using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using Newtonsoft.Json;
using tnpd.Controllers;
using tnpd.Filters;
using TnpdModels;

namespace tnpd.Areas.madou.Controllers
{
    public class FAQController : _BaseController
    {
        //
        // GET: /News/
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 10;

        [MyAuthorize]
        public ActionResult Index(Guid id, int? page, FormCollection fc)
        {
            string areaName = ControllerContext.RouteData.DataTokens["area"].ToString();

            ViewBag.UnId = id.ToString();
            int currentPageIndex = 0;
            //記住搜尋條件
            GetCatcheRoutes(page, fc);
            //取得正確的頁面
            currentPageIndex = getCurrentPage(page, fc);


            //ViewBag.SearchByCategoryId = sClass;




            var newses = _db.Newses.Where(x => x.NewsCatalogs.Count(y => y.WebSite.SiteCode==areaName) > 0 && x.NewsCatalogs.Count(y => y.WebCategoryId == 58) > 0).OrderByDescending(p => p.InitDate).AsQueryable();
            DateTime yesterDay = DateTime.Now.AddDays(-1);
            newses = newses.Where(w => w.StartDate <= DateTime.Now && w.EndDate >= yesterDay && w.IsConfirm == BooleanType.是);


            //ViewBag.CategoryId = new SelectList(_db.NewsCatalogs.Where(x=>x.WebSiteId==1 && x.WebCategoryId==sClass).OrderBy(p => p.ListNum), "Id", "Subject");
            ViewBag.CategoryId = 58;

            var CategoryCount = _db.NewsCatalogs.Count(x => x.WebCategoryId == 58 && x.WebSite.SiteCode == areaName);
            ViewBag.CategoryCount = CategoryCount;

            //ViewBag.Subject = getViewDateStr("SearchBySubject");
            return View(newses.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }


        public ActionResult Details(string unid, int sClass, int id = 0)
        {
            ViewBag.UnId = unid;
            ViewBag.sClass = sClass;
            News news = _db.Newses.Find(id);
            if (news == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<NewsFiles> fileses = new List<NewsFiles>();
            foreach (NewsFiles newsFiles in news.NewsFileses)
            {
                string filePath = Server.MapPath(Server.UrlDecode(newsFiles.UpFile));
                FileInfo myInfo = new FileInfo(filePath);
                if (myInfo.Exists)
                {
                    if (myInfo.Extension.ToLower() == ".docx")
                    {

                        // PDF 儲存位置
                        string targetpdf = filePath.ToLower().Replace(".docx", ".pdf");
                        string targetodf = filePath.ToLower().Replace(".docx", ".odf");
                        FileInfo pdfInfo = new FileInfo(targetpdf);
                        FileInfo odfInfo = new FileInfo(targetodf);
                        if (pdfInfo.Exists)
                        {
                            NewsFiles file = new NewsFiles();
                            file.UpFile = newsFiles.UpFile.ToLower().Replace(".docx", ".pdf");
                            file.NewId = newsFiles.NewId;
                            file.ListNum = newsFiles.ListNum;
                            file.Subject = newsFiles.Subject + "(pdf)";
                            fileses.Add(file);
                        }
                        if (odfInfo.Exists)
                        {
                            NewsFiles file = new NewsFiles();
                            file.UpFile = newsFiles.UpFile.ToLower().Replace(".docx", ".odf");
                            file.NewId = newsFiles.NewId;
                            file.ListNum = newsFiles.ListNum;
                            file.Subject = newsFiles.Subject + "(odf)";
                            fileses.Add(file);
                        }

                    }
                }
            }
            foreach (NewsFiles newsFile in fileses)
            {
                news.NewsFileses.Add(newsFile);
            }

            news.Views = news.Views + 1;
            news.Update(_db, _db.Newses);

            NewsCatalog catalog = news.NewsCatalogs.FirstOrDefault();
            //ViewBag.Subject = news.Subject;
            ViewBag.Theme = catalog.MetaIndex.Theme;
            ViewBag.Cake = catalog.MetaIndex.Cake;
            ViewBag.Service = catalog.MetaIndex.Service;


            return View(news);
        }





    }
}
