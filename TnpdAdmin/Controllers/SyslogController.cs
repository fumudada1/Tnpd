using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using Tnpd.Controllers;
using Tnpd.Filters;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class SyslogController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 15;
        //




        public ActionResult Index(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);


            var systemlogs = _db.SystemLogs.OrderByDescending(p => p.InitDate).AsQueryable();
            if (hasViewData("SearchByPoster"))
            {
                string searchByPoster = getViewDateStr("SearchByPoster");
                systemlogs = systemlogs.Where(w => w.Poster.Contains(searchByPoster));
            }
            if (hasViewData("SearchBySubject"))
            {
                string searchBySubject = getViewDateStr("SearchBySubject");
                systemlogs = systemlogs.Where(w => w.Subject.Contains(searchBySubject));
            }
            if (hasViewData("SearchByOrgs"))
            {
                int UnitId = getViewDateInt("SearchByOrgs");
                systemlogs = systemlogs.Where(w => w.Unitid==UnitId);
            }

            if (hasViewData("SearchByattr"))
            {
                string AttrStatus = getViewDateStr("SearchByattr");
                TnpdModels.SystemLogAttr searchByAttr =
                    (TnpdModels.SystemLogAttr)Enum.Parse(typeof(TnpdModels.SystemLogAttr), AttrStatus, false);
                systemlogs = systemlogs.Where(w => w.attr==searchByAttr);
            }
            if (hasViewData("SearchByEvent"))
            {
                string EventStatus = getViewDateStr("SearchByEvent");
                TnpdModels.SystemLogEvent searchByEvent =
                    (TnpdModels.SystemLogEvent)Enum.Parse(typeof(TnpdModels.SystemLogEvent), EventStatus, false);
                systemlogs = systemlogs.Where(w => w.Enent == searchByEvent);
            }
            if (hasViewData("SearchByStartDate") && hasViewData("SearchByEndDate"))
            {
                DateTime startDate = Convert.ToDateTime(getViewDateStr("SearchByStartDate"));
                DateTime endDate = Convert.ToDateTime(getViewDateStr("SearchByEndDate")).AddDays(1);
                systemlogs = systemlogs.Where(w => w.InitDate >= startDate && w.InitDate <= endDate);
            }
            var orgs = _db.Units.Where(x => x.ParentId == null).ToList();

            ViewBag.Orgs = new SelectList(orgs, "Id", "Subject");
            //            ViewBag.Subject = Subject;
            return View(systemlogs.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }





        //
        // GET: /Syslog/Details/5

        public ActionResult Details(int id = 0)
        {
            SystemLog systemlog = _db.SystemLogs.Find(id);
            if (systemlog == null)
            {
                //return HttpNotFound();
                return View();
            }
            return View(systemlog);
        }

        public ActionResult Report()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Report(string SearchByStartDate, string SearchByEndDate)
        {

            var systemlogs = _db.SystemLogs.OrderByDescending(p => p.InitDate).AsQueryable();
            


            if (!string.IsNullOrEmpty(SearchByStartDate) && !string.IsNullOrEmpty(SearchByEndDate))
            {
                DateTime startDate = Convert.ToDateTime(SearchByStartDate);
                DateTime endDate = Convert.ToDateTime(SearchByEndDate).AddDays(1);
                var ExportData = systemlogs.Where(w => w.InitDate >= startDate && w.InitDate <= endDate).Select(x=>new
                {
                    x.InitDate,
                    x.Subject,
                    x.Poster,
                    x.Org,
                    x.Enent,
                    x.attr
                }).ToList();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("日期,行為,姓名/帳號,單位,事件,屬性");
                foreach (var row in ExportData)
                {
                    foreach (PropertyInfo prop in row.GetType().GetProperties())
                    {
                        var propertyValue = prop.GetValue(row, null);

                        var cellValue =
                            propertyValue == null ?
                                "," : "\"" + prop.GetValue(row, null).ToString() + "\"" + ',';

                        sb.Append(cellValue);
                    }
                    sb.Append("\r\n");
                }
                return File(
                    Encoding.Default.GetBytes(
                        sb.ToString()),
                    "text/csv",
                    string.Format("Export-{0}.csv", DateTime.Now.ToString("yyyyMMdd-HHmmss")
                    ));
            }
            else
            {
                return View();
            }
           
            

        }

        
    

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
