using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using tnpd.Filters;
using TnpdModels;

namespace tnpd.Controllers
{
    public class ServiceController : _BaseController
    {
        //
        // GET: /News/
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 10;

        [MyAuthorize]
        public ActionResult Index(Guid id, int? page, FormCollection fc)
        {

            ViewBag.UnId = id.ToString();
            int currentPageIndex = 0;
            //記住搜尋條件
            GetCatcheRoutes(page, fc);
            //取得正確的頁面
            currentPageIndex = getCurrentPage(page, fc);


            //ViewBag.SearchByCategoryId = sClass;




            //var eses = _db.Eses.Where(x => x.Catalogs.Count(y => y.WebSiteId == 1) > 0).OrderByDescending(p => p.InitDate).AsQueryable();
            var eses = _db.Eses.Where(x => x.Catalogs.Count(y => y.WebSiteId == 100) > 0).OrderByDescending(p => p.InitDate).AsQueryable();
            DateTime yesterDay = DateTime.Now.AddDays(-1);
            //newses = newses.Where(w => w.StartDate <= DateTime.Now && w.EndDate >= yesterDay && w.IsConfirm == BooleanType.是);


            //ViewBag.CategoryId = new SelectList(_db.NewsCatalogs.Where(x=>x.WebSiteId==1 && x.WebCategoryId==sClass).OrderBy(p => p.ListNum), "Id", "Subject");
          

            //ViewBag.Subject = getViewDateStr("SearchBySubject");
            return View(eses.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }


        public ActionResult Details(string unid, int id = 0)
        {
            ViewBag.UnId = unid;

            Ese ese = _db.Eses.Find(id);
            if (ese == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<EseFile> fileses = new List<EseFile>();
            foreach (EseFile escFile in ese.Fileses)
            {
                string filePath = Server.MapPath(Server.UrlDecode(escFile.UpFile));
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
                            EseFile file = new EseFile();
                            file.UpFile = file.UpFile.ToLower().Replace(".docx", ".pdf");
                            file.EseId = file.EseId;
                            file.ListNum = file.ListNum;
                            file.Subject = file.Subject + "(pdf)";
                            fileses.Add(file);
                        }
                        if (odfInfo.Exists)
                        {
                            EseFile file = new EseFile();
                            file.UpFile = file.UpFile.ToLower().Replace(".docx", ".odf");
                            file.EseId = file.EseId;
                            file.ListNum = file.ListNum;
                            file.Subject = file.Subject + "(odf)";
                            fileses.Add(file);
                        }

                    }
                }
            }
            foreach (EseFile newsFile in fileses)
            {
                ese.Fileses.Add(newsFile);
            }

            ese.Views = ese.Views + 1;
            ese.Update(_db, _db.Newses);

            EseCatalog catalog = ese.Catalogs.FirstOrDefault();
            ViewBag.Subject = "申辦服務";
            ViewBag.Theme = catalog.MetaIndex.Theme;
            ViewBag.Cake = catalog.MetaIndex.Cake;
            ViewBag.Service = catalog.MetaIndex.Service;


            return View(ese);
        }





    }
}