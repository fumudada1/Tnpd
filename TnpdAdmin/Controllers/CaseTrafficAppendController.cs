using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tnpd.Models;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
   
    public class CaseTrafficAppendController : Controller
    {
        private BackendContext _db = new BackendContext();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
      
        /// </summary>
        /// <param name="id">檢舉案號</param>
        /// <param name="deadline">補正期限</param>
        /// <param name="memo">補正原因</param>
        /// <param name="Append">補正欄位</param>Guid.NewGuid().ToString();
        /// <returns>案件GUID</returns>
        [HttpPost]
        public ActionResult Index(string id, DateTime deadline, string memo, string Append)
        {
            CaseTraffic caseTraffic=_db.CaseTraffics.FirstOrDefault(x => x.CaseID == id);
            if (caseTraffic == null)
            {
                return Content("找不到案件");
            }

            
            if (string.IsNullOrEmpty(Append))
            {
                return Content("補正欄位必填");
            }

            StringBuilder sb = new StringBuilder();
            if (Append.ToLower().IndexOf("violation_carno")>-1)
            {
                sb.Append("violation_carno,");
            }
            if (Append.ToLower().IndexOf("violation_place")>-1)
            {
                sb.Append(" violation_place,");
            }
            if (Append.ToLower().IndexOf("violation_date")>-1)
            {
                sb.Append("violation_date,");
            }

            if (Append.ToLower().IndexOf("itemno")>-1)
            {
                sb.Append("itemno,");
            }

            if (Append.ToLower().IndexOf("upfile")>-1)
            {
                sb.Append("Upfile");
            }

            CaseTrafficAppend append = new CaseTrafficAppend();
            append.CaseID = caseTraffic.CaseID;
            append.MyCaseId = caseTraffic.Id;
            append.memo = memo;
            append.CaseTrafficAppendGuid = Utility.GetGUID(); ;
            append.deadline = deadline;
            append.Append = sb.ToString();
            append.InitDate=DateTime.Now;
            _db.CaseTrafficAppends.Add(append);
            _db.SaveChanges();
            return Content(append.CaseTrafficAppendGuid);
        }

    }
}
