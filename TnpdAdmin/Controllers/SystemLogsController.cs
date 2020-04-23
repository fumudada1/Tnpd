using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using Tnpd.Filters;
using TnpdModels;

namespace Tnpd.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class SystemLogsController : _BaseController
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

            var newses = _db.SystemLogs.OrderByDescending(p => p.InitDate).AsQueryable();

            if (hasViewData("SearchByPoster"))
            {
                string searchByPoster = getViewDateStr("SearchByPoster");
                newses = newses.Where(w => w.Poster.Contains(searchByPoster));
            }

           

            if (hasViewData("SearchByStartDate") && hasViewData("SearchByEndDate"))
            {
                DateTime startDate = Convert.ToDateTime(getViewDateStr("SearchByStartDate"));
                DateTime endDate = Convert.ToDateTime(getViewDateStr("SearchByEndDate")).AddDays(1);
                newses = newses.Where(w => w.InitDate >= startDate && w.InitDate <= endDate);
            }

            //ViewBag.Subject = getViewDateStr("SearchBySubject");
            return View(newses.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }



        

        //
        // GET: /SystemLogs/Details/5

        public ActionResult Details(int id = 0)
        {
            SystemLog systemlog = _db.SystemLogs.Find(id);
            if (systemlog == null)
            {
                return HttpNotFound();
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


            string tempSql = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/systemlog.sql"), System.Text.Encoding.UTF8);

           
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
            string tempBody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/systemlog.xls"), System.Text.Encoding.UTF8);
            string temptr = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/systemlog.txt"), System.Text.Encoding.UTF8);
            tempBody = tempBody.Replace("2019/1/1", SearchByStartDate);
            tempBody = tempBody.Replace("2019/3/1", SearchByEndDate);

         
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("TnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = tempSql;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable() ;
            da.Fill(dt);




          
            tempBody = tempBody.Replace("{initDate}", SearchByStartDate + "~" + SearchByEndDate);

           
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                string strTr = temptr;
                strTr = strTr.Replace("{Name}", row["Name"].ToString());
                strTr = strTr.Replace("{Unit}", row["Unit1"].ToString());
                strTr = strTr.Replace("{Unit1}", row["Unit"].ToString());
                strTr = strTr.Replace("{Total}", row["Total"].ToString());
  
                sb.AppendLine(strTr);
            }
            tempBody = tempBody.Replace("{bodytr}", sb.ToString());

            string fileName = "systemlogUse.xls";
            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + fileName), tempBody, System.Text.Encoding.UTF8);

            return File(Server.MapPath("/MailTemp/" + fileName), "application/msexcel", "網站維護統計.xls");

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
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
